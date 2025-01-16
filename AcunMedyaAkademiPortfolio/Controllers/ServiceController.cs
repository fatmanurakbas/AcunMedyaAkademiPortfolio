using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AcunMedyaAkademiPortfolio.Models;

namespace AcunMedyaAkademiPortfolio.Controllers
{
    public class ServiceController : Controller
    {
        DbPortfolioEntities db = new DbPortfolioEntities(); // Nesne Oluşturma

        // Index Action - Tüm projeleri listeleme
        public ActionResult Index()
        {
            var values = db.TblServices.ToList();
            return View(values);
        }

        // Create Project GET - Yeni proje oluşturma sayfasını gösterme
        [HttpGet]
        public ActionResult CreateService()
        {
            return View();
        }

        // Create Project POST - Yeni proje oluşturma işlemi
        [HttpPost]
        public ActionResult CreateService(TblService p, HttpPostedFileBase ServiceImage)
        {
            if (ModelState.IsValid)
            {
                if (ServiceImage != null && ServiceImage.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(ServiceImage.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Images/Service"), fileName);

                    // Klasörün varlığını kontrol et ve oluştur
                    if (!Directory.Exists(Server.MapPath("~/Images/Service")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Images/Service"));
                    }

                    ServiceImage.SaveAs(filePath);
                    p.IconUrl = "/Images/Service/" + fileName;
                }

                db.TblServices.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(p);
        }

        // Delete Project - Projeyi silme işlemi
        public ActionResult DeleteService(int id)
        {
            var value = db.TblServices.Find(id);
            if (value != null)
            {
                // Fotoğrafı sunucudan sil
                string filePath = Server.MapPath(value.IconUrl);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                db.TblServices.Remove(value);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // Update Project GET - Proje güncelleme sayfasını gösterme
        [HttpGet]
        public ActionResult UpdateService(int? id)
        {
            if (id == null || id <= 0)
            {
                return HttpNotFound("Geçersiz veya eksik ID.");
            }

            var value = db.TblServices.Find(id.Value);
            if (value == null)
            {
                return HttpNotFound("Güncellenecek kayıt bulunamadı.");
            }

            return View(value);
        }

        // Update Project POST - Proje güncelleme işlemi
        [HttpPost]
        public ActionResult UpdateService(TblService p, HttpPostedFileBase ServiceImage)
        {
            if (ModelState.IsValid)
            {
                var value = db.TblServices.Find(p.Serviceld);
                if (value != null)
                {
                    value.Title = p.Title;
                    value.Description = p.Description;

                    // Dosya yükleme işlemi
                    if (ServiceImage != null && ServiceImage.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(ServiceImage.FileName);
                        string filePath = Path.Combine(Server.MapPath("~/Images/Projects"), fileName);

                        // Mevcut resmi sunucudan silme
                        string existingFilePath = Server.MapPath(value.IconUrl);
                        if (System.IO.File.Exists(existingFilePath))
                        {
                            System.IO.File.Delete(existingFilePath);
                        }

                        ServiceImage.SaveAs(filePath);
                        value.IconUrl = "/Images/Service/" + fileName;
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
