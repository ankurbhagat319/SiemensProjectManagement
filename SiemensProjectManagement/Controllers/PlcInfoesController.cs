using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SiemensProjectManagement.Models;

namespace SiemensProjectManagement.Controllers
{
    public class PlcInfoesController : Controller
    {
        private ProjectManagementDB db = new ProjectManagementDB();

        // GET: PlcInfoes
        public async Task<ActionResult> Index()
        {
            var plcInfoes = db.PlcInfoes.Include(p => p.Device).Include(p => p.PlcInfo1).Include(p => p.PlcInfo2);
            return View(await plcInfoes.ToListAsync());
        }

        // GET: PlcInfoes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlcInfo plcInfo = await db.PlcInfoes.FindAsync(id);
            if (plcInfo == null)
            {
                return HttpNotFound();
            }
            return View(plcInfo);
        }

        // GET: PlcInfoes/Create
        public ActionResult Create()
        {
            ViewBag.DeviceId = new SelectList(db.Devices, "Id", "Mlfb");
            ViewBag.PlcId = new SelectList(db.PlcInfoes, "PlcId", "UserName");
            ViewBag.PlcId = new SelectList(db.PlcInfoes, "PlcId", "UserName");
            return View();
        }

        // POST: PlcInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PlcId,UserId,UserName,ProjectId,IpAddress,DeviceId,Rack_No,Serial_No,Comments,AssetType_id,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,IsActive")] PlcInfo plcInfo)
        {
            if (ModelState.IsValid)
            {
                db.PlcInfoes.Add(plcInfo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.DeviceId = new SelectList(db.Devices, "Id", "Mlfb", plcInfo.DeviceId);
            ViewBag.PlcId = new SelectList(db.PlcInfoes, "PlcId", "UserName", plcInfo.PlcId);
            ViewBag.PlcId = new SelectList(db.PlcInfoes, "PlcId", "UserName", plcInfo.PlcId);
            return View(plcInfo);
        }

        // GET: PlcInfoes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlcInfo plcInfo = await db.PlcInfoes.FindAsync(id);
            if (plcInfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeviceId = new SelectList(db.Devices, "Id", "Mlfb", plcInfo.DeviceId);
            ViewBag.PlcId = new SelectList(db.PlcInfoes, "PlcId", "UserName", plcInfo.PlcId);
            ViewBag.PlcId = new SelectList(db.PlcInfoes, "PlcId", "UserName", plcInfo.PlcId);
            return View(plcInfo);
        }

        // POST: PlcInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PlcId,UserId,UserName,ProjectId,IpAddress,DeviceId,Rack_No,Serial_No,Comments,AssetType_id,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,IsActive")] PlcInfo plcInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(plcInfo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DeviceId = new SelectList(db.Devices, "Id", "Mlfb", plcInfo.DeviceId);
            ViewBag.PlcId = new SelectList(db.PlcInfoes, "PlcId", "UserName", plcInfo.PlcId);
            ViewBag.PlcId = new SelectList(db.PlcInfoes, "PlcId", "UserName", plcInfo.PlcId);
            return View(plcInfo);
        }

        // GET: PlcInfoes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlcInfo plcInfo = await db.PlcInfoes.FindAsync(id);
            if (plcInfo == null)
            {
                return HttpNotFound();
            }
            return View(plcInfo);
        }

        // POST: PlcInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PlcInfo plcInfo = await db.PlcInfoes.FindAsync(id);
            db.PlcInfoes.Remove(plcInfo);
            await db.SaveChangesAsync();
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
