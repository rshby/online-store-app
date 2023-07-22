using Microsoft.EntityFrameworkCore;
using online_store_app.Data;
using online_store_app.Models.Entity;
using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace online_store_app.Repositories
{
   public class ChartRepository
   {
      // global variable
      private readonly OnlineStoreContext _db;

      // create constructor
      public ChartRepository(OnlineStoreContext db)
      {
         this._db = db;
      }

      // method get data charts by user_id
      public async Task<List<Chart>?> GetChartsByUserIdAsync([Required] TransactionScope tr, int? userId)
      {
         try
         {
            List<Chart>? charts = await _db.Charts.AsQueryable().Where(x => x.UserId == userId).ToListAsync();

            // jika data tidak ditemukan
            if (charts == null || charts.Count == 0)
            {
               return null;
            }

            // success get all charts by user_id
            return charts;
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
