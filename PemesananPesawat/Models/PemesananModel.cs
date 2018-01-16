using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PemesananPesawat.Models
{
    public class PemesananModel
    {
        public PemesananModel()
        {
            Pemesanans = new List<SelectListItem>();
        }

        public int Id { get; set; }
        public string NamaPemesan { get; set; }
        public string TanggalPemesanan { get; set; }

        public int RuteId { get; set; }
        public string NamaMaskapai { get; set; }
        public int MaskapaiId { get; set; }
        public string Keberangkatan { get; set; }
        public string Kedatangan { get; set; }
        public string NomorPenerbangan { get; set; }
        
        public IEnumerable<SelectListItem> Pemesanans { get; set; }
    }
}