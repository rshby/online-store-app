using System.ComponentModel.DataAnnotations;

namespace online_store_app.Models.DTO
{
   [GraphQLName("add_chart")]
   public class AddChartRequest
   {
      [Required]
      [GraphQLName("user_id")]
      [GraphQLNonNullType]
      public int? UserId { get; set; }

      [Required]
      [GraphQLName("product_id")]
      [GraphQLNonNullType]
      public int? ProductId { get; set; }

      [Required]
      [GraphQLName("amount")]
      [GraphQLNonNullType]
      public int? Amount { get; set; }
   }
}
