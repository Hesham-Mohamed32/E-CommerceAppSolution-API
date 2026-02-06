using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace E_CommerceApp.Factories
{
    public class ApiResponseFactory
    {
        public static IActionResult CustomValidationErrorResponse(ActionContext context)
        {
            //context ==> Errors , [Key] Field 
            //context.modelstate<string,modelStateEnry> 
            //string == the Name Of Field 
            //modelStateEntry ==> errors ==> Error Messages 
            var errors = context.ModelState
                    .Where(error => error.Value?.Errors.Any() == true).Select(error => new ValidationError()
                    {
                        Field = error.Key,
                        Errors = error.Value?.Errors.Select(error => error.ErrorMessage) ?? new List<string>()
                    });
            var Response = new ValidationErrorResponse()
            {
                Errors = errors,
                StatesCode = StatusCodes.Status400BadRequest,
                ErrorMessage = "one or more validation error happend"
            };
            return new BadRequestObjectResult(Response);
        }
    }
}
