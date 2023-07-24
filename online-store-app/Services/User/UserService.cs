using online_store_app.Models.DTO;
using online_store_app.Models.Entity;
using online_store_app.Repositories;
using online_store_app.Services.Chart;
using System.Transactions;

namespace online_store_app.Services.User
{
   public class UserService : IUserService
   {
      // global variable
      private readonly UserRepository _userRepo;
      private readonly ChartRepository _chartRepo;
      private readonly ProductRepository _productRepo;

      // create constructor
      public UserService(UserRepository userRepo, ChartRepository chartRepo, ProductRepository productRepo)
      {
         this._userRepo = userRepo;
         this._chartRepo = chartRepo;
         this._productRepo = productRepo;
      }

      // method get all data users
      public async Task<List<UserResponse>?> GetAllUserAsync()
      {
         using (var tr = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
         {
            try
            {
               // call procedure get users in repository
               var users = await _userRepo.GetAllUsersAsync(tr);

               var response = users?.Select(x => new UserResponse()
               {
                  Id = x.Id,
                  IdentityNumber = x.IdentityNumber,
                  Name = x.Name,
                  BirthDate = ((DateTime)x.BirthDate).ToString("yyyy-MM-dd"),
                  Gender = x.Gender,
                  PhoneNumber = x.PhoneNumber,
                  Address = x.Address,
                  Charts = _chartRepo.GetChartsByUserIdAsync(tr, x.Id).Result
               }).ToList();

               // add product
               foreach (var data in response)
               {
                  if (data.Charts != null)
                  {
                     foreach (var chart in data.Charts)
                     {
                        chart.Product = _productRepo.GetProductByIdAsync(tr, chart.ProductId).Result;
                     }
                  }
               }

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

      // method get data user by id
      public async Task<UserResponse?> GetUserByIdAsync(int? id)
      {
         using (var tr = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
         {
            try
            {
               // call procedure GetUserById in Repository
               var user = await _userRepo.GetUserByIdAsync(tr, id);

               // jika data user tidak ditemukan
               if (user == null)
               {
                  tr.Dispose();

                  // send error not found message
                  throw new GraphQLException(new ErrorBuilder().SetMessage("record not found").Build());
               }

               // mapping to DTO
               var response = new UserResponse()
               {
                  Id = user.Id,
                  IdentityNumber = user.IdentityNumber,
                  Name = user.Name,
                  BirthDate = ((DateTime)user.BirthDate).ToString("yyyy-MM-dd"),
                  Gender = user.Gender,
                  PhoneNumber = user.PhoneNumber,
                  Address = user.Address,
                  Charts = _chartRepo.GetChartsByUserIdAsync(tr, user.Id).Result
               };

               // add product in each chart
               if (response.Charts != null)
               {
                  foreach (var chart in response.Charts)
                  {
                     chart.Product = await _productRepo.GetProductByIdAsync(tr, chart.Id);
                  }
               }

               // success
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

      // method to add new data user
      public async Task<UserResponse?> AddUserAsync(AddUserRequest? request)
      {
         using (var tr = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
         {
            try
            {
               // cek data dengan identity_number
               var cekuser = await _userRepo.GetUserByIdentityNumberAsync(tr, request.IdentityNumber);

               // jika data sudah ada
               if (cekuser != null)
               {
                  tr.Dispose();

                  // send error bad request
                  throw new GraphQLException(new ErrorBuilder().SetMessage("data with same identity_number already exist in our database").Build());
               }

               // create entity
               var newUser = new Models.Entity.User()
               {
                  IdentityNumber = request.IdentityNumber,
                  Name = request.Name,
                  BirthDate = DateTime.ParseExact($"{request.BirthDate} 00:00:00", "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture),
                  Gender = request.Gender,
                  PhoneNumber = request.PhoneNumber,
                  Address = request.Address,
               };

               // call procedure insert in Repository
               var resultInsert = await _userRepo.AddUserAsync(tr, newUser);

               // mapping to DTO
               tr.Complete();
               return new UserResponse()
               {
                  Id = resultInsert?.Id,
                  IdentityNumber = resultInsert?.IdentityNumber,
                  Name = resultInsert?.Name,
                  BirthDate = ((DateTime)resultInsert?.BirthDate).ToString("yyyy-MM-dd"),
                  Gender = resultInsert?.Gender,
                  PhoneNumber = resultInsert?.PhoneNumber,
                  Address = resultInsert?.Address,
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
   }
}
