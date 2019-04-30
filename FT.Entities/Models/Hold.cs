using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FT.Entities.Models
{
    [Table("HoldListe")]
    public class Hold
    {
        public int HoldId { get; set; }
        [StringLength(50,MinimumLength =2)]
        public string Navn { get; set; }
        public ICollection<Turnering> Turneringer { get; set; }
        public ICollection<Kamp> Kampe { get; set; }
    }
}