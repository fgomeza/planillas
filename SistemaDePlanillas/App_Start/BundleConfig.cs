using System.Web;
using System.Web.Optimization;

namespace SistemaDePlanillas
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/scripts").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/modernizr-*",
                        //"~/Scripts/dropzone/dropzone.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js",
                        "~/Scripts/AppScripts/site.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/jasny-bootstrap.js"));

            bundles.Add(new StyleBundle("~/bundles/styles/common").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/jasny-bootstrap.css",
                      "~/Content/dropzone/dropzone.css",
                      "~/Content/AppStyles/site.css",
                      "~/Content/AppStyles/sidebar.css"));

            bundles.Add(new ScriptBundle("~/bundles/js/dropzone").Include(
                      "~/scripts/dropzone/dropzone.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/partials"));
            bundles.Add(new StyleBundle("~/bundles/styles/partials"));
        }
    }
}
