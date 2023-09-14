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




using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQWebapi.Models.ControllerModels;
using RabbitMQWebapi.Models.RabbitMQ;
using RabbitMQWebapi.Models.ResponseModels;
using RestSharp;
using System.Text;

internal class Program
{

    private static void Main(string[] args)
    {
        //Console.Out.WriteLineAsync("test");
    

        Customer customer = new Customer
        {
            Mail = "enes.gelmez@hotmail.com",
            Name = "Enes",
            Surname = "Gelmez",
        };
        //test2(customer);
        while (true)
        {
            //test2(customer);
        }

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

    public static async void test2(Customer customer)
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

        await Console.Out.WriteLineAsync($"Sending Message");
        byte[] messageBodyBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(customer));
        decloration.Model.BasicPublish(exchangeName, routingKey, basicProperties: null, messageBodyBytes);
        //Thread.Sleep(1000);
        generalResponse.Data = "İşleminiz sıraya alımıştır.";
        await Console.Out.WriteLineAsync("İşleminiz sıraya alımıştır.");
        //return await Task.FromResult<GeneralResponse<string>>(generalResponse);
    }
}
