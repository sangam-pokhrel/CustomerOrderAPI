using CustomerOrder.DTO;
using FluentValidation.Results;

namespace CustomerOrder.Common.Helpers
{
    public static class Extentions
    {
        public static IEnumerable<object> ToValidationMessage(this List<ValidationFailure> errors)
        {
            return errors.Select(x => new ValidationErrorResponse
            {
                Property = x.PropertyName,
                ErrorMessage = x.ErrorMessage
            });
        }
    }
}