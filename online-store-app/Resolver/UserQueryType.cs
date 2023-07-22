using online_store_app.Models.DTO;
using online_store_app.Services.User;

namespace online_store_app.Resolver
{
   public class UserQueryType
   {
      // handler to get all data users
      [GraphQLName("users")]
      public async Task<List<UserResponse>?> GetUsersAsync([Service] IUserService userService) => await userService.GetAllUserAsync();
   }
}
