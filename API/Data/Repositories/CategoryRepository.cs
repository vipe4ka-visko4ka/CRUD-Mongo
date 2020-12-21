using API.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IDatabaseContext _context;

        public CategoryRepository(IDatabaseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _context
                .Categories
                .Find(c => true)
                .ToListAsync();
        }

        public async Task<Category> GetById(string id)
        {
            return await _context
                .Categories
                .Find(c => c.Id == id)
                .FirstOrDefaultAsync();
        }
    }

    public interface ICategoryRepository
    {
        public Task<IEnumerable<Category>> GetAll();
        public Task<Category> GetById(string id);
    }
}
