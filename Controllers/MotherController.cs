using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace stock.Controllers
{
    public class MotherController : Controller
    {
        // GET: Mother
        public ActionResult Index()
        {
            return View();
        }
    }
}