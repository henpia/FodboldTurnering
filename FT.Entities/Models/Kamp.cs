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
        public int TurneringsRundeId { get; set; }
        public TurneringsRunde Runde { get; set; }
        public ICollection<Hold> HoldListe { get; set; }
        public string ScoreHjemmeHold { get; set; }
        public string ScoreUdeHold { get; set; }
    }
}