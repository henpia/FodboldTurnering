using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FT.DAL.Repositories;
using FT.Entities.Models;

namespace FT.WEB.Controllers
{
    public class HoldController : Controller
    {
        private FodboldTurneringDB db = new FodboldTurneringDB();

        // GET: Hold
        public ActionResult Index()
        {
            var holdListe = db.HoldListe.Include(h => h.Turnering);
            return View(holdListe.ToList());
        }

        // GET: Hold/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hold hold = db.HoldListe.Find(id);
            if (hold == null)
            {
                return HttpNotFound();
            }
            return View(hold);
        }

        // GET: Hold/Create
        public ActionResult Create()
        {
            ViewBag.TurneringId = new SelectList(db.Turneringer, "TurneringId", "Navn");
            return View();
        }

        // POST: Hold/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HoldId,Navn,TurneringId")] Hold hold)
        {
            if (ModelState.IsValid)
            {
                db.HoldListe.Add(hold);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TurneringId = new SelectList(db.Turneringer, "TurneringId", "Navn", hold.TurneringId);
            return View(hold);
        }

        // GET: Hold/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hold hold = db.HoldListe.Find(id);
            if (hold == null)
            {
                return HttpNotFound();
            }
            ViewBag.TurneringId = new SelectList(db.Turneringer, "TurneringId", "Navn", hold.TurneringId);
            return View(hold);
        }

        // POST: Hold/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HoldId,Navn,TurneringId")] Hold hold)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hold).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TurneringId = new SelectList(db.Turneringer, "TurneringId", "Navn", hold.TurneringId);
            return View(hold);
        }

        // GET: Hold/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hold hold = db.HoldListe.Find(id);
            if (hold == null)
            {
                return HttpNotFound();
            }
            return View(hold);
        }

        // POST: Hold/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hold hold = db.HoldListe.Find(id);
            db.HoldListe.Remove(hold);
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
