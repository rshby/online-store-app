namespace online_store_app.Models.DTO
{
   [GraphQLName("product_response")]
   public class ProductResponse
   {
      [GraphQLName("id")]
      public int? Id { get; set; }

      [GraphQLName("brand")]
      public string? Brand { get; set; }

      [GraphQLName("name")]
      public string? Name { get; set; }

      [GraphQLName("price")]
      public int? Price { get; set; }

      [GraphQLName("stock")]
      public int? Stock { get; set; }
   }
}
