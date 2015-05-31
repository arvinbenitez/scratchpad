using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using AguaDeMaria.Common.Data;
using AguaDeMaria.Model;
using ServiceStack.ServiceInterface.Auth;

namespace AguaDeMaria.Areas.Accounts.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserAuthRepository userAuthRepository;
        private readonly IRepository<User> userRepository;
        private readonly AuthService authService;

        public UserController(IUserAuthRepository userAuthRepository, IRepository<User> userRepository, AuthService authService)
        {
            this.userAuthRepository = userAuthRepository;
            this.userRepository = userRepository;
            this.authService = authService;
        }

        [HttpGet]
        public JsonResult List()
        {
            var list = userRepository.Get(x => x.Id > 0, y => y.OrderBy(z => z.LastName).ThenBy(z => z.FirstName));
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Get(int id)
        {
            var user = userAuthRepository.GetUserAuth(id.ToString());
            return Json(new {user.Id, user.Email, user.UserName, user.FirstName, user.LastName}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Post(User user)
        {
            var result = new UserUpdateResult();
            if (user.IsNew)
            {
                if (user.PasswordMatch)
                {
                    var newUserAuth = new UserAuth
                    {
                        Email = user.Email,
                        UserName = user.UserName,
                        FirstName = user.FirstName,
                        LastName = user.LastName,

                    };
                    userAuthRepository.CreateUserAuth(newUserAuth, user.NewPassword);
                    result.Success = true;
                }
                else
                {
                    result.Success = false;
                    result.Message = "Password does not match";
                }
            }
            else
            {
                if (user.PasswordChanged)
                {
                    if (user.PasswordMatch)
                    {
                        UserAuth userAuth;
                        if (userAuthRepository.TryAuthenticate(user.UserName, user.CurrentPassword, out userAuth))
                        {
                            var newUserAuth = new UserAuth
                            {
                                Email = user.Email,
                                UserName = user.UserName,
                                FirstName = user.FirstName,
                                LastName = user.LastName,

                            };
                            userAuthRepository.UpdateUserAuth(userAuth, newUserAuth, user.NewPassword);
                            result.Success = true;
                        }
                        else
                        {
                            result.Success = false;
                            result.Message = "Incorrect password";
                        }
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "Password does not match";
                    }
                }
                else
                {
                    userRepository.Update(user);
                    userRepository.Commit();
                    result.Success = true;
                }
            }
            return Json(result);
        }
        public class UserUpdateResult
        {
            public bool Success { get; set; }
            public string Message { get; set; }
        }
    }

}
