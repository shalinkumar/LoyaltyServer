using Application.Products.Model;
using MediatR;

namespace Application.Products.CreateProduct
{
    public class CreateProductHandler : IRequestHandler<CreateProdcutCommand, ProductModel>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ProductModel> Handle(CreateProdcutCommand request, CancellationToken cancellationToken)
        {
            var res = new ProductModel()
            {
                Category = request.Category,
                Name = request.Name,
                Description = request.UserDescription + "-" + request.Name,
                Price = request.Price,
                Color = request.Color
            };
            await _productRepository.CreateProduct(res, cancellationToken);
            Sender.Send("Product Created");
            return res;
        }
    }
}
