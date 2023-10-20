using Application.Products.Model;

namespace Application.Products
{
    public interface IProductRepository
    {
        Task<List<ProductModel>> GetAllProducts(CancellationToken token);
        Task CreateProduct(ProductModel model, CancellationToken token);
        Task UpdateProduct(ProductModel model, CancellationToken token);
        Task<bool> DeleteProduct(ProductModel model, CancellationToken token);

    }
}
