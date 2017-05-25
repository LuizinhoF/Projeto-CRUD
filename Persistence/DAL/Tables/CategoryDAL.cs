
using Model.Tables;
using Persistence.Contexts;
using System.Data.Entity;
using System.Linq;

namespace Persistence.DAL.Tables
{
    public class CategoryDAL
    {
        private EFContext context = new EFContext();
        public IQueryable<Category>GetOrderbyName()
        {
            return context.Categories.OrderBy(c => c.Name);
        }
        public Category GetOrderById(long id)
        {
            return context.Categories.Where(c => c.CategoryID == id).First(); //.Include("Products.Supplier").First();
        }
        public void SaveProduct(Category category)
        {
            if (category.CategoryID == null)
            {
                context.Categories.Add(category);
            }
            else
            {
                context.Entry(category).State = EntityState.Modified;
            }
            context.SaveChanges();
        }
        public Category DeleteByID(long id)
        {
            Category category = GetOrderById(id);
            context.Products.RemoveRange(context.Products).Where(m => m.CategoryID == id);
            context.Categories.Remove(category);
            context.SaveChanges();
            return category;
        }
    }
}
