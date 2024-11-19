namespace BeautyLand.Infrastructure.Messages.MessagesBus.Dtos.Publisher
{
    public class RabbitMQOrderConfigurationDto
    {
        public string HostName { get; set; }
        public string OrderQueueName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
