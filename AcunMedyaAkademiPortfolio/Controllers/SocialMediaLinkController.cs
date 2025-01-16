using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AcunMedyaAkademiPortfolio.Models;

namespace AcunMedyaAkademiPortfolio.Controllers
{
    public class SocialMediaLinkController : Controller
    {
        DbPortfolioEntities db = new DbPortfolioEntities(); //Nesne Oluşturma
        private object p;

        public ActionResult Index()
        {
            var values = db.SocialMediaLinks.ToList(); //tablonun içindeki tüm verileri listeleme sql deki select * from gibi
            return View(values);
        }
        [HttpGet] // sayfanın getirmesini sağlar
        public ActionResult CreateSocialMedia()
        {
            return View();
        }
        [HttpPost] // butona bastığımız zaman yapacağımız işleri tutuyor.
        public ActionResult CreateSocialMedia(SocialMediaLink p)
        {
            db.SocialMediaLinks.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteSocialMedia(int id)
        {
            var value = db.SocialMediaLinks.Find(id);
            db.SocialMediaLinks.Remove(value);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult UpdateSocialMedia(int id)
        {
            var value = db.SocialMediaLinks.Find(id);
            return View(value);

        }
        [HttpPost]

        public ActionResult UpdateSocialMedia(SocialMediaLink p)
        {
            var value = db.SocialMediaLinks.Find(p.SocialMediaLinkId);
            if (value == null)
            {
                return Content("Kayıt bulunamadı." + p.SocialMediaLinkId);
            }

            
            value.PlatformName = p.PlatformName;
            value.Url = p.Url;

            db.SaveChanges();

            return RedirectToAction("Index");
        }




    }
}