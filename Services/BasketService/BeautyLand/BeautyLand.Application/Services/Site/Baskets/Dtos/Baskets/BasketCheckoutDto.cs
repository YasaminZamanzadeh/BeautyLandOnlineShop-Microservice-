using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyLand.Application.Services.Site.Baskets.Dtos.Baskets
{
    public class BasketCheckoutDto
	{
        public Guid Id { get; set; }
		public string UserId { get; set; }	
		public string Name { get; set; }	
		public string PhoneNumber { get; set; }	
		public string Address { get; set; }	
		public string PostalCode { get; set; }
    }
}
