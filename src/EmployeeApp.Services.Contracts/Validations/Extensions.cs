using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EmployeeApp.Services.Contracts.Validations;

public static class Extensions
{
    public static void AddToModelState(this ValidationResult result, ModelStateDictionary modelState,
        string requestName)
    {
        foreach (ValidationFailure? error in result.Errors)
        {
            modelState.AddModelError($"{requestName}.{error.PropertyName}", error.ErrorMessage);
        }
    }
}