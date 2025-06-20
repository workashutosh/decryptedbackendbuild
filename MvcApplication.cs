using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ATS;

namespace DabbaTrading
{
	// Token: 0x02000006 RID: 6
	public class MvcApplication : HttpApplication
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00002DB4 File Offset: 0x00000FB4
		protected void Application_Start()
		{
			ServicePointManager.SecurityProtocol = 3072;
			Universal.InitConnection("");
			AreaRegistration.RegisterAllAreas();
			RouteConfig.RegisterRoutes(RouteTable.Routes);
		}
	}
}
