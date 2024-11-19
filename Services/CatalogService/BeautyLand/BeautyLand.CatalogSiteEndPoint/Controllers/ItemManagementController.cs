using BeautyLand.Application.Services.Site.Catalogs.Dtos.Items;
using BeautyLand.Application.Services.Site.Catalogs.Items;
using BeautyLand.Infrastructure.Messages.MessagesBus.Dtos.Publisher;
using BeautyLand.Infrastructure.Messages.MessagesBus.Publisher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace BeautyLand.CatalogSiteEndPoint.Controllers
{
    [Authorize(Policy = "CatalogControllerPolicy", Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
   
    public class ItemManagementController : ControllerBase
    {
        private readonly IItemService _itemService;
        public ItemManagementController(IItemService itemService)
        {
            _itemService = itemService;
        }
        [HttpPost()]
        public IActionResult Post([FromBody] CreateItemDto createItem)
        {
           var item =  _itemService.CreateItem(createItem);
            return Created($"/api/ItemManagement/Get/{item}", item);
        }
        [HttpGet()]
       // [AllowAnonymous]
        public IActionResult Get()
        {
            var item = _itemService.GetItem();
            return Ok(item);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var item = _itemService.GetItemById(id);
            return Ok(item);
        }
        [HttpPut()]
        public IActionResult Put(UpdateItemDto updateItem, [FromServices] IMessageBus messageBus, [FromServices] IConfiguration configuration)
        {
            var item = _itemService.UpdateItem(updateItem);
            if (item)
            {
                ItemChangesDto itemChanges = new ItemChangesDto
                {
                    Id = updateItem.Id,
                    Name = updateItem.Name,
                };
                messageBus.Publish(itemChanges, configuration.GetSection("RabbitMQItemChangesConfiguration:ItemExchange").Value ); 
            }
            return Ok(item);
        }
    }
}