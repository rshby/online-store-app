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

      // method to add new product
      public async Task<ProductResponse?> AddProductAsync(AddProductRequest? request)
      {
         using (var tr = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
         {
            try
            {
               // create entity
               var newProduct = new Models.Entity.Product()
               {
                  Brand = request?.Brand,
                  Name = request?.Name,
                  Price = request?.Price,
                  Stock = request?.Stock
               };

               // call procedure add in Repository
               var resultInsert = await _productRepo.AddProductAsync(tr, newProduct);

               // mapping to DTO
               tr.Complete();
               return new ProductResponse()
               {
                  Id = resultInsert?.Id,
                  Brand = resultInsert?.Brand,
                  Name = resultInsert?.Name,
                  Price = resultInsert?.Price,
                  Stock = resultInsert?.Stock,
               };
            }
            catch (Exception err)
            {
               tr.Dispose();

               // send error message
               throw new GraphQLException(new ErrorBuilder().SetMessage(err.Message).Build());
            }
         }
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
                  tr.Dispose();

                  // send error message
                  throw new GraphQLException(new ErrorBuilder().SetMessage("record not found").Build());
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
               tr.Complete();
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
                  tr.Dispose();

                  // send error not found message
                  throw new GraphQLException(new ErrorBuilder().SetMessage("record not found").Build());
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
               tr.Complete();
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
