using AutoMapper;
using BookXtation.DAL.Models.Data;
using BookXtation.DAL.Models.ViewModels;
using BooXtation.BLL.Repositories.Interfaces;
using BooXtation.BLL.Repositories.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace BooXtation.Controllers
{
    [Authorize(Roles = "Admin,Editor")]
    public class AdminController : Controller
    {
        private readonly IOrder_ItemRepository _orderItemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPublisherRepository _publisherRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager; 
        private readonly RoleManager<IdentityRole> _roleManager;


        public AdminController(
                               IOrder_ItemRepository orderItemRepository,
                               IOrderRepository orderRepository,
                               IBookRepository bookRepository,
                               IAuthorRepository authorRepository,
                               IMapper mapper,
                               ICategoryRepository categoryRepository,
                               ICustomerRepository customerRepository,
                               IPublisherRepository publisherRepository,
                               UserManager<ApplicationUser> userManager,
                               RoleManager<IdentityRole> roleManager) 
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _customerRepository = customerRepository;
            _publisherRepository = publisherRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _orderItemRepository= orderItemRepository;
            _orderRepository = orderRepository;
        }

        // For Books
        [HttpGet]
        public async Task<IActionResult> ViewBooks()
        {
            var books = await _bookRepository.GetAll();
            return View(books);
        }

        // For Authors
        [HttpGet]
        public async Task<IActionResult> ViewAuthors()
        {
            var authors = await _authorRepository.GetAll();
            return View(authors);
        }
        // For Customers
        [HttpGet]
        public async Task<IActionResult> ListCustomers() // Ensure the name matches the URL
        {
            var customers = await _customerRepository.GetAllCustomersAsync();
            return View(customers); 
        }

        [HttpGet]
        public async Task<IActionResult> ViewCategories()
        {
            var categories = await _categoryRepository.GetAll();

            return View(categories);
        }
        [HttpGet]
        public async Task<IActionResult> ViewPublishers()
        {
            var publishers = await _publisherRepository.GetAllAsync();

            return View(publishers);
        }

        public async Task<IActionResult> BookDetails(int? id)
        {

            if (!id.HasValue)
                return BadRequest();

            var bookDetails = await _bookRepository.GetBookByIdAsync(id.Value);
            if (bookDetails == null) return NotFound("Not Found");

            return View(bookDetails);
        }

        [HttpGet]
        public async Task<IActionResult> ViewOrders()
        {
            var orders = await _orderRepository.GetAll();

            var orderList = await orders.ToListAsync();

            return View(orders);
        }

        public async Task<IActionResult> OrderDetails(int id)
        {
            var authorDetails= await _orderRepository.GetByIdAsync(id);
            if (authorDetails == null)return NotFound();

            return View(authorDetails);
        }
        public async Task<IActionResult> Index()
        {
            var model = new AdminDashboardViewModel
            {
                TotalBooks = await _bookRepository.Count(),
                TotalAuthors = await _authorRepository.Count(),
                TotalOrders = await _orderRepository.Count(),
                TotalPublishers = await _publisherRepository.Count(),
                TotalCategories = await _categoryRepository.Count(),
                TotalCustomers = await _customerRepository.Count(),
                OrderDates = new List<string>(),
                OrderCounts = new List<int>(),
                CustomerNames = new List<string>(),
                CustomerOrderCounts = new List<int>(),
                CustomerCreationDates = new List<string>(),
                CustomerCreationCounts = new List<int>()
            };

            var orders = await _orderRepository.GetAll();

            var groupedOrders = orders
                .GroupBy(o => o.OrderDate.Date)
                .OrderBy(g => g.Key)
                .Select(g => new
                {
                    Date = g.Key.ToShortDateString(),
                    Count = g.Count()
                });

            foreach (var orderGroup in groupedOrders)
            {
                model.OrderDates.Add(orderGroup.Date);
                model.OrderCounts.Add(orderGroup.Count);
            }

            var customerOrders = orders
                .GroupBy(o => new { o.Customer.FirstName, o.Customer.LastName })
                .Select(g => new
                {
                    CustomerName = $"{g.Key.FirstName} {g.Key.LastName}",
                    Count = g.Count()
                });

            foreach (var customerOrder in customerOrders)
            {
                model.CustomerNames.Add(customerOrder.CustomerName);
                model.CustomerOrderCounts.Add(customerOrder.Count);
            }

            var customers = await _customerRepository.GetAll();

            var groupedCustomers = customers
                .Where(c => c.applicationUser.CreatedAt.HasValue) 
                .GroupBy(c => c.applicationUser.CreatedAt.Value.Date)
                .OrderBy(g => g.Key)
                .Select(g => new
                {
                    Date = g.Key.ToShortDateString(),
                    Count = g.Count()
                });

            int cumulativeCount = 0;
            foreach (var customerGroup in groupedCustomers)
            {
                cumulativeCount += customerGroup.Count; 
                model.CustomerCreationDates.Add(customerGroup.Date);
                model.CustomerCreationCounts.Add(cumulativeCount);
            }

            return View(model);
        }


    }
}
