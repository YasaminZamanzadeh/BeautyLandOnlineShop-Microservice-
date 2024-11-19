using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BeautyLand.Application.Services.Site.Dtos;
using BeautyLand.Application.Services.Site.Orders.Dtos;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using RestSharp;

namespace BeautyLand.Application.Services.Site.Orders
{
    public class OrderService : IOrderService
    {
        //private readonly RestClient _restClient;
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string _accessToken = null;
        public OrderService(/*RestClient restClient*/ HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
           _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }
        public OrderDetailDto GetOrderById(Guid id)
        {
            //var restRequest = new RestRequest
            //    (
            //    $"/api/Order/{id}",
            //    Method.Get
            //    );
            //if (GetAccessToken() is not null)
            //{
            //    restRequest.AddHeader
            //        (
            //        "Authorization",
            //        $"Bearer {_accessToken}"
            //        );

            //var getAccessToken = _httpContextAccessor.HttpContext.GetTokenAsync("access_token").Result;

            //restRequest.AddHeader
            //       (
            //       "Authorization",
            //       $"Bearer {getAccessToken}"
            //       );

            //RestResponse restResponse = _httpClient.Get(restRequest);

            var request = _httpClient.GetAsync(string.Format("/api/Order")).Result;
            var response = request.Content.ReadAsStringAsync().Result;
            var order = JsonSerializer.Deserialize<OrderDetailDto>
                (
                response,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );
            return order;
        }

        public async Task<List<OrderDto>> GetOrders(string userId)
        {
            var restRequest = new RestRequest
                 (
                 $"/api/Order",
                 Method.Get
                );
            //if (await GetAccessToken() is not null)
            //{
            //    restRequest.AddHeader
            //                (
            //                "Authorization",
            //                $"Bearer {_accessToken}"
            //                );
            //}
            //var getAccessToken = _httpContextAccessor.HttpContext.GetTokenAsync("access_token").Result;

            //We do not need adding getAccessToken to Authorization the header of rest with adding this method AddUserAccessTokenHandler() in IOC
            //restRequest.AddHeader
            //       (
            //       "Authorization",
            //       $"Bearer {getAccessToken}"
            //       );
            //var restResponse = _restClient.Get(restRequest);

            var request = _httpClient.GetAsync(string.Format("/api/Order")).Result;
            var response = request.Content.ReadAsStringAsync().Result;
            var orders = JsonSerializer.Deserialize<List<OrderDto>>
                (
                response,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );
            return orders;
        }

        public ResultDto RequestPayment(Guid orderId)
        {
            //var restRequest = new RestRequest
            //    (
            //    $"/api/Get?orderId={orderId}",
            //    Method.Get
            //    );

            //restRequest.AddHeader("Content-Type", "application/json");
            //RestResponse restResponse = _restClient.Get(restRequest);
            //return GetStatusCode(restResponse);
            var request = _httpClient.GetAsync(string.Format("/api/Order")).Result;
            var response = request.Content.ReadAsStringAsync().Result;
            return new ResultDto
            {
                IsSuccess = true,   
            };
        }



        private async Task<string> GetAccessToken()
        {
            if (!string.IsNullOrEmpty(_accessToken))
            {
                return _accessToken;
            }
            //Because installed Identity.Model package some extention methods have been added in it 
            HttpClient httpClient = new HttpClient();
            var discoveryDocument = httpClient.GetDiscoveryDocumentAsync
                (
               address: "https://localhost:44393"
                ).Result;

            var token = httpClient.RequestClientCredentialsTokenAsync
                (
                new ClientCredentialsTokenRequest
                {
                    Address = discoveryDocument.TokenEndpoint,
                    ClientId = "B14EC808-C1CD-40E4-A9F0-5D818ABCA98A",
                    ClientSecret = "23830719",
                    Scope = "FullAccess"
                }).Result;

            if (token.IsError)
            {
                throw new Exception(token.Error);
            }
            _accessToken = token.AccessToken;

            return _accessToken;

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
