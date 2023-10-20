using Application.Category.Model;

namespace Application.Category
{
    public interface ICategoryRepository
    {
        Task<List<CategoryModel>> GetAllCategory(CancellationToken token);
        Task CreateCategory(CategoryModel model, CancellationToken token);
        Task UpdateCategory(CategoryModel model, CancellationToken token);
        Task<bool> DeleteCategory(CategoryModel model, CancellationToken token);
    }
}
