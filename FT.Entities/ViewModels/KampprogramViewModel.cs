using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FT.Entities.Models;

namespace FT.Entities.ViewModels
{
    public class KampprogramViewModel
    {

        public int TurneringId { get; set; }
        public string TurneringsNavn { get; set; }
        public List<Hold> HoldTilmeldtTurnering { get; set; }
        public List<Kamp> Kampe { get; set; }
    }
}