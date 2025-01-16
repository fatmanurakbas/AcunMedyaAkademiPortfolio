using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AcunMedyaAkademiPortfolio.Models;

namespace AcunMedyaAkademiPortfolio.Controllers
{
    public class ContactController : Controller
    {
        DbPortfolioEntities db = new DbPortfolioEntities(); //Nesne Oluşturma
        private object p;

        public ActionResult Index()
        {
            var values = db.TblContacts.ToList(); //tablonun içindeki tüm verileri listeleme sql deki select * from gibi
            return View(values);
        }
        [HttpGet] // sayfanın getirmesini sağlar
        public ActionResult CreateContact()
        {
            return View();
        }
        [HttpPost] // butona bastığımız zaman yapacağımız işleri tutuyor.
        public ActionResult CreateContact(TblContact p)
        {
            db.TblContacts.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteContact(int id)
        {
            var value = db.TblContacts.Find(id);
            db.TblContacts.Remove(value);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult UpdateContact(int id)
        {
            var value = db.TblContacts.Find(id);
            return View(value);

        }
        [HttpPost]

        public ActionResult UpdateContact(TblContact p)
        {
            var value = db.TblContacts.Find(p.ContectId);
            if (value == null)
            {
                return Content("Kayıt bulunamadı." + p.ContectId);
            }

            value.Name = p.Name;
            value.Email = p.Email;
            value.Subject = p.Subject;
            value.Description = p.Description;
            value.CreateDate = p.CreateDate;


            db.SaveChanges();

            return RedirectToAction("Index");
        }




    }
}