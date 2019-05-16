using Plus.Collections;
using Plus.Runtime.Validation.Interception;
using System;
using System.Collections.Generic;

namespace Plus.Configuration.Startup
{
    public class ValidationConfiguration : IValidationConfiguration
    {
        public List<Type> IgnoredTypes
        {
            get;
        }

        public ITypeList<IMethodParameterValidator> Validators
        {
            get;
        }

        public ValidationConfiguration()
        {
            IgnoredTypes = new List<Type>();
            Validators = new TypeList<IMethodParameterValidator>();
        }
    }
}