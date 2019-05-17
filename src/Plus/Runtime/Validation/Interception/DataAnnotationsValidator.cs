using Plus.Dependency;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Plus.Runtime.Validation.Interception
{
    /// <summary>
    /// DataAnnotationsValidator
    /// </summary>
    public class DataAnnotationsValidator : IMethodParameterValidator, ITransientDependency
    {
        public virtual IReadOnlyList<ValidationResult> Validate(object validatingObject)
        {
            return GetDataAnnotationAttributeErrors(validatingObject);
        }

        protected virtual List<ValidationResult> GetDataAnnotationAttributeErrors(object validatingObject)
        {
            var validationErrors = new List<ValidationResult>();

            var properties = TypeDescriptor.GetProperties(validatingObject).Cast<PropertyDescriptor>();
            foreach (var property in properties)
            {
                var validationAttributes = property.Attributes.OfType<ValidationAttribute>().ToArray();
                if (validationAttributes.IsNullOrEmpty())
                {
                    continue;
                }

                var validationContext = new ValidationContext(validatingObject)
                {
                    DisplayName = property.DisplayName,
                    MemberName = property.Name
                };

                foreach (var attribute in validationAttributes)
                {
                    var result = attribute.GetValidationResult(property.GetValue(validatingObject), validationContext);
                    if (result != null)
                    {
                        validationErrors.Add(result);
                    }
                }
            }

            return validationErrors;
        }
    }
}