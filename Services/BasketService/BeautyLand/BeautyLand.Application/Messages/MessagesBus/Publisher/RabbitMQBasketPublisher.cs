using BeautyLand.Subscription.Messages.MessagesBus.Dtos.Publisher;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace BeautyLand.Subscription.Messages.MessagesBus.Publisher
{
    public class RabbitMQBasketPublisher : IMessagePublisher
    {
        private readonly string _hostName;
        private readonly string _queueName;
        private readonly string _userName;
        private readonly string _password;
        private IConnection _connection;
        public RabbitMQBasketPublisher(IOptions<RabbitMQBasketConfigurationDto> options)
        {
            _hostName = options.Value.HostName;
            _queueName = options.Value.BasketQueueName;
            _userName = options.Value.UserName;
            _password = options.Value.Password;

        }
        public void Publish(BaseMessageDto message, string queueName)
        {
            if (CheckConnection())
            {
                using (var channel = _connection.CreateModel())
                {
                    channel.QueueDeclare
                        (
                        queue: _queueName,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                        );

                    var json = JsonConvert.SerializeObject(message);
                    var body = Encoding.UTF8.GetBytes(json);

                    var basicProperties = channel.CreateBasicProperties();
                    basicProperties.Persistent = true;

                    channel.BasicPublish
                        (
                        exchange: string.Empty,
                        routingKey: _queueName,
                        basicProperties: null,
                        body: body
                        );
                }
            }
        }
        private void RabbitMQConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostName,
                    UserName = _userName,
                    Password = _password

                };
                _connection = factory.CreateConnection();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private bool CheckConnection()
        {
            if (_connection != null)
            {
                return true;
            }
            else
            {
                RabbitMQConnection();
                return _connection != null;
            }
        }
    }

}
