using RabbitMQ.Client;
using System.Text;


ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new Uri(uriString: "amqp://guest:guest@localhost:5672");
factory.ClientProvidedName = "Rabbit Sender App";


IConnection connection = factory.CreateConnection();
IModel model = connection.CreateModel();

string exchangeName = "DemoExchange";
string routingKey = "demo-routing-key";
string queueName = "DemoQueue";

model.ExchangeDeclare(exchangeName, ExchangeType.Direct);
model.QueueDeclare(queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
model.QueueBind(queueName, exchangeName, routingKey, arguments: null);

for (int i = 1; i <= 60; i++)
{
    Console.WriteLine($"Sending Message {i}");
    byte[] messageBodyBytes = Encoding.UTF8.GetBytes($"Message  #{i}");
    model.BasicPublish(exchangeName, routingKey, basicProperties: null, messageBodyBytes);
    Thread.Sleep(1000);
}

model.Close();
connection.Close();