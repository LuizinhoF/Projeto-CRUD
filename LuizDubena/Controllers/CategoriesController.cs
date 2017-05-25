using LuizDubena.Model;
using Model.Tables;
using Newtonsoft.Json;
using Services.Tables;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LuizDubena.Controllers
{
    public class CategoriesController : Controller
    {
        #region [ Properties ]

        private CategoryService categoryService = new CategoryService();

        #endregion

        #region [ Action ]

        #region [ Index ]
        public async Task<ActionResult> Index()
        {
            var apiModel = new CategoryListAPIModel();

            var resp = await GetFromAPI(null, response =>
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    apiModel = JsonConvert.DeserializeObject<CategoryListAPIModel>(result);
                }
            });
            return View(apiModel.Result);
        }

        #endregion

        #region [ CREATE ]

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(Category category)
        {
            return SaveCategory(category);
        }

        #endregion

        #region [ Edit ]

        public ActionResult Edit(long? id)
        {
            return ByID(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            return SaveCategory(category);
        }

        #endregion

        #region [ Delete ]

        public ActionResult Delete(long id)
        {
            return ByID(id);
        }

        [HttpPost, ActionName ("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            try
            {
                Category category = categoryService.DeleteByID(id);
                TempData["Message"] = "Category " + category.Name.ToUpper() + "	was removed";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        #endregion

        #region [ Details ]

        public async Task<ActionResult> Details(long? id)
        {
            return await GetViewByID(id);
        }

        #endregion

        #endregion

        #region [ Methods ]

        private async Task<HttpResponseMessage> GetFromAPI(long? id, Action<HttpResponseMessage> action)
        {
            using (var client = new HttpClient())
            {
                var baseUrl = string.Format("{0}://{1}", HttpContext.Request.Url.Scheme, HttpContext.Request.Url.Authority);
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();

                var url = "Api/Categories"; 
                if (id != null)
                    url = "Api/Categories/" + id;

                var request = await client.GetAsync(url);

               if (action != null)
                    action.Invoke(request);

                return request;
            }
        }

        private async Task<ActionResult> GetViewByID(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            CategoryAPIModel item = null;

            var resp = await GetFromAPI(id.Value, response =>
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    item = JsonConvert.DeserializeObject<CategoryAPIModel>(result);
                }
            });

            if (!resp.IsSuccessStatusCode)
                return new HttpStatusCodeResult(resp.StatusCode);

            if (item.Message == "!OK" || item.Result == null)
                return HttpNotFound();

            return View(item.Result);
        }        private ActionResult ByID(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryService.GetByID((long)id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }        private ActionResult SaveCategory(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    categoryService.Save(category);
                    return RedirectToAction("Index");
                }
                return View(category);
            }
            catch
            {
                return View(category);
            }
        }

        #endregion
    }

}