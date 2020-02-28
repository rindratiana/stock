using stock.Models.Classe;
using stock.Models.Classe.Stock;
using stock.Models.DAO;
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
        public JsonResult GetListeCommande(string dateDebut, string dateFin)
        {
            try
            {
                Commande commande = new Commande();
                List<Commande> liste = commande.GetListeCommande(dateDebut, dateFin);
                return Json(liste);
            }
            catch (Exception exception)
            {
                return Json(exception.Message);
            }
        }
        [HttpPost]
        public JsonResult GetListeCommandeAnnule(string dateDebut, string dateFin)
        {
            try
            {
                Commande commande = new Commande();
                List<Commande> liste = commande.GetListeCommandeAnnule(dateDebut, dateFin);
                return Json(liste);
            }
            catch (Exception exception)
            {
                return Json(exception.Message);
            }
        }
        [HttpPost]
        public JsonResult GetStatCommandesMouvement(string dateDebut, string dateFin)
        {
            try
            {
                StatCommande statDuree = new StatCommande();
                StatCommande reponse = statDuree.GetStatCommandesMouvement(dateDebut, dateFin);
                return Json(reponse);
            }
            catch (Exception exception)
            {
                return Json(exception.Message);
            }
        }
        [HttpPost]
        public JsonResult GetStatCommandesAnnule(string dateDebut, string dateFin)
        {
            try
            {
                StatCommande statDuree = new StatCommande();
                StatCommande reponse = statDuree.GetStatCommandesAnnule(dateDebut,dateFin);
                return Json(reponse);
            }
            catch (Exception exception)
            {
                return Json(exception.Message);
            }
        }
        [HttpPost]
        public JsonResult GetStatDuree(string dateDebut, string dateFin)
        {
            try
            {
                StatDuree statDuree = new StatDuree();
                double reponse = statDuree.GetStatDuree(dateDebut, dateFin);
                //conversion en milliseconde
                reponse = reponse * 1000;
                return Json(reponse);
            }
            catch (Exception exception)
            {
                return Json(exception.Message);
            }
        }
        //GetStatistiquesByEmplacement
        [HttpPost]
        public JsonResult GetStatistiquesByEmplacement(string dateDebut, string dateFin,string numeroEmplacements)
        {
            try
            {
                StatArticles stat = new StatArticles();
                List<StatArticles> liste = stat.GetStatArticles(dateDebut, dateFin,numeroEmplacements);
                return Json(liste);
            }
            catch (Exception exception)
            {
                return Json(exception.Message);
            }
        }
        [HttpPost]
        public JsonResult GetStatistiquesEntreDeuxDates(string dateDebut,string dateFin)
        {
            try
            {
                StatEmplacement stat = new StatEmplacement();
                List<StatEmplacement> liste = stat.GetStatEmplacementsEntreDeuxDates(dateDebut,dateFin);
                return Json(liste);
            }
            catch (Exception exception)
            {
                return Json(exception.Message);
            }
        }
        [HttpPost]
        public JsonResult GetStatistiques()
        {
            try
            {
                StatEmplacement stat = new StatEmplacement();
                List<StatEmplacement> liste = stat.GetStatEmplacements();
                return Json(liste);
            }
            catch (Exception exception)
            {
                return Json(exception.Message);
            }
        }
        [HttpPost]
        public JsonResult AutoCompleteBinome(string nom)
        {
            try
            {
                UtilisateurDAO utilisateurDAO = new UtilisateurDAO();
                List<string> liste = utilisateurDAO.GetNomBinome(nom);
                return Json(liste);
            }
            catch (Exception exception)
            {
                return Json(exception.Message);
            }
        }
        [HttpPost]
        public JsonResult Sortie(string numero_ticket,string id_magasinier,string id_binome)
        {
            try
            {
                Commande commande = new Commande();
                commande.Sortie(numero_ticket,id_magasinier,id_binome);
                return Json("Sortie avec succès");
            }
            catch (Exception exception)
            {
                return Json(exception.Message);
            }
        }

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
            try
            {
                if (HttpContext.Session["utilisateur"] == null)
                {
                    ViewBag.erreur = "Veuillez vous connecter d'abord";
                    return View("Login");
                }
                else
                {
                    Utilisateur utilisateur = HttpContext.Session["utilisateur"] as Utilisateur;
                    if (utilisateur.Poste.IdPoste == "2") { 
                        ViewBag.date = DateTime.Now.ToString("yyyy-MM-dd");
                        Commande commande = new Commande();
                        List<Commande> commandeEncours = commande.GetListeToutCommandeEnCours();
                        ViewData["commandeEnCours"] = commandeEncours;
                        string titre = "";
                        if (commandeEncours.Count == 0)
                        {
                            titre = "Attente commande";
                        }
                        else
                        {
                            titre = "Commande en cours";
                        }
                        ViewBag.titre = titre;
                        ViewBag.espaceStock = "ok";
                        ViewBag.userName = utilisateur.Prenoms;
                        return View("Accueil_stock");
                    }
                    else
                    {
                        ViewBag.erreur = "Veuillez vous connecter en tant qu'utilisateur Magasinier";
                        return View("Login");
                    }
                }
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        [HttpPost]
        public JsonResult GetNotifications()
        {
            CommandeDAO commandeDAO = new CommandeDAO();
            string reponse = commandeDAO.getDernierNumeroTicket();
            return Json(reponse);
        }
        public ActionResult Statistiques()
        {
            try
            {
                if (HttpContext.Session["utilisateur"] == null)
                {
                    ViewBag.erreur = "Veuillez vous connecter d'abord";
                    return View("Login");
                }
                else
                {
                    Utilisateur utilisateur = HttpContext.Session["utilisateur"] as Utilisateur;
                    if (utilisateur.Poste.IdPoste == "2")
                    {
                        ViewBag.date = DateTime.Now.ToString("yyyy-MM-dd");
                        ViewBag.userName = utilisateur.Prenoms;
                        ViewBag.espaceStock = "ok";
                        ViewBag.titre = "Statistiques";
                        return View("Statistiques");
                    }
                    else
                    {
                        ViewBag.erreur = "Veuillez vous connecter en tant qu'utilisateur Magasinier";
                        return View("Login");
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
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