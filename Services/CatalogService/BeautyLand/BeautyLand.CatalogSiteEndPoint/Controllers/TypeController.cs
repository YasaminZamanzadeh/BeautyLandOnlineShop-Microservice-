using BeautyLand.Application.Services.Site.Catalogs.Dtos.Types;
using BeautyLand.Application.Services.Site.Catalogs.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;


namespace BeautyLand.CatalogSiteEndPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly ITypeService _typeService;
        public TypeController(ITypeService typeService)
        {
                _typeService = typeService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var model = _typeService.GetType();
            return Ok(model);
        }
        [HttpPost]
        [Authorize(Policy = "CatalogControllerPolicy")]
        public IActionResult Post([FromBody] TypeDto type)
        {
             _typeService.CreateType(type);
            return Ok();
        }
    }
}
