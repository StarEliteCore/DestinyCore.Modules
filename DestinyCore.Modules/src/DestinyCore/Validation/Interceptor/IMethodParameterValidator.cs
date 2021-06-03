using System.Collections.Generic;

namespace DestinyCore.Validation.Interceptor
{
    public interface IMethodParameterValidator
    {
        IEnumerable<ValidationFailure> Validate(object parameter);
    }
}
