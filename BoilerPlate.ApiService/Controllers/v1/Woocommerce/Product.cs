using BoilerPlate.Application.Dto.Product;
using BoilerPlate.Application.Sections.Exceptions;
using BoilerPlate.Application.Woocommerce.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BoilerPlate.ApiService.Controllers.v1.Woocommerce
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "woocommerce")]
    public class ProductController(GlobalizacionService globalizacionService, IProductService productService) : BaseController(globalizacionService)
    {
        private readonly IProductService _productService = productService;

        [HttpGet]
        [Authorize("read:woocommerce:product")]
        [ProducesResponseType(typeof(Response<List<ProductResponse>>), ((int)HttpStatusCode.OK))]
        public async Task<IActionResult> Get()
        {
            return await ExecuteServiceAsync(() => _productService.GetAllAsync());
        }

        [HttpGet("{sku}")]
        //[Authorize("read:woocommerce:product")]
        [ProducesResponseType(typeof(Response<ProductResponse>), ((int)HttpStatusCode.OK))]
        public async Task<IActionResult> Get(string sku)
        {
            return await ExecuteServiceAsync(() => _productService.GetBySkuAsync(sku));
        }

        [HttpGet("{sku}/variations")]
        //[Authorize("read:woocommerce:product")]
        [ProducesResponseType(typeof(Response<List<ProductResponse>>), ((int)HttpStatusCode.OK))]
        public async Task<IActionResult> GetVariations(string sku)
        {
            return await ExecuteServiceAsync(() => _productService.GetVariationsBySkuAsync(sku));
        }
    }
}
