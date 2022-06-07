using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SCG.CAD.ETAX.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.UTILITY
{
    public class AuthGuard : ActionFilterAttribute, IAuthorizationFilter
    {

        
        public int OnAuthentication(AuthenticationModel filterContext)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(filterContext.username)) && filterContext.authenticated == true)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session)))
            {
                filterContext.Result = new RedirectResult("~/Auth/Login");
            }
        }
    }
}
