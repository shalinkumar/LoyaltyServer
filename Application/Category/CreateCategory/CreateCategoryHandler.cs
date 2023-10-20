using Application.Category.Model;
using MediatR;

namespace Application.Category.CreateCategory
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, CategoryModel>
    {
        private readonly ICategoryRepository _categoryRepository;
        public CreateCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryModel> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var res = new CategoryModel()
            {
                Name = request.Name,
                Description = request.Description
            };
            await _categoryRepository.CreateCategory(res, cancellationToken);
            Sender.Send("Category Created");
            return res;
        }
    }
}
