namespace webapi.Requests.Product
{
    public class UpdateProductRequest
    {
        public string Id { get; set; }
        public required string Name { get; set; }
        public required string UserDescription { get; set; }
        public required string Price { get; set; }
        public required string Category { get; set; }
        public string? Color { get; set; }
    }
}
