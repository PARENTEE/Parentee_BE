using Parentee_BE.DAL.Data.Metadatas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Parentee_BE.ActionFilters;

// ref: https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions-1/controllers-and-routing/understanding-action-filters-cs
public class ValidAttributeActionFilter : ActionFilterAttribute
{
    // This method is called BEFORE a controller action is executed.
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine("OnActionExecuting-----------------------");
        var errors = new Dictionary<string, string[]>();
        // Always run this validation, even if the model state is invalid
        if (context.ModelState.IsValid) return;
        foreach (var (key, value) in context.ModelState)
        {
            var errorMessages = new List<string>();

            foreach (var error in value.Errors)
            {
                if (!string.IsNullOrEmpty(error.ErrorMessage))
                {
                    errorMessages.Add(error.ErrorMessage);
                }
                else if (error.Exception != null)
                {
                    // Check for specific exceptions related to invalid data types
                    if (error.Exception is FormatException or InvalidCastException)
                    {
                        string propertyName = key.Split('.').Last();
                        errorMessages.Add($"The {propertyName} field has an invalid value type.");
                    }
                    else
                    {
                        errorMessages.Add(error.Exception.Message);
                    }
                }
            }

            // Add default required field message if no errors were added but the field is invalid
            if (errorMessages.Count == 0 && value.ValidationState == ModelValidationState.Invalid)
            {
                string propertyName = key.Split('.').Last();
                errorMessages.Add($"The {propertyName} field is required.");
            }

            if (errorMessages.Count != 0)
            {
                errors[key] = errorMessages.ToArray();
            }
        }

        var response = ApiResponseBuilder.BuildErrorResponse(
            data: errors,
            message: "Validation failed",
            statusCode: StatusCodes.Status400BadRequest,
            reason: "One or more validation errors occurred"
        );

        context.Result = new BadRequestObjectResult(response);
    }
}