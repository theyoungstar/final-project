using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Catalyte.Apparel.DTOs.Products;
using Catalyte.Apparel.API.DTOMappings;
using Catalyte.Apparel.Data.Model;
using Catalyte.Apparel.Providers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        [HttpGet("/products/filters")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsByAllFiltersAsync(int pageNumber, [FromQuery] List<string> brand, [FromQuery] List<string> category, [FromQuery] List<string> type, [FromQuery] List<string> demographic, [FromQuery] List<string> primaryColorCode, [FromQuery] List<string> secondaryColorCode, [FromQuery] List<string> material, double min, double max)
        {
            _logger.LogInformation("Request received for GetProductsByAllFiltersAsync");

            var products = await _productProvider.GetProductsByAllFiltersAsync(pageNumber, brand, category, type, demographic, primaryColorCode, secondaryColorCode, material, min, max);
            var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);

            return Ok(productDTOs);
        }

        /// <summary>
        /// Endpoint for count of total pages for filtered products
        /// </summary>
        /// <returns>prodCount</returns>
        [HttpGet("/products/count")]
        public async Task<ActionResult<double>> GetProductsCountByFilterAsync([FromQuery] List<string> brand, [FromQuery] List<string> category, [FromQuery] List<string> type, [FromQuery] List<string> demographic, [FromQuery] List<string> primaryColorCode, [FromQuery] List<string> secondaryColorCode, [FromQuery] List<string> material, double min, double max)
        {
            _logger.LogInformation("Request received for GetProductsCountByFilterAsync");

            var prodCount = await _productProvider.GetProductsCountByFilterAsync(brand, category, type, demographic, primaryColorCode, secondaryColorCode, material, min, max);            

            return Ok(prodCount);
        }


        [HttpPost]
        public async Task<ActionResult<ProductDTO>> CreateProductAsync([FromBody] ProductDTO product)
        {
            _logger.LogInformation("Request received for CreateProduct");

            var newProduct = _mapper.Map<Product>(product);
            var savedProduct = await _productProvider.CreateProductAsync(newProduct);
            var productDTO = _mapper.Map<ProductDTO>(savedProduct);

            if (productDTO != null)
            {
                return Created($"/products", productDTO);
            }

            return NoContent();
        }
    }
}
