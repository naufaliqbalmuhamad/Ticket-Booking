using PemesananPesawat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PemesananPesawat.Controllers
{
    public class PilotController : Controller
    {
        private OperationDataContext context;
        public PilotController()
        {
            context = new OperationDataContext();
        }
        
        // GET: Pilot
        public ActionResult Index()
        {
            IList<PilotModel> pilotList = new List<PilotModel>();
            var query = from Pilot in context.Pilots select Pilot;

            var pilots = query.ToList();
            foreach (var pilotItem in pilots)
            {
                pilotList.Add(new PilotModel()
                {
                    Id = pilotItem.Id,
                    NamaPilot = pilotItem.NamaPilot
                });
            }

            return View(pilotList);
        }

        [HttpPost]
        public ActionResult Create(PilotModel model)
        {
            try
            {
                Pilot pilot = new Pilot()
                {
                    NamaPilot = model.NamaPilot
                };

                context.Pilots.InsertOnSubmit(pilot);
                context.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}