using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FialBP.Models;

namespace FialBP.Controllers
{
    public class BusTypesController : Controller
    {
        private Model1 db = new Model1();

        // GET: BusTypes
        public ActionResult Index()
        {
            return View(db.BusTypes.ToList());
        }

        // GET: BusTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusType busType = db.BusTypes.Find(id);
            if (busType == null)
            {
                return HttpNotFound();
            }
            return View(busType);
        }

        // GET: BusTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BusTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BTypeId,BTName")] BusType busType)
        {
            if (ModelState.IsValid)
            {
                db.BusTypes.Add(busType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(busType);
        }

        // GET: BusTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusType busType = db.BusTypes.Find(id);
            if (busType == null)
            {
                return HttpNotFound();
            }
            return View(busType);
        }

        // POST: BusTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BTypeId,BTName")] BusType busType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(busType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(busType);
        }

        // GET: BusTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusType busType = db.BusTypes.Find(id);
            if (busType == null)
            {
                return HttpNotFound();
            }
            return View(busType);
        }

        // POST: BusTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BusType busType = db.BusTypes.Find(id);
            db.BusTypes.Remove(busType);
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
