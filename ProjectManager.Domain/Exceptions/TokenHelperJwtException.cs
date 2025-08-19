using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Domain.Exceptions
{
    public class TokenHelperJwtException : Exception
    {
        public TokenHelperJwtException() { }
        public TokenHelperJwtException(string message) : base(message) { }
    }
}
