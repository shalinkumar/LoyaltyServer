using Application.Products.Model;
using MediatR;

namespace Application.Products.DeleteProduct
{
    public class DeleteProductHandler : IRequestHandler<DeleteProdcutCommand, bool>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(DeleteProdcutCommand request, CancellationToken cancellationToken)
        {
            return await _productRepository.DeleteProduct(new ProductModel()
            {
                Id = request.Id
            }, cancellationToken);
        }
    }
}
