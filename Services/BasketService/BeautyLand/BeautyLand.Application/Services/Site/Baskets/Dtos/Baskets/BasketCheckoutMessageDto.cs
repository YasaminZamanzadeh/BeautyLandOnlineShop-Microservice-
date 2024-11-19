using BeautyLand.Application.Services.Site.BasketItems.Dtos.BasketItems;
using BeautyLand.Subscription.Messages.MessagesBus.Dtos.Publisher;
using System;
using System.Collections.Generic;

namespace BeautyLand.Application.Services.Site.Baskets.Dtos.Baskets
{
    public class BasketCheckoutMessageDto : BaseMessageDto
	{
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public int TotalPrice { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }

        public List<BasketItemMessageDto> BasketItems { get; set; } = new List<BasketItemMessageDto>();

    }
}
