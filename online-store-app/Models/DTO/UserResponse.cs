using online_store_app.Models.Entity;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace online_store_app.Models.DTO
{
   [GraphQLName("user_response")]
   public class UserResponse
   {
      [GraphQLName("id")]
      public int? Id { get; set; }

      [GraphQLName("identity_number")]
      public string? IdentityNumber { get; set; }

      [GraphQLName("name")]
      public string? Name { get; set; }

      [GraphQLName("birth_date")]
      public string? BirthDate { get; set; }

      [GraphQLName("gender")]
      [StringLength(1)]
      public string? Gender { get; set; }

      [GraphQLName("phone_number")]
      public string? PhoneNumber { get; set; }

      [GraphQLName("address")]
      public string? Address { get; set; }

      [GraphQLName("charts")]
      public List<Chart>? Charts { get; set; }
   }
}
