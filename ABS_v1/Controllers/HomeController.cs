using ABS_v1.Models;
using System;
using System.Collections.Generic;
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
        public ActionResult AddRegistration(FormData model)
        {
            FormData data = new FormData();
            data = model;
            data.RegDate = DateTime.Now;
            data.RegPlace = "Raipur";
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
            var data = db.FormData.Where(x=>x.Id == id).FirstOrDefault();
            model = data;
            return View(model);
        }
    }
}