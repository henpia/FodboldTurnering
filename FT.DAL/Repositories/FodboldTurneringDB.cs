using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using FT.Entities.Models;

namespace FT.DAL.Repositories
{
    public class FodboldTurneringDB : DbContext
    {
        public DbSet<Turnering> Turneringer { get; set; }
        public DbSet<Hold> Hold { get; set; }
        public DbSet<Kamp> Kampe { get; set; }
        public DbSet<Runde> Runder { get; set; }
    }
}