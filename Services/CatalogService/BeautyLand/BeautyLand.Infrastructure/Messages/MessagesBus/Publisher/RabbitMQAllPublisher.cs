using BeautyLand.Infrastructure.Messages.MessagesBus.Dtos.Publisher;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace BeautyLand.Infrastructure.Messages.MessagesBus.Publisher
{
    public class RabbitMQAllPublisher : IMessageBus
    {
        private readonly string _hostName;
        private readonly string _userName;
        private readonly string _password;
        private IConnection _connection;
        public RabbitMQAllPublisher(IOptions<RabbitMQAllConfigurationDto> options)
        {
            _hostName = options.Value.HostName;
            _userName = options.Value.UserName;
            _password = options.Value.Password;

        }
        public void Publish(BaseMessageDto messages, string exchange)
        {
            if (CheckConnection())
            {
                using (var channel = _connection.CreateModel())
                {

                    channel.ExchangeDeclare
                        (
                        exchange: exchange, 
                        type: ExchangeType.Fanout,
                        durable: true,
                        autoDelete: false,  
                        arguments: null
                        );
                    var message = JsonConvert.SerializeObject(messages);
                    var body = Encoding.UTF8.GetBytes(message);

                    var basicProperties = channel.CreateBasicProperties();
                    basicProperties.Persistent = true;

                    channel.BasicPublish
                        (
                        exchange: exchange,
                        routingKey: string.Empty,
                        basicProperties: basicProperties,
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
