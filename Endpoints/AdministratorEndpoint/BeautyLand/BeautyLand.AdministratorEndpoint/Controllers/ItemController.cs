using BeautyLand.Application.Administrator.ViewServices.Catalogs.Items;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BeautyLand.AdministratorEndpoint.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;
        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }
        public IActionResult Index()
        {
            var item = _itemService.GetItem();
            return View(item);
        }
        public IActionResult Edit(Guid id, string name)
        {
            var item = _itemService.UpadeteItem
                (
                new UpdateItemDto
                (
                    id,
                    name
                    )
                );
            return RedirectToAction(nameof(Index));
        }
    }
}
