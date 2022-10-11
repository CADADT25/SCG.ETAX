using Microsoft.AspNetCore.Mvc.Filters;

namespace SCG.CAD.ETAX.WEB.Controllers
{
    public class PermissionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool permission = false;
            //Check if session is supported
            if (filterContext.HttpContext.Session != null)
            {
                //Check if a new session id was generated
                if (filterContext.HttpContext.Session.IsAvailable)
                {
                    //If it says it is a new session but an existing cookie exists
                    var checklogin = filterContext.HttpContext.Session.GetInt32("checkpermissionpage");
                    if (checklogin != null)
                    {
                        if (checklogin == 1)
                        {
                            permission = true;
                        }
                    }
                }
            }

            if (!permission)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "AuthSinIn" }));
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
