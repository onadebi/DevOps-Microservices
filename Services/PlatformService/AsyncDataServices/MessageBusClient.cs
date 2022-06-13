using System.Text.Json;
using PlatformService.Dtos;
using RabbitMQ.Client;
using System.Text;

namespace PlatformService.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration config;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _exchange;

        public MessageBusClient(IConfiguration _config)
        {
            config = _config;
            string hostName = _config["RabbitMq:Host"];
            int hostPort = Convert.ToInt32(_config["RabbitMq:Port"]);
            var factory = new ConnectionFactory() { HostName = hostName, Port = hostPort };
            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _exchange = "trigger";
                _channel.ExchangeDeclare(exchange: _exchange, type: ExchangeType.Fanout);

                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine($"--> Cound not connect to the MessageBus: {ex.Message}");
            }
        }
        public void PublishNewPlatform(PlatformPublishedDto platformPublishedDto)
        {
            var message = JsonSerializer.Serialize(platformPublishedDto);
            if (_connection.IsOpen)
            {
                System.Console.WriteLine($"--> RabbitMQ Connection Open, sending message.");
                //TODO: Send message
                SendMessage(message);
            }
            else
            {
                System.Console.WriteLine($"--> RabbitMQ connection is closed, not able to send message.");
            }
        }

        public void Dispose()
        {
            System.Console.WriteLine("MessageBus Disposed");
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }


        #region HELPERS
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            System.Console.WriteLine($"--> RabbitMQ COnnection Shutdown");
        }

        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(
                routingKey: "",
                basicProperties: null,
                exchange: _exchange,
                body: body
            );
            System.Console.WriteLine($"--> MessageBUS event sent:\n{message}");
        }

        #endregion
    }
}