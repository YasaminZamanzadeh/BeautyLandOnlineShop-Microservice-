using BeautyLand.Application.Services.Site.Baskets.Basket.Dtos;
using BeautyLand.Application.Services.Site.Dtos;
using RestSharp;
using System;
using System.Net;
using System.Text.Json;

namespace BeautyLand.Application.Services.Site.Baskets.Basket
{
    public class BasketService : IBasketService
    {
        private readonly RestClient _restClient;
        public BasketService(RestClient restClient)
        {
            _restClient = restClient;
        }

        public ResultDto ApplyDiscountBasket(Guid basketId, Guid discountId)
        {
            var restRequest = new RestRequest
                 (
               $"api/Basket/{basketId}/{discountId}",
               Method.Put
                );
            RestResponse restResponse = _restClient.Put(restRequest);
            return GetStatusCode(restResponse);

        }

        public ResultDto CheckoutBasket(CheckoutBasketDto checkoutBasket)
        {
            var restRequest = new RestRequest
                (
                "api/Basket/CheckoutBasket",
                Method.Post
                );

            restRequest.AddHeader("Content-Type", "application/json");
            var basketJson = JsonSerializer.Serialize(checkoutBasket);
            restRequest.AddParameter("application/json", basketJson, ParameterType.RequestBody);
            var restResponse = _restClient.Execute(restRequest);
            return GetStatusCode(restResponse);
        }

        public ResultDto CreateBasket(CreateBasketDto createBasket, string userId)
        {
            var restRequest = new RestRequest
                (
                $"api/Basket?userId={userId}",
                Method.Post
                );
            restRequest.AddHeader
                (
                "Content-Type",
                "application/json"
                );
            //Serialize method converts csharp model to json
            var basket = JsonSerializer.Serialize
                (
                createBasket,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

            restRequest.AddParameter
                (
                "application/json",
                basket,
                ParameterType.RequestBody
                );

            RestResponse restResponse = _restClient.Post(restRequest);
            return GetStatusCode(restResponse);

        }

        public ResultDto DeleteBasket(Guid itemId)
        {
            var restRequest = new RestRequest
                (
                $"api/Basket?itemId={itemId}",
                Method.Delete
                );
            RestResponse restResponse = _restClient.Delete(restRequest);
            return GetStatusCode(restResponse);
        }

        public BasketDto GetBasket(string userId)
        {
            var restRequest = new RestRequest(
                $"api/Basket?userId={userId}",
                Method.Get
                );
            RestResponse restResponse = _restClient.Get(restRequest);
            // Deserialize method converts json type to csharp model
            var basket = JsonSerializer.Deserialize<BasketDto>
                (
                restResponse.Content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );
            return basket;
        }

        public ResultDto UpdateBasket(Guid id, int quantity)
        {
            var restRequest = new RestRequest(
              $"api/Basket?id={id}&quantity={quantity}",
              Method.Put
              );
            RestResponse restResponse = _restClient.Put(restRequest);
            return GetStatusCode(restResponse);
        }

        private ResultDto GetStatusCode(RestResponse restResponse)
        {
            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "OK"
                };
            }
            else
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = restResponse.ErrorMessage
                };
            }
        }
    }
}