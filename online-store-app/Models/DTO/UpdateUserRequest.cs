using System.ComponentModel.DataAnnotations;

namespace online_store_app.Models.DTO
{
   [GraphQLName("update_user")]
   public class UpdateUserRequest
   {
      [Required]
      [GraphQLName("id")]
      [GraphQLNonNullType]
      public int? Id { get; set; }

      [Required]
      [GraphQLName("identity_number")]
      [GraphQLNonNullType]
      public string? IdentityNumber { get; set; }

      [Required]
      [GraphQLName("name")]
      [GraphQLNonNullType]
      public string? Name { get; set; }

      [Required]
      [GraphQLName("birth_date")]
      [GraphQLNonNullType]
      public string? BirthDate { get; set; }

      [GraphQLName("gender")]
      public string? Gender { get; set; }

      [Required]
      [GraphQLName("phone_number")]
      [GraphQLNonNullType]
      public string? PhoneNumber { get; set; }

      [GraphQLName("address")]
      public string? Address { get; set; }
   }
}
