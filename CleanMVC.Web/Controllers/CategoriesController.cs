using CleanMVC.Application.DTOs;
using CleanMVC.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanMVC.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return View(categories);
        }

        [HttpGet()]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO category)
        {
            if(ModelState.IsValid)
            {
                await _categoryService.CreateAsync(category);
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        [HttpGet()]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
                return NotFound();

            var categoriaDTO = await _categoryService.GetByIdAsync(id);

            if(categoriaDTO == null)
                return NotFound();

            return View(categoriaDTO);
        }

        [HttpPost()]
        public async Task<IActionResult> Edit(CategoryDTO category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryService.UpdateAsync(category);
                     
                }
                catch (Exception)
                {

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        [HttpGet()]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
                return NotFound();

            var productDelete = await _categoryService.GetByIdAsync(id);

            if (productDelete == null)
                return NotFound();

            return View(productDelete);
        }

        [HttpPost(), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryService.DeleteAsync(id);

            return RedirectToAction("Index");
        }

        [HttpGet()]
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
                return NotFound();

            var details = await _categoryService.GetByIdAsync(id);

            if (details == null)
                return NotFound();


            return View(details);
        }
    }
}
