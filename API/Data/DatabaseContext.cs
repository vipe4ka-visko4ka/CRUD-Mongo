using API.Models;
using MongoDB.Driver;

namespace API.Data
{
    public class DatabaseContext : IDatabaseContext
    {
        public DatabaseContext(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Categories = database.GetCollection<Category>(settings.CategoryCollectionName);
        }

        public IMongoCollection<Category> Categories { get; }
    }

    public interface IDatabaseContext
    {
        IMongoCollection<Category> Categories { get; }
    }
}
