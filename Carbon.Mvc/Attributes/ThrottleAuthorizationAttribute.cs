using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace Carbon.Mvc.Attributes
{
    public class ThrottleAuthorizationAttribute : AuthorizeAttribute
    {
        int _currentRequests = 1;
        int _maxRequestsPerHour;
        int _hours;
        public ThrottleAuthorizationAttribute(int maxRequestsPerHour = 60, int hours = 1)
        {
            _maxRequestsPerHour = maxRequestsPerHour;
            _hours = hours;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            base.AuthorizeCore(httpContext);
            var ip = HttpContext.Current.Request.UserHostAddress;
            if (HttpContext.Current.Cache[$"Carbon-Throttle-{ip}"] != null)
            {
                _currentRequests = (int)HttpContext.Current.Cache[$"Carbon-Throttle-{ip}"] + 1;
                HttpContext.Current.Cache[$"Carbon-Throttle-{ip}"] = _currentRequests;
            }
            else
            {
                HttpContext.Current.Cache.Add($"Carbon-Throttle-{ip}",
                    _currentRequests,
                    null,
                    Cache.NoAbsoluteExpiration,
                    TimeSpan.FromHours(_hours),
                    CacheItemPriority.Low,
                    null);
            }
            if (_currentRequests > _maxRequestsPerHour)
            {
                return false;
            }
            return true;
        }
    }
}
