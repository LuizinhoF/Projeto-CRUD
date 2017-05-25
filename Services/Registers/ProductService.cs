using Model.Registers;
using Persistence.DAL.Registers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Registers
{
    public class ProductService
    {
        private ProductsDAL productDAL = new ProductsDAL();
        public IQueryable GetByName()
        {
            return productDAL.GetOrderByName();
        }
        public Product GetByID(long id)
        {
            return productDAL.GetOrderById(id);
        }
        public IQueryable<Product> GetByCategory(long? id)
        {
            return productDAL.GetByCategory(id);
        }
        public void Save(Product product)
        {
            productDAL.SaveProduct(product);
        }
        public Product DeleteByID(long id)
        {
            return productDAL.DeleteByID(id);
        }
    }
}
