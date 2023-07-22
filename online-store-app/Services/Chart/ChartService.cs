using online_store_app.Models.DTO;
using online_store_app.Repositories;
using System.Transactions;

namespace online_store_app.Services.Chart
{
   public class ChartService : IChartService
   {
      // global variable
      private readonly ChartRepository _chartRepo;

      // create constructor
      public ChartService(ChartRepository chartRepo)
      {
         this._chartRepo = chartRepo;
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
                  return null;
               }

               // success get data charts by user_id
               var response = charts.Select(x => new ChartResponse()
               {
                  Id = x.Id,
                  UserId = x.UserId,
                  ProductId = x.ProductId,
                  Amount = x.Amount,
                  Product = 
               });

               // return
               tr.Complete();
               return response.ToList();
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
