using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PemesananPesawat.Models
{
    public class RuteModel
    {
        public RuteModel()
        {
            Maskapais = new List<SelectListItem>();
        }

        public int Id { get; set; }
        public string Keberangkatan { get; set; }
        public string Kedatangan { get; set; }
        public string NomorPenerbangan { get; set; }
        [DisplayName("Maskapai")]
        public int MaskapaiId { get; set; }
        public string NamaMaskapai { get; set; }
        public IEnumerable<SelectListItem> Maskapais { get; set; }
    }
}