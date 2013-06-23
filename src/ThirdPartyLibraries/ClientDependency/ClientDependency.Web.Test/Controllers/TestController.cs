﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClientDependency.Web.Models;

namespace ClientDependency.Web.Controllers
{
    [HandleError]
    public class TestController : Controller
    {
        /// <summary>
        /// Yes i realize this shouldn't be hard coded here, but it's an example website!
        /// </summary>
        /// <returns></returns>
        public ActionResult Default()
        {
            var model = new TestModel()
            {
                Heading = "Using the default provider specified in the web.config",
                BodyContent = @"<p>
Nothing fancy here, just rendering the script and style blocks using the default renderer. 
This library ships with the <b>StandardRenderer</b> set to the default renderer. 
The default renderer will render out standard script/style html blocks wherever you specify using
the HtmlHelper:</p>
<p>
&lt;%= Html.RenderCssHere(new BasicPath(""Styles"", ""/Css"")) %&gt;
</p>
<p>
To make a page/view/etc... dependent on a script or file just use this HtmlHelper methods:
</p>
<p>
<% Html.RequiresCss(""Site.css"", ""Styles""); %><br/>
&lt;% Html.RequiresJs(""/Js/jquery-1.3.2.min.js""); %&gt;
</p>
"
            };

            return View(model);
        }

        public ActionResult RogueDependencies()
        {
            var model = new TestModel()
            {
                Heading = "Replacing rogue scripts/styles with composite files",
                BodyContent = @"<p>
This page demonstrates the replacement of 'Rogue' script/style (link) tags that exist in the raw html of the page. These scripts get replaced with the compression handler URL and are handled then just like other scripts that are being rendered via a client dependency object. 
</p>
<p>
The term 'Rogue' refers to a script/styles that hasn't been registered on the page with the ClientDependency framework and instead is registered as raw script/style tags. 
</p>
<p>
IMPORTANT: Please note that although Rogue scripts/styles get replaced with compressed scripts/styles using this framework, it still means that there will be more requests when they are not properly registered because rogue scripts are not combined into one request! 
</p>
"
            };

            return View(model);
        }
    }
}
