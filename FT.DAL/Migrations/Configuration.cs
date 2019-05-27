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
                    Navn = "Champions",
                    MaxAntalHold = 4,
                    AabenForTilmelding = true
                },
                new Turnering
                {
                    TurneringId = 2,
                    Navn = "The Greatest",
                    MaxAntalHold = 5,
                    AabenForTilmelding = true
                },
                new Turnering
                {
                    TurneringId = 3,
                    Navn = "League of Excellence",
                    MaxAntalHold = 6,
                    AabenForTilmelding = true
                },
                new Turnering
                {
                    TurneringId = 4,
                    Navn = "Final Round",
                    MaxAntalHold = 7,
                    AabenForTilmelding = true
                },
                new Turnering
                {
                    TurneringId = 5,
                    Navn = "The Golden League",
                    MaxAntalHold = 8,
                    AabenForTilmelding = true
                }
            );
            context.SaveChanges();

            context.HoldListe.AddOrUpdate(
                new Hold
                {
                    HoldId = 1,
                    Navn = "Barcelona"
                },
                new Hold
                {
                    HoldId = 2,
                    Navn = "Real Madrid"
                },
                new Hold
                {
                    HoldId = 3,
                    Navn = "Juventus"
                },
                new Hold
                {
                    HoldId = 4,
                    Navn = "Milan"
                },
                new Hold
                {
                    HoldId = 5,
                    Navn = "Inter"
                },
                new Hold
                {
                    HoldId = 6,
                    Navn = "Bayern Munchen"
                },
                new Hold
                {
                    HoldId = 7,
                    Navn = "Liverpool"
                },
                new Hold
                {
                    HoldId = 8,
                    Navn = "Manchester United"
                }
            );

           
        }
    }
}
