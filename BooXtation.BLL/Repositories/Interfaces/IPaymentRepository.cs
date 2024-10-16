using BookXtation.DAL.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooXtation.BLL.Repositories.Interfaces
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
        Task <Payment> GetAsync(int Order_ID);
        Task StatusAsync(int Order_ID);
        Task AddPaymentByOrderIDAsync(Payment payment , int OrderID);
    }
}
