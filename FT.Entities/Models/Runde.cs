using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FT.Entities.Models
{
    public class Runde
    {
        public int RundeId { get; set; }
        public string Betegnelse { get; set; }
        public ICollection<Kamp> Kampe { get; set; }
        public int TurneringsId { get; set; }
        public Turnering Turnering { get; set; }
    }
}