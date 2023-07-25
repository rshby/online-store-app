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
         catch (Exception err)
         {
            tr.Dispose();

            // send error message
            throw new GraphQLException(new ErrorBuilder().SetMessage(err.Message).Build());
         }
      }

      // method get data chart by id
      public async Task<Chart?> GetChartByIdAsync([Required] TransactionScope tr, int? id)
      {
         try
         {
            return await _db.Charts.AsQueryable().FirstOrDefaultAsync<Chart>(x => x.Id == id);
         }
         catch (Exception err)
         {
            tr.Dispose();
            throw new GraphQLException(new ErrorBuilder().SetMessage(err.Message).Build());
         }
      }

      // method get all data charts
      public async Task<List<Chart>?> GetAllChartsAsync([Required] TransactionScope tr)
      {
         try
         {
            // select data from database
            List<Chart>? charts = await _db.Charts.AsQueryable().ToListAsync();

            // jika data tidak ditemukan
            if (charts == null || charts.Count == 0)
            {
               return null;
            }

            // success get all data charts
            return charts;
         }
         catch (Exception err)
         {
            tr.Dispose();

            // send error message
            throw new GraphQLException(new ErrorBuilder().SetMessage(err.Message).Build());
         }
      }

      // method add new data chart
      public async Task<Chart?> AddChartAsync([Required] TransactionScope tr, Chart? newChart)
      {
         try
         {
            await _db.Charts.AddAsync(newChart);

            await _db.SaveChangesAsync();

            return newChart;
         }
         catch (Exception err)
         {
            tr.Dispose();

            // send error message
            throw new GraphQLException(new ErrorBuilder().SetMessage(err.Message).Build());
         }
      }

      // method to update data chart
      public async Task<Chart?> UpdateChartAsync([Required] TransactionScope tr, Chart? oldChart, Chart? newChart)
      {
         try
         {
            _db.Charts.Entry(oldChart).CurrentValues.SetValues(newChart);

            await _db.SaveChangesAsync();
            return newChart;
         }
         catch (Exception err)
         {
            tr.Dispose();
            throw new GraphQLException(new ErrorBuilder().SetMessage(err.Message).Build());
         }
      }
   }
}
