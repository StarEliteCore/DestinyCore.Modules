﻿using DestinyCore.Ui;
using System.Collections.Generic;
using System.Linq;

namespace DestinyCore.Validation
{
    public static class ModelValidationResultExtensions
    {
        public static OperationResponse<IEnumerable<ValidationFailure>> ToResult(this IEnumerable<ValidationFailure> failures)
        {

            failures = failures as ValidationFailure[] ?? failures.ToArray();
            return !failures.Any() ? new OperationResponse<IEnumerable<ValidationFailure>>() : new OperationResponse<IEnumerable<ValidationFailure>>(Enums.OperationResponseType.Error) { Data = failures };
        }
    }
}
