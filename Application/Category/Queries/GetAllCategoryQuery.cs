using Application.Category.Model;
using MediatR;

namespace Application.Category.Queries
{
    public class GetAllCategoryQuery : IRequest<List<CategoryModel>>
    {

    }
}
