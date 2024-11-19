using BeautyLand.Application.Services.Site.Discounts.Dots;
using BeautyLand.SiteEndPoint.Proto;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using System;

namespace BeautyLand.Application.Services.Site.Discounts
{
	public class DiscountService : IDiscountService
	{
	   private readonly GrpcChannel  _grpcChannel;
		private readonly IConfiguration _configuration;
        public DiscountService(IConfiguration configuration)
        {
			_configuration = configuration;
			_grpcChannel = GrpcChannel.ForAddress(_configuration["MicroServiceAddresses:Discounts:Uri"]);

		}
		public DiscountDto GetDiscountByCode(string code)
		{
			var discontService = new DicountProtoBuff.DicountProtoBuffClient(_grpcChannel);
			var discount = discontService.GetDiscountByCode(new RequestGetDiscountByCode
			{
				Code = code
			});
			if (discount.IsSuccesss)
			{
				return new DiscountDto
				{
					Id = Guid.Parse(discount.Model.Id),
					Code = discount.Model.Code,
					Amount = discount.Model.Amount,
					IsUsed = discount.Model.IsUsed,
				};
				
			}
			return null;
		}

		public DiscountDto GetDiscountById(Guid id)
		{
			var discountService = new DicountProtoBuff.DicountProtoBuffClient(_grpcChannel);
			var discount = discountService.GetDiscountById(new RequestGetDiscountById
			{
				Id = id.ToString()
			});
			if (discount.IsSuccesss)
			{
				return new DiscountDto
				{
					Id = Guid.Parse(discount.Model.Id),
					Code = discount.Model.Code,
					Amount = discount.Model.Amount,
					IsUsed = discount.Model.IsUsed,
				};

			}
			return null;
		}
	}

}
