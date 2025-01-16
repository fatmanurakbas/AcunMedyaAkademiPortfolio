using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AcunMedyaAkademiPortfolio.Models;
namespace AcunMedyaAkademiPortfolio.Controllers
{
    public class StaticticController : Controller
    {
        DbPortfolioEntities db = new DbPortfolioEntities();

        
        public ActionResult Index()
        {
            ViewBag.categoryCount = db.TblCategories.Count();
            ViewBag.ProjectCount = db.TblProjects.Count();
            ViewBag.skillCount = db.TblSkills.Count();
            ViewBag.skillAvgValue = db.TblSkills.Average(x => x.Value); //linq sorgular
           //ViewBag.lastSkillTitleName = db.GetLastSkillTitle().FirsOrDefault(); //tek bir veri istenildiğinde FirtOrDefault çağırılır.
            ViewBag.mvcCategoryProjectCount = db.TblProjects.Where(x => x.ProjectCategory == 3).Count();
            ViewBag.serviceCount = db.TblServices.Count();
            ViewBag.testimonialCount = db.TblTestimonials.Count();
            ViewBag.hobbyCount = db.tblHobbies.Count();
            ViewBag.mobilCategoryProjectCount = db.TblProjects.Where(x => x.ProjectCategory == 2).Count();
            ViewBag.webCategoryProjectCount = db.TblProjects.Where(x => x.ProjectCategory == 1).Count();

            return View();
        }
    }
}