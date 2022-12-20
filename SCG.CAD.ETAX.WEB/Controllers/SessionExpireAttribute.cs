using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SCG.CAD.ETAX.WEB.Controllers
{
    public class SessionExpireAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool login = false;
            //Check if session is supported
            if (filterContext.HttpContext.Session != null)
            {
                //Check if a new session id was generated
                if (filterContext.HttpContext.Session.IsAvailable)
                {
                    //If it says it is a new session but an existing cookie exists
                    var checklogin = filterContext.HttpContext.Session.GetInt32("islogin");
                    if (checklogin != null)
                    {
                        if (checklogin == 1)
                        {
                            login = true;
                        }
                    }
                }
            }

            if (!login)
            {
                //Redirect to the login page
                //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "AuthSinIn" }));
                filterContext.HttpContext.Response.StatusCode = 401;
            }

            base.OnActionExecuting(filterContext);
        }

    }
}