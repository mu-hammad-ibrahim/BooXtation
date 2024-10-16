using AutoMapper;
using BookXtation.DAL.Models.Data;
using BooXtation.BLL.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BooXtation.Controllers
{
    [Authorize(Roles = "Customer")]
    public class FavouriteBooksController : Controller
    {
        private readonly IFavouriteBooksRepository _favouriteBooksRepository;
        private readonly IBookRepository _bookRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public FavouriteBooksController(ICustomerRepository customerRepository , IFavouriteBooksRepository favouriteBooksRepository,
                                        IBookRepository bookRepository , IMapper mapper)
        {
            _customerRepository = customerRepository;
            _bookRepository = bookRepository;
            _favouriteBooksRepository = favouriteBooksRepository;
            _mapper = mapper;
        }

        private async Task<Customer> GetCustomerAsync()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            return await _customerRepository.GetByUserIdAsync(userId);
        }

       
        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> AddBookToFavouriteList(int Book_ID)
        {
            var customer = await GetCustomerAsync();
            var CreateFavouriteItem = new FavouriteBooks()
            {
                Book_ID = Book_ID,
                Customer_ID = customer.Customer_ID
            };
            var check = await _favouriteBooksRepository.SearchBookBycusID_bookID(Book_ID, customer.Customer_ID);

            if (check != null)
            {
                await _favouriteBooksRepository.DeleteAsync(check);
                return Json(new { massage = "deleted" });
            }
            else
            {
                await _favouriteBooksRepository.AddAsync(CreateFavouriteItem);
                return Json(new { massage = "added" });
            }
  
        }
        
        public async Task<JsonResult> CheckFavouriteForHeartDesign(int Book_ID)
        {
            var customer = await GetCustomerAsync();
            var check = await _favouriteBooksRepository.SearchBookBycusID_bookID(Book_ID, customer.Customer_ID);
            
            return check != null ? Json(new { massage = "added" }) : Json(new { massage = "deleted" });
            
        }

        public async Task<IActionResult> ViewWishList()
        {
            var customer = await GetCustomerAsync();
            var WishList = await _favouriteBooksRepository.GetFavourites(customer.Customer_ID);
            return View(WishList);
        }

    }
}
