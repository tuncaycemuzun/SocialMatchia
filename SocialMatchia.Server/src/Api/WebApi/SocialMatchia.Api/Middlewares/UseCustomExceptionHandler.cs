using Ardalis.Result;
using Microsoft.AspNetCore.Diagnostics;
using SocialMatchia.Common.Exceptions;
using Newtonsoft.Json;

namespace SocialMatchia.Api.Middlewares
{
    public static class UseCustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                //TODO : Fail methodunu düzelt
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (exceptionFeature is null) return;

                    var statusCode = StatusCodes.Status500InternalServerError;

                    statusCode = exceptionFeature.Error switch
                    {
                        PropertyValidationException => StatusCodes.Status500InternalServerError,
                        NotFoundException => StatusCodes.Status404NotFound,
                        _ => statusCode,
                    };

                    var response = Result.Error(exceptionFeature.Error.Message);

                    if (exceptionFeature.Error is PropertyValidationException)
                    {
                        response = Result.Error(exceptionFeature.Error.Message.Split("##"));
                    }

                    var result = JsonConvert.SerializeObject(response);

                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = statusCode;

                    await context.Response.WriteAsync(result);

                });

            });

        }
    }
}
