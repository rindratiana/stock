using stock.Models.Classe;
using stock.Models.Classe.Stock;
using stock.Models.Classe.Utilisateurs;
using stock.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace stock.Controllers.Login
{
    public class LoginController : Controller
    {
        public ActionResult Deconnecter()
        {
            try
            {
                Session.Remove("utilisateur");
                return View("Login");
            }
            catch (Exception ex)
            {
                ViewBag.erreur = ex.Message;
                return View("Login");
            }
        }
        public ActionResult Register()
        {
            Poste poste = new Poste();
            try
            {
                Utilisateur utilisateur = new Utilisateur();
                string nom = Request.Form["nom"];
                string prenoms = Request.Form["prenoms"];
                string identifiant = Request.Form["identifiant"];
                string idposte = Request.Form["poste"];
                string mdp1 = Request.Form["mdp1"];
                string mdp2 = Request.Form["mdp2"];
                utilisateur.comparaisonMdp(mdp1, mdp2);
                utilisateur.NomUtilisateur = nom;
                utilisateur.Prenoms = prenoms;
                utilisateur.Poste = poste;
                poste.IdPoste = idposte;
                utilisateur.Identifiants = identifiant;
                utilisateur.Mdp = mdp1;
                utilisateur.CreateUtilisateur();
                ViewBag.message = "Inscription fait avec succès";
                return View("Login");
            }
            catch (Exception ex)
            {
                ViewData["listePoste"] = poste.GetPostes();
                ViewBag.erreur = ex.Message;
                return View("Inscription");
            }
        }
        public ActionResult Inscription()
        {
            Poste poste = new Poste();
            try
            {
                ViewData["listePoste"] = poste.GetPostes();
                return View("Inscription");
            }
            catch (Exception ex)
            {
                return View("Inscription");
            }
        }
        // GET: Login
        public ActionResult Connecter()
        {
            try
            {
                string identifiant = Request.Form["identifiant"];
                string mdp = Request.Form["mdp"];
                LoginUsers loginUsers = new LoginUsers(identifiant,mdp);
                Utilisateur utilisateur = Utilisateur.Connecter(loginUsers);
                HttpContext.Session["utilisateur"] = utilisateur;
                Utilisateur userSession = HttpContext.Session["utilisateur"] as Utilisateur;
                string idposte = userSession.Poste.IdPoste;
                Commande commande = new Commande();
                switch (idposte)
                {
                    case "1":
                        AccesSageDAO accesSageDAO = new AccesSageDAO();
                        Comptoir comptoir = accesSageDAO.GetComptoirByNomCaisse(utilisateur.Identifiants);
                        List<Commande> listeCommandeEnCours = commande.GetListeCommandeEnCours(comptoir);
                        ViewData["listeCommandeEnCours"] = listeCommandeEnCours;
                        ViewBag.date = DateTime.Now.ToString("yyyy-MM-dd");
                        ViewBag.titre = "Commande";
                        ViewBag.espaceVente = "ok";
                        ViewBag.userName = utilisateur.Prenoms;
                        return View("Accueil_vente");
                    case "2":
                        ViewBag.date = DateTime.Now.ToString("yyyy-MM-dd");
                        List<Commande> commandeEncours = commande.GetListeToutCommandeEnCours();
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
                        ViewData["commandeEnCours"] = commandeEncours;
                        ViewBag.espaceStock = "ok";
                        ViewBag.userName = utilisateur.Prenoms;
                        return View("Accueil_stock");
                    default:
                        return View("login");
                }
            }
            catch(Exception ex)
            {
                ViewBag.erreur = ex.Message;
                return View("Login");
            }
        }
        public ActionResult Index()
        {
            return View("Login");
        }
    }
}