using BeautyLand.Application.Services.Site.Payments;
using BeautyLand.Infrastructure.Messages.MessagesBus.Dtos.Consumer;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IModel = RabbitMQ.Client.IModel;

namespace BeautyLand.Infrastructure.Messages.MessagesBus.Consumer
{
    public class RabbitMQOrderConsumer : BackgroundService
    {
        private readonly string _hostName;
        private readonly string _queueName;
        private readonly string _userName;
        private readonly string _password;
        private IConnection _connection;
        private IModel _channel;
        private readonly IPaymentService _paymentService;
        public RabbitMQOrderConsumer(IOptions<RabbitMQOrderConfigurationDto> options, IPaymentService paymentService)
        {
            _hostName = options.Value.HostName;
            _queueName = options.Value.OrderQueueName;
            _userName = options.Value.UserName;
            _password = options.Value.Password;
            _paymentService = paymentService;

            var factory = new ConnectionFactory
            {
                HostName = _hostName,
                UserName = _userName,   
                Password = _password,   
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare
                (
                queue: _queueName,
                exclusive: false,
                durable: false,
                autoDelete: false,  
                arguments: null
                );
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (Model, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());

                var order = JsonConvert.DeserializeObject<OrderDto>(message);

                var result = CreaePayment(order.Id, order.Amount);

                if (result)
                {
                    _channel.BasicAck(ea.DeliveryTag, false);
                }
            };

                _channel.BasicConsume
                (
                    queue: _queueName,
                    autoAck: false,
                    consumer: consumer
                    );
            
            return Task.CompletedTask;
        }

        private bool CreaePayment(Guid orderId, int amount)
        {
            return _paymentService.CreatePayment(orderId, amount);
        }
    }
}
