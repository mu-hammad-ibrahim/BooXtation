using BooXtation.BLL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Stripe.V2;
using Stripe;
using BookXtation.DAL.Models.ViewModels;
using AutoMapper;
using BookXtation.DAL;
using Microsoft.Extensions.Options;
using BooXtation.Helpers;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BooXtation.Controllers
{

    [Authorize(Roles = "Customer")]
    public class PaymentController : Controller
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrder_ItemRepository _order_ItemRepository;
        private readonly IBookRepository _bookRepository;
        private readonly string _secretKey;
        private readonly IMapper _mapper;
        private readonly StripeSettings _stripeSettings;
        private readonly EmailService _emailService;


        public PaymentController(IConfiguration configuration , IPaymentRepository paymentRepository , IOrderRepository orderRepository
                                    ,IMapper mapper, IBookRepository bookRepository, EmailService emailService, IOrder_ItemRepository order_ItemRepository , IOptions<StripeSettings> stripeSettings)
        {
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;
            _bookRepository = bookRepository;
            _order_ItemRepository = order_ItemRepository;
            _mapper = mapper;
            _emailService = emailService;
            _secretKey = configuration["Stripe:SecretKey"];
            _stripeSettings = stripeSettings.Value;
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
        }
        public IActionResult Index()
        {
            return View();
        }

        [CustomAuthorizationAttribute]
        [HttpGet]
        public async Task<IActionResult> PaymentGateWay(int Order_ID)
        {
            
            ViewBag.paymentDetails = await _paymentRepository.GetAsync(Order_ID);
            HttpContext.Session.SetString("CanAccessPayment", "false");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PayOrder(string stripeToken , float amount , int Payment_ID)
        {
            
            try
            {

                var paymentItem = await _paymentRepository.GetByIdAsync(Payment_ID);
                var orderId = paymentItem.Order_ID;

                var OrderItem = await _orderRepository.GetByIdAsync(orderId);
                var options = new ChargeCreateOptions
                {
                    Amount = (long)(amount*100),
                    Currency = "USD",
                    Source = stripeToken, 
                    Description = $"Order Number : {paymentItem.Order_ID} Is Paid"
                };

                var service = new ChargeService();
                Charge charge = service.Create(options);

                if (charge.Status == "succeeded")
                {
                    

                    //string confirmationCode = GenerateConfirmationCode();

                    //// Send confirmation email
                    //_emailService.SendConfirmationEmail("co.mahmoud13@gmail.com", UserName, confirmationCode);

                    paymentItem.PaymentStatus = "Is_Paid";
                    OrderItem.PaymentStatus = "Is_Paid";
                    OrderItem.OrderStatus = "Processing";
                    await _paymentRepository.UpdateAsync(paymentItem);
                    await _orderRepository.UpdateAsync(OrderItem);

                    var listOfOrderItems = await _order_ItemRepository.GetOrderItemWithAllEntity(OrderItem.Order_ID);
                    var UserEmail = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
                    var UserName = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;

                    await _emailService.SendEmailAsync(UserEmail, "Order Payment Receipt", HtmlText.CreateHTMLtext(charge, listOfOrderItems.ToList(), UserName));


                    return View("ChargeResult", charge);
                }
                paymentItem.PaymentStatus = "Payment Refused";
                OrderItem.PaymentStatus = "Payment Refused";
                OrderItem.OrderStatus = "Rejected";

                var orderItems = await _order_ItemRepository.GetCustomerOrderItems(orderId);
                await _bookRepository.ReturnBooksToStock(orderItems);
                TempData["ChargeError"] = "Payment Failed";
                return View("ChargeResult");
            }
            catch
            {
                TempData["ChargeError"] = "Error Happen When You Pay The Order";
                return View("ChargeResult");
            }
        }
    }
}
