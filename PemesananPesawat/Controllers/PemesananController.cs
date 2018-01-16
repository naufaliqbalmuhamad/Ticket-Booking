using PemesananPesawat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PemesananPesawat.Controllers
{
    public class PemesananController : Controller
    {
        private OperationDataContext context;
        public PemesananController()
        {
            context = new OperationDataContext();
        }
        
        // GET: Pemesanan
        public ActionResult Index()
        {
            IList<PemesananModel> pemesananList = new List<PemesananModel>();
            var query = from Pemesanan in context.Pemesanans
                        join Maskapai in context.Maskapais
                        on Pemesanan.MaskapaiId equals Maskapai.Id
                        join Rute in context.Rutes
                        on Pemesanan.RuteId equals Rute.Id
                        select new PemesananModel()
                        {
                            Id = Pemesanan.Id,
                            NamaPemesan = Pemesanan.NamaPemesan,
                            TanggalPemesanan = Pemesanan.TanggalPemesanan,
                            RuteId = Rute.Id,
                            MaskapaiId = Rute.MaskapaiId,
                            Keberangkatan = Rute.Keberangkatan,
                            Kedatangan = Rute.Kedatangan,
                            NomorPenerbangan = Rute.NomorPenerbangan
                        };

            pemesananList = query.ToList();
            return View(pemesananList);
        }

        public ActionResult Create()
        {
            PemesananModel model = new PemesananModel();
            //PreparePublisher(model);
            return View(model);
        }

        //private void PreparePublisher(PemesananModel model)
        //{
        //    model.Pemesanans = context.Rutes.AsQueryable<Rute>().Select
        //        (
        //            x => new SelectListItem()
        //            {
        //                Text = x.NamaMaskapai,
        //                Value = x.Id.ToString()
        //            }
        //        );
        //}
       
        [HttpPost]
        public ActionResult Create(PemesananModel model)
        {
            try
            {
                Pemesanan pemesanan = new Pemesanan()
                {
                    NamaPemesan = model.NamaPemesan,
                    TanggalPemesanan = model.TanggalPemesanan,
                    MaskapaiId = model.MaskapaiId,
                    RuteId = model.RuteId
                };

                context.Pemesanans.InsertOnSubmit(pemesanan);
                context.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult Delete(int Id)
        {
            PemesananModel model = context.Pemesanans.Where(c => c.Id == Id).Select(
                c => new PemesananModel()
                {
                    Id = c.Id,
                    NamaPemesan = c.NamaPemesan,
                    TanggalPemesanan = c.TanggalPemesanan,
                    RuteId = c.Id,
                    MaskapaiId = c.MaskapaiId,
                }).SingleOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(PemesananModel model)
        {
            Pemesanan pemesanan = context.Pemesanans.Where(e => e.Id == model.Id).
                SingleOrDefault();

            context.Pemesanans.DeleteOnSubmit(pemesanan);
            context.SubmitChanges();

            return RedirectToAction("Index");
        }
    }
}