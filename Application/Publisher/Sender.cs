using System.Text;
using RabbitMQ.Client;

public static class Sender
{
    public static void Send(string message)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        //factory.Uri = new Uri("amqp://guest:guest@20.49.104.44");
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "hello",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: string.Empty,
                             routingKey: "hello",
                             basicProperties: null,
                             body: body);
        Console.WriteLine($" [x] Sent Message : {message}");
    }
}

