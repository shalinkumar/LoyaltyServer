using Application.Category;
using Application.Category.Model;
using MongoDB.Driver;

namespace Infrastructure.Database.DataAccess
{
    public class CategoryRepository : ICategoryRepository
    {
        public string ConnectionString = "mongodb://localhost:27017/";
        public string DataBaseName = "LoyaltyDb";
        public string CategoryCollection = "CategoryCollection";


        public IMongoCollection<T> ConnectToMongo<T>(in string collection)
        {
            var client = new MongoClient(ConnectionString);
            var db = client.GetDatabase(DataBaseName);
            return db.GetCollection<T>(collection);
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
