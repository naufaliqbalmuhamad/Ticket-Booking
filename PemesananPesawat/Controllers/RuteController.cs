using PemesananPesawat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PemesananPesawat.Controllers
{
    public class RuteController : Controller
    {
        private OperationDataContext context;
        public RuteController()
        {
            context = new OperationDataContext();
        }

        // GET: Rute
        public ActionResult Index()
        {
            IList<RuteModel> ruteList = new List<RuteModel>();
            var query = from Rute in context.Rutes
                        join Maskapai in context.Maskapais
                        on Rute.MaskapaiId equals Maskapai.Id
                        select new RuteModel()
                        {
                            Id = Rute.Id,
                            NamaMaskapai = Maskapai.NamaMaskapai,
                            Keberangkatan = Rute.Keberangkatan,
                            Kedatangan = Rute.Kedatangan,
                            NomorPenerbangan = Rute.NomorPenerbangan
                        };

            ruteList = query.ToList();
            return View(ruteList);
        }

        public ActionResult Create()
        {
            RuteModel model = new RuteModel();
            PreparePublisher(model);
            return View(model);
        }

        private void PreparePublisher(RuteModel model)
        {
            model.Maskapais = context.Maskapais.AsQueryable<Maskapai>().Select
                (
                    x => new SelectListItem()
                    {
                        Text = x.NamaMaskapai,
                        Value = x.Id.ToString()
                    }
                );
        }

        [HttpPost]
        public ActionResult Create(RuteModel model)
        {
            try
            {
                Rute rute = new Rute()
                {
                    MaskapaiId = model.MaskapaiId,
                    Keberangkatan = model.Keberangkatan,
                    Kedatangan = model.Kedatangan,
                    NomorPenerbangan = model.NomorPenerbangan
                };

                context.Rutes.InsertOnSubmit(rute);
                context.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult Edit(int Id)
        {
            RuteModel model = context.Rutes.Where(c => c.Id == Id).Select(
                c => new RuteModel()
                {
                    Id = c.Id,
                    MaskapaiId = c.MaskapaiId,
                    Keberangkatan = c.Keberangkatan,
                    Kedatangan = c.Kedatangan,
                    NomorPenerbangan = c.NomorPenerbangan
                }).SingleOrDefault();
            PreparePublisher(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(RuteModel model)
        {
            Rute rute = context.Rutes.Where(e => e.Id == model.Id).
                SingleOrDefault();

            rute.MaskapaiId = model.MaskapaiId;
            rute.Keberangkatan = model.Keberangkatan;
            rute.Kedatangan = model.Kedatangan;
            rute.NomorPenerbangan = model.NomorPenerbangan;

            context.SubmitChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int Id)
        {
            RuteModel model = context.Rutes.Where(c => c.Id == Id).Select(
                c => new RuteModel()
                {
                    Id = c.Id,
                    NamaMaskapai = c.Maskapai.NamaMaskapai,
                    Keberangkatan = c.Keberangkatan,
                    Kedatangan = c.Kedatangan,
                    NomorPenerbangan = c.NomorPenerbangan
                }).SingleOrDefault();
            PreparePublisher(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(RuteModel model)
        {
            Rute rute = context.Rutes.Where(e => e.Id == model.Id).
                SingleOrDefault();

            context.Rutes.DeleteOnSubmit(rute);
            context.SubmitChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Details(int Id)
        {
            RuteModel model = context.Rutes.Where(c => c.Id == Id).Select(
                c => new RuteModel()
                {
                    NamaMaskapai = c.Maskapai.NamaMaskapai,
                    Keberangkatan = c.Keberangkatan,
                    Kedatangan = c.Kedatangan,
                    NomorPenerbangan = c.NomorPenerbangan
                }).SingleOrDefault();

            PreparePublisher(model);
            return View(model);

        }
    }
}