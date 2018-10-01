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
    public class Lodgetypescontroller : Controller
    {
        private KunjaDBConnection db = new KunjaDBConnection();

        // GET: Lodgetypescontroller
        public ActionResult Index()
        {
            return View(db.LodgeTypes.ToList());
        }

        // GET: Lodgetypescontroller/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LodgeTypes lodgeTypes = db.LodgeTypes.Find(id);
            if (lodgeTypes == null)
            {
                return HttpNotFound();
            }
            return View(lodgeTypes);
        }

        // GET: Lodgetypescontroller/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lodgetypescontroller/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LodgeTypeNaam,LodgeOmschrijving,aantalpersonen,prijs")] LodgeTypes lodgeTypes)
        {
            if (ModelState.IsValid)
            {
                db.LodgeTypes.Add(lodgeTypes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lodgeTypes);
        }

        // GET: Lodgetypescontroller/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LodgeTypes lodgeTypes = db.LodgeTypes.Find(id);
            if (lodgeTypes == null)
            {
                return HttpNotFound();
            }
            return View(lodgeTypes);
        }

        // POST: Lodgetypescontroller/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LodgeTypeNaam,LodgeOmschrijving,aantalpersonen,prijs")] LodgeTypes lodgeTypes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lodgeTypes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lodgeTypes);
        }

        // GET: Lodgetypescontroller/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LodgeTypes lodgeTypes = db.LodgeTypes.Find(id);
            if (lodgeTypes == null)
            {
                return HttpNotFound();
            }
            return View(lodgeTypes);
        }

        // POST: Lodgetypescontroller/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LodgeTypes lodgeTypes = db.LodgeTypes.Find(id);
            db.LodgeTypes.Remove(lodgeTypes);
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
