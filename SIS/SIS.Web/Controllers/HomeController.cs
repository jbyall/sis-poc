using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace SIS.Web.Controllers
{
    // Handles Home requests that are made by the use
    // NOTE - Uncomment the Authorize attribute and modify the role
    //      to control access to this controller.
    //      use the [AllowAnonymous] attribute on any method to override

    //[Authorize(Roles = @"domain\group")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // NOTE - ALL COMMENTED CODE BELOW IS USED FOR TESTING
            // AND SHOULD NOT BE USED IN PRODUCTION

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

        // This is hit when the user click the EXIT link from the top menu
        public ActionResult Exit()
        {
            return View();
        }
    }
}