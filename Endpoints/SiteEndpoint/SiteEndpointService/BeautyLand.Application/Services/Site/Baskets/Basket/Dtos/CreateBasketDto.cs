namespace BeautyLand.Application.Services.Site.Baskets.Basket.Dtos
{
	public class CreateBasketDto
    {
            public string BasketId { get; set; }
            public string ItemId { get; set; }
            public string Name { get; set; }
            public string Image { get; set; }
            public int Price { get; set; }
            public int Quantity { get; set; }
        
    }
}
