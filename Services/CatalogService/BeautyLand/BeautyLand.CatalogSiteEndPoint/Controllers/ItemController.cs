using App.Metrics;
using App.Metrics.Counter;
using BeautyLand.Application.Services.Site.Catalogs.Items;
using BeautyLand.SiteEndPoint.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;


namespace BeautyLand.CatalogSiteEndPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly IMetrics _metrics;
        private readonly ILogger<ItemController> _logger;


        public ItemController(IItemService itemService, IMetrics metrics, ILogger<ItemController> logger)
        {
            _itemService = itemService;
            _metrics = metrics;
            _logger = logger;   
        }

        [HttpGet]
        public IActionResult Get()
        {

            _logger.LogWarning("The Log warning seq that implements through serilog");
            _metrics.Measure.Counter.Increment(new CounterOptions
            {
                Name = "The number of request that use /Item/Index is :"
            });
            _logger.LogError("The Log warning seq that implements through serilog");
            var model = _itemService.GetItem();
            return Ok(model);
        }
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            _metrics.Measure.Counter.Increment(new CounterOptions
            {
                Name = "The number of request that use /Item/Details is :"
            });

            var model = _itemService.GetItemById(id);
            return Ok(model);
        }
        [HttpGet("/api/Item/Verify/{id}")]
        public IActionResult Verify(Guid id)
        {
            var item = _itemService.GetItemById(id);
            return Ok(new ItemVerificationDto
            {
                Id = item.Id,
                Consignment = item.Name
            });
        }
    }
}
