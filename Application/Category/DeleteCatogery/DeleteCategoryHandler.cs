using Application.Category.Model;
using MediatR;

namespace Application.Category.DeleteCatogery
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly ICategoryRepository _categoryRepository;
        public DeleteCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            return await _categoryRepository.DeleteCategory(new CategoryModel()
            {
                Id = request.Id
            }, cancellationToken);
        }
    }
}
