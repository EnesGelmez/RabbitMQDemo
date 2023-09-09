//using RabbitMQ.Client;
//using System.Text;


//ConnectionFactory factory = new ConnectionFactory();
//factory.Uri = new Uri(uriString: "amqp://guest:guest@localhost:5672");
//factory.ClientProvidedName = "Rabbit Sender App";


//IConnection connection = factory.CreateConnection();
//IModel model = connection.CreateModel();

//string exchangeName = "DemoExchange";
//string routingKey = "demo-routing-key";
//string queueName = "DemoQueue";

//model.ExchangeDeclare(exchangeName, ExchangeType.Direct);
//model.QueueDeclare(queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
//model.QueueBind(queueName, exchangeName, routingKey, arguments: null);

//for (int i = 1; i <= 60; i++)
//{
//    Console.WriteLine($"Sending Message {i}");
//    byte[] messageBodyBytes = Encoding.UTF8.GetBytes($"Message  #{i}");
//    model.BasicPublish(exchangeName, routingKey, basicProperties: null, messageBodyBytes);
//    Thread.Sleep(1000);
//}

//model.Close();
//connection.Close();




using RabbitMQ.Client;
using RestSharp;
internal class Program
{

    private static void Main(string[] args)
    {
        Console.Out.WriteLineAsync("test");
        test();
        Task.Run(() => test());
        //while (true)
        //{
        //    Console.Out.WriteLineAsync("test");
        //    test();
        //    //Task.Run(() => test());
        //}

    }

    public static async void test()
    {
        var options = new RestClientOptions("//api/GeneralCustomer")
        {
            MaxTimeout = -1,
        };
        var client = new RestClient(options);
        var request = new RestRequest("", Method.Post);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Accept", "text/plain");
        var body = @"{" + "\n" +
        @"  ""id"": 54521829," + "\n" +
        @"  ""name"": ""reprehenderit ex ipsum Excepteur tempor""," + "\n" +
        @"  ""surname"": ""eu cupidatat""," + "\n" +
        @"  ""mail"": ""velit non deserunt adipisicing""" + "\n" +
        @"}";
        request.AddStringBody(body, DataFormat.Json);
        RestResponse response = await client.ExecuteAsync(request);
        Console.WriteLine(response.Content);
        //while (true)
        //{
        //    var options = new RestClientOptions("//api/GeneralCustomer")
        //    {
        //        MaxTimeout = -1,
        //    };
        //    var client = new RestClient(options);
        //    var request = new RestRequest("", Method.Post);
        //    request.AddHeader("Content-Type", "application/json");
        //    request.AddHeader("Accept", "text/plain");
        //    var body = @"{" + "\n" +
        //    @"  ""id"": 54521829," + "\n" +
        //    @"  ""name"": ""reprehenderit ex ipsum Excepteur tempor""," + "\n" +
        //    @"  ""surname"": ""eu cupidatat""," + "\n" +
        //    @"  ""mail"": ""velit non deserunt adipisicing""" + "\n" +
        //    @"}";
        //    request.AddStringBody(body, DataFormat.Json);
        //    RestResponse response = await client.ExecuteAsync(request);
        //    Console.WriteLine(response.Content);
        //};
    }
}
