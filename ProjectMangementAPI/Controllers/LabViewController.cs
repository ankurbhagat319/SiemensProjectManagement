using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectMangementAPI.Controllers
{
    public class LabViewController : Controller
    {
        // GET: LabView
        public ActionResult Index()
        {
            return View();
        }
    }
}