using Microsoft.EntityFrameworkCore;
using online_store_app.Data;
using online_store_app.Models.Entity;
using System.ComponentModel.DataAnnotations;
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
      public async Task<List<User>?> GetAllUsersAsync([Required] TransactionScope tr)
      {
         try
         {
            List<User>? users = await _db.Users.AsQueryable().ToListAsync();

            // cek jika data nasabah tidak ditemukan
            if (users == null || users.Count == 0)
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
      public async Task<User?> GetUserByIdAsync([Required] TransactionScope tr, int? id)
      {
         try
         {
            return await _db.Users.AsQueryable().FirstOrDefaultAsync<User>(x => x.Id == id);
         }
         catch (Exception err)
         {
            tr.Dispose();

            // send error message
            throw new GraphQLException(new ErrorBuilder().SetMessage(err.Message).Build());
         }
      }

      // method to get data user by identity_number
      public async Task<User?> GetUserByIdentityNumberAsync([Required] TransactionScope tr, string? identityNumber)
      {
         try
         {
            User? user = await _db.Users.AsQueryable().FirstOrDefaultAsync(x => x.IdentityNumber == identityNumber);

            // jika data tidak ditemukan
            if (user == null)
            {
               return null;
            }

            // success get data
            return user;
         }
         catch (Exception err)
         {
            tr.Dispose();

            // send error message
            throw new GraphQLException(new ErrorBuilder().SetMessage(err.Message).Build());
         }
      }

      // method to insert/add new data user
      public async Task<User?> AddUserAsync([Required] TransactionScope tr, User? newUser)
      {
         try
         {
            await _db.Users.AddAsync(newUser);

            await _db.SaveChangesAsync();

            return newUser;
         }
         catch (Exception err)
         {
            tr.Dispose();

            // send error message
            throw new GraphQLException(new ErrorBuilder().SetMessage(err.Message).Build());
         }
      }

      // method to update data user
      public async Task<User?> UpdateUserAsync([Required] TransactionScope tr, User? oldUser, User? newUser)
      {
         try
         {
            _db.Users.Entry(oldUser).CurrentValues.SetValues(newUser);

            await _db.SaveChangesAsync();

            return newUser;
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
