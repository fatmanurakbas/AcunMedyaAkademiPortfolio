using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AcunMedyaAkademiPortfolio.Models;

namespace AcunMedyaAkademiPortfolio.Controllers
{
    public class TestimonialController : Controller
    {
        DbPortfolioEntities db = new DbPortfolioEntities(); //Nesne Oluşturma
    
        public ActionResult Index()
        {
            var values = db.TblTestimonials.ToList(); //tablonun içindeki tüm verileri listeleme sql deki select * from gibi

            return View(values);
        }
        [HttpGet]
        public ActionResult CreateTestimonial()
        {
 
            return View();
        }
        [HttpPost]
        public ActionResult CreateTestimonial(TblTestimonial p, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                // Define the path to save the file
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Images/Testimonial"), fileName);

                // Save the file to the server
                file.SaveAs(path);

                // Save the path to the model
                p.TestimonialImageUrl = "~/Images/Testimonial" + fileName;
            }

            db.TblTestimonials.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteTestimonial(int id)
        {
            var value = db.TblTestimonials.Find(id);
            if (value != null)
            {
                // Fotoğrafı sunucudan sil
                string filePath = Server.MapPath(value.TestimonialImageUrl);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                db.TblTestimonials.Remove(value);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateTestimonial(int? id)
        {
            if (id == null || id <= 0)
            {
                return HttpNotFound("Geçersiz veya eksik ID.");
            }

            var value = db.TblTestimonials.Find(id.Value);
            if (value == null)
            {
                return HttpNotFound("Güncellenecek kayıt bulunamadı.");
            }

            return View(value);
        }


        [HttpPost]
        public ActionResult UpdateTestimonial(TblTestimonial p, HttpPostedFileBase file)
        {
            if (p == null || p.TestimonialId <= 0)
            {
                return HttpNotFound("Gönderilen model geçersiz.");
            }

            var value = db.TblTestimonials.Find(p.TestimonialId);
            if (value == null)
            {
                return HttpNotFound("Güncellenecek kayıt bulunamadı.");
            }

            // Update the text fields
            value.TestimonialDescription = p.TestimonialDescription;
            value.TestimonialName = p.TestimonialName;
            value.TestimonialTitle = p.TestimonialTitle;
            value.Status = true;

            // Check if a new file is uploaded
            if (file != null && file.ContentLength > 0)
            {
                // Define the path to save the new file
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Images/Testimonial"), fileName);

                // Save the new file to the server
                file.SaveAs(path);

                // Update the model with the new file path
                value.TestimonialImageUrl = "~/Images/Testimonial" + fileName;
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }





        /* [HttpPost]
        public ActionResult UpdateTestimonial(TblTestimonial p)
        {


             var value = db.TblTestimonials.Find(p.TestimonialId);
             value.TestimonialDescription = p.TestimonialDescription;
             value.TestimonialImageUrl = p.TestimonialImageUrl;
             value.TestimonialName = p.TestimonialName;
             value.TestimonialTitle = p.TestimonialTitle;
             value.Status = true;
             db.SaveChanges();

             return RedirectToAction("Index");
         }*/



    }
}