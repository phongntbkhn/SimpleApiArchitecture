using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simple.API.Models;
using Simple.API.Service.IService;
using System.ComponentModel.DataAnnotations;

namespace Simple.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductionController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        public IProductionService _productionService { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productionService"></param>
        public ProductionController(IProductionService productionService)
        {
            _productionService = productionService;
        }

        /// <summary>
        /// Get list category
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetList")]
        [MapToApiVersion("1.0")]
        public ActionResult GetList([FromQuery] string? name)
        {
            var tempIQueryable = _productionService.GetIQueryable(name);
            if (tempIQueryable == null)
                return Ok(BaseResultModel.Success("Get List Production Sucess", data: null));

            var result = tempIQueryable.ToList();
            return Ok(BaseResultModel.Success("Get List Production Sucess", result));
        }
    }
}
