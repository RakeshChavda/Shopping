using eShoppingCart.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eShoppingCart.DAL;
using eShoppingCart.Models;
using Newtonsoft.Json;

namespace eShoppingCart.Controllers
{
    public class AdminController : Controller
    {
        public GenericUnitOfWork _UnitOfWork = new GenericUnitOfWork();

        #region Category Drop down for product
        public List<SelectListItem> GetCategory()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var category = _UnitOfWork.GetRepositoryInstance<CategoryM>().GetAllRecords();
            foreach(var item in category)
            {
                list.Add(new SelectListItem { Value = item.CategoryId.ToString(), Text = item.CategoryName });
            }
            return list;
        }
        #endregion


        // GET: Admin
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Categories()
        {
            List<CategoryM> AllCategoryList = _UnitOfWork.GetRepositoryInstance<CategoryM>().GetAllRecordsIQueryable().Where(x => x.IsDelete == false).ToList();
            return View(AllCategoryList);
        }

        public ActionResult AddCategory()
        {

          return UpdateCategory(0);            
        }

        public ActionResult UpdateCategory(int categoryId)
        {
            CategoryDetail cd;
            if (categoryId != null)
            {
                cd = JsonConvert.DeserializeObject<CategoryDetail>(JsonConvert.SerializeObject(_UnitOfWork.GetRepositoryInstance<CategoryM>().GetFirstOrDefault(categoryId)));
            }
            else
            {
                cd = new CategoryDetail();
            }
            return View("UpdateCategory", cd);
        }



        #region Add Categorty
        [HttpPost]
        public ActionResult UpdateCategory(CategoryM tbl)
        {
            tbl.IsDelete = false;
            tbl.IsActive = true;
            _UnitOfWork.GetRepositoryInstance<CategoryM>().Add(tbl);
            return RedirectToAction("Categories");
        }

        #endregion


        public ActionResult CetegoryEdit(int categoryId)
        {
            return View(_UnitOfWork.GetRepositoryInstance<CategoryM>().GetFirstOrDefault(categoryId));
        }

        [HttpPost]
        public ActionResult CetegoryEdit(CategoryM tbl)
        {            
            _UnitOfWork.GetRepositoryInstance<CategoryM>().Update(tbl);
            return RedirectToAction("Categories");
        }


        /*----------------------------product----------------------*/

        public ActionResult Product()
        {
            return View(_UnitOfWork.GetRepositoryInstance<ProductM>().GetProduct());
        }


        public ActionResult ProductEdit(int productId)
        {
            // pass category list to view 
            ViewBag.CategoryList = GetCategory();
            return View(_UnitOfWork.GetRepositoryInstance<ProductM>().GetFirstOrDefault(productId));
        }
        [HttpPost]
        public ActionResult ProductEdit(ProductM tbl, HttpPostedFileBase file)
        {
            string img = null;
            if (file != null)
            {
                img = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ProductImage/"), img);
                file.SaveAs(path);
            }
            tbl.ProductImage = file!=null ? img : tbl.ProductImage;
            tbl.ModifiedDate = DateTime.Now;
            _UnitOfWork.GetRepositoryInstance<ProductM>().Update(tbl);
            return RedirectToAction("Product"); 
        }

        public ActionResult ProductAdd()
        {
            // pass category list to view 
            ViewBag.CategoryList = GetCategory();
            return View();
        }

        [HttpPost]
        public ActionResult ProductAdd(ProductM tbl, HttpPostedFileBase file)
        {
            string img = null;
            if (file!= null)
            {
                img = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ProductImage/"),img);
                file.SaveAs(path);
            }
            tbl.ProductImage = img; 
            tbl.CreatedDate = DateTime.Now;
            _UnitOfWork.GetRepositoryInstance<ProductM>().Add(tbl);
            return RedirectToAction("Product");
        }

    }
}