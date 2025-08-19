using ProjectManager.Application.Models;
using ProjectManager.Application.Models.Requests.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Interfaces.Services
{
    public interface IAuthService
    {
        GenericResponse<string> Login(LoginAuthRequest request);
    }
}
