using AutoMapper;
using BookXtation.DAL.Models.Data;
using BookXtation.DAL.Models.ViewModels;
using BooXtation.BLL.Repositories.Interfaces;
using BooXtation.BLL.Repositories.Repository;
using BooXtation.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Security.Claims;

namespace BooXtation.Controllers
{
    [Authorize(Roles = "Customer")]
    public class Cart_ItemController : Controller
    {
        private readonly ICart_ItemRepository _cart_ItemRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IShopping_CartRepository _cartRepository;
        private readonly IOrder_ItemRepository _order_ItemRepository;

        private readonly IMapper _mapper;
        public Cart_ItemController(IShopping_CartRepository cartRepository ,ICart_ItemRepository cart_ItemRepository 
                                    , IOrder_ItemRepository order_ItemRepository , ICustomerRepository customerRepository , IMapper mapper)
        {
            _mapper = mapper;
            _cart_ItemRepository = cart_ItemRepository;
            _customerRepository = customerRepository;
            _cartRepository = cartRepository;
            _order_ItemRepository = order_ItemRepository;
        }

        private async Task<Customer> GetCustomerAsync()
        {
            var userId =  User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            return await _customerRepository.GetByUserIdAsync(userId);
        } 

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> addBookToCart_items(int Book_ID, int quantity)
        {
            var customer = await GetCustomerAsync();
            var ShoppingCart = await _cartRepository.getShoppingCartByCustIdAsync(customer.Customer_ID);
            
            // عشان يشوف اخر رقم للعميل معمول ليه عربيه شراء ولا لأ و المفروض تبقي بالكوكيز
            // لو ملهوش و كمان مفيش رقم عربيه بيتساوي في الايتيمز مع الشوبينج كارت يعمل عربيه جديده ليه

            if (ShoppingCart == null /*&& ShoppingCart?.Cart_ID == cartItems?.Cart_ID*/)
            {
                 await _cartRepository.createNewCartAsync(customer.Customer_ID);
            }

            var cart = await _cartRepository.getShoppingCartByCustIdAsync(customer.Customer_ID);

            var BookStock = await _order_ItemRepository.GetStockOfBooks(Book_ID);
            
            if(BookStock > 0 && quantity > 0)
            {
                await _cart_ItemRepository.AddBookToCartAsync(Book_ID, customer.Customer_ID, cart.Cart_ID, quantity);
            }
            else if (BookStock > 0)
            {
                await _cart_ItemRepository.AddBookToCartAsync(Book_ID, customer.Customer_ID, cart.Cart_ID, 1);
            }

            return NoContent();
        }

        [HttpGet]
        public async Task<JsonResult> GetCountOfItemsAsync()
        {
            
            var customer = await GetCustomerAsync();
            var cart = await _cartRepository.getShoppingCartByCustIdAsync(customer.Customer_ID);
            
            var count = cart == null 
                                ? 0 
                                : await _cart_ItemRepository.getCountOfCartItemsAsync(cart.Cart_ID);
            
            return Json(count);
        }
        
        [HttpPost]
        public async Task<JsonResult> DeleteBook(int id)
        {
            try
            {
                await _cart_ItemRepository.DeleteBookFromCartAsync(id);
                return Json(new { deleted = true });
            }
            catch (Exception ex) {
                return Json(new { deleted = false , massage = ex.Message});
            }
        }

        [HttpPost]
        public async Task<JsonResult> CheckQuantityWithoutSubmit (int id , int quantity , int customerId)
        {

            if (await _cart_ItemRepository.CheckQuantityAsync(id, quantity, customerId))
            {
                return Json(new { available = true, message = "Avaliable"  });
            }
            else
            {
                return Json(new { notAvailable = true, message = $"Not Avaliable" });
            }
        }


        
        
    }
}
