
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Diagnostics;

namespace BookStoreApplicationAPI.Filters
{
    public class LogRequestFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var log = new
            {
                Controller = filterContext.ActionDescriptor.FilterDescriptors.Count,
                Action = filterContext.ActionDescriptor.DisplayName,
                IP = filterContext.HttpContext.Request.Host.Value,
                //DateTime = filterContext.HttpContext.RequestServices.GetService<DateTime>()
            };
            Debug.WriteLine(JsonConvert.SerializeObject(log));           
        }
    }
}
