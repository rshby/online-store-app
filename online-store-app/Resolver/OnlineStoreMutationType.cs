﻿using online_store_app.Models.DTO;
using online_store_app.Services.Chart;
using online_store_app.Services.Product;
using online_store_app.Services.User;
using System.ComponentModel.DataAnnotations;

namespace online_store_app.Resolver
{
   public class OnlineStoreMutationType
   {
      // handler to add new data user
      [GraphQLName("add_user")]
      [UseMutationConvention]
      public async Task<UserResponse?> AddUserAsync([Service] IUserService userService, [Required] AddUserRequest? request) => await userService.AddUserAsync(request);

      // handler to add new data product
      [GraphQLName("add_product")]
      public async Task<ProductResponse?> AddProductAsync([Service] IProductService productService, [Required] AddProductRequest? request) => await productService.AddProductAsync(request);

      // handler to add new data chart
      [GraphQLName("add_chart")]
      public async Task<ChartResponse?> AddChartAsync([Service] IChartService chartService, [Required] AddChartRequest? request) => await chartService.AddNewChartAsync(request);

      // handler to update data user
      [GraphQLName("update_user")]
      public async Task<UserResponse?> UpdateUserAsync([Service] IUserService userService, [Required] UpdateUserRequest? request) => await userService.UpdateUserAsync(request);
   }
}
