using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductManageController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategory;

        public ProductManageController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            this.context = productContext;
            this.productCategory = productCategoryContext;
        }

        // GET: ProductManage
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            ProductManageViewModel viewModel = new ProductManageViewModel();
            viewModel.Product = new Product();
            viewModel.productCategories = productCategory.Collection();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            if (file != null)
            {
                product.Image = product.Id + Path.GetExtension(file.FileName);
                file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image);
            }

            context.Insert(product);
            context.Commit();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            Product product = context.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            ProductManageViewModel viewModel = new ProductManageViewModel();
            viewModel.Product = product;
            viewModel.productCategories = productCategory.Collection();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(Product product, string id, HttpPostedFileBase file)
        {
            Product productToEdit = context.Find(id);

            if (productToEdit == null)
            {
                return HttpNotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(product);
            }

            if (file != null)
            {
                productToEdit.Image = product.Id + Path.GetExtension(file.FileName);
                file.SaveAs(Server.MapPath("//Content//ProductImages//") + productToEdit.Image);
            }

            productToEdit.Category = product.Category;
            productToEdit.Description = product.Description;
            productToEdit.Name = product.Name;
            productToEdit.Price = product.Price;

            context.Commit();
            return RedirectToAction("Index");
        }


        public ActionResult Delete(string id)
        {
            Product product = context.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {
            Product product = context.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            context.Delete(id);
            context.Commit();
            return RedirectToAction("Index");
        }
    }
}