using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FialBP.Models;

namespace FialBP.Controllers
{
    public class BusesController : Controller
    {
        private Model1 db = new Model1();

        // GET: Buses
        public async Task<ActionResult> Index()
        {
            var buses = db.Buses.Include(b => b.BusType1);
            return View(await buses.ToListAsync());
        }

        // GET: Buses/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bus bus = await db.Buses.FindAsync(id);
            if (bus == null)
            {
                return HttpNotFound();
            }
            return View(bus);
        }

        // GET: Buses/Create
        public ActionResult Create()
        {
            ViewBag.BusType = new SelectList(db.BusTypes, "BTypeId", "BTName");
            return View();
        }

        // POST: Buses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BusId,BusName,BusType,NoOfSeat,LicenseNo,FitnessStatus")] Bus bus)
        {
            if (ModelState.IsValid)
            {
                db.Buses.Add(bus);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BusType = new SelectList(db.BusTypes, "BTypeId", "BTName", bus.BusType);
            return View(bus);
        }

        // GET: Buses/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bus bus =await db.Buses.FindAsync(id);
            if (bus == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusType = new SelectList(db.BusTypes, "BTypeId", "BTName", bus.BusType);
            return View(bus);
        }

        // POST: Buses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "BusId,BusName,BusType,NoOfSeat,LicenseNo,FitnessStatus")] Bus bus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bus).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BusType = new SelectList(db.BusTypes, "BTypeId", "BTName", bus.BusType);
            return View(bus);
        }

        // GET: Buses/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bus bus =await db.Buses.FindAsync(id);
            if (bus == null)
            {
                return HttpNotFound();
            }
            return View(bus);
        }

        // POST: Buses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Bus bus =await db.Buses.FindAsync(id);
            db.Buses.Remove(bus);
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
