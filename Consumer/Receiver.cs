using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Consumer
{
    public class Receiver
    {
        public static void Main(string[] args)
        {
            // Create a connection to the RabbitMQ server.
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    // Declare a queue to which you want to receive messages.
                    channel.QueueDeclare(queue: "ReturnQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray(); ;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine("Received message: {0}...", message);
                    };

                    channel.BasicConsume(queue: "ReturnQueue", autoAck: true, consumer: consumer);

                    Console.WriteLine("Press [enter] to exit the Consumer App...");
                    Console.ReadLine();
                }

            }




        }
    }
}