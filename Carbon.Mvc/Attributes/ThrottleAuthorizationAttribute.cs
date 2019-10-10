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

    public enum TimeSpanType
    {
        FromSec = 0,
        FromMin = 1,
        FromHrs = 2,
        FromDays = 3,
        FromMilliSec = 4,
        FromTicks = 5
    }

    public class ThrottleAuthorizationAttribute : AuthorizeAttribute
    {
        int _currentRequests = 1;
        int _maxRequestsPerTimeSpan;
        TimeSpanType _timeSpanType;
        int _timeSpanValue;

        public ThrottleAuthorizationAttribute(int timeSpanValue = 1, TimeSpanType timeSpanType = TimeSpanType.FromHrs, int maxRequestsPerTimeSpan = 60)
        {
            this._maxRequestsPerTimeSpan = maxRequestsPerTimeSpan;
            this._timeSpanType = timeSpanType;
            this._timeSpanValue = timeSpanValue;
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
                    AssignTimeSpan(),
                    CacheItemPriority.Low,
                    null);
            }
            if (_currentRequests > this._maxRequestsPerTimeSpan)
            {
                return false;
            }
            return true;
        }

        private TimeSpan AssignTimeSpan()
        {
            switch (this._timeSpanType)
            {
                case TimeSpanType.FromSec:
                    return TimeSpan.FromSeconds(this._timeSpanValue);
                case TimeSpanType.FromMin:
                    return TimeSpan.FromMinutes(this._timeSpanValue);
                case TimeSpanType.FromHrs:
                    return TimeSpan.FromHours(this._timeSpanValue);
                case TimeSpanType.FromDays:
                    return TimeSpan.FromDays(this._timeSpanValue);
                case TimeSpanType.FromMilliSec:
                    return TimeSpan.FromMilliseconds(this._timeSpanValue);
                case TimeSpanType.FromTicks:
                    return TimeSpan.FromTicks(this._timeSpanValue);
                default:
                    throw new Exception($"Invalid TimeSpanType-{this._timeSpanType}");
            }
        }
    }
}
