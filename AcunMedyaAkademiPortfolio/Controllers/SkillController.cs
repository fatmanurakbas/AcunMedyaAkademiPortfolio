using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AcunMedyaAkademiPortfolio.Models;

namespace AcunMedyaAkademiPortfolio.Controllers
{
    public class SkillController : Controller
    {
        DbPortfolioEntities db = new DbPortfolioEntities(); //Nesne Oluşturma
        private object p;
        
        public ActionResult SkillList()
        {
            var values =db.TblSkills.ToList(); //tablonun içindeki tüm verileri listeleme sql deki select * from gibi
            return View(values);
        }
        [HttpGet] // sayfanın getirmesini sağlar
        public ActionResult CreateSkill()
        {
            return View(); 
        }
        [HttpPost] // butona bastığımız zaman yapacağımız işleri tutuyor.
        public ActionResult CreateSkill(TblSkill p)
        {
            db.TblSkills.Add(p);
            db.SaveChanges();
            return RedirectToAction("SkillList");
        }
        public ActionResult DeleteSkill(int id)
        {
            var value = db.TblSkills.Find(id);
            db.TblSkills.Remove(value);
            db.SaveChanges();
            return RedirectToAction("SkillList");
        }
       

        [HttpGet]
        public ActionResult UpdateSkill(int id)
        {
            var value = db.TblSkills.Find(id);
            return View(value);

        }
        [HttpPost]

        public ActionResult UpdateSkill(TblSkill p)
        {
            var value = db.TblSkills.Find(p.Skilld);
            if (value == null)
            {
                return Content("Kayıt bulunamadı. SkillId=" + p.Skilld);
            }

            value.Title = p.Title;
            value.Value = p.Value;
            value.LastWeekValue = p.LastWeekValue;
            value.LastMonthValue = p.LastMonthValue;
            db.SaveChanges();

            return RedirectToAction("SkillList");
        }


        

    }
}