using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1
{
    public class CustomViewEngine : VirtualPathProviderViewEngine
    {
        public CustomViewEngine()
        {
            this.ViewLocationFormats = new string[]
            { "~/Views/{1}/{2}.mytheme ", "~/Views/Shared/{2}.mytheme" };
            this.PartialViewLocationFormats = new string[]
            { "~/Views/{1}/{2}.mytheme ", "~/Views/Shared/{2}. mytheme " };
        }

        protected override IView CreatePartialView
        (ControllerContext controllerContext, string partialPath)
        {
            var physicalpath =
            controllerContext.HttpContext.Server.MapPath(partialPath);
            return new MyCustomView(physicalpath);
        }

        protected override IView CreateView
        (ControllerContext controllerContext, string viewPath, string masterPath)
        {
            var physicalpath = controllerContext.HttpContext.Server.MapPath(viewPath);
            return new MyCustomView(physicalpath);
        }
    }

    public class CustomResult<T> : ActionResult
    {
        public T Data { private get; set; }
        public override void ExecuteResult(ControllerContext context)
        {
            // do work here
            string resultFromWork = "work that was done";
            context.HttpContext.Response.Write(resultFromWork);
        }
    }

    public class MyCustomView : IView
    {
        private string _viewPhysicalPath;
        public MyCustomView(string ViewPhysicalPath)
        {
            _viewPhysicalPath = ViewPhysicalPath;
        }
        public void Render(ViewContext viewContext, System.IO.TextWriter writer)
        {
            string rawcontents = File.ReadAllText(_viewPhysicalPath);
            string parsedcontents = Parse(rawcontents, viewContext.ViewData);
            writer.Write(parsedcontents);
        }
        public string Parse(string contents, ViewDataDictionary viewdata)
        {
            return Regex.Replace(contents, "\\{(.+)\\}", m => GetMatch(m, viewdata));
        }
        public virtual string GetMatch(Match m, ViewDataDictionary viewdata)
        {
            if (m.Success)
            {
                string key = m.Result("$1");
                if (viewdata.ContainsKey(key))
                {
                    return viewdata[key].ToString();
                }
            }
            return string.Empty;
        }
    }
}