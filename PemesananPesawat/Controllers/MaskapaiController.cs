using PemesananPesawat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PemesananPesawat.Controllers
{
    public class MaskapaiController : Controller
    {
        private OperationDataContext context;
        public MaskapaiController()
        {
            context = new OperationDataContext();
        }
        
        // GET: Maskapai
        public ActionResult Index()
        {
            IList<MaskapaiModel> maskapaiList = new List<MaskapaiModel>();
            var query = from Maskapai in context.Maskapais select Maskapai;

            var maskapais = query.ToList();
            foreach (var maskapaiItem in maskapais)
            {
                maskapaiList.Add(new MaskapaiModel()
                {
                    Id = maskapaiItem.Id,
                    NamaMaskapai = maskapaiItem.NamaMaskapai
                });
            }

            return View(maskapaiList);
        }

        [HttpPost]
        public ActionResult Create(MaskapaiModel model)
        {
            try
            {
                Maskapai maskapai = new Maskapai()
                {
                    NamaMaskapai = model.NamaMaskapai
                };

                context.Maskapais.InsertOnSubmit(maskapai);
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