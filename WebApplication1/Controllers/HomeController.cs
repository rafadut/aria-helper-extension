using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }

    public static class Class1
    {
        public static IHtmlString ARIATextBoxFor<TModel, TProperty>(this HtmlHelper<TModel>
helper, Expression<Func<TModel, TProperty>> exp)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(exp,
            helper.ViewData);
            var attr = new RouteValueDictionary();
            if (metadata.IsRequired)
            {
                attr.Add("aria-required", true);
            }
            return helper.TextBoxFor(exp, attr);
        }
    }
}