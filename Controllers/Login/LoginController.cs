using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace stock.Controllers.Login
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Connecter()
        {
            try
            {
                return View("AccueilVente");
            }
            catch(Exception ex)
            {
                return View("Login");
            }
        }
        public ActionResult Index()
        {
            return View("Login");
        }
    }
}