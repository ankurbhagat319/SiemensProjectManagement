using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiemensProjectManagement.Models;

namespace SiemensProjectManagement.Controllers
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
            ProjectManagementDB db = new ProjectManagementDB();

        
            
            var assets = db.AssetDetails.ToList();

       

            TempData["assets"] = assets;
            return View();
        }
    }
}