using System.Web.Mvc;

namespace AguaDeMaria.Areas.Accounts
{
    public class AccountsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Accounts";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Accounts_default",
                url: "Accounts/{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "AguaDeMaria.Areas.Accounts.Controllers" }
            );
        }
    }
}
