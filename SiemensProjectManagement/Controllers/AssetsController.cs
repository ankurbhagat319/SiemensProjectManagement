using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public ActionResult Assets()
        {
            return View();
        }
        public ActionResult GetAsset(string SerialNo)
        {
            List<AssetDetailsModel> assetDetails = new List<AssetDetailsModel>();

            assetDetails = db.AssetDetails.Where(x => x.Serial_No == SerialNo).Select(x => new AssetDetailsModel
            {
                UserID = x.UserID,
                UserName = x.UserName,
                DisplayName = x.User.DisplayName,
                ProjectId = x.ProjectID,
                ProjectName = x.Project.ProjectName,
                Type = x.Type,
                AssetType_Id = x.AssetType_Id,
                AssetType_Name = x.AssetType.AssetType_Name,
                Manufacturer = x.Manufacturer,
                Resources_Class = x.Resources_Class,
                Serial_No = x.Serial_No,
                HostName = x.HostName,
                SpiridonNo = x.SpiridonNo,
                Location = x.Location,
                PRNO = x.PRNO,
                PONO = x.PONO,
                WarrantyStartDate = x.WarrantyStartDate,
                AgeOfAsset = x.AgeOfAsset,
                ExpireBy = x.ExpireBy,
                Owner = x.Owner,
                RAM = x.RAM,
                Storage = x.Storage,
                Processor = x.Processor,
                CPUClockSpeed = x.CPUClockSpeed,
                PhysicalCores = x.PhysicalCores,
                NIC_Count = x.NIC_Count,
                 CreatedBy = x.CreatedBy,
                ModifiedBy = x.ModifiedBy,
       
            }).ToList();
       
            // return Json(assetDetails.ToArray(), JsonRequestBehavior.AllowGet);

            return new JsonResult()
            {
                Data = assetDetails.ToArray(),
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
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

     
    }
}