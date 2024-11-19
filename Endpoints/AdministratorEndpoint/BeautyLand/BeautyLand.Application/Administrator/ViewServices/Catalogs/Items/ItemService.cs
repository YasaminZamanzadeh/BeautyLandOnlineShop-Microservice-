using BeautyLand.Application.Administrator.ViewServices.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;


namespace BeautyLand.Application.Administrator.ViewServices.Catalogs.Items
{
    public class ItemService : IItemService
    {
        private readonly RestClient _restClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ItemService(RestClient restClient, IHttpContextAccessor httpContextAccessor)
        {
            _restClient = restClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<ItemDto> GetItem()
        {
            var restRequest = new RestRequest
                 (
                $"/api/ItemManagement",
                Method.Get
                );

            var getAccessToken = _httpContextAccessor.HttpContext.GetTokenAsync("access_token").Result;
            restRequest.AddHeader
                (
                "Authorization",
                $"Bearer {getAccessToken}"
                );
            RestResponse restResponse = _restClient.Get(restRequest);
            var items = JsonSerializer.Deserialize<IEnumerable<ItemDto>>
                (
                restResponse.Content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true}
                );
            return items;
        }

        public ResultDto UpadeteItem(UpdateItemDto updateItem)
        {
            var restRequest =new RestRequest
                (
                "/api/ItemManagement",
                Method.Put
                );

            restRequest.AddHeader("Content-Type", "application/json");
            var item = JsonSerializer.Serialize(updateItem);
            restRequest.AddParameter("application/json", item, ParameterType.RequestBody);
            RestResponse restResponse = _restClient.Put(restRequest);
            return GetStatusCode(restResponse);
        }
        private static ResultDto GetStatusCode(RestResponse restResponse)
        {
            if (restResponse.StatusCode is HttpStatusCode.OK)
            {
                return new ResultDto(true,"Success");
            }
            else
            {
                return new ResultDto(true, "Failed");
            }
        }
    }
    
}
