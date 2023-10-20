
namespace webapi.Requests.Product
{
    public class CreateProductRequest
    {
        public required string Name { get; set; }
        public string UserDescription { get; set; } = string.Empty;
        public required string Price { get; set; }
        public required string Category { get; set; }
        public string? Color { get; set; }
    }
}
