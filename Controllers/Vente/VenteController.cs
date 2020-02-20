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
        
        public VenteController(){}
        // GET: Vente
        public ActionResult Index()
        {
            try {
                if (HttpContext.Session["utilisateur"] == null)
                {
                    ViewBag.erreur = "Veuillez vous connecter d'abord";
                    return View("Login");
                }
                else {
                    Utilisateur utilisateur = HttpContext.Session["utilisateur"] as Utilisateur;
                    if (utilisateur.Poste.IdPoste == "1")
                    {
                        AccesSageDAO accesSageDAO = new AccesSageDAO();
                        Commande commande = new Commande();
                        Comptoir comptoir = accesSageDAO.GetComptoirByNomCaisse(utilisateur.Identifiants);
                        List<Commande> listeCommandeEnCours = commande.GetListeCommandeEnCours(comptoir);
                        ViewData["listeCommandeEnCours"] = listeCommandeEnCours;
                        ViewBag.date = DateTime.Now.ToString("yyyy-MM-dd");
                        ViewBag.espaceVente = "ok";
                        ViewBag.titre = "Commande";
                        ViewBag.userName = utilisateur.Prenoms;
                        return View("Accueil_vente");
                    }
                    else
                    {
                        ViewBag.erreur = "Veuillez vous connecter en tant qu'utilisateur Auxiliaire de vente";
                        return View("Login");
                    }
                }
            }
            catch(Exception exception)
            {
                return View("Login");
            }
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
                Commande temp = commande.Valider(id_commande);
                return Json(temp);
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
                Utilisateur utilisateur = HttpContext.Session["utilisateur"] as Utilisateur;
                AccesSageDAO acces = new AccesSageDAO();
                List<string> liste = acces.GetNumeroPiece(numerocomplete,utilisateur);
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
                if(HttpContext.Session["utilisateur"] == null)
                {
                    ViewBag.erreur = "Veuillez vous connecter d'abord";
                    return View("Login");
                }
                else
                {
                    Utilisateur utilisateur = HttpContext.Session["utilisateur"] as Utilisateur;
                    if (utilisateur.Poste.IdPoste == "1")
                    {
                        int taille = Int32.Parse(Request.Form["taille"]);
                        string numero_ticket = Request.Form["numero_ticket"];
                        string client = Request.Form["client"];
                        List<DetailCommande> listeCommande = new List<DetailCommande>();

                        for (int i = 0; i < taille; i++)
                        {
                            if (Request.Form["article_checked" + i + ""] == "on")
                            {
                                DetailCommande commandeTemp = new DetailCommande();
                                Article articleTemp = new Article();
                                articleTemp.References = Request.Form["ref_article" + i + ""];
                                commandeTemp.Article = articleTemp;
                                commandeTemp.Quantite = Int32.Parse(Request.Form["quantite" + i + ""]);
                                commandeTemp.Emplacement = Int32.Parse(Request.Form["emplacement" + i + ""]);
                                listeCommande.Add(commandeTemp);
                            }
                        }

                        Commande commande = new Commande();
                        commande.Client = commande.ParseInt(client, "Veuillez choisir un client");
                        commande.CreateCommande(listeCommande, numero_ticket);

                        AccesSageDAO accesSageDAO = new AccesSageDAO();
                        Comptoir comptoir = accesSageDAO.GetComptoirByNomCaisse(utilisateur.Identifiants);
                        List<Commande> listeCommandeEnCours = commande.GetListeCommandeEnCours(comptoir);
                        ViewBag.espaceVente = "ok";
                        ViewData["listeCommandeEnCours"] = listeCommandeEnCours;
                        ViewBag.message = "Commande envoyé";
                        ViewBag.userName = utilisateur.Prenoms;
                        return View("Accueil_vente");
                    }
                    else
                    {
                        ViewBag.erreur = "Veuillez vous connecter en tant qu'utilisateur Auxiliaire de vente";
                        return View("Login");
                    }
                }
            }
            catch(Exception exception)
            {
                ViewBag.erreur = exception.Message;
                return View("Accueil_vente");
            }
        }
    }
}