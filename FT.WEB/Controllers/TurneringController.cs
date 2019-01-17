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
    public class TurneringController : Controller
    {
        private FodboldTurneringDB db = new FodboldTurneringDB();

        // GET: Turnering
        public ActionResult Index()
        {
            return View(db.Turneringer.ToList());
        }

        // GET: Turnering/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Turnering turnering = db.Turneringer.Find(id);
            // TODO: Overskrifter for liste over hold er hardcoded i Details View - skal rettes.
            Turnering turnering = db.Turneringer.Include(h => h.HoldListe).Where(d => d.TurneringId == id).First();
            if (turnering == null)
            {
                return HttpNotFound();
            }
            return View(turnering);
        }

        // GET: Turnering/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Turnering/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TurneringId,Navn,MaxAntalHold")] Turnering turnering)
        {
            if (ModelState.IsValid)
            {
                db.Turneringer.Add(turnering);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(turnering);
        }

        // GET: Turnering/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turnering turnering = db.Turneringer.Find(id);
            if (turnering == null)
            {
                return HttpNotFound();
            }
            return View(turnering);
        }

        // POST: Turnering/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TurneringId,Navn,MaxAntalHold")] Turnering turnering)
        {
            if (ModelState.IsValid)
            {
                db.Entry(turnering).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(turnering);
        }

        // GET: Turnering/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turnering turnering = db.Turneringer.Find(id);
            if (turnering == null)
            {
                return HttpNotFound();
            }
            return View(turnering);
        }

        // POST: Turnering/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Turnering turnering = db.Turneringer.Find(id);
            db.Turneringer.Remove(turnering);
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
