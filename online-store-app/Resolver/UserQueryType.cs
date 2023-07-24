using online_store_app.Models.DTO;
using online_store_app.Repositories;
using online_store_app.Services.Chart;
using online_store_app.Services.Product;
using online_store_app.Services.User;
using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace online_store_app.Resolver
{
   public class UserQueryType
   {
      // handler to get all data users
      [GraphQLName("users")]
      public async Task<List<UserResponse>?> GetUsersAsync([Service] IUserService userService) => await userService.GetAllUserAsync();

      // handler to get data user by id
      [GraphQLName("user")]
      public async Task<UserResponse?> GetUserByIdAsync([Service] IUserService userService, [Required] int? id) => await userService.GetUserByIdAsync(id);

      // handler get data charts by user_id
      [GraphQLName("chart")]
      public async Task<List<ChartResponse>?> GetAllChartByIdAsync([Service] IChartService chartService, [Required] int? user_id) => await chartService.GetChartsByUserIdAsync(user_id);

      // handler get all data charts
      [GraphQLName("charts")]
      public async Task<List<ChartResponse>?> GetAllChartsAsync([Service] IChartService chartService) => await chartService.GetAllChartsAsync();

      // handler get all products
      [GraphQLName("prodcts")]
      public async Task<List<ProductResponse>?> GetAllProductsAsync([Service] IProductService productService) => await productService.GetAllProductsAsync();

      // handler get product by id
      [GraphQLName("product")]
      public async Task<ProductResponse?> GetProductByIdAsync([Service] IProductService productService, [Required] int? id) => await productService.GetProductByIdAsync(id);
   }
}
