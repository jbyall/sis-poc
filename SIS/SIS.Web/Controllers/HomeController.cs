using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace SIS.Web.Controllers
{
    // To Authorize access based on AD domain and group
    //[Authorize(Roles = @"domain\group")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //// Get the logged-in user's WindowsIdentity
            //WindowsIdentity identity = (WindowsIdentity)User.Identity;

            //// Get a list of the user's groups
            //var groups = identity.Groups.ToList();
            
            //// Loop over each of the user's groups and find the NT group name
            //// Note - by default, the group is displayed as an SID
            //foreach (var item in identity.Groups.ToList())
            //{
            //    var groupName = item.Translate(typeof(NTAccount)).Value;
            //}

            //// To test if the user is in a specific group
            ////var isInGroup = User.IsInRole(@"domain\group");

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

        public ActionResult Exit()
        {
            return View();
        }
    }
}