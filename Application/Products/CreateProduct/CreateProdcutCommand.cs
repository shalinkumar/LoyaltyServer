using Application.Products.Model;
using MediatR;

namespace Application.Products.CreateProduct
{
    public class CreateProdcutCommand : IRequest<ProductModel>
    {
        public required string Name { get; set; }        
        public required string UserDescription { get; set; }
        public required string Price { get; set; }
        public required string Category { get; set; }
        public string? Color { get; set; }
    }
}
