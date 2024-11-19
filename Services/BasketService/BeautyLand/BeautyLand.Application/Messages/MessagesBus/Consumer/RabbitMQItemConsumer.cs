using BeautyLand.Application.Messages.MessagesBus.Dtos.Consumer;
using BeautyLand.Application.Services.Site.Catalogs.Items;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace BeautyLand.Application.Messages.MessagesBus.Consumer
{
    public partial class RabbitMQItemConsumer : BackgroundService
    {
        private readonly string _hostName;
        private readonly string _queueName;
        private readonly string _exchangeName;
        private readonly string _userName;
        private readonly string _password;
        private IConnection _connection;
        private IModel _channel;
        private readonly IItemService _itemService;
        public RabbitMQItemConsumer(IOptions<RabbitMQItemConfigurationDto> options, IItemService itemService)
        {
            _hostName = options.Value.HostName;
            _queueName = options.Value.ItemQueueName;
            _exchangeName= options.Value.ItemExchange;
            _userName = options.Value.UserName;
            _password = options.Value.Password;
            _itemService = itemService;

            var factory = new ConnectionFactory
            {
                HostName = _hostName,
                UserName = _userName,
                Password = _password,
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare
                (
                exchange: _exchangeName,
                type: ExchangeType.Fanout,
                durable: true,
                autoDelete: false,
                arguments: null
                );

            _channel.QueueDeclare
              (
              queue: _queueName,
              durable: true,
              exclusive: false,
              autoDelete: false,
              arguments: null
              );

            _channel.QueueBind
                (
                queue: _queueName,
                exchange: _exchangeName,
                routingKey: string.Empty,
                arguments: null
                );
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ae) =>
            {
                var json = ae.Body.ToArray();
                var message = Encoding.UTF8.GetString(json);
                var item = JsonConvert.DeserializeObject<ItemDto>(message);
                var result = UpdateItem(item);
                if (result)
                {
                    _channel.BasicAck(
                        ae.DeliveryTag,
                        false);
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

        private bool UpdateItem(ItemDto item)
        {
            return _itemService.UpdateItem(item.Id, item.Name);
        }
    }
}
