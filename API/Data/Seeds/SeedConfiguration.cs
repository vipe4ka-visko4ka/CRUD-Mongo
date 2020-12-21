using System;
using Microsoft.Extensions.DependencyInjection;
using API.Data.Repositories;
using API.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using API.Utils;

namespace API.Data.Seeds
{
    public static class SeedConfiguration
    {
        public static void SeedDatabase(this IServiceProvider serviceScope)
        {
            var categoryRepository = serviceScope.GetService<ICategoryRepository>();

            Task.WaitAll(
                 new CategorySeeder(categoryRepository).seed()
            );
        }
    }

    public abstract class BaseSeeder<T> where T : BaseModel
    {
        protected readonly IBaseRepository<T> _repository;
        protected readonly string _jsonPath;

        public BaseSeeder(IBaseRepository<T> repository, string jsonPath)
        {
            _repository = repository;
            _jsonPath = jsonPath;
        }

        public async Task seed()
        {
            bool existData = await _repository.CheckIfAnyExists();

            if (existData)
            {
                return;
            }

            var data = await readJson();

            await _repository.InsertManyAsync(data);
        }

        private async Task<IEnumerable<T>> readJson()
        {
            using (var jsonFileReader = File.OpenText(PathUtils.SeedsDataPath(_jsonPath)))
            {
                return JsonSerializer.Deserialize<T[]>(await jsonFileReader.ReadToEndAsync(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }
    }

    public class CategorySeeder : BaseSeeder<Category>
    {
        public CategorySeeder(ICategoryRepository repository) : base(repository, "categories.json")
        {
        }
    }
}
