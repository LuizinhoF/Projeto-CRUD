using Model.Registers;
using Persistence.DAL.Registers;
using System.Linq;

namespace Services.Registers
{
    public class SupplierService
    {
        private SupplierDAL supplierDAL = new SupplierDAL();
        public IQueryable<Supplier> GetByName()
		{
			return	supplierDAL.GetOrderbyName();
		}

        public Supplier GetByID(long id)
        {
            return supplierDAL.GetOrderById(id);
        }
        public void Save(Supplier supplier)
        {
            supplierDAL.SaveProduct(supplier);
        }
        public Supplier DeleteByID(long id)
        {
            return supplierDAL.DeleteByID(id);
        }
    }
}
