using Application.Category.Model;
using MediatR;

namespace Application.Category.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest<CategoryModel>
    {
        public string Id { get; set; } = string.Empty;
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
