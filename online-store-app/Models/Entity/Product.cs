using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace online_store_app.Models.Entity
{
   // representasi tabel products

   [Table("products")]
   public class Product
   {
      [Required]
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      [Column("id", Order = 1)]
      [GraphQLName("id")]
      public int? Id { get; set; }

      [Required]
      [Column("brand", Order = 2)]
      [MaxLength(255)]
      [GraphQLName("brand")]
      public string? Brand { get; set; }

      [Required]
      [Column("name", Order = 3)]
      [MaxLength(255)]
      [GraphQLName("name")]
      public string? Name { get; set; }

      [Required]
      [Column("price", Order = 4)]
      [GraphQLName("price")]
      public int? Price { get; set; }

      [Required]
      [Column("stock", Order = 5)]
      [GraphQLName("stock")]
      public int? Stock { get; set; }

      // relation
      [JsonIgnore]
      [GraphQLName("charts")]
      public ICollection<Chart>? Charts { get; set; }
   }
}
