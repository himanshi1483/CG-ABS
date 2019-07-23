using ABS_v1.Models;
using InstamojoAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace ABS_v1.Controllers
{
    public class HomeController : Controller
    {
        //System.Net.Mime.MediaTypeNames.Application.EnableVisualStyles();
        //Application.SetCompatibleTextRenderingDefault(false);

            string Insta_client_id = "tmLkZZ0zV41nJwhayBGBOI4m4I7bH55qpUBdEXGS",
                   Insta_client_secret = "IDejdccGqKaFlGav9bntKULvMZ0g7twVFolC9gdrh9peMS0megSFr7iDpWwWIDgFUc3W5SlX99fKnhxsoy6ipdAv9JeQwebmOU6VRvOEQnNMWwZnWglYmDGrfgKRheXs",
                   Insta_Endpoint = InstamojoConstants.INSTAMOJO_API_ENDPOINT,
                   Insta_Auth_Endpoint = InstamojoConstants.INSTAMOJO_AUTH_ENDPOINT;
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


        public ActionResult Payment()
        {
            try
            {
                Instamojo objClass = InstamojoImplementation.getApi(Insta_client_id, Insta_client_secret, Insta_Endpoint, Insta_Auth_Endpoint);

                #region   1. Create Payment Order
                //  Create Payment Order
                PaymentOrder objPaymentRequest = new PaymentOrder();
                //Required POST parameters
                objPaymentRequest.name = "ABCD";
                objPaymentRequest.email = "foo@example.com";
                objPaymentRequest.phone = "9969156561";
                objPaymentRequest.amount = 9;
                objPaymentRequest.currency = "INR";

                string randomName = Path.GetRandomFileName();
                randomName = randomName.Replace(".", string.Empty);
                objPaymentRequest.transaction_id = "test"+ 1121211;

                objPaymentRequest.redirect_url = "https://swaggerhub.com/api/saich/pay-with-instamojo/1.0.0";
                //Extra POST parameters 

                if (objPaymentRequest.validate())
                {

                    if (objPaymentRequest.nameInvalid)
                    {
                       throw new Exception("Name is not valid");
                    }

                }
                else
                {
                    try
                    {
                        CreatePaymentOrderResponse objPaymentResponse = objClass.createNewPaymentRequest(objPaymentRequest);
                        Console.Write("Order Id = " + objPaymentResponse.order.id);
                    }
                    catch (ArgumentNullException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    catch (WebException ex)
                    {
                         throw new Exception(ex.Message);
                    }
                    catch (IOException ex)
                    {
                         throw new Exception(ex.Message);
                    }
                    catch (InvalidPaymentOrderException ex)
                    {
                         throw new Exception(ex.Message);
                    }
                    catch (ConnectionException ex)
                    {
                         throw new Exception(ex.Message);
                    }
                    catch (BaseException ex)
                    {
                         throw new Exception(ex.Message);
                    }
                    catch (Exception ex)
                    {
                         throw new Exception("Error:" + ex.Message);
                    }
                }

                #endregion
                #region   2. Get All your Payment Orders List
                //  Get All your Payment Orders
                try
                {
                    PaymentOrderListRequest objPaymentOrderListRequest = new PaymentOrderListRequest();
                    //Optional Parameters
                    objPaymentOrderListRequest.limit = 21;
                    objPaymentOrderListRequest.page = 3;

                    PaymentOrderListResponse objPaymentRequestStatusResponse = objClass.getPaymentOrderList(objPaymentOrderListRequest);
                    foreach (var item in objPaymentRequestStatusResponse.orders)
                    {
                        Console.WriteLine(item.email + item.description + item.amount);
                    }
                    Console.Write("Order List = " + objPaymentRequestStatusResponse.orders.Count());
                }
                catch (ArgumentNullException ex)
                {
                     throw new Exception(ex.Message);
                }
                catch (WebException ex)
                {
                     throw new Exception(ex.Message);
                }
                catch (Exception ex)
                {
                     throw new Exception("Error:" + ex.Message);
                }
                #endregion

                #region   3. Get details of this payment order Using Order Id
                ////  Get details of this payment order
                try
                {
                    PaymentOrderDetailsResponse objPaymentRequestDetailsResponse = objClass.getPaymentOrderDetails("3189cff7c68245bface8915cac1f"); //"3189cff7c68245bface8915cac1f89df");
                    Console.Write("Transaction Id = " + objPaymentRequestDetailsResponse.transaction_id);
                }
                catch (ArgumentNullException ex)
                {
                     throw new Exception(ex.Message);
                }
                catch (WebException ex)
                {
                     throw new Exception(ex.Message);
                }
                catch (Exception ex)
                {
                     throw new Exception("Error:" + ex.Message);
                }
                #endregion

                #region   4. Get details of this payment order Using TransactionId
                ////  Get details of this payment order Using TransactionId
                try
                {
                    PaymentOrderDetailsResponse objPaymentRequestDetailsResponse = objClass.getPaymentOrderDetailsByTransactionId("test1");
                    Console.Write("Transaction Id = " + objPaymentRequestDetailsResponse.transaction_id);
                }
                catch (ArgumentNullException ex)
                {
                     throw new Exception(ex.Message);
                }
                catch (WebException ex)
                {
                     throw new Exception(ex.Message);
                }
                catch (Exception ex)
                {
                     throw new Exception("Error:" + ex.Message);
                }
                #endregion

                #region   5. Create Refund
                //  Create Payment Order
                Refund objRefundRequest = new Refund();
                //Required POST parameters
                //objPaymentRequest.name = "ABCD";
                objRefundRequest.payment_id = "MOJO6701005J41260385";
                objRefundRequest.type = "TNR";
                objRefundRequest.body = "abcd";
                objRefundRequest.refund_amount = 9;

                if (objRefundRequest.validate())
                {
                    if (objRefundRequest.payment_idInvalid)
                    {
                         throw new Exception("payment_id is not valid");
                    }
                }
                else
                {
                    try
                    {
                        CreateRefundResponce objRefundResponse = objClass.createNewRefundRequest(objRefundRequest);
                        Console.Write("Refund Id = " + objRefundResponse.refund.id);
                    }
                    catch (ArgumentNullException ex)
                    {
                         throw new Exception(ex.Message);
                    }
                    catch (WebException ex)
                    {
                         throw new Exception(ex.Message);
                    }
                    catch (IOException ex)
                    {
                         throw new Exception(ex.Message);
                    }
                    catch (InvalidPaymentOrderException ex)
                    {
                         throw new Exception(ex.Message);
                    }
                    catch (ConnectionException ex)
                    {
                         throw new Exception(ex.Message);
                    }
                    catch (BaseException ex)
                    {
                         throw new Exception(ex.Message);
                    }
                    catch (Exception ex)
                    {
                         throw new Exception("Error:" + ex.Message);
                    }
                }
                #endregion

            }
            catch (BaseException ex)
            {
                 throw new Exception("CustomException" + ex.Message);
            }
            catch (Exception ex)
            {
                 throw new Exception("Exception" + ex.Message);
            }

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
                data.ImageName = file.FileName;

            }
           
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

        public ActionResult ContactsList()
        {
            var model = new ContactModel();
            var data = db.ContactData.ToList();
            List<ContactModel> _lstData = new List<ContactModel>();
            _lstData = data;
            model.ContactList = _lstData;
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