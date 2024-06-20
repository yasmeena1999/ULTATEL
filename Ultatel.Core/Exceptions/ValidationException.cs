using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace Ultatel.Core.Exceptions
{
    public class ValidationException:Exception
    {
        public List<string> ValidationErrors { get; set; }

        public ValidationException(IEnumerable<string> errors)
        {
            ValidationErrors = new List<string>(errors);
        }
    }
}
