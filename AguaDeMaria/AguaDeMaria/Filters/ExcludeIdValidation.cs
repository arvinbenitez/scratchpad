using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AguaDeMaria.Filters
{
    public class ExcludeIdValidation : ActionFilterAttribute
    {
        public string IdField { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewData.ModelState.Remove(IdField);
            base.OnActionExecuting(filterContext);
        }
    }
}