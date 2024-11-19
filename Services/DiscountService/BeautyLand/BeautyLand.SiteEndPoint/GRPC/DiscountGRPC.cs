
using BeautyLand.Application.Services.Site.Discounts;
using BeautyLand.SiteEndPoint.Proto;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace BeautyLand.SiteEndPoint.GRPC
{
    public class DiscountGRPC : DicountProtoBuff.DicountProtoBuffBase
    {
        private readonly IDiscountServices _discountsService;
        public DiscountGRPC(IDiscountServices discountsService)
        {
            _discountsService = discountsService;
        }
        public override Task<ResponseGetDiscountByCode> GetDiscountByCode(RequestGetDiscountByCode request, ServerCallContext context)
        {
            var discount = _discountsService.GetDiscountByCode(request.Code);
            if (discount == null)
            {
                return Task.FromResult(new ResponseGetDiscountByCode
                {
                    IsSuccesss = false,
                    Message = "Discount is null",
                    Model = null
                });
            }
            return Task.FromResult(new ResponseGetDiscountByCode
            {
                IsSuccesss = true,
                Message = "It is success",
                Model = new GetDiscountByCode
                {
                    Id = discount.Id.ToString(),
                    Amount = discount.Amount,
                    Code = discount.Code,
                    IsUsed = discount.IsUsed,
                }
            });
        }
        public override Task<ResponseGetDiscountById> GetDiscountById(RequestGetDiscountById request, ServerCallContext context)
        {
            var discount = _discountsService.GetDiscountById(Guid.Parse(request.Id));
            if (discount == null)
            {
                return Task.FromResult(new ResponseGetDiscountById
                {
                    IsSuccesss = false,
                    Message = "Discount is null",
                    Model = null
                });
            }
            return Task.FromResult(new ResponseGetDiscountById
            {
                IsSuccesss = true,
                Message = "It is success",
                Model = new GetDiscountById
                {
                    Id = discount.Id.ToString(),
                    Amount = discount.Amount,
                    Code = discount.Code,
                    IsUsed = discount.IsUsed,
                }
            });
        }
        public override Task<ResponseUseDiscount> UseDiscount(RequestUseDiscount request, ServerCallContext context)
        {
            var discount = _discountsService.UseDiscount(Guid.Parse(request.Id));
            return Task.FromResult(new ResponseUseDiscount
            {
                IsSuccesss = discount
            });
        }

        public override Task<ResponseCreateDiscount> CreateDiscount(RequestCreateDiscount request, ServerCallContext context)
        {
            var discount = _discountsService.CreateDiscount(request.Code, request.Amount);
            return Task.FromResult(new ResponseCreateDiscount
            {
                IsSuccesss = discount
            });
        }
    }
}
