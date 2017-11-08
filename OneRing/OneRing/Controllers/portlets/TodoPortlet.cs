using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OneRing.Controllers
{
    public class TodoPortletController : Controller
    {
        // GET: TodoPortlet
        public ActionResult Index()
        {
            return View();
        }
    }
}