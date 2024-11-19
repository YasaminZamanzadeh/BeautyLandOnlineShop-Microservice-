using BeautyLand.Application.Services.Site.Dtos;
using BeautyLand.Application.Services.Site.Payments.Dtos;
using BeautyLand.Application.Services.Site.Payments;
using Dto.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using ZarinPal.Class;
using BeautyLand.Infrastructure.Messages.MessagesBus.Publisher;
using BeautyLand.Infrastructure.Messages.MessagesBus.Dtos.Publisher;

namespace BeautyLand.SiteEndPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly Payment _payment;
        private readonly Authority _authority;
        private readonly Transactions _transactions;
        private readonly IConfiguration _configuration;
        private readonly string _merchantId;
        private readonly IMessageBus _messageBus;
        private readonly string _queueName;
        public PaymentController(IPaymentService paymentService, IConfiguration configuration, IMessageBus messageBus)
        {
            _paymentService = paymentService;
            var expose = new Expose();
            _payment = expose.CreatePayment();
            _authority = expose.CreateAuthority();
            _transactions = expose.CreateTransactions();
            _configuration = configuration;
            _merchantId = _configuration["ZarinPalConfiguration:MerchantId"];
            _messageBus = messageBus;
            _queueName = _configuration["RabbitMQPaymentRequestOrderConfiguration:OrderQueueName"];
        }
        [HttpPost("CreatePayment")]
        public IActionResult Post(Guid orderId, int amount)
        {
            var payment = _paymentService.CreatePayment(orderId, amount);
            return Ok(payment);
        }
        [HttpGet("RequestPayment")]
        public IActionResult GetZarinPal(Guid orderId, string callBackUrl)
        {
            var payment = _paymentService.GetPaymentOrderById(orderId);


            var calledBackUrl = Url.Action
                (
                "Verification",
                "Payment",
                new { id = payment.Id, callingBackUrl = callBackUrl },
                protocol: Request.Scheme
            );

            var requestPayment = _payment.Request(new DtoRequest()
            {
                Amount = payment.Amount,
                CallbackUrl = calledBackUrl,
                Description = "Test",
                Email = "user@example.com",
                Mobile = "1234567890",
                MerchantId = _merchantId
            }, Payment.Mode.zarinpal).Result;

            var redirectUrl = $"https://zarinpal.com/pg/StartPay/{requestPayment.Authority}";

            return Ok(new ResultDto<ReturnPaymentLinkDto>
            {
                IsSuccess = true,
                Message = "",
                Model = new ReturnPaymentLinkDto
                {
                    Link = redirectUrl,
                }
            });

        }

        [AllowAnonymous]
        [HttpGet("Verification")]
        public IActionResult Get(Guid id, string callingBackUrl)
        {
            var status = HttpContext.Request.Query["status"];
            var authority = HttpContext.Request.Query["authority"];
            if (status != "" && status.ToString().ToLower() == "ok" && authority != "")
            {
                var payment = _paymentService.GetPaymentOrderById(id);
                if (payment == null)
                {
                    return NotFound();
                }
                var restClient = new RestClient("https://www.zarinpal.com/pg/rest/WebGate/PaymentVerification.json");
                var restRequest = new RestRequest("", Method.Post);
                restRequest.AddHeader("Content-Type", "application/json");
                restRequest.AddParameter("application/json", $"{{\"MerchantID\" :\"{_merchantId}\",\"Authority\":\"{authority}\",\"Amount\":\"{payment.Amount}\"}}", ParameterType.RequestBody);
                RestResponse response = restClient.Execute(restRequest);
                var verification = Newtonsoft.Json.JsonConvert.DeserializeObject<VerificationPayResultDto>(response.Content);

                if (verification.Status == 100)
                {
                    _paymentService.Paid(id, authority, verification.RefId);

                    // Send message for order service
                    RequestOrderMessageDto requestOrderMessage = new RequestOrderMessageDto
                    {
                        Id = payment.OrderId
                    };
                    _messageBus.Publish(requestOrderMessage, _queueName);

                    return Redirect(callingBackUrl);

                }
                else
                {
                    return NotFound(callingBackUrl);
                }
            }
            return Redirect(callingBackUrl);
        }
    }
}