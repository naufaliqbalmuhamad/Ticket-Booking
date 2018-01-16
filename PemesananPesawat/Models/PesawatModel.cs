using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PemesananPesawat.Models
{
    public class PesawatModel
    {
        public PesawatModel()
        {
            Maskapais = new List<SelectListItem>();
            Tipes = new List<SelectListItem>();
            Pilots = new List<SelectListItem>();
        }
        public int Id { get; set; }
        public string Jadwal { get; set; }

        [DisplayName("Maskapai")]
        public int MaskapaiId { get; set; }
        public string NamaMaskapai { get; set; }
        public IEnumerable<SelectListItem> Maskapais { get; set; }

        [DisplayName("Tipe")]
        public int TipeId { get; set; }
        public string TipePesawat { get; set; }
        public IEnumerable<SelectListItem> Tipes { get; set; }

        [DisplayName("Pilot")]
        public int PilotId { get; set; }
        public string NamaPilot { get; set; }
        public IEnumerable<SelectListItem> Pilots { get; set; }
    }
}