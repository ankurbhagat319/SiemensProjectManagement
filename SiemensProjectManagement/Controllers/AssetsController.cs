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
        ProjectManagementDB db = new ProjectManagementDB();
        int selectedID;
        // GET: Assets
        public ActionResult Index()
        {
            UserModel uModel;
            uModel = new UserModel
            {
                Users = db.Users
            };
            return View(uModel);
        }

        [HttpPost]
        public ActionResult Index(UserModel model)
        {
            selectedID = model.selectedUserID;
            var assets = db.AssetDetails.Where(x => x.UserID == selectedID).ToList();
            var plcs = db.PlcInfoes.Where(x => x.UserId == selectedID).ToList();
            TempData["assets"] = assets;
            TempData["plcs"] = plcs;
            return RedirectToAction("Assets"); 
        }

        public ActionResult Assets()
        {
            return View();
        }
    }
}