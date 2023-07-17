using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace TestAPI.Filters
{
    public class LogAttribute : ActionFilterAttribute
    {
        public LogAttribute()
        {

        }

        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            Console.WriteLine("Nguyen Viet Duc");
            Trace.WriteLine(string.Format("Action Method {0} executing at {1}", actionContext.ActionDescriptor.DisplayName, DateTime.Now.ToShortDateString()), "Web API Logs");
        }

        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            Console.WriteLine("Tran Quoc Viet");
            Trace.WriteLine(string.Format("Action Method {0} executed at {1}", actionExecutedContext.HttpContext.Response, DateTime.Now.ToShortDateString()), "Web API Logs");
        }
    }
}
