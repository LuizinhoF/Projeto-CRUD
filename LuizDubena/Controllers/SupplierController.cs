using Model.Registers;
using Services.Registers;
using System.Net;
using System.Web.Mvc;

namespace LuizDubena.Controllers
{
    public class SupplierController : Controller
    {
        private SupplierService supplierService = new SupplierService();

        #region [ Actions ]

        #region [ Index ]
        public ActionResult Index()
        {
            return View(supplierService.GetByName());
        }

        #endregion

        #region [ Create ]

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Supplier supplier)
        {
            return SaveSupplier(supplier);
        }

        #endregion

        #region [ Edit ]

        public ActionResult Edit(long? id)
        {
            return GetViewByID(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Supplier supplier)
        {
            return SaveSupplier(supplier);
        }

        #endregion

        #region [ Details ]

        public ActionResult Details(long? id)
        {
            return GetViewByID(id);        
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
                Supplier supplier = supplierService.DeleteByID(id);
                TempData["Message"] = "Supplier " + supplier.Name.ToUpper() + "	was removed";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #endregion

        private ActionResult GetViewByID(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = supplierService.GetByID((long)id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }        private ActionResult SaveSupplier(Supplier supplier)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    supplierService.Save(supplier);
                    return RedirectToAction("Index");
                }
                return View(supplier);
            }
            catch
            {
                return View(supplier);
            }
        }
    }
}