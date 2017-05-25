using Model.Registers;
using Persistence.Contexts;
using System.Data.Entity;
using System.Linq;

namespace Persistence.DAL.Registers
{
    public class SupplierDAL
    {
        private EFContext context = new EFContext();
        public IQueryable<Supplier>GetOrderbyName()
        {
            return context.Suppliers.OrderBy(b => b.Name);
        }

        public Supplier GetOrderById(long id)
        {
            return context.Suppliers.Where(s => s.SupplierID == id).Include("Products.Category").First() ;
        }
        public void SaveProduct(Supplier supplier)
        {
            if (supplier.SupplierID == null)
            {
                context.Suppliers.Add(supplier);
            }
            else
            {
                context.Entry(supplier).State = EntityState.Modified;
            }
            context.SaveChanges();
        }
        public Supplier DeleteByID(long id)
        {
            Supplier supplier = GetOrderById(id);
            context.Products.RemoveRange(context.Products).Where(m => m.ProductId == id);
            context.Suppliers.Remove(supplier);
            context.SaveChanges();
            return supplier;
        }
    }
}
