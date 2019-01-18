using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FT.Entities.Models
{
    [Table("Kampe")]
    public class Kamp
    {
        public int KampId { get; set; }
        public string Resultat { get; set; }
        public ICollection<Hold> HoldListe { get; set; }
        public int RundeId { get; set; }
        public Runde Runde { get; set; }
    }
}