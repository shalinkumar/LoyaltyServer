using MediatR;

namespace Application.Category.DeleteCatogery
{
    public class DeleteCategoryCommand : IRequest<bool>
    {
        public string Id { get; set; } = string.Empty;
    }
}
