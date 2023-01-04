using System.Web;
using System.Web.Mvc;

namespace tx_aspnet_mvc_framework_viewer {
	public class FilterConfig {
		public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
			filters.Add(new HandleErrorAttribute());
		}
	}
}
