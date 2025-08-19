using Microsoft.AspNetCore.Mvc;
using ProjectManager.Application.Helpers;
using ProjectManager.Domain.Context;

namespace ProjectManager.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(IConfiguration configuration) : ControllerBase
    {
        private readonly IConfiguration configuration = configuration;

        [HttpGet(Name = "GetWeatherForecast")]
        public string Get()
        {
            var user = new User
            {
                UserId = Guid.NewGuid()
            };

            return TokenHelper.Create(user, configuration);
        }
    }
}
