﻿using online_store_app.Models.DTO;

namespace online_store_app.Services.Chart
{
   public interface IChartService
   {
      // method to get charts by user_id
      public Task<List<ChartResponse>?> GetChartsByUserIdAsync(int? userId);

      // method to get all data charts
      public Task<List<ChartResponse>?> GetAllChartsAsync();

      // method to add new chart
      public Task<ChartResponse?> AddNewChartAsync(AddChartRequest? request);

      // method to update data
      public Task<ChartResponse?> UpdateChartAsync(UpdateChartRequest? request);
   }
}
