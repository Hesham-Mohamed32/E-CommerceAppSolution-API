using DomainLayer.Exceptions;
using Shared.ErrorModels;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text.Json;

namespace E_CommerceApp.MiddelWares
{
    public class GlobalExceptionHandlingMiddelWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public GlobalExceptionHandlingMiddelWare(RequestDelegate next , ILogger<GlobalExceptionHandlingMiddelWare> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try 
            {
                await _next(context);
                if (context.Response.StatusCode == StatusCodes.Status404NotFound)
                    await HandelNotFoundApiAsync(context);
            }
            catch(Exception ex)
            {
                _logger.LogError($"SomeThing Went Wrong ==> {ex.Message}");
                await HandelExeptionAsync(context, ex);
            }
        }

        private async Task HandelNotFoundApiAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            var Response = new ErrorDetails()
            {
                StatusCode = StatusCodes.Status404NotFound,
                ErrorMessage = $"The EndPoint With url {context.Request.Path} Not Found"
            }.ToString();
            await context.Response.WriteAsync(Response);
        }

        private async Task HandelExeptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            var response = new ErrorDetails
            {

                ErrorMessage = ex.Message
            };
            //Change StatesCode
            //context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedException => StatusCodes.Status401Unauthorized, 
                IdentityValidationException identityValidationException => HandelIdentityValidationException(identityValidationException,response),
                (_) => StatusCodes.Status500InternalServerError
            };

            //Change Content Type 

            //Write Response In Body 
            //  StatusCode = context.Response.StatusCode,

            response.StatusCode = context.Response.StatusCode;
             await context.Response.WriteAsync(response.ToString());
        }

        private int HandelIdentityValidationException(IdentityValidationException identityValidationException, ErrorDetails response)
        {
           response.Errors = identityValidationException.Errors;
            return StatusCodes.Status400BadRequest;
        }
    }
}
