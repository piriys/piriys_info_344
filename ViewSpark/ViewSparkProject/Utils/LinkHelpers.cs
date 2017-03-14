using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ViewSparkProject.Utils.Helpers
{
    public static class VSExtensions
    {
        //public static IHtmlString VSLink(this HtmlHelper html, string text, string controller, string action, 


        private static IHtmlString _VSLink_NoPrefix(this HtmlHelper html, string text, string url)
        {
            TagBuilder t = new TagBuilder("a");

            string divid = Convert.ToString(html.ViewData["divid"]);

            if (divid != null)
            {
                t.Attributes.Add(new KeyValuePair<string, string>("data-ajax", "true"));
                t.Attributes.Add(new KeyValuePair<string, string>("data-ajax-update", "#" + divid));


                t.Attributes.Add(new KeyValuePair<string, string>("href", url));
            }
            else
            {
                t.Attributes.Add(new KeyValuePair<string, string>("href", url));
            }

            t.InnerHtml = text;

            return MvcHtmlString.Create(t.ToString());

        }



        private static IHtmlString _VSLink(this HtmlHelper html, string text, string url)
        {
            string divid = Convert.ToString(html.ViewData["divid"]);

            if(divid != null && !url.StartsWith("/sub"))
            {
                return _VSLink_NoPrefix(html, text, "/sub/" + divid + url);
            }

            return _VSLink_NoPrefix(html, text, url);
        }



        public static IHtmlString VSLink(this HtmlHelper html, string text, string controller, string action, object routeValues)
        {            
            string divid = Convert.ToString(html.ViewData["divid"]);

            UrlHelper uh = new UrlHelper(html.ViewContext.RequestContext, html.RouteCollection);

            string url = uh.Action(action,controller,routeValues);

            return _VSLink(html, text, url);
        }


        public static IHtmlString VSLink(this HtmlHelper html, string text, string controller, string action)
        {
            string divid = Convert.ToString(html.ViewData["divid"]);

            UrlHelper uh = new UrlHelper(html.ViewContext.RequestContext, html.RouteCollection);

            string url = uh.Action(action, controller);

            return _VSLink(html, text, url);
        }



        public static IHtmlString VSLink(this HtmlHelper html, string text, string action)
        {
            string c = html.ViewContext.HttpContext.Request.FilePath;
            string divid = Convert.ToString(html.ViewData["divid"]);

            UrlHelper uh = new UrlHelper(html.ViewContext.RequestContext, html.RouteCollection);

            string url = uh.Action(action);

            return _VSLink(html, text, url);
        }



        public static IHtmlString VSLink(this HtmlHelper html, string text, string action, object routeValues)
        {
            string divid = Convert.ToString(html.ViewData["divid"]);

            UrlHelper uh = new UrlHelper(html.ViewContext.RequestContext, html.RouteCollection);

            string url = uh.Action(action,routeValues);

            return _VSLink_NoPrefix(html, text, url);
        }


    }
}