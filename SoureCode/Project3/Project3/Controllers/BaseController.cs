using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Project3.Controllers
{
    public class BaseController : Controller, IActionFilter
    {
      
        public override async void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("LoginName") == null)
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { Area = "", Controller = "Login", Action = "Index" })
                );
            }
            base.OnActionExecuting(context);
        }
    }

}
