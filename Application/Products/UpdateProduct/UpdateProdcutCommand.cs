using Application.Products.Model;
using MediatR;
using MongoDB.Bson.Serialization.Attributes;

namespace Application.Products.UpdateProduct
{
    public class UpdateProdcutCommand : IRequest<ProductModel>
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public required string Name { get; set; }
        public required string UserDescription { get; set; }
        public required string Price { get; set; }
        public required string Category { get; set; }
        public string? Color { get; set; }

    }
}
