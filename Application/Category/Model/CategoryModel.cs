using MongoDB.Bson.Serialization.Attributes;

namespace Application.Category.Model
{
    public class CategoryModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

    }
}
