using System;
using System.Collections.Specialized;
using System.Reflection;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace AguaDeMaria.Filters
{
    public class ConvertDatesToUtcAttribute : ActionFilterAttribute
    {
        public const string DateOffset = "DateTimeOffset";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var offset = filterContext.HttpContext.Request.Form[DateOffset];
            if (!string.IsNullOrEmpty(offset) && offset != "0")
            {
                var minuteOffset = 0;
                if (int.TryParse(offset, out minuteOffset))
                {
                    foreach (object param in filterContext.ActionParameters.Values)
                    {
                        ChangeDateProperties(param, minuteOffset);
                    }
                }
            }
            base.OnActionExecuting(filterContext);
        }

        private void ChangeDateProperties(object actionParameter, int minuteOffset)
        {
            var props = actionParameter.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo t in props)
            {
                if (t.PropertyType != typeof (DateTime) || !t.CanWrite || !t.CanRead) continue;

                var currentValue = (DateTime) t.GetValue(actionParameter);
                currentValue = currentValue.AddMinutes(minuteOffset);
                t.SetValue(actionParameter, currentValue);
            }
        }
    }
}