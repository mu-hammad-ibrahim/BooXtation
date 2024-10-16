using BookXtation.DAL.Models.Data;
using BookXtation.DAL.Models.ViewModels;
using BooXtation.BLL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Threading.Tasks;
using System.Data;
using System.Security.Policy;
using Microsoft.AspNetCore.Authorization;

namespace BooXtation.Controllers
{
    [Authorize(Roles = "Admin,Editor")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: Category/Index
        public async Task<IActionResult> Index()
        {
            var categories = await _repository.GetAll();  // Ensure GetAllAsync is implemented
            return View(categories);
        }

        // GET: Category/Details/id
        public async Task<IActionResult> Details(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
                return NotFound(); // Handle not found scenario

            var categoryVM = _mapper.Map<CategoryViewModel>(category);
            return View(categoryVM);
        }

        // GET: Category/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel categoryVM)
        {
            if (ModelState.IsValid)
            {
                var mappedCategory = _mapper.Map<Category>(categoryVM);  // Map from ViewModel to Model
                await _repository.AddAsync(mappedCategory); // Ensure AddAsync is implemented
                return RedirectToAction("ViewCategories", "admin");
            }

            return View(categoryVM);
        }

        // GET: Category/Edit/id
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
                return NotFound(); // Handle not found scenario

            var categoryVM = _mapper.Map<CategoryViewModel>(category);  // Map from Model to ViewModel
            return View(categoryVM);
        }

        // POST: Category/Edit/id
        [HttpPost]
        public async Task<IActionResult> Edit(int id, CategoryViewModel categoryVM)
        {
            if (ModelState.IsValid)
            {
                var category = _mapper.Map<Category>(categoryVM);  // Map from ViewModel to Model
                category.Category_ID = id;
                await _repository.UpdateAsync(category); // Ensure UpdateAsync is implemented
                return RedirectToAction("ViewCategories", "admin");
            }

            return View(categoryVM);
        }

        // GET: Category/Delete/id
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
                return NotFound(); // Handle not found scenario

            if (category.Books.Count > 0)
            {
                TempData["Error"] = "Cannot delete this publisher, it has Books";
                return RedirectToAction("ViewCategories", "admin");
            }else return View(category);
        }

        // POST: Category/Delete/id
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null) return NotFound();
            if (category.Books.Count > 0)
            {
                TempData["Error"] = "Cannot delete this category, it has Books";
                return RedirectToAction("ViewCategories", "admin");
            }
            else
            {
                await _repository.DeleteAsync(category);
                return RedirectToAction("ViewCategories", "admin");
            }
            
        }
    }
}
