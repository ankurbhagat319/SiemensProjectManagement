using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SiemensProjectManagement.Models;

namespace SiemensProjectManagement.Controllers
{
    public class AssetDetailsController : Controller
    {
        private ProjectManagementDBEntities db = new ProjectManagementDBEntities();

        // GET: AssetDetails
        public ActionResult Index()
        {
            var assetDetails = db.AssetDetails.Include(a => a.AssetType).Include(a => a.Project).Include(a => a.User);
            return View(assetDetails.ToList());
        }

        // GET: AssetDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssetDetail assetDetail = db.AssetDetails.Find(id);
            if (assetDetail == null)
            {
                return HttpNotFound();
            }
            return View(assetDetail);
        }

        // GET: AssetDetails/Create
        public ActionResult Create()
        {
            ViewBag.AssetType_Id = new SelectList(db.AssetTypes, "AssetType_Id", "AssetType_Name");
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName");
            return View();
        }

        // POST: AssetDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserID,UserName,ProjectID,Type,AssetType_Id,Manufacturer,Resources_Class,Serial_No,HostName,SpiridonNo,Location,PRNO,PONO,WarrantyStartDate,AgeOfAsset,ExpireBy,Owner,RAM,Storage,Processor,CPUClockSpeed,PhysicalCores,NIC_Count,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,ISActive,ToBeReplaced,Acknowledge")] AssetDetail assetDetail)
        {
            if (ModelState.IsValid)
            {
                db.AssetDetails.Add(assetDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AssetType_Id = new SelectList(db.AssetTypes, "AssetType_Id", "AssetType_Name", assetDetail.AssetType_Id);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName", assetDetail.ProjectID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", assetDetail.UserID);
            return View(assetDetail);
        }

        // GET: AssetDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssetDetail assetDetail = db.AssetDetails.Find(id);
            if (assetDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssetType_Id = new SelectList(db.AssetTypes, "AssetType_Id", "AssetType_Name", assetDetail.AssetType_Id);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName", assetDetail.ProjectID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", assetDetail.UserID);
            return View(assetDetail);
        }

        // POST: AssetDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserID,UserName,ProjectID,Type,AssetType_Id,Manufacturer,Resources_Class,Serial_No,HostName,SpiridonNo,Location,PRNO,PONO,WarrantyStartDate,AgeOfAsset,ExpireBy,Owner,RAM,Storage,Processor,CPUClockSpeed,PhysicalCores,NIC_Count,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,ISActive,ToBeReplaced,Acknowledge")] AssetDetail assetDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assetDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssetType_Id = new SelectList(db.AssetTypes, "AssetType_Id", "AssetType_Name", assetDetail.AssetType_Id);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName", assetDetail.ProjectID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", assetDetail.UserID);
            return View(assetDetail);
        }

        // GET: AssetDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssetDetail assetDetail = db.AssetDetails.Find(id);
            if (assetDetail == null)
            {
                return HttpNotFound();
            }
            return View(assetDetail);
        }

        // POST: AssetDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssetDetail assetDetail = db.AssetDetails.Find(id);
            db.AssetDetails.Remove(assetDetail);
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
