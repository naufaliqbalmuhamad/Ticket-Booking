using PemesananPesawat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PemesananPesawat.Controllers
{
    public class TipeController : Controller
    {
        private OperationDataContext context;
        public TipeController()
        {
            context = new OperationDataContext();
        }
        
        // GET: Tipe
        public ActionResult Index()
        {
            IList<TipeModel> tipeList = new List<TipeModel>();
            var query = from Tipe in context.Tipes select Tipe;

            var tipes = query.ToList();
            foreach (var tipeItem in tipes)
            {
                tipeList.Add(new TipeModel()
                {
                    Id = tipeItem.Id,
                    TipePesawat = tipeItem.TipePesawat
                });
            }

            return View(tipeList);
        }

        [HttpPost]
        public ActionResult Create(TipeModel model)
        {
            try
            {
                Tipe tipe = new Tipe()
                {
                    TipePesawat = model.TipePesawat
                };

                context.Tipes.InsertOnSubmit(tipe);
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