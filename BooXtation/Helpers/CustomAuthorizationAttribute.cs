using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace BooXtation.Helpers
{
    public class CustomAuthorizationAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //context.HttpContext.Session.SetString("CanAccessPayment" , "false");

            if (context.HttpContext.Session.GetString("CanAccessPayment") != "true")
            {
               
                context.Result = new RedirectToActionResult("AccessDenied", "Home", null);
            }

            //if (context.HttpContext.Request.Method != "POST"
            //    && !context.HttpContext.Request.Form.ContainsKey("buttonAction"))
            //{
               
            //    context.Result = new RedirectToActionResult("Index", "Home", null);
            //}
        }
        
        
        
    }
}
