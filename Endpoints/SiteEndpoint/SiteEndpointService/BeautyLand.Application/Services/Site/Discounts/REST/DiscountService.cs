using BeautyLand.Application.Services.Site.Discounts.Dtos;
using BeautyLand.Application.Services.Site.Dtos;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyLand.Application.Services.Site.Discounts.REST
{
    public class DiscountService : IDiscountService
    {
        private readonly RestClient _restClient;
        public DiscountService(RestClient restClient)
        {
                _restClient = restClient;
        }
        public ResultDto<DiscountDto> GetDiscountByCode(string code)
        {
            var restRequest = new RestRequest
                (
                $"/api/Discount?code={code}",
                Method.Get
                );
            var restResponse = _restClient.Get( restRequest );
            var discount = JsonConvert.DeserializeObject<ResultDto<DiscountDto>>(restResponse.Content);
            return discount;    
        }

        public ResultDto<DiscountDto> GetDiscountById(Guid id)
        {
            var restRequest = new RestRequest
                 (
                 $"/api/Discount/{id}",
                 Method.Get 
                );
            var restResponse = _restClient.Get( restRequest );
            var discount = JsonConvert.DeserializeObject<ResultDto<DiscountDto>>(restResponse.Content);
            return discount;

        }

        public ResultDto UseDiscount(Guid id)
        {
            var restRequest = new RestRequest
                 (
                 $"/api/Discount/{id}",
                 Method.Get
                );
            var restResponse = _restClient.Get(restRequest);
            var discount = JsonConvert.DeserializeObject<ResultDto>(restResponse.Content);
            return discount;

        }
    }
}
