using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AcunMedyaAkademiPortfolio.Models;

namespace AcunMedyaAkademiPortfolio.Controllers
{
    public class ProfileController : Controller
    {
        DbPortfolioEntities db = new DbPortfolioEntities(); //Nesne Oluşturma
        private object p;

        public ActionResult Index()
        {
            var values = db.TblProfiles.ToList(); //tablonun içindeki tüm verileri listeleme sql deki select * from gibi
            return View(values);
        }
        [HttpGet] // sayfanın getirmesini sağlar
        public ActionResult CreateProfile()
        {
            return View();
        }
        [HttpPost] // butona bastığımız zaman yapacağımız işleri tutuyor.
        public ActionResult CreateProfile(TblProfile p)
        {
            db.TblProfiles.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteProfile(int id)
        {
            var value = db.TblProfiles.Find(id);
            db.TblProfiles.Remove(value);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult UpdateProfile(int id)
        {
            var value = db.TblProfiles.Find(id);
            return View(value);

        }
        [HttpPost]

        public ActionResult UpdateProfile(TblProfile p)
        {
            var value = db.TblProfiles.Find(p.Profield);
            if (value == null)
            {
                return Content("Kayıt bulunamadı." + p.Profield);
            }

            value.Adrees = p.Adrees;
            value.Name = p.Name;
            value.Birthday = p.Birthday;
            value.Email = p.Email;
            value.Telefone = p.Telefone;

            db.SaveChanges();

            return RedirectToAction("Index");
        }




    }
}