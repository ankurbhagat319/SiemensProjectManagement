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
            var assets = db.AssetDetails.Select(x => x).Where(x => x.UserName == "z003tyvc").ToList();

            //var tmp = from et in db.AssetDetails
                 
            //          select new
            //          {
            //            ProjectName =  et.Project.ProjectName,
            //            HostName  = et.HostName,


            //          };

            TempData["assets"] = assets;
            return View();
        }
    }
}