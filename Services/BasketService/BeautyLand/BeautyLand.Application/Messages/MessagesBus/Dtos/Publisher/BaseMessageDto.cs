using System;
namespace BeautyLand.Subscription.Messages.MessagesBus.Dtos.Publisher
{
    public class BaseMessageDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }

}