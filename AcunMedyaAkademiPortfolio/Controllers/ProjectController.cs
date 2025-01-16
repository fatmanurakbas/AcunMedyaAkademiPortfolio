using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AcunMedyaAkademiPortfolio.Models;

namespace AcunMedyaAkademiPortfolio.Controllers
{
    public class ProjectController : Controller
    {
        DbPortfolioEntities db = new DbPortfolioEntities(); // Nesne Oluşturma

        // Index Action - Tüm projeleri listeleme
        public ActionResult Index()
        {
            var values = db.TblProjects.ToList();
            return View(values);
        }

        // Create Project GET - Yeni proje oluşturma sayfasını gösterme
        [HttpGet]
        public ActionResult CreateProject()
        {
            return View();
        }

        // Create Project POST - Yeni proje oluşturma işlemi
        [HttpPost]
        public ActionResult CreateProject(TblProject p, HttpPostedFileBase ProjectImage)
        {
            if (ModelState.IsValid)
            {
                if (ProjectImage != null && ProjectImage.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(ProjectImage.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Images/Projects"), fileName);

                    // Klasörün varlığını kontrol et ve oluştur
                    if (!Directory.Exists(Server.MapPath("~/Images/Projects")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Images/Projects"));
                    }

                    ProjectImage.SaveAs(filePath);
                    p.ProjectImageUrl = "/Images/Projects/" + fileName;
                }

                db.TblProjects.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(p);
        }

        // Delete Project - Projeyi silme işlemi
        public ActionResult DeleteProject(int id)
        {
            var value = db.TblProjects.Find(id);
            if (value != null)
            {
                // Fotoğrafı sunucudan sil
                string filePath = Server.MapPath(value.ProjectImageUrl);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                db.TblProjects.Remove(value);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // Update Project GET - Proje güncelleme sayfasını gösterme
        [HttpGet]
        public ActionResult UpdateProject(int? id)
        {
            if (id == null || id <= 0)
            {
                return HttpNotFound("Geçersiz veya eksik ID.");
            }

            var value = db.TblProjects.Find(id.Value);
            if (value == null)
            {
                return HttpNotFound("Güncellenecek kayıt bulunamadı.");
            }

            return View(value);
        }

        // Update Project POST - Proje güncelleme işlemi
        [HttpPost]
        public ActionResult UpdateProject(TblProject p, HttpPostedFileBase ProjectImage)
        {
            if (ModelState.IsValid)
            {
                var value = db.TblProjects.Find(p.ProjectID);
                if (value != null)
                {
                    value.ProjectName = p.ProjectName;
                    value.ProjectCategory = p.ProjectCategory;

                    // Dosya yükleme işlemi
                    if (ProjectImage != null && ProjectImage.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(ProjectImage.FileName);
                        string filePath = Path.Combine(Server.MapPath("~/Images/Projects"), fileName);

                        // Mevcut resmi sunucudan silme
                        string existingFilePath = Server.MapPath(value.ProjectImageUrl);
                        if (System.IO.File.Exists(existingFilePath))
                        {
                            System.IO.File.Delete(existingFilePath);
                        }

                        ProjectImage.SaveAs(filePath);
                        value.ProjectImageUrl = "/Images/Projects/" + fileName;
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
