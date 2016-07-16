using EFTest1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EFTest1.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login(string ReturnUrl)
        {
            LoginVM model = new LoginVM();
            model.ReturnUrl = ReturnUrl;

            ViewData.Model = model;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM model)
        {
            if(ModelState.IsValid)
            {
                if(model.Authenticate())
                {
                    FormsAuthentication.SetAuthCookie(model.Email, true, "");

                    return Redirect(model.ReturnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Email or Password combination incorrect");
                }
            }

            return View(model);
        }
    }
}