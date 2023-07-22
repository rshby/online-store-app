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
      public int? Id { get; set; }

      [Required]
      [Column("brand", Order = 2)]
      [MaxLength(255)]
      public string? Brand { get; set; }

      [Required]
      [Column("name", Order = 3)]
      [MaxLength(255)]
      public string? Name { get; set; }

      [Required]
      [Column("price", Order = 4)]
      public int? Price { get; set; }

      [Required]
      [Column("stock", Order = 5)]
      public int? Stock { get; set; }

      // relation
      [JsonIgnore]
      public ICollection<Chart>? Charts { get; set; }
   }
}
