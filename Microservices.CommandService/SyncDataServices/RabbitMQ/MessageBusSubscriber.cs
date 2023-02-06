using Microservices.CommandService.SyncDataServices.RabbitMQ.EventProcessing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microservices.CommandService.SyncDataServices.RabbitMQ
{
    public class MessageBusSubscriber : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IEventProcessor _eventProcessor;
        private IConnection _connection;
        private IModel _channel;
        private string _queueName;

        public MessageBusSubscriber(IConfiguration configuration, IEventProcessor eventProcessor)
        {
            _configuration = configuration;
            _eventProcessor = eventProcessor;
            InitializeRabbitMq();
        }

        private void InitializeRabbitMq()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"])
            };
            try
            {
                _connection = factory.CreateConnection();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Can not create connection in RabbitMQ server Error - {e.Message}");
                return;
            }
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
            _queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: _queueName, exchange: "trigger", routingKey: "");
            
            Console.WriteLine("--> Listening on the Message Bus...");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (_channel is null)
                return Task.CompletedTask;
            
            stoppingToken.ThrowIfCancellationRequested();
            
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (moduleHandle, ea) =>
            {
                Console.WriteLine("--> Event received!");

                var body = ea.Body;
                var notificationMessage = Encoding.UTF8.GetString(body.ToArray());

                _eventProcessor.ProcessEvent(notificationMessage);
            };
            
            _channel.BasicConsume(_queueName, autoAck: true, consumer);
            
            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            Console.WriteLine("--> MessageBus Disposed");
            if (_channel is { IsOpen: true })
            {
                _channel.Close();
                _connection.Close();
            }
        }
    }
}