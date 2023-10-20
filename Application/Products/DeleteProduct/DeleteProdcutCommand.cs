using MediatR;

namespace Application.Products.DeleteProduct
{
    public class DeleteProdcutCommand : IRequest<bool>
    {
        public string Id { get; set; } = string.Empty;

    }
}
