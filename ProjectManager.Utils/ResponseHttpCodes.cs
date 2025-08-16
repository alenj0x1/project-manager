using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Utils
{
    public static class ResponseHttpCodes
    {
        public const int Success = 200;
        public const int Created = 201;
        public const int BadRequest = 400;
        public const int NotFound = 404;
        public const int Unauthorized = 401;
        public const int Forbidden = 403;
        public const int InternalServerError = 500;
    }
}
