using BookXtation.DAL.Models;
using BookXtation.DAL.Models.Data;
using BooXtation.BLL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooXtation.BLL.Repositories.Repository
{
    public class PaymentRepository : GenericReposiory<Payment> , IPaymentRepository
    {
        public PaymentRepository(BooXtationContext dbcontext) : base(dbcontext) 
        { 
        
        }
        
        public async Task AddPaymentByOrderIDAsync(Payment payment , int id)
        {

        }

        public async Task<Payment> GetAsync(int Order_ID) => 
            await _dbcontext.Payments.Where(x => x.Order_ID == Order_ID).FirstOrDefaultAsync();

        public async Task StatusAsync(int Order_ID)
        {
            var paymentItem = await _dbcontext.Payments.Where(x => x.Order_ID == Order_ID).FirstOrDefaultAsync();
            paymentItem.PaymentStatus = "Is_Paid";
            await _dbcontext.SaveChangesAsync();
        }
    }
}
