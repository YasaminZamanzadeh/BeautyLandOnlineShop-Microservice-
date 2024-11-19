using System.Text;
using BeautyLand.Infrastructure.Messages.MessagesBus.Dtos.Publisher;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace BeautyLand.Infrastructure.Messages.MessagesBus.Publisher
{
    public class RabbitMQOrderPublisher : IMessageBus
    {
        private readonly string _hostName;
        private readonly string _queueName;
        private readonly string _userName;
        private readonly string _password;
        private IConnection _connection;
        public RabbitMQOrderPublisher(IOptions<RabbitMQOrderConfigurationDto> options)
        {
            _hostName = options.Value.HostName;
            _queueName = options.Value.OrderQueueName;
            _userName = options.Value.UserName;
            _password = options.Value.Password;
        }
        public void Publish(BaseMessageDto messages, string quesueName)
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
                    var message = JsonConvert.SerializeObject(messages);
                    var body = Encoding.UTF8.GetBytes(message);

                    var basicProperties = channel.CreateBasicProperties();
                    basicProperties.Persistent = true;

                    channel.BasicPublish
                        (
                           exchange: string.Empty,
                           routingKey: _queueName,
                           basicProperties : basicProperties,
                           body: body   
                        );
                }              
            }
        }
        private void CreateRabbitMQConnection ()
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
            catch (System.Exception)
            {

                throw;
            }
        }

       private bool CheckConnection()
        {
            if (_connection != null )
            {
                return true;
            }
            else
            {
                CreateRabbitMQConnection();
                return _connection != null;
            }
        }
    }
}
