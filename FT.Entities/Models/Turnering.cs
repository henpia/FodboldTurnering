using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FT.Entities.Models
{
    [Table("Turnering")]
    public class Turnering
    {
        public int TurneringId { get; set; }
        public string Navn { get; set; }
        public ICollection<Hold> HoldListe { get; set; }
        public ICollection<Runde> Runder { get; set; }
    }
}