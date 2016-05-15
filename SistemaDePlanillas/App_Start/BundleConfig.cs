using System.Web;
using System.Web.Optimization;

namespace SistemaDePlanillas
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            /******************/
            /*      Libs      */
            /******************/
            bundles.Add(new ScriptBundle("~/bundles/js/libs").Include(
                        "~/Scripts/lib/jquery-{version}.js",
                        "~/Scripts/lib/knockout-{version}.js",
                        "~/Scripts/lib/sammy-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/scripts").Include(
                        "~/Scripts/lib/jquery-{version}.js",
                        "~/Scripts/lib/bootstrap.js",
                        "~/Scripts/lib/jasny-bootstrap.js",
                        "~/Scripts/lib/bootstrap-switch.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/modernizr").Include(
                        "~/Scripts/lib/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/js/dropzone").Include(
                      "~/Scripts/lib/dropzone.js"));


            /*****************/
            /*      App      */
            /*****************/
            bundles.Add(new ScriptBundle("~/bundles/js/knockout").Include(
                        "~/Scripts/App/knockout-config.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/sammy").Include(
                        "~/Scripts/App/sammy-routing.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/app-scripts").Include(
                        "~/Scripts/App/site.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/bootstrap").Include(
                      "~/Scripts/lib/bootstrap.js",
                      "~/Scripts/lib/respond.js",
                      "~/Scripts/lib/jasny-bootstrap.js"));


            /********************/
            /*      Styles      */
            /********************/
            bundles.Add(new StyleBundle("~/bundles/styles").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/jasny-bootstrap.css",
                      "~/Content/bootstrap-switch.css",
                      "~/Content/daterangepicker.css",
                      "~/Content/dropzone/dropzone.css",
                      "~/Content/site.css"));

            BundleTable.EnableOptimizations = false;
        }
    }
}
