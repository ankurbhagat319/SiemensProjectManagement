using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            var Users = db.Users.Select(x => new Requester
            {
                id = x.UserID,
           
               text = x.DisplayName
            });

            // return Json(assetDetails.ToArray(), JsonRequestBehavior.AllowGet);

            return new JsonResult()
            {
                Data = new
                {
                   Users = Users.ToArray(),
                   AsseDetails =  assetDetails.ToArray(),

                },
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
           
            };
        }

        public ActionResult AssetTransfer(string list)
        {
            //var jsonData = JObject.Parse(list);
            AssestTransfer _transefr = JsonConvert.DeserializeObject<AssestTransfer>(list);
            //var js = new jsonSear();
            //var deserializedList = (object[])js.DeserializeObject(list);
            ProjectManagementDB dbcontext = new ProjectManagementDB();
            if (ModelState.IsValid)
            {
                Create(_transefr);
                dbcontext.SaveChanges();
            }


            return null;
        }


        [HttpPost]
        public ActionResult Index(UserModel model)
        {
            selectedID = model.selectedUserID;
            var assets = db.AssetDetails.Where(x => x.UserID == selectedID).ToList();
            var plcs = db.PlcInfoes.Where(x => x.UserId == selectedID).ToList();
            var transfer = db.AssestTransfers.Where(x => x.Responsible_UserId == selectedID).ToList();
            var processList = db.AssestTransfers.Where(x => x.Requester_UserId == selectedID.ToString()).ToList();
            TempData["assets"] = assets;
            TempData["plcs"] = plcs;
            TempData["transfer"] = transfer;
            TempData["processList"] = processList;
            return RedirectToAction("Assets"); 
        }



        // POST: AssetTransfer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Transfer_id,ProjectId,AssetId,AssetType_Id,Responsible_UserId,Requester_UserId,Transfer_State,Responsible_Comments,Requester_Comments,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,IsCancelled,IsAcknowledeged,IsActive")] AssestTransfer assestTransfer)
        {
            if (ModelState.IsValid)
            {
                db.AssestTransfers.Add(assestTransfer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AssetType_Id = new SelectList(db.AssetTypes, "AssetType_Id", "AssetType_Name", assestTransfer.AssetType_Id);
            return View(assestTransfer);
        }


    }
}