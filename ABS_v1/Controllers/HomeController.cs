using ABS_v1.Models;
using Instamojo.NET.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace ABS_v1.Controllers
{
    public class HomeController : Controller
    {
        //System.Net.Mime.MediaTypeNames.Application.EnableVisualStyles();
        //Application.SetCompatibleTextRenderingDefault(false);

        string Insta_client_id = "DmOuuhRS4LS8S3LhbX9n2EjW7shaKt0QpT7FciN4",
               Insta_client_secret = "WY65xDAVaVMQhYWykNs23eL2Qc24UejH2DgKJrznNiXJi2MIsmC44xg2U7GVzGWgIxdQYeOgSjPa2hO8xw4jTg0Xhaq4cDDrnvVNcKabICwI3dZTuvLzzhIaXgJ5e1NS",
               Insta_Endpoint = "https://www.instamojo.com/v2/",//InstamojoConstants.INSTAMOJO_API_ENDPOINT,
               Insta_Auth_Endpoint = "https://www.instamojo.com/oauth2/token/";//InstamojoConstants.INSTAMOJO_AUTH_ENDPOINT;
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

        [HttpPost]
        public ActionResult Contact(ContactModel model)
        {
            model.Date = DateTime.Now;
            db.ContactData.Add(model);
            db.SaveChanges();

            return View("Index");

        }

        public ActionResult Gallery()
        {
            ViewBag.Message = "Your contact page.";

            return View();

        }


        public ActionResult Payment(int regId)
        {
            ViewBag.Message = "Your contact page.";
            FormData formData = new FormData();
            formData = db.FormData.Where(x => x.Id == regId).FirstOrDefault();
            return View("Payment", formData);

        }

        [HttpPost]
        public async Task<ActionResult> Payment(FormData formData)
        {

            formData.paymentDetails.PaymentStatus = "";
            formData.paymentDetails.PaymentReqId = "";
            formData.paymentDetails.RegId = formData.Id;
            db.PaymentData.Add(formData.paymentDetails);
            db.SaveChanges();

            //Instamojo.NET.Instamojo im = new Instamojo.NET.Instamojo("test_f2b269c4e7c933a88107ebfff76", "test_500c9ec9e355d7e79bf577d050b");
            
             Instamojo.NET.Instamojo im = new Instamojo.NET.Instamojo("71ab7fc45edff7f47455a2481aff1373", "dca3a056703e4094486eb686b9ad1f4b");
            PaymentRequest pr = new PaymentRequest();
            
            pr.allow_repeated_payments = false;
            pr.amount = formData.paymentDetails.Amount.ToString();
            pr.buyer_name = formData.paymentDetails.PayeeName;
            pr.email = formData.paymentDetails.Email;
            pr.phone = formData.paymentDetails.PhoneNumber;
            pr.send_email = true;
            pr.send_sms = true;
            pr.redirect_url = "http://akhandbramhansamaj.in/Home/PaymentStatus?id=" + formData.paymentDetails.Id;
        
            pr.purpose = "Registration Fees";
            PaymentRequest npr = await im.CreatePaymentRequest(pr);

            formData.paymentDetails.PaymentStatus = npr.status;
            formData.paymentDetails.PaymentReqId = npr.id;
            
            db.Entry(formData.paymentDetails).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectPermanent(npr.longurl);
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
                                      Server.MapPath("~/image/registration"), pic);

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
                data.ImageName = file.FileName;

            }

            db.FormData.Add(data);

            db.SaveChanges();
            int regId = data.Id;
            return RedirectToAction("Payment", new { @regId = regId });
        }

        [Authorize]
        public ActionResult DataList()
        {
            var model = new FormData();
            var data = db.FormData.Where(x=>x.PaymentStatus == true).ToList();
            List<FormData> _lstData = new List<FormData>();
            _lstData = data;
            model.DataList = _lstData;
            return View(model);
        }

        [Authorize]
        public ActionResult ContactsList()
        {
            var model = new ContactModel();
            var data = db.ContactData.ToList();
            List<ContactModel> _lstData = new List<ContactModel>();
            _lstData = data;
            model.ContactList = _lstData;
            return View(model);
        }

        [Authorize]
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

        public async Task<ActionResult> PaymentStatus(int id)
        {
            PaymentModel pm = new PaymentModel();
            pm = db.PaymentData.Where(x => x.Id == id).FirstOrDefault();
            var status = pm.PaymentStatus;
            var reqId = pm.PaymentReqId;
            Instamojo.NET.Instamojo im = new Instamojo.NET.Instamojo("71ab7fc45edff7f47455a2481aff1373", "dca3a056703e4094486eb686b9ad1f4b");
            PaymentRequest npr = await im.GetPaymentRequest(reqId);
            FormData form = new FormData();
            var d = db.FormData.Where(x => x.Id == pm.RegId).FirstOrDefault();
            if (npr.status == "Completed")
            {
                d.PaymentStatus = true;
                db.Entry(d).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("PaymentSucces");
            }
            else
            {
                d.PaymentStatus = false;
                db.Entry(d).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("PaymentFailure");

            }
        }

        public ActionResult PaymentSucces()
        {

            return View();
        }
        public ActionResult PaymentFailure()
        {

            return View();
        }
    }
}