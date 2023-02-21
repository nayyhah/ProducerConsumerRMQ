using RabbitMQ.Client;
using System.Text;

namespace Producer
{
    public class Sender
    {
        public static void Main(string[] args)
        {
            // Create a connection to the RabbitMQ server.
            var factory = new ConnectionFactory() { HostName = "localhost" };
            
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    // Declare a queue to which you want to send messages.
                    channel.QueueDeclare(queue: "ReturnQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);

                    // Convert the message to a byte array.
                    string message = "Getting started with RabbitMQ";
                    var body = Encoding.UTF8.GetBytes(message);

                    // Publish the message to the queue.
                    channel.BasicPublish(exchange: "", routingKey: "ReturnQueue", basicProperties: null, body: body);
                    Console.WriteLine("Sent message: {0}...", message);
                    
                }

            }
            
            Console.WriteLine("Press [enter] to exit the Sender App...");
            Console.ReadLine();
            

        }
    }
}