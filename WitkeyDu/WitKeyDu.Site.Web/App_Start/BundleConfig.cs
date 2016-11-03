using System.Web;
using System.Web.Optimization;

namespace WitKeyDu.Site.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bootstrap/css").Include(
                "~/Content/RefrenceFile/Bootstrap/css/bootstrap.css"));
            bundles.Add(new ScriptBundle("~/bootstrap/js").Include(
                "~/Content/RefrenceFile/Bootstrap/js/jquery-2.2.0.js",
                "~/Content/RefrenceFile/Bootstrap/js/bootstrap.js"));
        }
    }
}