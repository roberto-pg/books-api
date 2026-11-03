using System.ComponentModel.DataAnnotations;

namespace Books.Filters;

public static class Validate
{
    public static bool TryValidate<T>(T model, out List<ValidationResult> results)
    {
        results = new();

        if (model is null)
        {
            results.Add(new ValidationResult("Model cannot be null"));
            return false;
        }

        var context = new ValidationContext(model);
        return Validator.TryValidateObject(model, context, results, true);
    }
}