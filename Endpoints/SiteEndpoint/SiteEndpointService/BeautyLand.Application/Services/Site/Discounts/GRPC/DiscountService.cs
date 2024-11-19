using BeautyLand.Application.Services.Site.Discounts.Dtos;
using BeautyLand.Application.Services.Site.Dtos;
using BeautyLand.SiteEndPoint.Proto;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using System;

namespace BeautyLand.Application.Services.Site.Discounts.GRPC
{
    public class DiscountService : IDiscountService
    {
        private readonly GrpcChannel _grpcChannel;
        private readonly IConfiguration _configuration;
        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _grpcChannel = GrpcChannel.ForAddress(_configuration["MicroserviceAddresses:ApiGatewayAddresses:Uri"]);
        }
        public DiscountService()
        {

        }

        public ResultDto<DiscountDto> GetDiscountByCode(string code)
        {
            var discountService = new DicountProtoBuff.DicountProtoBuffClient(_grpcChannel);
            var discount = discountService.GetDiscountByCode(new RequestGetDiscountByCode
            {
                Code = code
            });
            if (discount.IsSuccesss)
            {
                return new ResultDto<DiscountDto>
                {
                    IsSuccess = true,
                    Message = "Discount is received",
                    Model = new DiscountDto
                    {
                        Id = Guid.Parse(discount.Model.Id),
                        Code = code,
                        Amount = discount.Model.Amount,
                        IsUsed = discount.Model.IsUsed
                    }
                };
            }
            return new ResultDto<DiscountDto>
            {
                IsSuccess = discount.IsSuccesss,
                Message = discount.Message
            };

        }

        public ResultDto<DiscountDto> GetDiscountById(Guid id)
        {
            var discountService = new DicountProtoBuff.DicountProtoBuffClient(_grpcChannel);
            var discount = discountService.GetDiscountById(new RequestGetDiscountById
            {
                Id = id.ToString()
            });
            if (discount.IsSuccesss)
            {
                return new ResultDto<DiscountDto>
                {
                    IsSuccess = true,
                    Message = "Discount is received",
                    Model = new DiscountDto
                    {
                        Id = Guid.Parse(discount.Model.Id),
                        Code = discount.Model.Code,
                        Amount = discount.Model.Amount,
                        IsUsed = discount.Model.IsUsed
                    }
                };
            }
            return new ResultDto<DiscountDto>
            {
                IsSuccess = discount.IsSuccesss,
                Message = discount.Message
            };
        }

        public ResultDto UseDiscount(Guid id)
        {
            var discountService = new DicountProtoBuff.DicountProtoBuffClient(_grpcChannel);
            var discount = discountService.UseDiscount(new RequestUseDiscount
            {
                Id = id.ToString()
            });

            return new ResultDto
            {
                IsSuccess = discount.IsSuccesss,
            };
        }
    }

}
