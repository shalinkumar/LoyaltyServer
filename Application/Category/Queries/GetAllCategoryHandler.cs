using Application.Category.Model;
using MediatR;

namespace Application.Category.Queries
{
    public class GetAllCategoryHandler : IRequestHandler<GetAllCategoryQuery, List<CategoryModel>>
    {
        private readonly ICategoryRepository _categoryRepository;
        public GetAllCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryModel>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            return await _categoryRepository.GetAllCategory(cancellationToken);
        }
    }
}
