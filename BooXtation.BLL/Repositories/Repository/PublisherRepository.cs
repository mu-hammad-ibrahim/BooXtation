using BookXtation.DAL.Models;
using BookXtation.DAL.Models.Data;
using BooXtation.BLL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BooXtation.BLL.Repositories.Repository
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly BooXtationContext _context;

        public PublisherRepository(BooXtationContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<IEnumerable<Publisher>> GetAllAsync()
        {
            return await _context.Publishers.Include(a => a.Books)
                        .ToListAsync();
        }

        public async Task<Publisher> GetByIdAsync(int id)
        {
            return await _context.Publishers
                .Include(p => p.Books) 
                .FirstOrDefaultAsync(p => p.Publisher_ID == id);
        }

        public async Task AddAsync(Publisher publisher)
        {
            await _context.Publishers.AddAsync(publisher);  // Asynchronously add a new publisher
            await _context.SaveChangesAsync();         // Save changes asynchronously
        }

        public async Task UpdateAsync(Publisher publisher)
        {
            _context.Publishers.Update(publisher);            // Update the publisher
            await _context.SaveChangesAsync();          // Save changes asynchronously
        }

        public async Task DeleteAsync(int id)
        {
            var publisher = await GetByIdAsync(id);
            if (publisher != null)
            {
                _context.Publishers.Remove(publisher);         // Remove the publisher
                await _context.SaveChangesAsync();      // Save changes asynchronously
            }
        }
        public async Task<int> Count()
        {
            return await _context.Publishers.CountAsync();
        }
        public void Add(Publisher publisher)
        {
            throw new NotImplementedException();
        }

        public void Update(Publisher publisher)
        {
            throw new NotImplementedException();
        }

        public void Delete(int publisherId)
        {
            throw new NotImplementedException();
        }

        public Publisher GetById(int publisherId)
        {
            throw new NotImplementedException();
        }

        public bool PublisherExists(int publisherId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Publisher> GetAll()
        {
            throw new NotImplementedException();
        }
    }
    // Check if a publisher exists by ID
    //   public bool PublisherExists(int publisherId)
    //	{
    //		return _context.Publishers.Any(a => a.Publisher_ID == publisherId);
    //	}
    //}
}