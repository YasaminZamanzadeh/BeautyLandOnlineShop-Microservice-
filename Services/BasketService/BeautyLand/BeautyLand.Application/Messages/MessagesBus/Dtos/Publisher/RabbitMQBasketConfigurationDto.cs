namespace BeautyLand.Subscription.Messages.MessagesBus.Dtos.Publisher
{
    public class RabbitMQBasketConfigurationDto
    {
        public string HostName { get; set; }
        public string BasketQueueName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
