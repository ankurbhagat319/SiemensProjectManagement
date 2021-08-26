using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SiemensLibraries;
using SiemensProjectManagement.Models;

namespace SiemensProjectManagement.Controllers
{
    public class AssetTransferController : Controller
    {
        private ProjectManagementDB db = new ProjectManagementDB();

        // GET: AssetTransfer
        public ActionResult Index()
        {
            var assestTransfers = db.AssestTransfers.Include(a => a.AssetType);
            return View(assestTransfers.ToList());
        }

        // GET: AssetTransfer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssestTransfer assestTransfer = db.AssestTransfers.Find(id);
            if (assestTransfer == null)
            {
                return HttpNotFound();
            }
            return View(assestTransfer);
        }

        // GET: AssetTransfer/Create
        public ActionResult Create()
        {
            ViewBag.AssetType_Id = new SelectList(db.AssetTypes, "AssetType_Id", "AssetType_Name");
            return View();
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

        // GET: AssetTransfer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssestTransfer assestTransfer = db.AssestTransfers.Find(id);
            if (assestTransfer == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssetType_Id = new SelectList(db.AssetTypes, "AssetType_Id", "AssetType_Name", assestTransfer.AssetType_Id);
            return View(assestTransfer);
        }

        // POST: AssetTransfer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Transfer_id,ProjectId,AssetId,AssetType_Id,Responsible_UserId,Requester_UserId,Transfer_State,Responsible_Comments,Requester_Comments,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,IsCancelled,IsAcknowledeged,IsActive")] AssestTransfer assestTransfer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assestTransfer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssetType_Id = new SelectList(db.AssetTypes, "AssetType_Id", "AssetType_Name", assestTransfer.AssetType_Id);
            return View(assestTransfer);
        }

        public ActionResult EditEntry(string list)
        {
            AssestTransfer assetTransfer = JsonConvert.DeserializeObject<AssestTransfer>(list);
            var entry = db.AssestTransfers.Where(x => x.Transfer_id == assetTransfer.Transfer_id).ToList();
            if (ModelState.IsValid)
            {
                entry[0].Transfer_State = assetTransfer.Transfer_State;
                entry[0].ModifiedBy = assetTransfer.ModifiedBy;
                entry[0].ModifiedDate = assetTransfer.ModifiedDate;
                entry[0].IsAcknowledeged = assetTransfer.IsAcknowledeged;
                entry[0].IsActive = assetTransfer.IsActive;
                entry[0].Requester_Comments = assetTransfer.Requester_Comments;

                db.Entry(entry[0]).State = EntityState.Modified;
                db.SaveChanges();
                if (assetTransfer.AssetType_Id == 1)
                {
                    var PlcUpdate = db.PlcInfoes.Where(x => x.PlcId == assetTransfer.AssetId).ToList();
                    PlcUpdate[0].UserId = Convert.ToInt16(assetTransfer.Requester_UserId);
                    var userID = Convert.ToInt16(assetTransfer.Requester_UserId);
                    var UserName = db.Users.Where(x => x.UserID == userID).ToList();
                    PlcUpdate[0].UserName = UserName[0].UserName.ToString();
                    db.Entry(PlcUpdate[0]).State = EntityState.Modified;
                    db.SaveChanges();
                    var template = EmailUtility.UpdateEmailTemplate(entry[0].AssetType.AssetType_Name, entry[0].AssetType.PlcInfoes.FirstOrDefault().Device.Mlfb, entry[0].Transfer_State, entry[0].User.DisplayName, PlcUpdate[0].Serial_No, entry[0].Requester_Comments, "Asset Transfer Request has been Approved by User Successfully"); ;
                    var Requester = Convert.ToInt32(entry[0].Requester_UserId);
                    var Responsible = Convert.ToInt32(entry[0].Responsible_UserId);
                    var RequesterEmail = db.Users.Where(x => x.UserID == Requester).FirstOrDefault().Email.ToString();
                    var Responsiblemail = db.Users.Where(x => x.UserID == Responsible).FirstOrDefault().Email.ToString();
                    var emailRecipents = RequesterEmail + "," + Responsiblemail;
                    EmailUtility.SendEmail("ProjectAssetsdept@Itest.com", emailRecipents, "Asset Transfer Request has been Approved by User Successfully", template, true);
                }
                else
                {
                    var PlcUpdate = db.AssetDetails.Where(x => x.Id == assetTransfer.AssetId).ToList();
                    PlcUpdate[0].UserID = Convert.ToInt16(assetTransfer.Requester_UserId);
                    var userID = Convert.ToInt16(assetTransfer.Requester_UserId);
                    var UserName = db.Users.Where(x => x.UserID == userID).ToList();
                    PlcUpdate[0].UserName = UserName[0].UserName.ToString();
                    db.Entry(PlcUpdate[0]).State = EntityState.Modified;
                    db.SaveChanges();
                    var template = EmailUtility.UpdateEmailTemplate(entry[0].AssetType.AssetType_Name, entry[0].AssetType.AssetDetails.FirstOrDefault().Serial_No, entry[0].Transfer_State, entry[0].User.DisplayName, PlcUpdate[0].Serial_No, entry[0].Requester_Comments, "Asset Transfer Request has been Approved by User Successfully"); ;
                    var Requester = Convert.ToInt32(entry[0].Requester_UserId);
                    var Responsible = Convert.ToInt32(entry[0].Responsible_UserId);
                    var RequesterEmail = db.Users.Where(x => x.UserID == Requester).FirstOrDefault().Email.ToString();
                    var Responsiblemail = db.Users.Where(x => x.UserID == Responsible).FirstOrDefault().Email.ToString();
                    var emailRecipents = RequesterEmail + "," + Responsiblemail;
                    EmailUtility.SendEmail("ProjectAssetsdept@Itest.com", emailRecipents, "Asset Transfer Request has been Approved by User Successfully", template, true);
                }
             
               
                //return RedirectToAction("Index");
            }

            return null;
        }
        public ActionResult RejectEntry(int transferId,int Requester,int Responsible)
        {

            var template = "";
            var entry = db.AssestTransfers.Where(x => x.Transfer_id == transferId).ToList();
            if (ModelState.IsValid)
            {
                
                entry[0].IsActive =false;
                entry[0].Transfer_State = "Rejected";

                db.Entry(entry[0]).State = EntityState.Modified;
                db.SaveChanges();
            }
            var RequesterEmail = db.Users.Where(x => x.UserID == Requester).FirstOrDefault().Email.ToString();
            var Responsiblemail = db.Users.Where(x => x.UserID == Responsible).FirstOrDefault().Email.ToString();
            var emailRecipents = RequesterEmail + "," + Responsiblemail;
            if(entry[0].AssetType_Id == 1)
            {
                template = EmailUtility.UpdateEmailTemplate(entry[0].AssetType.AssetType_Name, entry[0].AssetType.PlcInfoes.FirstOrDefault().Device.Mlfb, entry[0].Transfer_State, entry[0].User.DisplayName, "", "", "Asset Transfer Request has been rejected by Requester.");
            }
            else
            {
                template = EmailUtility.UpdateEmailTemplate(entry[0].AssetType.AssetType_Name, entry[0].AssetType.AssetDetails.FirstOrDefault().HostName, entry[0].Transfer_State, entry[0].User.DisplayName,  entry[0].AssetType.AssetDetails.First().Serial_No, entry[0].Requester_Comments, "Asset Transfer Request has been rejected by Requester.");
            }
             
            // EmailUtility.SendEmail("ProjectAssetsdept@Itest.com", "ankur.bhagat@siemens.com,ajith.k@siemens.com,monika.babu@siemens.com,suresh.mohan@siemens.com,ashutosh.kumar@siemens.com", "Asset Transfer Request", template, true);
            EmailUtility.SendEmail("ProjectAssetsdept@Itest.com", emailRecipents, "Asset Transfer Request has been rejected by Requester.", template, true);
            return Json(new { message = "Rejected" });
        }

        public ActionResult GetTransferDetails(int? id)
        {
            List<AssetTransferModel> assetTransferDetails = new List<AssetTransferModel>();
         
            assetTransferDetails = db.AssestTransfers.Where(x => x.Transfer_id == id).Select(x => new AssetTransferModel
            {
                
                TransferId = x.Transfer_id.ToString(),
                ProjectId = x.ProjectId.ToString(),
                ProjectName = x.Project.ProjectName.ToString(),
                AssetId = x.AssetId.ToString(),
                AssetType_Id = x.AssetType_Id.ToString(),
                AssetTypeName = x.AssetType.AssetType_Name.ToString(),
                Responsible_UserId = x.Responsible_UserId.ToString(),
                Responsible_Name = x.User.DisplayName.ToString(),
                Requester_Name = x.User.DisplayName.ToString(),
       
                Requester_UserId = x.Requester_UserId,
                
                Transfer_State = x.Transfer_State,
                Responsible_Comments = x.Responsible_Comments,
                Requester_Comments = x.Requester_Comments,
                CreatedDate = x.CreatedDate.ToString(),
                CreatedBy = x.CreatedBy,
                ModifiedDate = x.ModifiedDate.ToString(),
                ModifiedBy = x.ModifiedBy,
                IsCancelled = x.IsCancelled.ToString(),
                IsAcknowledeged = x.IsAcknowledeged.ToString(),
                IsActive = x.IsActive.ToString()

            }).ToList();
            //var Users = db.Users.Select(x => new Requester
            //{
            //    id = x.UserID,

            //    text = x.DisplayName
            //});

            // return Json(assetDetails.ToArray(), JsonRequestBehavior.AllowGet);

            return new JsonResult()
            {
                Data = new
                {
                    //Users = Users.ToArray(),
                    AssetTransferDetails = assetTransferDetails.ToArray(),
                },
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue

            };
        }

        // GET: AssetTransfer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssestTransfer assestTransfer = db.AssestTransfers.Find(id);
            if (assestTransfer == null)
            {
                return HttpNotFound();
            }
            return View(assestTransfer);
        }

        // POST: AssetTransfer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssestTransfer assestTransfer = db.AssestTransfers.Find(id);
            db.AssestTransfers.Remove(assestTransfer);
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
