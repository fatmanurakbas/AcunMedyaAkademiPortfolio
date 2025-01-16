using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AcunMedyaAkademiPortfolio.Models;

namespace AcunMedyaAkademiPortfolio.Controllers
{
    public class HobbyController : Controller
    {
        DbPortfolioEntities db = new DbPortfolioEntities(); // Nesne Oluşturma

        // Index Action
        public ActionResult Index()
        {
            var hobbies = db.tblHobbies.ToList();
            Console.WriteLine(hobbies.Count);  // Sayıyı kontrol et
            return View(hobbies);
        }


        // Create Hobby GET
        [HttpGet]
        public ActionResult CreateHobby()
        {
            return View();
        }

        // Create Hobby POST
        // Create Hobby POST
        [HttpPost]
        public ActionResult CreateHobby(tblHobby p, HttpPostedFileBase IconUrl)
        {
            if (ModelState.IsValid) // Model doğrulama
            {
                if (IconUrl != null && IconUrl.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(IconUrl.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/images/Hobbys/"), fileName);

                    // Klasörün varlığını kontrol et ve oluştur
                    if (!Directory.Exists(Server.MapPath("~/images/Hobbys/")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/images/Hobbys/"));
                    }

                    IconUrl.SaveAs(filePath);
                    p.IconUrl = "/images/Hobbys/" + fileName; // İkon URL'sini kaydet
                }

                db.tblHobbies.Add(p); // Yeni hobby'yi ekle
                db.SaveChanges(); // Değişiklikleri kaydet
                return RedirectToAction("Index"); // Index sayfasına yönlendir
            }
            return View(p); // ModelState geçerli değilse formu tekrar göster
        }

        // Delete Hobby
        public ActionResult DeleteHobby(int id)
        {
            var value = db.tblHobbies.Find(id);
            db.tblHobbies.Remove(value);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Update Hobby GET
        [HttpGet]
        public ActionResult UpdateHobby(int? id)
        {
            if (id == null || id <= 0)
            {
                return HttpNotFound("Geçersiz veya eksik ID.");
            }

            var value = db.tblHobbies.Find(id.Value);
            if (value == null)
            {
                return HttpNotFound("Güncellenecek kayıt bulunamadı.");
            }

            return View(value);
        }

        // Update Hobby POST
       
        [HttpPost]
        public ActionResult UpdateHobby(tblHobby p, HttpPostedFileBase Icon)
        {
            if (ModelState.IsValid)
            {
                var value = db.tblHobbies.Find(p.HobbyId);
                if (value != null)
                {
                    value.Title = p.Title;
                    

                    // Dosya yükleme işlemi
                    if (Icon != null && Icon.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(Icon.FileName);
                        string filePath = Path.Combine(Server.MapPath("~/images/Hobbys"), fileName);
                        Icon.SaveAs(filePath);

                        // URL'yi veritabanına kaydet
                        value.IconUrl = "/images/Hobbys/" + fileName;
                    }

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return HttpNotFound("Güncellenecek kayıt bulunamadı.");
                }
            }
            return View(p); // ModelState geçerli değilse formu tekrar göster
        }
    }
}
