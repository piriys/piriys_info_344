using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ViewSparkProject;
using System.Web.Mvc;

namespace ViewSparkProject
{
    /// <summary>
    /// A partial class to still enable validation
    /// </summary>
    public partial class Slide
    {
        [AllowHtml]
        public MvcHtmlString HtmlContentHtmlString
        {
            get { return MvcHtmlString.Create(this.Content); }
            set { this.Content = value.ToString(); }
        }

        [AllowHtml]
        public string HtmlContentString
        {
            get { return this.Content; }
            set { this.Content = value.ToString(); }
        }

    }
}