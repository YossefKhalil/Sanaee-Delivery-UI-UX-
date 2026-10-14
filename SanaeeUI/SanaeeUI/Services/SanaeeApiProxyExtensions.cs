using System.Net.Http.Headers;

namespace SanaeeUI.Services;

// Reverse proxy (BFF) that forwards this app's /api/** requests to the real
// Sanaee-Delivery API. This lets the WebAssembly client call the API same-origin
// (through this server) so the browser's CORS policy is never hit — the server
// talks to the API server-to-server. The original Sanaee code is not touched.
public static class SanaeeApiProxyExtensions
{
    private static readonly string[] Methods = { "GET", "POST", "PUT", "PATCH", "DELETE", "HEAD", "OPTIONS" };

    public static IEndpointRouteBuilder MapSanaeeApiProxy(this IEndpointRouteBuilder app)
    {
        app.MapMethods("/api/{**path}", Methods, HandleAsync);
        return app;
    }

    private static async Task HandleAsync(HttpContext context, IHttpClientFactory factory)
    {
        var path = context.Request.RouteValues["path"]?.ToString() ?? string.Empty;
        var client = factory.CreateClient(SanaeeApiProxyClient.Name);

        var target = client.BaseAddress is null
            ? null
            : new Uri(client.BaseAddress, "api/" + path + context.Request.QueryString);
        if (target is null)
        {
            context.Response.StatusCode = StatusCodes.Status502BadGateway;
            await context.Response.WriteAsync("Sanaee API base URL is not configured.");
            return;
        }

        using var request = new HttpRequestMessage(new HttpMethod(context.Request.Method), target);

        // Forward request headers (except Host / content-length which the handler manages).
        foreach (var header in context.Request.Headers)
        {
            if (header.Key.Equals("Host", StringComparison.OrdinalIgnoreCase)) continue;
            if (header.Key.Equals("Content-Length", StringComparison.OrdinalIgnoreCase)) continue;
            request.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
        }

        // Forward the request body for POST/PUT/PATCH.
        if (!HttpMethods.IsGet(context.Request.Method) && !HttpMethods.IsHead(context.Request.Method))
        {
            request.Content = new StreamContent(context.Request.Body);
            if (!string.IsNullOrEmpty(context.Request.ContentType))
            {
                request.Content.Headers.TryAddWithoutValidation("Content-Type", context.Request.ContentType);
            }
        }

        try
        {
            using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.RequestAborted);

            context.Response.StatusCode = (int)response.StatusCode;
            foreach (var header in response.Headers)
            {
                context.Response.Headers[header.Key] = header.Value.ToArray();
            }
            foreach (var header in response.Content.Headers)
            {
                context.Response.Headers[header.Key] = header.Value.ToArray();
            }
            // Let the framework handle chunking/transfer-encoding itself.
            context.Response.Headers.Remove("transfer-encoding");

            await response.Content.CopyToAsync(context.Response.Body, context.RequestAborted);
        }
        catch (HttpRequestException ex)
        {
            // API unreachable (not running / wrong port). Surface a clear 502 so the
            // client can show a friendly error instead of a network exception.
            context.Response.StatusCode = StatusCodes.Status502BadGateway;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new { error = "Sanaee API unreachable", detail = ex.Message });
        }
    }
}

public static class SanaeeApiProxyClient
{
    public const string Name = "SanaeeApiProxy";
}
