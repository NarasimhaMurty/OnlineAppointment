using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}