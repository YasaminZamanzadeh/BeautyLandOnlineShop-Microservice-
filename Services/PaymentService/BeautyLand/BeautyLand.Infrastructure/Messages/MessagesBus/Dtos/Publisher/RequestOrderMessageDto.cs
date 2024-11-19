using System;

namespace BeautyLand.Infrastructure.Messages.MessagesBus.Dtos.Publisher
{
    public class RequestOrderMessageDto : BaseMessageDto
    {
        public Guid OrderId { get; set; }
    }
}
