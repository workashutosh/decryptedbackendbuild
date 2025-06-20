using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Mvc;
using ATS;

namespace DabbaTrading.Controllers
{
	// Token: 0x02000016 RID: 22
	public class homeController : Controller
	{
		// Token: 0x060001D9 RID: 473 RVA: 0x00023B74 File Offset: 0x00021D74
		public ActionResult dashboard()
		{
			return base.View();
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00023B74 File Offset: 0x00021D74
		public ActionResult historychart()
		{
			return base.View();
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00023B7C File Offset: 0x00021D7C
		public ActionResult mobile_marketwatch_component()
		{
			return base.PartialView();
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00023B7C File Offset: 0x00021D7C
		public ActionResult mobile_trades_component()
		{
			return base.PartialView();
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00023B7C File Offset: 0x00021D7C
		public ActionResult mobile_portfolio_component()
		{
			return base.PartialView();
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00023B7C File Offset: 0x00021D7C
		public ActionResult mobile_account_component()
		{
			return base.PartialView();
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00023B84 File Offset: 0x00021D84
		public Task<ActionResult> paymentsuccess()
		{
			homeController.<paymentsuccess>d__6 <paymentsuccess>d__;
			<paymentsuccess>d__.<>t__builder = AsyncTaskMethodBuilder<ActionResult>.Create();
			<paymentsuccess>d__.<>4__this = this;
			<paymentsuccess>d__.<>1__state = -1;
			<paymentsuccess>d__.<>t__builder.Start<homeController.<paymentsuccess>d__6>(ref <paymentsuccess>d__);
			return <paymentsuccess>d__.<>t__builder.Task;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00023BC8 File Offset: 0x00021DC8
		public static Task<string> CallstatusApi(string trnsid)
		{
			homeController.<CallstatusApi>d__7 <CallstatusApi>d__;
			<CallstatusApi>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<CallstatusApi>d__.trnsid = trnsid;
			<CallstatusApi>d__.<>1__state = -1;
			<CallstatusApi>d__.<>t__builder.Start<homeController.<CallstatusApi>d__7>(ref <CallstatusApi>d__);
			return <CallstatusApi>d__.<>t__builder.Task;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00023C0C File Offset: 0x00021E0C
		public ActionResult paymentcancel()
		{
			using (StreamReader reader = new StreamReader(base.Request.InputStream))
			{
				string[] resparr = reader.ReadToEnd().Split(new char[]
				{
					'&'
				});
				string txnid = "";
				string amount = "";
				string firstname = "";
				for (int i = 0; i < resparr.Length; i++)
				{
					if (resparr[i].Contains("txnid"))
					{
						txnid = resparr[i].Replace("txnid=", "");
					}
					if (resparr[i].Contains("mode"))
					{
						resparr[i].Replace("mode=", "");
					}
					if (resparr[i].Contains("amount"))
					{
						amount = resparr[i].Replace("amount=", "");
					}
					if (resparr[i].Contains("phone"))
					{
						resparr[i].Replace("phone=", "");
					}
					if (resparr[i].Contains("status"))
					{
						resparr[i].Replace("status=", "");
					}
					if (resparr[i].Contains("firstname"))
					{
						firstname = resparr[i].Replace("firstname=", "");
					}
				}
				Universal.ExecuteNonQuery(string.Concat(new string[]
				{
					"insert into tbl_payment_transaction_master (UserId,TransactionId,Amount,Remarks,CreatedDateTime,Status_,CreatedBy) values('",
					firstname,
					"','",
					txnid,
					"','",
					amount,
					"','Payment Canceled',now(),'canceled','Online')"
				}));
			}
			return base.View();
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00023DB4 File Offset: 0x00021FB4
		public ActionResult paymentfail()
		{
			using (StreamReader reader = new StreamReader(base.Request.InputStream))
			{
				string[] resparr = reader.ReadToEnd().Split(new char[]
				{
					'&'
				});
				string txnid = "";
				string amount = "";
				string firstname = "";
				for (int i = 0; i < resparr.Length; i++)
				{
					if (resparr[i].Contains("txnid"))
					{
						txnid = resparr[i].Replace("txnid=", "");
					}
					if (resparr[i].Contains("mode"))
					{
						resparr[i].Replace("mode=", "");
					}
					if (resparr[i].Contains("amount"))
					{
						amount = resparr[i].Replace("amount=", "");
					}
					if (resparr[i].Contains("phone"))
					{
						resparr[i].Replace("phone=", "");
					}
					if (resparr[i].Contains("status"))
					{
						resparr[i].Replace("status=", "");
					}
					if (resparr[i].Contains("firstname"))
					{
						firstname = resparr[i].Replace("firstname=", "");
					}
				}
				Universal.ExecuteNonQuery(string.Concat(new string[]
				{
					"insert into tbl_payment_transaction_master (UserId,TransactionId,Amount,Remarks,CreatedDateTime,Status_,CreatedBy) values('",
					firstname,
					"','",
					txnid,
					"','",
					amount,
					"','Payment failed',now(),'failed','Online')"
				}));
			}
			return base.View();
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00023B74 File Offset: 0x00021D74
		public ActionResult Autosqarapartialview()
		{
			return base.View();
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00023B74 File Offset: 0x00021D74
		public ActionResult Autosqarapartialview_App(string userid, string M_MCX, string M_NSE, string M_CDS, string MCX_BRKG, string NSE_BRKG, string CDS_BRKG, string MCX_brokerage_per_crore, string Equity_brokerage_per_crore, string CDS_brokerage_per_crore, string BULLDEX_brokerage, string GOLD_brokerage, string SILVER_brokerage, string CRUDEOIL_brokerage, string COPPER_brokerage, string NICKEL_brokerage, string ZINC_brokerage, string LEAD_brokerage, string NATURALGAS_brokerage, string ALUMINIUM_brokerage, string MENTHAOIL_brokerage, string COTTON_brokerage, string CPO_brokerage, string GOLDM_brokerage, string SILVERM_brokerage, string SILVERMIC_brokerage, string LB, string CL)
		{
			return base.View();
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00023B74 File Offset: 0x00021D74
		public ActionResult AutosqaraPendingToActive()
		{
			return base.View();
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00023B74 File Offset: 0x00021D74
		public ActionResult AutosqaraPendingToActive_app(string userid, string UserName, string LB, string CL)
		{
			return base.View();
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00023B74 File Offset: 0x00021D74
		[OutputCache(Duration = 60, VaryByParam = "name1")]
		public ActionResult mcxfuture()
		{
			return base.View();
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00023B74 File Offset: 0x00021D74
		[OutputCache(Duration = 60, VaryByParam = "name2")]
		public ActionResult nsefuture()
		{
			return base.View();
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00023B74 File Offset: 0x00021D74
		[OutputCache(Duration = 60, VaryByParam = "name3")]
		public ActionResult cdsfuture()
		{
			return base.View();
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00023B74 File Offset: 0x00021D74
		[OutputCache(Duration = 60, VaryByParam = "name4")]
		public ActionResult option()
		{
			return base.View();
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00023B74 File Offset: 0x00021D74
		[OutputCache(Duration = 60, VaryByParam = "name5")]
		public ActionResult tradedetails()
		{
			return base.View();
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00023B74 File Offset: 0x00021D74
		[OutputCache(Duration = 60, VaryByParam = "name6")]
		public ActionResult tradedetailsapp()
		{
			return base.View();
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00023B74 File Offset: 0x00021D74
		[OutputCache(Duration = 60, VaryByParam = "name7")]
		public ActionResult marketwatchapp()
		{
			return base.View();
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00023B74 File Offset: 0x00021D74
		[OutputCache(Duration = 60, VaryByParam = "name8")]
		public ActionResult accounts()
		{
			return base.View();
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00023B74 File Offset: 0x00021D74
		[OutputCache(Duration = 60, VaryByParam = "name9")]
		public ActionResult portfolioapp()
		{
			return base.View();
		}
	}
}
