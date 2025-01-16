using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AcunMedyaAkademiPortfolio.Models;

namespace AcunMedyaAkademiPortfolio.Controllers
{
    public class AboutController : Controller
    {
        DbPortfolioEntities db = new DbPortfolioEntities(); // Nesne oluşturma

        // Index Action - Tüm 'About' kayıtlarını listeleme
        public ActionResult Index()
        {
            var values = db.TblAbouts.ToList();
            foreach (var about in values)
            {
                Console.WriteLine($"Title: {about.Title}, Image URL: {about.ImageUrl}");
            }
            return View(values);          
        }

        // Create About GET - Yeni 'About' kaydı oluşturma sayfasını gösterme
        [HttpGet]
        public ActionResult CreateAbout()
        {
            return View();
        }

        // Create About POST - Yeni 'About' kaydı oluşturma işlemi
        [HttpPost]
        public ActionResult CreateAbout(TblAbout p, HttpPostedFileBase AboutImage)
        {
            if (ModelState.IsValid)
            {
                if (AboutImage != null && AboutImage.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(AboutImage.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Images/About"), fileName);

                    // Klasörün varlığını kontrol et ve oluştur
                    if (!Directory.Exists(Server.MapPath("~/Images/About")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Images/About"));
                    }

                    AboutImage.SaveAs(filePath);
                    p.ImageUrl = "/Images/About/" + fileName;
                }

                db.TblAbouts.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(p);
        }

        // Delete About - 'About' kaydını silme işlemi
        public ActionResult DeleteAbout(int id)
        {
            var value = db.TblAbouts.Find(id);
            if (value != null)
            {
                // Fotoğrafı sunucudan sil
                string filePath = Server.MapPath(value.ImageUrl);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                db.TblAbouts.Remove(value);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // Update About GET - 'About' kaydını güncelleme sayfasını gösterme
        [HttpGet]
        public ActionResult UpdateAbout(int? id)
        {
            if (id == null || id <= 0)
            {
                return HttpNotFound("Geçersiz veya eksik ID.");
            }

            var value = db.TblAbouts.Find(id.Value);
            if (value == null)
            {
                return HttpNotFound("Güncellenecek kayıt bulunamadı.");
            }

            return View(value);
        }

        // Update About POST - 'About' kaydını güncelleme işlemi
        [HttpPost]
        public ActionResult UpdateAbout(TblAbout p, HttpPostedFileBase AboutImage)
        {
            if (ModelState.IsValid)
            {
                var value = db.TblAbouts.Find(p.AbaoutId);
                if (value != null)
                {
                    value.Descrition = p.Descrition; // 'Descrition' yazımı düzeltildi
                    value.Title = p.Title;

                    // Dosya yükleme işlemi
                    if (AboutImage != null && AboutImage.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(AboutImage.FileName);
                        string filePath = Path.Combine(Server.MapPath("~/Images/About"), fileName);

                        // Mevcut resmi sunucudan silme
                        string existingFilePath = Server.MapPath(value.ImageUrl);
                        if (System.IO.File.Exists(existingFilePath))
                        {
                            System.IO.File.Delete(existingFilePath);
                        }

                        AboutImage.SaveAs(filePath);
                        value.ImageUrl = "/Images/About/" + fileName;
                    }

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return HttpNotFound("Güncellenecek kayıt bulunamadı.");
                }
            }
            return View(p);
        }
    }
}
