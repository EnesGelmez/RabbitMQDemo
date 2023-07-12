using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;


ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new Uri(uriString: "amqp://guest:guest@localhost:5672");
factory.ClientProvidedName = "Rabbit Receiver App 1";


IConnection connection = factory.CreateConnection();
IModel model = connection.CreateModel();

string exchangeName = "DemoExchange";
string routingKey = "demo-routing-key";
string queueName = "DemoQueue";

model.ExchangeDeclare(exchangeName, ExchangeType.Direct);
model.QueueDeclare(queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
model.QueueBind(queueName, exchangeName, routingKey, arguments: null);
model.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

var consumer = new EventingBasicConsumer(model);
consumer.Received += Consumer_Received;

string consumerTag = model.BasicConsume(queueName, autoAck: false, consumer);
Console.ReadLine();
model.BasicCancel(consumerTag);
model.Close();
connection.Close();


void Consumer_Received(object? sender, BasicDeliverEventArgs e)
{
    Task.Delay(TimeSpan.FromSeconds(5)).Wait();
    var body = e.Body.ToArray();
    string message = Encoding.UTF8.GetString(body);

    if (!message.Contains("12"))
    {
        Console.WriteLine(value: $"Message Received: {message}");
        model.BasicAck(e.DeliveryTag, multiple: false);
    }
    else
    {
        Console.WriteLine(value: $"Message Rejected: {message}");
        model.BasicNack(e.DeliveryTag, multiple: false, requeue: false);
    }
}
