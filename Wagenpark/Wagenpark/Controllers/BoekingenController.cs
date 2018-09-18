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
    public class BoekingenController : Controller
    {
        private MijnBoekingenDB db = new MijnBoekingenDB();

        // GET: Boekingen
        public ActionResult Index()
        {
            return View(db.Boekingen.ToList());
        }

        // GET: Boekingen/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Boekingen boekingen = db.Boekingen.Find(id);
            if (boekingen == null)
            {
                return HttpNotFound();
            }
            return View(boekingen);
        }

        // GET: Boekingen/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Boekingen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Boekingid,gastID,lodgeID,incheckdatum,uitcheckdatum")] Boekingen boekingen)
        {
            if (ModelState.IsValid)
            {
                db.Boekingen.Add(boekingen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(boekingen);
        }

        // GET: Boekingen/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Boekingen boekingen = db.Boekingen.Find(id);
            if (boekingen == null)
            {
                return HttpNotFound();
            }
            return View(boekingen);
        }

        // POST: Boekingen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Boekingid,gastID,lodgeID,incheckdatum,uitcheckdatum")] Boekingen boekingen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(boekingen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(boekingen);
        }

        // GET: Boekingen/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Boekingen boekingen = db.Boekingen.Find(id);
            if (boekingen == null)
            {
                return HttpNotFound();
            }
            return View(boekingen);
        }

        // POST: Boekingen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Boekingen boekingen = db.Boekingen.Find(id);
            db.Boekingen.Remove(boekingen);
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
