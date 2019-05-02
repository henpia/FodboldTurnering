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
            Turnering turnering = db.Turneringer.Include(h => h.HoldListe).Include(k => k.Kampe).Where(d => d.TurneringId == id).First();
            if (turnering == null)
            {
                return HttpNotFound();
            }

            if (turnering.HoldListe.Count >= turnering.MaxAntalHold || turnering.AabenForTilmelding == false)
            {
                return RedirectToAction("Kampprogram", new { turneringsId = turnering.TurneringId });
            }

            TurneringDetailsViewModel viewModel = OpbygTurneringDetailsViewModel(turnering);
            return View(viewModel);
        }

        public ActionResult StartTurnering(int? turneringsId)
        {
            // REPOSITORY !!!
            Turnering turnering = db.Turneringer.Include(h => h.HoldListe).Where(t => t.TurneringId == turneringsId).First();

            LukForTilmeldinger(turnering);

            UdregnKampprogram(turnering);

            GemKampprogram(turnering);

            return RedirectToAction("Kampprogram", new { turneringsId = turnering.TurneringId });
        }

        private KampprogramViewModel OpbygKampprogramViewModel(Turnering turnering)
        {
            List<Hold> holdTilmeldtTurnering = turnering.HoldListe.ToList();
            List<Kamp> kampe = turnering.Kampe.ToList();

            KampprogramViewModel viewModel = new KampprogramViewModel()
            {
                TurneringId = turnering.TurneringId,
                TurneringsNavn = turnering.Navn,
                HoldTilmeldtTurnering = holdTilmeldtTurnering,
                Kampe = kampe
            };
            
            return viewModel;
        }

        private void GemKampprogram(Turnering turnering)
        {
            db.SaveChanges();
        }

        public ActionResult Kampprogram(int? turneringsId = 0)
        {
            var turnering = new Turnering();
            if (turneringsId == 0)
            {
                // Hvis der ikke er valgt nogen turnering, så vælg den første
                turnering = db.Turneringer.Include(h => h.HoldListe).First();
                // for at finde kampe nedenfor sættes turneringsId til 
                // den første turnering, der blev fundet ovenfor
                turneringsId = turnering.TurneringId;
            } else
            {
                turnering = db.Turneringer.Include(h => h.HoldListe).Where(t => t.TurneringId == turneringsId).First();
            }
            
            ICollection<Kamp> kampe = db.Kampe.Include(h => h.HoldListe).Where(t => t.TurneringId == turneringsId).ToList();
            turnering.Kampe = kampe;
            KampprogramViewModel viewModel = OpbygKampprogramViewModel(turnering);
            return View(viewModel);
        }

        public void LukForTilmeldinger(Turnering turnering)
        {
            turnering.AabenForTilmelding = false;
            // REPOSITORY !!!
            db.SaveChanges();
        }

        private void UdregnKampprogram(Turnering turnering)
        {
            Hold[] holdListe = turnering.HoldListe.ToArray();
            int antalHold = holdListe.Count();
            int antalTurneringsRunder = BeregnAntalTurneringsRunder(antalHold);

            List<Hold> hjemmeHold, udeHold;
            FordelHjemmeOgUdehold(holdListe, antalHold, antalTurneringsRunder, out hjemmeHold, out udeHold);

            var antalKampePrRunde = antalHold / 2;
            List<Kamp> TurneringsRundeKampe = BeregnKampeForTurnering(turnering, antalTurneringsRunder, hjemmeHold, udeHold, antalKampePrRunde);
            turnering.Kampe = TurneringsRundeKampe;
        }

        private static List<Kamp> BeregnKampeForTurnering(Turnering turnering, int antalTurneringsRunder, List<Hold> hjemmeHold, List<Hold> udeHold, int antalKampePrRunde)
        {
            List<Kamp> TurneringsRundeKampe = new List<Kamp>();

            for (int j = 1; j <= antalTurneringsRunder; j++)
            {
                for (int i = 0; i < antalKampePrRunde; i++)
                {
                    Kamp kamp = new Kamp
                    {
                        TurneringId = turnering.TurneringId,
                        TurneringsRunde = j,
                        HoldListe = new List<Hold>
                        {
                            hjemmeHold[i],
                            udeHold[i]
                        },
                        ScoreHjemmeHold = "",
                        ScoreUdeHold = ""
                    };
                    TurneringsRundeKampe.Add(kamp);
                }

                OmrokerHjemmeOgUdeholdTilNaesteTurneringsRunde(hjemmeHold, udeHold);
            }

            return TurneringsRundeKampe;
        }

        private static void OmrokerHjemmeOgUdeholdTilNaesteTurneringsRunde(List<Hold> hjemmeHold, List<Hold> udeHold)
        {
            Hold foersteUdeHold = udeHold[0];
            udeHold.Remove(udeHold[0]);
            hjemmeHold.Insert(1, foersteUdeHold);

            Hold sidsteHjemmeHold = hjemmeHold[hjemmeHold.Count - 1];
            hjemmeHold.Remove(hjemmeHold[hjemmeHold.Count - 1]);
            udeHold.Insert(udeHold.Count, sidsteHjemmeHold);
        }

        private static void FordelHjemmeOgUdehold(Hold[] holdListe, int antalHold, int antalTurneringsRunder, out List<Hold> hjemmeHold, out List<Hold> udeHold)
        {
            hjemmeHold = new List<Hold>();
            udeHold = new List<Hold>();

            // fordel hjemme- og udehold
            int index = 0;
            while (index < antalTurneringsRunder)
            {
                hjemmeHold.Add(holdListe[index]);
                udeHold.Add(holdListe[index + 1]);
                index += 2;
            }
            if (antalHold % 2 != 0)
            {
                hjemmeHold.Add(holdListe[antalHold - 1]);
            }
        }

        private static int BeregnAntalTurneringsRunder(int antalHold)
        {
            int antalTurneringsRunder;
            if (antalHold % 2 != 0)
            {
                antalTurneringsRunder = antalHold;
            }
            else
            {
                antalTurneringsRunder = antalHold - 1;
            }

            return antalTurneringsRunder;
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
                turnering.AabenForTilmelding = true;
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
        public ActionResult Edit([Bind(Include = "TurneringId,Navn,MaxAntalHold,AabenForTilmelding")] Turnering turnering)
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
