namespace webapi.Requests.Category
{
    public class UpdateCategoryRequest
    {
        public string Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}
