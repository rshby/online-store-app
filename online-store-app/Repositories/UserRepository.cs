using Microsoft.EntityFrameworkCore;
using online_store_app.Data;
using online_store_app.Models.Entity;
using System.Transactions;

namespace online_store_app.Repositories
{
   public class UserRepository
   {
      // global variable
      private readonly OnlineStoreContext _db;

      // create constructor
      public UserRepository(OnlineStoreContext db)
      {
         this._db = db;
      }

      // method to get all data users
      public async Task<List<User>?> GetAllUsersAsync(TransactionScope tr)
      {
         try
         {
            List<User>? users = await _db.Users.AsQueryable().ToListAsync();

            // cek jika data nasabah tidak ditemukan
            if(users == null || users.Count == 0)
            {
               tr.Dispose();

               // send error message
               throw new GraphQLException(new ErrorBuilder().SetMessage("record not found").Build());
            }

            // success get all data users
            return users;
         }
         catch (Exception err)
         {
            tr.Dispose();

            // send error message
            throw new GraphQLException(new ErrorBuilder().SetMessage(err.Message).Build());
         }
      }

      // method to get data users by id
      public async Task<User?> GetUserByIdAsync(TransactionScope tr, int? id)
      {
         try
         {
            User? user = await _db.Users.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

            // cek jika data tidak ditemukan
            if (user == null)
            {
               tr.Dispose();

               // send error message
               throw new GraphQLException(new ErrorBuilder().SetMessage("record not found").Build());
            }

            // success get data users by Id
            return user;
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
