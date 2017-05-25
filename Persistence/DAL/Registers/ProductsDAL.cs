using Model.Registers;
using Persistence.Contexts;
using System.Data.Entity;
using System.Linq;

namespace Persistence.DAL.Registers
{
    public class ProductsDAL
    {
        private EFContext context = new EFContext();

        public IQueryable GetOrderByName()
        {
            return context.Products.Include(c => c.Category).Include(p => p.Supplier).OrderBy(n => n.Name);
        }
        public Product GetOrderById(long id)
        {
            return context.Products.Where(p => p.ProductId == id).Include(c => c.Category).Include(p => p.Supplier).First();
        }
        public void SaveProduct(Product product)
        {
            if (product.ProductId == null)
            {
                context.Products.Add(product);
            }
            else
            {
                context.Entry(product).State = EntityState.Modified;
            }
            context.SaveChanges();
        }
        public Product DeleteByID(long id)
        {
            Product product = GetOrderById(id);
            context.Products.Remove(product);
            context.SaveChanges();
            return product;
        }

        public IQueryable<Product> GetByCategory(long? categoryId)
        {
            return context.Products.Where(p => p.CategoryID.HasValue && p.CategoryID.Value == categoryId);
        }
    }
}
