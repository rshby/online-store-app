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
            catch(Exception err)
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
            catch(Exception err)
            {
               tr.Dispose();

               // send error message
               throw new GraphQLException(new ErrorBuilder().SetMessage(err.Message).Build());
            }
         }
      }
   }
}
