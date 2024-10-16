using AutoMapper;
using BookXtation.DAL.Models.Data;
using BookXtation.DAL.Models.ViewModels;
using BooXtation.BLL.Repositories.Interfaces;
using BooXtation.BLL.Repositories.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BooXtation.Controllers
{
    public class Order_ItemController : Controller
    {
        private readonly IOrder_ItemRepository _repository;
        private readonly IMapper _mapper;


        public Order_ItemController(IOrder_ItemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        
    }
}
