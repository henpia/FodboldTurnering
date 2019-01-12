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
    public class KampController : Controller
    {
        private FodboldTurneringDB db = new FodboldTurneringDB();

        // GET: Kamp
        public ActionResult Index()
        {
            var kampe = db.Kampe.Include(k => k.Runde);
            return View(kampe.ToList());
        }

        // GET: Kamp/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kamp kamp = db.Kampe.Find(id);
            if (kamp == null)
            {
                return HttpNotFound();
            }
            return View(kamp);
        }

        // GET: Kamp/Create
        public ActionResult Create()
        {
            ViewBag.RundeId = new SelectList(db.Runder, "RundeId", "Betegnelse");
            return View();
        }

        // POST: Kamp/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KampId,DatoForKamp,Resultat,RundeId")] Kamp kamp)
        {
            if (ModelState.IsValid)
            {
                db.Kampe.Add(kamp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RundeId = new SelectList(db.Runder, "RundeId", "Betegnelse", kamp.RundeId);
            return View(kamp);
        }

        // GET: Kamp/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kamp kamp = db.Kampe.Find(id);
            if (kamp == null)
            {
                return HttpNotFound();
            }
            ViewBag.RundeId = new SelectList(db.Runder, "RundeId", "Betegnelse", kamp.RundeId);
            return View(kamp);
        }

        // POST: Kamp/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KampId,DatoForKamp,Resultat,RundeId")] Kamp kamp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kamp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RundeId = new SelectList(db.Runder, "RundeId", "Betegnelse", kamp.RundeId);
            return View(kamp);
        }

        // GET: Kamp/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kamp kamp = db.Kampe.Find(id);
            if (kamp == null)
            {
                return HttpNotFound();
            }
            return View(kamp);
        }

        // POST: Kamp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kamp kamp = db.Kampe.Find(id);
            db.Kampe.Remove(kamp);
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
