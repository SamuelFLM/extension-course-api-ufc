using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace api_rest.Extensions
{
    public static class ModelStateExtension
    {
        public static List<string> GetErrorMessage(this ModelStateDictionary dictionary)
        {
            return dictionary.SelectMany(m => m.Value.Errors)
                .Select(m => m.ErrorMessage)
                .ToList();
        }
    }
}
