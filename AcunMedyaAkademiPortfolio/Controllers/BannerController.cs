using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AcunMedyaAkademiPortfolio.Models;

namespace AcunMedyaAkademiPortfolio.Controllers
{
    public class BannerController : Controller
    {
        DbPortfolioEntities db = new DbPortfolioEntities(); // Nesne Oluşturma

        // Index Action - Tüm projeleri listeleme
        public ActionResult Index()
        {
            var values = db.TblBanners.ToList();
            return View(values);
        }

        // Create Project GET - Yeni proje oluşturma sayfasını gösterme
        [HttpGet]
        public ActionResult CreateBanner()
        {
            return View();
        }

        // Create Project POST - Yeni proje oluşturma işlemi
        [HttpPost]
        public ActionResult CreateBanner(TblBanner p, HttpPostedFileBase BannerImage)
        {
            if (ModelState.IsValid)
            {
                if (BannerImage != null && BannerImage.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(BannerImage.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Images/Banner"), fileName);

                    // Klasörün varlığını kontrol et ve oluştur
                    if (!Directory.Exists(Server.MapPath("~/Images/Banner")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Images/Banner"));
                    }

                    BannerImage.SaveAs(filePath);
                    p.ImageUrl = "/Images/Banner/" + fileName;
                }

                db.TblBanners.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(p);
        }

        // Delete Project - Projeyi silme işlemi
        public ActionResult DeleteBanner(int id)
        {
            var value = db.TblBanners.Find(id);
            if (value != null)
            {
                // Fotoğrafı sunucudan sil
                string filePath = Server.MapPath(value.ImageUrl);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                db.TblBanners.Remove(value);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // Update Project GET - Proje güncelleme sayfasını gösterme
        [HttpGet]
        public ActionResult UpdateBanner(int? id)
        {
            if (id == null || id <= 0)
            {
                return HttpNotFound("Geçersiz veya eksik ID.");
            }

            var value = db.TblBanners.Find(id.Value);
            if (value == null)
            {
                return HttpNotFound("Güncellenecek kayıt bulunamadı.");
            }

            return View(value);
        }

        // Update Project POST - Proje güncelleme işlemi
        [HttpPost]
        public ActionResult UpdateBanner(TblBanner p, HttpPostedFileBase BannerImage)
        {
            if (ModelState.IsValid)
            {
                var value = db.TblBanners.Find(p.BannerId);
                if (value != null)
                {
                    value.Title = p.Title;
                    value.Description = p.Description;

                    // Dosya yükleme işlemi
                    if (BannerImage != null && BannerImage.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(BannerImage.FileName);
                        string filePath = Path.Combine(Server.MapPath("~/Images/Banner"), fileName);

                        // Mevcut resmi sunucudan silme
                        string existingFilePath = Server.MapPath(value.ImageUrl);
                        if (System.IO.File.Exists(existingFilePath))
                        {
                            System.IO.File.Delete(existingFilePath);
                        }

                        BannerImage.SaveAs(filePath);
                        value.ImageUrl = "/Images/Banner/" + fileName;
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
