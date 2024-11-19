using BeautyLand.Application.Services.Site.Dtos;
using BeautyLand.Application.Services.Site.Payments.Dtos;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BeautyLand.Application.Services.Site.Payments
{
    public interface IPaymentsService
    {
        ResultDto<PaymentDto> GetPayment(Guid orderId, string callingBackUrl);
    }

    public class PaymentService : IPaymentsService
    {
        private readonly RestClient _restClient;
        public PaymentService(RestClient restClient)
        {
                _restClient = restClient;
        }
        public ResultDto<PaymentDto> GetPayment(Guid orderId, string callingBackUrl)
        {
           var restRequest = new RestRequest
                (
               $"/api/Get?orderId={orderId}& callingBackUrl={callingBackUrl}",
               Method.Get
               );
            RestResponse restResponse = _restClient.Get(restRequest);
            var order = JsonConvert.DeserializeObject<ResultDto<PaymentDto>>(restResponse.Content);
            return order;
        }
    }
}
