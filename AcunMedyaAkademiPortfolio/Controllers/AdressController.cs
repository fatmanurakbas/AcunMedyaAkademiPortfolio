using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AcunMedyaAkademiPortfolio.Models;

namespace AcunMedyaAkademiPortfolio.Controllers
{
    public class AdressController : Controller
    {
        DbPortfolioEntities db = new DbPortfolioEntities(); //Nesne Oluşturma
        private object p;

        public ActionResult Index()
        {
            var values = db.TblAdresses.ToList(); //tablonun içindeki tüm verileri listeleme sql deki select * from gibi
            return View(values);
        }
        [HttpGet] // sayfanın getirmesini sağlar
        public ActionResult CreateAdress()
        {
            return View();
        }
        [HttpPost] // butona bastığımız zaman yapacağımız işleri tutuyor.
        public ActionResult CreateAdress(TblAdress p)
        {
            db.TblAdresses.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteAdress(int id)
        {
            var value = db.TblAdresses.Find(id);
            db.TblAdresses.Remove(value);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult UpdateAdress(int id)
        {
            var value = db.TblAdresses.Find(id);
            return View(value);

        }
        [HttpPost]

        public ActionResult UpdateAdress(TblAdress p)
        {
            var value = db.TblAdresses.Find(p.AdressId);
            if (value == null)
            {
                return Content("Kayıt bulunamadı." + p.AdressId);
            }

            value.Adress = p.Adress;
            value.Phone = p.Phone;
            value.Email = p.Email;
            value.Website = p.Website;
            
            db.SaveChanges();

            return RedirectToAction("Index");
        }




    }
}