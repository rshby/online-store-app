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
      [GraphQLName("id")]
      [Column("id", Order = 1)]
      public int? Id { get; set; }

      [Required]
      [Column("user_id", Order = 2)]
      [ForeignKey("User")]
      [GraphQLName("user_id")]
      public int? UserId { get; set; }

      [Required]
      [Column("product_id", Order = 3)]
      [ForeignKey("Product")]
      [GraphQLName("product_id")]
      public int? ProductId { get; set; }

      [Required]
      [Column("amount", Order = 4)]
      [GraphQLName("amount")]
      public int? Amount { get; set; }

      [Required]
      [Column("total_price", Order = 5)]
      [GraphQLName("total_price")]
      public int? TotalPrice { get; set; }

      // relation
      [JsonIgnore]
      [GraphQLName("user")]
      public User? User { get; set; }

      [JsonIgnore]
      [GraphQLName("product")]
      public Product? Product { get; set; }
   }
}
