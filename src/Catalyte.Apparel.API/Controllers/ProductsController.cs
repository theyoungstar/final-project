using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Catalyte.Apparel.DTOs.Products;
using Catalyte.Apparel.Providers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Catalyte.Apparel.API.Controllers
{
    /// <summary>
    /// The ProductsController exposes endpoints for product related actions.
    /// </summary>
    [ApiController]
    [Route("/products")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductProvider _productProvider;
        private readonly IMapper _mapper;

        public ProductsController(
            ILogger<ProductsController> logger,
            IProductProvider productProvider,
            IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _productProvider = productProvider;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsAsync()
        {
            _logger.LogInformation("Request received for GetProductsAsync");

            var products = await _productProvider.GetProductsAsync();
            var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);

            return Ok(productDTOs);
        }

        [HttpGet("/products/{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductByIdAsync(int id)
        {
            _logger.LogInformation($"Request received for GetProductByIdAsync for id: {id}");

            var product = await _productProvider.GetProductByIdAsync(id);
            var productDTO = _mapper.Map<ProductDTO>(product);

            return Ok(productDTO);
        }
        [HttpGet("/products/categories")]
        public async Task<ActionResult<string>> GetAllUniqueCategoriesAsync()
        {
            _logger.LogInformation($"Request received for GetAllUniqueCategoriesAsync");

            var categories = await _productProvider.GetAllUniqueCategoriesAsync();

            return Ok(categories);
        }
        [HttpGet("/products/types")]

        public async Task<ActionResult<string>> GetAllUniqueTypesAsync()
        {
            _logger.LogInformation($"Request received for GetAllUniqueTypesAsync");

            var types = await _productProvider.GetAllUniqueTypesAsync();


            return Ok(types);

        }
        [HttpGet("/products/filters/categories/{category}")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsByCategoryAsync(string category)
        {
            _logger.LogInformation("Request received for GetProductsAsync");

            var products = await _productProvider.GetProductsByCategoryAsync(category);
            var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);

            return Ok(productDTOs);
        }
        [HttpGet("/products/filters/types/{type}")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsByTypeAsync(string type)
        {
            _logger.LogInformation("Request received for GetProductsByTypeAsync");

            var products = await _productProvider.GetProductsByTypeAsync(type);
            var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);

            return Ok(productDTOs);
        }
        [HttpGet("/products/filters/demographics/{demographic}")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsByDemographicAsync(List<string> demographic)
        {
            _logger.LogInformation("Request received for GetProductsByDemographicAsync");

            var products = await _productProvider.GetProductsByDemographicAsync(demographic);
            var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);

            return Ok(productDTOs);
        }
        [HttpGet("/products/filters/primarycolor/{primaryColorCode}")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsByPrimaryColorCodeAsync(string primaryColorCode)
        {
            _logger.LogInformation("Request received for GetProductsByPrimaryColorCodeAsync");

            var products = await _productProvider.GetProductsByPrimaryColorCodeAsync(primaryColorCode);
            var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);

            return Ok(productDTOs);
        }
        [HttpGet("/products/filters/secondarycolor/{secondaryColorCode}")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsBySecondaryColorCodeAsync(string secondaryColorCode)
        {
            _logger.LogInformation("Request received for GetProductsBySecondaryColorCodeAsync");

            var products = await _productProvider.GetProductsBySecondaryColorCodeAsync(secondaryColorCode);
            var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);

            return Ok(productDTOs);
        }
        [HttpGet("/products/filters/material/{material}")]  
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsByMaterialAsync(List<string> material)
        {
            _logger.LogInformation("Request received for GetProductsByMaterialAsync");

            var products = await _productProvider.GetProductsByMaterialAsync(material);
            var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);

            return Ok(productDTOs);
        }
        [HttpGet("/products/filters/brand/{brand}")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsByBrandAsync(List<string> brands)
        {
            _logger.LogInformation("Request received for GetProductsByBrandAsync");

            var products = await _productProvider.GetProductsByBrandAsync(brands);
            var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);

            return Ok(productDTOs);
        }
        [HttpGet("/products/filters")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsByAllFiltersAsync([FromQuery]List<string> brand, string category, string type,[FromQuery]List<string> demographic, string primaryColorCode, string secondaryColorCode, [FromQuery]List<string> material, string price)
        {
            _logger.LogInformation("Request received for GetProductsByAllFiltersAsync");
            
            var products = await _productProvider.GetProductsByAllFiltersAsync(brand, category, type, demographic, primaryColorCode, secondaryColorCode, material, price) ;
            var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);

            return Ok(productDTOs);
        }
    }
}
