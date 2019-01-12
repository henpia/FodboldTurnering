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
    public class RundeController : Controller
    {
        private FodboldTurneringDB db = new FodboldTurneringDB();

        // GET: Runde
        public ActionResult Index()
        {
            var runder = db.Runder.Include(r => r.Turnering);
            return View(runder.ToList());
        }

        // GET: Runde/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Runde runde = db.Runder.Find(id);
            if (runde == null)
            {
                return HttpNotFound();
            }
            return View(runde);
        }

        // GET: Runde/Create
        public ActionResult Create()
        {
            ViewBag.TurneringId = new SelectList(db.Turneringer, "TurneringId", "Navn");
            return View();
        }

        // POST: Runde/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RundeId,Betegnelse,TurneringId")] Runde runde)
        {
            if (ModelState.IsValid)
            {
                db.Runder.Add(runde);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TurneringId = new SelectList(db.Turneringer, "TurneringId", "Navn", runde.TurneringId);
            return View(runde);
        }

        // GET: Runde/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Runde runde = db.Runder.Find(id);
            if (runde == null)
            {
                return HttpNotFound();
            }
            ViewBag.TurneringId = new SelectList(db.Turneringer, "TurneringId", "Navn", runde.TurneringId);
            return View(runde);
        }

        // POST: Runde/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RundeId,Betegnelse,TurneringId")] Runde runde)
        {
            if (ModelState.IsValid)
            {
                db.Entry(runde).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TurneringId = new SelectList(db.Turneringer, "TurneringId", "Navn", runde.TurneringId);
            return View(runde);
        }

        // GET: Runde/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Runde runde = db.Runder.Find(id);
            if (runde == null)
            {
                return HttpNotFound();
            }
            return View(runde);
        }

        // POST: Runde/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Runde runde = db.Runder.Find(id);
            db.Runder.Remove(runde);
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
