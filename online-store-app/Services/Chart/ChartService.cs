using online_store_app.Models.DTO;
using online_store_app.Repositories;
using online_store_app.Services.Product;
using online_store_app.Services.User;
using System.Transactions;

namespace online_store_app.Services.Chart
{
   public class ChartService : IChartService
   {
      // global variable
      private readonly ChartRepository _chartRepo;
      private readonly UserRepository _userRepo;
      private readonly ProductRepository _productRepo;

      // create constructor
      public ChartService(ChartRepository chartRepo, UserRepository userRepo, ProductRepository productRepo)
      {
         this._chartRepo = chartRepo;
         this._userRepo = userRepo;
         this._productRepo = productRepo;
      }

      // method get all data charts by user_id
      public async Task<List<ChartResponse>?> GetChartsByUserIdAsync(int? userId)
      {
         using (var tr = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
         {
            try
            {
               // call procedure in repository
               var charts = await _chartRepo.GetChartsByUserIdAsync(tr, userId);

               // jika data tidak ditemukan
               if (charts == null || charts.Count == 0)
               {
                  tr.Dispose();

                  // send error message
                  throw new GraphQLException(new ErrorBuilder().SetMessage("record not found").Build());
               }

               // success get data charts by user_id -> Mapping to DTO
               var response = charts.Select(x => new ChartResponse()
               {
                  Id = x.Id,
                  UserId = x.UserId,
                  ProductId = x.ProductId,
                  Amount = x.Amount,
                  TotalPrice = x.TotalPrice,
                  User = _userRepo.GetUserByIdAsync(tr, x.UserId).Result,
                  Product = _productRepo.GetProductByIdAsync(tr, x.ProductId).Result
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

      // method get all data charts
      public async Task<List<ChartResponse>?> GetAllChartsAsync()
      {
         using (var tr = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
         {
            try
            {
               // call procedure get all charts in repository
               var charts = await _chartRepo.GetAllChartsAsync(tr);

               // jika data tidak ditemukan
               if (charts == null || charts.Count == 0)
               {
                  tr.Dispose();

                  // send error message
                  throw new GraphQLException(new ErrorBuilder().SetMessage("record not found").Build());
               }

               List<ChartResponse>? responses = charts.Select(data => new ChartResponse()
               {
                  Id = data.Id,
                  UserId = data.UserId,
                  ProductId = data.ProductId,
                  Amount = data.Amount,
                  TotalPrice = data.TotalPrice,
                  User = _userRepo.GetUserByIdAsync(tr, data.UserId).Result,
                  Product = _productRepo.GetProductByIdAsync(tr, data.ProductId).Result
               }).ToList();

               // return
               tr.Complete();
               return responses;
            }
            catch (Exception err)
            {
               tr.Dispose();

               // send error message
               throw new GraphQLException(new ErrorBuilder().SetMessage(err.Message).Build());
            }
         }
      }

      // method to add new chart
      public async Task<ChartResponse?> AddNewChartAsync(AddChartRequest? request)
      {
         using (var tr = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
         {
            try
            {
               // validasi user_id
               var findUser = await _userRepo.GetUserByIdAsync(tr, request?.UserId);

               // jika user tidak ada
               if (findUser == null)
               {
                  tr.Dispose();
                  throw new GraphQLException(new ErrorBuilder().SetMessage("user not found").Build());
               }

               // validasi product_id
               var findProduct = await _productRepo.GetProductByIdAsync(tr, request?.ProductId);

               // jika product tidak ditemukan
               if (findProduct == null)
               {
                  tr.Dispose();
                  throw new GraphQLException(new ErrorBuilder().SetMessage("product not found").Build());
               }

               // create entity
               var newChart = new Models.Entity.Chart()
               {
                  UserId = request?.UserId,
                  ProductId = request?.ProductId,
                  Amount = request?.Amount,
                  TotalPrice = request?.Amount * findProduct.Price
               };

               // call procedure insert in Repository
               var resultInsert = await _chartRepo.AddChartAsync(tr, newChart);

               // success add new chart
               ChartResponse? response = new ChartResponse()
               {
                  Id = resultInsert?.Id,
                  UserId = request?.UserId,
                  ProductId = resultInsert?.ProductId,
                  Amount = resultInsert?.Amount,
                  TotalPrice = resultInsert?.TotalPrice,
                  User = findUser,
                  Product = findProduct
               };

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
