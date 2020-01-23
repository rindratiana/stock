using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace stock.Controllers.Stock
{
    public class StockController : Controller
    {
        // GET: Stock
        public ActionResult Index()
        {
            ViewBag.date = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.titre = "Commande en cours";
            return View("Accueil_stock");
        }
        public ActionResult Import()
        {
            ViewBag.date = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.titre = "Import";
            return View("Import");
        }
    }
}