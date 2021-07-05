using System.Web;
using System.Web.Optimization;

namespace SiemensProjectManagement
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/DataTables/jquery.dataTables.js",
                        "~/Scripts/DataTables/dataTables.select.js",
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/qlass.js",
                        "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/jquery-confirm.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/angular.js"));

            //bundles.Add(new ScriptBundle("~/bundles/plotly").Include("~/Scripts/plotly/plotly-latest.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/respond.js",
                      "~/Scripts/typeahead.bundle.js",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap-select.js",
                      "~/Scripts/bootstrap-multiselect.js",
                      "~/Scripts/bootstrap-toggle.js",
                      "~/Scripts/bootstrap-slider.js",
                      "~/Scripts/select2.js",
                      "~/Scripts/underscore.js",
                      "~/Scripts/toastr.min.js",
                      "~/Scripts/html2canvas.min.js"
                    
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/site.css",
                      "~/Content/typeahead.css",
                      "~/Content/DataTables/css/jquery.dataTables.css",
                      "~/Content/DataTables/css/select.dataTables.css",
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-select.css",
                      //"~/Content/bootstrap-multiselect.js",
                      "~/Content/bootstrap-toggle.css",
                      "~/Content/css/select2.css",
                      "~/Content/css/bootstrap-slider.css",
                      "~/Content/css/bootstrap-treeview.min.css",
                      "~/Content/themes/base/jquery-ui.css",
                       "~/Content/jquery.treegrid.min.css",
                       "~/Content/jquery.jquery.treegrid.css",
                       "~/Content/jquery.treetable.min.css",
                       "~/Content/jquery.treetable.theme.default.css",
                       "~/Content/jquery.treetable.theme.default.min.css",
                       "~/Content/toastr.min.css",
                       "~/Content/jquery-confirm.min.css"
                      ));

            // from http://d3js.org/
            bundles.Add(new ScriptBundle("~/bundles/d3").Include(
                "~/Scripts/d3.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/siemens.itest1500").Include(
                "~/Scripts/siemens.itest1500.FormValues.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/flot").Include(
                "~/Scripts/plotly/plotly-latest.min.js",
                 "~/Scripts/dataTables.buttons.min.js",
                  "~/Scripts/buttons.flash.min.js",
                  "~/Scripts/jszip.min.js",
                   "~/Scripts/buttons.html5.min.js",
                    "~/Scripts/buttons.print.min.js",
                      "~/Scripts/pdfmake.min.js",
                      "~/Scripts/vfs_fonts.js",
                        "~/Scripts/bootstrap-treeview.min.js",
                            "~/Scripts/jquery.treegrid.min.js",
                              "~/Scripts/jquery.treetable.js",
                              "~/Scripts/jquery.treetable.min.js",
                                    "~/Scripts/jspdf.min.js",
                                    "~/Scripts/ html2canvas.js"


                ));
        }
    }
}
