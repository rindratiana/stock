using stock.Models.Classe;
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
        public JsonResult Annulation(string id_commande)
        {
            try
            {
                Commande commande = new Commande();
                commande.Annuler(id_commande);
                string message = "Annulation de la commande avec succès";
                return Json(message);
            }
            catch (Exception exception)
            {
                return Json(exception.Message);
            }
        }
        [HttpPost]
        public JsonResult Reception(string id_commande)
        {
            try
            {
                Commande commande = new Commande();
                commande.Valider(id_commande);
                return Json("Clôture du ticket fait avec succès");
            }
            catch (Exception exception)
            {
                return Json(exception.Message);
            }
        }
        [HttpPost]
        public JsonResult GetListeCommandeStock(string numerocomplete)
        {
            try
            {
                Commande commande = new Commande();
                List<DetailCommande> liste = commande.GetArticlesCommandesStock(numerocomplete);
                return Json(liste);
            }
            catch (Exception exception)
            {
                return Json(exception.Message);
            }
        }
        [HttpPost]
        public JsonResult GetListeCommande(string numerocomplete)
        {
            try
            {
                AccesSageDAO acces = new AccesSageDAO();
                List<DetailCommande> liste = acces.GetArticlesCommandes(numerocomplete);
                return Json(liste);
            }
            catch (Exception exception)
            {
                return Json(exception.Message);
            }
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
        public JsonResult TestExistence(string num_ticket)
        {
            try
            {
                Commande commande = new Commande();
                Boolean reponse = commande.TestExistence(num_ticket);
                return Json(reponse);
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
                int taille = Int32.Parse(Request.Form["taille"]);
                string numero_ticket = Request.Form["numero_ticket"];
                string client = Request.Form["client"];
                List<DetailCommande> listeCommande = new List<DetailCommande>();
                
                for(int i = 0; i < taille; i++)
                {
                    if(Request.Form["article_checked" + i + ""] == "on") { 
                        DetailCommande commandeTemp = new DetailCommande();
                        Article articleTemp = new Article();
                        articleTemp.References = Request.Form["ref_article"+ i +""];
                        commandeTemp.Article = articleTemp;
                        commandeTemp.Quantite = Int32.Parse(Request.Form["quantite"+i+""]);
                        commandeTemp.Emplacement = Int32.Parse(Request.Form["emplacement" + i + ""]);
                        listeCommande.Add(commandeTemp);
                    }
                }

                Commande commande = new Commande();
                commande.Client = commande.ParseInt(client,"Veuillez choisir un client");
                commande.CreateCommande(listeCommande,numero_ticket);

                AccesSageDAO accesSageDAO = new AccesSageDAO();
                Comptoir comptoir = accesSageDAO.GetComptoirByNumTicket(numero_ticket);
                List<Commande> listeCommandeEnCours = commande.GetListeCommandeEnCours(comptoir);
                ViewData["listeCommandeEnCours"] = listeCommandeEnCours;
                ViewBag.message = "Commande envoyé";
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