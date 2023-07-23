using online_store_app.Models.DTO;
using online_store_app.Repositories;
using System.Transactions;

namespace online_store_app.Services.Product
{
   public class ProductService : IProductService
   {
      // global variable
      private readonly ProductRepository _productRepo;

      // create constructor
      public ProductService(ProductRepository productRepo)
      {
         this._productRepo = productRepo;
      }

      // method get all data products
      public async Task<List<ProductResponse>?> GetAllProductsAsync()
      {
         using (var tr = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
         {
            try
            {
               // call procedure GetAllProducts in repository
               var products = await _productRepo.GetAllProductsAsync(tr);

               // jika data tidak ditemukan
               if (products == null || products.Count == 0)
               {
                  return null;
               }

               // success get all data products -> mapping to DTO
               List<ProductResponse>? response = products.Select(data => new ProductResponse()
               {
                  Id = data.Id,
                  Brand = data.Brand,
                  Name = data.Name,
                  Price = data.Price,
                  Stock = data.Stock,
               }).ToList();

               // return
               return response;
            }
            catch (Exception err)
            {
               tr.Dispose();

               // send error message
               throw new GraphQLException(new ErrorBuilder().SetMessage(err.Message).Build());
            }
         }
      }

      // method to get data product by Id
      public async Task<ProductResponse?> GetProductByIdAsync(int? id)
      {
         using (var tr = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
         {
            try
            {
               // call procedure from repository
               var product = await _productRepo.GetProductByIdAsync(tr, id);

               // jika data tidak ditemukan
               if (product == null)
               {
                  return null;
               }

               // success get data product by Id
               ProductResponse? response = new ProductResponse()
               {
                  Id = product.Id,
                  Brand = product.Brand,
                  Name = product.Name,
                  Price = product.Price,
                  Stock = product.Stock,
               };

               // return
               return response;
            }
            catch (Exception err)
            {
               tr.Dispose();

               // send error message
               throw new GraphQLException(new ErrorBuilder().SetMessage(err.Message).Build());
            }
         }
      }
   }
}
