using PemesananPesawat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PemesananPesawat.Controllers
{
    public class PesawatController : Controller
    {
        private OperationDataContext context;
        public PesawatController()
        {
            context = new OperationDataContext();
        }

        // GET: Pesawat
        public ActionResult Index()
        {
            IList<PesawatModel> PesawatList = new List<PesawatModel>();
            var query = from Pesawat in context.Pesawats
                        join Maskapai in context.Maskapais
                        on Pesawat.MaskapaiId equals Maskapai.Id
                        join Tipe in context.Tipes
                        on Pesawat.TipeId equals Tipe.Id
                        join Pilot in context.Pilots
                        on Pesawat.PilotId equals Pilot.Id
                        select new PesawatModel()
                        {
                            Id = Pesawat.Id,
                            NamaMaskapai = Maskapai.NamaMaskapai,
                            Jadwal = Pesawat.Jadwal,
                            TipePesawat = Tipe.TipePesawat,
                            NamaPilot = Pilot.NamaPilot
                        };
            PesawatList = query.ToList();
            return View(PesawatList);
        }

        public ActionResult Create()
        {
            PesawatModel model = new PesawatModel();
            PreparePublisher(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(PesawatModel model)
        {
            Pesawat pesawat = new Pesawat()
            {
                MaskapaiId = model.MaskapaiId,
                Jadwal = model.Jadwal,
                TipeId = model.TipeId,
                PilotId = model.PilotId
            };

            context.Pesawats.InsertOnSubmit(pesawat);
            context.SubmitChanges();
            return RedirectToAction("Index");
        }

        private void PreparePublisher(PesawatModel model)
        {
            model.Maskapais = context.Maskapais.AsQueryable<Maskapai>().Select
                (
                    x => new SelectListItem()
                    {
                        Text = x.NamaMaskapai,
                        Value = x.Id.ToString()
                    }
                );
            model.Tipes = context.Tipes.AsQueryable<Tipe>().Select
                (
                    x => new SelectListItem()
                    {
                        Text = x.TipePesawat,
                        Value = x.Id.ToString()
                    }
                );
            model.Pilots = context.Pilots.AsQueryable<Pilot>().Select
                (
                    x => new SelectListItem()
                    {
                        Text = x.NamaPilot,
                        Value = x.Id.ToString()
                    }
                );
        }

        public ActionResult Edit(int Id)
        {
            PesawatModel model = context.Pesawats.Where(c => c.Id == Id).Select(
                c => new PesawatModel()
                {
                    Id = c.Id,
                    NamaMaskapai = c.Maskapai.NamaMaskapai,
                    Jadwal = c.Jadwal,
                    TipePesawat = c.Tipe.TipePesawat,
                    NamaPilot = c.Pilot.NamaPilot
                }).SingleOrDefault();
            PreparePublisher(model);
            return View(model);
        }

       
        [HttpPost]
        public ActionResult Edit(PesawatModel model)
        {
            Pesawat pesawat = context.Pesawats.Where(e => e.Id == model.Id).
                SingleOrDefault();

            pesawat.MaskapaiId = model.MaskapaiId;
            pesawat.Jadwal = model.Jadwal;
            pesawat.TipeId = model.TipeId;
            pesawat.PilotId = model.PilotId;

            context.SubmitChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int Id)
        {
            PesawatModel model = context.Pesawats.Where(c => c.Id == Id).Select(
                c => new PesawatModel()
                {
                    Id = c.Id,
                    NamaMaskapai = c.Maskapai.NamaMaskapai,
                    Jadwal = c.Jadwal,
                    TipePesawat = c.Tipe.TipePesawat,
                    NamaPilot = c.Pilot.NamaPilot
                }).SingleOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(PesawatModel model)
        {
            Pesawat pesawat = context.Pesawats.Where(e => e.Id == model.Id).
                SingleOrDefault();

            context.Pesawats.DeleteOnSubmit(pesawat);
            context.SubmitChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Details(int Id)
        {
            PesawatModel model = context.Pesawats.Where(c => c.Id == Id).Select(
                c => new PesawatModel()
                {
                    NamaMaskapai = c.Maskapai.NamaMaskapai,
                    Jadwal = c.Jadwal,
                    TipePesawat = c.Tipe.TipePesawat,
                    NamaPilot = c.Pilot.NamaPilot
                }).SingleOrDefault();

            return View(model);
        }

    }
}