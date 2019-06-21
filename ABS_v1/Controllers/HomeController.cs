using ABS_v1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABS_v1.Controllers
{
    public class HomeController : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();

        }

        public ActionResult Gallery()
        {
            ViewBag.Message = "Your contact page.";

            return View();

        }


        public ActionResult Events()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Registration(FormData model)
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult AddRegistration(FormData model, HttpPostedFileBase file)
        {
            FormData data = new FormData();
            data = model;
            data.RegDate = DateTime.Now;
            data.RegPlace = "Raipur";

            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                      Server.MapPath("~/image/registration"),pic);

               // path = path + "\\" + data.FullName.Trim() + Path.GetExtension(path);
                //// Get the name of the file to upload.
                //string fileName = file.FileName;

                //// Create the path and file name to check for duplicates.
                //string pathToCheck = path + fileName;

                //// Create a temporary file name to use for checking duplicates.
                //string tempfileName = data.FullName;

                //if (System.IO.File.Exists(pathToCheck))
                //{
                //    int counter = 2;
                //    while (System.IO.File.Exists(pathToCheck))
                //    {
                //        // if a file with this name already exists,
                //        // prefix the filename with a number.
                //        tempfileName = counter.ToString() + fileName;
                //        pathToCheck = path + tempfileName;
                //        counter++;
                //    }

                //    fileName = tempfileName;
                //}
               
                // file is uploaded
                file.SaveAs(path);

                // save the image path path to the database or you can send image 
                // directly to database
                // in-case if you want to store byte[] ie. for DB
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }

            }
            data.ImageName = file.FileName;
            db.FormData.Add(data);

            db.SaveChanges();
            return View("Registration");
        }

        public ActionResult DataList()
        {
            var model = new FormData();
            var data = db.FormData.ToList();
            List<FormData> _lstData = new List<FormData>();
            _lstData = data;
            model.DataList = _lstData;
            return View(model);
        }

        public ActionResult FormDetails(int id)
        {
            var model = new FormData();
            var data = db.FormData.Where(x => x.Id == id).FirstOrDefault();
            model = data;
            return View(model);
        }

        public ActionResult Members()
        {

            return View();
        }


    }
}