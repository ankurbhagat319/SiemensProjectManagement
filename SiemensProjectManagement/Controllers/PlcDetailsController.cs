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
    public class PlcDetailsController : Controller
    {
        private ProjectManagementDB db = new ProjectManagementDB();

        // GET: PlcDetails
        public async Task<ActionResult> Index()
        {
            var plcDetails = db.PlcDetails.Include(p => p.PlcInfo);
            return View(await plcDetails.ToListAsync());
        }

        // GET: PlcDetails/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlcDetail plcDetail = await db.PlcDetails.FindAsync(id);
            if (plcDetail == null)
            {
                return HttpNotFound();
            }
            return View(plcDetail);
        }

        // GET: PlcDetails/Create
        public ActionResult Create()
        {
            ViewBag.PlcId = new SelectList(db.PlcInfoes, "PlcId", "UserName");
            return View();
        }

        // POST: PlcDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PlcId,SerialNo,IpAddress,Subnet,Mlfb,Firmware,Mac_Address,Operating_State,Station_Name,PLC_Name,PLC_Type,SMC_SerialNo,Plant_DesignationId,Location_Id,IsPingable,IsFaulty")] PlcDetail plcDetail)
        {
            if (ModelState.IsValid)
            {
                db.PlcDetails.Add(plcDetail);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PlcId = new SelectList(db.PlcInfoes, "PlcId", "UserName", plcDetail.PlcId);
            return View(plcDetail);
        }

        // GET: PlcDetails/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlcDetail plcDetail = await db.PlcDetails.FindAsync(id);
            if (plcDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlcId = new SelectList(db.PlcInfoes, "PlcId", "UserName", plcDetail.PlcId);
            return View(plcDetail);
        }

        // POST: PlcDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PlcId,SerialNo,IpAddress,Subnet,Mlfb,Firmware,Mac_Address,Operating_State,Station_Name,PLC_Name,PLC_Type,SMC_SerialNo,Plant_DesignationId,Location_Id,IsPingable,IsFaulty")] PlcDetail plcDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(plcDetail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PlcId = new SelectList(db.PlcInfoes, "PlcId", "UserName", plcDetail.PlcId);
            return View(plcDetail);
        }

        // GET: PlcDetails/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlcDetail plcDetail = await db.PlcDetails.FindAsync(id);
            if (plcDetail == null)
            {
                return HttpNotFound();
            }
            return View(plcDetail);
        }

        // POST: PlcDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            PlcDetail plcDetail = await db.PlcDetails.FindAsync(id);
            db.PlcDetails.Remove(plcDetail);
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
