namespace online_store_app.Models.DTO
{
   [GraphQLName("chart_response")]
   public class ChartResponse
   {
      [GraphQLName("id")]
      public int? Id { get; set; }

      [GraphQLName("user_id")]
      public int? UserId { get; set; }

      [GraphQLName("product_id")]
      public int? ProductId { get; set; }

      [GraphQLName("amount")]
      public int? Amount { get; set; }

      [GraphQLName("total_price")]
      public int? TotalPrice { get; set; }

      [GraphQLName("user")]
      public UserResponse? User { get; set; }

      [GraphQLName("product")]
      public ProductResponse? Product { get; set; }
   }
}
