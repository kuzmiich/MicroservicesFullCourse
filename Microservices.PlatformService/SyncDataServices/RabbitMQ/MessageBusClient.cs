using Microservices.PlatformService.Dtos;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;

namespace Microservices.PlatformService.SyncDataServices.RabbitMQ
{
    public class MessageBusClient : IMessageBusClient
    {
        private IConnection _connection;
        private IModel _channel;

        public MessageBusClient(IConfiguration configuration)
        {
            var factory = new ConnectionFactory()
            {
                HostName = configuration["RabbitMQHost"],
                Port = int.Parse(configuration["RabbitMQPort"])
            };
            SetUpConnection(factory);
            SetUpChannel();
        }

        public void PublishPlatform(PlatformPublishDto platformPublishedDto)
        {
            var message  = JsonSerializer.Serialize(platformPublishedDto);

            if (_connection?.IsOpen != null)
            {
                Console.WriteLine("--> RabbitMQ Connection Open, sending message...");
                SendMessage(message);
                return;
            }
            Console.WriteLine("--> RabbitMQ connection is closed, not sending");
        }

        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "trigger",
                            routingKey: "",
                            basicProperties: null,
                            body: body);
            Console.WriteLine($"--> We have sent {message}");
        }

        public void Dispose()
        {
            Console.WriteLine("MessageBus Disposed");
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }

        private static void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("--> RabbitMQ Connection Shutdown");
        }
        
        private void SetUpChannel()
        {
            _channel = _connection?.CreateModel();
            _channel?.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
        }

        private void SetUpConnection(IConnectionFactory factory)
        {
            try
            {
                _connection = factory.CreateConnection();
                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not connect to the Message Bus: {ex.Message}");
                return;
            }
            Console.WriteLine("--> Connected to MessageBus");
        }
    }
}