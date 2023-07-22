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
      private readonly ChartService _chartService;

      // create constructor
      public UserService(UserRepository userRepo, ChartRepository chartRepo, ChartService chartService)
      {
         this._userRepo = userRepo;
         this._chartRepo = chartRepo;
         this._chartService = chartService;
      }

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

      public Task<UserResponse?> GetUserByIdAsync(int? id)
      {
         throw new NotImplementedException();
      }
   }
}
