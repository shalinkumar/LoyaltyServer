using Application.Category.Model;
using MediatR;

namespace Application.Category.UpdateCategory
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, CategoryModel>
    {
        private readonly ICategoryRepository _categoryRepository;
        public UpdateCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryModel> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var res = new CategoryModel()
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description
            };
            await _categoryRepository.UpdateCategory(res, cancellationToken);
            //Sender.Send("Category Updated");
            return res;
        }
    }
}
