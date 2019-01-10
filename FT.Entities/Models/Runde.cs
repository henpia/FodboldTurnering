using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FT.Entities.Models
{
    [Table("Runde")]
    public class Runde
    {
        public int RundeId { get; set; }
        public string Betegnelse { get; set; }
        public ICollection<Kamp> Kampe { get; set; }
        public int TurneringsId { get; set; }
        public Turnering Turnering { get; set; }
    }
}