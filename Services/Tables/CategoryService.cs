using Model.Registers;
using Model.Tables;
using Persistence.DAL.Registers;
using Persistence.DAL.Tables;
using System.Linq;

namespace Services.Tables
{
    public class CategoryService
    {
        private CategoryDAL categoryDAL = new CategoryDAL();
        public IQueryable<Category>GetByName()
        {
            return categoryDAL.GetOrderbyName();
        }
        public Category GetByID(long? id)
        {
            return categoryDAL.GetOrderById(id);
        }

        public IQueryable<Category> Get(long? id)
        {
            return categoryDAL.Get();
        }

        public void Save(Category category)
        {
            categoryDAL.SaveProduct(category);
        }
        public Category DeleteByID(long id)
        {
            return categoryDAL.DeleteByID(id);
        }
    }
}
