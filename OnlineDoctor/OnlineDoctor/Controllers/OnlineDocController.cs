using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineDoctor.Models;

namespace OnlineDoctor.Controllers
{
    public class OnlineDocController : Controller
    {
        // GET: OnlineDoc
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Registration")]
        public ActionResult Registration_Post(Patient patient)
        {
            string message = "";
            bool status = false;
            if (ModelState.IsValid)
            {

                if (isEmailExist(patient.EmailID))
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    return View(patient);
                }
                else {
                    patient.Password = Crypto.Hash(patient.Password);
                   // patient.ConfirmPassword = Crypto.Hash(patient.ConfirmPassword);
                   using (PatientContext db = new PatientContext())
                    {
                        db.Patients.Add(patient);
                        db.SaveChanges();
                        status = true;
                        message = "Registration successfully done.";
                    }

                }
            }
            else
            {
                message = "Invalid Request";
            }
            ViewBag.status = status;
            ViewBag.message = message;

            return View(patient);
        }
        [NonAction]
        public bool isEmailExist(string emaiID)
        {
            bool isEmail= false;
            using(PatientContext db=new PatientContext())
            {
                var value = db.Patients.Where(p => p.EmailID == emaiID).FirstOrDefault();
                if(value !=null)
                {
                    isEmail = true;
                }
            }
            return isEmail;
        }
    }
}