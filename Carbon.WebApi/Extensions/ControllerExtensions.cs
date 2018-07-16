using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Carbon.WebApi.Extensions
{
    public static class ControllerExtensions
    {
        public static string GetModelErrors(this ApiController controller)
        {
            return string.Join(" ", controller.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
        }
    }
}
