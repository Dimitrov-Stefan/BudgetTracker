using Microsoft.AspNetCore.Mvc;
using System;

namespace Web.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string ControllerName(this IUrlHelper helper, Type controller)
        {
            return controller.Name.Replace("Controller", string.Empty);
        }
    }
}
