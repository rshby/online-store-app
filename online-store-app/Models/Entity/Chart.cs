using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;

namespace online_store_app.Models.Entity
{
   // representasi tabel charts

   [Table("charts")]
   public class Chart
   {
      [Required]
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      [Column("id", Order = 1)]
      public int? Id { get; set; }

      [Required]
      [Column("user_id", Order = 2)]
      [ForeignKey("User")]
      public int? UserId { get; set; }

      [Required]
      [Column("product_id", Order = 3)]
      [ForeignKey("Product")]
      public int? ProductId { get; set; }

      [Required]
      [Column("amount", Order = 4)]
      public int? Amount { get; set; }

      [Required]
      [Column("total_price", Order = 5)]
      public int? TotalPrice { get; set; }

      // relation
      [JsonIgnore]
      public User? User { get; set; }

      [JsonIgnore]
      public Product? Product { get; set; }
   }
}
