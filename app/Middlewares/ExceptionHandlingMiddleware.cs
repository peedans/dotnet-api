namespace dotnetapi.Middlewares;

using System.Diagnostics;
using System.Net;
using System.Text.Json;
using dotnetapi.ViewModels.Base;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<ExceptionHandlingMiddleware> logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var response = context.Response;

        var errorResponse = new BaseResponseView<string>
        {
            success = false
        };
        switch (exception)
        {
            case ApplicationException ex:
                if (ex.Message.Contains("Invalid Token"))
                {
                    response.StatusCode = (int)HttpStatusCode.Forbidden;
                    errorResponse.error_message = ex.Message;
                    errorResponse.error_code = response.StatusCode;
                    break;
                }
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.error_message = ex.Message;
                errorResponse.error_code = response.StatusCode;
                break;
            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorResponse.error_message = exception?.Message.ToString()!;
                errorResponse.error_code = response.StatusCode;

                if (errorResponse.error_message!.Contains("::") == true)
                {
                    errorResponse.error_message = errorResponse.error_message.Split("::").LastOrDefault()!;  
                }
                break;
        }

        var st = new StackTrace(exception, true);
        // Get the top stack frame
        var frame = st.GetFrame(0);
        // Get the line number from the stack frame
        var line = frame.GetFileLineNumber();
        var file = frame.GetFileName();

        logger.LogError("Error: " + exception.Message + ", File: " + file + ", Line: " + line);
        // logger.LogError(exception!.Message);
        var result = JsonSerializer.Serialize(errorResponse);
        await context.Response.WriteAsync(result);
    }
}