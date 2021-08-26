using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SiemensLibraries;
using SiemensProjectManagement.Models;

namespace SiemensProjectManagement.Controllers
{
    public class WishListsController : Controller
    {
        private ProjectManagementDB db = new ProjectManagementDB();

        
        // GET: WishLists
        public ActionResult Index()
        {
            //var wishLists = db.WishLists.Include(w => w.AssetType).Include(w => w.Project).Include(w => w.User)
            //    .Include(w => w.WishListType);
            //.Select(x=> new { AssetId = ((x.AssetTypeId == 1) ? (from v1 in db.Devices where v1.Id == x.AssetId select v1.ProductName)
            //: ((x.AssetTypeId == 2) ? (from v1 in getList where v1.Key == x.AssetId select v1.Value)
            // : (from v1 in db.Tools where v1.ToolId == x.AssetId select v1.Description)))
            // });
            //(from val in db.WishLists

            // select new
            // {
            //     val.WishListType,
            //     val.Description,
            //     ["AssetId#"] = ((val.AssetTypeId == 1) ? (from v1 in db.Devices where v1.Id == val.AssetId select v1.ProductName)
            //   : ((val.AssetTypeId == 2) ? (from v1 in getList where v1.Key == val.AssetId select v1.Value)
            //    : (from v1 in db.Tools where v1.ToolId == val.AssetId select v1.Description))),

            // });

            List<WishListDetailsModel> wishLists = db.GetWishListDetails().Select(x => new WishListDetailsModel {
                WishlistId = x.WishlistId,
                WishTypeName = x.WishTypeName,
                UserName = x.UserName,
                ProjectName = x.ProjectName,
                AssetType_Name = x.AssetType_Name,
                Asset = x.Asset,
                Quantity =(int) x.Quantity,
                Description = x.Description,
                RequestDuration = x.RequestDuration
            }).ToList();

            TempData["PLCwishList"] = wishLists.Where(x => x.AssetType_Name.ToUpper() == "PLC").ToList();
            TempData["PCwishList"] = wishLists.Where(x => x.AssetType_Name.Contains("PC")).ToList();
            TempData["ToolswishList"] = wishLists.Where(x => x.AssetType_Name.ToUpper() == "TOOLS").ToList();

            return View();
        }


        public JsonResult GetDetails(string id)
        {
            JsonResult result = null;
            switch (id)
            {
                case "1":
                    ViewBag.AssetId = (from clist in db.Devices
                                       where (clist.TypeName == id)
                                       select new { clist.Id, clist.ProductName }); //new SelectList(db.Devices, "Id", "ProductName");

                    result = Json(new SelectList(db.Devices, "Id", "ProductName"));
                    break;
                case "2":
                    ViewBag.AssetId = (from clist in db.Assets
                                       select new { clist.Id, clist.Asset1 });  //new SelectList(db.Devices, "Id", "ProductName");

                    result = Json(new SelectList(db.Assets, "Id", "Asset1"));
                    break;
                case "3":
                    ViewBag.AssetId = (from clist in db.Tools
                                       where (clist.AssetType_Id == int.Parse(id))
                                       select new { clist.ToolId, clist.Description }); //new SelectList(db.Devices, "Id", "ProductName");

                    result = Json(new SelectList(db.Tools, "ToolId", "Description"));
                    break;
            }

            return result;
        }
        public JsonResult GetAutoMappedAssetDetails()
        {

            JsonResult result = null;
            var wishLists = db.GetWishAutoMappedDetails().Select(x => new GetWishAutomappedDetailsModel
            {
                WishlistId = x.WishlistId,
                WishTypeName = x.WishTypeName,
                ProjectName = x.ProjectName,
                AssetType_Name = x.AssetType_Name,
                Asset = x.Asset,
                AvailableQuantity = (int)x.AvailableQuantity,
                Description = x.Description,
                RequestDuration = x.RequestDuration,
                AvailableDuration = x.AvailableDuration
            }).ToList();

            TempData["MappedAssetDetails"] = wishLists.ToList();

            result = Json( wishLists.ToList());
            return result;
        }
        public ActionResult GetmappingRequest(int wishId) {

        var mappedWish =     db.WishLists.Where(x => x.WishlistId == wishId).ToList();
            
            var  responsibleUserId = Convert.ToInt32(mappedWish[0].UserId);
          
            var Responsiblemail = db.Users.Where(x => x.UserID == responsibleUserId).FirstOrDefault().Email.ToString();


            var recipents = Responsiblemail;
            if (mappedWish[0].AssetTypeId == 1)
            {
                var template = EmailUtility.UpdateEmailTemplate(mappedWish[0].AssetType.AssetType_Name, mappedWish[0].AssetType.PlcInfoes.FirstOrDefault().Device.Mlfb, "Initiated", Responsiblemail, mappedWish[0].AssetType.PlcInfoes.FirstOrDefault().IpAddress, mappedWish[0].Comments, "Automapped Transfer Request sent to user");
                EmailUtility.SendEmail("ProjectAssetsdept@Itest.com", recipents + ", ankur.bhagat@siemens.com", "Asset Transfer Request", template, true);
            }
            else if (mappedWish[0].AssetTypeId == 2)
            {
                var template = EmailUtility.UpdateEmailTemplate(mappedWish[0].AssetType.AssetType_Name, mappedWish[0].AssetType.AssetDetails.FirstOrDefault().HostName, "Initiated", Responsiblemail, mappedWish[0].AssetType.AssetDetails.FirstOrDefault().Serial_No, mappedWish[0].Comments, "Automapped Transfer Request sent to user");
                EmailUtility.SendEmail("ProjectAssetsdept@Itest.com", recipents + ", ankur.bhagat@siemens.com", "Asset Transfer Request", template, true);
            }
            else
            {
                var template = EmailUtility.UpdateEmailTemplate(mappedWish[0].AssetType.AssetType_Name, mappedWish[0].AssetType.Tools.FirstOrDefault().Description, "Initiated", Responsiblemail, mappedWish[0].AssetType.Tools.FirstOrDefault().SerialNo, mappedWish[0].Comments, "Automapped Transfer Request sent to user");
                EmailUtility.SendEmail("ProjectAssetsdept@Itest.com", recipents + ", ankur.bhagat@siemens.com", "Asset Transfer Request", template, true);
            }
            return Json("Request sent");
                
                }
        public ActionResult MappedAssetDetails()
        {

            var wishLists = db.GetWishAutoMappedDetails().Select(x => new GetWishAutomappedDetailsModel
            {
                WishlistId = x.WishlistId,
                WishTypeName = x.WishTypeName,
                ProjectName = x.ProjectName,
                AssetType_Name = x.AssetType_Name,
                Asset = x.Asset,
                AvailableQuantity = (int)x.AvailableQuantity,
                Description = x.Description,
                RequestDuration = x.RequestDuration,
                AvailableDuration = x.AvailableDuration
            }).ToList();

            TempData["MappedAssetDetails"] = wishLists.ToList();

            ViewBag.MappedAssetDetails = wishLists.ToList();
            return View();
        }
        // GET: WishLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WishList wishList = db.WishLists.Find(id);
            if (wishList == null)
            {
                return HttpNotFound();
            }
            return View(wishList);
        }

        // GET: WishLists/Create
        public ActionResult Create()
        {
            ViewBag.AssetTypeId = new SelectList(db.AssetTypes, "AssetType_Id", "AssetType_Name");
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName");
            ViewBag.UserId = new SelectList(db.Users, "UserID", "DisplayName");
            ViewBag.WishTypeId = new SelectList(db.WishListTypes, "WishTypeId", "WishTypeName");
            ViewBag.AssetId = new SelectList(db.Devices, "Id", "ProductName");
            return View();
        }

        // POST: WishLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "WishlistId,WishTypeId,Description,UserId,ProjectID,AssetTypeId,AssetId,Quantity,RequestDuration,Comments,CreatedTime,CreatedBy,ModifiedTime,ModifiedBy,IsAcknowledged,IsMapped,IsActive")] WishList wishList)

        public ActionResult Create([Bind(Include = "WishlistId,WishTypeId,Description,UserId,ProjectID,AssetTypeId,AssetId,Quantity,RequestDuration,Comments")] WishList wishList)
        {
            if (ModelState.IsValid)
            {
                db.WishLists.Add(wishList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AssetTypeId = new SelectList(db.AssetTypes, "AssetType_Id", "AssetType_Name", wishList.AssetTypeId);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName", wishList.ProjectID);
            ViewBag.UserId = new SelectList(db.Users, "UserID", "DisplayName", wishList.UserId);
            ViewBag.WishTypeId = new SelectList(db.WishListTypes, "WishTypeId", "WishTypeName", wishList.WishTypeId);
            ViewBag.AssetId = (wishList.AssetTypeId==1)?new SelectList(db.Devices, "Id", "ProductName", wishList.AssetId)
               : ((wishList.AssetTypeId == 2) ? new SelectList(db.Assets, "Id", "Asset", wishList.AssetId) 
               :new SelectList(db.Tools, "ToolId", "Description", wishList.AssetId));
            return View(wishList);
        }

        // GET: WishLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WishList wishList = db.WishLists.Find(id);
            if (wishList == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssetTypeId = new SelectList(db.AssetTypes, "AssetType_Id", "AssetType_Name", wishList.AssetTypeId);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName", wishList.ProjectID);
            ViewBag.UserId = new SelectList(db.Users, "UserID", "DisplayName", wishList.UserId);
            ViewBag.WishTypeId = new SelectList(db.WishListTypes, "WishTypeId", "WishTypeName", wishList.WishTypeId);
            ViewBag.AssetId = (wishList.AssetTypeId == 1) ? new SelectList(db.Devices, "Id", "ProductName", wishList.AssetId)
               : ((wishList.AssetTypeId == 2) ? new SelectList(db.Assets, "Id", "Asset1", wishList.AssetId)
               : new SelectList(db.Tools, "ToolId", "Description", wishList.AssetId));
            return View(wishList);
        }

        // POST: WishLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WishlistId,WishTypeId,Description,UserId,ProjectID,AssetTypeId,AssetId,Quantity,RequestDuration,Comments,CreatedTime,CreatedBy,ModifiedTime,ModifiedBy,IsAcknowledged,IsMapped,IsActive")] WishList wishList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wishList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssetTypeId = new SelectList(db.AssetTypes, "AssetType_Id", "AssetType_Name", wishList.AssetTypeId);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName", wishList.ProjectID);
            ViewBag.UserId = new SelectList(db.Users, "UserID", "DisplayName", wishList.UserId);
            ViewBag.WishTypeId = new SelectList(db.WishListTypes, "WishTypeId", "WishTypeName", wishList.WishTypeId);
            ViewBag.AssetId = (wishList.AssetTypeId == 1) ? new SelectList(db.Devices, "Id", "ProductName", wishList.AssetId)
               : ((wishList.AssetTypeId == 2) ? new SelectList(db.Assets, "Id", "Asset1", wishList.AssetId)
               : new SelectList(db.Tools, "ToolId", "Description", wishList.AssetId));
            return View(wishList);
        }

        // GET: WishLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WishList wishList = db.WishLists.Find(id);
            if (wishList == null)
            {
                return HttpNotFound();
            }
            //wishList. 
            return View(wishList);
        }

        // POST: WishLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WishList wishList = db.WishLists.Find(id);
            db.WishLists.Remove(wishList);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
