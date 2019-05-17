using Plus.Dependency;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Plus.Runtime.Validation.Interception
{
    /// <summary>
    /// ValidatableObjectValidator
    /// </summary>
    public class ValidatableObjectValidator : IMethodParameterValidator, ITransientDependency
    {
        public virtual IReadOnlyList<ValidationResult> Validate(object validatingObject)
        {
            List<ValidationResult> list = new List<ValidationResult>();
            IValidatableObject validatableObject;
            if ((validatableObject = (validatingObject as IValidatableObject)) != null)
            {
                list.AddRange(validatableObject.Validate(new ValidationContext(validatableObject)));
            }
            return list;
        }
    }
}