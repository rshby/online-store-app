namespace online_store_app.Models.DTO
{
   [GraphQLName("user_response")]
   public class UserResponse
   {
      [GraphQLName("id")]
      public int? Id { get; set; }

      [GraphQLName("identity_number")]
      public string? IdentityNumber { get; set; }
   }
}
