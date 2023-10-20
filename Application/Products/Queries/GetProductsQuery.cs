using Application.Products.Model;
using MediatR;

namespace Application.Products.Queries
{
    public class GetProductsQuery : IRequest<List<ProductModel>>
    {

    }
}
