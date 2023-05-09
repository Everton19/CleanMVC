using CleanMVC.Application.DTOs;
using CleanMVC.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace CleanMVC.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<ProductDTO> products = await _productService.GetAllProductsAsync();

            if (products == null)
                return NotFound("Products not found!");

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) 
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null)
                return NotFound("Product not found!");

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductDTO productDTO)
        {
            if (productDTO == null)
                return BadRequest("Invalid create product!");

            await _productService.CreateAsync(productDTO);

            return Ok(productDTO);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductDTO productDTO)
        {
            await _productService.UpdateAsync(productDTO);

            if (productDTO == null)
                return BadRequest("Invalid Update Data!");

            return Ok(productDTO);
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(int id)
        {
            var productDeleted = await _productService.GetByIdAsync(id);

            if (productDeleted == null)
                return NotFound("Product Not Found!");

            await _productService.DeleteAsync(id);

            return Ok(productDeleted);
        }
    }
}
