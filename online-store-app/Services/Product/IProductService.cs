using online_store_app.Models.DTO;

namespace online_store_app.Services.Product
{
   public interface IProductService
   {
      // method to get all data products
      public Task<List<ProductResponse>?> GetAllProductsAsync();

      // method to get data product by Id
      public Task<ProductResponse?> GetProductByIdAsync(int? id);
   }
}
