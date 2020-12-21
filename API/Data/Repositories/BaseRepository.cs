using API.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        protected readonly IMongoCollection<T> _collection;

        protected BaseRepository(IMongoCollection<T> collection)
        {
            _collection = collection;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _collection
                .Find(c => true)
                .ToListAsync();
        }

        public async Task<T> GetById(string id)
        {
            return await _collection
                .Find(c => c.Id == id)
                .FirstOrDefaultAsync();
        }
    }

    public interface IBaseRepository<T> where T : BaseModel
    {
        public Task<IEnumerable<T>> GetAll();
        public Task<T> GetById(string id);
    }
}
