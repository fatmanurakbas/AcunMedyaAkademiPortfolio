using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AcunMedyaAkademiPortfolio.Models;

namespace AcunMedyaAkademiPortfolio.Controllers
{
    public class FeatureController : Controller
    {
        DbPortfolioEntities db = new DbPortfolioEntities(); // Nesne Oluşturma

        // Index Action - Tüm projeleri listeleme
        public ActionResult Index()
        {
            var values = db.TblFeatures.ToList();
            return View(values);
        }

        // Create Project GET - Yeni proje oluşturma sayfasını gösterme
        [HttpGet]
        public ActionResult CreateFeature()
        {
            return View();
        }

        // Create Project POST - Yeni proje oluşturma işlemi
        [HttpPost]
        public ActionResult CreateFeature(TblFeature p, HttpPostedFileBase FeatureImage)
        {
            if (ModelState.IsValid)
            {
                if (FeatureImage != null && FeatureImage.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(FeatureImage.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Images/Feature"), fileName);

                    // Klasörün varlığını kontrol et ve oluştur
                    if (!Directory.Exists(Server.MapPath("~/Images/Feature")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Images/Feature"));
                    }

                    FeatureImage.SaveAs(filePath);
                    p.FeatureImager = "/Images/Feature/" + fileName;
                }

                db.TblFeatures.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(p);
        }

        // Delete Project - Projeyi silme işlemi
        public ActionResult DeleteFeature(int id)
        {
            var value = db.TblFeatures.Find(id);
            if (value != null)
            {
                // Fotoğrafı sunucudan sil
                string filePath = Server.MapPath(value.FeatureImager);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                db.TblFeatures.Remove(value);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // Update Project GET - Proje güncelleme sayfasını gösterme
        [HttpGet]
        public ActionResult UpdateFeature(int? id)
        {
            if (id == null || id <= 0)
            {
                return HttpNotFound("Geçersiz veya eksik ID.");
            }

            var value = db.TblFeatures.Find(id.Value);
            if (value == null)
            {
                return HttpNotFound("Güncellenecek kayıt bulunamadı.");
            }

            return View(value);
        }

        // Update Project POST - Proje güncelleme işlemi
        [HttpPost]
        public ActionResult UpdateFeature(TblFeature p, HttpPostedFileBase FeatureImage)
        {
            if (ModelState.IsValid)
            {
                var value = db.TblFeatures.Find(p.FeatureId);
                if (value != null)
                {
                    value.FeatureTitle = p.FeatureTitle;
                    value.FeatureSubtitle = p.FeatureSubtitle;

                    // Dosya yükleme işlemi
                    if (FeatureImage != null && FeatureImage.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(FeatureImage.FileName);
                        string filePath = Path.Combine(Server.MapPath("~/Images/Feature"), fileName);

                        // Mevcut resmi sunucudan silme
                        string existingFilePath = Server.MapPath(value.FeatureImager);
                        if (System.IO.File.Exists(existingFilePath))
                        {
                            System.IO.File.Delete(existingFilePath);
                        }

                        FeatureImage.SaveAs(filePath);
                        value.FeatureImager = "/Images/Feature/" + fileName;
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
