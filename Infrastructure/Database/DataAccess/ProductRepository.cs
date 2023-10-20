using Application.Products;
using Application.Products.Model;
using MongoDB.Driver;

namespace Infrastructure.Database.DataAccess
{
    public class ProductRepository : IProductRepository
    {
        public string ConnectionString = "mongodb://localhost:27017/";
        public string DataBaseName = "LoyaltyDb";
        public string ProductCollection = "ProductCollection";
        public string Collection = "Users";
        public string ChoreHistoryCollection = "ChoreHistory";


        public IMongoCollection<T> ConnectToMongo<T>(in string collection)
        {
            var client = new MongoClient(ConnectionString);
            var db = client.GetDatabase(DataBaseName);
            return db.GetCollection<T>(collection);
        }

        public async Task<List<ProductModel>> GetAllProducts(CancellationToken token)
        {
            var productCollection = ConnectToMongo<ProductModel>(ProductCollection);
            var result = await productCollection.FindAsync(_ => true);
            return result.ToList();
        }

        public async Task CreateProduct(ProductModel model, CancellationToken token)
        {
            var productCollection = ConnectToMongo<ProductModel>(ProductCollection);
            await productCollection.InsertOneAsync(model);
        }

        public async Task UpdateProduct(ProductModel model, CancellationToken token)
        {
            var productCollection = ConnectToMongo<ProductModel>(ProductCollection);
            var filter = Builders<ProductModel>.Filter.Eq("Id", model.Id);
            await productCollection.ReplaceOneAsync(filter, model, new ReplaceOptions { IsUpsert = true });
        }

        public async Task<bool> DeleteProduct(ProductModel model, CancellationToken token)
        {
            var productCollection = ConnectToMongo<ProductModel>(ProductCollection);
            var filter = Builders<ProductModel>.Filter.Eq("Id", model.Id);
            await productCollection.DeleteOneAsync(filter);

            return true;
        }
    }
}
