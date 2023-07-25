using Microsoft.EntityFrameworkCore;
using online_store_app.Data;
using online_store_app.Models.Entity;
using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace online_store_app.Repositories
{
   public class ProductRepository
   {
      // global variable
      private readonly OnlineStoreContext _db;

      // create constructor
      public ProductRepository(OnlineStoreContext db)
      {
         this._db = db;
      }

      // method get data all products
      public async Task<List<Product>?> GetAllProductsAsync([Required] TransactionScope tr)
      {
         try
         {
            // get all data
            List<Product>? products = await _db.Products.AsQueryable().ToListAsync();

            // jika data tidak ditemukan
            if (products == null || products.Count == 0)
            {
               return null;
            }

            // success get all data products
            return products;
         }
         catch (Exception err)
         {
            tr.Dispose();

            // send error message
            throw new GraphQLException(new ErrorBuilder().SetMessage(err.Message).Build());
         }
      }

      // method get products by Id
      public async Task<Product?> GetProductByIdAsync([Required] TransactionScope tr, int? id)
      {
         try
         {
            // get data products by id
            Product? product = await _db.Products.AsQueryable().Where(x => x.Id == id).FirstOrDefaultAsync();

            // jika data tidak ditemukan
            if (product == null)
            {
               return null;
            }

            // success get data product
            return product;
         }
         catch (Exception err)
         {
            tr.Dispose();

            // send error message
            throw new GraphQLException(new ErrorBuilder().SetMessage(err.Message).Build());
         }
      }

      // method insert/add new product
      public async Task<Product?> AddProductAsync([Required] TransactionScope tr, [Required] Product? newProduct)
      {
         try
         {
            await _db.Products.AddAsync(newProduct);

            await _db.SaveChangesAsync();

            return newProduct;
         }
         catch (Exception err)
         {
            tr.Dispose();

            // send error message
            throw new GraphQLException(new ErrorBuilder().SetMessage(err.Message).Build());
         }
      }

      // method update data Product
      public async Task<Product?> UpdateProductAsync([Required] TransactionScope tr, Product? oldProduct, Product? newpProduct)
      {
         try
         {
            _db.Products.Entry(oldProduct).CurrentValues.SetValues(newpProduct);

            await _db.SaveChangesAsync();
            return newpProduct;
         }
         catch (Exception err)
         {
            tr.Dispose();
            throw new GraphQLException(new ErrorBuilder().SetMessage(err.Message).Build());
         }
      }
   }
}
