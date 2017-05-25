using Model.Tables;
using Services.Tables;
using System.Collections.Generic;
using System.Web.Http;

namespace LuizDubena.Controllers.API
{
    public class CategoriesController : ApiController
    {
        private CategoryService service = new CategoryService();

        // GET: api/Categories
        public IEnumerable<Category> Get()
        {
            return service.GetByName();
        }

        // GET: api/Categories/5
        public Category Get(int id)
        {
            return service.GetByID(id);
        }

        // POST: api/Categories
        public void Post([FromBody]Category value)
        {
            service.Save(value);
        }

        // PUT: api/Categories/5
        public void Put(int id, [FromBody]Category value)
        {
            service.Save(value);
        }

        // DELETE: api/Categories/5
        public void Delete(int id)
        {
            service.DeleteByID(id);
        }
    }
}
