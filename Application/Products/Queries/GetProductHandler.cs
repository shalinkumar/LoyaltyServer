using MediatR;
using Application.Products.Model;

namespace Application.Products.Queries
{
    public class GetProductHandler : IRequestHandler<GetProductsQuery, List<ProductModel>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductModel>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetAllProducts(cancellationToken);
        }
    }
}
