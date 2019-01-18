namespace FT.DAL.Migrations
{
    using FT.Entities.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FT.DAL.Repositories.FodboldTurneringDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FT.DAL.Repositories.FodboldTurneringDB context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Turneringer.AddOrUpdate(new Turnering
            {
                TurneringId = 1,
                Navn = "Dalby Open",
                MaxAntalHold = 4
            });
            context.SaveChanges();

            context.HoldListe.AddOrUpdate(
                new Hold
                {
                    HoldId = 1,
                    Navn = "Dalby",
                    TurneringId = 1,
                    DatoForOprettelse = DateTime.Now
                },
                new Hold
                {
                    HoldId = 2,
                    Navn = "Rønnede",
                    TurneringId = 1,
                    DatoForOprettelse = DateTime.Now
                },
                new Hold
                {
                    HoldId = 3,
                    Navn = "Faxe",
                    TurneringId = 1,
                    DatoForOprettelse = DateTime.Now
                },
                new Hold
                {
                    HoldId = 4,
                    Navn = "Haslev",
                    TurneringId = 1,
                    DatoForOprettelse = DateTime.Now
                }
            );
            context.SaveChanges();

            context.Runder.AddOrUpdate(
                new Runde
                {
                    RundeId = 1,
                    TurneringId = 1,
                    Betegnelse = "1ste Runde"
                },
                new Runde
                {
                    RundeId = 2,
                    TurneringId = 1,
                    Betegnelse = "2. Runde"
                }
            );
            context.SaveChanges();

            context.Kampe.AddOrUpdate(
                new Kamp
                {
                    KampId = 1,
                    DatoForKamp = DateTime.Now,
                    HoldListe = new List<Hold>()
                    {
                        context.HoldListe.Single(h => h.HoldId == 1),
                        context.HoldListe.Single(h => h.HoldId == 2)
                    },
                    RundeId = 1
                },
                new Kamp
                {
                    KampId = 2,
                    DatoForKamp = DateTime.Now,
                    HoldListe = new List<Hold>()
                    {
                        context.HoldListe.Single(h => h.HoldId == 3),
                        context.HoldListe.Single(h => h.HoldId == 4)
                    },
                    RundeId = 1
                }
            );
            context.SaveChanges();
        }
    }
}
