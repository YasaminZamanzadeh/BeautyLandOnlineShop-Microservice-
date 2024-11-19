using System;

namespace BeautyLand.Infrastructure.Messages.MessagesBus.Dtos.Publisher
{
    public class BaseMessageDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
