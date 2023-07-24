using System.ComponentModel.DataAnnotations;

namespace online_store_app.Models.DTO
{
   [GraphQLName("add_user")]
   public class AddUserRequest
   {
      [Required]
      [GraphQLName("identity_number")]
      [GraphQLNonNullType]
      [StringLength(16, MinimumLength = 16)]
      public string? IdentityNumber { get; set; }

      [Required]
      [GraphQLName("name")]
      [GraphQLNonNullType]
      [StringLength(255)]
      public string? Name { get; set; }

      [Required]
      [GraphQLName("birth_date")]
      [GraphQLNonNullType]
      public string? BirthDate { get; set; }

      [GraphQLName("gender")]
      [StringLength(1)]
      public string? Gender { get; set; }

      [Required]
      [GraphQLName("phone_number")]
      [GraphQLNonNullType]
      [StringLength(20)]
      public string? PhoneNumber { get; set; }

      [GraphQLName("address")]
      [StringLength(500)]
      public string? Address { get; set; }
   }
}
