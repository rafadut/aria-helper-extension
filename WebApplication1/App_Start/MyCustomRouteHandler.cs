using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication1
{
    public class MyCustomRouteHandler : MvcRouteHandler
    {
        protected override IHttpHandler GetHttpHandler(RequestContext reqContext)
        {
            string importantValue = reqContext.HttpContext.Request.Headers.Get(
            "User-Agent");
            if (!string.IsNullOrWhiteSpace(importantValue))
            {
                reqContext.RouteData.Values["action"] = importantValue +
                reqContext.RouteData.Values["action"];
            }
            return base.GetHttpHandler(reqContext);
        }
    }
}