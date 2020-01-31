using stock.Models.Classe.Stock;
using stock.Models.DAO;
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
        [HttpPost]
        public JsonResult AutoComplete(string numerocomplete)
        {
            try
            {
                AccesSageDAO acces = new AccesSageDAO();
                List<string> liste = acces.GetNumeroPiece(numerocomplete);
                return Json(liste);
            }
            catch (Exception exception)
            {
                return Json(exception.Message);
            }
        }
        public ActionResult Commande()
        {
            try
            {
                string date = Request.Form["date"];
                string numero = Request.Form["numero"];
                string comptoir = Request.Form["comptoir"];
                string client = Request.Form["client"];
                AccesSageDAO acces = new AccesSageDAO();
                List<Article> liste = acces.GetArticlesCommandes(numero);
                ViewData["articles"] = liste;
                ViewBag.date = DateTime.Now.ToString("yyyy-MM-dd");
                return View("Accueil_vente");
            }
            catch(Exception exception)
            {
                ViewBag.erreur = exception.Message;
                return View("Accueil_vente");
            }
        }
    }
}