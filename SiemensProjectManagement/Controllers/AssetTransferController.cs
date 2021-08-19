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
            var entry = db.AssestTransfers.Where(x => x.Transfer_id == assetTransfer.Transfer_id).FirstOrDefault();
            if (ModelState.IsValid)
            {
                entry.Transfer_State = assetTransfer.Transfer_State;
                entry.ModifiedBy = assetTransfer.ModifiedBy;
                entry.ModifiedDate = assetTransfer.ModifiedDate;
                entry.IsAcknowledeged = assetTransfer.IsAcknowledeged;
                entry.IsActive = assetTransfer.IsActive;
                entry.Requester_Comments = assetTransfer.Requester_Comments;
                db.Entry(entry).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
            }

            return null;
        }
        public ActionResult DeleteEntry(int? id)
        {
            AssestTransfer assestTransfer = db.AssestTransfers.Find(id);
            db.AssestTransfers.Remove(assestTransfer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetTransferDetails(int? id)
        {
            List<AssetTransferModel> assetTransferDetails = new List<AssetTransferModel>();

            assetTransferDetails = db.AssestTransfers.Where(x => x.Transfer_id == id).Select(x => new AssetTransferModel
            {
                TransferId = x.Transfer_id.ToString(),
                ProjectId = x.ProjectId.ToString(),
                AssetId = x.AssetId.ToString(),
                AssetType_Id = x.AssetType_Id.ToString(),
                Responsible_UserId = x.Responsible_UserId.ToString(),
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
