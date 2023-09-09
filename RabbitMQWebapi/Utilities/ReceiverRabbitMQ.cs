using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using RabbitMQWebapi.Models.ControllerModels;
using RabbitMQDemo.Data;
using System.Threading.Channels;
using Microsoft.EntityFrameworkCore;
using System;
using RabbitMQWebapi.Background;
using RabbitMQWebapi.Models.Configurations;
using RabbitMQWebapi.Models.RabbitMQ;
using System.Xml.Linq;

namespace RabbitMQWebapi.Utilities
{
    public class ReceiverRabbitMQ
    {
        private GeneralDbContext _generalDbContext;
        private readonly ILogger _logger;
        private Decloration _decloration;
        //private IConnection _connection;
        //private IModel _model;

        public ReceiverRabbitMQ(ILogger logger, Decloration decloration, GeneralDbContext generalDbContext)
        {
            _logger = logger;
            _decloration = decloration;
            _generalDbContext = generalDbContext;
        }
        public async void ReceiveData(IGeneralConfig _generalConfig, Decloration decloration)
        {

            var consumer = new EventingBasicConsumer(decloration.Model);
            string consumerTag = decloration.Model.BasicConsume(decloration.QueueName, autoAck: false, consumer);

            consumer.Received += async (ch, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    string message = Encoding.UTF8.GetString(body);
                    Customer customer = JsonConvert.DeserializeObject<Customer>(value: message);
                    await _generalDbContext.Customers.AddAsync(customer);
                    await _generalDbContext.SaveChangesAsync();
                    _logger.LogInformation($"Message Received: {message}");
                    _decloration.Model.BasicAck(ea.DeliveryTag, multiple: false);
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"Message Received: {ex.Message}");
                    _decloration.Model.BasicNack(ea.DeliveryTag, multiple: false, requeue: true);
                    throw;
                }

            };

        }
    }
}
