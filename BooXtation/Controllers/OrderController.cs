using AutoMapper;
using BookXtation.DAL.Models.Data;
using BookXtation.DAL.Models.ViewModels;
using BooXtation.BLL.Repositories.Interfaces;
using BooXtation.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Security.Claims;

namespace BooXtation.Controllers
{
    //[Authorize(Roles = "Customer")]
    public class OrderController : Controller
    {
        private readonly IOrder_ItemRepository _orderItemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerDetailsRepository _customerDetailsRepository;
        private readonly IOrderDetailsRepository _orderDetailsRepository;
        private readonly IShopping_CartRepository _cartRepository;
        private readonly EmailService _emailService;
        private readonly IMapper _mapper;

        public OrderController(IOrder_ItemRepository orderItemRepository, IOrderRepository orderRepository, IShopping_CartRepository cartRepository
                                , IPaymentRepository paymentRepository, ICustomerRepository customerRepository, ICustomerDetailsRepository customerDetailsRepository
                                , IOrderDetailsRepository orderDetailsRepository, IBookRepository bookRepository , EmailService emailService, IMapper mapper)
        
        {
            _mapper = mapper;
            _orderItemRepository = orderItemRepository;
            _orderRepository = orderRepository;
            _paymentRepository = paymentRepository;
            _customerRepository = customerRepository;
            _customerDetailsRepository = customerDetailsRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _cartRepository = cartRepository;
            _bookRepository = bookRepository;
            _emailService = emailService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Customer")]
        private async Task<Customer> GetCustomerAsync()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            return await _customerRepository.GetByUserIdAsync(userId);
        }

        [Authorize(Roles = "Customer")]
        [AcceptVerbs("Get", "Post")]
        /////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> OrderPage(int Customer_ID)
        {


            /////////////////// Get By Id //////////////////////////

            ViewBag.Customer = await GetCustomerAsync();

            var CartItems = _cartRepository.getShoppingCartItems(Customer_ID).GetAwaiter().GetResult().ToList();

            if (CartItems.Count == 0)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            for (int i = CartItems.Count - 1; i >= 0; i--)
            {
                if (CartItems[i].Quantity > await _orderItemRepository.GetStockOfBooks(CartItems[i].Book_ID))
                {
                    CartItems.RemoveAt(i);
                }

            }
            var mappedCartItems = _mapper.Map<List<Order_ItemViewModel>>(CartItems);

            var OrderItemsModel = new OrderViewModel(mappedCartItems);

            //await _repository.AddListAsync(mappedOrderItems);

            return View(OrderItemsModel);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> PlaceOrder(OrderViewModel OrderView)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (OrderView.PaymentMethod == "cash_on_delivery")
                    {
                        OrderView.PaymentStatus = "Processing";
                        OrderView.OrderStatus = "Processing";
                    }

                    var Mapped = _mapper.Map<Order>(OrderView);

                    var MapOrderItem = _mapper.Map<List<Order_Item>>(OrderView._Items);
                    var resultOfDelete = await _orderItemRepository.DeleteFromBookStock(MapOrderItem);

                    if (resultOfDelete)
                    {
                        OrderView.Order_ID = await _orderRepository.AddNewOrderAsync(Mapped);
                        var MapPayment = _mapper.Map<Payment>(OrderView);
                        MapPayment.Amount = OrderView.TotalAmount;

                        MapOrderItem.ForEach(x => x.Order_ID = OrderView.Order_ID);

                        await _paymentRepository.AddAsync(MapPayment);
                        await _orderItemRepository.AddListAsync(MapOrderItem);

                        var customerDetails = await _customerDetailsRepository.GetByIdAsync(OrderView.CustomerDetails_ID);
                        var mappedOrderDetails = _mapper.Map<CustomerDetails, OrderDetails>(customerDetails);
                        mappedOrderDetails.Order_ID = OrderView.Order_ID;
                        await _orderDetailsRepository.AddAsync(mappedOrderDetails);

                        await _cartRepository.createNewCartAfterPlaceOrderAsync(OrderView.Customer_ID);


                        if(OrderView.PaymentMethod == "cash_on_delivery" || OrderView.PaymentMethod == "Wallet")
                        {
                            var UserEmail = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
                            var UserName = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
                            await _emailService.SendEmailAsync(UserEmail, "Order Details", HtmlText.CreateHTMLtext(MapOrderItem, UserName));
                        }
                        if (OrderView.PaymentMethod == "credit_card")
                        {
                            HttpContext.Session.SetString("CanAccessPayment", "true");
                            return RedirectToAction("PaymentGateWay", "Payment", new { Order_ID = OrderView.Order_ID });
                        }
                        return View(resultOfDelete);
                    }
                    else
                    {
                        TempData["outofStock"] = "We Are Sorry But One Of Your Books Quantity Were Sold Before You Place Order";
                        return RedirectToAction("ViewCart", "Shopping_Cart");
                    }


                }
                catch
                {
                    // If ModelState is not valid, redirect to the Error view
                    ViewBag.ErrorMessage = "There was an error saving the book. Please try again.";
                    return View("Error");
                }

            }
            else
            {
                if (OrderView.CustomerDetails_ID == 0)
                {
                    TempData["NotChecked"] = "Please Add Your Address *";
                }
                return RedirectToAction("OrderPage", new { Customer_ID = OrderView.Customer_ID });
            }

        }

        [Authorize(Roles = "Customer")]
        [HttpGet]
        public async Task<IActionResult> ShowCustomerOrders()
        {
            var customerID = await GetCustomerAsync();
            var Orders = await _orderRepository.GetAllOrderForCustomer(customerID.Customer_ID);

            return View(Orders);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet]
        public async Task<IActionResult> ShowOrderDetails(int itemID)
        {

            var orderItem = await _orderItemRepository.GetOrderItemWithOrderAndBookasync(itemID);

            return View(orderItem);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<JsonResult> CancelOrder(int Order_ID)
        {
            try
            {
                
                await _orderRepository.OrderIsCancelledAsync(Order_ID);
                var orderItems = await _orderItemRepository.GetCustomerOrderItems(Order_ID);
                await _bookRepository.ReturnBooksToStock(orderItems);

                return Json(new { success = true, message = "Your Order Cancelled successfully!" });
            }
            catch
            {
                return Json(new { success = false, message = "Your Order Not Cancelled" });

            }

        }

        [Authorize(Roles = "Customer")]
        [HttpGet]
        public async Task<IActionResult> AddNewAddress(int Customer_ID)
        {
            var customerDetails = new customerDetailsViewModel();
            customerDetails.Customer_ID = Customer_ID;
            //ViewBag.Customer_ID = Customer_ID;
            return View(customerDetails);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> AddNewAddress(customerDetailsViewModel customerDetailsViewModel)
        {
            if (ModelState.IsValid)
            {
                var MappedDetails = _mapper.Map<customerDetailsViewModel, CustomerDetails>(customerDetailsViewModel);

                await _customerDetailsRepository.AddAsync(MappedDetails);

                return RedirectToAction("OrderPage", new { Customer_ID = MappedDetails.Customer_ID });
            }
            return View(customerDetailsViewModel);

        }

        [Authorize(Roles = "Customer")]
        [HttpGet]
        public async Task<IActionResult> EditAddress(int CustomerDetails_ID)
        {
            var customerDetails = await _customerDetailsRepository.GetByIdAsync(CustomerDetails_ID);

            var Mapping = _mapper.Map<CustomerDetails, customerDetailsViewModel>(customerDetails);
            return View(Mapping);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> EditAddress(customerDetailsViewModel customerDetailsViewModel)
        {
            if (ModelState.IsValid)
            {
                var MappedDetails = _mapper.Map<customerDetailsViewModel, CustomerDetails>(customerDetailsViewModel);

                await _customerDetailsRepository.UpdateAsync(MappedDetails);

                return RedirectToAction("OrderPage", new { Customer_ID = MappedDetails.Customer_ID });
            }
            return View(customerDetailsViewModel);
        }

        [Authorize(Roles = "Admin,Editor")]
        [HttpGet]
        public async Task<IActionResult> ChangeStatus(int id , string status)
        {
            await _orderRepository.ChangeStatusAsync(id , status);
            if(status == "Rejected")
            {
                var orderItems = await _orderItemRepository.GetCustomerOrderItems(id);
                await _bookRepository.ReturnBooksToStock(orderItems);

            }
            return RedirectToAction("OrderDetails", "Admin" , new {id = id});

        }
    }
}
