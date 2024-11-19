using BeautyLand.Application.Services.Site.Catalogs.Item.Dtos;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace BeautyLand.Application.Services.Site.Catalogs.Item
{
    public class ItemService : IItemService
    {
        private readonly RestClient _restClient;
        public ItemService(RestClient restClient)
        {
            _restClient = restClient;
        }

        public IEnumerable<ItemDto> GetItem()
        {
            var restRequest = new RestRequest(
                "/api/Item", 
                Method.Get
                );
            RestResponse restResponse = _restClient.Get(restRequest);
            var items = JsonSerializer.Deserialize<IEnumerable<ItemDto>>
                ( 
                restResponse.Content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );
            return items;   
        }

        public ItemDto GetItemById(Guid id)
        {
            var restRequest = new RestRequest
                (
                $"/api/Item/{id}",
                Method.Get
                );
            RestResponse restResponse = _restClient.Get(restRequest);
            var item = JsonSerializer.Deserialize<ItemDto>
                (
                restResponse.Content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );
            return item;    
        }
    }
}
