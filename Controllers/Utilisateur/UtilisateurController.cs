using stock.Models.DAO;
using stock.Models.Classe.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using stock.Models.Classe;

namespace stock.Controllers.Utilisateurs
{
    public class UtilisateurController : Controller
    {
        // GET: Utilisateur
        public ActionResult Index()
        {
            return View();
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