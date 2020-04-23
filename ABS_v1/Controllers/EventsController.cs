using ABS_v1.Models;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ABS_v1.Controllers
{
    public class EventsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Events
        public ActionResult Index()
        {
            return View(db.Events.ToList());
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventModel eventModel = db.Events.Find(id);
            if (eventModel == null)
            {
                return HttpNotFound();
            }
            return View(eventModel);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventId,EventDate,EventName,EventDescription,EventLocation,Image")] EventModel eventModel, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var originalFilename = Path.GetFileName(file.FileName);
                var ext = Path.GetExtension(file.FileName);

                var newFileName = eventModel.EventId.ToString() + ext;
                var path = Path.Combine(Server.MapPath(@"~/Uploads/Events/"), eventModel.EventId.ToString() + ext);
                file.SaveAs(path);
                eventModel.Image = newFileName;


                db.Events.Add(eventModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eventModel);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventModel eventModel = db.Events.Find(id);
            if (eventModel == null)
            {
                return HttpNotFound();
            }
            return View(eventModel);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventId,EventDate,EventName,EventDescription,EventLocation,Image")] EventModel eventModel, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var originalFilename = Path.GetFileName(file.FileName);
                    var ext = Path.GetExtension(file.FileName);
                    var newFileName = eventModel.EventId.ToString() + ext;
                    var path = Path.Combine(Server.MapPath(@"~/Uploads/Events/"), eventModel.EventId.ToString() + ext);
                    file.SaveAs(path);
                    eventModel.Image = newFileName;


                }
                db.Entry(eventModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eventModel);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventModel eventModel = db.Events.Find(id);
            if (eventModel == null)
            {
                return HttpNotFound();
            }
            return View(eventModel);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EventModel eventModel = db.Events.Find(id);
            db.Events.Remove(eventModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
