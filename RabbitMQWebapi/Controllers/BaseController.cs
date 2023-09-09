using Microsoft.AspNetCore.Mvc;
using RabbitMQWebapi.Models.Configurations;
using RabbitMQ.Client;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RabbitMQWebapi.Controllers
{
    public class BaseController : ControllerBase
    {
        public BaseController()
        {
        }


        private IServiceProvider? _serviceProvider;
        private ILogger<ControllerBase>? _logger;
        private IGeneralConfig? _generalConfig;
        protected IServiceProvider ServiceProvider => _serviceProvider ?? (_serviceProvider = HttpContext.RequestServices);
        protected ILogger<ControllerBase> Logger
        {
            get
            {
                if (_logger == null)
                {
                    ILoggerFactory loggerFactory = HttpContext.RequestServices.GetService<ILoggerFactory>();
                    _logger = loggerFactory.CreateLogger<ControllerBase>();
                }
                return _logger;
            }
        }

        protected IGeneralConfig GeneralConfig => _generalConfig ??= HttpContext.RequestServices.GetServices<IGeneralConfig>().FirstOrDefault();
    }
}
