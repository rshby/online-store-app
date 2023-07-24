using online_store_app.Models.DTO;
using online_store_app.Services.User;
using System.ComponentModel.DataAnnotations;

namespace online_store_app.Resolver
{
   public class OnlineStoreMutationType
   {
      // handler to add new data user
      [GraphQLName("user")]
      [UseMutationConvention]
      public async Task<UserResponse?> AddUserAsync([Service] IUserService userService, [Required] AddUserRequest? request) => await userService.AddUserAsync(request);
   }
}
