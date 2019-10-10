using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Carbon.WebApi.Attributes
{
    public class ConcurrentRequestAttribute : ActionFilterAttribute
    {
        private int _concurrentRequestAllowed;
        private string _key;
        public ConcurrentRequestAttribute(int concurrentRequestsAllowed = 0)
        {
            _concurrentRequestAllowed = concurrentRequestsAllowed;
        }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var identity = HttpContext.Current.Request.UserHostAddress;
            if (HttpContext.Current.User?.Identity?.IsAuthenticated != null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                identity = HttpContext.Current.User.Identity.Name;
            }
            _key = $"Carbon-ConcurrentRequest-{identity}";

            int activeRequests = 0;
            if (HttpContext.Current.Cache[_key] == null)
            {
                HttpContext.Current.Cache.Add(_key, activeRequests, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(1), CacheItemPriority.Low, null);
                return;
            }
            activeRequests = (int)HttpContext.Current.Cache[_key];

            if (activeRequests > _concurrentRequestAllowed)
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.ServiceUnavailable)
                {
                    ReasonPhrase = "Too many requests",
                    RequestMessage = actionContext.Request,
                    Content = new ObjectContent(typeof(object), new { message = "Too many requests" }, GlobalConfiguration.Configuration.Formatters.JsonFormatter, "application/json")
                };
            }
            else
            {
                HttpContext.Current.Cache[_key] = ++activeRequests;
            }

            base.OnActionExecuting(actionContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            HttpContext.Current.Cache[_key] = 0;
            base.OnActionExecuted(actionExecutedContext);
        }

    }

}
