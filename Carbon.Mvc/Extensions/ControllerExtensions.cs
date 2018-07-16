using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Carbon.Mvc.Extensions
{
    public static class ControllerExtensions
    {
        
        public static string GetModelErrors(this Controller controller)
        {
            return string.Join(" ", controller.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
        }

        public static void ShowDangerAlert(this Controller controller, string message)
        {
            controller.TempData["message"] = message;
            controller.TempData["message-style"] = "alert-danger";
        }

        public static void ShowDangerAlert(this Controller controller, string format, params object[] args)
        {
            controller.ShowDangerAlert(string.Format(format, args));
        }

        public static void ShowSuccessAlert(this Controller controller, string message)
        {
            controller.TempData["message"] = message;
            controller.TempData["message-style"] = "alert-success";
        }

        public static void ShowSuccessAlert(this Controller controller, string format, params object[] args)
        {
           controller.ShowSuccessAlert(string.Format(format, args));
        }
    }
}
