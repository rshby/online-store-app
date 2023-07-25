using System.ComponentModel.DataAnnotations;

namespace online_store_app.Models.DTO
{
   [GraphQLName("update_chart")]
   public class UpdateChartRequest
   {
      [Required]
      [GraphQLName("id")]
      [GraphQLNonNullType]
      public int? Id { get; set; }

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
