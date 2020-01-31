using stock.Models.Classe.Stock;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace stock.Controllers.Stock
{
    public class StockController : Controller
    {
        [HttpPost]
        public ActionResult ImportBase(HttpPostedFileBase files)
        {
            try
            {
                Article article = new Article();
                List<Article> liste = article.GetListeArticleExcel(files);
                //List<Article> liste = article.GetListeArticleExcel2();
                ViewBag.message = liste.Count;
                ViewData["liste"] = liste;
                return View("Import");
            }
            catch(Exception exception)
            {
                ViewBag.message = exception.Message;
                return View("Import");
            }
        }
        // GET: Stock
        public ActionResult Index()
        {
            ViewBag.date = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.titre = "Commande en cours";
            return View("Accueil_stock");
        }
        public ActionResult Statistiques()
        {
            ViewBag.date = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.titre = "Statistiques";
            return View("Statistiques");
        }
        public ActionResult Import()
        {
            DateTime date1 = new DateTime();
            ViewBag.date = DateTime.Now.ToString("D", CultureInfo.CreateSpecificCulture("fr"));
            ViewBag.titre = "Import";
            return View("Import");
        }
    }
}