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
using FT.Entities.ViewModels;
using Microsoft.Ajax.Utilities;

namespace FT.WEB.Controllers
{
    public class TurneringController : Controller
    {
        // REPOSITORY !!!
        private FodboldTurneringDB db = new FodboldTurneringDB();

        // GET: Turnering
        public ActionResult Index()
        {
            // REPOSITORY !!!
            return View(db.Turneringer.ToList());
        }

        // GET: Turnering/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // REPOSITORY !!!
            Turnering turnering = db.Turneringer.Include(h => h.HoldListe).Where(d => d.TurneringId == id).First();
            if (turnering == null)
            {
                return HttpNotFound();
            }

            TurneringDetailsViewModel viewModel = OpbygTurneringDetailsViewModel(turnering);
            return View(viewModel);
        }

        public void StartTurnering(int? turneringsId)
        {
            // REPOSITORY !!!
            var turnering = db.Turneringer.Include(h => h.HoldListe).Where(t => t.TurneringId == turneringsId).First();

            LukForTilmeldinger(turnering);

            UdregnKampprogram(turnering);

            // vis kampprogram for turnering
        }

        public void LukForTilmeldinger(Turnering turnering)
        {
            turnering.AabenForTilmelding = false;
            // REPOSITORY !!!
            db.SaveChanges();
        }

        private void UdregnKampprogram(Turnering turnering)
        {
            var antalTurneringsRunder = turnering.HoldListe.Count - 1;
            var antalHold = turnering.HoldListe.Count;

            Hold[] holdListe = turnering.HoldListe.ToArray();
            List<Hold> hjemmeHold = new List<Hold>();
            List<Hold> udeHold = new List<Hold>();

            for (int i = 0; i < antalHold; i += 2)
            {
                hjemmeHold.Add(holdListe[i]);
                udeHold.Add(holdListe[i + 1]);
            }

            udeHold.Reverse();

            var antalKampe = antalHold / 2;

            for (int j = 1; j < antalTurneringsRunder; j++)
            {
                List<Kamp> TurneringsRundeKampe = new List<Kamp>();
                for (int i = 0; i < antalKampe; i++) // For hver runde
                {
                    // Opbyg kamp
                    Kamp kamp = new Kamp
                    {
                        TurneringsRundeId = j,
                        HoldListe = new List<Hold>
                        {
                            hjemmeHold[i],
                            udeHold[i]
                        },
                        ScoreHjemmeHold = "",
                        ScoreUdeHold = ""
                    };
                    TurneringsRundeKampe.Add(kamp);

                    // variabel = 1ste udehold
                    // fjerne det 1ste udehold
                    // indsætte variabel på 2. hjemmeholds plads
                    Hold foersteUdeHold = udeHold[0];
                    udeHold.Remove(udeHold[0]);

                    // variabel = sidste hjemmehold
                    // fjerne det sidste hjemmehold
                    // indsætte variabel på sidste udeholds plads

                } 
            }


            foreach (var runde in TurneringsRundeKampe)
            {
                // løkke med antal kampe. antal kampe = antalhold / 2
                // lav kamp: hjemmeHold[i] mod udeHold[i].
                // tilføj kamp til kampProgram. inclusive runde

                // ryk hold rundt i hjemmeHold og udeHold
            }

            //hjemmeHold.Add(holdListe[0]);
            //hjemmeHold.Add(holdListe[1]);
            //Hold firstHold = hjemmeHold[0];
            //hjemmeHold.Remove(firstHold);

            //var holdQueue = new Queue<Hold>();
            //foreach (var hold in holdListe)
            //{
            //    holdQueue.Enqueue(hold);
            //}

            //var kampProgram = new List<Kamp>();
            //for (int i = 0; i < antalTurneringsRunder; i++)
            //{
            //    for (int j = 0; j < antalHold; j++)
            //    {
            //        var hjemmeHold = holdQueue.Dequeue();
            //        var udeHold = holdQueue.Dequeue();
            //        kampProgram.Add(
            //                new Kamp
            //                {
            //                    TurneringsRundeId = i,
            //                    HoldListe = new List<Hold>()
            //                    {
            //                        hjemmeHold,
            //                        udeHold
            //                    },
            //                    ScoreHjemmeHold = "",
            //                    ScoreUdeHold = ""
            //                }
            //        );
            //        holdQueue.Enqueue(hjemmeHold);
            //        holdQueue.Enqueue(udeHold);
            //    }
            //}
            //// Der skal tages højde for lige såvel som ulige antal tilmeldte hold

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
                // REPOSITORY !!!
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
            // REPOSITORY !!!
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

        public ActionResult TilmeldHold(int turneringsId, int holdId) // ActionResult
        {
            // REPOSITORY !!!
            Turnering turnering = db.Turneringer.Include(h => h.HoldListe).Where(t => t.TurneringId == turneringsId).First();
            if (turnering.HoldListe.Count >= turnering.MaxAntalHold)
            {
                return RedirectToAction("Index");
            }

            // REPOSITORY !!!
            Hold holdAtTilmelde = db.HoldListe.Find(holdId);
            turnering.HoldListe.Add(holdAtTilmelde);
            db.SaveChanges();

            TurneringDetailsViewModel viewModel = OpbygTurneringDetailsViewModel(turnering);
            return View("Details", viewModel);
        }

        public ActionResult AfmeldHold(int turneringsId, int holdId)
        {
            // REPOSITORY !!!
            Turnering turnering = db.Turneringer.Include(h => h.HoldListe).Where(t => t.TurneringId == turneringsId).First();
            Hold holdAtFjerne = db.HoldListe.Find(holdId);

            turnering.HoldListe.Remove(holdAtFjerne);
            db.SaveChanges();

            TurneringDetailsViewModel viewModel = OpbygTurneringDetailsViewModel(turnering);
            return View("Details", viewModel);
        }

        private TurneringDetailsViewModel OpbygTurneringDetailsViewModel(Turnering turnering)
        {
            List<Hold> tilmeldteHold = turnering.HoldListe.ToList();
            List<Hold> alleHold = db.HoldListe.ToList();
            List<Hold> ikkeTilmeldteHold = alleHold.Except(tilmeldteHold).ToList();

            var viewModel = new TurneringDetailsViewModel()
            {
                Turnering = turnering,
                IkkeTilmeldteHold = ikkeTilmeldteHold
            };
            return viewModel;
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
