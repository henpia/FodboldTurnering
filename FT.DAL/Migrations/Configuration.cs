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
                    Navn = "Turnering 1",
                    MaxAntalHold = 4,
                    AabenForTilmelding = true
                },
                new Turnering
                {
                    TurneringId = 2,
                    Navn = "Turnering 2",
                    MaxAntalHold = 5,
                    AabenForTilmelding = true
                },
                new Turnering
                {
                    TurneringId = 3,
                    Navn = "Turnering 3",
                    MaxAntalHold = 6,
                    AabenForTilmelding = true
                },
                new Turnering
                {
                    TurneringId = 4,
                    Navn = "Turnering 4",
                    MaxAntalHold = 7,
                    AabenForTilmelding = true
                },
                new Turnering
                {
                    TurneringId = 5,
                    Navn = "Turnering 5",
                    MaxAntalHold = 8,
                    AabenForTilmelding = true
                }
            );
            context.SaveChanges();

            context.HoldListe.AddOrUpdate(
                new Hold
                {
                    HoldId = 1,
                    Navn = "Hold 1"
                },
                new Hold
                {
                    HoldId = 2,
                    Navn = "Hold 2"
                },
                new Hold
                {
                    HoldId = 3,
                    Navn = "Hold 3"
                },
                new Hold
                {
                    HoldId = 4,
                    Navn = "Hold 4"
                },
                new Hold
                {
                    HoldId = 5,
                    Navn = "Hold 5"
                },
                new Hold
                {
                    HoldId = 6,
                    Navn = "Hold 6"
                },
                new Hold
                {
                    HoldId = 7,
                    Navn = "Hold 7"
                },
                new Hold
                {
                    HoldId = 8,
                    Navn = "Hold 8"
                }
            );

           
        }
    }
}
