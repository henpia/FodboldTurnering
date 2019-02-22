using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FT.Entities.Models
{
    [Table("TurneringsRunder")]
    public class TurneringsRunde
    {
        public int TurneringsRundeId { get; set; }
        public int TurneringId { get; set; }
        public Turnering Turnering { get; set; }
        public ICollection<Kamp> Kampe { get; set; }
    }
}