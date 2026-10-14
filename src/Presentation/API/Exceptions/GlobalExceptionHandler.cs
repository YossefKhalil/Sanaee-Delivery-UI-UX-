using Core.Application.Common.Exceptions;
using Core.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Api.Exceptions
{
    public sealed class GlobalExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            ProblemDetails problem = exception switch
            {
                ValidationException ex => new ValidationProblemDetails(
                                             ex.Errors.GroupBy(g => g.PropertyName)
                                                      .ToDictionary(g => g.Key,
                                                                    g => g.Select(e => e.ErrorMessage).ToArray()
                                                                    )
                                        )
                {
                    Title = "Validation Failed",
                    Status = StatusCodes.Status400BadRequest
                },

                NotFoundException ex => new ProblemDetails
                {
                    Title = "Resource not found!",
                    Detail = ex.Message,
                    Status = StatusCodes.Status404NotFound
                },

                BadRequestException ex => new ProblemDetails
                {
                    Title = "Bad request sent!",
                    Detail = ex.Message,
                    Status = StatusCodes.Status400BadRequest
                },

                ConflictException ex => new ProblemDetails
                {
                    Title = "Conflict!",
                    Detail = ex.Message,
                    Status = StatusCodes.Status409Conflict
                },

                InvalidOperationException ex => new ProblemDetails
                {
                    Title = "Invalid Operation!",
                    Detail = ex.Message,
                    Status = StatusCodes.Status500InternalServerError
                },

                DomainException ex => new ProblemDetails
                {
                    Title = "Domain Exception!",
                    Detail = ex.Message,
                    Status = StatusCodes.Status406NotAcceptable
                },

                _ => new ProblemDetails
                {
                    Title = "Server Error!",
                    Detail = "An unhandled exception!",
                    Status = StatusCodes.Status500InternalServerError
                }
            };

            httpContext.Response.StatusCode = problem.Status!.Value;

            await problemDetailsService.WriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                ProblemDetails = problem
            });

            return true;
        }
    }
}