using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FT.Entities.Models
{
    public class Hold
    {
        public int HoldId { get; set; }
        public string Navn { get; set; }
        public DateTime DatoForTilmelding { get; set; }
        public int TurneringsId { get; set; }
        public Turnering Turnering { get; set; }
        public ICollection<Kamp> Kampe { get; set; }

    }
}