using MongoDB.Bson.Serialization.Attributes;

namespace Application.Products.Model
{
    public class ProductModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string? Color { get; set; } = string.Empty;
    }
}
