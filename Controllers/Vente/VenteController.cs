using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace stock.Controllers.Vente
{
    public class VenteController : Controller
    {
        // GET: Vente
        public ActionResult Index()
        {
            ViewBag.date = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.titre = "Commande";
            return View("Accueil_vente");
        }
    }
}