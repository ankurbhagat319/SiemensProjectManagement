using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectMangementAPI.Controllers
{
    public class AssetsController : Controller
    {
        // GET: Assets
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Assets()
        {
            return View();
        }
    }
}