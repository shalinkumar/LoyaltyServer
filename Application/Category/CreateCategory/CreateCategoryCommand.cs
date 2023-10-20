using Application.Category.Model;
using MediatR;

namespace Application.Category.CreateCategory
{
    public class CreateCategoryCommand : IRequest<CategoryModel>
    {
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
