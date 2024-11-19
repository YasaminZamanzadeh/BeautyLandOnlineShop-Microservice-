namespace BeautyLand.Infrastructure.Messages.MessagesBus.Dtos.Publisher
{
    public class RabbitMQAllConfigurationDto
    {
        public string HostName { get; set; }
        public string ItemExchange { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
