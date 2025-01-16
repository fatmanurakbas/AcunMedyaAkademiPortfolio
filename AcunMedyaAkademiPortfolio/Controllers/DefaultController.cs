using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AcunMedyaAkademiPortfolio.Models;


namespace AcunMedyaAkademiPortfolio.Controllers
{
    public class DefaultController : Controller
    {
        DbPortfolioEntities db = new DbPortfolioEntities();
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult PartialHead()
        {
            return PartialView();
        }
        public PartialViewResult PartialNavbar()
            
        {
            return PartialView();
        }
        public PartialViewResult PartialFeature()
        {
            var values = db.TblFeatures.ToList(); //tablonun içindeki tüm verileri listeleme sql deki select * from gibi
            return PartialView(values);
        }
        public PartialViewResult PartialAbout()
        {
            var values = db.TblAbouts.ToList();
            return PartialView(values);
        }
        public PartialViewResult PartialSkill()
        {
            var values = db.TblSkills.ToList(); 
            return PartialView(values);
        }
        public PartialViewResult PartialStatistic()
        {
            var skillcount = db.TblSkills.ToList().Count();
            ViewBag.SkillCount = skillcount;
            var ProjectCount = db.TblProjects.Count();
            ViewBag.projectCount = ProjectCount;
            var skillAvgValue = db.TblSkills.Average(x => x.Value);
            ViewBag.SkillAvgValue = skillAvgValue;
            var mvcCategoryProjectCount = db.TblProjects.Where(x => x.ProjectCategory == 4).Count();
            ViewBag.MvcCategoryProjectCount = mvcCategoryProjectCount;
            var serviceCount = db.TblServices.Count();
            ViewBag.ServiceCount = serviceCount;
            var testimonialCount = db.TblTestimonials.Count();
            ViewBag.TestimonialCount = testimonialCount;
            var mobilCategoryProjectCount= db.TblProjects.Where(x => x.ProjectCategory == 2).Count();
            ViewBag.MobilCategoryProjectCount = mobilCategoryProjectCount;
            var webCategoryProjectCount = db.TblProjects.Where(x => x.ProjectCategory == 1).Count();
            ViewBag.WebCategoryProjectCount = webCategoryProjectCount;

            return PartialView();

        }
        public PartialViewResult PartialServices()
        {
            var values = db.TblServices.ToList();
            return PartialView(values);
        }
        public PartialViewResult PartialProject()
        {
            var values = db.TblProjects.ToList();
            return PartialView(values);
        }
        public PartialViewResult PartialProfile()
        {
            var values = db.TblProfiles.ToList();
            return PartialView(values);
        }
        public PartialViewResult PartialHobby()
        {
            var values = db.tblHobbies.ToList();
            return PartialView(values);
        }
        public PartialViewResult PartialTestimonial()
        {
            var values = db.TblTestimonials.ToList();
            return PartialView(values);
        }
        public ActionResult PartialContact()
        {
            return PartialView();
        }

        // POST: Contact
        [HttpPost]
        public ActionResult PartialContact(TblContact contact)
        {
            if (ModelState.IsValid)
            {
                db.TblContacts.Add(contact); // Yeni iletiyi veritabanına ekle
                db.SaveChanges(); // Değişiklikleri kaydet
                return RedirectToAction("ThankYou"); // Başka bir sayfaya yönlendirme
            }

            return PartialView("PartialContact", contact); // Hata durumunda tekrar formu göster
        }

        public ActionResult ThankYou()
        {
            return View(); // Başarılı gönderi için teşekkür sayfası
        }
        public PartialViewResult PartialAdress()
        {
            var values = db.TblAdresses.ToList();
            return PartialView(values);
        }
        public PartialViewResult PartialSocialMedia()
        {
            var values = db.SocialMediaLinks.ToList();
            return PartialView(values);
        }
        public PartialViewResult PartialScript()
        {
            return PartialView();
        }
        public PartialViewResult PartialFooter()
        {       
            return PartialView();
        }


    }
}