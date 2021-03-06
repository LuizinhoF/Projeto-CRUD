﻿using LuizDubena.Model;
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
            var apiModel = new CategoryListModel();

            var resp = await GetFromAPI(null, response =>
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    apiModel = JsonConvert.DeserializeObject<CategoryListModel>(result);
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
          
        public async Task<ActionResult> Create(Category category)
        {
            var apiModel = new CategoryAPIModel();

            var resp = await PostFromAPI( null, response =>
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    apiModel = JsonConvert.DeserializeObject<CategoryAPIModel>(result);
                }
            }, category);

            return RedirectToAction("Index");
        }
        /*
        public ActionResult Create(Category category)
        {
            return SaveCategory(category);
        }
        */
        #endregion

        #region [ Edit ]

        public async Task<ActionResult> Edit(long? id)
        {
            return await GetViewByID(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Category category)
        {
            var apiModel = new CategoryAPIModel();

            var resp = await PostFromAPI(5, response =>
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    apiModel = JsonConvert.DeserializeObject<CategoryAPIModel>(result);
                }
            }, category);

            return RedirectToAction("Index");
        }

        #endregion

        #region [ Delete ]

        public async Task<ActionResult> Delete(long? id)
        {
            return await GetViewByID(id);
        }

        [HttpPost, ActionName ("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            try
            {
                var apiModel = new CategoryAPIModel();

                var resp = await DeleteFromAPI(id, response =>
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        apiModel = JsonConvert.DeserializeObject<CategoryAPIModel>(result);
                    }
                });

             // TempData["Message"] = "Category " + category.Name.ToUpper() + "	was removed";
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

        private async Task<HttpResponseMessage>PostFromAPI(long? id,Action<HttpResponseMessage> action, Category category)
        {
            using (var client = new HttpClient())
            {
                var baseUrl = string.Format("{0}://{1}", HttpContext.Request.Url.Scheme, HttpContext.Request.Url.Authority);
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();

                var url = "Api/Categories";

                if (id != null)
                    url = "Api/Categories/" + id;

                var request = await client.PostAsJsonAsync(url, category);
                await client.DeleteAsync(url);
                if (action != null)
                    action.Invoke(request);

                return request;
            }
        }

        private async Task<HttpResponseMessage>DeleteFromAPI(long id, Action<HttpResponseMessage> action)
        {
            using (var client = new HttpClient())
            {
                var baseUrl = string.Format("{0}://{1}", HttpContext.Request.Url.Scheme, HttpContext.Request.Url.Authority);
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
               var url = "Api/Categories/" + id;
                var request = await client.DeleteAsync(url);

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
        }      private ActionResult SaveCategory(Category category)
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