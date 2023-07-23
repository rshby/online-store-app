using online_store_app.Models.Entity;

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
      public User? User { get; set; }

      [GraphQLName("product")]
      public Product? Product { get; set; }
   }
}
