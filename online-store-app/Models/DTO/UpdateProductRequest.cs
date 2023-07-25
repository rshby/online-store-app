using System.ComponentModel.DataAnnotations;

namespace online_store_app.Models.DTO
{
   [GraphQLName("update_product")]
   public class UpdateProductRequest
   {
      [Required]
      [GraphQLName("id")]
      [GraphQLNonNullType]
      public int? Id { get; set; }

      [Required]
      [GraphQLName("brand")]
      [GraphQLNonNullType]
      [StringLength(255)]
      public string? Brand { get; set; }

      [Required]
      [GraphQLName("name")]
      [GraphQLNonNullType]
      [StringLength(255)]
      public string? Name { get; set; }

      [Required]
      [GraphQLName("price")]
      [GraphQLNonNullType]
      public int? Price { get; set; }

      [Required]
      [GraphQLName("stock")]
      [GraphQLNonNullType]
      public int? Stock { get; set; }
   }
}
