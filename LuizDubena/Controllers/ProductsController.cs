using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using Model.Registers;
using Services.Registers;
using Services.Tables;

namespace LuizDubena.Controllers
{
    public class ProductsController : Controller
    {

        private ProductService productService = new ProductService();
        private CategoryService categoryService = new CategoryService();
        private SupplierService supplierService = new SupplierService();
        #region [ Index ]
        public ActionResult Index()
        {
            return View(productService.GetByName());
        }
        #endregion

        #region [ Details ]
        public ActionResult Details(long? id)
        {
            return GetViewByID(id);
        }
        #endregion

        #region [ Create ]
        public ActionResult Create()
        {
            PopulateViewBag();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            return SaveProduct(product);
        }
        #endregion

        #region [ Edit ]
        public ActionResult Edit(long? id)
        {
            PopulateViewBag(productService.GetByID((long)id));
            return GetViewByID(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            return SaveProduct(product);
            //[Bind(Include = "ProductId,Name,CategoryID,SupplierId")]
        }
        #endregion

        #region [ Delete ]
        public ActionResult Delete(long? id)
        {
            return GetViewByID(id);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            try
            {
                Product product = productService.DeleteByID(id);
                TempData["Message"] = "Product	" + product.Name.ToUpper() + "	was removed";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion

        private ActionResult GetViewByID(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productService.GetByID((long)id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }        private void PopulateViewBag(Product product = null)
        {
            if (product == null)
            {
                ViewBag.CategoryID = new SelectList(categoryService.GetByName(),"CategoryID", "name");
                ViewBag.SupplierID = new SelectList(supplierService.GetByName(),"SupplierId", "Name");
            }
            else
            {
                ViewBag.CategoryID = new SelectList(categoryService.GetByName (), "CategoryID", "name", product.CategoryID);
                ViewBag.SupplierID = new SelectList(supplierService.GetByName(), "SupplierId", "Name", product.SupplierId);
            }
        }        private ActionResult SaveProduct(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    productService.Save(product);
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch
            {
                return View(product);
            }
        }
    }
}
