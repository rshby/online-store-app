using System.ComponentModel.DataAnnotations;

namespace online_store_app.Models.DTO
{
   [GraphQLName("add_product")]
   public class AddProductRequest
   {
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
