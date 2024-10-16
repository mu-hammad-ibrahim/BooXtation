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
    public class PublisherController : Controller
    {
        private readonly IPublisherRepository _repository;
        private readonly IMapper _mapper;

        public PublisherController(IPublisherRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: Publisher/Index
        public async Task<IActionResult> Index()
        {
            var publishers = await _repository.GetAllAsync();  // Ensure GetAllAsync is implemented
            return View(publishers);
        }

        // GET: Publisher/Details/id
        public async Task<IActionResult> Details(int id)
        {
            var publisher = await _repository.GetByIdAsync(id);
            if (publisher == null)
                return NotFound(); // Handle not found scenario

            var publisherVM = _mapper.Map<PublisherViewModel>(publisher);
            return View(publisherVM);
        }

        // GET: Publisher/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Publisher/Create
        [HttpPost]
        public async Task<IActionResult> Create(PublisherViewModel publisherVM)
        {
            if (ModelState.IsValid)
            {
                var mappedPublisher = _mapper.Map<Publisher>(publisherVM);  // Map from ViewModel to Model
                await _repository.AddAsync(mappedPublisher); // Ensure AddAsync is implemented
                return RedirectToAction("ViewPublishers", "admin");
            }

            return View(publisherVM);
        }

        // GET: Publisher/Edit/id
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var publisher = await _repository.GetByIdAsync(id);
            if (publisher == null)
                return NotFound(); // Handle not found scenario

            var publisherVM = _mapper.Map<PublisherViewModel>(publisher);  // Map from Model to ViewModel
            return View(publisherVM);
        }

        // POST: Publisher/Edit/id
        [HttpPost]
        public async Task<IActionResult> Edit(int id, PublisherViewModel publisherVM)
        {
            if (ModelState.IsValid)
            {
                var publisher = _mapper.Map<Publisher>(publisherVM);  // Map from ViewModel to Model
                publisher.Publisher_ID = id;
                await _repository.UpdateAsync(publisher); // Ensure UpdateAsync is implemented
                return RedirectToAction("ViewPublishers", "admin");
            }

            return View(publisherVM);
        }

        // GET: Publisher/Delete/id
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var publisher = await _repository.GetByIdAsync(id);
            if (publisher == null)
                return NotFound(); // Handle not found scenario

            if (publisher.Books.Count > 0)
            {
                TempData["Error"] = "Cannot delete this publisher, it has Books";
                return RedirectToAction("ViewPublishers", "admin");
            }
            else return View(publisher);


        }

        // POST: Publisher/Delete/id
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publisher = await _repository.GetByIdAsync(id);
            if (publisher == null)
                return NotFound(); // Handle not found scenario

            if (publisher.Books.Count > 0)
            {
                TempData["Error"] = "Cannot delete this publisher, it has Books";
                return RedirectToAction("ViewPublishers", "admin");
            }
            else
            {
                await _repository.DeleteAsync(id); // Ensure DeleteAsync is implemented
                return RedirectToAction("ViewPublishers", "admin");
            }

            
        }
    }
}

