using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace online_store_app.Models.Entity
{
   [Table("users")]
   public class User
   {
      [Required]
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      [Column("id", Order = 1)]
      public int? Id { get; set; }

      [Required]
      [Column("identity_number", Order = 2)]
      [MaxLength(16), MinLength(16)]
      public string? IdentityNumber { get; set; }

      [Required]
      [Column("name", Order = 3)]
      [MaxLength(255)]
      public string? Name { get; set; }

      [Required]
      [Column("birth_date", Order = 4)]
      public DateTime? BirthDate { get; set; }

      [Column("gender", Order = 5)]
      [MaxLength(1), MinLength(1)]
      public string? Gender { get; set; }

      [Required]
      [Column("phone_number", Order = 6)]
      [MaxLength(20)]
      public string? PhoneNumber { get; set; }

      [Column("address", Order = 7)]
      [MaxLength(500)]
      public string? Address { get; set; }

      // relation

      [JsonIgnore]
      public ICollection<Chart>? Charts { get; set; }
   }
}
