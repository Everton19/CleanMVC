using CleanMVC.Application.DTOs;
using CleanMVC.Application.Interfaces;
using CleanMVC.Domain.Entities;
using CleanMVC.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanMVC.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAll()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            if(categories == null)
                return NotFound("Categories not found!");

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category == null)
                return NotFound("Category Not Found!");

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
                return BadRequest("Invalid Data!");

            await _categoryService.CreateAsync(categoryDTO);
            
            return Ok(categoryDTO);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] CategoryDTO categoryDTO)
        {
            await _categoryService.UpdateAsync(categoryDTO);

            if (categoryDTO == null)
                return BadRequest("Invalid Update Data!");

            return Ok(categoryDTO);
        }

        [HttpDelete]
        public async Task<ActionResult> Remove(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category == null)
                return NotFound("Category Not Found!");

            await _categoryService.DeleteAsync(id);

            return Ok(category);
        }
    }
}
