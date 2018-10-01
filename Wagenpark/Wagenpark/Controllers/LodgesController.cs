using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Wagenpark.Models;

namespace Wagenpark.Controllers
{
    public class LodgesController : Controller
    {
        private KunjaDBConnection db = new KunjaDBConnection();

        // GET: Lodges
        public ActionResult Index()
        {
            var lodges = db.Lodges.Include(l => l.LodgeTypes);
            return View(lodges.ToList());
        }

        // GET: Lodges/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lodges lodges = db.Lodges.Find(id);
            if (lodges == null)
            {
                return HttpNotFound();
            }
            return View(lodges);
        }

        // GET: Lodges/Create
        public ActionResult Create()
        {
            ViewBag.LodgeTypeID = new SelectList(db.LodgeTypes, "LodgeTypeID", "LodgeTypeNaam");
            return View();
        }

        // POST: Lodges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LodgeID,LodgeTypeID,LodgeNR,LodgeNaam,Bezettingsgraad")] Lodges lodges)
        {
            if (ModelState.IsValid)
            {
                db.Lodges.Add(lodges);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LodgeTypeID = new SelectList(db.LodgeTypes, "LodgeTypeID", "LodgeTypeNaam", lodges.LodgeTypeID);
            return View(lodges);
        }

        // GET: Lodges/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lodges lodges = db.Lodges.Find(id);
            if (lodges == null)
            {
                return HttpNotFound();
            }
            ViewBag.LodgeTypeID = new SelectList(db.LodgeTypes, "LodgeTypeID", "LodgeTypeNaam", lodges.LodgeTypeID);
            return View(lodges);
        }

        // POST: Lodges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LodgeID,LodgeTypeID,LodgeNR,LodgeNaam,Bezettingsgraad")] Lodges lodges)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lodges).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LodgeTypeID = new SelectList(db.LodgeTypes, "LodgeTypeID", "LodgeTypeNaam", lodges.LodgeTypeID);
            return View(lodges);
        }

        // GET: Lodges/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lodges lodges = db.Lodges.Find(id);
            if (lodges == null)
            {
                return HttpNotFound();
            }
            return View(lodges);
        }

        // POST: Lodges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lodges lodges = db.Lodges.Find(id);
            db.Lodges.Remove(lodges);
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
