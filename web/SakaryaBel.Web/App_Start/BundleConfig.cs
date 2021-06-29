﻿using System.Web;
using System.Web.Optimization;

namespace SakaryaBel
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();
            bundles.IgnoreList.Ignore("*-vsdoc.js");
            bundles.IgnoreList.Ignore("*intellisense.js");

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                    "~/Scripts/angular.min.js",
                    "~/Scripts/jquery-{version}.js",
                    "~/Scripts/jquery.uniform.js",
                    "~/Scripts/amplify.min.js",
                    "~/Scripts/bootstrap.min.js"
                    ));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                       "~/Scripts/jquery-ui-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            #region AdminPanelJs definition
            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                        "~/Scripts/knockout-{version}.js",
                        "~/Scripts/knockout.mapping-latest.js",
                        "~/Scripts/knockout.validation.js"

                        ));

            bundles.Add(new ScriptBundle("~/bundles/dataservice").Include(
                        "~/Scripts/dataservice/amplify.define.js",
                        "~/Scripts/dataservice/dataservice.*",
                        "~/Scripts/dataservice/dataserviceInitializer.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/application").Include(
                        "~/Scripts/jquery.inputmask.js",
                        "~/Scripts/jquery.numeric.js",
                        //"~/Scripts/blmsSettings.js",
                        "~/Scripts/application/*.js"
                     // "~/Scripts/DT_bootstrap.js"
                     ));

            bundles.Add(new ScriptBundle("~/bundles/applicationAngular").Include(
                    "~/Scripts/applicationAngular/*.js"
                     ));

            #endregion
        }
    }
}
