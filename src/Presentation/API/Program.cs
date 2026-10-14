using Core.Application;
using Api.Exceptions;
using Persistence;
using API.Hubs;
//using Persistence.Seed;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();


// 1. إضافة دعم الـ Controllers و Swagger
builder.Services.AddControllers();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // تفعيل تحويل الـ Enums من وإلى النصوص تلقائياً
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

// 2. تسجيل خدمات طبقة الـ Application (MediatR & FluentValidation)
builder.Services.AddApplicationServices();

// 3. تسجيل خدمات الـ Persistence
builder.Services.AddInfrastructureServices(builder.Configuration);

// 4. إضافة خدمات SignalR
builder.Services.AddSignalR();

var app = builder.Build();

// 2. ربط الـ Hub برابط معين للـ Client
app.MapHub<ChatHub>("/hubs/chat");

app.UseExceptionHandler();

//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    var logger = services.GetRequiredService<ILogger<Program>>();

//    try
//    {
//        var context = services.GetRequiredService<AppDbContext>();
//        await DbInitializer.SeedAsync(context, logger);
//        logger.LogInformation("Sanaee_Delivery database seeded successfully!"); 
//    }
//    catch (Exception ex)
//    {
//        logger.LogError(ex, "An error occurred while seeding the database.");
//    }
//}


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();