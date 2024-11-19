using System;

namespace BeautyLand.Infrastructure.Messages.MessagesBus.Dtos.Consumer
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
    }
}
