using Application.Category;
using Application.Category.Model;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using System.Text.Json;
using System;

namespace Infrastructure.Database.DataAccess
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly IConfiguration _configuration;
        private string ConnectionString = string.Empty;
        private string DataBaseName = string.Empty;
        private string CategoryCollection = string.Empty;

        public CategoryRepository(IConfiguration configuration)
        {
            _configuration = configuration;


            ConnectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";
            DataBaseName = _configuration.GetConnectionString("DataBaseName") ?? "";
            CategoryCollection = _configuration.GetConnectionString("CategoryCollection") ?? "";

        }

        public IMongoCollection<T> ConnectToMongo<T>(in string collection)
        {
           
            var settings = MongoClientSettings.FromConnectionString(ConnectionString);

            // Set the ServerApi field of the settings object to Stable API version 1
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(ConnectionString);

            try
            {              
                var result = client.GetDatabase(DataBaseName);
                Console.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");
                return result.GetCollection<T>(collection);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public async Task CreateCategory(CategoryModel model, CancellationToken token)
        {
            var categoryCollection = ConnectToMongo<CategoryModel>(CategoryCollection);
            await categoryCollection.InsertOneAsync(model);
        }

        public async Task<bool> DeleteCategory(CategoryModel model, CancellationToken token)
        {
            var categoryCollection = ConnectToMongo<CategoryModel>(CategoryCollection);
            var filter = Builders<CategoryModel>.Filter.Eq("Id", model.Id);
            await categoryCollection.DeleteOneAsync(filter);

            return true;
        }

        public async Task<List<CategoryModel>> GetAllCategory(CancellationToken token)
        {
            var categoryCollection = ConnectToMongo<CategoryModel>(CategoryCollection);
            var result = await categoryCollection.FindAsync(_ => true);
            return result.ToList();
        }

        public async Task UpdateCategory(CategoryModel model, CancellationToken token)
        {
            var categoryCollection = ConnectToMongo<CategoryModel>(CategoryCollection);
            var filter = Builders<CategoryModel>.Filter.Eq("Id", model.Id);
            await categoryCollection.ReplaceOneAsync(filter, model, new ReplaceOptions { IsUpsert = true });
        }
    }
}
