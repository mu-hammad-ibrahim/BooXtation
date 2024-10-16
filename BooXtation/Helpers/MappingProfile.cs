using AutoMapper;
using BookXtation.DAL.Models.Data;
using BookXtation.DAL.Models.ViewModels;
using Stripe;

namespace BooXtation.Helpers
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookViewModel>()
                .ReverseMap();
            CreateMap<Author, AuthorViewModel>()
                .ReverseMap();
            CreateMap<Category, CategoryViewModel>()
               .ReverseMap();
            CreateMap<Publisher, PublisherViewModel>()
               .ReverseMap();
            CreateMap<CustomerDetails, customerDetailsViewModel>()
                .ReverseMap();
            CreateMap<Cart_Item, Order_ItemViewModel>()
                .ReverseMap();
            CreateMap <Order , OrderViewModel > ()
                .ReverseMap();
            CreateMap<Order_Item, OrderViewModel>()
                .ReverseMap();
            CreateMap<Payment, OrderViewModel>()
                .ReverseMap();
            CreateMap<Order_Item, Order_ItemViewModel>() 
                .ReverseMap();
            CreateMap<CustomerDetails, OrderDetails>();
            CreateMap<PaymentRequestViewModel, PaymentIntentCreateOptions>();
        }
    }
}
