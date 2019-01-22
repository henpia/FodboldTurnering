using FT.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FT.Entities.ViewModels
{
    public class TurneringDetailsViewModel
    {
        public Turnering Turnering { get; set; }
        public List<Hold> IkkeTilmeldteHold { get; set; }
    }
}