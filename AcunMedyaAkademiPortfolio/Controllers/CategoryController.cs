using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AcunMedyaAkademiPortfolio.Models;
namespace AcunMedyaAkademiPortfolio.Controllers
{
    public class CategoryController : Controller
    {
        DbPortfolioEntities db = new DbPortfolioEntities(); //Nesne Oluşturma

        public ActionResult Index()
        {
            var values = db.TblCategories.ToList(); //tablonun içindeki tüm verileri listeleme sql deki select * from gibi

            return View(values);
        }
        [HttpGet]
        public ActionResult CreateCategory()
        {

            return View();
        }
        [HttpPost]
        public ActionResult CreateCategory(TblCategory p)
        {
            db.TblCategories.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteCategory(int id)
        {
            var value = db.TblCategories.Find(id);
            db.TblCategories.Remove(value);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult UpdateCategory(int? id)
        {
            if (id == null || id <= 0)
            {
                return HttpNotFound("Geçersiz veya eksik ID.");
            }

            var value = db.TblCategories.Find(id.Value);
            if (value == null)
            {
                return HttpNotFound("Güncellenecek kayıt bulunamadı.");
            }

            return View(value);
        }


        [HttpPost]
        public ActionResult UpdateCategory(TblCategory p)
        {
            if (p == null || p.CategoryId <= 0)
            {
                return HttpNotFound("Gönderilen model geçersiz.");
            }

            var value = db.TblCategories.Find(p.CategoryId);
            if (value == null)
            {
                return HttpNotFound("Güncellenecek kayıt bulunamadı.");
            }

            // Eğer kayıt bulunduysa, değerleri güncelle
            
            value.CategoryName = p.CategoryName;
            

            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}