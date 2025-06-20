using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace DabbaTrading
{
	// Token: 0x02000005 RID: 5
	public class RouteConfig
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002D82 File Offset: 0x00000F82
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.MapRoute("Default", "{controller}/{action}/{id}", new
			{
				controller = "login",
				action = "index",
				id = UrlParameter.Optional
			});
		}
	}
}
