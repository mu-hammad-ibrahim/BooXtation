using BookXtation.DAL.Models.Data;
using BookXtation.DAL.Models.ViewModels;
using BooXtation.BLL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace BooXtation.Controllers
{
    [Authorize(Roles = "Admin,Editor")]
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository _repository;
        private readonly IMapper _mapper;

        public AuthorController(IAuthorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: Author/Index
        public async Task<IActionResult> Index()
        {
            
            var authors = await _repository.GetAll();  // Ensure GetAllAsync is implemented
            return View("/Views/Author/Index.cshtml", authors);
        }

        // GET: Author/Details/id
        public async Task<IActionResult> Details(int id)
        {
            var author = await _repository.GetByIdAsync(id);
            if (author == null)
                return NotFound(); // Handle not found scenario

            var authorVM = _mapper.Map<AuthorViewModel>(author);
            return View("/Views/Author/Details.cshtml", authorVM);
        }

        // GET: Author/Create
        [HttpGet]
        public IActionResult Create() => View();
        

        // POST: Author/Create
        [HttpPost]
        public async Task<IActionResult> Create(AuthorViewModel authorVM)
        {
            if (ModelState.IsValid)
            {
                var mappedAuthor = _mapper.Map<AuthorViewModel, Author>(authorVM);
                await _repository.AddAsync(mappedAuthor);
                return RedirectToAction("ViewAuthors", "Admin");
            }

            return View(authorVM);
        }

        // GET: Author/Edit/id
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var author = await _repository.GetByIdAsync(id);
            if (author == null)
                return NotFound(); // Handle not found scenario

            var authorVM = _mapper.Map<AuthorViewModel>(author);


            return View("/Views/Author/Edit.cshtml", authorVM);
         
        }

        // POST: Author/Edit/id
        [HttpPost]
        public async Task<IActionResult> Edit(int id, AuthorViewModel authorVM)
        {

            if (ModelState.IsValid)
            {
                var author = _mapper.Map<Author>(authorVM);
                author.Author_ID = id;

                await _repository.UpdateAsync(author);
                return RedirectToAction("ViewAuthors", "Admin"); // Redirect to index after successful edit
            }

            // Return the Edit view with the current model if validation fails
            return View("/Views/Author/Edit.cshtml", authorVM);
        }

        // GET: Author/Delete/id
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var author = await _repository.GetByIdAsync(id);
            if (author == null)
                return NotFound(); // Handle not found scenario

            if (author.Books.Count > 0)
            {
                TempData["Error"] = "Cannot delete this author, it has Books";
                return RedirectToAction("ViewAuthors", "admin");
            }
            else return View("/Views/Author/Delete.cshtml", author);
        }

        // POST: Author/Delete/id
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = await _repository.GetByIdAsync(id);
            if (author == null)
                return NotFound();

            if (author.Books.Count > 0)
            {
                TempData["Error"] = "Cannot delete this author, it has Books";
                return View("/Views/Author/Delete.cshtml", author);
            }
            else
            {
                await _repository.DeleteAsync(author); // Ensure DeleteAsync is implemented
                return RedirectToAction("ViewAuthors", "admin");
            }
        }
    }
}