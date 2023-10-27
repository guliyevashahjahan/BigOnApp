using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Infrastructure.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message)
            : base(message) { }
       
        public NotFoundException(string message, IDictionary<string, IEnumerable<string>> errors)
         : base(message)
        {
            this.Errors = errors;
        }
        public NotFoundException(string message, IDictionary<string, IEnumerable<string>> errors, Exception innerException)
            : base(message, innerException)
        {
            this.Errors = errors;
        }

        public IDictionary<string, IEnumerable<string>> Errors { get; }
    }
}
