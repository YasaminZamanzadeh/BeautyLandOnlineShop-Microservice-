using App.Metrics;
using App.Metrics.Counter;
using BeautyLand.Application.Services.Site.Catalogs.Item;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeautyLand.SiteEndpoint.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;
        private readonly IMetrics _metrics;
        public ItemController(IItemService itemService, IMetrics metrics)
        {
            _itemService = itemService;
            _metrics = metrics;
        }
        public IActionResult Index()
        {
            _metrics.Measure.Counter.Increment(new CounterOptions
            {
                Name = "The number of request that use /Item/Index is :"
            });

            var items =  _itemService.GetItem();
            return View(items);
        }
        public IActionResult Details(Guid id)
        {
            _metrics.Measure.Counter.Increment(new CounterOptions
            {
                Name = "The number of request that use /Item/Details is :"
            });

            var item = _itemService.GetItemById(id);    
            return View(item);
        }
    }
}
