using API.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Data.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        protected readonly IMongoCollection<T> _collection;

        public BaseRepository(IMongoCollection<T> collection)
        {
            _collection = collection;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _collection
                .Find(item => true)
                .ToListAsync();
        }

        public async Task<T> GetById(string id)
        {
            return await _collection
                .Find(item => item.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<bool> CheckIfAnyExists() => _collection.Find(item => true).AnyAsync();
        public Task InsertManyAsync(IEnumerable<T> data) => _collection.InsertManyAsync(data);
    }

    public interface IBaseRepository<T> where T : BaseModel
    {
        public Task<IEnumerable<T>> GetAll();
        public Task<T> GetById(string id);
        public Task<bool> CheckIfAnyExists();
        public Task InsertManyAsync(IEnumerable<T> data);
    }
}
