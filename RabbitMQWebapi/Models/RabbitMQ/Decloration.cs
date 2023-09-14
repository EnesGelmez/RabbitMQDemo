using RabbitMQ.Client;
using System;

namespace RabbitMQWebapi.Models.RabbitMQ
{
    public class Decloration
    {
        public Decloration(string uri, string clientProvideName, string exchangeName, string queueName, string routingKey)
        {
            Console.WriteLine("Decloration");
            Factory = new();
            Factory.Uri = new Uri(uri);
            Factory.ClientProvidedName = clientProvideName;
            Connection = Factory.CreateConnection();
            ExchangeName = exchangeName;
            QueueName = queueName;
            RoutingKey = routingKey;
            Model = Connection.CreateModel();
            Model.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            Model.QueueDeclare(queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            Model.QueueBind(queueName, exchangeName, routingKey, arguments: null);
            Model.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
        }
        public string ExchangeName { get; set; }
        public string QueueName { get; set; }
        public string RoutingKey { get; set; }
        public ConnectionFactory Factory { get; set; }
        public IConnection Connection { get; set; }
        public IModel Model { get; set; }
    }
}
