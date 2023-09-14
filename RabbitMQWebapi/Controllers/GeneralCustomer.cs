using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQDemo.Data;
using RabbitMQWebapi.Models.ControllerModels;
using RabbitMQWebapi.Models.ResponseModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Text;
using Newtonsoft.Json;
using RabbitMQWebapi.Models.Configurations;
using RabbitMQWebapi.Models.RabbitMQ;

namespace RabbitMQWebapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeneralCustomer : BaseController
    {
        [HttpPost]
        public async Task<GeneralResponse<string>> AddCustomer(Customer customer)
        {
            GeneralResponse<string> generalResponse = new GeneralResponse<string>();
            string uri = "amqp://guest:guest@localhost:5672";
            string clientProvideName = "Worker ReceiverRabbitMQ";
            string exchangeName = "AddCustomer";
            string queueName = "AddCustomerMethod";
            string routingKey = "RabbitMQDemo_Key";
            Decloration decloration = new Decloration(uri, clientProvideName, exchangeName, queueName, routingKey);

            //decloration.Model.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            //decloration.Model.QueueDeclare(queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            //decloration.Model.QueueBind(queueName, exchangeName, routingKey, arguments: null);

            Logger.LogInformation($"Sending Message");
            byte[] messageBodyBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(customer));
            decloration.Model.BasicPublish(exchangeName, routingKey, basicProperties: null, messageBodyBytes);
            //Thread.Sleep(1000);
            generalResponse.Data = "İşleminiz sıraya alımıştır.";
            return await Task.FromResult<GeneralResponse<string>>(generalResponse);

        }
    }
}
