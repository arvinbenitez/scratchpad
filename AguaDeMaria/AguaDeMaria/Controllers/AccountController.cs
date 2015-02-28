using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AguaDeMaria.Models;
using ServiceStack;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface.Auth;

namespace AguaDeMaria.Controllers
{
    public class AccountController : Controller
    {
        private AuthService AuthService { get; set; }

        public AccountController(AuthService auth)
        {
            AuthService = auth;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            var loginData = new LoginModel { Redirect = returnUrl };
            return View(loginData);
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", model);
            }

            AuthService.RequestContext = System.Web.HttpContext.Current.ToRequestContext();
            try
            {

                AuthResponse response = AuthService.Authenticate(new Auth
                {
                    UserName = model.UserName,
                    Password = model.Password,
                    RememberMe = model.RememberMe,
                    Continue = model.Redirect
                });


                var authTicket = new FormsAuthenticationTicket(
                    1, model.UserName, DateTime.Now, DateTime.Now.AddDays(1), model.RememberMe, "", "/");

                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                    FormsAuthentication.Encrypt(authTicket));

                Response.Cookies.Add(cookie);

                //FormsAuthentication.SetAuthCookie(response.UserName, false);
                if (model.Redirect != null)
                {
                    return Redirect(model.Redirect);
                }
                return RedirectToAction("Index", "Home");
            }
            catch (HttpError ex)
            {

                if (ex.Status == 401)
                {
                    ModelState.AddModelError("", ex.ErrorCode);
                    return View("Login", model);
                }
                throw;
            }
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");

        }

    }
}
