using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace REST.Controllers
{
    public class PagesController : Controller
    {
        public ActionResult Main()
        {
            return View();
        }

        public ActionResult Queue()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
    }
}
