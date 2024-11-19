using System;

namespace BeautyLand.Application.Messages.MessagesBus.Consumer
{
    public partial class RabbitMQItemConsumer
    {
        public class ItemDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }
    }
}
