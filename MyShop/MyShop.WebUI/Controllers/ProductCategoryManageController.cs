using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductCategoryManageController : Controller
    {
        IRepository<ProductCategory> context;

        public ProductCategoryManageController(IRepository<ProductCategory> context)
        {
            this.context = context;
        }

        // GET: ProductManage
        public ActionResult Index()
        {
            List<ProductCategory> productCategories = context.Collection().ToList();
            return View(productCategories);
        }

        public ActionResult Create()
        {
            ProductCategory productCategory = new ProductCategory();
            return View(productCategory);
        }

        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(productCategory);
            }

            context.Insert(productCategory);
            context.Commit();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            ProductCategory productCategory = context.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }

            return View(productCategory);
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory, string id)
        {
            ProductCategory productCategoryToEdit = context.Find(id);

            if (productCategoryToEdit == null)
            {
                return HttpNotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(productCategory);
            }

            productCategoryToEdit.Category = productCategory.Category;

            context.Commit();
            return RedirectToAction("Index");
        }


        public ActionResult Delete(string id)
        {
            ProductCategory productCategory = context.Find(id);

            if (productCategory == null)
            {
                return HttpNotFound();
            }

            return View(productCategory);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {
            ProductCategory productCategory = context.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }

            context.Delete(id);
            context.Commit();
            return RedirectToAction("Index");
        }
    }
}