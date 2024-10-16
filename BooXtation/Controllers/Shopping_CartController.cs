using BookXtation.DAL.Models.Data;
using BooXtation.BLL.Repositories.Interfaces;
using BooXtation.BLL.Repositories.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BooXtation.Controllers
{
    [Authorize(Roles = "Customer")]
    public class Shopping_CartController : Controller
    {
        private readonly IShopping_CartRepository _cartRepository;
        private readonly ICustomerRepository _customerRepository;
        public Shopping_CartController(IShopping_CartRepository cartRepository, ICustomerRepository customerRepository)
        {
            _cartRepository = cartRepository;
            _customerRepository = customerRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        private async Task<Customer> GetCustomerAsync()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            return await _customerRepository.GetByUserIdAsync(userId);
        }

        
        public async Task<IActionResult> ViewCart()
        {
            var _customer = await GetCustomerAsync();
            ViewBag.Customer_ID = _customer.Customer_ID;

            
                var CartItems = await _cartRepository.getShoppingCartItems(_customer.Customer_ID);
                return View(CartItems.ToList());
            
        }



        

    }
}
