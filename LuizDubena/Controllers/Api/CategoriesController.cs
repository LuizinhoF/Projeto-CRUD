﻿using LuizDubena.Model;
using Model.Tables;
using Services.Registers;
using Services.Tables;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace LuizDubena.Controllers.API
{
    public class CategoriesController : ApiController
    {
        private CategoryService service = new CategoryService();
        private ProductService productService = new ProductService();

        // GET: api/Categories
        public CategoryListModel Get()
        {
            var apiModel = new CategoryListModel();

            try
            {
                apiModel.Result = service.Get().ToList();
            }
            catch (System.Exception)
            {
                apiModel.Message = "!OK";
            }

            return apiModel;
        }

        // GET: api/Categories/5
        public CategoryAPIModel Get(long? id)
        {
            var apiModel = new CategoryAPIModel();

            try
            {
                if (id == null)
                {
                    apiModel.Message = "!OK";
                    return apiModel;
                }
                else
                {
                    apiModel.Result = service.GetByID(id.Value);
                    if (apiModel.Result != null)
                        apiModel.Result.Products = productService.GetByCategory(id.Value).ToList();
                }
            }
            catch (System.Exception)
            {
                apiModel.Message = "!OK";
            }

            return apiModel;
        }

        // POST: api/Categories
        public void Post([FromBody]Category value)
        {
            service.Save(value);
        }

        // PUT: api/Categories/5
        public void Put(long id, [FromBody]Category value)
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
