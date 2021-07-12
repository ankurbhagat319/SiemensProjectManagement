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
    public class WishListsController : Controller
    {
        private ProjectManagementDB db = new ProjectManagementDB();

        // GET: WishLists
        public ActionResult Index()
        {
            var wishLists = db.WishLists.Include(w => w.AssetType).Include(w => w.Project).Include(w => w.User).Include(w => w.WishListType).Include(w => w.WishMapper);
            return View(wishLists.ToList());
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
            ViewBag.UserId = new SelectList(db.Users, "UserID", "UserName");
            ViewBag.WishTypeId = new SelectList(db.WishListTypes, "WishTypeId", "WishTypeName");
            ViewBag.WishlistId = new SelectList(db.WishMappers, "WishListId", "RequestedDuration");
            return View();
        }

        // POST: WishLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WishlistId,WishTypeId,Description,UserId,ProjectID,AssetTypeId,AssetId,Quantity,RequestDuration,Comments,CreatedTime,CreatedBy,ModifiedTime,ModifiedBy,IsAcknowledged,IsMapped,IsActive")] WishList wishList)
        {
            if (ModelState.IsValid)
            {
                db.WishLists.Add(wishList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AssetTypeId = new SelectList(db.AssetTypes, "AssetType_Id", "AssetType_Name", wishList.AssetTypeId);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName", wishList.ProjectID);
            ViewBag.UserId = new SelectList(db.Users, "UserID", "UserName", wishList.UserId);
            ViewBag.WishTypeId = new SelectList(db.WishListTypes, "WishTypeId", "WishTypeName", wishList.WishTypeId);
            ViewBag.WishlistId = new SelectList(db.WishMappers, "WishListId", "RequestedDuration", wishList.WishlistId);
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
            ViewBag.UserId = new SelectList(db.Users, "UserID", "UserName", wishList.UserId);
            ViewBag.WishTypeId = new SelectList(db.WishListTypes, "WishTypeId", "WishTypeName", wishList.WishTypeId);
            ViewBag.WishlistId = new SelectList(db.WishMappers, "WishListId", "RequestedDuration", wishList.WishlistId);
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
            ViewBag.UserId = new SelectList(db.Users, "UserID", "UserName", wishList.UserId);
            ViewBag.WishTypeId = new SelectList(db.WishListTypes, "WishTypeId", "WishTypeName", wishList.WishTypeId);
            ViewBag.WishlistId = new SelectList(db.WishMappers, "WishListId", "RequestedDuration", wishList.WishlistId);
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
