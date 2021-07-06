using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectMangementAPI.Models;


namespace SiemensProjectManagement.Controllers
{
    public class PlcVirtualizationController : Controller
    {
        // GET: PlcVirtualization
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PlcVirtualization()
        {
            ProjectManagementDBEntities db = new ProjectManagementDBEntities();
          
            return View();
        }
    }
}