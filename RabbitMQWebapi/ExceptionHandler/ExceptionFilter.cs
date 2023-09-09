using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;
using RabbitMQWebapi.Models.BaseModels;
using Microsoft.Extensions.Logging;
using RabbitMQWebapi.Background;

namespace RabbitMQWebapi.ExceptionHandler
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;
        public ExceptionFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            var loggerFactory = _serviceProvider.GetRequiredService<ILoggerFactory>();
            _logger = loggerFactory.CreateLogger<Worker>();
        }
        public void OnException(ExceptionContext exceptionContext)
        {
            var error = new GeneralError();
            switch (exceptionContext.Exception)
            {
                case InvalidOperationException:
                    _logger.LogError($"Hata: {exceptionContext.Exception.GetType().FullName} ->" + exceptionContext.Exception.Message);
                    //
                    break;
                default:
                    _logger.LogError($"Hata: {exceptionContext.Exception.GetType().FullName} ->" + exceptionContext.Exception.Message);
                    break;
            }

            exceptionContext.Result = new JsonResult(error)
            {
                StatusCode = 1000,
            };
        }
    }
}
