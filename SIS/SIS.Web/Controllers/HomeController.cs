using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace SIS.Web.Controllers
{
    //[Authorize(Roles = @"domain\group")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            WindowsIdentity identity = (WindowsIdentity)User.Identity;
            if (User.IsInRole("S-1-5-4"))
            {
                var testGroup = identity.Groups.Where(g => g.Value == "S-1-5-4").First();
                foreach (var item in identity.Groups.ToList())
                {
                    var groupName = item.Translate(typeof(NTAccount)).Value;
                }
                

            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}