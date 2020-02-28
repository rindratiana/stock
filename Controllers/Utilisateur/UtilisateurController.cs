using stock.Models.DAO;
using stock.Models.Classe.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using stock.Models.Classe;
using stock.Models.Classe.Utilisateurs;

namespace stock.Controllers.Utilisateurs
{
    public class UtilisateurController : Controller
    {
        // GET: Utilisateur
        [HttpPost]
        public JsonResult AutoComplete(string identifiant)
        {
            try
            {
                Utilisateur utilisateur = new Utilisateur();
                List<string> liste = utilisateur.GetListeIdentifiant(identifiant);
                return Json(liste);
            }
            catch (Exception exception)
            {
                return Json(exception.Message); ;
            }
        }
        public ActionResult UpdatePwd()
        {
            try
            {
                if (HttpContext.Session["utilisateur"] == null)
                {
                    return View("Login");
                }
                else
                {
                    string mdp1 = Request.Form["mdp1"];
                    string mdp2 = Request.Form["mdp2"];
                    Utilisateur utilisateur = HttpContext.Session["utilisateur"] as Utilisateur;
                    if (mdp1!= mdp2)
                    {
                        ViewBag.message_reset = "Veuillez entrer le même mot de passe";
                        return View("Accueil_vente");
                    }
                    else {
                        if (mdp1.Length <= 4 || mdp1=="")
                        {
                            ViewBag.message_reset = "Le mot de passe ne doît pas être inférieur à 4 caractères";
                            return View("Accueil_vente");
                        }
                        else
                        {
                            utilisateur.Mdp = mdp1;
                            Utilisateur.UpdateMdp(utilisateur);
                            if (utilisateur.Poste.IdPoste == "1") { 
                                ViewBag.titre = "Commande";
                                ViewBag.espaceVente = "ok";
                                ViewBag.userName = utilisateur.Prenoms;
                                return View("Accueil_vente");
                            }
                            else if(utilisateur.Poste.IdPoste == "2")
                            {
                                ViewBag.titre = "Commande";
                                ViewBag.espaceStock = "ok";
                                ViewBag.userName = utilisateur.Prenoms;
                                return View("Accueil_stock");
                            }
                            else
                            {
                                return View("Login");
                            }
                        }
                    }
                }
            }
            catch(Exception exception)
            {
                return View("Login");
            }
        }
        [HttpPost]
        public JsonResult TestEtatMdp()
        {
            try
            {
                if (HttpContext.Session["utilisateur"] == null)
                {
                    return Json("Veuillez vous connecter d'abord");
                }
                else
                {
                    Utilisateur utilisateurSession = HttpContext.Session["utilisateur"] as Utilisateur;
                    Utilisateur utilisateur = Utilisateur.GetUtilisateurById(utilisateurSession.IdUtilisateur);

                    if (utilisateur.EtatMdp == "0")
                    {
                        return Json("ko");
                    }
                    else
                    {
                        return Json("ok");
                    }
                }
            }
            catch (Exception exception)
            {
                return Json(exception.Message); ;
            }
        }
        public ActionResult ResetPwd(string id)
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
                    Utilisateur utilisateur = new Utilisateur();
                    utilisateur.ResetPwd(id);
                    ViewBag.userName = utilisateur.Prenoms;
                    ViewData["utilisateursValide"] = utilisateur.GetUtilisateurValidation("1");
                    ViewData["utilisateursNonValide"] = utilisateur.GetUtilisateurValidation("0");
                    ViewBag.message = "Réinitialisation du mot de passe avec succès, veuillez lui contacter pour son nouveau mot de passe";
                    ViewBag.titre = "Gestion des utilisateurs";
                    return View("Admin");
                }
            }
            catch (Exception exception)
            {
                return View("Login");
            }
        }
        public ActionResult DeleteUser(string id)
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
                    Utilisateur utilisateur = new Utilisateur();
                    utilisateur.DeleteRequest(id);
                    ViewBag.userName = utilisateur.Prenoms;
                    ViewData["utilisateursValide"] = utilisateur.GetUtilisateurValidation("1");
                    ViewData["utilisateursNonValide"] = utilisateur.GetUtilisateurValidation("0");
                    ViewBag.message ="Suppression d'un utilisateur avec succès";
                    ViewBag.titre = "Gestion des utilisateurs";
                    return View("Admin");
                }
            }
            catch (Exception exception)
            {
                return View("Login");
            }
        }
        public ActionResult DeleteRequest(string id)
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
                    Utilisateur utilisateur = new Utilisateur();
                    utilisateur.DeleteRequest(id);
                    ViewBag.userName = utilisateur.Prenoms;
                    ViewData["utilisateursValide"] = utilisateur.GetUtilisateurValidation("1");
                    ViewData["utilisateursNonValide"] = utilisateur.GetUtilisateurValidation("0");
                    ViewBag.message = "Suppression d'une demande avec succès";
                    ViewBag.titre = "Gestion des utilisateurs";
                    return View("Admin");
                }
            }
            catch (Exception exception)
            {
                return View("Login");
            }
        }
        public ActionResult ConfirmUser(string id)
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
                    Utilisateur utilisateur = new Utilisateur();
                    ViewBag.userName = utilisateur.Prenoms;
                    utilisateur.ConfirmUser(id);
                    ViewData["utilisateursValide"] = utilisateur.GetUtilisateurValidation("1");
                    ViewData["utilisateursNonValide"] = utilisateur.GetUtilisateurValidation("0");
                    ViewBag.message = "Confirmation d'un utilisateur avec succès";
                    ViewBag.titre = "Gestion des utilisateurs";
                    return View("Admin");
                }
            }
            catch (Exception exception)
            {
                return View("Login");
            }
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult UpdateProfil(string nom,string prenom,string mdp)
        {
            try
            {
                if (HttpContext.Session["utilisateur"] == null)
                {
                    return Json("Veuillez vous connecter d'abord");
                }
                else
                {
                    Utilisateur utilisateur = HttpContext.Session["utilisateur"] as Utilisateur;
                    utilisateur.NomUtilisateur = nom;
                    utilisateur.Prenoms = prenom;
                    utilisateur.Mdp = mdp;
                    utilisateur.UpdateProfil();
                    return Json("ok");
                }
            }
            catch(Exception exception)
            {
                return Json(exception.Message); ;
            }
        }
        [HttpPost]
        public JsonResult TestAncienMdp(string mdp)
        {
            try
            {
                if (HttpContext.Session["utilisateur"] == null)
                {
                    ViewBag.erreur = "Veuillez vous connecter d'abord";
                    return Json("Veuillez vous connecter");
                }
                else
                {
                    Utilisateur utilisateur = HttpContext.Session["utilisateur"] as Utilisateur;
                    LoginUsers login = new LoginUsers(utilisateur.Identifiants, mdp);
                    return Json(utilisateur.GetUtilisateurByLogin(login));
                }
            }
            catch (Exception exception)
            {
                return Json(exception.Message);
            }
        }
        public ActionResult Profile()
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
                    ViewBag.titre = "Profil";
                    Utilisateur utilisateur = HttpContext.Session["utilisateur"] as Utilisateur;
                    if (utilisateur.Poste.IdPoste == "1") { 
                        ViewBag.espaceVente = "ok";
                    }
                    else if(utilisateur.Poste.IdPoste == "2")
                    {
                        ViewBag.espaceStock = "ok";
                    }
                    ViewData["utilisateur"] = utilisateur;
                    return View("Profile");
                }
            }
            catch (Exception exception)
            {
                return View("Profile");
            }
        }
        [HttpPost]
        public JsonResult GetMagasinierByMdp(string mdp)
        {
            try
            {
                Utilisateur utilisateur = new Utilisateur();
                return Json(utilisateur.GetUtilisateurByMdp(mdp));
            }
            catch (Exception exception)
            {
                return Json(exception.Message);
            }
        }
        [HttpPost]
        public JsonResult GetBinome(string nomBinome)
        {
            try
            {
                Utilisateur utilisateur = new Utilisateur();
                return Json(utilisateur.GetUtilisateurByName(nomBinome));
            }
            catch (Exception exception)
            {
                return Json(exception.Message);
            }
        }
    }
}