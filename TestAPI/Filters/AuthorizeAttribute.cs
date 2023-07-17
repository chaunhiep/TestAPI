using Microsoft.AspNetCore.Mvc.Filters;

namespace TestAPI.Filters
{
    public class CustomAuthorizeAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        public CustomAuthorizeAttribute() { }
        private static bool SkipAuthorization(AuthorizationFilterContext context)
        {
            return false;
        }

        protected bool IsAuthorized(AuthorizationFilterContext context)
        {
            Console.WriteLine("Heyyyyy tui kho qua");
            System.Diagnostics.Debug.WriteLine("Troi dat oi");
            return true;

            //if (!base.IsAuthorized(context))
            //{
            //    return false;
            //}

            //System.Diagnostics.Debug.WriteLine("Users {0} - {1}", principal?.Identity, principal?.Identity?.Name);

            //if (!principal.Identity.Name.Contains("nhiep"))
            //{
            //    return false;
            //}

            //return true;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context == null || SkipAuthorization(context))
            {
                System.Diagnostics.Debug.WriteLine("Context null {0}", context?.HttpContext.User.Identities);
                return;
            }

            if (!IsAuthorized(context))
            {
                return;
                // HandleUnauthorizedRequest(context);
            }
        }
    }
}
