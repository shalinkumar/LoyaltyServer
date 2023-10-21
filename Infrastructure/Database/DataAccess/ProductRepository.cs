using Application.Products;
using Application.Products.Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Database.DataAccess
{
    public class ProductRepository : IProductRepository
    {
        public string ConnectionString = "mongodb+srv://sayhi2shalin:9nQp8PjePkcEZitL@cluster0.cvloioj.mongodb.net/?retryWrites=true&w=majority";
        public string DataBaseName = "LoyaltyDb";
        public string ProductCollection = "ProductCollection";
       
        public IMongoCollection<T> ConnectToMongo<T>(in string collection)
        {
            
            var settings = MongoClientSettings.FromConnectionString(ConnectionString);
            
            // Set the ServerApi field of the settings object to Stable API version 1
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(ConnectionString);
            
            try
            {
                //var result = client.GetDatabase(DataBaseName).RunCommand<BsonDocument>(new BsonDocument("ping", 1));
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
