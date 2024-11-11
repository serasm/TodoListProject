using System.Text.Json;
using Microsoft.AspNetCore.Http.Extensions;
using TodoList.Application.Exceptions;

namespace TodoList.Api.Middlewares;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    
    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, Serilog.ILogger logger)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            if (!context.Response.HasStarted)
            {
                var errorModel = ex.ToErrorModel(context.Request.GetDisplayUrl());
                
                if(ex is IHasHttpCode hasHttpCode)
                    context.Response.StatusCode = (int)hasHttpCode.StatusCode;
                else
                    context.Response.StatusCode = 500;
                    
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(errorModel, JsonSerializerOptions.Default);
                logger.LogException(ex, errorModel);
            }
        }
    }
}