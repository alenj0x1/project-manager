using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Domain.Exceptions
{
    public class ArgumentNotFoundException : Exception
    {
        public ArgumentNotFoundException() { }
        public ArgumentNotFoundException(string message) : base(message) { }
    }
}
