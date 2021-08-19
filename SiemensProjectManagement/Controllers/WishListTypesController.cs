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
    public class WishListTypesController : Controller
    {
        private ProjectManagementDB db = new ProjectManagementDB();

        // GET: WishListTypes
        public ActionResult Index()
        {
            return View(db.WishListTypes.ToList());
        }

        // GET: WishListTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WishListType wishListType = db.WishListTypes.Find(id);
            if (wishListType == null)
            {
                return HttpNotFound();
            }
            return View(wishListType);
        }

        // GET: WishListTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WishListTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WishTypeId,WishTypeName")] WishListType wishListType)
        {
            if (ModelState.IsValid)
            {
                db.WishListTypes.Add(wishListType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(wishListType);
        }

        // GET: WishListTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WishListType wishListType = db.WishListTypes.Find(id);
            if (wishListType == null)
            {
                return HttpNotFound();
            }
            return View(wishListType);
        }

        // POST: WishListTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WishTypeId,WishTypeName")] WishListType wishListType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wishListType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wishListType);
        }

        // GET: WishListTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WishListType wishListType = db.WishListTypes.Find(id);
            if (wishListType == null)
            {
                return HttpNotFound();
            }
            return View(wishListType);
        }

        // POST: WishListTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WishListType wishListType = db.WishListTypes.Find(id);
            db.WishListTypes.Remove(wishListType);
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
