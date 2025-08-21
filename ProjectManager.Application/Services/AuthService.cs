using Microsoft.Extensions.Configuration;
using ProjectManager.Application.Helpers;
using ProjectManager.Application.Interfaces.Services;
using ProjectManager.Application.Models;
using ProjectManager.Application.Models.Requests.Auth;
using ProjectManager.Domain.Exceptions;
using ProjectManager.Domain.Interfaces.Repositories;
using ProjectManager.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Services
{
    public class AuthService(IUserRepository userRepository, IConfiguration configuration) : IAuthService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IConfiguration _configuration = configuration;

        public GenericResponse<string> Login(LoginAuthRequest request)
        {
            try
            {
                var getUser = _userRepository.Get(request.EmailAddress);
                if (getUser == null)
                {
                    throw new UnauthorizedException("Correo o contraseña incorrectos");
                }

                if (getUser.IsActive == false)
                {
                    throw new UnauthorizedException(ResponseConsts.UserDeactivated);
                }

                var comparePassword = Hasher.ComparePassword(request.Password, getUser.Password);
                if (comparePassword == false)
                {
                    throw new UnauthorizedException("Correo o contraseña incorrectos");
                }

                return ResponseHelper.Create(TokenHelper.Create(getUser, _configuration), message: "Inició sesión correctamente");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
