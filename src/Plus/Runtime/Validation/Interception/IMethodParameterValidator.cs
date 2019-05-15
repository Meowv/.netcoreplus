using Plus.Dependency;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Plus.Runtime.Validation.Interception
{
    public interface IMethodParameterValidator : ITransientDependency
    {
        IReadOnlyList<ValidationResult> Validate(object validatingObject);
    }
}