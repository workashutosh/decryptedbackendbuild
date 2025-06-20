using System;
using System.Web.Mvc;

namespace DabbaTrading.Controllers
{
	// Token: 0x02000019 RID: 25
	public class PaymentController : Controller
	{
		// Token: 0x0600032A RID: 810 RVA: 0x00023B74 File Offset: 0x00021D74
		public ActionResult Success()
		{
			return base.View();
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00023B74 File Offset: 0x00021D74
		public ActionResult Failure()
		{
			return base.View();
		}
	}
}
