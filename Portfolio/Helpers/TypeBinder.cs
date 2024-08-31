using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Portfolio.Helpers
{
    public class TypeBinder<T> : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var propertyName = bindingContext.ModelName;    
            var valueProvider = bindingContext.ValueProvider
                .GetValue(propertyName);

            if (valueProvider == ValueProviderResult.None) 
                return Task.CompletedTask;

            try
            {
                var deserializedValue = JsonConvert
                    .DeserializeObject<T>(valueProvider.FirstValue);
                bindingContext.Result = ModelBindingResult.Success(deserializedValue);
            }
            catch 
            {
                bindingContext.ModelState
                    .TryAddModelError(propertyName, "Invalid value for type List<int>");
            }
        
            return Task.CompletedTask;
        }
    }
}
