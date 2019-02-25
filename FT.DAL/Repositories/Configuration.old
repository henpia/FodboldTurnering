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
            context.Turneringer.AddOrUpdate(
                new Turnering
                {
                    TurneringId = 1,
                    Navn = "Dalby Open",
                    MaxAntalHold = 4    
                },
                new Turnering
                {
                    TurneringId = 2,
                    Navn = "Dalby Championship",
                    MaxAntalHold = 6
                },
                new Turnering
                {
                    TurneringId = 3,
                    Navn = "Serie 1",
                    MaxAntalHold = 18
                },
                new Turnering
                {
                    TurneringId = 4,
                    Navn = "Danmarksserien",
                    MaxAntalHold = 14
                }
            );
            context.SaveChanges();

            context.HoldListe.AddOrUpdate(
                new Hold
                {
                    HoldId = 1,
                    Navn = "Dalby",
                    Turneringer = new List<Turnering>()
                    {
                        context.Turneringer.Single(t => t.TurneringId == 1),
                        context.Turneringer.Single(t => t.TurneringId == 2)
                    }
                },
                new Hold
                {
                    HoldId = 2,
                    Navn = "Haslev",
                    Turneringer = new List<Turnering>()
                    {
                        context.Turneringer.Single(t => t.TurneringId == 2)
                    }
                },
                new Hold
                {
                    HoldId = 3,
                    Navn = "Næstved",
                    Turneringer = new List<Turnering>()
                    {
                        context.Turneringer.Single(t => t.TurneringId == 4)
                    }
                },
                new Hold
                {
                    HoldId = 4,
                    Navn = "Herfølge",
                    Turneringer = new List<Turnering>()
                    {
                        context.Turneringer.Single(t => t.TurneringId == 1),
                        context.Turneringer.Single(t => t.TurneringId == 2),
                        context.Turneringer.Single(t => t.TurneringId == 3),
                        context.Turneringer.Single(t => t.TurneringId == 4)
                    }
                },
                new Hold
                {
                    HoldId = 5,
                    Navn = "Køge",
                    Turneringer = new List<Turnering>()
                    {
                        context.Turneringer.Single(t => t.TurneringId == 2),
                        context.Turneringer.Single(t => t.TurneringId == 3),
                    }
                }
            );

            //context.HoldListe.AddOrUpdate(
            //    new Hold
            //    {
            //        HoldId = 1,
            //        Navn = "Dalby",
            //        Turneringer = new List<Turnering>()
            //        {
            //            context.Turneringer.Single(t => t.TurneringId == 1),
            //            context.Turneringer.Single(t => t.TurneringId == 3)
            //        }
            //    },
            //    new Hold
            //    {
            //        HoldId = 2,
            //        Navn = "Rønnede",
            //        Turneringer = new List<Turnering>()
            //        {
            //            context.Turneringer.Single(t => t.TurneringId == 1)
            //        }
            //    },
            //    new Hold
            //    {
            //        HoldId = 3,
            //        Navn = "Faxe",
            //        Turneringer = new List<Turnering>()
            //        {
            //            context.Turneringer.Single(t => t.TurneringId == 1)
            //        }
            //    },
            //    new Hold
            //    {
            //        HoldId = 4,
            //        Navn = "Haslev",
            //        Turneringer = new List<Turnering>()
            //        {
            //            context.Turneringer.Single(t => t.TurneringId == 1)
            //        }
            //    }
            //);
            //context.SaveChanges();

            //context.Runder.AddOrUpdate(
            //    new Runde
            //    {
            //        RundeId = 1,
            //        TurneringId = 1,
            //        Betegnelse = "1ste Runde"
            //    },
            //    new Runde
            //    {
            //        RundeId = 2,
            //        TurneringId = 1,
            //        Betegnelse = "2. Runde"
            //    }
            //);
            //context.SaveChanges();

            //context.Kampe.AddOrUpdate(
            //    new Kamp
            //    {
            //        KampId = 1,
            //        HoldListe = new List<Hold>()
            //        {
            //            context.HoldListe.Single(h => h.HoldId == 1),
            //            context.HoldListe.Single(h => h.HoldId == 2)
            //        },
            //        RundeId = 1
            //    },
            //    new Kamp
            //    {
            //        KampId = 2,
            //        HoldListe = new List<Hold>()
            //        {
            //            context.HoldListe.Single(h => h.HoldId == 3),
            //            context.HoldListe.Single(h => h.HoldId == 4)
            //        },
            //        RundeId = 1
            //    }
            //);
            //context.SaveChanges();
        }
    }
}
