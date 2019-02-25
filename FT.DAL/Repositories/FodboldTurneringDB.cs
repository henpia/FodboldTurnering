using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using FT.Entities.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace FT.DAL.Repositories
{
    public class FodboldTurneringDB : DbContext
    {
        public DbSet<Turnering> Turneringer { get; set; }
        public DbSet<Hold> HoldListe { get; set; }
        public DbSet<Kamp> Kampe { get; set; }
        public DbSet<TurneringsRunde> TurneringsRunder { get; set; }
    }
}