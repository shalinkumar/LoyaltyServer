using Application.Products.Model;
using MediatR;

namespace Application.Products.UpdateProduct
{
    public class UpdateProductHandler : IRequestHandler<UpdateProdcutCommand, ProductModel>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductModel> Handle(UpdateProdcutCommand request, CancellationToken cancellationToken)
        {
            var res = new ProductModel()
            {
                Id = request.Id,
                Category = request.Category,
                Name = request.Name,
                Description = request.UserDescription + "-" + request.Name,
                Price = request.Price,
                Color = request.Color
            };
            await _productRepository.UpdateProduct(res, cancellationToken);
            //Sender.Send("Product Updated");
            return res;
        }
    }
}
