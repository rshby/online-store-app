using Microsoft.EntityFrameworkCore;
using online_store_app.Models.Entity;

namespace online_store_app.Data
{
   public class OnlineStoreContext : DbContext
   {
      // create constructor
      public OnlineStoreContext(DbContextOptions<OnlineStoreContext> options) : base(options)
      {

      }

      // list a table
      public virtual DbSet<User> Users { get; set; }
      public virtual DbSet<Product> Products { get; set; }
      public virtual DbSet<Chart> Charts { get; set; }
   }
}
