using online_store_app.Models.DTO;

namespace online_store_app.Services.User
{
   public interface IUserService
   {
      // method to get all data users
      public Task<List<UserResponse>?> GetAllUserAsync();

      // method to get data users by Id
      public Task<UserResponse?> GetUserByIdAsync(int? id);

      // method add new user data
      public Task<UserResponse?> AddUserAsync(AddUserRequest? request);
   }
}
