using AppWebGenesisFE.Models.Exceptions;
using AppWebGenesisFE.Models.Models;
using System.Net;
using System.Text.Json;

namespace AppWebGenesisFE.ApiService.Middleware
{
    // <summary>
    /// Middleware para el manejo centralizado de excepciones
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error no manejado");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var errorResponse = new ErrorResponse
            {
                Success = false,
                Message = exception.Message,
                ErrorCode = "INTERNAL_ERROR"
            };

            switch (exception)
            {
                case ValidationException validationException:
                    response.StatusCode = validationException.StatusCode;
                    errorResponse.Message = validationException.Message;
                    errorResponse.ErrorCode = validationException.ErrorCode;
                    errorResponse.Errors = validationException.Errors;
                    break;

                case NotFoundException notFoundException:
                    response.StatusCode = notFoundException.StatusCode;
                    errorResponse.Message = notFoundException.Message;
                    errorResponse.ErrorCode = notFoundException.ErrorCode;
                    break;

                case ApiException apiException:
                    response.StatusCode = apiException.StatusCode;
                    errorResponse.Message = apiException.Message;
                    errorResponse.ErrorCode = apiException.ErrorCode;
                    break;

                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonSerializer.Serialize(errorResponse);
            await response.WriteAsync(result);
        }
    }
}
