using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ATS;
using DabbaTrading.Models;
using Newtonsoft.Json;

namespace DabbaTrading.Controllers
{
	// Token: 0x02000015 RID: 21
	[EnableCors("*", "*", "*")]
	public class apiController : Controller
	{
		// Token: 0x0600017B RID: 379 RVA: 0x000038D8 File Offset: 0x00001AD8
		public string gethistorydata(string interval, string instrumenttoken, string fromdatetime, string todatetime)
		{
			string history = "";
			if (instrumenttoken != null && instrumenttoken != "null")
			{
				history = this.webPostMethod("", string.Concat(new string[]
				{
					"https://api.kite.trade/instruments/historical/",
					instrumenttoken,
					"/",
					interval,
					"?from=",
					fromdatetime,
					"&to=",
					todatetime
				}));
			}
			return history;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00003944 File Offset: 0x00001B44
		public string getprefix(string domain)
		{
			return Universal.ExecuteScalar("SELECT prefix FROM db_tradeing.tbl_domain_user where domainname='" + domain + "'").ToString();
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00003960 File Offset: 0x00001B60
		public string getcontactususerid(string domain)
		{
			return Universal.ExecuteScalar("SELECT contactus_userid FROM db_tradeing.tbl_domain_user where domainname='" + domain + "'").ToString();
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000397C File Offset: 0x00001B7C
		public string getcommondata(string domain)
		{
			domain = base.Request.Url.Host.Replace("www.", "");
			DataTable dt = Universal.SelectWithDS("SELECT ApplicationLogo,ApplicationName,userid,domainnamead,contactus_userid FROM db_tradeing.tbl_domain_user where domainname='" + domain + "'", "temp");
			List<domainuser> objlist = new List<domainuser>();
			if (dt.Rows.Count > 0)
			{
				objlist.Add(new domainuser
				{
					ApplicationLogo = dt.Rows[0]["ApplicationLogo"].ToString(),
					ApplicationName = dt.Rows[0]["ApplicationName"].ToString(),
					userid = dt.Rows[0]["userid"].ToString(),
					contactus_userid = dt.Rows[0]["contactus_userid"].ToString(),
					domainnamead = dt.Rows[0]["domainnamead"].ToString()
				});
				return new JavaScriptSerializer().Serialize(objlist);
			}
			return "";
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00003A9F File Offset: 0x00001C9F
		public string getusercount(string userid)
		{
			return Universal.ExecuteScalar("SELECT count(*) FROM db_tradeing.t_trading_all_users_master where UserName='" + userid + "'").ToString();
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00003ABC File Offset: 0x00001CBC
		public string userprofile(string UserId)
		{
			List<t_user_details> obj = new List<t_user_details>();
			DataTable dt = Universal.SelectWithDS("select * from t_trading_all_users_master where Id='" + UserId + "'", "t_trading_client_master");
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				obj.Add(new t_user_details
				{
					FirstName = dt.Rows[i]["FirstName"].ToString(),
					LastName = dt.Rows[i]["LastName"].ToString(),
					UserName = dt.Rows[i]["UserName"].ToString(),
					CreditLimit = dt.Rows[i]["CreditLimit"].ToString(),
					LedgerBalance = dt.Rows[i]["LedgerBalance"].ToString(),
					MobileNo = dt.Rows[i]["MobileNo"].ToString(),
					EmailId = dt.Rows[i]["EmailId"].ToString(),
					AadharNo = dt.Rows[i]["AadharNo"].ToString(),
					PanNo = dt.Rows[i]["PanNo"].ToString(),
					City = dt.Rows[i]["City"].ToString(),
					Address = dt.Rows[i]["Address"].ToString()
				});
			}
			return new JavaScriptSerializer().Serialize(obj);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00003C8C File Offset: 0x00001E8C
		[HttpPost]
		public string getmarketlivedata(string token)
		{
			List<marketdata_> obj = new List<marketdata_>();
			DataTable dt = Universal.SelectWithDS("select * from t_universal_tradeble_tokens where instrument_token IN(" + token + ")", "t_selected_symbols_by_user");
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				obj.Add(new marketdata_
				{
					Id = dt.Rows[i]["Id"].ToString(),
					instrument_token = dt.Rows[i]["instrument_token"].ToString(),
					symbolname = dt.Rows[i]["symbolname"].ToString(),
					last_price = dt.Rows[i]["last_price"].ToString(),
					average_price = dt.Rows[i]["average_price"].ToString(),
					open_ = dt.Rows[i]["open_"].ToString(),
					high_ = dt.Rows[i]["high_"].ToString(),
					low_ = dt.Rows[i]["low_"].ToString(),
					close_ = dt.Rows[i]["close_"].ToString(),
					change = dt.Rows[i]["change"].ToString(),
					oi = dt.Rows[i]["oi"].ToString(),
					oi_day_high = dt.Rows[i]["oi_day_high"].ToString(),
					oi_day_low = dt.Rows[i]["oi_day_low"].ToString(),
					buy = dt.Rows[i]["buy"].ToString(),
					volume = dt.Rows[i]["volume"].ToString(),
					sell = dt.Rows[i]["sell"].ToString(),
					expirydate = dt.Rows[i]["expirydate"].ToString()
				});
			}
			return new JavaScriptSerializer().Serialize(obj);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00003F20 File Offset: 0x00002120
		public string getmarketdata(string token)
		{
			List<marketdata_> obj = new List<marketdata_>();
			DataTable dt = Universal.SelectWithDS("select * from t_universal_tradeble_tokens where instrument_token IN(" + token + ")", "t_selected_symbols_by_user");
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				obj.Add(new marketdata_
				{
					Id = dt.Rows[i]["Id"].ToString(),
					instrument_token = dt.Rows[i]["instrument_token"].ToString(),
					symbolname = dt.Rows[i]["symbolname"].ToString(),
					last_price = dt.Rows[i]["last_price"].ToString(),
					average_price = dt.Rows[i]["average_price"].ToString(),
					open_ = dt.Rows[i]["open_"].ToString(),
					high_ = dt.Rows[i]["high_"].ToString(),
					low_ = dt.Rows[i]["low_"].ToString(),
					close_ = dt.Rows[i]["close_"].ToString(),
					change = dt.Rows[i]["change"].ToString(),
					oi = dt.Rows[i]["oi"].ToString(),
					oi_day_high = dt.Rows[i]["oi_day_high"].ToString(),
					oi_day_low = dt.Rows[i]["oi_day_low"].ToString(),
					buy = dt.Rows[i]["buy"].ToString(),
					volume = dt.Rows[i]["volume"].ToString(),
					sell = dt.Rows[i]["sell"].ToString(),
					expirydate = dt.Rows[i]["expirydate"].ToString()
				});
			}
			return new JavaScriptSerializer().Serialize(obj);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000041B4 File Offset: 0x000023B4
		public string getpaymentimage(string refid)
		{
			string paydata = "";
			DataTable dt = Universal.SelectWithDS(string.Concat(new string[]
			{
				"select tbl_qrimage_master.*,(select BankName from t_trading_all_users_master where Id='",
				refid,
				"') as BankName,(select AccountNo from t_trading_all_users_master where Id='",
				refid,
				"') as AccountNo,(select IFSCCode from t_trading_all_users_master where Id='",
				refid,
				"') as IFSCCode,(select AccountHolderName from t_trading_all_users_master where Id='",
				refid,
				"') as AccountHolderName from tbl_qrimage_master where AdminId='",
				refid,
				"' "
			}), "temp");
			if (dt.Rows.Count > 0)
			{
				paydata = "{" + "\"upiid\":\"" + dt.Rows[0]["UpiID"].ToString() + "\"," + "\"BankName\":\"" + dt.Rows[0]["BankName"].ToString() + "\"," + "\"AccountNo\":\"" + dt.Rows[0]["AccountNo"].ToString() + "\"," + "\"IFSCCode\":\"" + dt.Rows[0]["IFSCCode"].ToString() + "\"," + "\"AccountHolderName\":\"" + dt.Rows[0]["AccountHolderName"].ToString() + "\"," + "\"imgurl\":\"" + dt.Rows[0]["ImageUrl"].ToString() + "\"" + "}";
			}
			return paydata;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00004344 File Offset: 0x00002544
		public string getmarkettime(string Exchange, string refid)
		{
			string msg = "";
			if (Exchange == "MCX")
			{
				DataTable dt = Universal.SelectWithDS("select McxStartTrading,McxEndTrading from Settings where UserID='" + refid + "'", "Settings");
				if (dt.Rows.Count > 0)
				{
					msg = dt.Rows[0]["McxStartTrading"].ToString() + "|" + dt.Rows[0]["McxEndTrading"].ToString();
				}
			}
			else if (Exchange == "NSE")
			{
				DataTable dt2 = Universal.SelectWithDS("select EQStartTrading,EQEndTrading from Settings where UserID='" + refid + "'", "Settings");
				if (dt2.Rows.Count > 0)
				{
					msg = dt2.Rows[0]["EQStartTrading"].ToString() + "|" + dt2.Rows[0]["EQEndTrading"].ToString();
				}
			}
			else if (Exchange == "CDS" || Exchange == "OPT")
			{
				DataTable dt3 = Universal.SelectWithDS("select CdsStartTrading,CdsEndTrading from Settings where UserID='" + refid + "'", "Settings");
				if (dt3.Rows.Count > 0)
				{
					msg = dt3.Rows[0]["CdsStartTrading"].ToString() + "|" + dt3.Rows[0]["CdsEndTrading"].ToString();
				}
			}
			return msg;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000044DC File Offset: 0x000026DC
		public string getsinglemarketdata(string token)
		{
			marketdata_ cmd = new marketdata_();
			DataTable dt = Universal.SelectWithDS("select * from t_universal_tradeble_tokens where instrument_token IN(" + token + ")", "t_selected_symbols_by_user");
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				cmd.Id = dt.Rows[i]["Id"].ToString();
				cmd.instrument_token = dt.Rows[i]["instrument_token"].ToString();
				cmd.symbolname = dt.Rows[i]["symbolname"].ToString();
				cmd.last_price = dt.Rows[i]["last_price"].ToString();
				cmd.average_price = dt.Rows[i]["average_price"].ToString();
				cmd.open_ = dt.Rows[i]["open_"].ToString();
				cmd.high_ = dt.Rows[i]["high_"].ToString();
				cmd.low_ = dt.Rows[i]["low_"].ToString();
				cmd.close_ = dt.Rows[i]["close_"].ToString();
				cmd.change = dt.Rows[i]["change"].ToString();
				cmd.oi = dt.Rows[i]["oi"].ToString();
				cmd.oi_day_high = dt.Rows[i]["oi_day_high"].ToString();
				cmd.oi_day_low = dt.Rows[i]["oi_day_low"].ToString();
				cmd.buy = dt.Rows[i]["buy"].ToString();
				cmd.volume = dt.Rows[i]["volume"].ToString();
				cmd.sell = dt.Rows[i]["sell"].ToString();
				cmd.expirydate = dt.Rows[i]["expirydate"].ToString();
			}
			return new JavaScriptSerializer().Serialize(cmd);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00004764 File Offset: 0x00002964
		public string deleteorder(string orderid)
		{
			string result;
			try
			{
				DataTable dt = Universal.SelectWithDS("select UserId,UserName,Lot,ScriptName,OrderCategory,OrderPrice from t_user_order where Id='" + orderid + "'", "t_user_order");
				string userid = "";
				string username = "";
				string lot = "";
				string scriptname = "";
				string ordercategory = "";
				string orderprice = "";
				if (dt.Rows.Count > 0)
				{
					userid = dt.Rows[0]["UserId"].ToString();
					username = dt.Rows[0]["UserName"].ToString();
					lot = dt.Rows[0]["Lot"].ToString();
					scriptname = dt.Rows[0]["ScriptName"].ToString();
					ordercategory = dt.Rows[0]["OrderCategory"].ToString();
					orderprice = dt.Rows[0]["OrderPrice"].ToString();
				}
				if (Universal.ExecuteNonQuery("update t_user_order set OrderStatus='Cancel' where Id='" + orderid + "'") == 1)
				{
					string msg = string.Concat(new string[]
					{
						username,
						"(",
						userid,
						") have Cancelled Order ID: ",
						orderid,
						" of ",
						lot,
						" Lots of ",
						scriptname,
						" to ",
						ordercategory,
						" at ",
						orderprice,
						". Cancelled by Trader."
					});
					IPAddress externalIp = IPAddress.Parse(Universal.devip);
					string[] array = new string[11];
					array[0] = "insert into t_action_master (Message,ActionDate,ActionTime,ActionFrom,IpAddress) values('";
					array[1] = msg;
					array[2] = "','";
					array[3] = Universal.GetDate;
					array[4] = "','";
					array[5] = Universal.GetTime;
					array[6] = "','";
					array[7] = userid;
					array[8] = "','";
					int num = 9;
					IPAddress ipaddress = externalIp;
					array[num] = ((ipaddress != null) ? ipaddress.ToString() : null);
					array[10] = "')";
					Universal.ExecuteNonQuery(string.Concat(array));
					result = "success";
				}
				else
				{
					result = "error";
				}
			}
			catch (Exception ex)
			{
				result = ex.Message;
			}
			return result;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000049A8 File Offset: 0x00002BA8
		[HttpPost]
		public bool savesltp(string TradeId, string SL, string TP)
		{
			Universal.ExecuteNonQuery("delete from tbl_trade_user where TradeId='" + TradeId + "'");
			return Universal.ExecuteNonQuery(string.Concat(new string[]
			{
				"insert into tbl_trade_user (TradeId,SL,TP,DateTime,Status) values('",
				TradeId,
				"','",
				SL,
				"','",
				TP,
				"','",
				Universal.GetDate,
				" ",
				Universal.GetTime,
				"','Pending')"
			})) == 1;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00004A2D File Offset: 0x00002C2D
		[HttpPost]
		public bool deletesltp(string TradeId)
		{
			return Universal.ExecuteNonQuery("delete from tbl_trade_user where TradeId='" + TradeId + "'") == 1;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00004A47 File Offset: 0x00002C47
		public string getsltp(string UserId)
		{
			return JsonConvert.SerializeObject(Universal.SelectWithDS("select tbl_trade_user.*,t_user_order.ScriptName,TokenNo,OrderDate,OrderTime,selectedlotsize,Lot,MarginUsed,OrderStatus,OrderCategory,OrderPrice,BroughtBy,P_L,Brokerage,ClosedAt,ClosedTime from t_user_order INNER JOIN tbl_trade_user ON t_user_order.Id=tbl_trade_user.TradeId where t_user_order.UserId='" + UserId + "'", "dt"));
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00004A68 File Offset: 0x00002C68
		public string canceleorderfromapp(string orderid)
		{
			string result;
			try
			{
				DataTable dt = Universal.SelectWithDS("select UserId,UserName,Lot,ScriptName,OrderCategory,OrderPrice from t_user_order where Id='" + orderid + "'", "t_user_order");
				string userid = "";
				string username = "";
				string lot = "";
				string scriptname = "";
				string ordercategory = "";
				string orderprice = "";
				if (dt.Rows.Count > 0)
				{
					userid = dt.Rows[0]["UserId"].ToString();
					username = dt.Rows[0]["UserName"].ToString();
					lot = dt.Rows[0]["Lot"].ToString();
					scriptname = dt.Rows[0]["ScriptName"].ToString();
					ordercategory = dt.Rows[0]["OrderCategory"].ToString();
					orderprice = dt.Rows[0]["OrderPrice"].ToString();
				}
				if (Universal.ExecuteNonQuery("delete from t_user_order where Id='" + orderid + "'") == 1)
				{
					string msg = string.Concat(new string[]
					{
						username,
						"(",
						userid,
						") have Cancelled Order ID: ",
						orderid,
						" of ",
						lot,
						" Lots of ",
						scriptname,
						" to ",
						ordercategory,
						" at ",
						orderprice,
						". Cancelled by Trader."
					});
					string msgapp = string.Concat(new string[]
					{
						ordercategory,
						" order of ",
						scriptname,
						" (",
						lot,
						") lots has been canceled successfully"
					});
					IPAddress externalIp = IPAddress.Parse(Universal.devip);
					string[] array = new string[11];
					array[0] = "insert into t_action_master (Message,ActionDate,ActionTime,ActionFrom,IpAddress) values('";
					array[1] = msg;
					array[2] = "','";
					array[3] = Universal.GetDate;
					array[4] = "','";
					array[5] = Universal.GetTime;
					array[6] = "','";
					array[7] = userid;
					array[8] = "','";
					int num = 9;
					IPAddress ipaddress = externalIp;
					array[num] = ((ipaddress != null) ? ipaddress.ToString() : null);
					array[10] = "')";
					Universal.ExecuteNonQuery(string.Concat(array));
					result = "{" + "\"ResponseCode\":\"200\"," + "\"ResponseMessage\":\"" + msgapp + "\"" + "}";
				}
				else
				{
					result = "{" + "\"ResponseCode\":\"201\"," + "\"ResponseMessage\":\"Error In Removing Order\"" + "}";
				}
			}
			catch (Exception ex)
			{
				result = ex.Message;
			}
			return result;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00004D24 File Offset: 0x00002F24
		public string getnotification(string userid, string refid)
		{
			List<usernotification> nlist = new List<usernotification>();
			DateTime.Now.Year.ToString();
			DateTime startdate = this.FirstDayOfWeek(DateTime.Now);
			DateTime enddate = this.LastDayOfWeek(DateTime.Now);
			string sdate = string.Concat(new string[]
			{
				startdate.Year.ToString(),
				"-",
				startdate.Month.ToString(),
				"-",
				startdate.Day.ToString()
			});
			string edate = string.Concat(new string[]
			{
				enddate.Year.ToString(),
				"-",
				enddate.Month.ToString(),
				"-",
				enddate.Day.ToString()
			});
			DataTable dt = Universal.SelectWithDS(string.Concat(new string[]
			{
				"select Title,Message,DATE_FORMAT(CreatedDate,'%d/%m/%Y %h:%i') as CreatedDate from t_Notification where (Date(CreatedDate) between '",
				sdate,
				"' and '",
				edate,
				"') and (UserId='",
				userid,
				"' or UserId='-1') and CreatedBy='",
				refid,
				"' order by CreatedDate"
			}), "t_Notification");
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				usernotification obj = new usernotification();
				string Title = dt.Rows[i]["Title"].ToString();
				string Message = dt.Rows[i]["Message"].ToString();
				string CreatedDate = dt.Rows[i]["CreatedDate"].ToString();
				obj.Title = Title;
				obj.Message = Message;
				obj.CreatedDate = CreatedDate;
				nlist.Add(obj);
			}
			return new JavaScriptSerializer().Serialize(nlist);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00004F16 File Offset: 0x00003116
		public void userurl(string url)
		{
			Universal.ExecuteNonQuery("insert into useraccess (link) values('" + url + "')");
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00004F30 File Offset: 0x00003130
		[HttpPost]
		public string authlogin(string username, string password)
		{
			DataTable dt = Universal.SelectWithDS(string.Concat(new string[]
			{
				"select * from t_trading_all_users_master INNER JOIN settings ON t_trading_all_users_master.Refid=settings.UserID where UserName='",
				username,
				"' and password_='",
				password,
				"' And Type_='7'"
			}), "t_trading_client_master");
			if (dt.Rows.Count <= 0)
			{
				return "{" + "\"ResponseCode\":\"201\"," + "\"ResponseMessage\":\"Auth Failed\"" + "}";
			}
			if (!(dt.Rows[0]["Account_status"].ToString() == "true"))
			{
				return "{" + "\"ResponseCode\":\"201\"," + "\"ResponseMessage\":\"Auth Failed\"" + "}";
			}
			if (dt.Rows[0]["FirstTimeLogin"].ToString() == "true")
			{
				Universal.ExecuteNonQuery("update t_trading_all_users_master set FirstTimeLogin='false' where Id='" + dt.Rows[0]["Id"].ToString() + "'");
				return "{" + "\"ResponseCode\":\"200\"," + "\"FirstTimeLogin\":\"true\"," + "\"UserId\":\"" + dt.Rows[0]["Id"].ToString() + "\"," + "\"FirstName\":\"" + dt.Rows[0]["FirstName"].ToString() + "\"," + "\"LastName\":\"" + dt.Rows[0]["LastName"].ToString() + "\"," + "\"UserName\":\"" + dt.Rows[0]["UserName"].ToString() + "\"," + "\"Password_\":\"" + dt.Rows[0]["Password_"].ToString() + "\"," + "\"AadharNo\":\"" + dt.Rows[0]["AadharNo"].ToString() + "\"," + "\"PanNo\":\"" + dt.Rows[0]["PanNo"].ToString() + "\"," + "\"Address\":\"" + dt.Rows[0]["Address"].ToString() + "\"," + "\"City\":\"" + dt.Rows[0]["City"].ToString() + "\"," + "\"MobileNo\":\"" + dt.Rows[0]["MobileNo"].ToString() + "\"," + "\"EmailId\":\"" + dt.Rows[0]["EmailId"].ToString() + "\"," + "\"LedgerBalance\":\"" + dt.Rows[0]["LedgerBalance"].ToString() + "\"," + "\"CreditLimit\":\"" + dt.Rows[0]["CreditLimit"].ToString() + "\"," + "\"profittradestoptime\":\"" + dt.Rows[0]["profittradestoptime"].ToString() + "\"," + "\"Type_\":\"" + dt.Rows[0]["Type_"].ToString() + "\"," + "\"RefId\":\"" + dt.Rows[0]["RefId"].ToString() + "\"," + "\"ValidTill\":\"" + dt.Rows[0]["ValidTill"].ToString().Replace("12:00:00 AM", "").Replace("00:00:00", "").Replace("\n", "").Replace("\r", "") + "\"," + "\"Account_status\":\"" + dt.Rows[0]["Account_status"].ToString() + "\"," + "\"GrossPL\":\"" + dt.Rows[0]["GrossPL"].ToString() + "\"," + "\"Brokerage\":\"" + dt.Rows[0]["Brokerage"].ToString() + "\"," + "\"NetPL\":\"" + dt.Rows[0]["NetPL"].ToString() + "\"," + "\"MaxUserAllowed\":\"" + dt.Rows[0]["MaxUserAllowed"].ToString() + "\"," + "\"Maximum_no_of_trade_allowed\":\"" + dt.Rows[0]["Maximum_no_of_trade_allowed"].ToString() + "\"," + "\"IsOnline\":\"" + dt.Rows[0]["IsOnline"].ToString() + "\"," + "\"AllowOrdersCurrentBid\":\"" + dt.Rows[0]["AllowOrdersCurrentBid"].ToString() + "\"," + "\"AllowFreshEntryHighAndBelow\":\"" + dt.Rows[0]["AllowFreshEntryHighAndBelow"].ToString() + "\"," + "\"DemoAccount\":\"" + dt.Rows[0]["DemoAccount"].ToString() + "\"," + "\"OneSideBrokerageIntraday\":\"" + dt.Rows[0]["OneSideBrokerageIntraday"].ToString() + "\"," + "\"AutoCloseTradesLossesLimit\":\"" + dt.Rows[0]["AutoCloseTradesLossesLimit"].ToString() + "\"," + "\"AutoCloseTradesLossesInsufficient\":\"" + dt.Rows[0]["AutoCloseTradesLossesInsufficient"].ToString() + "\"," + "\"AllowOrdersHighLow\":\"" + dt.Rows[0]["AllowOrdersHighLow"].ToString() + "\"," + "\"TriggersOrdersHighLow\":\"" + dt.Rows[0]["TriggersOrdersHighLow"].ToString() + "\"," + "\"TradeEquityUnits\":\"" + dt.Rows[0]["TradeEquityUnits"].ToString() + "\"," + "\"TradeMCXUnits\":\"" + dt.Rows[0]["TradeMCXUnits"].ToString() + "\"," + "\"TradeCDSUnits\":\"" + dt.Rows[0]["TradeCDSUnits"].ToString() + "\"," + "\"auto_close_all_active_trades_when_the_losses_reach\":\"" + dt.Rows[0]["auto_close_all_active_trades_when_the_losses_reach"].ToString() + "\"," + "\"Notify_client_when_the_losses_reach\":\"" + dt.Rows[0]["Notify_client_when_the_losses_reach"].ToString() + "\"," + "\"Profit_loss_Share_to_broker\":\"" + dt.Rows[0]["Profit_loss_Share_to_broker"].ToString() + "\"," + "\"IsMCXTrade\":\"" + dt.Rows[0]["IsMCXTrade"].ToString() + "\"," + "\"Mcx_Brokerage_Type\":\"" + dt.Rows[0]["Mcx_Brokerage_Type"].ToString() + "\"," + "\"MCX_brokerage_per_crore\":\"" + dt.Rows[0]["MCX_brokerage_per_crore"].ToString() + "\"," + "\"Intraday_Exposure_Margin_MCX\":\"" + dt.Rows[0]["Intraday_Exposure_Margin_MCX"].ToString() + "\"," + "\"Holding_Exposure_Margin_MCX\":\"" + dt.Rows[0]["Holding_Exposure_Margin_MCX"].ToString() + "\"," + "\"Minimum_lot_size_required_per_single_trade_of_MCX\":\"" + dt.Rows[0]["Minimum_lot_size_required_per_single_trade_of_MCX"].ToString() + "\"," + "\"Maximum_lot_size_allowed_per_single_trade_of_MCX\":\"" + dt.Rows[0]["Maximum_lot_size_allowed_per_single_trade_of_MCX"].ToString() + "\"," + "\"Maximum_lot_size_allowed_per_script_of_MCX_to_be\":\"" + dt.Rows[0]["Maximum_lot_size_allowed_per_script_of_MCX_to_be"].ToString() + "\"," + "\"Maximum_lot_size_allowed_overall_in_MCX_to_be\":\"" + dt.Rows[0]["Maximum_lot_size_allowed_overall_in_MCX_to_be"].ToString() + "\"," + "\"BULLDEX_brokerage\":\"" + dt.Rows[0]["BULLDEX_brokerage"].ToString() + "\"," + "\"GOLD_brokerage\":\"" + dt.Rows[0]["GOLD_brokerage"].ToString() + "\"," + "\"SILVER_brokerage\":\"" + dt.Rows[0]["SILVER_brokerage"].ToString() + "\"," + "\"CRUDEOIL_brokerage\":\"" + dt.Rows[0]["CRUDEOIL_brokerage"].ToString() + "\"," + "\"COPPER_brokerage\":\"" + dt.Rows[0]["COPPER_brokerage"].ToString() + "\"," + "\"NICKEL_brokerage\":\"" + dt.Rows[0]["NICKEL_brokerage"].ToString() + "\"," + "\"ZINC_brokerage\":\"" + dt.Rows[0]["ZINC_brokerage"].ToString() + "\"," + "\"LEAD_brokerage\":\"" + dt.Rows[0]["LEAD_brokerage"].ToString() + "\"," + "\"NATURALGAS_brokerage\":\"" + dt.Rows[0]["NATURALGAS_brokerage"].ToString() + "\"," + "\"ALUMINIUM_brokerage\":\"" + dt.Rows[0]["ALUMINIUM_brokerage"].ToString() + "\"," + "\"MENTHAOIL_brokerage\":\"" + dt.Rows[0]["MENTHAOIL_brokerage"].ToString() + "\"," + "\"COTTON_brokerage\":\"" + dt.Rows[0]["COTTON_brokerage"].ToString() + "\"," + "\"CPO_brokerage\":\"" + dt.Rows[0]["CPO_brokerage"].ToString() + "\"," + "\"GOLDM_brokerage\":\"" + dt.Rows[0]["GOLDM_brokerage"].ToString() + "\"," + "\"SILVERM_brokerage\":\"" + dt.Rows[0]["SILVERM_brokerage"].ToString() + "\"," + "\"SILVERMIC_brokerage\":\"" + dt.Rows[0]["SILVERMIC_brokerage"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_BULLDEX_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_BULLDEX_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_BULLDEX_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_BULLDEX_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_GOLD_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_GOLD_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_GOLD_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_GOLD_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_SILVER_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_SILVER_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_SILVER_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_SILVER_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_CRUDEOIL_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_CRUDEOIL_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_CRUDEOIL_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_CRUDEOIL_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_COPPER_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_COPPER_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_COPPER_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_COPPER_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_NICKEL_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_NICKEL_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_NICKEL_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_NICKEL_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_ZINC_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_ZINC_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_ZINC_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_ZINC_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_LEAD_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_LEAD_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_LEAD_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_LEAD_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_NATURALGAS_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_NATURALGAS_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_NATURALGAS_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_NATURALGAS_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_ALUMINIUM_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_ALUMINIUM_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_ALUMINIUM_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_ALUMINIUM_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_MENTHAOIL_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_MENTHAOIL_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_MENTHAOIL_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_MENTHAOIL_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_COTTON_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_COTTON_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_COTTON_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_COTTON_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_CPO_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_CPO_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_CPO_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_CPO_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_GOLDM_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_GOLDM_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_GOLDM_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_GOLDM_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_SILVERM_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_SILVERM_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_SILVERM_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_SILVERM_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_SILVERMIC_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_SILVERMIC_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_SILVERMIC_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_SILVERMIC_Holding"].ToString() + "\"," + "\"IsNSETrade\":\"" + dt.Rows[0]["IsNSETrade"].ToString() + "\"," + "\"Equity_brokerage_per_crore\":\"" + dt.Rows[0]["Equity_brokerage_per_crore"].ToString() + "\"," + "\"NSE_Exposure_Type\":\"" + dt.Rows[0]["NSE_Exposure_Type"].ToString() + "\"," + "\"Intraday_Exposure_Margin_Equity\":\"" + dt.Rows[0]["Intraday_Exposure_Margin_Equity"].ToString() + "\"," + "\"Orders_to_be_away_by_from_current_price_Equity\":\"" + dt.Rows[0]["Orders_to_be_away_by_from_current_price_Equity"].ToString() + "\"," + "\"Minimum_lot_size_required_per_single_trade_of_Equity_Futures\":\"" + dt.Rows[0]["Minimum_lot_size_required_per_single_trade_of_Equity_Futures"].ToString() + "\"," + "\"Maximum_lot_size_allowed_per_single_trade_of_Equity_Futures\":\"" + dt.Rows[0]["Maximum_lot_size_allowed_per_single_trade_of_Equity_Futures"].ToString() + "\"," + "\"Min_lot_size_reqd_per_single_trade_of_Equity_Futures_INDEX\":\"" + dt.Rows[0]["Min_lot_size_reqd_per_single_trade_of_Equity_Futures_INDEX"].ToString() + "\"," + "\"Max_lot_size_allw_per_single_trade_of_Equity_Futures_INDEX\":\"" + dt.Rows[0]["Max_lot_size_allw_per_single_trade_of_Equity_Futures_INDEX"].ToString() + "\"," + "\"Maxi_lt_sz_ald_per_scrt_of_Eqty_to_be_acty_open_a_time\":\"" + dt.Rows[0]["Maxi_lt_sz_ald_per_scrt_of_Eqty_to_be_acty_open_a_time"].ToString() + "\"," + "\"Maxi_lt_sz_ald_per_scpt_of_Eqty_INDEX_to_be_acty_opn_a_time\":\"" + dt.Rows[0]["Maxi_lt_sz_ald_per_scpt_of_Eqty_INDEX_to_be_acty_opn_a_time"].ToString() + "\"," + "\"Max_lot_size_alld_overall_in_Equity_to_be_actively\":\"" + dt.Rows[0]["Max_lot_size_alld_overall_in_Equity_to_be_actively"].ToString() + "\"," + "\"Max_lot_size_alld_ovl_in_Equity_INDEX_to_be_actively\":\"" + dt.Rows[0]["Max_lot_size_alld_ovl_in_Equity_INDEX_to_be_actively"].ToString() + "\"," + "\"IsCDSTrade\":\"" + dt.Rows[0]["IsCDSTrade"].ToString() + "\"," + "\"Intraday_Exposure_Margin_CDS\":\"" + dt.Rows[0]["Intraday_Exposure_Margin_CDS"].ToString() + "\"," + "\"Holding_Exposure_Margin_CDS\":\"" + dt.Rows[0]["Holding_Exposure_Margin_CDS"].ToString() + "\"," + "\"CDS_Brokerage_Type\":\"" + dt.Rows[0]["CDS_Brokerage_Type"].ToString() + "\"," + "\"CDS_brokerage_per_crore\":\"" + dt.Rows[0]["CDS_brokerage_per_crore"].ToString() + "\"," + "\"CDS_Exposure_Type\":\"" + dt.Rows[0]["CDS_Exposure_Type"].ToString() + "\"," + "\"Minimum_lot_size_required_per_single_of_CDS\":\"" + dt.Rows[0]["Minimum_lot_size_required_per_single_of_CDS"].ToString() + "\"," + "\"Max_lot_size_ald_per_single_CDS_Futures\":\"" + dt.Rows[0]["Max_lot_size_ald_per_single_CDS_Futures"].ToString() + "\"," + "\"mlsa_per_script_of_CDS_to_be_actively_open_at_a_time\":\"" + dt.Rows[0]["mlsa_per_script_of_CDS_to_be_actively_open_at_a_time"].ToString() + "\"," + "\"mlsa_overall_in_CDS_to_be_actively_open_at_a_time\":\"" + dt.Rows[0]["mlsa_overall_in_CDS_to_be_actively_open_at_a_time"].ToString() + "\"," + "\"McxStartTrading\":\"" + dt.Rows[0]["McxStartTrading"].ToString() + "\"," + "\"McxEndTrading\":\"" + dt.Rows[0]["McxEndTrading"].ToString() + "\"," + "\"EQStartTrading\":\"" + dt.Rows[0]["EQStartTrading"].ToString() + "\"," + "\"EQEndTrading\":\"" + dt.Rows[0]["EQEndTrading"].ToString() + "\"," + "\"CdsStartTrading\":\"" + dt.Rows[0]["CdsStartTrading"].ToString() + "\"," + "\"CdsEndTrading\":\"" + dt.Rows[0]["CdsEndTrading"].ToString() + "\"" + "}";
			}
			return "{" + "\"ResponseCode\":\"200\"," + "\"FirstTimeLogin\":\"false\"," + "\"UserId\":\"" + dt.Rows[0]["Id"].ToString() + "\"," + "\"FirstName\":\"" + dt.Rows[0]["FirstName"].ToString() + "\"," + "\"LastName\":\"" + dt.Rows[0]["LastName"].ToString() + "\"," + "\"UserName\":\"" + dt.Rows[0]["UserName"].ToString() + "\"," + "\"Password_\":\"" + dt.Rows[0]["Password_"].ToString() + "\"," + "\"AadharNo\":\"" + dt.Rows[0]["AadharNo"].ToString() + "\"," + "\"PanNo\":\"" + dt.Rows[0]["PanNo"].ToString() + "\"," + "\"Address\":\"" + dt.Rows[0]["Address"].ToString() + "\"," + "\"City\":\"" + dt.Rows[0]["City"].ToString() + "\"," + "\"MobileNo\":\"" + dt.Rows[0]["MobileNo"].ToString() + "\"," + "\"EmailId\":\"" + dt.Rows[0]["EmailId"].ToString() + "\"," + "\"LedgerBalance\":\"" + dt.Rows[0]["LedgerBalance"].ToString() + "\"," + "\"CreditLimit\":\"" + dt.Rows[0]["CreditLimit"].ToString() + "\"," + "\"profittradestoptime\":\"" + dt.Rows[0]["profittradestoptime"].ToString() + "\"," + "\"Type_\":\"" + dt.Rows[0]["Type_"].ToString() + "\"," + "\"RefId\":\"" + dt.Rows[0]["RefId"].ToString() + "\"," + "\"ValidTill\":\"" + dt.Rows[0]["ValidTill"].ToString().ToString().Replace("12:00:00 AM", "").Replace("00:00:00", "").Replace("\n", "").Replace("\r", "") + "\"," + "\"Account_status\":\"" + dt.Rows[0]["Account_status"].ToString() + "\"," + "\"GrossPL\":\"" + dt.Rows[0]["GrossPL"].ToString() + "\"," + "\"Brokerage\":\"" + dt.Rows[0]["Brokerage"].ToString() + "\"," + "\"NetPL\":\"" + dt.Rows[0]["NetPL"].ToString() + "\"," + "\"MaxUserAllowed\":\"" + dt.Rows[0]["MaxUserAllowed"].ToString() + "\"," + "\"Maximum_no_of_trade_allowed\":\"" + dt.Rows[0]["Maximum_no_of_trade_allowed"].ToString() + "\"," + "\"IsOnline\":\"" + dt.Rows[0]["IsOnline"].ToString() + "\"," + "\"AllowOrdersCurrentBid\":\"" + dt.Rows[0]["AllowOrdersCurrentBid"].ToString() + "\"," + "\"AllowFreshEntryHighAndBelow\":\"" + dt.Rows[0]["AllowFreshEntryHighAndBelow"].ToString() + "\"," + "\"DemoAccount\":\"" + dt.Rows[0]["DemoAccount"].ToString() + "\"," + "\"OneSideBrokerageIntraday\":\"" + dt.Rows[0]["OneSideBrokerageIntraday"].ToString() + "\"," + "\"AutoCloseTradesLossesLimit\":\"" + dt.Rows[0]["AutoCloseTradesLossesLimit"].ToString() + "\"," + "\"AutoCloseTradesLossesInsufficient\":\"" + dt.Rows[0]["AutoCloseTradesLossesInsufficient"].ToString() + "\"," + "\"AllowOrdersHighLow\":\"" + dt.Rows[0]["AllowOrdersHighLow"].ToString() + "\"," + "\"TriggersOrdersHighLow\":\"" + dt.Rows[0]["TriggersOrdersHighLow"].ToString() + "\"," + "\"TradeEquityUnits\":\"" + dt.Rows[0]["TradeEquityUnits"].ToString() + "\"," + "\"TradeMCXUnits\":\"" + dt.Rows[0]["TradeMCXUnits"].ToString() + "\"," + "\"TradeCDSUnits\":\"" + dt.Rows[0]["TradeCDSUnits"].ToString() + "\"," + "\"auto_close_all_active_trades_when_the_losses_reach\":\"" + dt.Rows[0]["auto_close_all_active_trades_when_the_losses_reach"].ToString() + "\"," + "\"Notify_client_when_the_losses_reach\":\"" + dt.Rows[0]["Notify_client_when_the_losses_reach"].ToString() + "\"," + "\"Profit_loss_Share_to_broker\":\"" + dt.Rows[0]["Profit_loss_Share_to_broker"].ToString() + "\"," + "\"IsMCXTrade\":\"" + dt.Rows[0]["IsMCXTrade"].ToString() + "\"," + "\"Mcx_Brokerage_Type\":\"" + dt.Rows[0]["Mcx_Brokerage_Type"].ToString() + "\"," + "\"MCX_brokerage_per_crore\":\"" + dt.Rows[0]["MCX_brokerage_per_crore"].ToString() + "\"," + "\"Intraday_Exposure_Margin_MCX\":\"" + dt.Rows[0]["Intraday_Exposure_Margin_MCX"].ToString() + "\"," + "\"Holding_Exposure_Margin_MCX\":\"" + dt.Rows[0]["Holding_Exposure_Margin_MCX"].ToString() + "\"," + "\"Minimum_lot_size_required_per_single_trade_of_MCX\":\"" + dt.Rows[0]["Minimum_lot_size_required_per_single_trade_of_MCX"].ToString() + "\"," + "\"Maximum_lot_size_allowed_per_single_trade_of_MCX\":\"" + dt.Rows[0]["Maximum_lot_size_allowed_per_single_trade_of_MCX"].ToString() + "\"," + "\"Maximum_lot_size_allowed_per_script_of_MCX_to_be\":\"" + dt.Rows[0]["Maximum_lot_size_allowed_per_script_of_MCX_to_be"].ToString() + "\"," + "\"Maximum_lot_size_allowed_overall_in_MCX_to_be\":\"" + dt.Rows[0]["Maximum_lot_size_allowed_overall_in_MCX_to_be"].ToString() + "\"," + "\"BULLDEX_brokerage\":\"" + dt.Rows[0]["BULLDEX_brokerage"].ToString() + "\"," + "\"GOLD_brokerage\":\"" + dt.Rows[0]["GOLD_brokerage"].ToString() + "\"," + "\"SILVER_brokerage\":\"" + dt.Rows[0]["SILVER_brokerage"].ToString() + "\"," + "\"CRUDEOIL_brokerage\":\"" + dt.Rows[0]["CRUDEOIL_brokerage"].ToString() + "\"," + "\"COPPER_brokerage\":\"" + dt.Rows[0]["COPPER_brokerage"].ToString() + "\"," + "\"NICKEL_brokerage\":\"" + dt.Rows[0]["NICKEL_brokerage"].ToString() + "\"," + "\"ZINC_brokerage\":\"" + dt.Rows[0]["ZINC_brokerage"].ToString() + "\"," + "\"LEAD_brokerage\":\"" + dt.Rows[0]["LEAD_brokerage"].ToString() + "\"," + "\"NATURALGAS_brokerage\":\"" + dt.Rows[0]["NATURALGAS_brokerage"].ToString() + "\"," + "\"ALUMINIUM_brokerage\":\"" + dt.Rows[0]["ALUMINIUM_brokerage"].ToString() + "\"," + "\"MENTHAOIL_brokerage\":\"" + dt.Rows[0]["MENTHAOIL_brokerage"].ToString() + "\"," + "\"COTTON_brokerage\":\"" + dt.Rows[0]["COTTON_brokerage"].ToString() + "\"," + "\"CPO_brokerage\":\"" + dt.Rows[0]["CPO_brokerage"].ToString() + "\"," + "\"GOLDM_brokerage\":\"" + dt.Rows[0]["GOLDM_brokerage"].ToString() + "\"," + "\"SILVERM_brokerage\":\"" + dt.Rows[0]["SILVERM_brokerage"].ToString() + "\"," + "\"SILVERMIC_brokerage\":\"" + dt.Rows[0]["SILVERMIC_brokerage"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_BULLDEX_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_BULLDEX_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_BULLDEX_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_BULLDEX_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_GOLD_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_GOLD_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_GOLD_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_GOLD_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_SILVER_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_SILVER_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_SILVER_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_SILVER_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_CRUDEOIL_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_CRUDEOIL_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_CRUDEOIL_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_CRUDEOIL_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_COPPER_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_COPPER_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_COPPER_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_COPPER_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_NICKEL_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_NICKEL_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_NICKEL_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_NICKEL_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_ZINC_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_ZINC_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_ZINC_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_ZINC_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_LEAD_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_LEAD_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_LEAD_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_LEAD_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_NATURALGAS_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_NATURALGAS_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_NATURALGAS_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_NATURALGAS_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_ALUMINIUM_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_ALUMINIUM_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_ALUMINIUM_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_ALUMINIUM_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_MENTHAOIL_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_MENTHAOIL_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_MENTHAOIL_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_MENTHAOIL_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_COTTON_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_COTTON_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_COTTON_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_COTTON_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_CPO_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_CPO_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_CPO_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_CPO_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_GOLDM_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_GOLDM_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_GOLDM_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_GOLDM_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_SILVERM_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_SILVERM_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_SILVERM_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_SILVERM_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_SILVERMIC_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_SILVERMIC_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_SILVERMIC_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_SILVERMIC_Holding"].ToString() + "\"," + "\"IsNSETrade\":\"" + dt.Rows[0]["IsNSETrade"].ToString() + "\"," + "\"Equity_brokerage_per_crore\":\"" + dt.Rows[0]["Equity_brokerage_per_crore"].ToString() + "\"," + "\"NSE_Exposure_Type\":\"" + dt.Rows[0]["NSE_Exposure_Type"].ToString() + "\"," + "\"Intraday_Exposure_Margin_Equity\":\"" + dt.Rows[0]["Intraday_Exposure_Margin_Equity"].ToString() + "\"," + "\"Orders_to_be_away_by_from_current_price_Equity\":\"" + dt.Rows[0]["Orders_to_be_away_by_from_current_price_Equity"].ToString() + "\"," + "\"Minimum_lot_size_required_per_single_trade_of_Equity_Futures\":\"" + dt.Rows[0]["Minimum_lot_size_required_per_single_trade_of_Equity_Futures"].ToString() + "\"," + "\"Maximum_lot_size_allowed_per_single_trade_of_Equity_Futures\":\"" + dt.Rows[0]["Maximum_lot_size_allowed_per_single_trade_of_Equity_Futures"].ToString() + "\"," + "\"Min_lot_size_reqd_per_single_trade_of_Equity_Futures_INDEX\":\"" + dt.Rows[0]["Min_lot_size_reqd_per_single_trade_of_Equity_Futures_INDEX"].ToString() + "\"," + "\"Max_lot_size_allw_per_single_trade_of_Equity_Futures_INDEX\":\"" + dt.Rows[0]["Max_lot_size_allw_per_single_trade_of_Equity_Futures_INDEX"].ToString() + "\"," + "\"Maxi_lt_sz_ald_per_scrt_of_Eqty_to_be_acty_open_a_time\":\"" + dt.Rows[0]["Maxi_lt_sz_ald_per_scrt_of_Eqty_to_be_acty_open_a_time"].ToString() + "\"," + "\"Maxi_lt_sz_ald_per_scpt_of_Eqty_INDEX_to_be_acty_opn_a_time\":\"" + dt.Rows[0]["Maxi_lt_sz_ald_per_scpt_of_Eqty_INDEX_to_be_acty_opn_a_time"].ToString() + "\"," + "\"Max_lot_size_alld_overall_in_Equity_to_be_actively\":\"" + dt.Rows[0]["Max_lot_size_alld_overall_in_Equity_to_be_actively"].ToString() + "\"," + "\"Max_lot_size_alld_ovl_in_Equity_INDEX_to_be_actively\":\"" + dt.Rows[0]["Max_lot_size_alld_ovl_in_Equity_INDEX_to_be_actively"].ToString() + "\"," + "\"IsCDSTrade\":\"" + dt.Rows[0]["IsCDSTrade"].ToString() + "\"," + "\"Intraday_Exposure_Margin_CDS\":\"" + dt.Rows[0]["Intraday_Exposure_Margin_CDS"].ToString() + "\"," + "\"Holding_Exposure_Margin_CDS\":\"" + dt.Rows[0]["Holding_Exposure_Margin_CDS"].ToString() + "\"," + "\"CDS_Brokerage_Type\":\"" + dt.Rows[0]["CDS_Brokerage_Type"].ToString() + "\"," + "\"CDS_brokerage_per_crore\":\"" + dt.Rows[0]["CDS_brokerage_per_crore"].ToString() + "\"," + "\"CDS_Exposure_Type\":\"" + dt.Rows[0]["CDS_Exposure_Type"].ToString() + "\"," + "\"Minimum_lot_size_required_per_single_of_CDS\":\"" + dt.Rows[0]["Minimum_lot_size_required_per_single_of_CDS"].ToString() + "\"," + "\"Max_lot_size_ald_per_single_CDS_Futures\":\"" + dt.Rows[0]["Max_lot_size_ald_per_single_CDS_Futures"].ToString() + "\"," + "\"mlsa_per_script_of_CDS_to_be_actively_open_at_a_time\":\"" + dt.Rows[0]["mlsa_per_script_of_CDS_to_be_actively_open_at_a_time"].ToString() + "\"," + "\"mlsa_overall_in_CDS_to_be_actively_open_at_a_time\":\"" + dt.Rows[0]["mlsa_overall_in_CDS_to_be_actively_open_at_a_time"].ToString() + "\"," + "\"McxStartTrading\":\"" + dt.Rows[0]["McxStartTrading"].ToString() + "\"," + "\"McxEndTrading\":\"" + dt.Rows[0]["McxEndTrading"].ToString() + "\"," + "\"EQStartTrading\":\"" + dt.Rows[0]["EQStartTrading"].ToString() + "\"," + "\"EQEndTrading\":\"" + dt.Rows[0]["EQEndTrading"].ToString() + "\"," + "\"CdsStartTrading\":\"" + dt.Rows[0]["CdsStartTrading"].ToString() + "\"," + "\"CdsEndTrading\":\"" + dt.Rows[0]["CdsEndTrading"].ToString() + "\"" + "}";
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000079C4 File Offset: 0x00005BC4
		public string refreshdata_app(string UserId)
		{
			DateTime startdate = this.FirstDayOfWeek(DateTime.Now);
			DateTime enddate = this.LastDayOfWeek(DateTime.Now);
			string.Concat(new string[]
			{
				startdate.Year.ToString(),
				"-",
				startdate.Month.ToString(),
				"-",
				startdate.Day.ToString()
			});
			string.Concat(new string[]
			{
				enddate.Year.ToString(),
				"-",
				enddate.Month.ToString(),
				"-",
				enddate.Day.ToString()
			});
			string response = "false";
			DataTable dt = Universal.SelectWithDS(string.Concat(new string[]
			{
				"select * from t_trading_all_users_master INNER JOIN settings ON t_trading_all_users_master.Refid=settings.UserID where t_trading_all_users_master.Id='",
				UserId,
				"' And Type_='7' and ValidTill>='",
				Universal.GetDate,
				"'"
			}), "t_trading_client_master");
			if (dt.Rows.Count <= 0)
			{
				return response;
			}
			if (dt.Rows[0]["Account_status"].ToString() == "true")
			{
				return "{" + "\"ResponseCode\":\"200\"," + "\"UserId\":\"" + dt.Rows[0]["Id"].ToString() + "\"," + "\"FirstName\":\"" + dt.Rows[0]["FirstName"].ToString() + "\"," + "\"LastName\":\"" + dt.Rows[0]["LastName"].ToString() + "\"," + "\"UserName\":\"" + dt.Rows[0]["UserName"].ToString() + "\"," + "\"Password_\":\"" + dt.Rows[0]["Password_"].ToString() + "\"," + "\"AadharNo\":\"" + dt.Rows[0]["AadharNo"].ToString() + "\"," + "\"PanNo\":\"" + dt.Rows[0]["PanNo"].ToString() + "\"," + "\"Address\":\"" + dt.Rows[0]["Address"].ToString() + "\"," + "\"City\":\"" + dt.Rows[0]["City"].ToString() + "\"," + "\"MobileNo\":\"" + dt.Rows[0]["MobileNo"].ToString() + "\"," + "\"EmailId\":\"" + dt.Rows[0]["EmailId"].ToString() + "\"," + "\"LedgerBalance\":\"" + dt.Rows[0]["LedgerBalance"].ToString() + "\"," + "\"CreditLimit\":\"" + dt.Rows[0]["CreditLimit"].ToString() + "\"," + "\"profittradestoptime\":\"" + dt.Rows[0]["profittradestoptime"].ToString() + "\"," + "\"Type_\":\"" + dt.Rows[0]["Type_"].ToString() + "\"," + "\"RefId\":\"" + dt.Rows[0]["RefId"].ToString() + "\"," + "\"ValidTill\":\"" + dt.Rows[0]["ValidTill"].ToString().Replace("12:00:00 AM", "").Replace("00:00:00", "").Replace("\n", "").Replace("\r", "") + "\"," + "\"Account_status\":\"" + dt.Rows[0]["Account_status"].ToString() + "\"," + "\"GrossPL\":\"" + dt.Rows[0]["GrossPL"].ToString() + "\"," + "\"Brokerage\":\"" + dt.Rows[0]["Brokerage"].ToString() + "\"," + "\"NetPL\":\"" + dt.Rows[0]["NetPL"].ToString() + "\"," + "\"MaxUserAllowed\":\"" + dt.Rows[0]["MaxUserAllowed"].ToString() + "\"," + "\"Maximum_no_of_trade_allowed\":\"" + dt.Rows[0]["Maximum_no_of_trade_allowed"].ToString() + "\"," + "\"IsOnline\":\"" + dt.Rows[0]["IsOnline"].ToString() + "\"," + "\"AllowOrdersCurrentBid\":\"" + dt.Rows[0]["AllowOrdersCurrentBid"].ToString() + "\"," + "\"AllowFreshEntryHighAndBelow\":\"" + dt.Rows[0]["AllowFreshEntryHighAndBelow"].ToString() + "\"," + "\"DemoAccount\":\"" + dt.Rows[0]["DemoAccount"].ToString() + "\"," + "\"OneSideBrokerageIntraday\":\"" + dt.Rows[0]["OneSideBrokerageIntraday"].ToString() + "\"," + "\"AutoCloseTradesLossesLimit\":\"" + dt.Rows[0]["AutoCloseTradesLossesLimit"].ToString() + "\"," + "\"AutoCloseTradesLossesInsufficient\":\"" + dt.Rows[0]["AutoCloseTradesLossesInsufficient"].ToString() + "\"," + "\"AllowOrdersHighLow\":\"" + dt.Rows[0]["AllowOrdersHighLow"].ToString() + "\"," + "\"TriggersOrdersHighLow\":\"" + dt.Rows[0]["TriggersOrdersHighLow"].ToString() + "\"," + "\"TradeEquityUnits\":\"" + dt.Rows[0]["TradeEquityUnits"].ToString() + "\"," + "\"TradeMCXUnits\":\"" + dt.Rows[0]["TradeMCXUnits"].ToString() + "\"," + "\"TradeCDSUnits\":\"" + dt.Rows[0]["TradeCDSUnits"].ToString() + "\"," + "\"auto_close_all_active_trades_when_the_losses_reach\":\"" + dt.Rows[0]["auto_close_all_active_trades_when_the_losses_reach"].ToString() + "\"," + "\"Notify_client_when_the_losses_reach\":\"" + dt.Rows[0]["Notify_client_when_the_losses_reach"].ToString() + "\"," + "\"Profit_loss_Share_to_broker\":\"" + dt.Rows[0]["Profit_loss_Share_to_broker"].ToString() + "\"," + "\"IsMCXTrade\":\"" + dt.Rows[0]["IsMCXTrade"].ToString() + "\"," + "\"Mcx_Brokerage_Type\":\"" + dt.Rows[0]["Mcx_Brokerage_Type"].ToString() + "\"," + "\"MCX_brokerage_per_crore\":\"" + dt.Rows[0]["MCX_brokerage_per_crore"].ToString() + "\"," + "\"Intraday_Exposure_Margin_MCX\":\"" + dt.Rows[0]["Intraday_Exposure_Margin_MCX"].ToString() + "\"," + "\"Holding_Exposure_Margin_MCX\":\"" + dt.Rows[0]["Holding_Exposure_Margin_MCX"].ToString() + "\"," + "\"Minimum_lot_size_required_per_single_trade_of_MCX\":\"" + dt.Rows[0]["Minimum_lot_size_required_per_single_trade_of_MCX"].ToString() + "\"," + "\"Maximum_lot_size_allowed_per_single_trade_of_MCX\":\"" + dt.Rows[0]["Maximum_lot_size_allowed_per_single_trade_of_MCX"].ToString() + "\"," + "\"Maximum_lot_size_allowed_per_script_of_MCX_to_be\":\"" + dt.Rows[0]["Maximum_lot_size_allowed_per_script_of_MCX_to_be"].ToString() + "\"," + "\"Maximum_lot_size_allowed_overall_in_MCX_to_be\":\"" + dt.Rows[0]["Maximum_lot_size_allowed_overall_in_MCX_to_be"].ToString() + "\"," + "\"BULLDEX_brokerage\":\"" + dt.Rows[0]["BULLDEX_brokerage"].ToString() + "\"," + "\"GOLD_brokerage\":\"" + dt.Rows[0]["GOLD_brokerage"].ToString() + "\"," + "\"SILVER_brokerage\":\"" + dt.Rows[0]["SILVER_brokerage"].ToString() + "\"," + "\"CRUDEOIL_brokerage\":\"" + dt.Rows[0]["CRUDEOIL_brokerage"].ToString() + "\"," + "\"COPPER_brokerage\":\"" + dt.Rows[0]["COPPER_brokerage"].ToString() + "\"," + "\"NICKEL_brokerage\":\"" + dt.Rows[0]["NICKEL_brokerage"].ToString() + "\"," + "\"ZINC_brokerage\":\"" + dt.Rows[0]["ZINC_brokerage"].ToString() + "\"," + "\"LEAD_brokerage\":\"" + dt.Rows[0]["LEAD_brokerage"].ToString() + "\"," + "\"NATURALGAS_brokerage\":\"" + dt.Rows[0]["NATURALGAS_brokerage"].ToString() + "\"," + "\"ALUMINIUM_brokerage\":\"" + dt.Rows[0]["ALUMINIUM_brokerage"].ToString() + "\"," + "\"MENTHAOIL_brokerage\":\"" + dt.Rows[0]["MENTHAOIL_brokerage"].ToString() + "\"," + "\"COTTON_brokerage\":\"" + dt.Rows[0]["COTTON_brokerage"].ToString() + "\"," + "\"CPO_brokerage\":\"" + dt.Rows[0]["CPO_brokerage"].ToString() + "\"," + "\"GOLDM_brokerage\":\"" + dt.Rows[0]["GOLDM_brokerage"].ToString() + "\"," + "\"SILVERM_brokerage\":\"" + dt.Rows[0]["SILVERM_brokerage"].ToString() + "\"," + "\"SILVERMIC_brokerage\":\"" + dt.Rows[0]["SILVERMIC_brokerage"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_BULLDEX_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_BULLDEX_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_BULLDEX_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_BULLDEX_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_GOLD_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_GOLD_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_GOLD_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_GOLD_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_SILVER_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_SILVER_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_SILVER_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_SILVER_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_CRUDEOIL_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_CRUDEOIL_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_CRUDEOIL_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_CRUDEOIL_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_COPPER_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_COPPER_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_COPPER_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_COPPER_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_NICKEL_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_NICKEL_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_NICKEL_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_NICKEL_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_ZINC_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_ZINC_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_ZINC_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_ZINC_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_LEAD_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_LEAD_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_LEAD_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_LEAD_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_NATURALGAS_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_NATURALGAS_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_NATURALGAS_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_NATURALGAS_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_ALUMINIUM_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_ALUMINIUM_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_ALUMINIUM_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_ALUMINIUM_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_MENTHAOIL_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_MENTHAOIL_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_MENTHAOIL_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_MENTHAOIL_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_COTTON_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_COTTON_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_COTTON_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_COTTON_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_CPO_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_CPO_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_CPO_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_CPO_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_GOLDM_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_GOLDM_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_GOLDM_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_GOLDM_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_SILVERM_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_SILVERM_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_SILVERM_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_SILVERM_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_SILVERMIC_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_SILVERMIC_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_SILVERMIC_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_SILVERMIC_Holding"].ToString() + "\"," + "\"IsNSETrade\":\"" + dt.Rows[0]["IsNSETrade"].ToString() + "\"," + "\"Equity_brokerage_per_crore\":\"" + dt.Rows[0]["Equity_brokerage_per_crore"].ToString() + "\"," + "\"NSE_Exposure_Type\":\"" + dt.Rows[0]["NSE_Exposure_Type"].ToString() + "\"," + "\"Intraday_Exposure_Margin_Equity\":\"" + dt.Rows[0]["Intraday_Exposure_Margin_Equity"].ToString() + "\"," + "\"Orders_to_be_away_by_from_current_price_Equity\":\"" + dt.Rows[0]["Orders_to_be_away_by_from_current_price_Equity"].ToString() + "\"," + "\"Minimum_lot_size_required_per_single_trade_of_Equity_Futures\":\"" + dt.Rows[0]["Minimum_lot_size_required_per_single_trade_of_Equity_Futures"].ToString() + "\"," + "\"Maximum_lot_size_allowed_per_single_trade_of_Equity_Futures\":\"" + dt.Rows[0]["Maximum_lot_size_allowed_per_single_trade_of_Equity_Futures"].ToString() + "\"," + "\"Min_lot_size_reqd_per_single_trade_of_Equity_Futures_INDEX\":\"" + dt.Rows[0]["Min_lot_size_reqd_per_single_trade_of_Equity_Futures_INDEX"].ToString() + "\"," + "\"Max_lot_size_allw_per_single_trade_of_Equity_Futures_INDEX\":\"" + dt.Rows[0]["Max_lot_size_allw_per_single_trade_of_Equity_Futures_INDEX"].ToString() + "\"," + "\"Maxi_lt_sz_ald_per_scrt_of_Eqty_to_be_acty_open_a_time\":\"" + dt.Rows[0]["Maxi_lt_sz_ald_per_scrt_of_Eqty_to_be_acty_open_a_time"].ToString() + "\"," + "\"Maxi_lt_sz_ald_per_scpt_of_Eqty_INDEX_to_be_acty_opn_a_time\":\"" + dt.Rows[0]["Maxi_lt_sz_ald_per_scpt_of_Eqty_INDEX_to_be_acty_opn_a_time"].ToString() + "\"," + "\"Max_lot_size_alld_overall_in_Equity_to_be_actively\":\"" + dt.Rows[0]["Max_lot_size_alld_overall_in_Equity_to_be_actively"].ToString() + "\"," + "\"Max_lot_size_alld_ovl_in_Equity_INDEX_to_be_actively\":\"" + dt.Rows[0]["Max_lot_size_alld_ovl_in_Equity_INDEX_to_be_actively"].ToString() + "\"," + "\"IsCDSTrade\":\"" + dt.Rows[0]["IsCDSTrade"].ToString() + "\"," + "\"Intraday_Exposure_Margin_CDS\":\"" + dt.Rows[0]["Intraday_Exposure_Margin_CDS"].ToString() + "\"," + "\"Holding_Exposure_Margin_CDS\":\"" + dt.Rows[0]["Holding_Exposure_Margin_CDS"].ToString() + "\"," + "\"CDS_Brokerage_Type\":\"" + dt.Rows[0]["CDS_Brokerage_Type"].ToString() + "\"," + "\"CDS_brokerage_per_crore\":\"" + dt.Rows[0]["CDS_brokerage_per_crore"].ToString() + "\"," + "\"CDS_Exposure_Type\":\"" + dt.Rows[0]["CDS_Exposure_Type"].ToString() + "\"," + "\"Minimum_lot_size_required_per_single_of_CDS\":\"" + dt.Rows[0]["Minimum_lot_size_required_per_single_of_CDS"].ToString() + "\"," + "\"Max_lot_size_ald_per_single_CDS_Futures\":\"" + dt.Rows[0]["Max_lot_size_ald_per_single_CDS_Futures"].ToString() + "\"," + "\"mlsa_per_script_of_CDS_to_be_actively_open_at_a_time\":\"" + dt.Rows[0]["mlsa_per_script_of_CDS_to_be_actively_open_at_a_time"].ToString() + "\"," + "\"mlsa_overall_in_CDS_to_be_actively_open_at_a_time\":\"" + dt.Rows[0]["mlsa_overall_in_CDS_to_be_actively_open_at_a_time"].ToString() + "\"," + "\"McxStartTrading\":\"" + dt.Rows[0]["McxStartTrading"].ToString() + "\"," + "\"McxEndTrading\":\"" + dt.Rows[0]["McxEndTrading"].ToString() + "\"," + "\"EQStartTrading\":\"" + dt.Rows[0]["EQStartTrading"].ToString() + "\"," + "\"EQEndTrading\":\"" + dt.Rows[0]["EQEndTrading"].ToString() + "\"," + "\"CdsStartTrading\":\"" + dt.Rows[0]["CdsStartTrading"].ToString() + "\"," + "\"CdsEndTrading\":\"" + dt.Rows[0]["CdsEndTrading"].ToString() + "\"" + "}";
			}
			return "Bloked";
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00008FC5 File Offset: 0x000071C5
		public string GetIPAddress()
		{
			return new WebClient().DownloadString("https://api.ipify.org").Replace("\\r\\n", "").Replace("\\n", "").Trim();
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00008FF9 File Offset: 0x000071F9
		public DataTable checkisonline(string refid)
		{
			return Universal.SelectWithDS("WITH RECURSIVE RefidHierarchy AS (SELECT Id,Type_,IsOnlinePayment,RefId FROM t_trading_all_users_master WHERE id = '" + refid + "' UNION ALL SELECT t.Id, t.Type_, t.IsOnlinePayment, t.RefId FROM t_trading_all_users_master t INNER JOIN RefidHierarchy rh ON t.id = rh.RefId) SELECT* FROM RefidHierarchy;", "tempdata");
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00009018 File Offset: 0x00007218
		public string refreshdata(string userid, string devip)
		{
			if (devip == "localhost")
			{
				devip = "192.168.1.1";
			}
			Universal.devip = devip;
			DateTime startdate = this.FirstDayOfWeek(DateTime.Now);
			DateTime enddate = this.LastDayOfWeek(DateTime.Now);
			string sdate = string.Concat(new string[]
			{
				startdate.Year.ToString(),
				"-",
				startdate.Month.ToString(),
				"-",
				startdate.Day.ToString()
			});
			string edate = string.Concat(new string[]
			{
				enddate.Year.ToString(),
				"-",
				enddate.Month.ToString(),
				"-",
				enddate.Day.ToString()
			});
			string response = "false";
			DataTable dt = Universal.SelectWithDS(string.Concat(new string[]
			{
				"select *,(select DATE_FORMAT(ValidTill,'%Y-%m-%d') from t_trading_all_users_master as tmp1 where tmp1.Id=t_trading_all_users_master.RefId) as RefValidTill,(select Account_status from t_trading_all_users_master as tmp2 where tmp2.Id=t_trading_all_users_master.RefId) as RefAccount_status,(select IsRazorpayAllow from t_trading_all_users_master as tmp2 where tmp2.Id=t_trading_all_users_master.RefId) as IsRazorpayAllowornot,(select isonlinepayment from t_trading_all_users_master as tmp3 where tmp3.Id=t_trading_all_users_master.Refid limit 1) as isonlinepmt,(select checkprevtrade from t_trading_all_users_master as tm3 where tm3.Id=t_trading_all_users_master.Refid limit 1) as checkptrade,(select count(*) from t_user_order where OrderStatus='Active' and UserId=t_trading_all_users_master.Id and Orderdate between '",
				sdate,
				"' and '",
				edate,
				"') as TotalActive,(select count(*) from t_user_order where OrderStatus='Pending' and UserId=t_trading_all_users_master.Id and Orderdate = '",
				Universal.GetDate,
				"') as TotalPending,(select count(*) from t_user_order where OrderStatus='Closed' and UserId=t_trading_all_users_master.Id and Orderdate between '",
				sdate,
				"' and '",
				edate,
				"') as TotalClosed from t_trading_all_users_master where Id='",
				userid,
				"' And Type_='7'"
			}), "t_trading_client_master");
			if (dt.Rows.Count <= 0)
			{
				return response;
			}
			if (!(dt.Rows[0]["Account_status"].ToString() == "true"))
			{
				return "Bloked";
			}
			string RefValidTill = dt.Rows[0]["RefValidTill"].ToString();
			string a = dt.Rows[0]["RefAccount_status"].ToString();
			DateTime refvaldtill = DateTime.ParseExact(RefValidTill, "yyyy-MM-dd", CultureInfo.InvariantCulture);
			if (!(a == "true") || !(refvaldtill >= DateTime.Now))
			{
				return "false";
			}
			if (dt.Rows[0]["isonlinepmt"].ToString() == "false")
			{
				DataTable userlistforchk = this.checkisonline(dt.Rows[0]["Refid"].ToString());
				for (int rindx = 0; rindx < userlistforchk.Rows.Count; rindx++)
				{
					if (userlistforchk.Rows[rindx]["IsOnlinePayment"].ToString() == "true")
					{
						dt.Rows[0]["Refid"] = userlistforchk.Rows[rindx]["Id"].ToString();
						dt.Rows[0]["isonlinepmt"] = "true";
						break;
					}
				}
			}
			return "{" + "\"ClientName\":\"" + dt.Rows[0]["UserName"].ToString() + "\"," + "\"MobileNo\":\"" + dt.Rows[0]["MobileNo"].ToString() + "\"," + "\"EmailId\":\"" + dt.Rows[0]["EmailId"].ToString() + "\"," + "\"IsMCXTrade\":\"" + dt.Rows[0]["IsMCXTrade"].ToString() + "\"," + "\"IsNSETrade\":\"" + dt.Rows[0]["IsNSETrade"].ToString() + "\"," + "\"IsCDSTrade\":\"" + dt.Rows[0]["IsCDSTrade"].ToString() + "\"," + "\"CreditLimit\":\"" + dt.Rows[0]["CreditLimit"].ToString() + "\"," + "\"profittradestoptime\":\"" + dt.Rows[0]["profittradestoptime"].ToString() + "\"," + "\"Refid\":\"" + dt.Rows[0]["Refid"].ToString() + "\"," + "\"ValidTill\":\"" + dt.Rows[0]["ValidTill"].ToString() + "\"," + "\"Account_status\":\"" + dt.Rows[0]["Account_status"].ToString() + "\"," + "\"Password_\":\"" + dt.Rows[0]["Password_"].ToString() + "\"," + "\"LedgerBalance\":\"" + dt.Rows[0]["LedgerBalance"].ToString() + "\"," + "\"AllowOrdersCurrentBid\":\"" + dt.Rows[0]["AllowOrdersCurrentBid"].ToString() + "\"," + "\"AllowFreshEntryHighAndBelow\":\"" + dt.Rows[0]["AllowFreshEntryHighAndBelow"].ToString() + "\"," + "\"AllowOrdersHighLow\":\"" + dt.Rows[0]["AllowOrdersHighLow"].ToString() + "\"," + "\"AutoCloseTradesLossesLimit\":\"" + dt.Rows[0]["AutoCloseTradesLossesLimit"].ToString() + "\"," + "\"auto_close_all_active_trades_when_the_losses_reach\":\"" + dt.Rows[0]["auto_close_all_active_trades_when_the_losses_reach"].ToString() + "\"," + "\"Maximum_lot_size_allowed_per_single_trade_of_MCX\":\"" + dt.Rows[0]["Maximum_lot_size_allowed_per_single_trade_of_MCX"].ToString() + "\"," + "\"Minimum_lot_size_required_per_single_trade_of_MCX\":\"" + dt.Rows[0]["Minimum_lot_size_required_per_single_trade_of_MCX"].ToString() + "\"," + "\"Maximum_lot_size_allowed_per_script_of_MCX_to_be\":\"" + dt.Rows[0]["Maximum_lot_size_allowed_per_script_of_MCX_to_be"].ToString() + "\"," + "\"Maximum_lot_size_allowed_overall_in_MCX_to_be\":\"" + dt.Rows[0]["Maximum_lot_size_allowed_overall_in_MCX_to_be"].ToString() + "\"," + "\"Mcx_Brokerage_Type\":\"" + dt.Rows[0]["Mcx_Brokerage_Type"].ToString() + "\"," + "\"MCX_brokerage_per_crore\":\"" + dt.Rows[0]["MCX_brokerage_per_crore"].ToString() + "\"," + "\"Mcx_Exposure_Type\":\"" + dt.Rows[0]["Mcx_Exposure_Type"].ToString() + "\"," + "\"BULLDEX_brokerage\":\"" + dt.Rows[0]["BULLDEX_brokerage"].ToString() + "\"," + "\"GOLD_brokerage\":\"" + dt.Rows[0]["GOLD_brokerage"].ToString() + "\"," + "\"SILVER_brokerage\":\"" + dt.Rows[0]["SILVER_brokerage"].ToString() + "\"," + "\"CRUDEOIL_brokerage\":\"" + dt.Rows[0]["CRUDEOIL_brokerage"].ToString() + "\"," + "\"COPPER_brokerage\":\"" + dt.Rows[0]["COPPER_brokerage"].ToString() + "\"," + "\"NICKEL_brokerage\":\"" + dt.Rows[0]["NICKEL_brokerage"].ToString() + "\"," + "\"ZINC_brokerage\":\"" + dt.Rows[0]["ZINC_brokerage"].ToString() + "\"," + "\"LEAD_brokerage\":\"" + dt.Rows[0]["LEAD_brokerage"].ToString() + "\"," + "\"NATURALGAS_brokerage\":\"" + dt.Rows[0]["NATURALGAS_brokerage"].ToString() + "\"," + "\"ALUMINIUM_brokerage\":\"" + dt.Rows[0]["ALUMINIUM_brokerage"].ToString() + "\"," + "\"MENTHAOIL_brokerage\":\"" + dt.Rows[0]["MENTHAOIL_brokerage"].ToString() + "\"," + "\"COTTON_brokerage\":\"" + dt.Rows[0]["COTTON_brokerage"].ToString() + "\"," + "\"CPO_brokerage\":\"" + dt.Rows[0]["CPO_brokerage"].ToString() + "\"," + "\"GOLDM_brokerage\":\"" + dt.Rows[0]["GOLDM_brokerage"].ToString() + "\"," + "\"SILVERM_brokerage\":\"" + dt.Rows[0]["SILVERM_brokerage"].ToString() + "\"," + "\"SILVERMIC_brokerage\":\"" + dt.Rows[0]["SILVERMIC_brokerage"].ToString() + "\"," + "\"ALUMINI_brokerage\":\"" + dt.Rows[0]["ALUMINI_brokerage"].ToString() + "\"," + "\"CRUDEOILM_brokerage\":\"" + dt.Rows[0]["CRUDEOILM_brokerage"].ToString() + "\"," + "\"LEADMINI_brokerage\":\"" + dt.Rows[0]["LEADMINI_brokerage"].ToString() + "\"," + "\"NATGASMINI_brokerage\":\"" + dt.Rows[0]["NATGASMINI_brokerage"].ToString() + "\"," + "\"ZINCMINI_brokerage\":\"" + dt.Rows[0]["ZINCMINI_brokerage"].ToString() + "\"," + "\"Intraday_Exposure_Margin_MCX\":\"" + dt.Rows[0]["Intraday_Exposure_Margin_MCX"].ToString() + "\"," + "\"Holding_Exposure_Margin_MCX\":\"" + dt.Rows[0]["Holding_Exposure_Margin_MCX"].ToString() + "\"," + "\"TradeEquityUnits\":\"" + dt.Rows[0]["TradeEquityUnits"].ToString() + "\"," + "\"TradeMCXUnits\":\"" + dt.Rows[0]["TradeMCXUnits"].ToString() + "\"," + "\"TradeCDSUnits\":\"" + dt.Rows[0]["TradeCDSUnits"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_BULLDEX_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_BULLDEX_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_BULLDEX_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_BULLDEX_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_GOLD_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_GOLD_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_GOLD_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_GOLD_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_SILVER_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_SILVER_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_SILVER_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_SILVER_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_CRUDEOIL_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_CRUDEOIL_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_CRUDEOIL_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_CRUDEOIL_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_COPPER_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_COPPER_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_COPPER_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_COPPER_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_NICKEL_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_NICKEL_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_NICKEL_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_NICKEL_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_ZINC_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_ZINC_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_ZINC_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_ZINC_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_LEAD_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_LEAD_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_LEAD_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_LEAD_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_NATURALGAS_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_NATURALGAS_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_NATURALGAS_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_NATURALGAS_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_ALUMINIUM_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_ALUMINIUM_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_ALUMINIUM_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_ALUMINIUM_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_MENTHAOIL_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_MENTHAOIL_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_MENTHAOIL_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_MENTHAOIL_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_COTTON_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_COTTON_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_CPO_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_CPO_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_ALUMINI_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_ALUMINI_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_CRUDEOILM_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_CRUDEOILM_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_LEADMINI_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_LEADMINI_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_NATGASMINI_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_NATGASMINI_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_ZINCMINI_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_ZINCMINI_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_COTTON_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_COTTON_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_CPO_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_CPO_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_GOLDM_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_GOLDM_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_GOLDM_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_GOLDM_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_SILVERM_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_SILVERM_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_SILVERM_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_SILVERM_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_SILVERMIC_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_SILVERMIC_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_SILVERMIC_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_SILVERMIC_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_ALUMINI_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_ALUMINI_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_CRUDEOILM_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_CRUDEOILM_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_LEADMINI_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_LEADMINI_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_NATGASMINI_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_NATGASMINI_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_ZINCMINI_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_ZINCMINI_Holding"].ToString() + "\"," + "\"NSE_Brokerage_Type\":\"" + dt.Rows[0]["NSE_Brokerage_Type"].ToString() + "\"," + "\"Equity_brokerage_per_crore\":\"" + dt.Rows[0]["Equity_brokerage_per_crore"].ToString() + "\"," + "\"NSE_Exposure_Type\":\"" + dt.Rows[0]["NSE_Exposure_Type"].ToString() + "\"," + "\"Intraday_Exposure_Margin_EQUITY\":\"" + dt.Rows[0]["Intraday_Exposure_Margin_EQUITY"].ToString() + "\"," + "\"Holding_Exposure_Margin_EQUITY\":\"" + dt.Rows[0]["Holding_Exposure_Margin_EQUITY"].ToString() + "\"," + "\"CDS_Brokerage_Type\":\"" + dt.Rows[0]["CDS_Brokerage_Type"].ToString() + "\"," + "\"CDS_brokerage_per_crore\":\"" + dt.Rows[0]["CDS_brokerage_per_crore"].ToString() + "\"," + "\"CDS_Exposure_Type\":\"" + dt.Rows[0]["CDS_Exposure_Type"].ToString() + "\"," + "\"Intraday_Exposure_Margin_CDS\":\"" + dt.Rows[0]["Intraday_Exposure_Margin_CDS"].ToString() + "\"," + "\"Holding_Exposure_Margin_CDS\":\"" + dt.Rows[0]["Holding_Exposure_Margin_CDS"].ToString() + "\"," + "\"TotalActive\":\"" + dt.Rows[0]["TotalActive"].ToString() + "\"," + "\"TotalPending\":\"" + dt.Rows[0]["TotalPending"].ToString() + "\"," + "\"TotalClosed\":\"" + dt.Rows[0]["TotalClosed"].ToString() + "\"," + "\"IsStopTrading\":\"" + dt.Rows[0]["IsStopTrading"].ToString() + "\"," + "\"IsSellAllowed\":\"" + dt.Rows[0]["IsSellAllowed"].ToString() + "\"," + "\"IsOnlinePayment\":\"" + dt.Rows[0]["isonlinepmt"].ToString() + "\"," + "\"checkprevtrade\":\"" + dt.Rows[0]["checkptrade"].ToString() + "\"," + "\"BankName\":\"" + dt.Rows[0]["BankName"].ToString() + "\"," + "\"AccountNo\":\"" + dt.Rows[0]["AccountNo"].ToString() + "\"," + "\"IFSCCode\":\"" + dt.Rows[0]["IFSCCode"].ToString() + "\"," + "\"AccountHolderName\":\"" + dt.Rows[0]["AccountHolderName"].ToString() + "\"," + "\"IsRazorpayAllowornot\":\"" + dt.Rows[0]["IsRazorpayAllowornot"].ToString() + "\"," + "\"UserId\":\"" + dt.Rows[0]["Id"].ToString() + "\"" + "}";
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000A6AC File Offset: 0x000088AC
		[EnableCors("*", "*", "*")]
		public string checklogin(string username, string password, string deviceid, string refidbyuser, string refidformatch)
		{
			DateTime startdate = this.FirstDayOfWeek(DateTime.Now);
			DateTime enddate = this.LastDayOfWeek(DateTime.Now);
			string sdate = string.Concat(new string[]
			{
				startdate.Year.ToString(),
				"-",
				startdate.Month.ToString(),
				"-",
				startdate.Day.ToString()
			});
			string edate = string.Concat(new string[]
			{
				enddate.Year.ToString(),
				"-",
				enddate.Month.ToString(),
				"-",
				enddate.Day.ToString()
			});
			string response = "false";
			if (!true)
			{
				return "false";
			}
			DataTable dt = Universal.SelectWithDS(string.Concat(new string[]
			{
				"select *,(select DATE_FORMAT(ValidTill,'%Y-%m-%d') from t_trading_all_users_master as tmp1 where tmp1.Id=t_trading_all_users_master.RefId) as RefValidTill,(select Account_status from t_trading_all_users_master as tmp2 where tmp2.Id=t_trading_all_users_master.RefId) as RefAccount_status,(select count(*) from t_user_order where OrderStatus='Active' and UserId=t_trading_all_users_master.Id and Orderdate between '",
				sdate,
				"' and '",
				edate,
				"') as TotalActive,(select count(*) from t_user_order where OrderStatus='Pending' and UserId=t_trading_all_users_master.Id and Orderdate = '",
				Universal.GetDate,
				"') as TotalPending,(select count(*) from t_user_order where OrderStatus='Closed' and UserId=t_trading_all_users_master.Id and Orderdate between '",
				sdate,
				"' and '",
				edate,
				"') as TotalClosed from t_trading_all_users_master where BINARY UserName='",
				username,
				"' and BINARY password_='",
				password,
				"' And Type_='7'"
			}), "t_trading_client_master");
			if (dt.Rows.Count <= 0)
			{
				return response;
			}
			if (!(dt.Rows[0]["Account_status"].ToString() == "true"))
			{
				return "Bloked";
			}
			dt.Rows[0]["FirstTimeLogin"].ToString() == "true";
			Universal.ExecuteNonQuery(string.Concat(new string[]
			{
				"update t_trading_all_users_master set LoggedInDeviceId='",
				deviceid,
				"' where Id='",
				dt.Rows[0]["Id"].ToString(),
				"'"
			}));
			string RefValidTill = dt.Rows[0]["RefValidTill"].ToString();
			string a = dt.Rows[0]["RefAccount_status"].ToString();
			DateTime refvaldtill = DateTime.ParseExact(RefValidTill, "yyyy-MM-dd", CultureInfo.InvariantCulture);
			if (!(a == "true") || !(refvaldtill >= DateTime.Now))
			{
				return "Bloked";
			}
			if (dt.Rows[0]["isonlinepayment"].ToString() == "false")
			{
				DataTable userlistforchk = this.checkisonline(dt.Rows[0]["Refid"].ToString());
				for (int rindx = 0; rindx < userlistforchk.Rows.Count; rindx++)
				{
					if (userlistforchk.Rows[rindx]["IsOnlinePayment"].ToString() == "true")
					{
						dt.Rows[0]["Refid"] = userlistforchk.Rows[rindx]["Id"].ToString();
						dt.Rows[0]["isonlinepayment"] = "true";
						break;
					}
				}
			}
			return "{" + "\"UserId\":\"" + dt.Rows[0]["Id"].ToString() + "\"," + "\"ClientName\":\"" + dt.Rows[0]["UserName"].ToString() + "\"," + "\"oldpassword\":\"" + dt.Rows[0]["Password_"].ToString() + "\"," + "\"Refid\":\"" + dt.Rows[0]["Refid"].ToString() + "\"," + "\"isonlinepayment\":\"" + dt.Rows[0]["isonlinepayment"].ToString() + "\"," + "\"MobileNo\":\"" + dt.Rows[0]["MobileNo"].ToString() + "\"," + "\"EmailId\":\"" + dt.Rows[0]["EmailId"].ToString() + "\"," + "\"IsMCXTrade\":\"" + dt.Rows[0]["IsMCXTrade"].ToString() + "\"," + "\"IsNSETrade\":\"" + dt.Rows[0]["IsNSETrade"].ToString() + "\"," + "\"IsCDSTrade\":\"" + dt.Rows[0]["IsCDSTrade"].ToString() + "\"," + "\"TradeEquityUnits\":\"" + dt.Rows[0]["TradeEquityUnits"].ToString() + "\"," + "\"TradeMCXUnits\":\"" + dt.Rows[0]["TradeMCXUnits"].ToString() + "\"," + "\"TradeCDSUnits\":\"" + dt.Rows[0]["TradeCDSUnits"].ToString() + "\"," + "\"profittradestoptime\":\"" + dt.Rows[0]["profittradestoptime"].ToString() + "\"," + "\"FirstTimeLogin\":\"" + dt.Rows[0]["FirstTimeLogin"].ToString() + "\"," + "\"ValidTill \":\"" + dt.Rows[0]["ValidTill"].ToString() + "\"," + "\"CreditLimit\":\"" + dt.Rows[0]["CreditLimit"].ToString() + "\"," + "\"LedgerBalance\":\"" + dt.Rows[0]["LedgerBalance"].ToString() + "\"," + "\"AllowOrdersCurrentBid\":\"" + dt.Rows[0]["AllowOrdersCurrentBid"].ToString() + "\"," + "\"AllowFreshEntryHighAndBelow\":\"" + dt.Rows[0]["AllowFreshEntryHighAndBelow"].ToString() + "\"," + "\"AllowOrdersHighLow\":\"" + dt.Rows[0]["AllowOrdersHighLow"].ToString() + "\"," + "\"AutoCloseTradesLossesLimit\":\"" + dt.Rows[0]["AutoCloseTradesLossesLimit"].ToString() + "\"," + "\"auto_close_all_active_trades_when_the_losses_reach\":\"" + dt.Rows[0]["auto_close_all_active_trades_when_the_losses_reach"].ToString() + "\"," + "\"Maximum_lot_size_allowed_per_single_trade_of_MCX\":\"" + dt.Rows[0]["Maximum_lot_size_allowed_per_single_trade_of_MCX"].ToString() + "\"," + "\"Minimum_lot_size_required_per_single_trade_of_MCX\":\"" + dt.Rows[0]["Minimum_lot_size_required_per_single_trade_of_MCX"].ToString() + "\"," + "\"Maximum_lot_size_allowed_per_script_of_MCX_to_be\":\"" + dt.Rows[0]["Maximum_lot_size_allowed_per_script_of_MCX_to_be"].ToString() + "\"," + "\"Maximum_lot_size_allowed_overall_in_MCX_to_be\":\"" + dt.Rows[0]["Maximum_lot_size_allowed_overall_in_MCX_to_be"].ToString() + "\"," + "\"Mcx_Brokerage_Type\":\"" + dt.Rows[0]["Mcx_Brokerage_Type"].ToString() + "\"," + "\"MCX_brokerage_per_crore\":\"" + dt.Rows[0]["MCX_brokerage_per_crore"].ToString() + "\"," + "\"Mcx_Exposure_Type\":\"" + dt.Rows[0]["Mcx_Exposure_Type"].ToString() + "\"," + "\"BULLDEX_brokerage\":\"" + dt.Rows[0]["BULLDEX_brokerage"].ToString() + "\"," + "\"GOLD_brokerage\":\"" + dt.Rows[0]["GOLD_brokerage"].ToString() + "\"," + "\"SILVER_brokerage\":\"" + dt.Rows[0]["SILVER_brokerage"].ToString() + "\"," + "\"CRUDEOIL_brokerage\":\"" + dt.Rows[0]["CRUDEOIL_brokerage"].ToString() + "\"," + "\"COPPER_brokerage\":\"" + dt.Rows[0]["COPPER_brokerage"].ToString() + "\"," + "\"NICKEL_brokerage\":\"" + dt.Rows[0]["NICKEL_brokerage"].ToString() + "\"," + "\"ZINC_brokerage\":\"" + dt.Rows[0]["ZINC_brokerage"].ToString() + "\"," + "\"LEAD_brokerage\":\"" + dt.Rows[0]["LEAD_brokerage"].ToString() + "\"," + "\"NATURALGAS_brokerage\":\"" + dt.Rows[0]["NATURALGAS_brokerage"].ToString() + "\"," + "\"ALUMINIUM_brokerage\":\"" + dt.Rows[0]["ALUMINIUM_brokerage"].ToString() + "\"," + "\"MENTHAOIL_brokerage\":\"" + dt.Rows[0]["MENTHAOIL_brokerage"].ToString() + "\"," + "\"COTTON_brokerage\":\"" + dt.Rows[0]["COTTON_brokerage"].ToString() + "\"," + "\"CPO_brokerage\":\"" + dt.Rows[0]["CPO_brokerage"].ToString() + "\"," + "\"GOLDM_brokerage\":\"" + dt.Rows[0]["GOLDM_brokerage"].ToString() + "\"," + "\"SILVERM_brokerage\":\"" + dt.Rows[0]["SILVERM_brokerage"].ToString() + "\"," + "\"SILVERMIC_brokerage\":\"" + dt.Rows[0]["SILVERMIC_brokerage"].ToString() + "\"," + "\"ALUMINI_brokerage\":\"" + dt.Rows[0]["ALUMINI_brokerage"].ToString() + "\"," + "\"CRUDEOILM_brokerage\":\"" + dt.Rows[0]["CRUDEOILM_brokerage"].ToString() + "\"," + "\"LEADMINI_brokerage\":\"" + dt.Rows[0]["LEADMINI_brokerage"].ToString() + "\"," + "\"NATGASMINI_brokerage\":\"" + dt.Rows[0]["NATGASMINI_brokerage"].ToString() + "\"," + "\"ZINCMINI_brokerage\":\"" + dt.Rows[0]["ZINCMINI_brokerage"].ToString() + "\"," + "\"Intraday_Exposure_Margin_MCX\":\"" + dt.Rows[0]["Intraday_Exposure_Margin_MCX"].ToString() + "\"," + "\"Holding_Exposure_Margin_MCX\":\"" + dt.Rows[0]["Holding_Exposure_Margin_MCX"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_BULLDEX_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_BULLDEX_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_BULLDEX_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_BULLDEX_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_GOLD_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_GOLD_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_GOLD_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_GOLD_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_SILVER_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_SILVER_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_SILVER_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_SILVER_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_CRUDEOIL_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_CRUDEOIL_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_CRUDEOIL_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_CRUDEOIL_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_ALUMINI_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_ALUMINI_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_CRUDEOILM_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_CRUDEOILM_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_LEADMINI_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_LEADMINI_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_NATGASMINI_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_NATGASMINI_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_ZINCMINI_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_ZINCMINI_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_COPPER_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_COPPER_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_ALUMINI_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_ALUMINI_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_CRUDEOILM_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_CRUDEOILM_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_LEADMINI_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_LEADMINI_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_NATGASMINI_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_NATGASMINI_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_ZINCMINI_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_ZINCMINI_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_COPPER_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_COPPER_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_NICKEL_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_NICKEL_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_NICKEL_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_NICKEL_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_ZINC_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_ZINC_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_ZINC_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_ZINC_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_LEAD_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_LEAD_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_LEAD_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_LEAD_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_NATURALGAS_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_NATURALGAS_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_NATURALGAS_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_NATURALGAS_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_ALUMINIUM_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_ALUMINIUM_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_ALUMINIUM_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_ALUMINIUM_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_MENTHAOIL_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_MENTHAOIL_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_MENTHAOIL_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_MENTHAOIL_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_COTTON_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_COTTON_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_COTTON_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_COTTON_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_CPO_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_CPO_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_CPO_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_CPO_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_GOLDM_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_GOLDM_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_GOLDM_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_GOLDM_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_SILVERM_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_SILVERM_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_SILVERM_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_SILVERM_Holding"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_SILVERMIC_Intraday\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_SILVERMIC_Intraday"].ToString() + "\"," + "\"MCX_Exposure_Lot_wise_SILVERMIC_Holding\":\"" + dt.Rows[0]["MCX_Exposure_Lot_wise_SILVERMIC_Holding"].ToString() + "\"," + "\"NSE_Brokerage_Type\":\"" + dt.Rows[0]["NSE_Brokerage_Type"].ToString() + "\"," + "\"Equity_brokerage_per_crore\":\"" + dt.Rows[0]["Equity_brokerage_per_crore"].ToString() + "\"," + "\"NSE_Exposure_Type\":\"" + dt.Rows[0]["NSE_Exposure_Type"].ToString() + "\"," + "\"Intraday_Exposure_Margin_EQUITY\":\"" + dt.Rows[0]["Intraday_Exposure_Margin_EQUITY"].ToString() + "\"," + "\"Holding_Exposure_Margin_EQUITY\":\"" + dt.Rows[0]["Holding_Exposure_Margin_EQUITY"].ToString() + "\"," + "\"CDS_Brokerage_Type\":\"" + dt.Rows[0]["CDS_Brokerage_Type"].ToString() + "\"," + "\"CDS_brokerage_per_crore\":\"" + dt.Rows[0]["CDS_brokerage_per_crore"].ToString() + "\"," + "\"CDS_Exposure_Type\":\"" + dt.Rows[0]["CDS_Exposure_Type"].ToString() + "\"," + "\"Intraday_Exposure_Margin_CDS\":\"" + dt.Rows[0]["Intraday_Exposure_Margin_CDS"].ToString() + "\"," + "\"Holding_Exposure_Margin_CDS\":\"" + dt.Rows[0]["Holding_Exposure_Margin_CDS"].ToString() + "\"," + "\"TotalActive\":\"" + dt.Rows[0]["TotalActive"].ToString() + "\"," + "\"TotalPending\":\"" + dt.Rows[0]["TotalPending"].ToString() + "\"," + "\"TotalClosed\":\"" + dt.Rows[0]["TotalClosed"].ToString() + "\"" + "}";
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000BC5C File Offset: 0x00009E5C
		[HttpPost]
		public string savetransaction(string userid, string txttransid, string txttransamount, string txttransremark, string refid)
		{
			if (Universal.ExecuteNonQuery(string.Concat(new string[]
			{
				"insert into tbl_payment_transaction_master (UserId,TransactionId,Amount,Remarks,Status_,CreatedBy) values('",
				userid,
				"','",
				txttransid,
				"','",
				txttransamount,
				"','",
				txttransremark,
				"','Pending','",
				refid,
				"')"
			})) == 1)
			{
				if (txttransremark.Contains("RazorpayId"))
				{
					DataTable usergetlb = Universal.SelectWithDS("select LedgerBalance from t_trading_all_users_master where Id='" + userid + "'", "t_trading_all_users_master");
					if (usergetlb.Rows.Count > 0)
					{
						Universal.ExecuteNonQuery(string.Concat(new string[]
						{
							"update t_trading_all_users_master set LedgerBalance='",
							(decimal.Parse(usergetlb.Rows[0]["LedgerBalance"].ToString()) + decimal.Parse(txttransamount)).ToString(),
							"' where Id='",
							userid,
							"'"
						}));
					}
				}
				return "true";
			}
			return "false";
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000BD70 File Offset: 0x00009F70
		public DataTable getrecursivedata(string refid)
		{
			return Universal.SelectWithDS("WITH RECURSIVE RefidHierarchy AS (SELECT * FROM t_trading_all_users_master WHERE id = '" + refid + "' UNION ALL SELECT t.* FROM t_trading_all_users_master t INNER JOIN RefidHierarchy rh ON t.id = rh.RefId) SELECT * FROM RefidHierarchy;", "temp");
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000BD8C File Offset: 0x00009F8C
		public string savetoken(string symbolname, string token, string userid, string exchangetype, string lotsize)
		{
			Universal.ExecuteNonQuery(string.Concat(new string[]
			{
				"delete from t_selected_symbols_by_user where UserId='",
				userid,
				"' and SymbolToken='",
				token,
				"'"
			}));
			if (Universal.ExecuteNonQuery(string.Concat(new string[]
			{
				"insert into t_selected_symbols_by_user (ExchangeType,SymbolName,SymbolToken,UserId,lotsize) values('",
				exchangetype,
				"','",
				symbolname,
				"','",
				token,
				"','",
				userid,
				"','",
				lotsize,
				"')"
			})) == 1)
			{
				return "true";
			}
			return "false";
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000BE34 File Offset: 0x0000A034
		[HttpPost]
		public string savetokenbyuser(string symbolname, string token, string UserId, string exchangetype, string lotsize)
		{
			if (Universal.ExecuteNonQuery(string.Concat(new string[]
			{
				"insert into t_selected_symbols_by_user (ExchangeType,SymbolName,SymbolToken,UserId,lotsize) values('",
				exchangetype,
				"','",
				symbolname,
				"','",
				token,
				"','",
				UserId,
				"','",
				lotsize,
				"')"
			})) == 1)
			{
				return "{" + "\"ResponseCode\":\"200\"," + "\"ResponseMessage\":\"Symbol Added Successfully\"" + "}";
			}
			return "{" + "\"ResponseCode\":\"201\"," + "\"ResponseMessage\":\"Error in adding symbol\"" + "}";
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000BEE4 File Offset: 0x0000A0E4
		public string deletetoken(string token, string userid)
		{
			if (Universal.ExecuteNonQuery(string.Concat(new string[]
			{
				"delete from t_selected_symbols_by_user where SymbolToken='",
				token,
				"' and UserId='",
				userid,
				"'"
			})) == 1)
			{
				return "true";
			}
			return "false";
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000BF24 File Offset: 0x0000A124
		[HttpPost]
		public string removetoken(string token, string UserId)
		{
			if (Universal.ExecuteNonQuery(string.Concat(new string[]
			{
				"delete from t_selected_symbols_by_user where SymbolToken='",
				token,
				"' and UserId='",
				UserId,
				"'"
			})) == 1)
			{
				return "{" + "\"ResponseCode\":\"200\"," + "\"ResponseMessage\":\"Symbol Removed Successfully\"" + "}";
			}
			return "{" + "\"ResponseCode\":\"201\"," + "\"ResponseMessage\":\"Error in Removing Symbol\"" + "}";
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000BFAC File Offset: 0x0000A1AC
		[HttpPost]
		public string getselectedtokenbyuser(string UserId, string exch)
		{
			List<getselectedtoken> obj = new List<getselectedtoken>();
			DataTable dt = Universal.SelectWithDS(string.Concat(new string[]
			{
				"select t_selected_symbols_by_user.*,t_universal_tradeble_tokens.sell,t_universal_tradeble_tokens.buy from t_selected_symbols_by_user INNER JOIN t_universal_tradeble_tokens ON t_selected_symbols_by_user.SymbolToken=t_universal_tradeble_tokens.instrument_token where UserId='",
				UserId,
				"' and ExchangeType='",
				exch,
				"' order by SymbolName"
			}), "t_selected_symbols_by_user");
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				obj.Add(new getselectedtoken
				{
					Id = dt.Rows[i]["Id"].ToString(),
					ExchangeType = dt.Rows[i]["ExchangeType"].ToString(),
					SymbolName = dt.Rows[i]["SymbolName"].ToString(),
					Lotsize = dt.Rows[i]["Lotsize"].ToString(),
					SymbolToken = dt.Rows[i]["SymbolToken"].ToString()
				});
			}
			return new JavaScriptSerializer().Serialize(obj);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000C0D0 File Offset: 0x0000A2D0
		public string getselectedtoken(string cid, string exch)
		{
			if (exch == "CDS" || exch == "cds")
			{
				exch = "OPT";
			}
			List<getselectedtoken> obj = new List<getselectedtoken>();
			DataTable dt = Universal.SelectWithDS(string.Concat(new string[]
			{
				"select t_selected_symbols_by_user.*,t_universal_tradeble_tokens.sell,t_universal_tradeble_tokens.buy,last_price,`change`,volume,open_,high_,low_,close_,oi from t_selected_symbols_by_user INNER JOIN t_universal_tradeble_tokens ON t_selected_symbols_by_user.SymbolToken=t_universal_tradeble_tokens.instrument_token where UserId='",
				cid,
				"' and ExchangeType='",
				exch,
				"' order by SymbolName"
			}), "t_selected_symbols_by_user");
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				obj.Add(new getselectedtoken
				{
					Id = dt.Rows[i]["Id"].ToString(),
					ExchangeType = dt.Rows[i]["ExchangeType"].ToString(),
					SymbolName = dt.Rows[i]["SymbolName"].ToString(),
					Lotsize = dt.Rows[i]["Lotsize"].ToString(),
					SymbolToken = dt.Rows[i]["SymbolToken"].ToString(),
					buy = dt.Rows[i]["buy"].ToString() + "&nbsp;",
					sell = dt.Rows[i]["sell"].ToString() + "&nbsp;",
					ltp = dt.Rows[i]["last_price"].ToString(),
					chg = dt.Rows[i]["change"].ToString(),
					high = dt.Rows[i]["high_"].ToString(),
					low = dt.Rows[i]["low_"].ToString(),
					opn = dt.Rows[i]["open_"].ToString(),
					cls = dt.Rows[i]["close_"].ToString(),
					ol = dt.Rows[i]["oi"].ToString(),
					vol = dt.Rows[i]["volume"].ToString()
				});
			}
			return new JavaScriptSerializer().Serialize(obj);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000C374 File Offset: 0x0000A574
		public string gettradeingsymbols(string extype, string searchkey, string refid)
		{
			if (extype == "CDS")
			{
				extype = "OPT";
			}
			if (searchkey == null || searchkey == "null")
			{
				return JsonConvert.SerializeObject(Universal.SelectWithDS(string.Concat(new string[]
				{
					"select symbolname as tradingsymbol,Exchange as exchange,instrument_token as exchange_token,DATE_FORMAT(expirydate,'%d %M %Y') as expiry,instrument_token,SymbolType as instrument_type,last_price,lotsize as lot_size from t_universal_tradeble_tokens inner join t_symbol_allocation ON t_symbol_allocation.symbolid = t_universal_tradeble_tokens.Id where userid = '",
					refid,
					"' and Exchange='",
					extype,
					"' order by symbolname"
				}), "tbl_states"));
			}
			return JsonConvert.SerializeObject(Universal.SelectWithDS(string.Concat(new string[]
			{
				"select symbolname as tradingsymbol,Exchange as exchange,instrument_token as exchange_token,DATE_FORMAT(expirydate,'%d %M %Y') as expiry,instrument_token,SymbolType as instrument_type,last_price,lotsize as lot_size from t_universal_tradeble_tokens inner join t_symbol_allocation ON t_symbol_allocation.symbolid = t_universal_tradeble_tokens.Id where userid = '",
				refid,
				"' and Exchange='",
				extype,
				"' and symbolname like '%",
				searchkey,
				"%'"
			}), "tbl_states"));
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000C428 File Offset: 0x0000A628
		public string getMCXsymbols(string extype, string searchkey, string refid)
		{
			if (extype == "CDS")
			{
				extype = "OPT";
			}
			List<t_mcx_data> mcxdata = new List<t_mcx_data>();
			if (searchkey == null || searchkey == "null")
			{
				DataTable dt = Universal.SelectWithDS(string.Concat(new string[]
				{
					"select symbolname,Exchange,instrument_token,DATE_FORMAT(expirydate,'%d %M %Y') as expiry,instrument_token,SymbolType,last_price,lotsize from t_universal_tradeble_tokens inner join t_symbol_allocation ON t_symbol_allocation.symbolid = t_universal_tradeble_tokens.Id where userid = '",
					refid,
					"' and Exchange='",
					extype,
					"' order by symbolname"
				}), "tbl_states");
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					mcxdata.Add(new t_mcx_data
					{
						name = dt.Rows[i]["symbolname"].ToString(),
						exchange = dt.Rows[i]["Exchange"].ToString(),
						tradingsymbol = dt.Rows[i]["symbolname"].ToString(),
						exchange_token = dt.Rows[i]["instrument_token"].ToString(),
						expiry = dt.Rows[i]["expiry"].ToString(),
						instrument_token = dt.Rows[i]["instrument_token"].ToString(),
						instrument_type = dt.Rows[i]["SymbolType"].ToString(),
						last_price = dt.Rows[i]["last_price"].ToString(),
						lot_size = dt.Rows[i]["lotsize"].ToString(),
						tick_size = ""
					});
				}
			}
			else
			{
				DataTable dt2 = Universal.SelectWithDS(string.Concat(new string[]
				{
					"select symbolname,Exchange,instrument_token,DATE_FORMAT(expirydate,'%d %M %Y') as expiry,instrument_token,SymbolType,last_price,lotsize from t_universal_tradeble_tokens inner join t_symbol_allocation ON t_symbol_allocation.symbolid = t_universal_tradeble_tokens.Id where userid = '",
					refid,
					"' and Exchange='",
					extype,
					"' and symbolname like '%",
					searchkey,
					"%'"
				}), "tbl_states");
				for (int j = 0; j < dt2.Rows.Count; j++)
				{
					mcxdata.Add(new t_mcx_data
					{
						name = dt2.Rows[j]["symbolname"].ToString(),
						exchange = dt2.Rows[j]["Exchange"].ToString(),
						tradingsymbol = dt2.Rows[j]["symbolname"].ToString(),
						exchange_token = dt2.Rows[j]["instrument_token"].ToString(),
						expiry = dt2.Rows[j]["expiry"].ToString(),
						instrument_token = dt2.Rows[j]["instrument_token"].ToString(),
						instrument_type = dt2.Rows[j]["SymbolType"].ToString(),
						last_price = dt2.Rows[j]["last_price"].ToString(),
						lot_size = dt2.Rows[j]["lotsize"].ToString(),
						tick_size = ""
					});
				}
			}
			return new JavaScriptSerializer().Serialize(mcxdata);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000C7C8 File Offset: 0x0000A9C8
		public string getCDSsymbols(string extype, string searchkey)
		{
			List<t_mcx_data> mcxdata = new List<t_mcx_data>();
			if (searchkey == "null")
			{
				DataTable dt = Universal.SelectWithDS("select name,exchange,exchange_token,DATE_FORMAT(expiry,'%d %M %Y') as expiry,instrument_token,tradingsymbol,instrument_type,last_price,lot_size,tick_size from t_cds_symbols where instrument_type='" + extype + "' order by symbolname", "tbl_states");
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					mcxdata.Add(new t_mcx_data
					{
						name = dt.Rows[i]["name"].ToString(),
						exchange = dt.Rows[i]["exchange"].ToString(),
						tradingsymbol = dt.Rows[i]["tradingsymbol"].ToString(),
						exchange_token = dt.Rows[i]["exchange_token"].ToString(),
						expiry = dt.Rows[i]["expiry"].ToString(),
						instrument_token = dt.Rows[i]["instrument_token"].ToString(),
						instrument_type = dt.Rows[i]["instrument_type"].ToString(),
						last_price = dt.Rows[i]["last_price"].ToString(),
						lot_size = dt.Rows[i]["lot_size"].ToString(),
						tick_size = dt.Rows[i]["tick_size"].ToString()
					});
				}
			}
			else
			{
				DataTable dt2 = Universal.SelectWithDS(string.Concat(new string[]
				{
					"select name,exchange,exchange_token,DATE_FORMAT(expiry,'%d %M %Y') as expiry,instrument_token,tradingsymbol,instrument_type,last_price,lot_size,tick_size from t_cds_symbols where instrument_type='",
					extype,
					"' and tradingsymbol like '%",
					searchkey,
					"%'"
				}), "tbl_states");
				for (int j = 0; j < dt2.Rows.Count; j++)
				{
					mcxdata.Add(new t_mcx_data
					{
						name = dt2.Rows[j]["name"].ToString().Replace(" ", "_"),
						exchange = dt2.Rows[j]["exchange"].ToString(),
						tradingsymbol = dt2.Rows[j]["tradingsymbol"].ToString().Replace(" ", "_"),
						exchange_token = dt2.Rows[j]["exchange_token"].ToString(),
						expiry = dt2.Rows[j]["expiry"].ToString(),
						instrument_token = dt2.Rows[j]["instrument_token"].ToString(),
						instrument_type = dt2.Rows[j]["instrument_type"].ToString(),
						last_price = dt2.Rows[j]["last_price"].ToString(),
						lot_size = dt2.Rows[j]["lot_size"].ToString(),
						tick_size = dt2.Rows[j]["tick_size"].ToString()
					});
				}
			}
			return new JavaScriptSerializer().Serialize(mcxdata);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000CB78 File Offset: 0x0000AD78
		public string gettoken()
		{
			DataTable dt = Universal.SelectWithDS("select * from t_token", "t_token");
			if (dt.Rows.Count > 0)
			{
				return dt.Rows[0]["Token"].ToString();
			}
			return "error";
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000CBC4 File Offset: 0x0000ADC4
		public string getbrokerageperct(string btype, string uid, string type)
		{
			if (btype == "MCX")
			{
				DataTable dt = Universal.SelectWithDS(string.Concat(new string[]
				{
					"select ",
					type,
					"_brokerage_per_crore from t_trading_all_users_master where Mcx_Brokerage_Type='",
					btype,
					"' and Id='",
					uid,
					"'"
				}), "tbl_states");
				if (dt.Rows.Count > 0)
				{
					return dt.Rows[0][type + "_brokerage_per_crore"].ToString();
				}
				return "0";
			}
			else
			{
				DataTable dt2 = Universal.SelectWithDS(string.Concat(new string[]
				{
					"select ",
					type,
					"_brokerage_per_crore from t_trading_all_users_master where Id='",
					uid,
					"'"
				}), "tbl_states");
				if (dt2.Rows.Count > 0)
				{
					return dt2.Rows[0][type + "_brokerage_per_crore"].ToString();
				}
				return "0";
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000CCC4 File Offset: 0x0000AEC4
		public string updateorder(string lp, string brokerage, string BroughtBy, string ClosedAt, string orderno, string uid, string ordertype, string tokenno)
		{
			string cbrokerage = brokerage;
			if (brokerage == "NaN")
			{
				cbrokerage = "0";
			}
			if (decimal.Parse(cbrokerage) < 0m)
			{
				cbrokerage = "0";
			}
			DataTable dt = Universal.SelectWithDS(string.Concat(new string[]
			{
				"select LedgerBalance,UserName,(select DATE_FORMAT(OrderDate,'%Y-%m-%d') as OrderDate from t_user_order where OrderNo='",
				orderno,
				"' and TokenNo='",
				tokenno,
				"' limit 1) as OrderDate,(select OrderTime from t_user_order where OrderNo='",
				orderno,
				"' and TokenNo='",
				tokenno,
				"' limit 1) as OrderTime from t_trading_all_users_master where Id='",
				uid,
				"'"
			}), "t_trading_all_users_master");
			string totalledgerbalance = "0";
			string username = "";
			if (dt.Rows.Count > 0)
			{
				totalledgerbalance = dt.Rows[0]["LedgerBalance"].ToString();
				username = dt.Rows[0]["UserName"].ToString();
				dt.Rows[0]["OrderDate"].ToString();
				dt.Rows[0]["OrderTime"].ToString();
			}
			string newlbalnce = (decimal.Parse(totalledgerbalance) - decimal.Parse(cbrokerage)).ToString();
			string finalbalance;
			if (decimal.Parse(lp) < 0m)
			{
				decimal plamt = Math.Abs(decimal.Parse(lp));
				finalbalance = (decimal.Parse(newlbalnce) - plamt).ToString();
			}
			else
			{
				decimal plamt2 = decimal.Parse(lp);
				finalbalance = (decimal.Parse(newlbalnce) + plamt2).ToString();
			}
			string msg = string.Concat(new string[]
			{
				username,
				"(",
				uid,
				") has closed the trade of Order No ",
				orderno,
				" at ",
				BroughtBy,
				". Traded by client @ market"
			});
			IPAddress externalIp = IPAddress.Parse(Universal.devip);
			string ordertypeclose = "Market";
			string actionclose;
			if (ordertype == "SELL")
			{
				actionclose = "Bought by Trader";
			}
			else
			{
				actionclose = "Sold by Trader";
			}
			Universal.ExecuteNonQuery(string.Concat(new string[]
			{
				"update t_trading_all_users_master set LedgerBalance='",
				finalbalance,
				"' where Id='",
				uid,
				"'"
			}));
			int i = Universal.ExecuteNonQuery(string.Concat(new string[]
			{
				"update t_user_order set OrderStatus='Closed',BroughtBy='",
				BroughtBy,
				"',P_L='",
				lp,
				"',Brokerage='",
				brokerage,
				"',ClosedAt='",
				Universal.GetDate,
				"',ClosedTime='",
				Universal.GetTime,
				"',OrderTypeClose='",
				ordertypeclose,
				"',ActionByClose='",
				actionclose,
				"' where orderno='",
				orderno,
				"'"
			}));
			Universal.ExecuteNonQuery(string.Concat(new string[]
			{
				"delete from tbl_tradepointer where UserId='",
				uid,
				"';insert into tbl_tradepointer (UserId) values('",
				uid,
				"')"
			}));
			if (i == 1)
			{
				string[] array = new string[11];
				array[0] = "insert into t_action_master (Message,ActionDate,ActionTime,ActionFrom,IpAddress) values('";
				array[1] = msg;
				array[2] = "','";
				array[3] = Universal.GetDate;
				array[4] = "','";
				array[5] = Universal.GetTime;
				array[6] = "','";
				array[7] = uid;
				array[8] = "','";
				int num = 9;
				IPAddress ipaddress = externalIp;
				array[num] = ((ipaddress != null) ? ipaddress.ToString() : null);
				array[10] = "')";
				Universal.ExecuteNonQuery(string.Concat(array));
				return "true";
			}
			return "false";
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000D058 File Offset: 0x0000B258
		[HttpPost]
		public string gettradesymbols(string extype, string searchkey, string UserId)
		{
			List<t_universal_tradeble_symbol> mcxdata = new List<t_universal_tradeble_symbol>();
			if (searchkey == "null")
			{
				DataTable dt = Universal.SelectWithDS("select instrument_token,symbolname,last_price,volume,open_,high_,low_,close_,`change`,oi,oi_day_high,oi_day_low,sell,buy,DATE_FORMAT(expirydate,'%d/%m/%Y') as expirydate,lotsize,Exchange,SymbolType from t_universal_tradeble_tokens where Exchange='" + extype + "' order by symbolname", "tbl_states");
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					t_universal_tradeble_symbol cmd = new t_universal_tradeble_symbol();
					cmd.instrument_token = dt.Rows[i]["instrument_token"].ToString();
					cmd.symbolname = dt.Rows[i]["symbolname"].ToString();
					cmd.last_price = dt.Rows[i]["last_price"].ToString();
					cmd.volume = dt.Rows[i]["volume"].ToString();
					cmd.open_ = dt.Rows[i]["open_"].ToString();
					cmd.high_ = dt.Rows[i]["high_"].ToString();
					cmd.low_ = dt.Rows[i]["low_"].ToString();
					cmd.close_ = dt.Rows[i]["close_"].ToString();
					cmd.change = dt.Rows[i]["change"].ToString();
					cmd.oi = dt.Rows[i]["oi"].ToString();
					cmd.oi_day_high = dt.Rows[i]["oi_day_high"].ToString();
					cmd.oi_day_low = dt.Rows[i]["oi_day_low"].ToString();
					cmd.sell = dt.Rows[i]["buy"].ToString();
					cmd.buy = dt.Rows[i]["sell"].ToString();
					cmd.expirydate = dt.Rows[i]["expirydate"].ToString();
					cmd.lotsize = dt.Rows[i]["lotsize"].ToString();
					cmd.Exchange = dt.Rows[i]["Exchange"].ToString();
					cmd.SymbolType = dt.Rows[i]["SymbolType"].ToString();
					if (Universal.SelectWithDS(string.Concat(new string[]
					{
						"select * from t_selected_symbols_by_user where UserId='",
						UserId,
						"' and SymbolToken='",
						cmd.instrument_token,
						"'"
					}), "selectedtokn").Rows.Count > 0)
					{
						cmd.isSelect = true;
					}
					else
					{
						cmd.isSelect = false;
					}
					mcxdata.Add(cmd);
				}
			}
			else
			{
				DataTable dt2 = Universal.SelectWithDS(string.Concat(new string[]
				{
					"select instrument_token,symbolname,last_price,volume,open_,high_,low_,close_,change,oi,oi_day_high,oi_day_low,sell,buy,DATE_FORMAT(expirydate,'%d/%m/%Y') as expirydate,lotsize,Exchange,SymbolType from t_universal_tradeble_tokens where Exchange='",
					extype,
					"' and symbolname like '%",
					searchkey,
					"%'"
				}), "tbl_states");
				for (int j = 0; j < dt2.Rows.Count; j++)
				{
					t_universal_tradeble_symbol cmd2 = new t_universal_tradeble_symbol();
					cmd2.instrument_token = dt2.Rows[j]["symbolname"].ToString();
					cmd2.symbolname = dt2.Rows[j]["symbolname"].ToString();
					cmd2.last_price = dt2.Rows[j]["last_price"].ToString();
					cmd2.volume = dt2.Rows[j]["volume"].ToString();
					cmd2.open_ = dt2.Rows[j]["open_"].ToString();
					cmd2.high_ = dt2.Rows[j]["high_"].ToString();
					cmd2.low_ = dt2.Rows[j]["low_"].ToString();
					cmd2.close_ = dt2.Rows[j]["close_"].ToString();
					cmd2.change = dt2.Rows[j]["change"].ToString();
					cmd2.oi = dt2.Rows[j]["oi"].ToString();
					cmd2.oi_day_high = dt2.Rows[j]["oi_day_high"].ToString();
					cmd2.oi_day_low = dt2.Rows[j]["oi_day_low"].ToString();
					cmd2.sell = dt2.Rows[j]["sell"].ToString();
					cmd2.buy = dt2.Rows[j]["buy"].ToString();
					cmd2.expirydate = dt2.Rows[j]["expirydate"].ToString();
					cmd2.lotsize = dt2.Rows[j]["lotsize"].ToString();
					cmd2.Exchange = dt2.Rows[j]["Exchange"].ToString();
					cmd2.SymbolType = dt2.Rows[j]["SymbolType"].ToString();
					if (Universal.SelectWithDS(string.Concat(new string[]
					{
						"select * as rowcount from t_selected_symbols_by_user where UserId='",
						UserId,
						"' and SymbolToken='",
						cmd2.instrument_token,
						"'"
					}), "selectedtokn").Rows.Count > 0)
					{
						cmd2.isSelect = true;
					}
					else
					{
						cmd2.isSelect = false;
					}
					mcxdata.Add(cmd2);
				}
			}
			return new JavaScriptSerializer().Serialize(mcxdata);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000D6C4 File Offset: 0x0000B8C4
		public string getsymbols(string extype, string searchkey)
		{
			List<t_mcx_data> mcxdata = new List<t_mcx_data>();
			if (searchkey == "null")
			{
				DataTable dt = Universal.SelectWithDS("select symbolname,Exchange,instrument_token,DATE_FORMAT(expirydate,'%d %M %Y') as expiry,instrument_token,SymbolType,last_price,lotsize from t_universal_tradeble_tokens where Exchange='" + extype + "' order by symbolname", "tbl_states");
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					mcxdata.Add(new t_mcx_data
					{
						name = dt.Rows[i]["symbolname"].ToString(),
						exchange = dt.Rows[i]["Exchange"].ToString(),
						tradingsymbol = dt.Rows[i]["symbolname"].ToString(),
						exchange_token = dt.Rows[i]["instrument_token"].ToString(),
						expiry = dt.Rows[i]["expiry"].ToString(),
						instrument_token = dt.Rows[i]["instrument_token"].ToString(),
						instrument_type = dt.Rows[i]["SymbolType"].ToString(),
						last_price = dt.Rows[i]["last_price"].ToString(),
						lot_size = dt.Rows[i]["lotsize"].ToString(),
						tick_size = ""
					});
				}
			}
			else
			{
				DataTable dt2 = Universal.SelectWithDS(string.Concat(new string[]
				{
					"select symbolname,Exchange,instrument_token,DATE_FORMAT(expirydate,'%d %M %Y') as expiry,instrument_token,SymbolType,last_price,lotsize from t_universal_tradeble_tokens where Exchange='",
					extype,
					"' and symbolname like '%",
					searchkey,
					"%'"
				}), "tbl_states");
				for (int j = 0; j < dt2.Rows.Count; j++)
				{
					mcxdata.Add(new t_mcx_data
					{
						name = dt2.Rows[j]["symbolname"].ToString(),
						exchange = dt2.Rows[j]["Exchange"].ToString(),
						tradingsymbol = dt2.Rows[j]["symbolname"].ToString(),
						exchange_token = dt2.Rows[j]["instrument_token"].ToString(),
						expiry = dt2.Rows[j]["expiry"].ToString(),
						instrument_token = dt2.Rows[j]["instrument_token"].ToString(),
						instrument_type = dt2.Rows[j]["SymbolType"].ToString(),
						last_price = dt2.Rows[j]["last_price"].ToString(),
						lot_size = dt2.Rows[j]["lotsize"].ToString(),
						tick_size = ""
					});
				}
			}
			return new JavaScriptSerializer().Serialize(mcxdata);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000DA28 File Offset: 0x0000BC28
		public string getledgerbalance(string uid)
		{
			DataTable dt = Universal.SelectWithDS("select LedgerBalance from t_trading_all_users_master where Id='" + uid + "' ", "t_trading_all_users_master");
			if (dt.Rows.Count > 0)
			{
				return dt.Rows[0]["LedgerBalance"].ToString();
			}
			return "0";
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000DA80 File Offset: 0x0000BC80
		[HttpPost]
		public string getuserbalanceledgerhistory(string UserId)
		{
			DateTime startdate = this.FirstDayOfWeek(DateTime.Now);
			DateTime enddate = this.LastDayOfWeek(DateTime.Now);
			string sdate = string.Concat(new string[]
			{
				startdate.Year.ToString(),
				"-",
				startdate.Month.ToString(),
				"-",
				startdate.Day.ToString()
			});
			string edate = string.Concat(new string[]
			{
				enddate.Year.ToString(),
				"-",
				enddate.Month.ToString(),
				"-",
				enddate.Day.ToString()
			});
			List<t_user_fund_details> userbalance = new List<t_user_fund_details>();
			DataTable dt = Universal.SelectWithDS(string.Concat(new string[]
			{
				"select Id,UserID,Amount,TransactionType,DATE_FORMAT(CreatedDate,'%d/%m/%Y') as CreatedDate from t_userfunds where UserID='",
				UserId,
				"' and DATE(CreatedDate) BETWEEN '",
				sdate,
				"' AND '",
				edate,
				"'"
			}), "t_userfunds");
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				t_user_fund_details cmd = new t_user_fund_details();
				cmd.Id = int.Parse(dt.Rows[i]["Id"].ToString());
				cmd.UserID = dt.Rows[i]["UserID"].ToString();
				cmd.Amount = dt.Rows[i]["Amount"].ToString();
				if (dt.Rows[i]["TransactionType"].ToString() == "1")
				{
					cmd.TransactionType = "Credit";
				}
				else
				{
					cmd.TransactionType = "Debit";
				}
				cmd.CreatedDate = dt.Rows[i]["CreatedDate"].ToString();
				userbalance.Add(cmd);
			}
			return new JavaScriptSerializer().Serialize(userbalance);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000DCB0 File Offset: 0x0000BEB0
		public string getuserbalanceledger(string uid)
		{
			DateTime startdate = this.FirstDayOfWeek(DateTime.Now);
			DateTime enddate = this.LastDayOfWeek(DateTime.Now);
			string sdate = string.Concat(new string[]
			{
				startdate.Year.ToString(),
				"-",
				startdate.Month.ToString(),
				"-",
				startdate.Day.ToString()
			});
			string edate = string.Concat(new string[]
			{
				enddate.Year.ToString(),
				"-",
				enddate.Month.ToString(),
				"-",
				enddate.Day.ToString()
			});
			List<t_user_fund_details> userbalance = new List<t_user_fund_details>();
			DataTable dt = Universal.SelectWithDS(string.Concat(new string[]
			{
				"select Id,UserID,Amount,TransactionType,DATE_FORMAT(CreatedDate,'%d-%M-%Y %H:%m') as CreatedDate,'' as orderid,'' as ScriptName,'' as TokenNo,'' as Lot,'' as OrderCategory from t_userfunds where UserID='",
				uid,
				"' and DATE(CreatedDate) BETWEEN '",
				sdate,
				"' AND '",
				edate,
				"' UNION SELECT Id,UserID, ((P_L)-(Brokerage)) as Amount,(CASE WHEN ((P_L)-(Brokerage))<=0 THEN 'Loss' else 'Profit' End) as TransactionType,DATE_FORMAT(concat(ClosedAt,' ',ClosedTime),'%d-%M-%Y %H:%i') as CreatedDate,id as orderid,ScriptName,TokenNo,Lot,OrderCategory FROM t_user_order WHERE UserId='",
				uid,
				"' and week(ClosedAt)=week(now()-1)  order by CreatedDate desc"
			}), "t_userfunds");
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				t_user_fund_details cmd = new t_user_fund_details();
				cmd.Id = int.Parse(dt.Rows[i]["Id"].ToString());
				cmd.UserID = dt.Rows[i]["UserID"].ToString();
				cmd.Amount = dt.Rows[i]["Amount"].ToString();
				if (dt.Rows[i]["TransactionType"].ToString() == "1")
				{
					cmd.TransactionType = "Credit";
				}
				else if (dt.Rows[i]["TransactionType"].ToString() == "Loss")
				{
					cmd.TransactionType = "Loss";
				}
				else if (dt.Rows[i]["TransactionType"].ToString() == "Profit")
				{
					cmd.TransactionType = "Profit";
				}
				else
				{
					cmd.TransactionType = "Debit";
				}
				cmd.CreatedDate = dt.Rows[i]["CreatedDate"].ToString();
				cmd.OrderId = dt.Rows[i]["orderid"].ToString();
				cmd.ScriptName = dt.Rows[i]["ScriptName"].ToString();
				cmd.TokenNo = dt.Rows[i]["TokenNo"].ToString();
				cmd.Lot = dt.Rows[i]["Lot"].ToString();
				cmd.OrderCategory = dt.Rows[i]["OrderCategory"].ToString();
				userbalance.Add(cmd);
			}
			return new JavaScriptSerializer().Serialize(userbalance);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000E010 File Offset: 0x0000C210
		[HttpPost]
		public string chgpwd(string UserId, string oldpass, string newpass)
		{
			if (Universal.ExecuteNonQuery(string.Concat(new string[]
			{
				"update t_trading_all_users_master set Password_='",
				newpass,
				"' where Id='",
				UserId,
				"' and Password_='",
				oldpass,
				"'"
			})) == 1)
			{
				return "{" + "\"ResponseCode\":\"200\"," + "\"ResponseMessage\":\"Password Changed Successfully\"" + "}";
			}
			return "{" + "\"ResponseCode\":\"201\"," + "\"ResponseMessage\":\"Failed\"" + "}";
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000E0A4 File Offset: 0x0000C2A4
		public string changepassword(string userid, string oldpass, string newpass)
		{
			if (Universal.ExecuteNonQuery(string.Concat(new string[]
			{
				"update t_trading_all_users_master set Password_='",
				newpass,
				"',FirstTimeLogin='false' where Id='",
				userid,
				"' and Password_='",
				oldpass,
				"'"
			})) == 1)
			{
				return "true";
			}
			return "false";
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000E0FB File Offset: 0x0000C2FB
		public string closechangepass(string userid)
		{
			if (Universal.ExecuteNonQuery("update t_trading_all_users_master set FirstTimeLogin='false' where Id='" + userid + "'") == 1)
			{
				return "true";
			}
			return "false";
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000E120 File Offset: 0x0000C320
		public string changeactivestatus(string userid)
		{
			if (Universal.ExecuteNonQuery("update t_trading_all_users_master set IsOnline='Active' where Id='" + userid + "'") == 1)
			{
				return Universal.ExecuteScalar("select Account_status from t_trading_all_users_master where Id='" + userid + "'").ToString();
			}
			return "false";
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000E15C File Offset: 0x0000C35C
		public string onselectforload(string tokenid)
		{
			string externalIpString = Universal.devip;
			if (externalIpString == "localhost")
			{
				externalIpString = "192.168.1.1";
			}
			IPAddress.Parse(externalIpString);
			if (Universal.ExecuteNonQuery(string.Concat(new string[]
			{
				"insert into t_selected_tokens_refreshcallback (symboltoken,ipaddress) values('",
				tokenid,
				"','",
				externalIpString,
				"') "
			})) == 1)
			{
				return "true";
			}
			return "false";
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000E1C8 File Offset: 0x0000C3C8
		public string getactiveorderwithcmp(string uid)
		{
			List<ActiveOrderWithCMP> orderdata = new List<ActiveOrderWithCMP>();
			DataTable dt = Universal.SelectWithDS("select t_user_order.Id,selectedlotsize,OrderCategory,TokenNo,OrderPrice,Lot,t_universal_tradeble_tokens.buy,t_universal_tradeble_tokens.sell from t_user_order INNER JOIN t_universal_tradeble_tokens ON t_user_order.TokenNo=t_universal_tradeble_tokens.instrument_token where t_user_order.OrderStatus='Active' and UserId='" + uid + "'", "t_user_order");
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				ActiveOrderWithCMP ord = new ActiveOrderWithCMP();
				ord.id = int.Parse(dt.Rows[i]["Id"].ToString());
				ord.selectedLotSize = dt.Rows[i]["selectedlotsize"].ToString();
				ord.OrderCategory = dt.Rows[i]["OrderCategory"].ToString();
				ord.tokenno = dt.Rows[i]["TokenNo"].ToString();
				ord.orderprice = dt.Rows[i]["OrderPrice"].ToString();
				ord.LotQty = dt.Rows[i]["Lot"].ToString();
				string orderCategory = ord.OrderCategory;
				string globaltradeprice = ord.orderprice;
				string sell = dt.Rows[i]["sell"].ToString();
				string buy = dt.Rows[i]["buy"].ToString();
				string selectedlotsize = ord.selectedLotSize;
				decimal closevalue = 0m;
				string cmp;
				if (orderCategory == "SELL")
				{
					cmp = sell;
					closevalue = decimal.Parse(globaltradeprice) - decimal.Parse(sell);
				}
				else
				{
					cmp = buy;
					closevalue = decimal.Parse(buy) - decimal.Parse(globaltradeprice);
				}
				ord.cmp = cmp;
				ord.diffval = closevalue.ToString();
				ord.pl = Math.Round(closevalue * int.Parse(selectedlotsize) * int.Parse(ord.LotQty), 1).ToString();
				orderdata.Add(ord);
			}
			return new JavaScriptSerializer().Serialize(orderdata);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000E3E8 File Offset: 0x0000C5E8
		public string getalltradeforAutoSqarePartialView(string uid)
		{
			List<t_order_master> orderdata = new List<t_order_master>();
			DataTable dt = Universal.SelectWithDS("select Id,OrderDate,OrderTime,OrderNo, MarginUsed,HoldingMarginReq,UserId,selectedlotsize,isstoplossorder,UserName,OrderCategory,OrderType,ScriptName,TokenNo,ActionBy,SymbolType,Lot,OrderPrice from t_user_order where OrderStatus='Active' and UserId='" + uid + "'", "t_user_order");
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				orderdata.Add(new t_order_master
				{
					Id = dt.Rows[i]["Id"].ToString(),
					OrderDate = dt.Rows[i]["OrderDate"].ToString().Replace("12:00:00 AM", "").Replace("00:00:00", ""),
					OrderTime = dt.Rows[i]["OrderTime"].ToString(),
					OrderNo = dt.Rows[i]["OrderNo"].ToString(),
					UserId = dt.Rows[i]["UserId"].ToString(),
					selectedlotsize = dt.Rows[i]["selectedlotsize"].ToString(),
					isstoplossorder = dt.Rows[i]["isstoplossorder"].ToString(),
					UserName = dt.Rows[i]["UserName"].ToString(),
					OrderCategory = dt.Rows[i]["OrderCategory"].ToString(),
					OrderType = dt.Rows[i]["OrderType"].ToString(),
					ScriptName = dt.Rows[i]["ScriptName"].ToString(),
					TokenNo = dt.Rows[i]["TokenNo"].ToString(),
					Lot = dt.Rows[i]["Lot"].ToString(),
					OrderPrice = dt.Rows[i]["OrderPrice"].ToString(),
					MarginUsed = dt.Rows[i]["MarginUsed"].ToString(),
					HoldingMarginReq = dt.Rows[i]["HoldingMarginReq"].ToString(),
					SymbolType = dt.Rows[i]["SymbolType"].ToString()
				});
			}
			return new JavaScriptSerializer().Serialize(orderdata);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000E69C File Offset: 0x0000C89C
		public string getconsolidatedtrade(string uid)
		{
			DateTime startdate = this.FirstDayOfWeek(DateTime.Now);
			DateTime enddate = this.LastDayOfWeek(DateTime.Now);
			string sdate = string.Concat(new string[]
			{
				startdate.Year.ToString(),
				"-",
				startdate.Month.ToString(),
				"-",
				startdate.Day.ToString()
			});
			string edate = string.Concat(new string[]
			{
				enddate.Year.ToString(),
				"-",
				enddate.Month.ToString(),
				"-",
				enddate.Day.ToString()
			});
			List<t_order_master> orderdata = new List<t_order_master>();
			DataTable dt = Universal.SelectWithDS(string.Concat(new string[]
			{
				"select Id,OrderDate,OrderTime,OrderNo,SUM(MarginUsed) as MarginUsed,HoldingMarginReq,UserId,selectedlotsize,(case when OrderCategory='SELL' THEN (select sell from t_universal_tradeble_tokens where instrument_token=t_user_order.TokenNo) WHEN OrderCategory='BUY' THEN (select buy from t_universal_tradeble_tokens where instrument_token=t_user_order.TokenNo)  END) as cmp,isstoplossorder,UserName,OrderCategory,OrderType,ScriptName,TokenNo,ActionBy,SymbolType,SUM(Lot) as Lot,SUM(OrderPrice*Lot)/SUM(Lot) as OrderPrice,count(*) as totalrows from t_user_order where OrderStatus='Active' and OrderDate between '",
				sdate,
				"' and '",
				edate,
				"' and UserId='",
				uid,
				"' group by ScriptName,OrderCategory"
			}), "t_user_order");
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				orderdata.Add(new t_order_master
				{
					Id = dt.Rows[i]["Id"].ToString(),
					OrderDate = dt.Rows[i]["OrderDate"].ToString().Replace("12:00:00 AM", "").Replace("00:00:00", ""),
					OrderTime = dt.Rows[i]["OrderTime"].ToString(),
					OrderNo = dt.Rows[i]["OrderNo"].ToString(),
					UserId = dt.Rows[i]["UserId"].ToString(),
					selectedlotsize = dt.Rows[i]["selectedlotsize"].ToString(),
					isstoplossorder = dt.Rows[i]["isstoplossorder"].ToString(),
					UserName = dt.Rows[i]["UserName"].ToString(),
					OrderCategory = dt.Rows[i]["OrderCategory"].ToString(),
					OrderType = dt.Rows[i]["OrderType"].ToString(),
					ScriptName = dt.Rows[i]["ScriptName"].ToString(),
					TokenNo = dt.Rows[i]["TokenNo"].ToString(),
					cmp = dt.Rows[i]["cmp"].ToString(),
					Lot = dt.Rows[i]["Lot"].ToString(),
					OrderPrice = decimal.Parse(dt.Rows[i]["OrderPrice"].ToString()).ToString(),
					MarginUsed = dt.Rows[i]["MarginUsed"].ToString(),
					HoldingMarginReq = dt.Rows[i]["HoldingMarginReq"].ToString(),
					SymbolType = dt.Rows[i]["SymbolType"].ToString()
				});
			}
			return new JavaScriptSerializer().Serialize(orderdata);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000EAA8 File Offset: 0x0000CCA8
		[HttpPost]
		public string getconsolidatedtrade_forapp(string userid)
		{
			List<t_order_master> orderdata = new List<t_order_master>();
			DataTable dt = Universal.SelectWithDS("select Id,OrderDate,OrderTime,OrderNo,MarginUsed,HoldingMarginReq,UserId,selectedlotsize,isstoplossorder,UserName,OrderCategory,OrderType,ScriptName,TokenNo,ActionBy,SymbolType,SUM(Lot) as Lot,SUM(OrderPrice) as OrderPrice,count(*) as totalrows from t_user_order where OrderStatus='Active' and UserId='" + userid + "' group by ScriptName,OrderCategory", "t_user_order");
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				orderdata.Add(new t_order_master
				{
					Id = dt.Rows[i]["Id"].ToString(),
					OrderDate = dt.Rows[i]["OrderDate"].ToString().Replace("12:00:00 AM", "").Replace("00:00:00", ""),
					OrderTime = dt.Rows[i]["OrderTime"].ToString(),
					OrderNo = dt.Rows[i]["OrderNo"].ToString(),
					UserId = dt.Rows[i]["UserId"].ToString(),
					selectedlotsize = dt.Rows[i]["selectedlotsize"].ToString(),
					isstoplossorder = dt.Rows[i]["isstoplossorder"].ToString(),
					UserName = dt.Rows[i]["UserName"].ToString(),
					OrderCategory = dt.Rows[i]["OrderCategory"].ToString(),
					OrderType = dt.Rows[i]["OrderType"].ToString(),
					ScriptName = dt.Rows[i]["ScriptName"].ToString(),
					TokenNo = dt.Rows[i]["TokenNo"].ToString(),
					Lot = dt.Rows[i]["Lot"].ToString(),
					OrderPrice = (decimal.Parse(dt.Rows[i]["OrderPrice"].ToString()) / decimal.Parse(dt.Rows[i]["totalrows"].ToString())).ToString(),
					MarginUsed = dt.Rows[i]["MarginUsed"].ToString(),
					HoldingMarginReq = dt.Rows[i]["HoldingMarginReq"].ToString(),
					SymbolType = dt.Rows[i]["SymbolType"].ToString()
				});
			}
			return new JavaScriptSerializer().Serialize(orderdata);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000ED8C File Offset: 0x0000CF8C
		[HttpPost]
		public void edit_tradesettlementpost_admin(t_order_master obj)
		{
			obj.OrderDate = Universal.GetDate;
			obj.OrderTime = Universal.GetTime;
			obj.OrderNo = new Random().Next().ToString();
			(decimal.Parse(obj.OrderPrice) * decimal.Parse(obj.Lot)).ToString();
			IPAddress externalIp = IPAddress.Parse(new WebClient().DownloadString("http://icanhazip.com").Replace("\\r\\n", "").Replace("\\n", "").Trim());
			string userId = obj.UserId;
			string checkcond;
			if (obj.OrderCategory == "SELL")
			{
				checkcond = "BUY";
				obj.ActionType = "Sold By Admin";
			}
			else
			{
				checkcond = "SELL";
				obj.ActionType = "Bought By Admin";
			}
			string similer_syml;
			if (obj.SymbolType == "MCX")
			{
				string[] symarr = obj.ScriptName.Split(new char[]
				{
					'_'
				});
				similer_syml = this.setsymbol(symarr[0].Trim());
			}
			else
			{
				similer_syml = "GOLD";
			}
			int totaldatabase_lot = 0;
			DataTable dt = Universal.SelectWithDS(string.Concat(new string[]
			{
				"select (select SUM(Lot) from t_user_order where UserId='",
				obj.UserId,
				"' and OrderCategory='",
				checkcond,
				"' and OrderStatus='Active' and TokenNo='",
				obj.TokenNo,
				"' and Id!='",
				obj.Id,
				"') as totaldatabase_lot,t_trading_all_users_master.NSE_Exposure_Type,t_trading_all_users_master.NSE_Brokerage_Type,t_trading_all_users_master.Equity_brokerage_per_crore,t_trading_all_users_master.Intraday_Exposure_Margin_Equity,t_trading_all_users_master.Holding_Exposure_Margin_Equity,t_trading_all_users_master.Mcx_Brokerage_Type,t_trading_all_users_master.MCX_brokerage_per_crore,t_trading_all_users_master.",
				similer_syml,
				"_brokerage,t_trading_all_users_master.Mcx_Exposure_Type, t_trading_all_users_master.Intraday_Exposure_Margin_MCX, t_trading_all_users_master.Holding_Exposure_Margin_MCX, t_trading_all_users_master.MCX_Exposure_Lot_wise_",
				similer_syml,
				"_Intraday, t_trading_all_users_master.MCX_Exposure_Lot_wise_",
				similer_syml,
				"_Holding,t_trading_all_users_master.CDS_Brokerage_Type ,t_trading_all_users_master.CDS_brokerage_per_crore ,t_trading_all_users_master.CDS_Exposure_Type ,t_trading_all_users_master.Intraday_Exposure_Margin_CDS ,t_trading_all_users_master.Holding_Exposure_Margin_CDS  from t_trading_all_users_master where Id='",
				obj.UserId,
				"'  "
			}), "t_user_order");
			string NSE_Exposure_Type = "";
			string NSE_Brokerage_Type = "";
			string Equity_brokerage_per_crore = "";
			string Intraday_Exposure_Margin_Equity = "";
			string Holding_Exposure_Margin_Equity = "";
			string Mcx_Brokerage_Type = "";
			string MCX_brokerage_per_crore = "";
			string MCXsymb_brokerage = "";
			string Mcx_Exposure_Type = "";
			string Intraday_Exposure_Margin_MCX = "";
			string Holding_Exposure_Margin_MCX = "";
			string MCX_Exposure_Lot_wise_sym_Intraday = "";
			string MCX_Exposure_Lot_wise_sym_Holding = "";
			string CDS_Exposure_Type = "";
			string CDS_Brokerage_Type = "";
			string CDS_brokerage_per_crore = "";
			string Intraday_Exposure_Margin_CDS = "";
			string Holding_Exposure_Margin_CDS = "";
			decimal finalbrokerage = 0m;
			decimal final_intraday_exp = 0m;
			decimal final_indraday_holding = 0m;
			if (dt.Rows.Count > 0)
			{
				NSE_Exposure_Type = dt.Rows[0]["NSE_Exposure_Type"].ToString();
				NSE_Brokerage_Type = dt.Rows[0]["NSE_Brokerage_Type"].ToString();
				Equity_brokerage_per_crore = dt.Rows[0]["Equity_brokerage_per_crore"].ToString();
				Intraday_Exposure_Margin_Equity = dt.Rows[0]["Intraday_Exposure_Margin_Equity"].ToString();
				Holding_Exposure_Margin_Equity = dt.Rows[0]["Holding_Exposure_Margin_Equity"].ToString();
				Mcx_Brokerage_Type = dt.Rows[0]["Mcx_Brokerage_Type"].ToString();
				MCX_brokerage_per_crore = dt.Rows[0]["MCX_brokerage_per_crore"].ToString();
				MCXsymb_brokerage = dt.Rows[0][similer_syml + "_brokerage"].ToString();
				Mcx_Exposure_Type = dt.Rows[0]["Mcx_Exposure_Type"].ToString();
				Intraday_Exposure_Margin_MCX = dt.Rows[0]["Intraday_Exposure_Margin_MCX"].ToString();
				Holding_Exposure_Margin_MCX = dt.Rows[0]["Holding_Exposure_Margin_MCX"].ToString();
				MCX_Exposure_Lot_wise_sym_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_" + similer_syml + "_Intraday"].ToString();
				MCX_Exposure_Lot_wise_sym_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_" + similer_syml + "_Holding"].ToString();
				CDS_Exposure_Type = dt.Rows[0]["CDS_Exposure_Type"].ToString();
				CDS_Brokerage_Type = dt.Rows[0]["CDS_Brokerage_Type"].ToString();
				CDS_brokerage_per_crore = dt.Rows[0]["CDS_brokerage_per_crore"].ToString();
				Intraday_Exposure_Margin_CDS = dt.Rows[0]["Intraday_Exposure_Margin_CDS"].ToString();
				Holding_Exposure_Margin_CDS = dt.Rows[0]["Holding_Exposure_Margin_CDS"].ToString();
				if (dt.Rows[0]["totaldatabase_lot"].ToString() != "")
				{
					totaldatabase_lot = int.Parse(dt.Rows[0]["totaldatabase_lot"].ToString());
				}
				else
				{
					totaldatabase_lot = 0;
				}
			}
			if (totaldatabase_lot > int.Parse(obj.Lot))
			{
				DataTable pretable = Universal.SelectWithDS(string.Concat(new string[]
				{
					"select t_user_order.Id,selectedlotsize,DATE_FORMAT(OrderDate,'%Y-%m-%d') as OrderDate,OrderTime,OrderCategory,TokenNo,OrderPrice,t_user_order.SymbolType,Lot,t_universal_tradeble_tokens.buy,t_universal_tradeble_tokens.sell from t_user_order INNER JOIN t_universal_tradeble_tokens ON t_user_order.TokenNo=t_universal_tradeble_tokens.instrument_token  INNER JOIN  t_trading_all_users_master ON t_trading_all_users_master.Id=t_user_order.UserId where UserId='",
					obj.UserId,
					"' and TokenNo='",
					obj.TokenNo,
					"' and OrderCategory='",
					checkcond,
					"' and OrderStatus='Active' and t_user_order.Id!='",
					obj.Id,
					"' ORDER BY Id"
				}), "t_user_order");
				int incoming_lot = int.Parse(obj.Lot);
				for (int i = 0; i < pretable.Rows.Count; i++)
				{
					string Id = pretable.Rows[i]["Id"].ToString();
					string Lot = pretable.Rows[i]["Lot"].ToString();
					pretable.Rows[i]["buy"].ToString();
					pretable.Rows[i]["sell"].ToString();
					string OrderPrice = pretable.Rows[i]["OrderPrice"].ToString();
					string OrderCategory = pretable.Rows[i]["OrderCategory"].ToString();
					string OrderDate = pretable.Rows[i]["OrderDate"].ToString();
					string OrderTime = pretable.Rows[i]["OrderTime"].ToString();
					if (obj.SymbolType == "MCX")
					{
						if (Mcx_Brokerage_Type == "per_lot")
						{
							finalbrokerage = decimal.Parse(MCXsymb_brokerage);
						}
						else
						{
							finalbrokerage = (decimal.Parse(OrderPrice) + decimal.Parse(obj.OrderPrice)) * decimal.Parse(obj.selectedlotsize) / 10000000m * decimal.Parse(MCX_brokerage_per_crore);
						}
						if (Mcx_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(MCX_Exposure_Lot_wise_sym_Intraday);
							final_indraday_holding = decimal.Parse(MCX_Exposure_Lot_wise_sym_Holding);
						}
						else
						{
							final_intraday_exp = decimal.Parse(OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_MCX);
							final_indraday_holding = decimal.Parse(OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_MCX);
						}
					}
					else if (obj.SymbolType == "NSE")
					{
						if (NSE_Brokerage_Type == "per_lot")
						{
							finalbrokerage = decimal.Parse(Equity_brokerage_per_crore);
						}
						else
						{
							finalbrokerage = (decimal.Parse(OrderPrice) + decimal.Parse(obj.OrderPrice)) * decimal.Parse(obj.selectedlotsize) / 10000000m * decimal.Parse(Equity_brokerage_per_crore);
						}
						if (NSE_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(Intraday_Exposure_Margin_Equity);
							final_indraday_holding = decimal.Parse(Holding_Exposure_Margin_Equity);
						}
						else
						{
							final_intraday_exp = decimal.Parse(OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_Equity);
							final_indraday_holding = decimal.Parse(OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_Equity);
						}
					}
					else if (obj.SymbolType == "OPT")
					{
						if (CDS_Brokerage_Type == "per_lot")
						{
							finalbrokerage = decimal.Parse(CDS_brokerage_per_crore);
						}
						else
						{
							finalbrokerage = (decimal.Parse(OrderPrice) + decimal.Parse(obj.OrderPrice)) * decimal.Parse(obj.selectedlotsize) / 10000000m * decimal.Parse(CDS_brokerage_per_crore);
						}
						if (CDS_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(Intraday_Exposure_Margin_CDS);
							final_indraday_holding = decimal.Parse(Holding_Exposure_Margin_CDS);
						}
						else
						{
							final_intraday_exp = decimal.Parse(OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_CDS);
							final_indraday_holding = decimal.Parse(OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_CDS);
						}
					}
					if (incoming_lot > int.Parse(Lot))
					{
						int sublot = incoming_lot - int.Parse(Lot);
						decimal closevalue = 0m;
						string ActionCloseBy;
						string cmprice;
						if (obj.OrderCategory == "SELL")
						{
							ActionCloseBy = "Sold By Trader";
							cmprice = obj.OrderPrice;
							closevalue = decimal.Parse(cmprice) - decimal.Parse(OrderPrice);
						}
						else
						{
							ActionCloseBy = "Bought By Trader";
							cmprice = obj.OrderPrice;
							closevalue = decimal.Parse(OrderPrice) - decimal.Parse(cmprice);
						}
						decimal final_pl = Math.Round(closevalue * int.Parse(obj.selectedlotsize) * int.Parse(Lot), 1);
						DataTable brokeragetbl = Universal.SelectWithDS("select LedgerBalance,creditLimit from t_trading_all_users_master where Id='" + obj.UserId + "'", "t_trading_all_users_master");
						decimal LedgerBalance = 0m;
						if (brokeragetbl.Rows.Count > 0)
						{
							LedgerBalance = decimal.Parse(brokeragetbl.Rows[0][0].ToString());
							decimal.Parse(brokeragetbl.Rows[0][1].ToString());
						}
						finalbrokerage *= int.Parse(Lot);
						if (Universal.ExecuteNonQuery(string.Concat(new string[]
						{
							"update t_user_order set P_L='",
							final_pl.ToString(),
							"',OrderTypeClose='Market',ActionByClose='",
							ActionCloseBy,
							"',OrderStatus='Closed',Brokerage='",
							finalbrokerage.ToString(),
							"',BroughtBy='",
							cmprice,
							"',ClosedAt='",
							Universal.GetDate,
							"',ClosedTime='",
							Universal.GetTime,
							"' where Id='",
							Id,
							"'"
						})) == 1)
						{
							string msg = string.Concat(new string[]
							{
								obj.UserName,
								"(",
								obj.UserId,
								") have ",
								obj.OrderType,
								" ",
								obj.Lot,
								" lots of ",
								obj.ScriptName,
								" at ",
								obj.OrderPrice,
								" Auto Closed due to settlement."
							});
							string[] array = new string[11];
							array[0] = "insert into t_action_master (Message,ActionDate,ActionTime,ActionFrom,IpAddress) values('";
							array[1] = msg;
							array[2] = "','";
							array[3] = Universal.GetDate;
							array[4] = "','";
							array[5] = Universal.GetTime;
							array[6] = "','";
							array[7] = obj.UserId;
							array[8] = "','";
							int num = 9;
							IPAddress ipaddress = externalIp;
							array[num] = ((ipaddress != null) ? ipaddress.ToString() : null);
							array[10] = "')";
							Universal.ExecuteNonQuery(string.Concat(array));
						}
						Universal.ExecuteNonQuery(string.Concat(new string[]
						{
							"update t_trading_all_users_master set LedgerBalance='",
							(LedgerBalance - finalbrokerage + final_pl).ToString(),
							"' where Id='",
							obj.UserId,
							"'"
						}));
						if (sublot > 0)
						{
							incoming_lot = sublot;
						}
					}
					else if (incoming_lot <= int.Parse(Lot))
					{
						int sublot2 = int.Parse(Lot) - incoming_lot;
						decimal closevalue2 = 0m;
						string CloseActionBy;
						string actiontype;
						string cmp2;
						if (obj.OrderCategory == "SELL")
						{
							CloseActionBy = "Sold By Trader";
							actiontype = "Bought By Trader";
							cmp2 = obj.OrderPrice;
							closevalue2 = decimal.Parse(cmp2) - decimal.Parse(OrderPrice);
						}
						else
						{
							CloseActionBy = "Bought By Trader";
							actiontype = "Sold By Trader";
							cmp2 = obj.OrderPrice;
							closevalue2 = decimal.Parse(OrderPrice) - decimal.Parse(cmp2);
						}
						decimal final_pl2 = Math.Round(closevalue2 * int.Parse(obj.selectedlotsize) * incoming_lot, 1);
						DataTable brokeragetbl2 = Universal.SelectWithDS("select LedgerBalance,creditLimit from t_trading_all_users_master where Id='" + obj.UserId + "'", "t_trading_all_users_master");
						decimal LedgerBalance2 = 0m;
						if (brokeragetbl2.Rows.Count > 0)
						{
							LedgerBalance2 = decimal.Parse(brokeragetbl2.Rows[0][0].ToString());
							decimal.Parse(brokeragetbl2.Rows[0][1].ToString());
						}
						if (sublot2 > 0)
						{
							decimal marginvalue = final_intraday_exp * sublot2;
							decimal holdmargn = final_indraday_holding * sublot2;
							if (Universal.ExecuteNonQuery(string.Concat(new string[]
							{
								"update t_user_order set Lot='",
								sublot2.ToString(),
								"',MarginUsed='",
								marginvalue.ToString(),
								"',HoldingMarginReq='",
								holdmargn.ToString(),
								"' where Id='",
								Id,
								"'"
							})) == 1)
							{
								string msg2 = string.Concat(new string[]
								{
									obj.UserName,
									"(",
									obj.UserId,
									") have ",
									obj.OrderType,
									" ",
									sublot2.ToString(),
									" lots of ",
									obj.ScriptName,
									" at ",
									obj.OrderPrice,
									". Trade has been modified by Autotrade settlement. "
								});
								string[] array2 = new string[11];
								array2[0] = "insert into t_action_master (Message,ActionDate,ActionTime,ActionFrom,IpAddress) values('";
								array2[1] = msg2;
								array2[2] = "','";
								array2[3] = Universal.GetDate;
								array2[4] = "','";
								array2[5] = Universal.GetTime;
								array2[6] = "','";
								array2[7] = obj.UserId;
								array2[8] = "','";
								int num2 = 9;
								IPAddress ipaddress2 = externalIp;
								array2[num2] = ((ipaddress2 != null) ? ipaddress2.ToString() : null);
								array2[10] = "')";
								Universal.ExecuteNonQuery(string.Concat(array2));
							}
						}
						decimal finalbrokerage2 = finalbrokerage * incoming_lot;
						Universal.ExecuteNonQuery(string.Concat(new string[]
						{
							"update t_trading_all_users_master set LedgerBalance='",
							(LedgerBalance2 - decimal.Parse(finalbrokerage2.ToString()) + final_pl2).ToString(),
							"' where Id='",
							obj.UserId,
							"'"
						}));
						decimal marginvalue2 = final_intraday_exp * incoming_lot;
						decimal holdmargn2 = final_indraday_holding * incoming_lot;
						if (Universal.ExecuteNonQuery(string.Concat(new string[]
						{
							"insert into t_user_order (selectedlotsize,OrderDate,OrderCategory,OrderTime,OrderNo,UserId,UserName,OrderType,ScriptName,TokenNo,ActionBy,OrderPrice,Lot,MarginUsed,HoldingMarginReq,OrderStatus,SymbolType,isstoplossorder,BroughtBy,P_L,Brokerage,ClosedAt,ClosedTime,OrderTypeClose,ActionByClose) values('",
							obj.selectedlotsize,
							"','",
							OrderDate,
							"','",
							OrderCategory,
							"','",
							OrderTime,
							"','",
							obj.OrderNo,
							"','",
							obj.UserId,
							"','",
							obj.UserName,
							"','",
							obj.OrderType,
							"','",
							obj.ScriptName,
							"','",
							obj.TokenNo,
							"','",
							actiontype,
							"','",
							OrderPrice.Trim().Replace(" ", ""),
							"','",
							incoming_lot.ToString(),
							"','",
							marginvalue2.ToString(),
							"','",
							holdmargn2.ToString(),
							"','Closed','",
							obj.SymbolType,
							"','",
							obj.isstoplossorder,
							"','",
							cmp2,
							"','",
							final_pl2.ToString(),
							"','",
							finalbrokerage2.ToString(),
							"','",
							Universal.GetDate,
							"','",
							Universal.GetTime,
							"','Market','",
							CloseActionBy,
							"')"
						})) == 1)
						{
							string msg3 = string.Concat(new string[]
							{
								obj.UserName,
								"(",
								obj.UserId,
								") have ",
								OrderCategory,
								" ",
								incoming_lot.ToString(),
								" lots of ",
								obj.ScriptName,
								" at ",
								OrderPrice.Trim().Replace(" ", ""),
								". Auto Closed due to settlement."
							});
							string[] array3 = new string[11];
							array3[0] = "insert into t_action_master (Message,ActionDate,ActionTime,ActionFrom,IpAddress) values('";
							array3[1] = msg3;
							array3[2] = "','";
							array3[3] = Universal.GetDate;
							array3[4] = "','";
							array3[5] = Universal.GetTime;
							array3[6] = "','";
							array3[7] = obj.UserId;
							array3[8] = "','";
							int num3 = 9;
							IPAddress ipaddress3 = externalIp;
							array3[num3] = ((ipaddress3 != null) ? ipaddress3.ToString() : null);
							array3[10] = "')";
							Universal.ExecuteNonQuery(string.Concat(array3));
						}
						if (sublot2 == 0)
						{
							Universal.ExecuteNonQuery("delete from t_user_order where Id='" + Id + "'");
							return;
						}
						return;
					}
				}
				return;
			}
			if (totaldatabase_lot <= int.Parse(obj.Lot))
			{
				DataTable pretable2 = Universal.SelectWithDS(string.Concat(new string[]
				{
					"select t_user_order.Id,selectedlotsize,OrderCategory,TokenNo,OrderPrice,t_user_order.SymbolType,Lot,t_universal_tradeble_tokens.buy,t_universal_tradeble_tokens.sell from t_user_order INNER JOIN t_universal_tradeble_tokens ON t_user_order.TokenNo=t_universal_tradeble_tokens.instrument_token where UserId='",
					obj.UserId,
					"' and TokenNo='",
					obj.TokenNo,
					"' and OrderCategory='",
					checkcond,
					"' and OrderStatus='Active' and t_user_order.Id !='",
					obj.Id,
					"' ORDER BY Lot"
				}), "t_user_order");
				int defflot = int.Parse(obj.Lot) - totaldatabase_lot;
				DataTable brokeragetbl3 = Universal.SelectWithDS("select LedgerBalance,creditLimit from t_trading_all_users_master where Id='" + obj.UserId + "'", "t_trading_all_users_master");
				decimal LedgerBalance3 = 0m;
				if (brokeragetbl3.Rows.Count > 0)
				{
					LedgerBalance3 = decimal.Parse(brokeragetbl3.Rows[0][0].ToString());
					decimal.Parse(brokeragetbl3.Rows[0][1].ToString());
				}
				for (int j = 0; j < pretable2.Rows.Count; j++)
				{
					string Id2 = pretable2.Rows[j]["Id"].ToString();
					string Lot2 = pretable2.Rows[j]["Lot"].ToString();
					pretable2.Rows[j]["buy"].ToString();
					pretable2.Rows[j]["sell"].ToString();
					string selectedlotsize = pretable2.Rows[j]["selectedlotsize"].ToString();
					string OrderPrice2 = pretable2.Rows[j]["OrderPrice"].ToString();
					if (obj.SymbolType == "MCX")
					{
						if (Mcx_Brokerage_Type == "per_lot")
						{
							finalbrokerage = decimal.Parse(MCXsymb_brokerage);
						}
						else
						{
							finalbrokerage = (decimal.Parse(OrderPrice2) + decimal.Parse(obj.OrderPrice)) * decimal.Parse(obj.selectedlotsize) / 10000000m * decimal.Parse(MCX_brokerage_per_crore);
						}
						if (Mcx_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(MCX_Exposure_Lot_wise_sym_Intraday);
							final_indraday_holding = decimal.Parse(MCX_Exposure_Lot_wise_sym_Holding);
						}
						else
						{
							final_intraday_exp = decimal.Parse(OrderPrice2) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_MCX);
							final_indraday_holding = decimal.Parse(OrderPrice2) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_MCX);
						}
					}
					else if (obj.SymbolType == "NSE")
					{
						if (NSE_Brokerage_Type == "per_lot")
						{
							finalbrokerage = decimal.Parse(Equity_brokerage_per_crore);
						}
						else
						{
							finalbrokerage = (decimal.Parse(OrderPrice2) + decimal.Parse(obj.OrderPrice)) * decimal.Parse(obj.selectedlotsize) / 10000000m * decimal.Parse(Equity_brokerage_per_crore);
						}
						if (NSE_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(Intraday_Exposure_Margin_Equity);
							final_indraday_holding = decimal.Parse(Holding_Exposure_Margin_Equity);
						}
						else
						{
							final_intraday_exp = decimal.Parse(OrderPrice2) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_Equity);
							final_indraday_holding = decimal.Parse(OrderPrice2) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_Equity);
						}
					}
					else if (obj.SymbolType == "OPT")
					{
						if (CDS_Brokerage_Type == "per_lot")
						{
							finalbrokerage = decimal.Parse(CDS_brokerage_per_crore);
						}
						else
						{
							finalbrokerage = (decimal.Parse(OrderPrice2) + decimal.Parse(obj.OrderPrice)) * decimal.Parse(obj.selectedlotsize) / 10000000m * decimal.Parse(CDS_brokerage_per_crore);
						}
						if (CDS_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(Intraday_Exposure_Margin_CDS);
							final_indraday_holding = decimal.Parse(Holding_Exposure_Margin_CDS);
						}
						else
						{
							final_intraday_exp = decimal.Parse(OrderPrice2) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_CDS);
							final_indraday_holding = decimal.Parse(OrderPrice2) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_CDS);
						}
					}
					decimal closevalue3 = 0m;
					string ActionCloseBy2;
					string cmprice2;
					if (obj.OrderCategory == "SELL")
					{
						ActionCloseBy2 = "Sold By Trader";
						cmprice2 = obj.OrderPrice;
						closevalue3 = decimal.Parse(cmprice2) - decimal.Parse(OrderPrice2);
					}
					else
					{
						ActionCloseBy2 = "Bought By Trader";
						cmprice2 = obj.OrderPrice;
						closevalue3 = decimal.Parse(OrderPrice2) - decimal.Parse(cmprice2);
					}
					decimal final_pl3 = Math.Round(closevalue3 * int.Parse(selectedlotsize) * int.Parse(Lot2), 1);
					decimal finalbrokerage3 = finalbrokerage * int.Parse(Lot2);
					if (Universal.ExecuteNonQuery(string.Concat(new string[]
					{
						"update t_user_order set P_L='",
						final_pl3.ToString(),
						"',OrderTypeClose='Market',ActionByClose='",
						ActionCloseBy2,
						"',OrderStatus='Closed',Brokerage='",
						finalbrokerage3.ToString(),
						"',BroughtBy='",
						cmprice2,
						"',ClosedAt='",
						Universal.GetDate,
						"',ClosedTime='",
						Universal.GetTime,
						"' where Id='",
						Id2,
						"'"
					})) == 1)
					{
						string msg4 = string.Concat(new string[]
						{
							obj.UserName,
							"(",
							obj.UserId,
							") have ",
							obj.OrderCategory,
							" ",
							obj.Lot,
							" lots of ",
							obj.ScriptName,
							" at ",
							obj.OrderPrice,
							". Auto Closed due to settlement."
						});
						string[] array4 = new string[11];
						array4[0] = "insert into t_action_master (Message,ActionDate,ActionTime,ActionFrom,IpAddress) values('";
						array4[1] = msg4;
						array4[2] = "','";
						array4[3] = Universal.GetDate;
						array4[4] = "','";
						array4[5] = Universal.GetTime;
						array4[6] = "','";
						array4[7] = obj.UserId;
						array4[8] = "','";
						int num4 = 9;
						IPAddress ipaddress4 = externalIp;
						array4[num4] = ((ipaddress4 != null) ? ipaddress4.ToString() : null);
						array4[10] = "')";
						Universal.ExecuteNonQuery(string.Concat(array4));
					}
					Universal.ExecuteNonQuery(string.Concat(new string[]
					{
						"update t_trading_all_users_master set LedgerBalance='",
						(LedgerBalance3 - decimal.Parse(finalbrokerage3.ToString()) + final_pl3).ToString(),
						"' where Id='",
						obj.UserId,
						"'"
					}));
				}
				if (defflot > 0)
				{
					if (obj.SymbolType == "MCX")
					{
						if (Mcx_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(MCX_Exposure_Lot_wise_sym_Intraday);
							final_indraday_holding = decimal.Parse(MCX_Exposure_Lot_wise_sym_Holding);
						}
						else
						{
							final_intraday_exp = decimal.Parse(obj.OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_MCX);
							final_indraday_holding = decimal.Parse(obj.OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_MCX);
						}
					}
					else if (obj.SymbolType == "NSE")
					{
						if (NSE_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(Intraday_Exposure_Margin_Equity);
							final_indraday_holding = decimal.Parse(Holding_Exposure_Margin_Equity);
						}
						else
						{
							final_intraday_exp = decimal.Parse(obj.OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_Equity);
							final_indraday_holding = decimal.Parse(obj.OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_Equity);
						}
					}
					else if (obj.SymbolType == "OPT")
					{
						if (CDS_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(Intraday_Exposure_Margin_CDS);
							final_indraday_holding = decimal.Parse(Holding_Exposure_Margin_CDS);
						}
						else
						{
							final_intraday_exp = decimal.Parse(obj.OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_CDS);
							final_indraday_holding = decimal.Parse(obj.OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_CDS);
						}
					}
					decimal marginvalue3 = final_intraday_exp * defflot;
					decimal holdmargn3 = final_indraday_holding * defflot;
					Universal.ExecuteNonQuery(string.Concat(new string[]
					{
						"update t_user_order set selectedlotsize='",
						obj.selectedlotsize,
						"',OrderDate='",
						Universal.GetDate,
						"', OrderCategory='",
						obj.OrderCategory,
						"',OrderTime='",
						obj.OrderTime,
						"',OrderNo='",
						obj.OrderNo,
						"',UserId='",
						obj.UserId,
						"', UserName='",
						obj.UserName,
						"',OrderType='",
						obj.OrderType,
						"',ScriptName='",
						obj.ScriptName,
						"',TokenNo='",
						obj.TokenNo,
						"',ActionBy='",
						obj.ActionType,
						"',OrderPrice='",
						obj.OrderPrice,
						"',Lot='",
						defflot.ToString(),
						"',MarginUsed='",
						marginvalue3.ToString(),
						"',HoldingMarginReq='",
						holdmargn3.ToString(),
						"',OrderStatus='Active',SymbolType='",
						obj.SymbolType,
						"',isstoplossorder='",
						obj.isstoplossorder,
						"' where Id='",
						obj.Id,
						"'"
					}));
				}
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00010A24 File Offset: 0x0000EC24
		public DateTime FirstDayOfWeek(DateTime date)
		{
			int offset = (int)(DayOfWeek.Sunday - date.DayOfWeek);
			return date.AddDays((double)offset);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00010A44 File Offset: 0x0000EC44
		public DateTime LastDayOfWeek(DateTime date)
		{
			return this.FirstDayOfWeek(date).AddDays(6.0);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00010A6C File Offset: 0x0000EC6C
		[HttpPost]
		public string getuserorders(string orderstatus, string UserId)
		{
			List<t_order_master> orderdata = new List<t_order_master>();
			if (orderstatus == "Pending")
			{
				DataTable dt = Universal.SelectWithDS(string.Concat(new string[]
				{
					"select Id,DATE_FORMAT(OrderDate,'%d/%m/%Y') as OrderDate,ClosedTime,OrderTime,OrderNo,UserId,OrderPrice,Lot,MarginUsed,Brokerage,ClosedAt,HoldingMarginReq,OrderStatus,P_L,SymbolType,BroughtBy,selectedlotsize,isstoplossorder,UserName,OrderCategory,OrderType,ScriptName,OrderTypeClose,TokenNo,ActionBy from t_user_order where (OrderStatus='Pending' or OrderStatus='Active') and OrderType='Limit' and UserId='",
					UserId,
					"' and OrderDate='",
					Universal.GetDate,
					"'"
				}), "t_user_order");
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					t_order_master ord = new t_order_master();
					ord.Id = dt.Rows[i]["Id"].ToString();
					ord.OrderDate = dt.Rows[i]["OrderDate"].ToString().Replace("12:00:00 AM", "").Replace("00:00:00", "");
					ord.OrderDate = DateTime.Parse(ord.OrderDate).ToString("dd/MM/yyyy");
					ord.OrderTime = dt.Rows[i]["OrderTime"].ToString();
					ord.OrderNo = dt.Rows[i]["OrderNo"].ToString();
					ord.UserId = dt.Rows[i]["UserId"].ToString();
					ord.selectedlotsize = dt.Rows[i]["selectedlotsize"].ToString();
					ord.isstoplossorder = dt.Rows[i]["isstoplossorder"].ToString();
					ord.UserName = dt.Rows[i]["UserName"].ToString();
					ord.OrderCategory = dt.Rows[i]["OrderCategory"].ToString();
					ord.OrderType = dt.Rows[i]["OrderType"].ToString();
					ord.ScriptName = dt.Rows[i]["ScriptName"].ToString();
					ord.TokenNo = dt.Rows[i]["TokenNo"].ToString();
					ord.ActionType = dt.Rows[i]["ActionBy"].ToString();
					ord.OrderPrice = dt.Rows[i]["OrderPrice"].ToString();
					ord.Lot = dt.Rows[i]["Lot"].ToString();
					ord.MarginUsed = dt.Rows[i]["MarginUsed"].ToString();
					ord.HoldingMarginReq = dt.Rows[i]["HoldingMarginReq"].ToString();
					ord.OrderStatus = dt.Rows[i]["OrderStatus"].ToString();
					ord.SymbolType = dt.Rows[i]["SymbolType"].ToString();
					ord.BroughtBy = dt.Rows[i]["BroughtBy"].ToString();
					ord.P_L = dt.Rows[i]["P_L"].ToString();
					ord.Brokerage = dt.Rows[i]["Brokerage"].ToString();
					ord.OrderTypeClose = dt.Rows[i]["OrderTypeClose"].ToString();
					orderdata.Add(ord);
				}
			}
			else if (orderstatus == "Active")
			{
				DateTime startdate = this.FirstDayOfWeek(DateTime.Now);
				DateTime enddate = this.LastDayOfWeek(DateTime.Now);
				string sdate = string.Concat(new string[]
				{
					startdate.Year.ToString(),
					"-",
					startdate.Month.ToString(),
					"-",
					startdate.Day.ToString()
				});
				string edate = string.Concat(new string[]
				{
					enddate.Year.ToString(),
					"-",
					enddate.Month.ToString(),
					"-",
					enddate.Day.ToString()
				});
				DataTable dt2 = Universal.SelectWithDS(string.Concat(new string[]
				{
					"select Id,DATE_FORMAT(OrderDate,'%d/%m/%Y') as OrderDate,OrderTime,ClosedTime,OrderNo,UserId,OrderPrice,Lot,MarginUsed,Brokerage,ClosedAt,HoldingMarginReq,OrderStatus,P_L,SymbolType,BroughtBy,selectedlotsize,isstoplossorder,UserName,OrderCategory,OrderType,ScriptName,TokenNo,ActionBy,OrderTypeClose from t_user_order where OrderStatus='",
					orderstatus,
					"' and UserId='",
					UserId,
					"' and OrderDate between '",
					sdate,
					"' and '",
					edate,
					"'   ORDER BY OrderDate DESC,OrderTime DESC"
				}), "t_user_order");
				for (int j = 0; j < dt2.Rows.Count; j++)
				{
					t_order_master ord2 = new t_order_master();
					ord2.Id = dt2.Rows[j]["Id"].ToString();
					ord2.OrderDate = dt2.Rows[j]["OrderDate"].ToString().Replace("12:00:00 AM", "").Replace("00:00:00", "");
					ord2.OrderDate = DateTime.Parse(ord2.OrderDate).ToString("dd/MM/yyyy");
					ord2.OrderTime = dt2.Rows[j]["OrderTime"].ToString();
					ord2.OrderNo = dt2.Rows[j]["OrderNo"].ToString();
					ord2.UserId = dt2.Rows[j]["UserId"].ToString();
					ord2.ClosedTime = dt2.Rows[j]["ClosedTime"].ToString();
					ord2.selectedlotsize = dt2.Rows[j]["selectedlotsize"].ToString();
					ord2.UserName = dt2.Rows[j]["UserName"].ToString();
					ord2.OrderCategory = dt2.Rows[j]["OrderCategory"].ToString();
					ord2.OrderType = dt2.Rows[j]["OrderType"].ToString();
					ord2.ScriptName = dt2.Rows[j]["ScriptName"].ToString();
					ord2.TokenNo = dt2.Rows[j]["TokenNo"].ToString();
					ord2.ActionType = dt2.Rows[j]["ActionBy"].ToString();
					ord2.OrderPrice = dt2.Rows[j]["OrderPrice"].ToString();
					ord2.Lot = dt2.Rows[j]["Lot"].ToString();
					ord2.MarginUsed = dt2.Rows[j]["MarginUsed"].ToString();
					ord2.OrderTypeClose = dt2.Rows[j]["OrderTypeClose"].ToString();
					ord2.HoldingMarginReq = dt2.Rows[j]["HoldingMarginReq"].ToString();
					ord2.OrderStatus = dt2.Rows[j]["OrderStatus"].ToString();
					ord2.SymbolType = dt2.Rows[j]["SymbolType"].ToString();
					ord2.BroughtBy = dt2.Rows[j]["BroughtBy"].ToString();
					ord2.P_L = dt2.Rows[j]["P_L"].ToString();
					ord2.Brokerage = dt2.Rows[j]["Brokerage"].ToString();
					ord2.ClosedAt = dt2.Rows[j]["ClosedAt"].ToString().Replace("-", "/").Replace("12:00:00 AM", "").Replace("00:00:00", "");
					orderdata.Add(ord2);
				}
			}
			else if (orderstatus == "Closed")
			{
				DateTime startdate2 = this.FirstDayOfWeek(DateTime.Now);
				DateTime enddate2 = this.LastDayOfWeek(DateTime.Now);
				string sdate2 = string.Concat(new string[]
				{
					startdate2.Year.ToString(),
					"-",
					startdate2.Month.ToString(),
					"-",
					startdate2.Day.ToString()
				});
				string edate2 = string.Concat(new string[]
				{
					enddate2.Year.ToString(),
					"-",
					enddate2.Month.ToString(),
					"-",
					enddate2.Day.ToString()
				});
				DataTable dt3 = Universal.SelectWithDS(string.Concat(new string[]
				{
					"select Id,OrderDate,OrderTime,ClosedTime,OrderNo,UserId,OrderPrice,Lot,MarginUsed,Brokerage,ClosedAt,HoldingMarginReq,OrderStatus,P_L,SymbolType,BroughtBy,selectedlotsize,isstoplossorder,UserName,OrderCategory,OrderType,ScriptName,TokenNo,ActionBy,OrderTypeClose from t_user_order where OrderStatus='",
					orderstatus,
					"' and UserId='",
					UserId,
					"' and ClosedAt between '",
					sdate2,
					"' and '",
					edate2,
					"' ORDER BY ClosedAt DESC,ClosedTime DESC"
				}), "t_user_order");
				for (int k = 0; k < dt3.Rows.Count; k++)
				{
					t_order_master ord3 = new t_order_master();
					ord3.Id = dt3.Rows[k]["Id"].ToString();
					ord3.OrderDate = dt3.Rows[k]["OrderDate"].ToString().Replace("12:00:00 AM", "").Replace("00:00:00", "");
					ord3.OrderDate = DateTime.Parse(ord3.OrderDate).ToString("dd/MM/yyyy");
					ord3.OrderTime = dt3.Rows[k]["OrderTime"].ToString();
					ord3.OrderNo = dt3.Rows[k]["OrderNo"].ToString();
					ord3.UserId = dt3.Rows[k]["UserId"].ToString();
					ord3.ClosedTime = dt3.Rows[k]["ClosedTime"].ToString();
					ord3.selectedlotsize = dt3.Rows[k]["selectedlotsize"].ToString();
					ord3.UserName = dt3.Rows[k]["UserName"].ToString();
					ord3.OrderCategory = dt3.Rows[k]["OrderCategory"].ToString();
					ord3.OrderType = dt3.Rows[k]["OrderType"].ToString();
					ord3.ScriptName = dt3.Rows[k]["ScriptName"].ToString();
					ord3.TokenNo = dt3.Rows[k]["TokenNo"].ToString();
					ord3.ActionType = dt3.Rows[k]["ActionBy"].ToString();
					ord3.OrderPrice = dt3.Rows[k]["OrderPrice"].ToString();
					ord3.Lot = dt3.Rows[k]["Lot"].ToString();
					ord3.OrderTypeClose = dt3.Rows[k]["OrderTypeClose"].ToString();
					ord3.MarginUsed = dt3.Rows[k]["MarginUsed"].ToString();
					ord3.HoldingMarginReq = dt3.Rows[k]["HoldingMarginReq"].ToString();
					ord3.OrderStatus = dt3.Rows[k]["OrderStatus"].ToString();
					ord3.SymbolType = dt3.Rows[k]["SymbolType"].ToString();
					ord3.BroughtBy = dt3.Rows[k]["BroughtBy"].ToString();
					ord3.P_L = dt3.Rows[k]["P_L"].ToString();
					ord3.Brokerage = dt3.Rows[k]["Brokerage"].ToString();
					ord3.ClosedAt = dt3.Rows[k]["ClosedAt"].ToString().Replace("-", "/").Replace("12:00:00 AM", "").Replace("00:00:00", "");
					ord3.ClosedAt = DateTime.Parse(ord3.ClosedAt).ToString("dd/MM/yyyy");
					orderdata.Add(ord3);
				}
			}
			return new JavaScriptSerializer().Serialize(orderdata);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000118A4 File Offset: 0x0000FAA4
		public string getscript(string exchangename)
		{
			List<t_universal_tradeble_symbol> list = new List<t_universal_tradeble_symbol>();
			DataTable dt = Universal.SelectWithDS("select symbolname,instrument_token from t_universal_tradeble_tokens where Exchange='" + exchangename + "'", "t_universal_tradeble_tokens");
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				list.Add(new t_universal_tradeble_symbol
				{
					instrument_token = dt.Rows[i]["instrument_token"].ToString(),
					symbolname = dt.Rows[i]["symbolname"].ToString()
				});
			}
			return new JavaScriptSerializer().Serialize(list);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00011944 File Offset: 0x0000FB44
		public string getscripttoken(string scriptname)
		{
			new List<t_universal_tradeble_symbol>();
			DataTable dt = Universal.SelectWithDS("select instrument_token from t_universal_tradeble_tokens where symbolname='" + scriptname + "'", "t_universal_tradeble_tokens");
			if (dt.Rows.Count > 0)
			{
				return dt.Rows[0]["instrument_token"].ToString();
			}
			return "";
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000119A4 File Offset: 0x0000FBA4
		public string getorders_todayonly(string orderstatus, string uid)
		{
			List<t_order_master> orderdata = new List<t_order_master>();
			if (orderstatus == "Pending")
			{
				DataTable dt = Universal.SelectWithDS(string.Concat(new string[]
				{
					"select * from t_user_order where ((OrderStatus='Pending' OR OrderStatus='Cancel') and UserId='",
					uid,
					"' and OrderDate='",
					Universal.GetDate,
					"') or (OrderType='Limit' and OrderStatus='Active' and UserId='",
					uid,
					"' and OrderDate='",
					Universal.GetDate,
					"') ORDER BY Id DESC"
				}), "t_user_order");
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					t_order_master ord = new t_order_master();
					ord.Id = dt.Rows[i]["Id"].ToString();
					ord.OrderDate = dt.Rows[i]["OrderDate"].ToString().Replace("12:00:00 AM", "").Replace("00:00:00", "");
					ord.OrderDate = DateTime.Parse(ord.OrderDate).ToString("dd") + " " + DateTime.Parse(ord.OrderDate).ToString("MMM");
					ord.OrderTime = dt.Rows[i]["OrderTime"].ToString();
					ord.OrderTime = DateTime.Parse(ord.OrderTime).ToString("HH:mm");
					ord.OrderNo = dt.Rows[i]["OrderNo"].ToString();
					ord.UserId = dt.Rows[i]["UserId"].ToString();
					ord.selectedlotsize = dt.Rows[i]["selectedlotsize"].ToString();
					ord.isstoplossorder = dt.Rows[i]["isstoplossorder"].ToString();
					ord.UserName = dt.Rows[i]["UserName"].ToString();
					ord.OrderCategory = dt.Rows[i]["OrderCategory"].ToString();
					ord.OrderType = dt.Rows[i]["OrderType"].ToString();
					ord.ScriptName = dt.Rows[i]["ScriptName"].ToString();
					ord.TokenNo = dt.Rows[i]["TokenNo"].ToString();
					ord.ActionType = dt.Rows[i]["ActionBy"].ToString();
					ord.OrderPrice = dt.Rows[i]["OrderPrice"].ToString();
					ord.Lot = dt.Rows[i]["Lot"].ToString();
					ord.MarginUsed = dt.Rows[i]["MarginUsed"].ToString();
					ord.HoldingMarginReq = dt.Rows[i]["HoldingMarginReq"].ToString();
					ord.OrderStatus = dt.Rows[i]["OrderStatus"].ToString();
					ord.SymbolType = dt.Rows[i]["SymbolType"].ToString();
					orderdata.Add(ord);
				}
			}
			else if (orderstatus == "Pendingonly")
			{
				DataTable dt2 = Universal.SelectWithDS(string.Concat(new string[]
				{
					"select * from t_user_order where OrderStatus='Pending' and UserId='",
					uid,
					"' and OrderDate='",
					Universal.GetDate,
					"' ORDER BY Id DESC"
				}), "t_user_order");
				for (int j = 0; j < dt2.Rows.Count; j++)
				{
					t_order_master ord2 = new t_order_master();
					ord2.Id = dt2.Rows[j]["Id"].ToString();
					ord2.OrderDate = dt2.Rows[j]["OrderDate"].ToString().Replace("12:00:00 AM", "").Replace("00:00:00", "");
					ord2.OrderDate = DateTime.Parse(ord2.OrderDate).ToString("dd") + " " + DateTime.Parse(ord2.OrderDate).ToString("MMM");
					ord2.OrderTime = dt2.Rows[j]["OrderTime"].ToString();
					ord2.OrderTime = DateTime.Parse(ord2.OrderTime).ToString("HH:mm");
					ord2.OrderNo = dt2.Rows[j]["OrderNo"].ToString();
					ord2.UserId = dt2.Rows[j]["UserId"].ToString();
					ord2.selectedlotsize = dt2.Rows[j]["selectedlotsize"].ToString();
					ord2.isstoplossorder = dt2.Rows[j]["isstoplossorder"].ToString();
					ord2.UserName = dt2.Rows[j]["UserName"].ToString();
					ord2.OrderCategory = dt2.Rows[j]["OrderCategory"].ToString();
					ord2.OrderType = dt2.Rows[j]["OrderType"].ToString();
					ord2.ScriptName = dt2.Rows[j]["ScriptName"].ToString();
					ord2.TokenNo = dt2.Rows[j]["TokenNo"].ToString();
					ord2.ActionType = dt2.Rows[j]["ActionBy"].ToString();
					ord2.OrderPrice = dt2.Rows[j]["OrderPrice"].ToString();
					ord2.Lot = dt2.Rows[j]["Lot"].ToString();
					ord2.MarginUsed = dt2.Rows[j]["MarginUsed"].ToString();
					ord2.HoldingMarginReq = dt2.Rows[j]["HoldingMarginReq"].ToString();
					ord2.OrderStatus = dt2.Rows[j]["OrderStatus"].ToString();
					ord2.SymbolType = dt2.Rows[j]["SymbolType"].ToString();
					orderdata.Add(ord2);
				}
			}
			else if (orderstatus == "Active")
			{
				DateTime startdate = DateTime.Now;
				string cdate = string.Concat(new string[]
				{
					startdate.Year.ToString(),
					"-",
					startdate.Month.ToString(),
					"-",
					startdate.Day.ToString()
				});
				DataTable dt3 = Universal.SelectWithDS(string.Concat(new string[]
				{
					"select * from t_user_order where OrderStatus='",
					orderstatus,
					"' and UserId='",
					uid,
					"' and OrderDate between '",
					cdate,
					"' and '",
					cdate,
					"'   ORDER BY OrderDate DESC,OrderTime DESC"
				}), "t_user_order");
				for (int k = 0; k < dt3.Rows.Count; k++)
				{
					t_order_master ord3 = new t_order_master();
					ord3.Id = dt3.Rows[k]["Id"].ToString();
					ord3.OrderDate = dt3.Rows[k]["OrderDate"].ToString().Replace("12:00:00 AM", "").Replace("00:00:00", "");
					ord3.OrderDate = DateTime.Parse(ord3.OrderDate).ToString("yyyy/MM/dd");
					ord3.OrderTime = dt3.Rows[k]["OrderTime"].ToString();
					ord3.OrderTimeFull = DateTime.Parse(ord3.OrderTime).ToString("HH:mm:ss");
					ord3.OrderTime = DateTime.Parse(ord3.OrderTime).ToString("HH:mm");
					ord3.OrderNo = dt3.Rows[k]["OrderNo"].ToString();
					ord3.UserId = dt3.Rows[k]["UserId"].ToString();
					ord3.selectedlotsize = dt3.Rows[k]["selectedlotsize"].ToString();
					ord3.UserName = dt3.Rows[k]["UserName"].ToString();
					ord3.OrderCategory = dt3.Rows[k]["OrderCategory"].ToString();
					ord3.OrderType = dt3.Rows[k]["OrderType"].ToString();
					ord3.ScriptName = dt3.Rows[k]["ScriptName"].ToString();
					ord3.TokenNo = dt3.Rows[k]["TokenNo"].ToString();
					ord3.ActionType = dt3.Rows[k]["ActionBy"].ToString();
					ord3.OrderPrice = dt3.Rows[k]["OrderPrice"].ToString();
					ord3.Lot = dt3.Rows[k]["Lot"].ToString();
					ord3.MarginUsed = dt3.Rows[k]["MarginUsed"].ToString();
					ord3.HoldingMarginReq = dt3.Rows[k]["HoldingMarginReq"].ToString();
					ord3.OrderStatus = dt3.Rows[k]["OrderStatus"].ToString();
					ord3.SymbolType = dt3.Rows[k]["SymbolType"].ToString();
					orderdata.Add(ord3);
				}
			}
			else
			{
				DateTime startdate2 = DateTime.Now;
				string cdate2 = string.Concat(new string[]
				{
					startdate2.Year.ToString(),
					"-",
					startdate2.Month.ToString(),
					"-",
					startdate2.Day.ToString()
				});
				DataTable dt4 = Universal.SelectWithDS(string.Concat(new string[]
				{
					"select * from t_user_order where OrderStatus='",
					orderstatus,
					"' and UserId='",
					uid,
					"' and ClosedAt between '",
					cdate2,
					"' and '",
					cdate2,
					"' ORDER BY ClosedAt DESC,ClosedTime DESC"
				}), "t_user_order");
				for (int l = 0; l < dt4.Rows.Count; l++)
				{
					t_order_master ord4 = new t_order_master();
					ord4.Id = dt4.Rows[l]["Id"].ToString();
					ord4.OrderDate = dt4.Rows[l]["OrderDate"].ToString().Replace("12:00:00 AM", "").Replace("00:00:00", "");
					ord4.OrderDate = DateTime.Parse(ord4.OrderDate).ToString("dd") + " " + DateTime.Parse(ord4.OrderDate).ToString("MMM");
					ord4.OrderTime = dt4.Rows[l]["OrderTime"].ToString();
					ord4.OrderTime = DateTime.Parse(ord4.OrderTime).ToString("HH:mm");
					ord4.OrderNo = dt4.Rows[l]["OrderNo"].ToString();
					ord4.UserId = dt4.Rows[l]["UserId"].ToString();
					ord4.ClosedTime = dt4.Rows[l]["ClosedTime"].ToString();
					ord4.ClosedTime = DateTime.Parse(ord4.ClosedTime).ToString("HH:mm");
					ord4.selectedlotsize = dt4.Rows[l]["selectedlotsize"].ToString();
					ord4.UserName = dt4.Rows[l]["UserName"].ToString();
					ord4.OrderCategory = dt4.Rows[l]["OrderCategory"].ToString();
					ord4.OrderType = dt4.Rows[l]["OrderType"].ToString();
					ord4.ScriptName = dt4.Rows[l]["ScriptName"].ToString();
					ord4.TokenNo = dt4.Rows[l]["TokenNo"].ToString();
					ord4.ActionType = dt4.Rows[l]["ActionBy"].ToString();
					ord4.ActionByClose = dt4.Rows[l]["ActionByClose"].ToString();
					ord4.OrderTypeClose = dt4.Rows[l]["OrderTypeClose"].ToString();
					ord4.OrderPrice = dt4.Rows[l]["OrderPrice"].ToString();
					ord4.Lot = dt4.Rows[l]["Lot"].ToString();
					ord4.MarginUsed = dt4.Rows[l]["MarginUsed"].ToString();
					ord4.HoldingMarginReq = dt4.Rows[l]["HoldingMarginReq"].ToString();
					ord4.OrderStatus = dt4.Rows[l]["OrderStatus"].ToString();
					ord4.SymbolType = dt4.Rows[l]["SymbolType"].ToString();
					ord4.BroughtBy = dt4.Rows[l]["BroughtBy"].ToString();
					ord4.P_L = dt4.Rows[l]["P_L"].ToString();
					ord4.Brokerage = dt4.Rows[l]["Brokerage"].ToString();
					ord4.ClosedAt = dt4.Rows[l]["ClosedAt"].ToString().Replace("12:00:00 AM", "").Replace("00:00:00", "");
					ord4.ClosedAt = DateTime.Parse(ord4.ClosedAt).ToString("dd") + " " + DateTime.Parse(ord4.ClosedAt).ToString("MMM");
					orderdata.Add(ord4);
				}
			}
			return new JavaScriptSerializer().Serialize(orderdata);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00012A64 File Offset: 0x00010C64
		public string getorders(string orderstatus, string uid)
		{
			List<t_order_master> orderdata = new List<t_order_master>();
			if (orderstatus == "Pending")
			{
				DataTable dt = Universal.SelectWithDS(string.Concat(new string[]
				{
					"select * from t_user_order where ((OrderStatus='Pending' OR OrderStatus='Cancel') and UserId='",
					uid,
					"' and OrderDate='",
					Universal.GetDate,
					"') or (OrderType='Limit' and OrderStatus='Active' and UserId='",
					uid,
					"' and OrderDate='",
					Universal.GetDate,
					"') ORDER BY Id DESC"
				}), "t_user_order");
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					t_order_master ord = new t_order_master();
					ord.Id = dt.Rows[i]["Id"].ToString();
					ord.OrderDate = dt.Rows[i]["OrderDate"].ToString().Replace("12:00:00 AM", "").Replace("00:00:00", "");
					ord.OrderDate = DateTime.Parse(ord.OrderDate).ToString("dd") + " " + DateTime.Parse(ord.OrderDate).ToString("MMM");
					ord.OrderTime = dt.Rows[i]["OrderTime"].ToString();
					ord.OrderTime = DateTime.Parse(ord.OrderTime).ToString("HH:mm");
					ord.OrderNo = dt.Rows[i]["OrderNo"].ToString();
					ord.UserId = dt.Rows[i]["UserId"].ToString();
					ord.selectedlotsize = dt.Rows[i]["selectedlotsize"].ToString();
					ord.isstoplossorder = dt.Rows[i]["isstoplossorder"].ToString();
					ord.UserName = dt.Rows[i]["UserName"].ToString();
					ord.OrderCategory = dt.Rows[i]["OrderCategory"].ToString();
					ord.OrderType = dt.Rows[i]["OrderType"].ToString();
					ord.ScriptName = dt.Rows[i]["ScriptName"].ToString();
					ord.TokenNo = dt.Rows[i]["TokenNo"].ToString();
					ord.ActionType = dt.Rows[i]["ActionBy"].ToString();
					ord.OrderPrice = dt.Rows[i]["OrderPrice"].ToString();
					ord.Lot = dt.Rows[i]["Lot"].ToString();
					ord.MarginUsed = dt.Rows[i]["MarginUsed"].ToString();
					ord.HoldingMarginReq = dt.Rows[i]["HoldingMarginReq"].ToString();
					ord.OrderStatus = dt.Rows[i]["OrderStatus"].ToString();
					ord.SymbolType = dt.Rows[i]["SymbolType"].ToString();
					orderdata.Add(ord);
				}
			}
			else if (orderstatus == "Pendingonly")
			{
				DataTable dt2 = Universal.SelectWithDS(string.Concat(new string[]
				{
					"select * from t_user_order where OrderStatus='Pending' and UserId='",
					uid,
					"' and OrderDate='",
					Universal.GetDate,
					"' ORDER BY Id DESC"
				}), "t_user_order");
				for (int j = 0; j < dt2.Rows.Count; j++)
				{
					t_order_master ord2 = new t_order_master();
					ord2.Id = dt2.Rows[j]["Id"].ToString();
					ord2.OrderDate = dt2.Rows[j]["OrderDate"].ToString().Replace("12:00:00 AM", "").Replace("00:00:00", "");
					ord2.OrderDate = DateTime.Parse(ord2.OrderDate).ToString("dd") + " " + DateTime.Parse(ord2.OrderDate).ToString("MMM");
					ord2.OrderTime = dt2.Rows[j]["OrderTime"].ToString();
					ord2.OrderTime = DateTime.Parse(ord2.OrderTime).ToString("HH:mm");
					ord2.OrderNo = dt2.Rows[j]["OrderNo"].ToString();
					ord2.UserId = dt2.Rows[j]["UserId"].ToString();
					ord2.selectedlotsize = dt2.Rows[j]["selectedlotsize"].ToString();
					ord2.isstoplossorder = dt2.Rows[j]["isstoplossorder"].ToString();
					ord2.UserName = dt2.Rows[j]["UserName"].ToString();
					ord2.OrderCategory = dt2.Rows[j]["OrderCategory"].ToString();
					ord2.OrderType = dt2.Rows[j]["OrderType"].ToString();
					ord2.ScriptName = dt2.Rows[j]["ScriptName"].ToString();
					ord2.TokenNo = dt2.Rows[j]["TokenNo"].ToString();
					ord2.ActionType = dt2.Rows[j]["ActionBy"].ToString();
					ord2.OrderPrice = dt2.Rows[j]["OrderPrice"].ToString();
					ord2.Lot = dt2.Rows[j]["Lot"].ToString();
					ord2.MarginUsed = dt2.Rows[j]["MarginUsed"].ToString();
					ord2.HoldingMarginReq = dt2.Rows[j]["HoldingMarginReq"].ToString();
					ord2.OrderStatus = dt2.Rows[j]["OrderStatus"].ToString();
					ord2.SymbolType = dt2.Rows[j]["SymbolType"].ToString();
					orderdata.Add(ord2);
				}
			}
			else if (orderstatus == "Active")
			{
				DateTime startdate = this.FirstDayOfWeek(DateTime.Now);
				DateTime enddate = this.LastDayOfWeek(DateTime.Now);
				string sdate = string.Concat(new string[]
				{
					startdate.Year.ToString(),
					"-",
					startdate.Month.ToString(),
					"-",
					startdate.Day.ToString()
				});
				string edate = string.Concat(new string[]
				{
					enddate.Year.ToString(),
					"-",
					enddate.Month.ToString(),
					"-",
					enddate.Day.ToString()
				});
				DataTable dt3 = Universal.SelectWithDS(string.Concat(new string[]
				{
					"select * from t_user_order where OrderStatus='",
					orderstatus,
					"' and UserId='",
					uid,
					"' and OrderDate between '",
					sdate,
					"' and '",
					edate,
					"'   ORDER BY OrderDate DESC,OrderTime DESC"
				}), "t_user_order");
				for (int k = 0; k < dt3.Rows.Count; k++)
				{
					t_order_master ord3 = new t_order_master();
					ord3.Id = dt3.Rows[k]["Id"].ToString();
					ord3.OrderDate = dt3.Rows[k]["OrderDate"].ToString().Replace("12:00:00 AM", "").Replace("00:00:00", "");
					ord3.OrderDate = DateTime.Parse(ord3.OrderDate).ToString("yyyy/MM/dd");
					ord3.OrderTime = dt3.Rows[k]["OrderTime"].ToString();
					ord3.OrderTimeFull = DateTime.Parse(ord3.OrderTime).ToString("HH:mm:ss");
					ord3.OrderTime = DateTime.Parse(ord3.OrderTime).ToString("HH:mm");
					ord3.OrderNo = dt3.Rows[k]["OrderNo"].ToString();
					ord3.UserId = dt3.Rows[k]["UserId"].ToString();
					ord3.selectedlotsize = dt3.Rows[k]["selectedlotsize"].ToString();
					ord3.UserName = dt3.Rows[k]["UserName"].ToString();
					ord3.OrderCategory = dt3.Rows[k]["OrderCategory"].ToString();
					ord3.OrderType = dt3.Rows[k]["OrderType"].ToString();
					ord3.ScriptName = dt3.Rows[k]["ScriptName"].ToString();
					ord3.TokenNo = dt3.Rows[k]["TokenNo"].ToString();
					ord3.ActionType = dt3.Rows[k]["ActionBy"].ToString();
					ord3.OrderPrice = dt3.Rows[k]["OrderPrice"].ToString();
					ord3.Lot = dt3.Rows[k]["Lot"].ToString();
					ord3.MarginUsed = dt3.Rows[k]["MarginUsed"].ToString();
					ord3.HoldingMarginReq = dt3.Rows[k]["HoldingMarginReq"].ToString();
					ord3.OrderStatus = dt3.Rows[k]["OrderStatus"].ToString();
					ord3.SymbolType = dt3.Rows[k]["SymbolType"].ToString();
					orderdata.Add(ord3);
				}
			}
			else
			{
				DateTime startdate2 = this.FirstDayOfWeek(DateTime.Now);
				DateTime enddate2 = this.LastDayOfWeek(DateTime.Now);
				string sdate2 = string.Concat(new string[]
				{
					startdate2.Year.ToString(),
					"-",
					startdate2.Month.ToString(),
					"-",
					startdate2.Day.ToString()
				});
				string edate2 = string.Concat(new string[]
				{
					enddate2.Year.ToString(),
					"-",
					enddate2.Month.ToString(),
					"-",
					enddate2.Day.ToString()
				});
				DataTable dt4 = Universal.SelectWithDS(string.Concat(new string[]
				{
					"select * from t_user_order where OrderStatus='",
					orderstatus,
					"' and UserId='",
					uid,
					"' and ClosedAt between '",
					sdate2,
					"' and '",
					edate2,
					"' ORDER BY ClosedAt DESC,ClosedTime DESC"
				}), "t_user_order");
				for (int l = 0; l < dt4.Rows.Count; l++)
				{
					t_order_master ord4 = new t_order_master();
					ord4.Id = dt4.Rows[l]["Id"].ToString();
					ord4.OrderDate = dt4.Rows[l]["OrderDate"].ToString().Replace("12:00:00 AM", "").Replace("00:00:00", "");
					ord4.OrderDate = DateTime.Parse(ord4.OrderDate).ToString("dd") + " " + DateTime.Parse(ord4.OrderDate).ToString("MMM");
					ord4.OrderTime = dt4.Rows[l]["OrderTime"].ToString();
					ord4.OrderTime = DateTime.Parse(ord4.OrderTime).ToString("HH:mm");
					ord4.OrderNo = dt4.Rows[l]["OrderNo"].ToString();
					ord4.UserId = dt4.Rows[l]["UserId"].ToString();
					ord4.ClosedTime = dt4.Rows[l]["ClosedTime"].ToString();
					ord4.ClosedTime = DateTime.Parse(ord4.ClosedTime).ToString("HH:mm");
					ord4.selectedlotsize = dt4.Rows[l]["selectedlotsize"].ToString();
					ord4.UserName = dt4.Rows[l]["UserName"].ToString();
					ord4.OrderCategory = dt4.Rows[l]["OrderCategory"].ToString();
					ord4.OrderType = dt4.Rows[l]["OrderType"].ToString();
					ord4.ScriptName = dt4.Rows[l]["ScriptName"].ToString();
					ord4.TokenNo = dt4.Rows[l]["TokenNo"].ToString();
					ord4.ActionType = dt4.Rows[l]["ActionBy"].ToString();
					ord4.ActionByClose = dt4.Rows[l]["ActionByClose"].ToString();
					ord4.OrderTypeClose = dt4.Rows[l]["OrderTypeClose"].ToString();
					ord4.OrderPrice = dt4.Rows[l]["OrderPrice"].ToString();
					ord4.Lot = dt4.Rows[l]["Lot"].ToString();
					ord4.MarginUsed = dt4.Rows[l]["MarginUsed"].ToString();
					ord4.HoldingMarginReq = dt4.Rows[l]["HoldingMarginReq"].ToString();
					ord4.OrderStatus = dt4.Rows[l]["OrderStatus"].ToString();
					ord4.SymbolType = dt4.Rows[l]["SymbolType"].ToString();
					ord4.BroughtBy = dt4.Rows[l]["BroughtBy"].ToString();
					ord4.P_L = dt4.Rows[l]["P_L"].ToString();
					ord4.Brokerage = dt4.Rows[l]["Brokerage"].ToString();
					ord4.ClosedAt = dt4.Rows[l]["ClosedAt"].ToString().Replace("12:00:00 AM", "").Replace("00:00:00", "");
					ord4.ClosedAt = DateTime.Parse(ord4.ClosedAt).ToString("dd") + " " + DateTime.Parse(ord4.ClosedAt).ToString("MMM");
					orderdata.Add(ord4);
				}
			}
			return new JavaScriptSerializer().Serialize(orderdata);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00013BF4 File Offset: 0x00011DF4
		public string gettradesbydate(string status, string fromdate, string todate, string uid)
		{
			List<t_order_master> orderdata = new List<t_order_master>();
			DataTable dt = Universal.SelectWithDS(string.Concat(new string[]
			{
				"select * from t_user_order where OrderStatus='",
				status,
				"' and UserId='",
				uid,
				"' and ClosedAt between '",
				fromdate,
				"' and '",
				todate,
				"' ORDER BY ClosedAt DESC,ClosedTime DESC"
			}), "t_user_order");
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				t_order_master ord = new t_order_master();
				ord.Id = dt.Rows[i]["Id"].ToString();
				ord.OrderDate = dt.Rows[i]["OrderDate"].ToString().Replace("12:00:00 AM", "").Replace("00:00:00", "");
				ord.OrderDate = DateTime.Parse(ord.OrderDate).ToString("dd") + " " + DateTime.Parse(ord.OrderDate).ToString("MMM");
				ord.OrderTime = dt.Rows[i]["OrderTime"].ToString();
				ord.OrderTime = DateTime.Parse(ord.OrderTime).ToString("HH:mm");
				ord.OrderNo = dt.Rows[i]["OrderNo"].ToString();
				ord.UserId = dt.Rows[i]["UserId"].ToString();
				ord.ClosedTime = dt.Rows[i]["ClosedTime"].ToString();
				ord.ClosedTime = DateTime.Parse(ord.ClosedTime).ToString("HH:mm");
				ord.selectedlotsize = dt.Rows[i]["selectedlotsize"].ToString();
				ord.UserName = dt.Rows[i]["UserName"].ToString();
				ord.OrderCategory = dt.Rows[i]["OrderCategory"].ToString();
				ord.OrderType = dt.Rows[i]["OrderType"].ToString();
				ord.ScriptName = dt.Rows[i]["ScriptName"].ToString();
				ord.TokenNo = dt.Rows[i]["TokenNo"].ToString();
				ord.ActionType = dt.Rows[i]["ActionBy"].ToString();
				ord.ActionByClose = dt.Rows[i]["ActionByClose"].ToString();
				ord.OrderTypeClose = dt.Rows[i]["OrderTypeClose"].ToString();
				ord.OrderPrice = dt.Rows[i]["OrderPrice"].ToString();
				ord.Lot = dt.Rows[i]["Lot"].ToString();
				ord.MarginUsed = dt.Rows[i]["MarginUsed"].ToString();
				ord.HoldingMarginReq = dt.Rows[i]["HoldingMarginReq"].ToString();
				ord.OrderStatus = dt.Rows[i]["OrderStatus"].ToString();
				ord.SymbolType = dt.Rows[i]["SymbolType"].ToString();
				ord.BroughtBy = dt.Rows[i]["BroughtBy"].ToString();
				ord.P_L = dt.Rows[i]["P_L"].ToString();
				ord.Brokerage = dt.Rows[i]["Brokerage"].ToString();
				ord.ClosedAt = dt.Rows[i]["ClosedAt"].ToString().Replace("12:00:00 AM", "").Replace("00:00:00", "");
				ord.ClosedAt = DateTime.Parse(ord.ClosedAt).ToString("dd") + " " + DateTime.Parse(ord.ClosedAt).ToString("MMM");
				orderdata.Add(ord);
			}
			return new JavaScriptSerializer().Serialize(orderdata);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000140C4 File Offset: 0x000122C4
		public string getallorders(string uid)
		{
			List<t_order_master> orderdata = new List<t_order_master>();
			DateTime startdate = this.FirstDayOfWeek(DateTime.Now);
			DateTime enddate = this.LastDayOfWeek(DateTime.Now);
			string sdate = string.Concat(new string[]
			{
				startdate.Year.ToString(),
				"-",
				startdate.Month.ToString(),
				"-",
				startdate.Day.ToString()
			});
			string edate = string.Concat(new string[]
			{
				enddate.Year.ToString(),
				"-",
				enddate.Month.ToString(),
				"-",
				enddate.Day.ToString()
			});
			string pending_smdtext = string.Concat(new string[]
			{
				"select * from t_user_order where ((OrderStatus='Pending' OR OrderStatus='Cancel') and UserId='",
				uid,
				"' and OrderDate='",
				Universal.GetDate,
				"') or (OrderType='Limit' and OrderStatus='Active' and UserId='",
				uid,
				"' and OrderDate='",
				Universal.GetDate,
				"') ORDER BY Id DESC"
			});
			string Active_cmdtext = string.Concat(new string[]
			{
				"select * from t_user_order where OrderStatus='Active' and UserId='",
				uid,
				"' and OrderDate between '",
				sdate,
				"' and '",
				edate,
				"'   ORDER BY OrderDate DESC,OrderTime DESC"
			});
			string closed_cmdtext = string.Concat(new string[]
			{
				"select * from t_user_order where OrderStatus='Closed' and UserId='",
				uid,
				"' and ClosedAt between '",
				sdate,
				"' and '",
				edate,
				"' ORDER BY ClosedAt DESC,ClosedTime DESC"
			});
			DataSet ds = Universal.SelectWithDSReturnDS(new string[]
			{
				pending_smdtext,
				Active_cmdtext,
				closed_cmdtext
			});
			for (int i = 0; i < ds.Tables.Count; i++)
			{
				for (int j = 0; j < ds.Tables[i].Rows.Count; j++)
				{
					t_order_master ord = new t_order_master();
					ord.Id = ds.Tables[i].Rows[j]["Id"].ToString();
					ord.OrderDate = ds.Tables[i].Rows[j]["OrderDate"].ToString().Replace("12:00:00 AM", "").Replace("00:00:00", "");
					ord.OrderTime = ds.Tables[i].Rows[j]["OrderTime"].ToString();
					ord.OrderTime = DateTime.Parse(ord.OrderTime).ToString("HH:mm");
					ord.OrderNo = ds.Tables[i].Rows[j]["OrderNo"].ToString();
					ord.UserId = ds.Tables[i].Rows[j]["UserId"].ToString();
					ord.OrderStatus = ds.Tables[i].Rows[j]["OrderStatus"].ToString();
					if (ord.OrderStatus == "Closed")
					{
						ord.ClosedTime = ds.Tables[i].Rows[j]["ClosedTime"].ToString();
						ord.ClosedTime = DateTime.Parse(ord.ClosedTime).ToString("HH:mm");
						ord.ClosedAt = ds.Tables[i].Rows[j]["ClosedAt"].ToString().Replace("12:00:00 AM", "").Replace("00:00:00", "");
						ord.ClosedAt = DateTime.Parse(ord.ClosedAt).ToString("dd") + " " + DateTime.Parse(ord.ClosedAt).ToString("MMM");
					}
					if (ord.OrderStatus == "Active")
					{
						ord.OrderDate = DateTime.Parse(ord.OrderDate).ToString("yyyy/MM/dd");
					}
					else
					{
						ord.OrderDate = DateTime.Parse(ord.OrderDate).ToString("dd") + " " + DateTime.Parse(ord.OrderDate).ToString("MMM");
					}
					ord.isstoplossorder = ds.Tables[i].Rows[j]["isstoplossorder"].ToString();
					ord.selectedlotsize = ds.Tables[i].Rows[j]["selectedlotsize"].ToString();
					ord.UserName = ds.Tables[i].Rows[j]["UserName"].ToString();
					ord.OrderCategory = ds.Tables[i].Rows[j]["OrderCategory"].ToString();
					ord.OrderType = ds.Tables[i].Rows[j]["OrderType"].ToString();
					ord.ScriptName = ds.Tables[i].Rows[j]["ScriptName"].ToString();
					ord.TokenNo = ds.Tables[i].Rows[j]["TokenNo"].ToString();
					ord.ActionType = ds.Tables[i].Rows[j]["ActionBy"].ToString();
					ord.ActionByClose = ds.Tables[i].Rows[j]["ActionByClose"].ToString();
					ord.OrderTypeClose = ds.Tables[i].Rows[j]["OrderTypeClose"].ToString();
					ord.OrderPrice = ds.Tables[i].Rows[j]["OrderPrice"].ToString();
					ord.Lot = ds.Tables[i].Rows[j]["Lot"].ToString();
					ord.MarginUsed = ds.Tables[i].Rows[j]["MarginUsed"].ToString();
					ord.HoldingMarginReq = ds.Tables[i].Rows[j]["HoldingMarginReq"].ToString();
					ord.SymbolType = ds.Tables[i].Rows[j]["SymbolType"].ToString();
					ord.BroughtBy = ds.Tables[i].Rows[j]["BroughtBy"].ToString();
					ord.P_L = ds.Tables[i].Rows[j]["P_L"].ToString();
					ord.Brokerage = ds.Tables[i].Rows[j]["Brokerage"].ToString();
					orderdata.Add(ord);
				}
			}
			return new JavaScriptSerializer().Serialize(orderdata);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00014914 File Offset: 0x00012B14
		[HttpPost]
		public string tradesettlementpost_app(t_order_master obj)
		{
			string appmsg = "New trade is not placed only settlement done.";
			(decimal.Parse(obj.OrderPrice) * decimal.Parse(obj.Lot)).ToString();
			IPAddress externalIp = IPAddress.Parse(Universal.devip);
			string userId = obj.UserId;
			string checkcond;
			if (obj.OrderCategory == "SELL")
			{
				checkcond = "BUY";
			}
			else
			{
				checkcond = "SELL";
			}
			string similer_syml;
			if (obj.SymbolType == "MCX")
			{
				string[] symarr = obj.ScriptName.Split(new char[]
				{
					'_'
				});
				similer_syml = this.setsymbol(symarr[0].Trim());
			}
			else
			{
				similer_syml = "GOLD";
			}
			int totaldatabase_lot = 0;
			DataTable dt = Universal.SelectWithDS(string.Concat(new string[]
			{
				"select (select SUM(Lot) from t_user_order where UserId='",
				obj.UserId,
				"' and OrderCategory='",
				checkcond,
				"' and OrderStatus='Active' and TokenNo='",
				obj.TokenNo,
				"') as totaldatabase_lot,t_trading_all_users_master.NSE_Exposure_Type,t_trading_all_users_master.NSE_Brokerage_Type,t_trading_all_users_master.Equity_brokerage_per_crore,t_trading_all_users_master.Intraday_Exposure_Margin_Equity,t_trading_all_users_master.Holding_Exposure_Margin_Equity,t_trading_all_users_master.Mcx_Brokerage_Type,t_trading_all_users_master.MCX_brokerage_per_crore,t_trading_all_users_master.",
				similer_syml,
				"_brokerage,t_trading_all_users_master.Mcx_Exposure_Type, t_trading_all_users_master.Intraday_Exposure_Margin_MCX, t_trading_all_users_master.Holding_Exposure_Margin_MCX, t_trading_all_users_master.MCX_Exposure_Lot_wise_",
				similer_syml,
				"_Intraday, t_trading_all_users_master.MCX_Exposure_Lot_wise_",
				similer_syml,
				"_Holding,t_trading_all_users_master.CDS_Brokerage_Type ,t_trading_all_users_master.CDS_brokerage_per_crore ,t_trading_all_users_master.CDS_Exposure_Type ,t_trading_all_users_master.Intraday_Exposure_Margin_CDS ,t_trading_all_users_master.Holding_Exposure_Margin_CDS  from t_trading_all_users_master where Id='",
				obj.UserId,
				"'  "
			}), "t_user_order");
			string NSE_Exposure_Type = "";
			string NSE_Brokerage_Type = "";
			string Equity_brokerage_per_crore = "";
			string Intraday_Exposure_Margin_Equity = "";
			string Holding_Exposure_Margin_Equity = "";
			string Mcx_Brokerage_Type = "";
			string MCX_brokerage_per_crore = "";
			string MCXsymb_brokerage = "";
			string Mcx_Exposure_Type = "";
			string Intraday_Exposure_Margin_MCX = "";
			string Holding_Exposure_Margin_MCX = "";
			string MCX_Exposure_Lot_wise_sym_Intraday = "";
			string MCX_Exposure_Lot_wise_sym_Holding = "";
			string CDS_Exposure_Type = "";
			string CDS_Brokerage_Type = "";
			string CDS_brokerage_per_crore = "";
			string Intraday_Exposure_Margin_CDS = "";
			string Holding_Exposure_Margin_CDS = "";
			decimal finalbrokerage = 0m;
			decimal final_intraday_exp = 0m;
			decimal final_indraday_holding = 0m;
			if (dt.Rows.Count > 0)
			{
				NSE_Exposure_Type = dt.Rows[0]["NSE_Exposure_Type"].ToString();
				NSE_Brokerage_Type = dt.Rows[0]["NSE_Brokerage_Type"].ToString();
				Equity_brokerage_per_crore = dt.Rows[0]["Equity_brokerage_per_crore"].ToString();
				Intraday_Exposure_Margin_Equity = dt.Rows[0]["Intraday_Exposure_Margin_Equity"].ToString();
				Holding_Exposure_Margin_Equity = dt.Rows[0]["Holding_Exposure_Margin_Equity"].ToString();
				Mcx_Brokerage_Type = dt.Rows[0]["Mcx_Brokerage_Type"].ToString();
				MCX_brokerage_per_crore = dt.Rows[0]["MCX_brokerage_per_crore"].ToString();
				MCXsymb_brokerage = dt.Rows[0][similer_syml + "_brokerage"].ToString();
				Mcx_Exposure_Type = dt.Rows[0]["Mcx_Exposure_Type"].ToString();
				Intraday_Exposure_Margin_MCX = dt.Rows[0]["Intraday_Exposure_Margin_MCX"].ToString();
				Holding_Exposure_Margin_MCX = dt.Rows[0]["Holding_Exposure_Margin_MCX"].ToString();
				MCX_Exposure_Lot_wise_sym_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_" + similer_syml + "_Intraday"].ToString();
				MCX_Exposure_Lot_wise_sym_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_" + similer_syml + "_Holding"].ToString();
				CDS_Exposure_Type = dt.Rows[0]["CDS_Exposure_Type"].ToString();
				CDS_Brokerage_Type = dt.Rows[0]["CDS_Brokerage_Type"].ToString();
				CDS_brokerage_per_crore = dt.Rows[0]["CDS_brokerage_per_crore"].ToString();
				Intraday_Exposure_Margin_CDS = dt.Rows[0]["Intraday_Exposure_Margin_CDS"].ToString();
				Holding_Exposure_Margin_CDS = dt.Rows[0]["Holding_Exposure_Margin_CDS"].ToString();
				if (dt.Rows[0]["totaldatabase_lot"].ToString() != "")
				{
					totaldatabase_lot = int.Parse(dt.Rows[0]["totaldatabase_lot"].ToString());
				}
				else
				{
					totaldatabase_lot = 0;
				}
			}
			if (totaldatabase_lot > int.Parse(obj.Lot))
			{
				DataTable pretable = Universal.SelectWithDS(string.Concat(new string[]
				{
					"select t_user_order.Id,selectedlotsize,DATE_FORMAT(OrderDate,'%Y-%m-%d') as OrderDate,OrderTime,OrderCategory,TokenNo,OrderPrice,t_user_order.SymbolType,Lot,t_universal_tradeble_tokens.buy,t_universal_tradeble_tokens.sell from t_user_order INNER JOIN t_universal_tradeble_tokens ON t_user_order.TokenNo=t_universal_tradeble_tokens.instrument_token  INNER JOIN  t_trading_all_users_master ON t_trading_all_users_master.Id=t_user_order.UserId where UserId='",
					obj.UserId,
					"' and TokenNo='",
					obj.TokenNo,
					"' and OrderCategory='",
					checkcond,
					"' and OrderStatus='Active' ORDER BY Id"
				}), "t_user_order");
				int incoming_lot = int.Parse(obj.Lot);
				for (int i = 0; i < pretable.Rows.Count; i++)
				{
					string Id = pretable.Rows[i]["Id"].ToString();
					string Lot = pretable.Rows[i]["Lot"].ToString();
					pretable.Rows[i]["buy"].ToString();
					pretable.Rows[i]["sell"].ToString();
					string OrderPrice = pretable.Rows[i]["OrderPrice"].ToString();
					string OrderCategory = pretable.Rows[i]["OrderCategory"].ToString();
					string OrderDate = pretable.Rows[i]["OrderDate"].ToString();
					string OrderTime = pretable.Rows[i]["OrderTime"].ToString();
					if (obj.SymbolType == "MCX")
					{
						if (Mcx_Brokerage_Type == "per_lot")
						{
							finalbrokerage = decimal.Parse(MCXsymb_brokerage);
						}
						else
						{
							finalbrokerage = (decimal.Parse(OrderPrice) + decimal.Parse(obj.OrderPrice)) * decimal.Parse(obj.selectedlotsize) / 10000000m * decimal.Parse(MCX_brokerage_per_crore);
						}
						if (Mcx_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(MCX_Exposure_Lot_wise_sym_Intraday);
							final_indraday_holding = decimal.Parse(MCX_Exposure_Lot_wise_sym_Holding);
						}
						else
						{
							final_intraday_exp = decimal.Parse(OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_MCX);
							final_indraday_holding = decimal.Parse(OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_MCX);
						}
					}
					else if (obj.SymbolType == "NSE")
					{
						if (NSE_Brokerage_Type == "per_lot")
						{
							finalbrokerage = decimal.Parse(Equity_brokerage_per_crore);
						}
						else
						{
							finalbrokerage = (decimal.Parse(OrderPrice) + decimal.Parse(obj.OrderPrice)) * decimal.Parse(obj.selectedlotsize) / 10000000m * decimal.Parse(Equity_brokerage_per_crore);
						}
						if (NSE_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(Intraday_Exposure_Margin_Equity);
							final_indraday_holding = decimal.Parse(Holding_Exposure_Margin_Equity);
						}
						else
						{
							final_intraday_exp = decimal.Parse(OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_Equity);
							final_indraday_holding = decimal.Parse(OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_Equity);
						}
					}
					else if (obj.SymbolType == "OPT")
					{
						if (CDS_Brokerage_Type == "per_lot")
						{
							finalbrokerage = decimal.Parse(CDS_brokerage_per_crore);
						}
						else
						{
							finalbrokerage = (decimal.Parse(OrderPrice) + decimal.Parse(obj.OrderPrice)) * decimal.Parse(obj.selectedlotsize) / 10000000m * decimal.Parse(CDS_brokerage_per_crore);
						}
						if (CDS_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(Intraday_Exposure_Margin_CDS);
							final_indraday_holding = decimal.Parse(Holding_Exposure_Margin_CDS);
						}
						else
						{
							final_intraday_exp = decimal.Parse(OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_CDS);
							final_indraday_holding = decimal.Parse(OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_CDS);
						}
					}
					if (incoming_lot > int.Parse(Lot))
					{
						int sublot = incoming_lot - int.Parse(Lot);
						decimal closevalue = 0m;
						string ActionCloseBy;
						string cmprice;
						if (obj.OrderCategory == "SELL")
						{
							ActionCloseBy = "Sold By Trader";
							cmprice = obj.OrderPrice;
							closevalue = decimal.Parse(cmprice) - decimal.Parse(OrderPrice);
						}
						else
						{
							ActionCloseBy = "Bought By Trader";
							cmprice = obj.OrderPrice;
							closevalue = decimal.Parse(OrderPrice) - decimal.Parse(cmprice);
						}
						decimal final_pl = Math.Round(closevalue * int.Parse(obj.selectedlotsize) * int.Parse(Lot), 1);
						DataTable brokeragetbl = Universal.SelectWithDS("select LedgerBalance,creditLimit from t_trading_all_users_master where Id='" + obj.UserId + "'", "t_trading_all_users_master");
						decimal LedgerBalance = 0m;
						if (brokeragetbl.Rows.Count > 0)
						{
							LedgerBalance = decimal.Parse(brokeragetbl.Rows[0][0].ToString());
							decimal.Parse(brokeragetbl.Rows[0][1].ToString());
						}
						finalbrokerage *= int.Parse(Lot);
						if (Universal.ExecuteNonQuery(string.Concat(new string[]
						{
							"update t_user_order set P_L='",
							final_pl.ToString(),
							"',OrderTypeClose='Market',ActionByClose='",
							ActionCloseBy,
							"',OrderStatus='Closed',Brokerage='",
							finalbrokerage.ToString(),
							"',BroughtBy='",
							cmprice,
							"',ClosedAt='",
							Universal.GetDate,
							"',ClosedTime='",
							Universal.GetTime,
							"',OrderRemark='OrderClosedFromClientSettlement' where Id='",
							Id,
							"'"
						})) == 1)
						{
							string msg = string.Concat(new string[]
							{
								obj.UserName,
								"(",
								obj.UserId,
								") have ",
								obj.OrderType,
								" ",
								obj.Lot,
								" lots of ",
								obj.ScriptName,
								" at ",
								obj.OrderPrice,
								" Auto Closed due to settlement."
							});
							string[] array = new string[11];
							array[0] = "insert into t_action_master (Message,ActionDate,ActionTime,ActionFrom,IpAddress) values('";
							array[1] = msg;
							array[2] = "','";
							array[3] = Universal.GetDate;
							array[4] = "','";
							array[5] = Universal.GetTime;
							array[6] = "','";
							array[7] = obj.UserId;
							array[8] = "','";
							int num = 9;
							IPAddress ipaddress = externalIp;
							array[num] = ((ipaddress != null) ? ipaddress.ToString() : null);
							array[10] = "')";
							Universal.ExecuteNonQuery(string.Concat(array));
						}
						Universal.ExecuteNonQuery(string.Concat(new string[]
						{
							"update t_trading_all_users_master set LedgerBalance='",
							(LedgerBalance - finalbrokerage + final_pl).ToString(),
							"' where Id='",
							obj.UserId,
							"'"
						}));
						if (sublot > 0)
						{
							incoming_lot = sublot;
						}
					}
					else if (incoming_lot <= int.Parse(Lot))
					{
						int sublot2 = int.Parse(Lot) - incoming_lot;
						decimal closevalue2 = 0m;
						string CloseActionBy;
						string actiontype;
						string cmp2;
						if (obj.OrderCategory == "SELL")
						{
							CloseActionBy = "Sold By Trader";
							actiontype = "Bought By Trader";
							cmp2 = obj.OrderPrice;
							closevalue2 = decimal.Parse(cmp2) - decimal.Parse(OrderPrice);
						}
						else
						{
							CloseActionBy = "Bought By Trader";
							actiontype = "Sold By Trader";
							cmp2 = obj.OrderPrice;
							closevalue2 = decimal.Parse(OrderPrice) - decimal.Parse(cmp2);
						}
						decimal final_pl2 = Math.Round(closevalue2 * int.Parse(obj.selectedlotsize) * incoming_lot, 1);
						DataTable brokeragetbl2 = Universal.SelectWithDS("select LedgerBalance,creditLimit from t_trading_all_users_master where Id='" + obj.UserId + "'", "t_trading_all_users_master");
						decimal LedgerBalance2 = 0m;
						if (brokeragetbl2.Rows.Count > 0)
						{
							LedgerBalance2 = decimal.Parse(brokeragetbl2.Rows[0][0].ToString());
							decimal.Parse(brokeragetbl2.Rows[0][1].ToString());
						}
						if (sublot2 > 0)
						{
							decimal marginvalue = final_intraday_exp * sublot2;
							decimal holdmargn = final_indraday_holding * sublot2;
							if (Universal.ExecuteNonQuery(string.Concat(new string[]
							{
								"update t_user_order set Lot='",
								sublot2.ToString(),
								"',MarginUsed='",
								marginvalue.ToString(),
								"',HoldingMarginReq='",
								holdmargn.ToString(),
								"' where Id='",
								Id,
								"'"
							})) == 1)
							{
								string msg2 = string.Concat(new string[]
								{
									obj.UserName,
									"(",
									obj.UserId,
									") have ",
									obj.OrderType,
									" ",
									sublot2.ToString(),
									" lots of ",
									obj.ScriptName,
									" at ",
									obj.OrderPrice,
									". Trade has been modified by Autotrade settlement. "
								});
								string[] array2 = new string[11];
								array2[0] = "insert into t_action_master (Message,ActionDate,ActionTime,ActionFrom,IpAddress) values('";
								array2[1] = msg2;
								array2[2] = "','";
								array2[3] = Universal.GetDate;
								array2[4] = "','";
								array2[5] = Universal.GetTime;
								array2[6] = "','";
								array2[7] = obj.UserId;
								array2[8] = "','";
								int num2 = 9;
								IPAddress ipaddress2 = externalIp;
								array2[num2] = ((ipaddress2 != null) ? ipaddress2.ToString() : null);
								array2[10] = "')";
								Universal.ExecuteNonQuery(string.Concat(array2));
							}
						}
						decimal finalbrokerage2 = finalbrokerage * incoming_lot;
						Universal.ExecuteNonQuery(string.Concat(new string[]
						{
							"update t_trading_all_users_master set LedgerBalance='",
							(LedgerBalance2 - decimal.Parse(finalbrokerage2.ToString()) + final_pl2).ToString(),
							"' where Id='",
							obj.UserId,
							"'"
						}));
						decimal marginvalue2 = final_intraday_exp * incoming_lot;
						decimal holdmargn2 = final_indraday_holding * incoming_lot;
						if (Universal.ExecuteNonQuery(string.Concat(new string[]
						{
							"insert into t_user_order (selectedlotsize,OrderDate,OrderCategory,OrderTime,OrderNo,UserId,UserName,OrderType,ScriptName,TokenNo,ActionBy,OrderPrice,Lot,MarginUsed,HoldingMarginReq,OrderStatus,SymbolType,isstoplossorder,BroughtBy,P_L,Brokerage,ClosedAt,ClosedTime,OrderTypeClose,ActionByClose,OrderRemark) values('",
							obj.selectedlotsize,
							"','",
							OrderDate,
							"','",
							OrderCategory,
							"','",
							OrderTime,
							"','",
							obj.OrderNo,
							"','",
							obj.UserId,
							"','",
							obj.UserName,
							"','",
							obj.OrderType,
							"','",
							obj.ScriptName,
							"','",
							obj.TokenNo,
							"','",
							actiontype,
							"','",
							OrderPrice.Trim().Replace(" ", ""),
							"','",
							incoming_lot.ToString(),
							"','",
							marginvalue2.ToString(),
							"','",
							holdmargn2.ToString(),
							"','Closed','",
							obj.SymbolType,
							"','",
							obj.isstoplossorder,
							"','",
							cmp2,
							"','",
							final_pl2.ToString(),
							"','",
							finalbrokerage2.ToString(),
							"','",
							Universal.GetDate,
							"','",
							Universal.GetTime,
							"','Market','",
							CloseActionBy,
							"','OrderInsertedFromClientSettlement')"
						})) == 1)
						{
							string msg3 = string.Concat(new string[]
							{
								obj.UserName,
								"(",
								obj.UserId,
								") have ",
								OrderCategory,
								" ",
								incoming_lot.ToString(),
								" lots of ",
								obj.ScriptName,
								" at ",
								OrderPrice.Trim().Replace(" ", ""),
								". Auto Closed due to settlement."
							});
							string[] array3 = new string[11];
							array3[0] = "insert into t_action_master (Message,ActionDate,ActionTime,ActionFrom,IpAddress) values('";
							array3[1] = msg3;
							array3[2] = "','";
							array3[3] = Universal.GetDate;
							array3[4] = "','";
							array3[5] = Universal.GetTime;
							array3[6] = "','";
							array3[7] = obj.UserId;
							array3[8] = "','";
							int num3 = 9;
							IPAddress ipaddress3 = externalIp;
							array3[num3] = ((ipaddress3 != null) ? ipaddress3.ToString() : null);
							array3[10] = "')";
							Universal.ExecuteNonQuery(string.Concat(array3));
						}
						if (sublot2 == 0)
						{
							Universal.ExecuteNonQuery("delete from t_user_order where Id='" + Id + "'");
							break;
						}
						break;
					}
				}
			}
			else if (totaldatabase_lot <= int.Parse(obj.Lot))
			{
				DataTable pretable2 = Universal.SelectWithDS(string.Concat(new string[]
				{
					"select t_user_order.Id,selectedlotsize,OrderCategory,TokenNo,OrderPrice,t_user_order.SymbolType,Lot,t_universal_tradeble_tokens.buy,t_universal_tradeble_tokens.sell from t_user_order INNER JOIN t_universal_tradeble_tokens ON t_user_order.TokenNo=t_universal_tradeble_tokens.instrument_token where UserId='",
					obj.UserId,
					"' and TokenNo='",
					obj.TokenNo,
					"' and OrderCategory='",
					checkcond,
					"' and OrderStatus='Active' ORDER BY Lot"
				}), "t_user_order");
				int defflot = int.Parse(obj.Lot) - totaldatabase_lot;
				DataTable brokeragetbl3 = Universal.SelectWithDS("select LedgerBalance,creditLimit from t_trading_all_users_master where Id='" + obj.UserId + "'", "t_trading_all_users_master");
				decimal LedgerBalance3 = 0m;
				if (brokeragetbl3.Rows.Count > 0)
				{
					LedgerBalance3 = decimal.Parse(brokeragetbl3.Rows[0][0].ToString());
					decimal.Parse(brokeragetbl3.Rows[0][1].ToString());
				}
				for (int j = 0; j < pretable2.Rows.Count; j++)
				{
					string Id2 = pretable2.Rows[j]["Id"].ToString();
					string Lot2 = pretable2.Rows[j]["Lot"].ToString();
					pretable2.Rows[j]["buy"].ToString();
					pretable2.Rows[j]["sell"].ToString();
					string selectedlotsize = pretable2.Rows[j]["selectedlotsize"].ToString();
					string OrderPrice2 = pretable2.Rows[j]["OrderPrice"].ToString();
					if (obj.SymbolType == "MCX")
					{
						if (Mcx_Brokerage_Type == "per_lot")
						{
							finalbrokerage = decimal.Parse(MCXsymb_brokerage);
						}
						else
						{
							finalbrokerage = (decimal.Parse(OrderPrice2) + decimal.Parse(obj.OrderPrice)) * decimal.Parse(obj.selectedlotsize) / 10000000m * decimal.Parse(MCX_brokerage_per_crore);
						}
						if (Mcx_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(MCX_Exposure_Lot_wise_sym_Intraday);
							final_indraday_holding = decimal.Parse(MCX_Exposure_Lot_wise_sym_Holding);
						}
						else
						{
							final_intraday_exp = decimal.Parse(OrderPrice2) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_MCX);
							final_indraday_holding = decimal.Parse(OrderPrice2) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_MCX);
						}
					}
					else if (obj.SymbolType == "NSE")
					{
						if (NSE_Brokerage_Type == "per_lot")
						{
							finalbrokerage = decimal.Parse(Equity_brokerage_per_crore);
						}
						else
						{
							finalbrokerage = (decimal.Parse(OrderPrice2) + decimal.Parse(obj.OrderPrice)) * decimal.Parse(obj.selectedlotsize) / 10000000m * decimal.Parse(Equity_brokerage_per_crore);
						}
						if (NSE_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(Intraday_Exposure_Margin_Equity);
							final_indraday_holding = decimal.Parse(Holding_Exposure_Margin_Equity);
						}
						else
						{
							final_intraday_exp = decimal.Parse(OrderPrice2) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_Equity);
							final_indraday_holding = decimal.Parse(OrderPrice2) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_Equity);
						}
					}
					else if (obj.SymbolType == "OPT")
					{
						if (CDS_Brokerage_Type == "per_lot")
						{
							finalbrokerage = decimal.Parse(CDS_brokerage_per_crore);
						}
						else
						{
							finalbrokerage = (decimal.Parse(OrderPrice2) + decimal.Parse(obj.OrderPrice)) * decimal.Parse(obj.selectedlotsize) / 10000000m * decimal.Parse(CDS_brokerage_per_crore);
						}
						if (CDS_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(Intraday_Exposure_Margin_CDS);
							final_indraday_holding = decimal.Parse(Holding_Exposure_Margin_CDS);
						}
						else
						{
							final_intraday_exp = decimal.Parse(OrderPrice2) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_CDS);
							final_indraday_holding = decimal.Parse(OrderPrice2) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_CDS);
						}
					}
					decimal closevalue3 = 0m;
					string ActionCloseBy2;
					string cmprice2;
					if (obj.OrderCategory == "SELL")
					{
						ActionCloseBy2 = "Sold By Trader";
						cmprice2 = obj.OrderPrice;
						closevalue3 = decimal.Parse(cmprice2) - decimal.Parse(OrderPrice2);
					}
					else
					{
						ActionCloseBy2 = "Bought By Trader";
						cmprice2 = obj.OrderPrice;
						closevalue3 = decimal.Parse(OrderPrice2) - decimal.Parse(cmprice2);
					}
					decimal final_pl3 = Math.Round(closevalue3 * int.Parse(selectedlotsize) * int.Parse(Lot2), 1);
					decimal finalbrokerage3 = finalbrokerage * int.Parse(Lot2);
					if (Universal.ExecuteNonQuery(string.Concat(new string[]
					{
						"update t_user_order set P_L='",
						final_pl3.ToString(),
						"',OrderTypeClose='Market',ActionByClose='",
						ActionCloseBy2,
						"',OrderStatus='Closed',Brokerage='",
						finalbrokerage3.ToString(),
						"',BroughtBy='",
						cmprice2,
						"',ClosedAt='",
						Universal.GetDate,
						"',ClosedTime='",
						Universal.GetTime,
						"',OrderRemark='OrderClosedFromClientSettlement' where Id='",
						Id2,
						"'"
					})) == 1)
					{
						string msg4 = string.Concat(new string[]
						{
							obj.UserName,
							"(",
							obj.UserId,
							") have ",
							obj.OrderCategory,
							" ",
							obj.Lot,
							" lots of ",
							obj.ScriptName,
							" at ",
							obj.OrderPrice,
							". Auto Closed due to settlement."
						});
						string[] array4 = new string[11];
						array4[0] = "insert into t_action_master (Message,ActionDate,ActionTime,ActionFrom,IpAddress) values('";
						array4[1] = msg4;
						array4[2] = "','";
						array4[3] = Universal.GetDate;
						array4[4] = "','";
						array4[5] = Universal.GetTime;
						array4[6] = "','";
						array4[7] = obj.UserId;
						array4[8] = "','";
						int num4 = 9;
						IPAddress ipaddress4 = externalIp;
						array4[num4] = ((ipaddress4 != null) ? ipaddress4.ToString() : null);
						array4[10] = "')";
						Universal.ExecuteNonQuery(string.Concat(array4));
					}
					Universal.ExecuteNonQuery(string.Concat(new string[]
					{
						"update t_trading_all_users_master set LedgerBalance='",
						(LedgerBalance3 - decimal.Parse(finalbrokerage3.ToString()) + final_pl3).ToString(),
						"' where Id='",
						obj.UserId,
						"'"
					}));
				}
				if (defflot > 0)
				{
					if (obj.SymbolType == "MCX")
					{
						if (Mcx_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(MCX_Exposure_Lot_wise_sym_Intraday);
							final_indraday_holding = decimal.Parse(MCX_Exposure_Lot_wise_sym_Holding);
						}
						else
						{
							final_intraday_exp = decimal.Parse(obj.OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_MCX);
							final_indraday_holding = decimal.Parse(obj.OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_MCX);
						}
					}
					else if (obj.SymbolType == "NSE")
					{
						if (NSE_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(Intraday_Exposure_Margin_Equity);
							final_indraday_holding = decimal.Parse(Holding_Exposure_Margin_Equity);
						}
						else
						{
							final_intraday_exp = decimal.Parse(obj.OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_Equity);
							final_indraday_holding = decimal.Parse(obj.OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_Equity);
						}
					}
					else if (obj.SymbolType == "OPT")
					{
						if (CDS_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(Intraday_Exposure_Margin_CDS);
							final_indraday_holding = decimal.Parse(Holding_Exposure_Margin_CDS);
						}
						else
						{
							final_intraday_exp = decimal.Parse(obj.OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_CDS);
							final_indraday_holding = decimal.Parse(obj.OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_CDS);
						}
					}
					decimal marginvalue3 = final_intraday_exp * defflot;
					decimal holdmargn3 = final_indraday_holding * defflot;
					if (Universal.ExecuteNonQuery(string.Concat(new string[]
					{
						"insert into t_user_order (selectedlotsize,OrderDate,OrderCategory,OrderTime,OrderNo,UserId,UserName,OrderType,ScriptName,TokenNo,ActionBy,OrderPrice,Lot,MarginUsed,HoldingMarginReq,OrderStatus,SymbolType,isstoplossorder,OrderRemark) values('",
						obj.selectedlotsize,
						"','",
						Universal.GetDate,
						"','",
						obj.OrderCategory,
						"','",
						obj.OrderTime,
						"','",
						obj.OrderNo,
						"','",
						obj.UserId,
						"','",
						obj.UserName,
						"','",
						obj.OrderType,
						"','",
						obj.ScriptName,
						"','",
						obj.TokenNo,
						"','",
						obj.ActionType,
						"','",
						obj.OrderPrice.Trim().Replace(" ", ""),
						"','",
						defflot.ToString(),
						"','",
						marginvalue3.ToString(),
						"','",
						holdmargn3.ToString(),
						"','Active','",
						obj.SymbolType,
						"','",
						obj.isstoplossorder,
						"','OrderInsertedFromClientSettlement')"
					})) == 1)
					{
						string msg5 = string.Concat(new string[]
						{
							obj.UserName,
							"(",
							obj.UserId,
							") have ",
							obj.OrderCategory,
							" ",
							obj.Lot,
							" lots of ",
							obj.ScriptName,
							" at ",
							obj.OrderPrice.Trim().Replace(" ", ""),
							". Traded by client @ Market."
						});
						string[] array5 = new string[11];
						array5[0] = "insert into t_action_master (Message,ActionDate,ActionTime,ActionFrom,IpAddress) values('";
						array5[1] = msg5;
						array5[2] = "','";
						array5[3] = Universal.GetDate;
						array5[4] = "','";
						array5[5] = Universal.GetTime;
						array5[6] = "','";
						array5[7] = obj.UserId;
						array5[8] = "','";
						int num5 = 9;
						IPAddress ipaddress5 = externalIp;
						array5[num5] = ((ipaddress5 != null) ? ipaddress5.ToString() : null);
						array5[10] = "')";
						Universal.ExecuteNonQuery(string.Concat(array5));
						return string.Concat(new string[]
						{
							"Trade placed of ",
							obj.ScriptName,
							" (",
							obj.Lot,
							" Lots) at price ",
							obj.OrderPrice
						});
					}
				}
			}
			return appmsg;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0001665C File Offset: 0x0001485C
		[HttpPost]
		public void tradesettlementpost_admin(t_order_master obj)
		{
			Universal.ExecuteNonQuery(string.Concat(new string[]
			{
				"delete from t_selected_symbols_by_user where UserId='",
				obj.UserId,
				"' and SymbolToken='",
				obj.TokenNo,
				"';insert into t_selected_symbols_by_user (ExchangeType,SymbolName,SymbolToken,UserId,lotsize) values('",
				obj.SymbolType,
				"','",
				obj.ScriptName,
				"','",
				obj.TokenNo,
				"','",
				obj.UserId,
				"','",
				obj.selectedlotsize,
				"')"
			}));
			obj.OrderType = "Market";
			obj.OrderDate = Universal.GetDate;
			obj.OrderTime = Universal.GetTime;
			obj.OrderNo = new Random().Next().ToString();
			(decimal.Parse(obj.OrderPrice) * decimal.Parse(obj.Lot)).ToString();
			IPAddress externalIp = IPAddress.Parse(new WebClient().DownloadString("http://icanhazip.com").Replace("\\r\\n", "").Replace("\\n", "").Trim());
			string userId = obj.UserId;
			string checkcond;
			if (obj.OrderCategory == "SELL")
			{
				obj.ActionType = "Sold by Admin";
				checkcond = "BUY";
			}
			else
			{
				obj.ActionType = "Bought by Admin";
				checkcond = "SELL";
			}
			string similer_syml;
			if (obj.SymbolType == "MCX")
			{
				string[] symarr = obj.ScriptName.Split(new char[]
				{
					'_'
				});
				similer_syml = this.setsymbol(symarr[0].Trim());
			}
			else
			{
				similer_syml = "GOLD";
			}
			int totaldatabase_lot = 0;
			DataTable dt = Universal.SelectWithDS(string.Concat(new string[]
			{
				"select (select SUM(Lot) from t_user_order where UserId='",
				obj.UserId,
				"' and OrderCategory='",
				checkcond,
				"' and OrderStatus='Active' and TokenNo='",
				obj.TokenNo,
				"') as totaldatabase_lot,t_trading_all_users_master.TradeEquityUnits,t_trading_all_users_master.TradeMCXUnits,t_trading_all_users_master.TradeCDSUnits,t_trading_all_users_master.NSE_Exposure_Type,t_trading_all_users_master.NSE_Brokerage_Type,t_trading_all_users_master.Equity_brokerage_per_crore,t_trading_all_users_master.Intraday_Exposure_Margin_Equity,t_trading_all_users_master.Holding_Exposure_Margin_Equity,t_trading_all_users_master.Mcx_Brokerage_Type,t_trading_all_users_master.MCX_brokerage_per_crore,t_trading_all_users_master.",
				similer_syml,
				"_brokerage,t_trading_all_users_master.Mcx_Exposure_Type, t_trading_all_users_master.Intraday_Exposure_Margin_MCX, t_trading_all_users_master.Holding_Exposure_Margin_MCX, t_trading_all_users_master.MCX_Exposure_Lot_wise_",
				similer_syml,
				"_Intraday, t_trading_all_users_master.MCX_Exposure_Lot_wise_",
				similer_syml,
				"_Holding,t_trading_all_users_master.CDS_Brokerage_Type ,t_trading_all_users_master.CDS_brokerage_per_crore ,t_trading_all_users_master.CDS_Exposure_Type ,t_trading_all_users_master.Intraday_Exposure_Margin_CDS ,t_trading_all_users_master.Holding_Exposure_Margin_CDS  from t_trading_all_users_master where Id='",
				obj.UserId,
				"'  "
			}), "t_user_order");
			string NSE_Exposure_Type = "";
			string NSE_Brokerage_Type = "";
			string Equity_brokerage_per_crore = "";
			string Intraday_Exposure_Margin_Equity = "";
			string Holding_Exposure_Margin_Equity = "";
			string Mcx_Brokerage_Type = "";
			string MCX_brokerage_per_crore = "";
			string MCXsymb_brokerage = "";
			string Mcx_Exposure_Type = "";
			string Intraday_Exposure_Margin_MCX = "";
			string Holding_Exposure_Margin_MCX = "";
			string MCX_Exposure_Lot_wise_sym_Intraday = "";
			string MCX_Exposure_Lot_wise_sym_Holding = "";
			string CDS_Exposure_Type = "";
			string CDS_Brokerage_Type = "";
			string CDS_brokerage_per_crore = "";
			string Intraday_Exposure_Margin_CDS = "";
			string Holding_Exposure_Margin_CDS = "";
			decimal finalbrokerage = 0m;
			decimal final_intraday_exp = 0m;
			decimal final_indraday_holding = 0m;
			string TradeEquityUnits = "";
			string TradeMCXUnits = "";
			string TradeCDSUnits = "";
			if (dt.Rows.Count > 0)
			{
				TradeEquityUnits = dt.Rows[0]["TradeEquityUnits"].ToString();
				TradeMCXUnits = dt.Rows[0]["TradeMCXUnits"].ToString();
				TradeCDSUnits = dt.Rows[0]["TradeCDSUnits"].ToString();
				NSE_Exposure_Type = dt.Rows[0]["NSE_Exposure_Type"].ToString();
				NSE_Brokerage_Type = dt.Rows[0]["NSE_Brokerage_Type"].ToString();
				Equity_brokerage_per_crore = dt.Rows[0]["Equity_brokerage_per_crore"].ToString();
				Intraday_Exposure_Margin_Equity = dt.Rows[0]["Intraday_Exposure_Margin_Equity"].ToString();
				Holding_Exposure_Margin_Equity = dt.Rows[0]["Holding_Exposure_Margin_Equity"].ToString();
				Mcx_Brokerage_Type = dt.Rows[0]["Mcx_Brokerage_Type"].ToString();
				MCX_brokerage_per_crore = dt.Rows[0]["MCX_brokerage_per_crore"].ToString();
				MCXsymb_brokerage = dt.Rows[0][similer_syml + "_brokerage"].ToString();
				Mcx_Exposure_Type = dt.Rows[0]["Mcx_Exposure_Type"].ToString();
				Intraday_Exposure_Margin_MCX = dt.Rows[0]["Intraday_Exposure_Margin_MCX"].ToString();
				Holding_Exposure_Margin_MCX = dt.Rows[0]["Holding_Exposure_Margin_MCX"].ToString();
				MCX_Exposure_Lot_wise_sym_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_" + similer_syml + "_Intraday"].ToString();
				MCX_Exposure_Lot_wise_sym_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_" + similer_syml + "_Holding"].ToString();
				CDS_Exposure_Type = dt.Rows[0]["CDS_Exposure_Type"].ToString();
				CDS_Brokerage_Type = dt.Rows[0]["CDS_Brokerage_Type"].ToString();
				CDS_brokerage_per_crore = dt.Rows[0]["CDS_brokerage_per_crore"].ToString();
				Intraday_Exposure_Margin_CDS = dt.Rows[0]["Intraday_Exposure_Margin_CDS"].ToString();
				Holding_Exposure_Margin_CDS = dt.Rows[0]["Holding_Exposure_Margin_CDS"].ToString();
				if (dt.Rows[0]["totaldatabase_lot"].ToString() != "")
				{
					totaldatabase_lot = int.Parse(dt.Rows[0]["totaldatabase_lot"].ToString());
				}
				else
				{
					totaldatabase_lot = 0;
				}
			}
			if (totaldatabase_lot > int.Parse(obj.Lot))
			{
				DataTable pretable = Universal.SelectWithDS(string.Concat(new string[]
				{
					"select t_user_order.Id,selectedlotsize,DATE_FORMAT(OrderDate,'%Y-%m-%d') as OrderDate,OrderTime,OrderCategory,TokenNo,OrderPrice,t_user_order.SymbolType,Lot,t_universal_tradeble_tokens.buy,t_universal_tradeble_tokens.sell from t_user_order INNER JOIN t_universal_tradeble_tokens ON t_user_order.TokenNo=t_universal_tradeble_tokens.instrument_token  INNER JOIN  t_trading_all_users_master ON t_trading_all_users_master.Id=t_user_order.UserId where UserId='",
					obj.UserId,
					"' and TokenNo='",
					obj.TokenNo,
					"' and OrderCategory='",
					checkcond,
					"' and OrderStatus='Active' ORDER BY Id"
				}), "t_user_order");
				int incoming_lot = int.Parse(obj.Lot);
				for (int i = 0; i < pretable.Rows.Count; i++)
				{
					string Id = pretable.Rows[i]["Id"].ToString();
					string Lot = pretable.Rows[i]["Lot"].ToString();
					pretable.Rows[i]["buy"].ToString();
					pretable.Rows[i]["sell"].ToString();
					string OrderPrice = pretable.Rows[i]["OrderPrice"].ToString();
					string OrderCategory = pretable.Rows[i]["OrderCategory"].ToString();
					string OrderDate = pretable.Rows[i]["OrderDate"].ToString();
					string OrderTime = pretable.Rows[i]["OrderTime"].ToString();
					if (obj.SymbolType == "MCX")
					{
						if (Mcx_Brokerage_Type == "per_lot")
						{
							finalbrokerage = decimal.Parse(MCXsymb_brokerage);
						}
						else
						{
							finalbrokerage = (decimal.Parse(OrderPrice) + decimal.Parse(obj.OrderPrice)) * decimal.Parse(obj.selectedlotsize) / 10000000m * decimal.Parse(MCX_brokerage_per_crore);
						}
						if (Mcx_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(MCX_Exposure_Lot_wise_sym_Intraday);
							final_indraday_holding = decimal.Parse(MCX_Exposure_Lot_wise_sym_Holding);
						}
						else
						{
							final_intraday_exp = decimal.Parse(OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_MCX);
							final_indraday_holding = decimal.Parse(OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_MCX);
						}
					}
					else if (obj.SymbolType == "NSE")
					{
						if (NSE_Brokerage_Type == "per_lot")
						{
							finalbrokerage = decimal.Parse(Equity_brokerage_per_crore);
						}
						else
						{
							finalbrokerage = (decimal.Parse(OrderPrice) + decimal.Parse(obj.OrderPrice)) * decimal.Parse(obj.selectedlotsize) / 10000000m * decimal.Parse(Equity_brokerage_per_crore);
						}
						if (NSE_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(Intraday_Exposure_Margin_Equity);
							final_indraday_holding = decimal.Parse(Holding_Exposure_Margin_Equity);
						}
						else
						{
							final_intraday_exp = decimal.Parse(OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_Equity);
							final_indraday_holding = decimal.Parse(OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_Equity);
						}
					}
					else if (obj.SymbolType == "OPT")
					{
						if (CDS_Brokerage_Type == "per_lot")
						{
							finalbrokerage = decimal.Parse(CDS_brokerage_per_crore);
						}
						else
						{
							finalbrokerage = (decimal.Parse(OrderPrice) + decimal.Parse(obj.OrderPrice)) * decimal.Parse(obj.selectedlotsize) / 10000000m * decimal.Parse(CDS_brokerage_per_crore);
						}
						if (CDS_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(Intraday_Exposure_Margin_CDS);
							final_indraday_holding = decimal.Parse(Holding_Exposure_Margin_CDS);
						}
						else
						{
							final_intraday_exp = decimal.Parse(OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_CDS);
							final_indraday_holding = decimal.Parse(OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_CDS);
						}
					}
					if (incoming_lot > int.Parse(Lot))
					{
						int sublot = incoming_lot - int.Parse(Lot);
						decimal closevalue = 0m;
						string ActionCloseBy;
						string cmprice;
						if (obj.OrderCategory == "SELL")
						{
							ActionCloseBy = "Sold By Trader";
							cmprice = obj.OrderPrice;
							closevalue = decimal.Parse(cmprice) - decimal.Parse(OrderPrice);
						}
						else
						{
							ActionCloseBy = "Bought By Trader";
							cmprice = obj.OrderPrice;
							closevalue = decimal.Parse(OrderPrice) - decimal.Parse(cmprice);
						}
						decimal final_pl = Math.Round(closevalue * int.Parse(obj.selectedlotsize) * int.Parse(Lot), 1);
						DataTable brokeragetbl = Universal.SelectWithDS("select LedgerBalance,creditLimit from t_trading_all_users_master where Id='" + obj.UserId + "'", "t_trading_all_users_master");
						decimal LedgerBalance = 0m;
						if (brokeragetbl.Rows.Count > 0)
						{
							LedgerBalance = decimal.Parse(brokeragetbl.Rows[0][0].ToString());
							decimal.Parse(brokeragetbl.Rows[0][1].ToString());
						}
						finalbrokerage *= int.Parse(Lot);
						if (Universal.ExecuteNonQuery(string.Concat(new string[]
						{
							"update t_user_order set P_L='",
							final_pl.ToString(),
							"',OrderTypeClose='Market',ActionByClose='",
							ActionCloseBy,
							"',OrderStatus='Closed',Brokerage='",
							finalbrokerage.ToString(),
							"',BroughtBy='",
							cmprice,
							"',ClosedAt='",
							Universal.GetDate,
							"',ClosedTime='",
							Universal.GetTime,
							"',OrderRemark='OrderClosedFromAdminSettlement' where Id='",
							Id,
							"'"
						})) == 1)
						{
							string msg = string.Concat(new string[]
							{
								obj.UserName,
								"(",
								obj.UserId,
								") have ",
								obj.OrderType,
								" ",
								obj.Lot,
								" lots of ",
								obj.ScriptName,
								" at ",
								obj.OrderPrice,
								" Auto Closed due to settlement."
							});
							string[] array = new string[11];
							array[0] = "insert into t_action_master (Message,ActionDate,ActionTime,ActionFrom,IpAddress) values('";
							array[1] = msg;
							array[2] = "','";
							array[3] = Universal.GetDate;
							array[4] = "','";
							array[5] = Universal.GetTime;
							array[6] = "','";
							array[7] = obj.UserId;
							array[8] = "','";
							int num = 9;
							IPAddress ipaddress = externalIp;
							array[num] = ((ipaddress != null) ? ipaddress.ToString() : null);
							array[10] = "')";
							Universal.ExecuteNonQuery(string.Concat(array));
						}
						Universal.ExecuteNonQuery(string.Concat(new string[]
						{
							"update t_trading_all_users_master set LedgerBalance='",
							(LedgerBalance - finalbrokerage + final_pl).ToString(),
							"' where Id='",
							obj.UserId,
							"'"
						}));
						if (sublot > 0)
						{
							incoming_lot = sublot;
						}
					}
					else if (incoming_lot <= int.Parse(Lot))
					{
						int sublot2 = int.Parse(Lot) - incoming_lot;
						decimal closevalue2 = 0m;
						string CloseActionBy;
						string actiontype;
						string cmp2;
						if (obj.OrderCategory == "SELL")
						{
							CloseActionBy = "Sold By Trader";
							actiontype = "Bought By Trader";
							cmp2 = obj.OrderPrice;
							closevalue2 = decimal.Parse(cmp2) - decimal.Parse(OrderPrice);
						}
						else
						{
							CloseActionBy = "Bought By Trader";
							actiontype = "Sold By Trader";
							cmp2 = obj.OrderPrice;
							closevalue2 = decimal.Parse(OrderPrice) - decimal.Parse(cmp2);
						}
						decimal final_pl2 = Math.Round(closevalue2 * int.Parse(obj.selectedlotsize) * incoming_lot, 1);
						DataTable brokeragetbl2 = Universal.SelectWithDS("select LedgerBalance,creditLimit from t_trading_all_users_master where Id='" + obj.UserId + "'", "t_trading_all_users_master");
						decimal LedgerBalance2 = 0m;
						if (brokeragetbl2.Rows.Count > 0)
						{
							LedgerBalance2 = decimal.Parse(brokeragetbl2.Rows[0][0].ToString());
							decimal.Parse(brokeragetbl2.Rows[0][1].ToString());
						}
						if (sublot2 > 0)
						{
							decimal marginvalue = final_intraday_exp * sublot2;
							decimal holdmargn = final_indraday_holding * sublot2;
							if (Universal.ExecuteNonQuery(string.Concat(new string[]
							{
								"update t_user_order set Lot='",
								sublot2.ToString(),
								"',MarginUsed='",
								marginvalue.ToString(),
								"',HoldingMarginReq='",
								holdmargn.ToString(),
								"' where Id='",
								Id,
								"'"
							})) == 1)
							{
								string msg2 = string.Concat(new string[]
								{
									obj.UserName,
									"(",
									obj.UserId,
									") have ",
									obj.OrderType,
									" ",
									sublot2.ToString(),
									" lots of ",
									obj.ScriptName,
									" at ",
									obj.OrderPrice,
									". Trade has been modified by Autotrade settlement. "
								});
								string[] array2 = new string[11];
								array2[0] = "insert into t_action_master (Message,ActionDate,ActionTime,ActionFrom,IpAddress) values('";
								array2[1] = msg2;
								array2[2] = "','";
								array2[3] = Universal.GetDate;
								array2[4] = "','";
								array2[5] = Universal.GetTime;
								array2[6] = "','";
								array2[7] = obj.UserId;
								array2[8] = "','";
								int num2 = 9;
								IPAddress ipaddress2 = externalIp;
								array2[num2] = ((ipaddress2 != null) ? ipaddress2.ToString() : null);
								array2[10] = "')";
								Universal.ExecuteNonQuery(string.Concat(array2));
							}
						}
						decimal finalbrokerage2 = finalbrokerage * incoming_lot;
						Universal.ExecuteNonQuery(string.Concat(new string[]
						{
							"update t_trading_all_users_master set LedgerBalance='",
							(LedgerBalance2 - decimal.Parse(finalbrokerage2.ToString()) + final_pl2).ToString(),
							"' where Id='",
							obj.UserId,
							"'"
						}));
						decimal marginvalue2 = final_intraday_exp * incoming_lot;
						decimal holdmargn2 = final_indraday_holding * incoming_lot;
						if (Universal.ExecuteNonQuery(string.Concat(new string[]
						{
							"insert into t_user_order (selectedlotsize,OrderDate,OrderCategory,OrderTime,OrderNo,UserId,UserName,OrderType,ScriptName,TokenNo,ActionBy,OrderPrice,Lot,MarginUsed,HoldingMarginReq,OrderStatus,SymbolType,isstoplossorder,BroughtBy,P_L,Brokerage,ClosedAt,ClosedTime,OrderTypeClose,ActionByClose,OrderRemark) values('",
							obj.selectedlotsize,
							"','",
							OrderDate,
							"','",
							OrderCategory,
							"','",
							OrderTime,
							"','",
							obj.OrderNo,
							"','",
							obj.UserId,
							"','",
							obj.UserName,
							"','",
							obj.OrderType,
							"','",
							obj.ScriptName,
							"','",
							obj.TokenNo,
							"','",
							actiontype,
							"','",
							OrderPrice.Trim().Replace(" ", ""),
							"','",
							incoming_lot.ToString(),
							"','",
							marginvalue2.ToString(),
							"','",
							holdmargn2.ToString(),
							"','Closed','",
							obj.SymbolType,
							"','",
							obj.isstoplossorder,
							"','",
							cmp2,
							"','",
							final_pl2.ToString(),
							"','",
							finalbrokerage2.ToString(),
							"','",
							Universal.GetDate,
							"','",
							Universal.GetTime,
							"','Market','",
							CloseActionBy,
							"','OrderInsertedFromAdminSettlement')"
						})) == 1)
						{
							string msg3 = string.Concat(new string[]
							{
								obj.UserName,
								"(",
								obj.UserId,
								") have ",
								OrderCategory,
								" ",
								incoming_lot.ToString(),
								" lots of ",
								obj.ScriptName,
								" at ",
								OrderPrice.Trim().Replace(" ", ""),
								". Auto Closed due to settlement."
							});
							string[] array3 = new string[11];
							array3[0] = "insert into t_action_master (Message,ActionDate,ActionTime,ActionFrom,IpAddress) values('";
							array3[1] = msg3;
							array3[2] = "','";
							array3[3] = Universal.GetDate;
							array3[4] = "','";
							array3[5] = Universal.GetTime;
							array3[6] = "','";
							array3[7] = obj.UserId;
							array3[8] = "','";
							int num3 = 9;
							IPAddress ipaddress3 = externalIp;
							array3[num3] = ((ipaddress3 != null) ? ipaddress3.ToString() : null);
							array3[10] = "')";
							Universal.ExecuteNonQuery(string.Concat(array3));
						}
						if (sublot2 == 0)
						{
							Universal.ExecuteNonQuery("delete from t_user_order where Id='" + Id + "'");
							return;
						}
						return;
					}
				}
				return;
			}
			if (totaldatabase_lot <= int.Parse(obj.Lot))
			{
				DataTable pretable2 = Universal.SelectWithDS(string.Concat(new string[]
				{
					"select t_user_order.Id,selectedlotsize,OrderCategory,TokenNo,OrderPrice,t_user_order.SymbolType,Lot,t_universal_tradeble_tokens.buy,t_universal_tradeble_tokens.sell from t_user_order INNER JOIN t_universal_tradeble_tokens ON t_user_order.TokenNo=t_universal_tradeble_tokens.instrument_token where UserId='",
					obj.UserId,
					"' and TokenNo='",
					obj.TokenNo,
					"' and OrderCategory='",
					checkcond,
					"' and OrderStatus='Active' ORDER BY Lot"
				}), "t_user_order");
				int defflot = int.Parse(obj.Lot) - totaldatabase_lot;
				DataTable brokeragetbl3 = Universal.SelectWithDS("select LedgerBalance,creditLimit from t_trading_all_users_master where Id='" + obj.UserId + "'", "t_trading_all_users_master");
				decimal LedgerBalance3 = 0m;
				if (brokeragetbl3.Rows.Count > 0)
				{
					LedgerBalance3 = decimal.Parse(brokeragetbl3.Rows[0][0].ToString());
					decimal.Parse(brokeragetbl3.Rows[0][1].ToString());
				}
				for (int j = 0; j < pretable2.Rows.Count; j++)
				{
					string Id2 = pretable2.Rows[j]["Id"].ToString();
					string Lot2 = pretable2.Rows[j]["Lot"].ToString();
					pretable2.Rows[j]["buy"].ToString();
					pretable2.Rows[j]["sell"].ToString();
					string selectedlotsize = pretable2.Rows[j]["selectedlotsize"].ToString();
					string OrderPrice2 = pretable2.Rows[j]["OrderPrice"].ToString();
					if (obj.SymbolType == "MCX")
					{
						if (Mcx_Brokerage_Type == "per_lot")
						{
							finalbrokerage = decimal.Parse(MCXsymb_brokerage);
						}
						else
						{
							finalbrokerage = (decimal.Parse(OrderPrice2) + decimal.Parse(obj.OrderPrice)) * decimal.Parse(obj.selectedlotsize) / 10000000m * decimal.Parse(MCX_brokerage_per_crore);
						}
						if (Mcx_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(MCX_Exposure_Lot_wise_sym_Intraday);
							final_indraday_holding = decimal.Parse(MCX_Exposure_Lot_wise_sym_Holding);
						}
						else
						{
							final_intraday_exp = decimal.Parse(OrderPrice2) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_MCX);
							final_indraday_holding = decimal.Parse(OrderPrice2) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_MCX);
						}
					}
					else if (obj.SymbolType == "NSE")
					{
						if (NSE_Brokerage_Type == "per_lot")
						{
							finalbrokerage = decimal.Parse(Equity_brokerage_per_crore);
						}
						else
						{
							finalbrokerage = (decimal.Parse(OrderPrice2) + decimal.Parse(obj.OrderPrice)) * decimal.Parse(obj.selectedlotsize) / 10000000m * decimal.Parse(Equity_brokerage_per_crore);
						}
						if (NSE_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(Intraday_Exposure_Margin_Equity);
							final_indraday_holding = decimal.Parse(Holding_Exposure_Margin_Equity);
						}
						else
						{
							final_intraday_exp = decimal.Parse(OrderPrice2) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_Equity);
							final_indraday_holding = decimal.Parse(OrderPrice2) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_Equity);
						}
					}
					else if (obj.SymbolType == "OPT")
					{
						if (CDS_Brokerage_Type == "per_lot")
						{
							finalbrokerage = decimal.Parse(CDS_brokerage_per_crore);
						}
						else
						{
							finalbrokerage = (decimal.Parse(OrderPrice2) + decimal.Parse(obj.OrderPrice)) * decimal.Parse(obj.selectedlotsize) / 10000000m * decimal.Parse(CDS_brokerage_per_crore);
						}
						if (CDS_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(Intraday_Exposure_Margin_CDS);
							final_indraday_holding = decimal.Parse(Holding_Exposure_Margin_CDS);
						}
						else
						{
							final_intraday_exp = decimal.Parse(OrderPrice2) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_CDS);
							final_indraday_holding = decimal.Parse(OrderPrice2) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_CDS);
						}
					}
					decimal closevalue3 = 0m;
					string ActionCloseBy2;
					string cmprice2;
					if (obj.OrderCategory == "SELL")
					{
						ActionCloseBy2 = "Sold By Trader";
						cmprice2 = obj.OrderPrice;
						closevalue3 = decimal.Parse(cmprice2) - decimal.Parse(OrderPrice2);
					}
					else
					{
						ActionCloseBy2 = "Bought By Trader";
						cmprice2 = obj.OrderPrice;
						closevalue3 = decimal.Parse(OrderPrice2) - decimal.Parse(cmprice2);
					}
					decimal final_pl3 = Math.Round(closevalue3 * int.Parse(selectedlotsize) * int.Parse(Lot2), 1);
					decimal finalbrokerage3 = finalbrokerage * int.Parse(Lot2);
					if (Universal.ExecuteNonQuery(string.Concat(new string[]
					{
						"update t_user_order set P_L='",
						final_pl3.ToString(),
						"',OrderTypeClose='Market',ActionByClose='",
						ActionCloseBy2,
						"',OrderStatus='Closed',Brokerage='",
						finalbrokerage3.ToString(),
						"',BroughtBy='",
						cmprice2,
						"',ClosedAt='",
						Universal.GetDate,
						"',ClosedTime='",
						Universal.GetTime,
						"',OrderRemark='OrderClosedFromAdminSettlement' where Id='",
						Id2,
						"'"
					})) == 1)
					{
						string msg4 = string.Concat(new string[]
						{
							obj.UserName,
							"(",
							obj.UserId,
							") have ",
							obj.OrderCategory,
							" ",
							obj.Lot,
							" lots of ",
							obj.ScriptName,
							" at ",
							obj.OrderPrice,
							". Auto Closed due to settlement."
						});
						string[] array4 = new string[11];
						array4[0] = "insert into t_action_master (Message,ActionDate,ActionTime,ActionFrom,IpAddress) values('";
						array4[1] = msg4;
						array4[2] = "','";
						array4[3] = Universal.GetDate;
						array4[4] = "','";
						array4[5] = Universal.GetTime;
						array4[6] = "','";
						array4[7] = obj.UserId;
						array4[8] = "','";
						int num4 = 9;
						IPAddress ipaddress4 = externalIp;
						array4[num4] = ((ipaddress4 != null) ? ipaddress4.ToString() : null);
						array4[10] = "')";
						Universal.ExecuteNonQuery(string.Concat(array4));
					}
					Universal.ExecuteNonQuery(string.Concat(new string[]
					{
						"update t_trading_all_users_master set LedgerBalance='",
						(LedgerBalance3 - decimal.Parse(finalbrokerage3.ToString()) + final_pl3).ToString(),
						"' where Id='",
						obj.UserId,
						"'"
					}));
				}
				if (defflot > 0)
				{
					if (obj.SymbolType == "MCX")
					{
						if (Mcx_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(MCX_Exposure_Lot_wise_sym_Intraday);
							final_indraday_holding = decimal.Parse(MCX_Exposure_Lot_wise_sym_Holding);
						}
						else
						{
							if (TradeMCXUnits == "true")
							{
								obj.selectedlotsize = "1";
							}
							final_intraday_exp = decimal.Parse(obj.OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_MCX);
							final_indraday_holding = decimal.Parse(obj.OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_MCX);
						}
					}
					else if (obj.SymbolType == "NSE")
					{
						if (NSE_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(Intraday_Exposure_Margin_Equity);
							final_indraday_holding = decimal.Parse(Holding_Exposure_Margin_Equity);
						}
						else
						{
							if (TradeEquityUnits == "true")
							{
								obj.selectedlotsize = "1";
							}
							final_intraday_exp = decimal.Parse(obj.OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_Equity);
							final_indraday_holding = decimal.Parse(obj.OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_Equity);
						}
					}
					else if (obj.SymbolType == "OPT")
					{
						if (CDS_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(Intraday_Exposure_Margin_CDS);
							final_indraday_holding = decimal.Parse(Holding_Exposure_Margin_CDS);
						}
						else
						{
							if (TradeCDSUnits == "true")
							{
								obj.selectedlotsize = "1";
							}
							final_intraday_exp = decimal.Parse(obj.OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_CDS);
							final_indraday_holding = decimal.Parse(obj.OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_CDS);
						}
					}
					decimal marginvalue3 = final_intraday_exp * defflot;
					decimal holdmargn3 = final_indraday_holding * defflot;
					Universal.ExecuteNonQuery(string.Concat(new string[]
					{
						"insert into t_user_order (selectedlotsize,OrderDate,OrderCategory,OrderTime,OrderNo,UserId,UserName,OrderType,ScriptName,TokenNo,ActionBy,OrderPrice,Lot,MarginUsed,HoldingMarginReq,OrderStatus,SymbolType,isstoplossorder,OrderRemark) values('",
						obj.selectedlotsize,
						"','",
						Universal.GetDate,
						"','",
						obj.OrderCategory,
						"','",
						obj.OrderTime,
						"','",
						obj.OrderNo,
						"','",
						obj.UserId,
						"','",
						obj.UserName,
						"','",
						obj.OrderType,
						"','",
						obj.ScriptName,
						"','",
						obj.TokenNo,
						"','",
						obj.ActionType,
						"','",
						obj.OrderPrice.Trim().Replace(" ", ""),
						"','",
						defflot.ToString(),
						"','",
						marginvalue3.ToString(),
						"','",
						holdmargn3.ToString(),
						"','Active','",
						obj.SymbolType,
						"','",
						obj.isstoplossorder,
						"','OrderInsertedFromAdminSettlement')"
					}));
				}
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00018424 File Offset: 0x00016624
		[HttpPost]
		public void tradesettlementpost_pendingtoactive(t_order_master obj)
		{
			(decimal.Parse(obj.OrderPrice) * decimal.Parse(obj.Lot)).ToString();
			IPAddress externalIp = IPAddress.Parse(Universal.devip);
			string userId = obj.UserId;
			string Act_by;
			string checkcond;
			if (obj.OrderCategory == "SELL")
			{
				obj.ActionType = "Sold By Trader";
				Act_by = "Sold By Trader";
				checkcond = "BUY";
			}
			else
			{
				obj.ActionType = "Bought By Trader";
				Act_by = "Bought By Trader";
				checkcond = "SELL";
			}
			string similer_syml;
			if (obj.SymbolType == "MCX")
			{
				string[] symarr = obj.ScriptName.Split(new char[]
				{
					'_'
				});
				similer_syml = this.setsymbol(symarr[0].Trim());
			}
			else
			{
				similer_syml = "GOLD";
			}
			int totaldatabase_lot = 0;
			DataTable dt = Universal.SelectWithDS(string.Concat(new string[]
			{
				"select (select SUM(Lot) from t_user_order where UserId='",
				obj.UserId,
				"' and OrderCategory='",
				checkcond,
				"' and OrderStatus='Active' and TokenNo='",
				obj.TokenNo,
				"') as totaldatabase_lot,t_trading_all_users_master.NSE_Exposure_Type,t_trading_all_users_master.NSE_Brokerage_Type,t_trading_all_users_master.Equity_brokerage_per_crore,t_trading_all_users_master.Intraday_Exposure_Margin_Equity,t_trading_all_users_master.Holding_Exposure_Margin_Equity,t_trading_all_users_master.Mcx_Brokerage_Type,t_trading_all_users_master.MCX_brokerage_per_crore,t_trading_all_users_master.",
				similer_syml,
				"_brokerage,t_trading_all_users_master.Mcx_Exposure_Type, t_trading_all_users_master.Intraday_Exposure_Margin_MCX, t_trading_all_users_master.Holding_Exposure_Margin_MCX, t_trading_all_users_master.MCX_Exposure_Lot_wise_",
				similer_syml,
				"_Intraday, t_trading_all_users_master.MCX_Exposure_Lot_wise_",
				similer_syml,
				"_Holding,t_trading_all_users_master.CDS_Brokerage_Type ,t_trading_all_users_master.CDS_brokerage_per_crore ,t_trading_all_users_master.CDS_Exposure_Type ,t_trading_all_users_master.Intraday_Exposure_Margin_CDS ,t_trading_all_users_master.Holding_Exposure_Margin_CDS  from t_trading_all_users_master where Id='",
				obj.UserId,
				"' "
			}), "t_user_order");
			string NSE_Exposure_Type = "";
			string NSE_Brokerage_Type = "";
			string Equity_brokerage_per_crore = "";
			string Intraday_Exposure_Margin_Equity = "";
			string Holding_Exposure_Margin_Equity = "";
			string Mcx_Brokerage_Type = "";
			string MCX_brokerage_per_crore = "";
			string MCXsymb_brokerage = "";
			string Mcx_Exposure_Type = "";
			string Intraday_Exposure_Margin_MCX = "";
			string Holding_Exposure_Margin_MCX = "";
			string MCX_Exposure_Lot_wise_sym_Intraday = "";
			string MCX_Exposure_Lot_wise_sym_Holding = "";
			string CDS_Exposure_Type = "";
			string CDS_Brokerage_Type = "";
			string CDS_brokerage_per_crore = "";
			string Intraday_Exposure_Margin_CDS = "";
			string Holding_Exposure_Margin_CDS = "";
			decimal finalbrokerage = 0m;
			decimal final_intraday_exp = 0m;
			decimal final_indraday_holding = 0m;
			if (dt.Rows.Count > 0)
			{
				NSE_Exposure_Type = dt.Rows[0]["NSE_Exposure_Type"].ToString();
				NSE_Brokerage_Type = dt.Rows[0]["NSE_Brokerage_Type"].ToString();
				Equity_brokerage_per_crore = dt.Rows[0]["Equity_brokerage_per_crore"].ToString();
				Intraday_Exposure_Margin_Equity = dt.Rows[0]["Intraday_Exposure_Margin_Equity"].ToString();
				Holding_Exposure_Margin_Equity = dt.Rows[0]["Holding_Exposure_Margin_Equity"].ToString();
				Mcx_Brokerage_Type = dt.Rows[0]["Mcx_Brokerage_Type"].ToString();
				MCX_brokerage_per_crore = dt.Rows[0]["MCX_brokerage_per_crore"].ToString();
				MCXsymb_brokerage = dt.Rows[0][similer_syml + "_brokerage"].ToString();
				Mcx_Exposure_Type = dt.Rows[0]["Mcx_Exposure_Type"].ToString();
				Intraday_Exposure_Margin_MCX = dt.Rows[0]["Intraday_Exposure_Margin_MCX"].ToString();
				Holding_Exposure_Margin_MCX = dt.Rows[0]["Holding_Exposure_Margin_MCX"].ToString();
				MCX_Exposure_Lot_wise_sym_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_" + similer_syml + "_Intraday"].ToString();
				MCX_Exposure_Lot_wise_sym_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_" + similer_syml + "_Holding"].ToString();
				CDS_Exposure_Type = dt.Rows[0]["CDS_Exposure_Type"].ToString();
				CDS_Brokerage_Type = dt.Rows[0]["CDS_Brokerage_Type"].ToString();
				CDS_brokerage_per_crore = dt.Rows[0]["CDS_brokerage_per_crore"].ToString();
				Intraday_Exposure_Margin_CDS = dt.Rows[0]["Intraday_Exposure_Margin_CDS"].ToString();
				Holding_Exposure_Margin_CDS = dt.Rows[0]["Holding_Exposure_Margin_CDS"].ToString();
				if (dt.Rows.Count > 0)
				{
					if (dt.Rows[0]["totaldatabase_lot"].ToString() != "")
					{
						totaldatabase_lot = int.Parse(dt.Rows[0]["totaldatabase_lot"].ToString());
					}
					else
					{
						totaldatabase_lot = 0;
					}
				}
			}
			if (totaldatabase_lot >= int.Parse(obj.Lot))
			{
				DataTable pretable = Universal.SelectWithDS(string.Concat(new string[]
				{
					"select t_user_order.Id,t_user_order.OrderType,selectedlotsize,DATE_FORMAT(OrderDate,'%Y-%m-%d') as OrderDate,OrderTime,OrderCategory,TokenNo,OrderPrice,t_user_order.SymbolType,Lot,t_universal_tradeble_tokens.buy,t_universal_tradeble_tokens.sell from t_user_order INNER JOIN t_universal_tradeble_tokens ON t_user_order.TokenNo=t_universal_tradeble_tokens.instrument_token where UserId='",
					obj.UserId,
					"' and TokenNo='",
					obj.TokenNo,
					"' and OrderCategory='",
					checkcond,
					"' and OrderStatus='Active' ORDER BY Id"
				}), "temp2");
				int incoming_lot = int.Parse(obj.Lot);
				for (int i = 0; i < pretable.Rows.Count; i++)
				{
					string Id = pretable.Rows[i]["Id"].ToString();
					string Lot = pretable.Rows[i]["Lot"].ToString();
					pretable.Rows[i]["buy"].ToString();
					pretable.Rows[i]["sell"].ToString();
					string OrderPrice = pretable.Rows[i]["OrderPrice"].ToString();
					string OrderCategory = pretable.Rows[i]["OrderCategory"].ToString();
					string OrderDate = pretable.Rows[i]["OrderDate"].ToString();
					string OrderTime = pretable.Rows[i]["OrderTime"].ToString();
					string OrderType = pretable.Rows[i]["OrderType"].ToString();
					if (obj.SymbolType == "MCX")
					{
						if (Mcx_Brokerage_Type == "per_lot")
						{
							finalbrokerage = decimal.Parse(MCXsymb_brokerage);
						}
						else
						{
							finalbrokerage = (decimal.Parse(OrderPrice) + decimal.Parse(obj.OrderPrice)) * decimal.Parse(obj.selectedlotsize) / 10000000m * decimal.Parse(MCX_brokerage_per_crore);
						}
						if (Mcx_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(MCX_Exposure_Lot_wise_sym_Intraday);
							final_indraday_holding = decimal.Parse(MCX_Exposure_Lot_wise_sym_Holding);
						}
						else
						{
							final_intraday_exp = decimal.Parse(OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_MCX);
							final_indraday_holding = decimal.Parse(OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_MCX);
						}
					}
					else if (obj.SymbolType == "NSE")
					{
						if (NSE_Brokerage_Type == "per_lot")
						{
							finalbrokerage = decimal.Parse(Equity_brokerage_per_crore);
						}
						else
						{
							finalbrokerage = (decimal.Parse(OrderPrice) + decimal.Parse(obj.OrderPrice)) * decimal.Parse(obj.selectedlotsize) / 10000000m * decimal.Parse(Equity_brokerage_per_crore);
						}
						if (NSE_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(Intraday_Exposure_Margin_Equity);
							final_indraday_holding = decimal.Parse(Holding_Exposure_Margin_Equity);
						}
						else
						{
							final_intraday_exp = decimal.Parse(OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_Equity);
							final_indraday_holding = decimal.Parse(OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_Equity);
						}
					}
					else if (obj.SymbolType == "OPT")
					{
						if (CDS_Brokerage_Type == "per_lot")
						{
							finalbrokerage = decimal.Parse(CDS_brokerage_per_crore);
						}
						else
						{
							finalbrokerage = (decimal.Parse(OrderPrice) + decimal.Parse(obj.OrderPrice)) * decimal.Parse(obj.selectedlotsize) / 10000000m * decimal.Parse(CDS_brokerage_per_crore);
						}
						if (CDS_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(Intraday_Exposure_Margin_CDS);
							final_indraday_holding = decimal.Parse(Holding_Exposure_Margin_CDS);
						}
						else
						{
							final_intraday_exp = decimal.Parse(OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_CDS);
							final_indraday_holding = decimal.Parse(OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_CDS);
						}
					}
					if (incoming_lot > int.Parse(Lot) && incoming_lot != 0)
					{
						decimal closevalue = 0m;
						string ActionCloseBy;
						string cmprice;
						if (obj.OrderCategory == "SELL")
						{
							ActionCloseBy = "Sold By Trader";
							cmprice = obj.OrderPrice;
							closevalue = decimal.Parse(cmprice) - decimal.Parse(OrderPrice);
						}
						else
						{
							ActionCloseBy = "Bought By Trader";
							cmprice = obj.OrderPrice;
							closevalue = decimal.Parse(OrderPrice) - decimal.Parse(cmprice);
						}
						decimal final_pl = Math.Round(closevalue * int.Parse(obj.selectedlotsize) * int.Parse(Lot), 1);
						DataTable brokeragetbl = Universal.SelectWithDS("select LedgerBalance,creditLimit from t_trading_all_users_master where Id='" + obj.UserId + "'", "temp3");
						decimal LedgerBalance = 0m;
						if (brokeragetbl.Rows.Count > 0)
						{
							LedgerBalance = decimal.Parse(brokeragetbl.Rows[0][0].ToString());
							decimal.Parse(brokeragetbl.Rows[0][1].ToString());
						}
						decimal finalbrokerage2 = int.Parse(Lot) * finalbrokerage;
						if (Universal.ExecuteNonQuery(string.Concat(new string[]
						{
							"update t_user_order set P_L='",
							final_pl.ToString(),
							"',OrderTypeClose='Limit',ActionByClose='",
							ActionCloseBy,
							"',OrderStatus='Closed',Brokerage='",
							finalbrokerage2.ToString(),
							"',BroughtBy='",
							cmprice,
							"',ClosedAt='",
							Universal.GetDate,
							"',ClosedTime='",
							Universal.GetTime,
							"',OrderRemark='ClosedFromClientPartialView' where Id='",
							Id,
							"'"
						})) == 1)
						{
							string msg = string.Concat(new string[]
							{
								obj.UserName,
								"(",
								obj.UserId,
								") have ",
								OrderType,
								" ",
								Lot,
								" lots of ",
								obj.ScriptName,
								" at ",
								obj.OrderPrice,
								" Auto Closed due to settlement."
							});
							string[] array = new string[11];
							array[0] = "insert into t_action_master (Message,ActionDate,ActionTime,ActionFrom,IpAddress) values('";
							array[1] = msg;
							array[2] = "','";
							array[3] = Universal.GetDate;
							array[4] = "','";
							array[5] = Universal.GetTime;
							array[6] = "','";
							array[7] = obj.UserId;
							array[8] = "','";
							int num = 9;
							IPAddress ipaddress = externalIp;
							array[num] = ((ipaddress != null) ? ipaddress.ToString() : null);
							array[10] = "')";
							Universal.ExecuteNonQuery(string.Concat(array));
						}
						Universal.ExecuteNonQuery(string.Concat(new string[]
						{
							"update t_trading_all_users_master set LedgerBalance='",
							(LedgerBalance - decimal.Parse(finalbrokerage2.ToString()) + final_pl).ToString(),
							"' where Id='",
							obj.UserId,
							"'"
						}));
						incoming_lot -= int.Parse(Lot);
					}
					else if (incoming_lot <= int.Parse(Lot) && incoming_lot != 0)
					{
						decimal closevalue2 = 0m;
						string CloseActionBy;
						string AdminActionBy;
						string cmp2;
						if (obj.OrderCategory == "SELL")
						{
							CloseActionBy = "Sold By Trader";
							AdminActionBy = "Bought By Trader";
							cmp2 = obj.OrderPrice;
							closevalue2 = decimal.Parse(cmp2) - decimal.Parse(OrderPrice);
						}
						else
						{
							AdminActionBy = "Sold By Trader";
							CloseActionBy = "Bought By Trader";
							cmp2 = obj.OrderPrice;
							closevalue2 = decimal.Parse(OrderPrice) - decimal.Parse(cmp2);
						}
						decimal final_pl2 = Math.Round(closevalue2 * int.Parse(obj.selectedlotsize) * incoming_lot, 1);
						DataTable brokeragetbl2 = Universal.SelectWithDS("select LedgerBalance,creditLimit from t_trading_all_users_master where Id='" + obj.UserId + "'", "t_trading_all_users_master");
						decimal LedgerBalance2 = 0m;
						if (brokeragetbl2.Rows.Count > 0)
						{
							LedgerBalance2 = decimal.Parse(brokeragetbl2.Rows[0][0].ToString());
							decimal.Parse(brokeragetbl2.Rows[0][1].ToString());
						}
						decimal finalbrokerage3 = incoming_lot * finalbrokerage;
						Universal.ExecuteNonQuery(string.Concat(new string[]
						{
							"update t_trading_all_users_master set LedgerBalance='",
							(LedgerBalance2 - decimal.Parse(finalbrokerage3.ToString()) + final_pl2).ToString(),
							"' where Id='",
							obj.UserId,
							"'"
						}));
						decimal marginvalue = final_intraday_exp * incoming_lot;
						decimal holdmargn = final_indraday_holding * incoming_lot;
						string ordern = new Random().Next().ToString();
						if (Universal.ExecuteNonQuery(string.Concat(new string[]
						{
							"insert into t_user_order (selectedlotsize,OrderDate,OrderCategory,OrderTime,OrderNo,UserId,UserName,OrderType,ScriptName,TokenNo,ActionBy,OrderPrice,Lot,MarginUsed,HoldingMarginReq,OrderStatus,SymbolType,isstoplossorder,BroughtBy,P_L,Brokerage,ClosedAt,ClosedTime,OrderTypeClose,ActionByClose,OrderRemark) values('",
							obj.selectedlotsize,
							"','",
							OrderDate,
							"','",
							OrderCategory,
							"','",
							OrderTime,
							"','",
							ordern,
							"','",
							obj.UserId,
							"','",
							obj.UserName,
							"','",
							OrderType,
							"','",
							obj.ScriptName,
							"','",
							obj.TokenNo,
							"','",
							AdminActionBy,
							"','",
							OrderPrice.Trim().Replace(" ", ""),
							"','",
							incoming_lot.ToString(),
							"','",
							marginvalue.ToString(),
							"','",
							holdmargn.ToString(),
							"','Closed','",
							obj.SymbolType,
							"','",
							obj.isstoplossorder,
							"','",
							cmp2,
							"','",
							final_pl2.ToString(),
							"','",
							finalbrokerage3.ToString(),
							"','",
							Universal.GetDate,
							"','",
							Universal.GetTime,
							"','Limit','",
							CloseActionBy,
							"','OrderInsertFromClientPartial')"
						})) == 1)
						{
							string msg2 = string.Concat(new string[]
							{
								obj.UserName,
								"(",
								obj.UserId,
								") have ",
								OrderCategory,
								" ",
								incoming_lot.ToString(),
								" lots of ",
								obj.ScriptName,
								" at ",
								OrderPrice.Trim().Replace(" ", ""),
								". Auto Closed due to settlement."
							});
							string[] array2 = new string[11];
							array2[0] = "insert into t_action_master (Message,ActionDate,ActionTime,ActionFrom,IpAddress) values('";
							array2[1] = msg2;
							array2[2] = "','";
							array2[3] = Universal.GetDate;
							array2[4] = "','";
							array2[5] = Universal.GetTime;
							array2[6] = "','";
							array2[7] = obj.UserId;
							array2[8] = "','";
							int num2 = 9;
							IPAddress ipaddress2 = externalIp;
							array2[num2] = ((ipaddress2 != null) ? ipaddress2.ToString() : null);
							array2[10] = "')";
							Universal.ExecuteNonQuery(string.Concat(array2));
						}
						int sublot = int.Parse(Lot) - incoming_lot;
						marginvalue = final_intraday_exp * sublot;
						holdmargn = final_indraday_holding * sublot;
						if (sublot > 0)
						{
							decimal marginvalue2 = final_intraday_exp * sublot;
							decimal holdmargn2 = final_indraday_holding * sublot;
							if (Universal.ExecuteNonQuery(string.Concat(new string[]
							{
								"update t_user_order set Lot='",
								sublot.ToString(),
								"',MarginUsed='",
								marginvalue2.ToString(),
								"',HoldingMarginReq='",
								holdmargn2.ToString(),
								"' where Id='",
								Id,
								"'"
							})) == 1)
							{
								string msg3 = string.Concat(new string[]
								{
									obj.UserName,
									"(",
									obj.UserId,
									") have ",
									OrderType,
									" ",
									sublot.ToString(),
									" lots of ",
									obj.ScriptName,
									" at ",
									OrderPrice,
									". Trade has been modified by Autotrade settlement. "
								});
								string[] array3 = new string[11];
								array3[0] = "insert into t_action_master (Message,ActionDate,ActionTime,ActionFrom,IpAddress) values('";
								array3[1] = msg3;
								array3[2] = "','";
								array3[3] = Universal.GetDate;
								array3[4] = "','";
								array3[5] = Universal.GetTime;
								array3[6] = "','";
								array3[7] = obj.UserId;
								array3[8] = "','";
								int num3 = 9;
								IPAddress ipaddress3 = externalIp;
								array3[num3] = ((ipaddress3 != null) ? ipaddress3.ToString() : null);
								array3[10] = "')";
								Universal.ExecuteNonQuery(string.Concat(array3));
								Universal.ExecuteNonQuery("delete from t_user_order where Id='" + obj.Id + "'");
							}
							return;
						}
						Universal.ExecuteNonQuery(string.Concat(new string[]
						{
							"delete from t_user_order where Id='",
							obj.Id,
							"' or Id='",
							Id,
							"'"
						}));
						return;
					}
				}
				return;
			}
			if (totaldatabase_lot < int.Parse(obj.Lot))
			{
				DataTable pretable2 = Universal.SelectWithDS(string.Concat(new string[]
				{
					"select t_user_order.Id,selectedlotsize,OrderCategory,TokenNo,OrderPrice,t_user_order.SymbolType,Lot,t_universal_tradeble_tokens.buy,t_universal_tradeble_tokens.sell from t_user_order INNER JOIN t_universal_tradeble_tokens ON t_user_order.TokenNo=t_universal_tradeble_tokens.instrument_token where UserId='",
					obj.UserId,
					"' and TokenNo='",
					obj.TokenNo,
					"' and OrderCategory='",
					checkcond,
					"' and OrderStatus='Active' ORDER BY Lot"
				}), "t_user_order");
				DataTable brokeragetbl3 = Universal.SelectWithDS("select LedgerBalance,creditLimit from t_trading_all_users_master where Id='" + obj.UserId + "'", "t_trading_all_users_master");
				decimal LedgerBalance3 = 0m;
				if (brokeragetbl3.Rows.Count > 0)
				{
					LedgerBalance3 = decimal.Parse(brokeragetbl3.Rows[0][0].ToString());
					decimal.Parse(brokeragetbl3.Rows[0][1].ToString());
				}
				for (int j = 0; j < pretable2.Rows.Count; j++)
				{
					string Id2 = pretable2.Rows[j]["Id"].ToString();
					string Lot2 = pretable2.Rows[j]["Lot"].ToString();
					pretable2.Rows[j]["buy"].ToString();
					pretable2.Rows[j]["sell"].ToString();
					string selectedlotsize = pretable2.Rows[j]["selectedlotsize"].ToString();
					string OrderPrice2 = pretable2.Rows[j]["OrderPrice"].ToString();
					string OrderCategory2 = pretable2.Rows[j]["OrderCategory"].ToString();
					if (obj.SymbolType == "MCX")
					{
						if (Mcx_Brokerage_Type == "per_lot")
						{
							finalbrokerage = decimal.Parse(MCXsymb_brokerage);
						}
						else
						{
							finalbrokerage = (decimal.Parse(OrderPrice2) + decimal.Parse(obj.OrderPrice)) * decimal.Parse(obj.selectedlotsize) / 10000000m * decimal.Parse(MCX_brokerage_per_crore);
						}
						if (Mcx_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(MCX_Exposure_Lot_wise_sym_Intraday);
							final_indraday_holding = decimal.Parse(MCX_Exposure_Lot_wise_sym_Holding);
						}
						else
						{
							final_intraday_exp = decimal.Parse(OrderPrice2) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_MCX);
							final_indraday_holding = decimal.Parse(OrderPrice2) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_MCX);
						}
					}
					else if (obj.SymbolType == "NSE")
					{
						if (NSE_Brokerage_Type == "per_lot")
						{
							finalbrokerage = decimal.Parse(Equity_brokerage_per_crore);
						}
						else
						{
							finalbrokerage = (decimal.Parse(OrderPrice2) + decimal.Parse(obj.OrderPrice)) * decimal.Parse(obj.selectedlotsize) / 10000000m * decimal.Parse(Equity_brokerage_per_crore);
						}
						if (NSE_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(Intraday_Exposure_Margin_Equity);
							final_indraday_holding = decimal.Parse(Holding_Exposure_Margin_Equity);
						}
						else
						{
							final_intraday_exp = decimal.Parse(OrderPrice2) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_Equity);
							final_indraday_holding = decimal.Parse(OrderPrice2) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_Equity);
						}
					}
					else if (obj.SymbolType == "OPT")
					{
						if (CDS_Brokerage_Type == "per_lot")
						{
							finalbrokerage = decimal.Parse(CDS_brokerage_per_crore);
						}
						else
						{
							finalbrokerage = (decimal.Parse(OrderPrice2) + decimal.Parse(obj.OrderPrice)) * decimal.Parse(obj.selectedlotsize) / 10000000m * decimal.Parse(CDS_brokerage_per_crore);
						}
						if (CDS_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(Intraday_Exposure_Margin_CDS);
							final_indraday_holding = decimal.Parse(Holding_Exposure_Margin_CDS);
						}
						else
						{
							final_intraday_exp = decimal.Parse(OrderPrice2) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_CDS);
							final_indraday_holding = decimal.Parse(OrderPrice2) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_CDS);
						}
					}
					decimal closevalue3 = 0m;
					string ActionCloseBy2;
					string cmprice2;
					if (obj.OrderCategory == "SELL")
					{
						ActionCloseBy2 = "Sold By Trader";
						cmprice2 = obj.OrderPrice;
						closevalue3 = decimal.Parse(cmprice2) - decimal.Parse(OrderPrice2);
					}
					else
					{
						ActionCloseBy2 = "Bought By Trader";
						cmprice2 = obj.OrderPrice;
						closevalue3 = decimal.Parse(OrderPrice2) - decimal.Parse(cmprice2);
					}
					decimal final_pl3 = Math.Round(closevalue3 * int.Parse(selectedlotsize) * int.Parse(Lot2), 1);
					decimal finalbrokerage4 = int.Parse(Lot2) * finalbrokerage;
					if (Universal.ExecuteNonQuery(string.Concat(new string[]
					{
						"update t_user_order set P_L='",
						final_pl3.ToString(),
						"',OrderTypeClose='Limit',ActionByClose='",
						ActionCloseBy2,
						"',OrderStatus='Closed',Brokerage='",
						finalbrokerage4.ToString(),
						"',BroughtBy='",
						cmprice2,
						"',ClosedAt='",
						Universal.GetDate,
						"',ClosedTime='",
						Universal.GetTime,
						"',OrderRemark='OrderClosedFromClientPartial' where Id='",
						Id2,
						"'"
					})) == 1)
					{
						string msg4 = string.Concat(new string[]
						{
							obj.UserName,
							"(",
							obj.UserId,
							") have ",
							OrderCategory2,
							" ",
							Lot2,
							" lots of ",
							obj.ScriptName,
							" at ",
							obj.OrderPrice,
							". Auto Closed due to settlement."
						});
						string[] array4 = new string[11];
						array4[0] = "insert into t_action_master (Message,ActionDate,ActionTime,ActionFrom,IpAddress) values('";
						array4[1] = msg4;
						array4[2] = "','";
						array4[3] = Universal.GetDate;
						array4[4] = "','";
						array4[5] = Universal.GetTime;
						array4[6] = "','";
						array4[7] = obj.UserId;
						array4[8] = "','";
						int num4 = 9;
						IPAddress ipaddress4 = externalIp;
						array4[num4] = ((ipaddress4 != null) ? ipaddress4.ToString() : null);
						array4[10] = "')";
						Universal.ExecuteNonQuery(string.Concat(array4));
					}
					Universal.ExecuteNonQuery(string.Concat(new string[]
					{
						"update t_trading_all_users_master set LedgerBalance='",
						(LedgerBalance3 - decimal.Parse(finalbrokerage4.ToString()) + final_pl3).ToString(),
						"' where Id='",
						obj.UserId,
						"'"
					}));
				}
				int defflot = int.Parse(obj.Lot) - totaldatabase_lot;
				if (defflot > 0)
				{
					if (obj.SymbolType == "MCX")
					{
						if (Mcx_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(MCX_Exposure_Lot_wise_sym_Intraday);
							final_indraday_holding = decimal.Parse(MCX_Exposure_Lot_wise_sym_Holding);
						}
						else
						{
							final_intraday_exp = decimal.Parse(obj.OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_MCX);
							final_indraday_holding = decimal.Parse(obj.OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_MCX);
						}
					}
					else if (obj.SymbolType == "NSE")
					{
						if (NSE_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(Intraday_Exposure_Margin_Equity);
							final_indraday_holding = decimal.Parse(Holding_Exposure_Margin_Equity);
						}
						else
						{
							final_intraday_exp = decimal.Parse(obj.OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_Equity);
							final_indraday_holding = decimal.Parse(obj.OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_Equity);
						}
					}
					else if (obj.SymbolType == "OPT")
					{
						if (CDS_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(Intraday_Exposure_Margin_CDS);
							final_indraday_holding = decimal.Parse(Holding_Exposure_Margin_CDS);
						}
						else
						{
							final_intraday_exp = decimal.Parse(obj.OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_CDS);
							final_indraday_holding = decimal.Parse(obj.OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_CDS);
						}
					}
					decimal marginvalue3 = final_intraday_exp * defflot;
					decimal holdmargn3 = final_indraday_holding * defflot;
					if (Universal.ExecuteNonQuery(string.Concat(new string[]
					{
						"update t_user_order set  ActionBy='",
						Act_by,
						"', OrderStatus='Active',Lot='",
						defflot.ToString(),
						"',MarginUsed='",
						marginvalue3.ToString(),
						"',HoldingMarginReq='",
						holdmargn3.ToString(),
						"',OrderConvertFrom='OrderConvertedFromClientPartial' where Id='",
						obj.Id,
						"'"
					})) == 1)
					{
						string msg5 = string.Concat(new string[]
						{
							obj.UserName,
							"(",
							obj.UserId,
							") have ",
							obj.OrderCategory,
							" ",
							defflot.ToString(),
							" Lots of ",
							obj.ScriptName,
							" at ",
							obj.OrderPrice,
							". Pending Order triggered into New Trade"
						});
						string[] array5 = new string[11];
						array5[0] = "insert into t_action_master (Message,ActionDate,ActionTime,ActionFrom,IpAddress) values('";
						array5[1] = msg5;
						array5[2] = "','";
						array5[3] = Universal.GetDate;
						array5[4] = "','";
						array5[5] = Universal.GetTime;
						array5[6] = "','";
						array5[7] = obj.UserId;
						array5[8] = "','";
						int num5 = 9;
						IPAddress ipaddress5 = externalIp;
						array5[num5] = ((ipaddress5 != null) ? ipaddress5.ToString() : null);
						array5[10] = "')";
						Universal.ExecuteNonQuery(string.Concat(array5));
					}
				}
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0001A0EC File Offset: 0x000182EC
		public string checkbeforetrade(t_order_master obj)
		{
			if (obj.OrderPrice.Contains(" "))
			{
				return "Due to Network Issue! Trade can't be place.";
			}
			string _symbolname;
			if (obj.SymbolType == "MCX")
			{
				string[] symarr = obj.ScriptName.Split(new char[]
				{
					'_'
				});
				_symbolname = this.setsymbol(symarr[0].Trim());
			}
			else
			{
				_symbolname = "GOLD";
			}
			string qry;
			if (obj.ScriptName.Contains("NIFTY") && obj.SymbolType == "NSE")
			{
				qry = string.Concat(new string[]
				{
					"select (select ifnull(SUM(LOT),0) from t_user_order where OrderStatus='Active' and week(OrderDate)= week(convert_tz(now(), '+00:00', '+00:00')) and UserId='",
					obj.UserId,
					"' and SymbolType='",
					obj.SymbolType,
					"' and ScriptName like '%NIFTY%') as totallotactive,(select ifnull(SUM(LOT),0) from t_user_order where OrderStatus='Pending'  and UserId='",
					obj.UserId,
					"' and week(OrderDate)= week(convert_tz(now(), '+00:00', '+00:00')) and SymbolType='",
					obj.SymbolType,
					"' and ScriptName like '%NIFTY%') as totallolpending,(select ifnull(SUM(LOT),0) from t_user_order where OrderStatus='Active' and week(OrderDate)= week(convert_tz(now(), '+00:00', '+00:00')) and UserId='",
					obj.UserId,
					"' and SymbolType='",
					obj.SymbolType,
					"' and TokenNo='",
					obj.TokenNo,
					"') as totallottoken_wise_active,(select ifnull(SUM(LOT),0) from t_user_order where OrderStatus='Pending' and week(OrderDate)= week(convert_tz(now(), '+00:00', '+00:00')) and UserId='",
					obj.UserId,
					"' and SymbolType='",
					obj.SymbolType,
					"' and TokenNo='",
					obj.TokenNo,
					"') as totallottoken_wise_pending,Minimum_lot_size_required_per_single_trade_of_MCX, Maximum_lot_size_allowed_per_single_trade_of_MCX, Maximum_lot_size_allowed_per_script_of_MCX_to_be, Maximum_lot_size_allowed_overall_in_MCX_to_be,Minimum_lot_size_required_per_single_trade_of_Equity_Futures, Maximum_lot_size_allowed_per_single_trade_of_Equity_Futures, Maxi_lt_sz_ald_per_scrt_of_Eqty_to_be_acty_open_a_time, Maximum_lot_size_allowed_per_script_of_Equity_to_be,Max_lot_size_alld_overall_in_Equity_to_be_actively, Maxi_lt_sz_ald_per_scpt_of_Eqty_INDEX_to_be_acty_opn_a_time, Min_lot_size_reqd_per_single_trade_of_Equity_Futures_INDEX, Max_lot_size_allw_per_single_trade_of_Equity_Futures_INDEX, Maximum_lot_size_allowed_per_script_of_Equity_INDEX_to_be, Max_lot_size_alld_ovl_in_Equity_INDEX_to_be_actively, Minimum_lot_size_required_per_single_of_CDS, Max_lot_size_ald_per_single_CDS_Futures, mlsa_per_script_of_CDS_to_be_actively_open_at_a_time, mlsa_overall_in_CDS_to_be_actively_open_at_a_time,Min_lot_size_reqper_single_trade_CDS_FutINDEX,mlsa_per_single_trade_of_CDS_Futures_INDEX,mlsa_per_script_of_CDS_INDEX_to_be_actively_open_ata_time,mlsa_overall_in_CDS_INDEX_to_be_actively_open_at_a_time, TradeMCXUnits, TradeCDSUnits, TradeEquityUnits,OneSideBrokerageIntraday,(select Lot_",
					_symbolname,
					" from t_trading_all_users_masterii where userid='",
					obj.UserId,
					"') as lotno from t_trading_all_users_master where id = '",
					obj.UserId,
					"'"
				});
			}
			else
			{
				qry = string.Concat(new string[]
				{
					"select (select ifnull(SUM(LOT),0) from t_user_order where OrderStatus='Active' and  week(OrderDate)= week(convert_tz(now(), '+00:00', '+00:00')) and UserId='",
					obj.UserId,
					"' and SymbolType='",
					obj.SymbolType,
					"') as totallotactive,(select ifnull(SUM(LOT),0) from t_user_order where OrderStatus='Pending' and  week(OrderDate)= week(convert_tz(now(), '+00:00', '+00:00'))  and UserId='",
					obj.UserId,
					"' and SymbolType='",
					obj.SymbolType,
					"') as totallolpending,(select ifnull(SUM(LOT),0) from t_user_order where OrderStatus='Active' and  week(OrderDate)= week(convert_tz(now(), '+00:00', '+00:00')) and UserId='",
					obj.UserId,
					"' and SymbolType='",
					obj.SymbolType,
					"' and TokenNo='",
					obj.TokenNo,
					"') as totallottoken_wise_active,(select ifnull(SUM(LOT),0) from t_user_order where OrderStatus='Pending' and week(OrderDate)= week(convert_tz(now(), '+00:00', '+00:00'))  and UserId='",
					obj.UserId,
					"' and SymbolType='",
					obj.SymbolType,
					"' and TokenNo='",
					obj.TokenNo,
					"') as totallottoken_wise_pending,Minimum_lot_size_required_per_single_trade_of_MCX, Maximum_lot_size_allowed_per_single_trade_of_MCX, Maximum_lot_size_allowed_per_script_of_MCX_to_be, Maximum_lot_size_allowed_overall_in_MCX_to_be,Minimum_lot_size_required_per_single_trade_of_Equity_Futures, Maximum_lot_size_allowed_per_single_trade_of_Equity_Futures, Maxi_lt_sz_ald_per_scrt_of_Eqty_to_be_acty_open_a_time, Maximum_lot_size_allowed_per_script_of_Equity_to_be,Max_lot_size_alld_overall_in_Equity_to_be_actively, Maxi_lt_sz_ald_per_scpt_of_Eqty_INDEX_to_be_acty_opn_a_time, Min_lot_size_reqd_per_single_trade_of_Equity_Futures_INDEX, Max_lot_size_allw_per_single_trade_of_Equity_Futures_INDEX, Maximum_lot_size_allowed_per_script_of_Equity_INDEX_to_be, Max_lot_size_alld_ovl_in_Equity_INDEX_to_be_actively, Minimum_lot_size_required_per_single_of_CDS, Max_lot_size_ald_per_single_CDS_Futures, mlsa_per_script_of_CDS_to_be_actively_open_at_a_time, mlsa_overall_in_CDS_to_be_actively_open_at_a_time,Min_lot_size_reqper_single_trade_CDS_FutINDEX,mlsa_per_single_trade_of_CDS_Futures_INDEX,mlsa_per_script_of_CDS_INDEX_to_be_actively_open_ata_time,mlsa_overall_in_CDS_INDEX_to_be_actively_open_at_a_time, TradeMCXUnits, TradeCDSUnits, TradeEquityUnits,OneSideBrokerageIntraday,(select Lot_",
					_symbolname,
					" from t_trading_all_users_masterii where userid='",
					obj.UserId,
					"') as lotno from t_trading_all_users_master where id = '",
					obj.UserId,
					"'"
				});
			}
			DataTable mcxdt = Universal.SelectWithDS(qry, "t_user_details");
			string Minimum_lot_size_required_per_single_trade_of_MCX = "";
			string Maximum_lot_size_allowed_per_single_trade_of_MCX = "";
			string Maximum_lot_size_allowed_per_script_of_MCX_to_be = "";
			string Maximum_lot_size_allowed_overall_in_MCX_to_be = "";
			string lotno = mcxdt.Rows[0]["lotno"].ToString();
			string TradeMCXUnits = mcxdt.Rows[0]["TradeMCXUnits"].ToString();
			string TradeEquityUnits = mcxdt.Rows[0]["TradeEquityUnits"].ToString();
			string TradeCDSUnits = mcxdt.Rows[0]["TradeCDSUnits"].ToString();
			string totallotactive = mcxdt.Rows[0]["totallotactive"].ToString();
			string OneSideBrokerageIntraday = mcxdt.Rows[0]["OneSideBrokerageIntraday"].ToString();
			decimal totallolpending = decimal.Parse(mcxdt.Rows[0]["totallolpending"].ToString());
			string totallottoken_wise_active = mcxdt.Rows[0]["totallottoken_wise_active"].ToString();
			decimal totallottoken_wise_pending = decimal.Parse(mcxdt.Rows[0]["totallottoken_wise_pending"].ToString());
			decimal lotsno = decimal.Parse(totallottoken_wise_active) + decimal.Parse(obj.Lot);
			if (obj.SymbolType == "MCX")
			{
				if (OneSideBrokerageIntraday == "true")
				{
					if (TradeMCXUnits == "true")
					{
						lotno = (decimal.Parse(lotno) * decimal.Parse(obj.actualLot)).ToString();
					}
					if (lotsno > decimal.Parse(lotno))
					{
						return "You reached maximum lots limit for " + obj.ScriptName + ". Please contact Admin.";
					}
				}
				if (TradeMCXUnits == "true")
				{
					Minimum_lot_size_required_per_single_trade_of_MCX = "1";
					Maximum_lot_size_allowed_per_single_trade_of_MCX = (decimal.Parse(obj.actualLot) * decimal.Parse(mcxdt.Rows[0]["Maximum_lot_size_allowed_per_single_trade_of_MCX"].ToString())).ToString();
					Maximum_lot_size_allowed_per_script_of_MCX_to_be = (decimal.Parse(obj.actualLot) * decimal.Parse(mcxdt.Rows[0]["Maximum_lot_size_allowed_per_script_of_MCX_to_be"].ToString())).ToString();
					Maximum_lot_size_allowed_overall_in_MCX_to_be = (decimal.Parse(obj.actualLot) * decimal.Parse(mcxdt.Rows[0]["Maximum_lot_size_allowed_overall_in_MCX_to_be"].ToString())).ToString();
				}
				else
				{
					Minimum_lot_size_required_per_single_trade_of_MCX = mcxdt.Rows[0]["Minimum_lot_size_required_per_single_trade_of_MCX"].ToString();
					Maximum_lot_size_allowed_per_single_trade_of_MCX = mcxdt.Rows[0]["Maximum_lot_size_allowed_per_single_trade_of_MCX"].ToString();
					Maximum_lot_size_allowed_per_script_of_MCX_to_be = mcxdt.Rows[0]["Maximum_lot_size_allowed_per_script_of_MCX_to_be"].ToString();
					Maximum_lot_size_allowed_overall_in_MCX_to_be = mcxdt.Rows[0]["Maximum_lot_size_allowed_overall_in_MCX_to_be"].ToString();
				}
			}
			else if (obj.SymbolType == "NSE")
			{
				if (obj.ScriptName.StartsWith("BANKNIFTY") || obj.ScriptName.StartsWith("NIFTY"))
				{
					if (TradeEquityUnits == "true")
					{
						Minimum_lot_size_required_per_single_trade_of_MCX = "1";
						Maximum_lot_size_allowed_per_single_trade_of_MCX = (decimal.Parse(obj.actualLot) * decimal.Parse(mcxdt.Rows[0]["Max_lot_size_allw_per_single_trade_of_Equity_Futures_INDEX"].ToString())).ToString();
						Maximum_lot_size_allowed_per_script_of_MCX_to_be = (decimal.Parse(obj.actualLot) * decimal.Parse(mcxdt.Rows[0]["Maximum_lot_size_allowed_per_script_of_Equity_INDEX_to_be"].ToString())).ToString();
						Maximum_lot_size_allowed_overall_in_MCX_to_be = (decimal.Parse(obj.actualLot) * decimal.Parse(mcxdt.Rows[0]["Max_lot_size_alld_ovl_in_Equity_INDEX_to_be_actively"].ToString())).ToString();
					}
					else
					{
						Minimum_lot_size_required_per_single_trade_of_MCX = mcxdt.Rows[0]["Min_lot_size_reqd_per_single_trade_of_Equity_Futures_INDEX"].ToString();
						Maximum_lot_size_allowed_per_single_trade_of_MCX = mcxdt.Rows[0]["Max_lot_size_allw_per_single_trade_of_Equity_Futures_INDEX"].ToString();
						Maximum_lot_size_allowed_per_script_of_MCX_to_be = mcxdt.Rows[0]["Maximum_lot_size_allowed_per_script_of_Equity_INDEX_to_be"].ToString();
						Maximum_lot_size_allowed_overall_in_MCX_to_be = mcxdt.Rows[0]["Max_lot_size_alld_ovl_in_Equity_INDEX_to_be_actively"].ToString();
					}
				}
				else if (TradeEquityUnits == "true")
				{
					Minimum_lot_size_required_per_single_trade_of_MCX = "1";
					Maximum_lot_size_allowed_per_single_trade_of_MCX = (decimal.Parse(obj.actualLot) * decimal.Parse(mcxdt.Rows[0]["Maximum_lot_size_allowed_per_single_trade_of_Equity_Futures"].ToString())).ToString();
					Maximum_lot_size_allowed_per_script_of_MCX_to_be = (decimal.Parse(obj.actualLot) * decimal.Parse(mcxdt.Rows[0]["Maxi_lt_sz_ald_per_scrt_of_Eqty_to_be_acty_open_a_time"].ToString())).ToString();
					Maximum_lot_size_allowed_overall_in_MCX_to_be = (decimal.Parse(obj.actualLot) * decimal.Parse(mcxdt.Rows[0]["Max_lot_size_alld_overall_in_Equity_to_be_actively"].ToString())).ToString();
				}
				else
				{
					Minimum_lot_size_required_per_single_trade_of_MCX = mcxdt.Rows[0]["Minimum_lot_size_required_per_single_trade_of_Equity_Futures"].ToString();
					Maximum_lot_size_allowed_per_single_trade_of_MCX = mcxdt.Rows[0]["Maximum_lot_size_allowed_per_single_trade_of_Equity_Futures"].ToString();
					Maximum_lot_size_allowed_per_script_of_MCX_to_be = mcxdt.Rows[0]["Maxi_lt_sz_ald_per_scrt_of_Eqty_to_be_acty_open_a_time"].ToString();
					Maximum_lot_size_allowed_overall_in_MCX_to_be = mcxdt.Rows[0]["Max_lot_size_alld_overall_in_Equity_to_be_actively"].ToString();
				}
			}
			else if (obj.SymbolType == "OPT")
			{
				if (obj.ScriptName.StartsWith("BANKNIFTY") || obj.ScriptName.StartsWith("NIFTY"))
				{
					if (TradeEquityUnits == "true")
					{
						Minimum_lot_size_required_per_single_trade_of_MCX = "1";
						Maximum_lot_size_allowed_per_single_trade_of_MCX = (decimal.Parse(obj.actualLot) * decimal.Parse(mcxdt.Rows[0]["mlsa_per_single_trade_of_CDS_Futures_INDEX"].ToString())).ToString();
						Maximum_lot_size_allowed_per_script_of_MCX_to_be = (decimal.Parse(obj.actualLot) * decimal.Parse(mcxdt.Rows[0]["mlsa_per_script_of_CDS_INDEX_to_be_actively_open_ata_time"].ToString())).ToString();
						Maximum_lot_size_allowed_overall_in_MCX_to_be = (decimal.Parse(obj.actualLot) * decimal.Parse(mcxdt.Rows[0]["mlsa_overall_in_CDS_INDEX_to_be_actively_open_at_a_time"].ToString())).ToString();
					}
					else
					{
						Minimum_lot_size_required_per_single_trade_of_MCX = mcxdt.Rows[0]["Min_lot_size_reqper_single_trade_CDS_FutINDEX"].ToString();
						Maximum_lot_size_allowed_per_single_trade_of_MCX = mcxdt.Rows[0]["mlsa_per_single_trade_of_CDS_Futures_INDEX"].ToString();
						Maximum_lot_size_allowed_per_script_of_MCX_to_be = mcxdt.Rows[0]["mlsa_per_script_of_CDS_INDEX_to_be_actively_open_ata_time"].ToString();
						Maximum_lot_size_allowed_overall_in_MCX_to_be = mcxdt.Rows[0]["mlsa_overall_in_CDS_INDEX_to_be_actively_open_at_a_time"].ToString();
					}
				}
				else if (TradeCDSUnits == "true")
				{
					Minimum_lot_size_required_per_single_trade_of_MCX = "1";
					Maximum_lot_size_allowed_per_single_trade_of_MCX = (decimal.Parse(obj.actualLot) * decimal.Parse(mcxdt.Rows[0]["Max_lot_size_ald_per_single_CDS_Futures"].ToString())).ToString();
					Maximum_lot_size_allowed_per_script_of_MCX_to_be = (decimal.Parse(obj.actualLot) * decimal.Parse(mcxdt.Rows[0]["mlsa_per_script_of_CDS_to_be_actively_open_at_a_time"].ToString())).ToString();
					Maximum_lot_size_allowed_overall_in_MCX_to_be = (decimal.Parse(obj.actualLot) * decimal.Parse(mcxdt.Rows[0]["mlsa_overall_in_CDS_to_be_actively_open_at_a_time"].ToString())).ToString();
				}
				else
				{
					Minimum_lot_size_required_per_single_trade_of_MCX = mcxdt.Rows[0]["Minimum_lot_size_required_per_single_of_CDS"].ToString();
					Maximum_lot_size_allowed_per_single_trade_of_MCX = mcxdt.Rows[0]["Max_lot_size_ald_per_single_CDS_Futures"].ToString();
					Maximum_lot_size_allowed_per_script_of_MCX_to_be = mcxdt.Rows[0]["mlsa_per_script_of_CDS_to_be_actively_open_at_a_time"].ToString();
					Maximum_lot_size_allowed_overall_in_MCX_to_be = mcxdt.Rows[0]["mlsa_overall_in_CDS_to_be_actively_open_at_a_time"].ToString();
				}
			}
			decimal finallot = decimal.Parse(totallotactive) + totallolpending + decimal.Parse(obj.Lot);
			decimal finallot_toknwise = decimal.Parse(totallottoken_wise_active) + totallottoken_wise_pending + decimal.Parse(obj.Lot);
			decimal.Parse(totallottoken_wise_active) + decimal.Parse(obj.Lot);
			if (decimal.Parse(obj.Lot) >= decimal.Parse(Minimum_lot_size_required_per_single_trade_of_MCX) && decimal.Parse(obj.Lot) <= decimal.Parse(Maximum_lot_size_allowed_per_single_trade_of_MCX))
			{
				if (finallot > decimal.Parse(Maximum_lot_size_allowed_overall_in_MCX_to_be))
				{
					if (!(totallolpending > 0m))
					{
						return "Trade can not placed. Your maximum lots/units limit is " + int.Parse(Maximum_lot_size_allowed_overall_in_MCX_to_be).ToString() + ".";
					}
					if (!(finallot_toknwise > decimal.Parse(Maximum_lot_size_allowed_per_script_of_MCX_to_be)))
					{
						return "true";
					}
					if (!(totallolpending > 0m))
					{
						return string.Concat(new string[]
						{
							"Trade can not placed. Your maximum lots/units limit for ",
							obj.ScriptName,
							" is ",
							int.Parse(Maximum_lot_size_allowed_per_script_of_MCX_to_be).ToString(),
							"."
						});
					}
					DataTable pendingorder = Universal.SelectWithDS("select * from t_user_order where UserId='" + obj.UserId + "' and OrderStatus='Pending'", "pendingorders");
					for (int i = 0; i < pendingorder.Rows.Count; i++)
					{
						string id = pendingorder.Rows[i]["Id"].ToString();
						string Lot = pendingorder.Rows[i]["Lot"].ToString();
						if (finallot_toknwise >= decimal.Parse(Maximum_lot_size_allowed_per_script_of_MCX_to_be) && Universal.ExecuteNonQuery("delete from t_user_order where id='" + id + "'") == 1)
						{
							Universal.ExecuteNonQuery(string.Concat(new string[]
							{
								"insert into t_Notification (UserID,Title,Message,CreatedDate) values ('",
								obj.UserId,
								"','Order Deleted','Order deleted of ",
								obj.ScriptName,
								" at ",
								Universal.GetDate,
								" ",
								Universal.GetTime,
								"','",
								Universal.GetDate,
								" ",
								Universal.GetTime,
								"')"
							}));
							totallottoken_wise_pending -= int.Parse(Lot);
							finallot_toknwise = decimal.Parse(totallottoken_wise_active) + totallottoken_wise_pending + decimal.Parse(obj.Lot);
						}
						if (finallot_toknwise < decimal.Parse(Maximum_lot_size_allowed_per_script_of_MCX_to_be))
						{
							return "true";
						}
					}
				}
				else
				{
					if (!(finallot_toknwise > decimal.Parse(Maximum_lot_size_allowed_per_script_of_MCX_to_be)))
					{
						return "true";
					}
					if (!(totallolpending > 0m))
					{
						return string.Concat(new string[]
						{
							"Trade can not placed. Your maximum lots/units limit for ",
							obj.ScriptName,
							" is ",
							int.Parse(Maximum_lot_size_allowed_per_script_of_MCX_to_be).ToString(),
							"."
						});
					}
					DataTable pendingorder2 = Universal.SelectWithDS("select * from t_user_order where UserId='" + obj.UserId + "' and OrderStatus='Pending'", "pendingorders");
					for (int j = 0; j < pendingorder2.Rows.Count; j++)
					{
						string id2 = pendingorder2.Rows[j]["Id"].ToString();
						string Lot2 = pendingorder2.Rows[j]["Lot"].ToString();
						if (finallot_toknwise >= decimal.Parse(Maximum_lot_size_allowed_per_script_of_MCX_to_be) && Universal.ExecuteNonQuery("delete from t_user_order where id='" + id2 + "'") == 1)
						{
							Universal.ExecuteNonQuery(string.Concat(new string[]
							{
								"insert into t_Notification (UserID,Title,Message,CreatedDate) values ('",
								obj.UserId,
								"','Order Deleted','Order deleted of ",
								obj.ScriptName,
								" at ",
								Universal.GetDate,
								" ",
								Universal.GetTime,
								"','",
								Universal.GetDate,
								" ",
								Universal.GetTime,
								"')"
							}));
							totallottoken_wise_pending -= int.Parse(Lot2);
							finallot_toknwise = decimal.Parse(totallottoken_wise_active) + totallottoken_wise_pending + decimal.Parse(obj.Lot);
						}
						if (finallot_toknwise < decimal.Parse(Maximum_lot_size_allowed_per_script_of_MCX_to_be))
						{
							return "true";
						}
					}
				}
				return "";
			}
			return string.Concat(new string[]
			{
				"Please enter Lots/Units between ",
				int.Parse(Minimum_lot_size_required_per_single_trade_of_MCX).ToString(),
				" and ",
				int.Parse(Maximum_lot_size_allowed_per_single_trade_of_MCX).ToString(),
				"."
			});
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0001B110 File Offset: 0x00019310
		public string checkbeforetradeForPending(t_order_master obj)
		{
			if (obj.OrderPrice.Contains(" "))
			{
				return "Due to Network Issue! Trade can't be place.";
			}
			DataTable mcxdt = Universal.SelectWithDS(string.Concat(new string[]
			{
				"select (select ifnull(SUM(LOT),0) from t_user_order where OrderStatus='Active' and week(OrderDate)= week(convert_tz(now(), '+00:00', '+00:00')) and UserId='",
				obj.UserId,
				"' and SymbolType='",
				obj.SymbolType,
				"') as totallotactive,(select ifnull(SUM(LOT),0) from t_user_order where OrderStatus='Pending' and  week(OrderDate)= week(convert_tz(now(), '+00:00', '+00:00')) and UserId='",
				obj.UserId,
				"' and SymbolType='",
				obj.SymbolType,
				"') as totallolpending,(select ifnull(SUM(LOT),0) from t_user_order where OrderStatus='Active' and  week(OrderDate)= week(convert_tz(now(), '+00:00', '+00:00')) and UserId='",
				obj.UserId,
				"' and SymbolType='",
				obj.SymbolType,
				"' and TokenNo='",
				obj.TokenNo,
				"') as totallottoken_wise_active,(select OrderCategory from t_user_order where OrderStatus='Active' and UserId='",
				obj.UserId,
				"' and SymbolType='",
				obj.SymbolType,
				"' and TokenNo='",
				obj.TokenNo,
				"' limit 1) as OrdCat,(select ifnull(SUM(LOT),0) from t_user_order where OrderStatus='Pending' and  week(OrderDate)= week(convert_tz(now(), '+00:00', '+00:00')) and UserId='",
				obj.UserId,
				"' and SymbolType='",
				obj.SymbolType,
				"' and TokenNo='",
				obj.TokenNo,
				"' and OrderCategory='",
				obj.OrderCategory,
				"') as totallottoken_wise_pending,(select ifnull(SUM(LOT),0) from t_user_order where OrderStatus='Pending' and  week(OrderDate)= week(convert_tz(now(), '+00:00', '+00:00')) and UserId='",
				obj.UserId,
				"' and SymbolType='",
				obj.SymbolType,
				"' and TokenNo='",
				obj.TokenNo,
				"' and OrderCategory!='",
				obj.OrderCategory,
				"') as totalOrderCategory_wise_pending,Minimum_lot_size_required_per_single_trade_of_MCX, Maximum_lot_size_allowed_per_single_trade_of_MCX, Maximum_lot_size_allowed_per_script_of_MCX_to_be, Maximum_lot_size_allowed_overall_in_MCX_to_be,Minimum_lot_size_required_per_single_trade_of_Equity_Futures, Maximum_lot_size_allowed_per_single_trade_of_Equity_Futures, Maxi_lt_sz_ald_per_scrt_of_Eqty_to_be_acty_open_a_time, Maximum_lot_size_allowed_per_script_of_Equity_to_be,Max_lot_size_alld_overall_in_Equity_to_be_actively, Maxi_lt_sz_ald_per_scpt_of_Eqty_INDEX_to_be_acty_opn_a_time, Min_lot_size_reqd_per_single_trade_of_Equity_Futures_INDEX, Max_lot_size_allw_per_single_trade_of_Equity_Futures_INDEX, Maximum_lot_size_allowed_per_script_of_Equity_INDEX_to_be, Max_lot_size_alld_ovl_in_Equity_INDEX_to_be_actively, Minimum_lot_size_required_per_single_of_CDS, Max_lot_size_ald_per_single_CDS_Futures,Min_lot_size_reqper_single_trade_CDS_FutINDEX,mlsa_per_single_trade_of_CDS_Futures_INDEX,mlsa_per_script_of_CDS_INDEX_to_be_actively_open_ata_time,mlsa_overall_in_CDS_INDEX_to_be_actively_open_at_a_time, mlsa_per_script_of_CDS_to_be_actively_open_at_a_time, mlsa_overall_in_CDS_to_be_actively_open_at_a_time,TradeMCXUnits, TradeCDSUnits, TradeEquityUnits from t_trading_all_users_master where id = '",
				obj.UserId,
				"'"
			}), "t_user_details");
			string Minimum_lot_size_required_per_single_trade_of_MCX = "";
			string Maximum_lot_size_allowed_per_single_trade_of_MCX = "";
			string Maximum_lot_size_allowed_per_script_of_MCX_to_be = "";
			string Maximum_lot_size_allowed_overall_in_MCX_to_be = "";
			string TradeMCXUnits = mcxdt.Rows[0]["TradeMCXUnits"].ToString();
			string TradeEquityUnits = mcxdt.Rows[0]["TradeEquityUnits"].ToString();
			string TradeCDSUnits = mcxdt.Rows[0]["TradeCDSUnits"].ToString();
			if (Universal.SelectWithDS(string.Concat(new string[]
			{
				"select count(*) as count from t_user_order where SymbolType='",
				obj.SymbolType,
				"' and TokenNo='",
				obj.TokenNo,
				"' and OrderStatus='Pending' and OrderCategory='",
				obj.OrderCategory,
				"' and OrderPrice='",
				obj.OrderPrice,
				"' "
			}), "temptbl").Rows[0][0].ToString() != "0")
			{
				return "Can not place duplicate order.";
			}
			if (obj.SymbolType == "MCX")
			{
				if (TradeMCXUnits == "true")
				{
					Minimum_lot_size_required_per_single_trade_of_MCX = "1";
					Maximum_lot_size_allowed_per_single_trade_of_MCX = (decimal.Parse(obj.actualLot) * decimal.Parse(mcxdt.Rows[0]["Maximum_lot_size_allowed_per_single_trade_of_MCX"].ToString())).ToString();
					Maximum_lot_size_allowed_per_script_of_MCX_to_be = (decimal.Parse(obj.actualLot) * decimal.Parse(mcxdt.Rows[0]["Maximum_lot_size_allowed_per_script_of_MCX_to_be"].ToString())).ToString();
					Maximum_lot_size_allowed_overall_in_MCX_to_be = (decimal.Parse(obj.actualLot) * decimal.Parse(mcxdt.Rows[0]["Maximum_lot_size_allowed_overall_in_MCX_to_be"].ToString())).ToString();
				}
				else
				{
					Minimum_lot_size_required_per_single_trade_of_MCX = mcxdt.Rows[0]["Minimum_lot_size_required_per_single_trade_of_MCX"].ToString();
					Maximum_lot_size_allowed_per_single_trade_of_MCX = mcxdt.Rows[0]["Maximum_lot_size_allowed_per_single_trade_of_MCX"].ToString();
					Maximum_lot_size_allowed_per_script_of_MCX_to_be = mcxdt.Rows[0]["Maximum_lot_size_allowed_per_script_of_MCX_to_be"].ToString();
					Maximum_lot_size_allowed_overall_in_MCX_to_be = mcxdt.Rows[0]["Maximum_lot_size_allowed_overall_in_MCX_to_be"].ToString();
				}
			}
			else if (obj.SymbolType == "NSE")
			{
				if (obj.ScriptName.StartsWith("BANKNIFTY") || obj.ScriptName.StartsWith("NIFTY"))
				{
					if (TradeEquityUnits == "true")
					{
						Minimum_lot_size_required_per_single_trade_of_MCX = "1";
						Maximum_lot_size_allowed_per_single_trade_of_MCX = (decimal.Parse(obj.actualLot) * decimal.Parse(mcxdt.Rows[0]["Max_lot_size_allw_per_single_trade_of_Equity_Futures_INDEX"].ToString())).ToString();
						Maximum_lot_size_allowed_per_script_of_MCX_to_be = (decimal.Parse(obj.actualLot) * decimal.Parse(mcxdt.Rows[0]["Maximum_lot_size_allowed_per_script_of_Equity_INDEX_to_be"].ToString())).ToString();
						Maximum_lot_size_allowed_overall_in_MCX_to_be = (decimal.Parse(obj.actualLot) * decimal.Parse(mcxdt.Rows[0]["Max_lot_size_alld_ovl_in_Equity_INDEX_to_be_actively"].ToString())).ToString();
					}
					else
					{
						Minimum_lot_size_required_per_single_trade_of_MCX = mcxdt.Rows[0]["Min_lot_size_reqd_per_single_trade_of_Equity_Futures_INDEX"].ToString();
						Maximum_lot_size_allowed_per_single_trade_of_MCX = mcxdt.Rows[0]["Max_lot_size_allw_per_single_trade_of_Equity_Futures_INDEX"].ToString();
						Maximum_lot_size_allowed_per_script_of_MCX_to_be = mcxdt.Rows[0]["Maximum_lot_size_allowed_per_script_of_Equity_INDEX_to_be"].ToString();
						Maximum_lot_size_allowed_overall_in_MCX_to_be = mcxdt.Rows[0]["Max_lot_size_alld_ovl_in_Equity_INDEX_to_be_actively"].ToString();
					}
				}
				else if (TradeEquityUnits == "true")
				{
					Minimum_lot_size_required_per_single_trade_of_MCX = "1";
					Maximum_lot_size_allowed_per_single_trade_of_MCX = (decimal.Parse(obj.actualLot) * decimal.Parse(mcxdt.Rows[0]["Maximum_lot_size_allowed_per_single_trade_of_Equity_Futures"].ToString())).ToString();
					Maximum_lot_size_allowed_per_script_of_MCX_to_be = (decimal.Parse(obj.actualLot) * decimal.Parse(mcxdt.Rows[0]["Maxi_lt_sz_ald_per_scrt_of_Eqty_to_be_acty_open_a_time"].ToString())).ToString();
					Maximum_lot_size_allowed_overall_in_MCX_to_be = (decimal.Parse(obj.actualLot) * decimal.Parse(mcxdt.Rows[0]["Max_lot_size_alld_overall_in_Equity_to_be_actively"].ToString())).ToString();
				}
				else
				{
					Minimum_lot_size_required_per_single_trade_of_MCX = mcxdt.Rows[0]["Minimum_lot_size_required_per_single_trade_of_Equity_Futures"].ToString();
					Maximum_lot_size_allowed_per_single_trade_of_MCX = mcxdt.Rows[0]["Maximum_lot_size_allowed_per_single_trade_of_Equity_Futures"].ToString();
					Maximum_lot_size_allowed_per_script_of_MCX_to_be = mcxdt.Rows[0]["Maxi_lt_sz_ald_per_scrt_of_Eqty_to_be_acty_open_a_time"].ToString();
					Maximum_lot_size_allowed_overall_in_MCX_to_be = mcxdt.Rows[0]["Max_lot_size_alld_overall_in_Equity_to_be_actively"].ToString();
				}
			}
			else if (obj.SymbolType == "OPT")
			{
				if (obj.ScriptName.StartsWith("BANKNIFTY") || obj.ScriptName.StartsWith("NIFTY"))
				{
					if (TradeEquityUnits == "true")
					{
						Minimum_lot_size_required_per_single_trade_of_MCX = "1";
						Maximum_lot_size_allowed_per_single_trade_of_MCX = (decimal.Parse(obj.actualLot) * decimal.Parse(mcxdt.Rows[0]["mlsa_per_single_trade_of_CDS_Futures_INDEX"].ToString())).ToString();
						Maximum_lot_size_allowed_per_script_of_MCX_to_be = (decimal.Parse(obj.actualLot) * decimal.Parse(mcxdt.Rows[0]["mlsa_per_script_of_CDS_INDEX_to_be_actively_open_ata_time"].ToString())).ToString();
						Maximum_lot_size_allowed_overall_in_MCX_to_be = (decimal.Parse(obj.actualLot) * decimal.Parse(mcxdt.Rows[0]["mlsa_overall_in_CDS_INDEX_to_be_actively_open_at_a_time"].ToString())).ToString();
					}
					else
					{
						Minimum_lot_size_required_per_single_trade_of_MCX = mcxdt.Rows[0]["Min_lot_size_reqper_single_trade_CDS_FutINDEX"].ToString();
						Maximum_lot_size_allowed_per_single_trade_of_MCX = mcxdt.Rows[0]["mlsa_per_single_trade_of_CDS_Futures_INDEX"].ToString();
						Maximum_lot_size_allowed_per_script_of_MCX_to_be = mcxdt.Rows[0]["mlsa_per_script_of_CDS_INDEX_to_be_actively_open_ata_time"].ToString();
						Maximum_lot_size_allowed_overall_in_MCX_to_be = mcxdt.Rows[0]["mlsa_overall_in_CDS_INDEX_to_be_actively_open_at_a_time"].ToString();
					}
				}
				else if (TradeCDSUnits == "true")
				{
					Minimum_lot_size_required_per_single_trade_of_MCX = "1";
					Maximum_lot_size_allowed_per_single_trade_of_MCX = (decimal.Parse(obj.actualLot) * decimal.Parse(mcxdt.Rows[0]["Max_lot_size_ald_per_single_CDS_Futures"].ToString())).ToString();
					Maximum_lot_size_allowed_per_script_of_MCX_to_be = (decimal.Parse(obj.actualLot) * decimal.Parse(mcxdt.Rows[0]["mlsa_per_script_of_CDS_to_be_actively_open_at_a_time"].ToString())).ToString();
					Maximum_lot_size_allowed_overall_in_MCX_to_be = (decimal.Parse(obj.actualLot) * decimal.Parse(mcxdt.Rows[0]["mlsa_overall_in_CDS_to_be_actively_open_at_a_time"].ToString())).ToString();
				}
				else
				{
					Minimum_lot_size_required_per_single_trade_of_MCX = mcxdt.Rows[0]["Minimum_lot_size_required_per_single_of_CDS"].ToString();
					Maximum_lot_size_allowed_per_single_trade_of_MCX = mcxdt.Rows[0]["Max_lot_size_ald_per_single_CDS_Futures"].ToString();
					Maximum_lot_size_allowed_per_script_of_MCX_to_be = mcxdt.Rows[0]["mlsa_per_script_of_CDS_to_be_actively_open_at_a_time"].ToString();
					Maximum_lot_size_allowed_overall_in_MCX_to_be = mcxdt.Rows[0]["mlsa_overall_in_CDS_to_be_actively_open_at_a_time"].ToString();
				}
			}
			string totallotactive = mcxdt.Rows[0]["totallotactive"].ToString();
			string ordcat = mcxdt.Rows[0]["OrdCat"].ToString();
			decimal totallolpending = decimal.Parse(mcxdt.Rows[0]["totallolpending"].ToString());
			string s = mcxdt.Rows[0]["totallottoken_wise_active"].ToString();
			decimal totallottoken_wise_pending = decimal.Parse(mcxdt.Rows[0]["totallottoken_wise_pending"].ToString());
			decimal totalOrderCategory_wise_pending = decimal.Parse(mcxdt.Rows[0]["totalOrderCategory_wise_pending"].ToString()) + decimal.Parse(obj.Lot);
			decimal finallot = decimal.Parse(totallotactive) + totallolpending + decimal.Parse(obj.Lot);
			decimal finallot_toknwise = decimal.Parse(s) + totallottoken_wise_pending + decimal.Parse(obj.Lot);
			if (!(decimal.Parse(obj.Lot) >= decimal.Parse(Minimum_lot_size_required_per_single_trade_of_MCX)) || !(decimal.Parse(obj.Lot) <= decimal.Parse(Maximum_lot_size_allowed_per_single_trade_of_MCX)))
			{
				return string.Concat(new string[]
				{
					"Please enter Lots/Units between ",
					int.Parse(Minimum_lot_size_required_per_single_trade_of_MCX).ToString(),
					" and ",
					int.Parse(Maximum_lot_size_allowed_per_single_trade_of_MCX).ToString(),
					"."
				});
			}
			if (!(finallot <= decimal.Parse(Maximum_lot_size_allowed_overall_in_MCX_to_be)))
			{
				return "You reached maximum lots/units limit. Please Contact Admin.";
			}
			if (!(finallot_toknwise > decimal.Parse(Maximum_lot_size_allowed_per_script_of_MCX_to_be)))
			{
				return "true";
			}
			if (!(ordcat != obj.OrderCategory))
			{
				return string.Concat(new string[]
				{
					"Order can not placed. Your maximum lots/units limit for ",
					obj.ScriptName,
					" is ",
					int.Parse(Maximum_lot_size_allowed_per_script_of_MCX_to_be).ToString(),
					"."
				});
			}
			if (totalOrderCategory_wise_pending > decimal.Parse(Maximum_lot_size_allowed_per_script_of_MCX_to_be))
			{
				return string.Concat(new string[]
				{
					"Order can not placed. Your maximum lots/units limit for ",
					obj.ScriptName,
					" is ",
					int.Parse(Maximum_lot_size_allowed_per_script_of_MCX_to_be).ToString(),
					"."
				});
			}
			return "true";
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0001BCD4 File Offset: 0x00019ED4
		public void tradesettlement(t_order_master obj)
		{
			(decimal.Parse(obj.OrderPrice) * decimal.Parse(obj.Lot)).ToString();
			IPAddress externalIp = IPAddress.Parse(Universal.devip);
			string userId = obj.UserId;
			string checkcond;
			if (obj.OrderCategory == "SELL")
			{
				checkcond = "BUY";
			}
			else
			{
				checkcond = "SELL";
			}
			string similer_syml;
			if (obj.SymbolType == "MCX")
			{
				string[] symarr = obj.ScriptName.Split(new char[]
				{
					'_'
				});
				similer_syml = this.setsymbol(symarr[0].Trim());
			}
			else
			{
				similer_syml = "GOLD";
			}
			int totaldatabase_lot = 0;
			DataTable dt = Universal.SelectWithDS(string.Concat(new string[]
			{
				"select (select SUM(Lot) from t_user_order where UserId='",
				obj.UserId,
				"' and OrderCategory='",
				checkcond,
				"' and OrderStatus='Active' and TokenNo='",
				obj.TokenNo,
				"') as totaldatabase_lot,t_trading_all_users_master.NSE_Exposure_Type,t_trading_all_users_master.NSE_Brokerage_Type,t_trading_all_users_master.Equity_brokerage_per_crore,t_trading_all_users_master.Intraday_Exposure_Margin_Equity,t_trading_all_users_master.Holding_Exposure_Margin_Equity,t_trading_all_users_master.Mcx_Brokerage_Type,t_trading_all_users_master.MCX_brokerage_per_crore,t_trading_all_users_master.",
				similer_syml,
				"_brokerage,t_trading_all_users_master.Mcx_Exposure_Type, t_trading_all_users_master.Intraday_Exposure_Margin_MCX, t_trading_all_users_master.Holding_Exposure_Margin_MCX, t_trading_all_users_master.MCX_Exposure_Lot_wise_",
				similer_syml,
				"_Intraday, t_trading_all_users_master.MCX_Exposure_Lot_wise_",
				similer_syml,
				"_Holding,t_trading_all_users_master.CDS_Brokerage_Type ,t_trading_all_users_master.CDS_brokerage_per_crore ,t_trading_all_users_master.CDS_Exposure_Type ,t_trading_all_users_master.Intraday_Exposure_Margin_CDS ,t_trading_all_users_master.Holding_Exposure_Margin_CDS  from t_trading_all_users_master where Id='",
				obj.UserId,
				"'  "
			}), "t_user_order");
			string NSE_Exposure_Type = "";
			string NSE_Brokerage_Type = "";
			string Equity_brokerage_per_crore = "";
			string Intraday_Exposure_Margin_Equity = "";
			string Holding_Exposure_Margin_Equity = "";
			string Mcx_Brokerage_Type = "";
			string MCX_brokerage_per_crore = "";
			string MCXsymb_brokerage = "";
			string Mcx_Exposure_Type = "";
			string Intraday_Exposure_Margin_MCX = "";
			string Holding_Exposure_Margin_MCX = "";
			string MCX_Exposure_Lot_wise_sym_Intraday = "";
			string MCX_Exposure_Lot_wise_sym_Holding = "";
			string CDS_Exposure_Type = "";
			string CDS_Brokerage_Type = "";
			string CDS_brokerage_per_crore = "";
			string Intraday_Exposure_Margin_CDS = "";
			string Holding_Exposure_Margin_CDS = "";
			decimal finalbrokerage = 0m;
			decimal final_intraday_exp = 0m;
			decimal final_indraday_holding = 0m;
			if (dt.Rows.Count > 0)
			{
				NSE_Exposure_Type = dt.Rows[0]["NSE_Exposure_Type"].ToString();
				NSE_Brokerage_Type = dt.Rows[0]["NSE_Brokerage_Type"].ToString();
				Equity_brokerage_per_crore = dt.Rows[0]["Equity_brokerage_per_crore"].ToString();
				Intraday_Exposure_Margin_Equity = dt.Rows[0]["Intraday_Exposure_Margin_Equity"].ToString();
				Holding_Exposure_Margin_Equity = dt.Rows[0]["Holding_Exposure_Margin_Equity"].ToString();
				Mcx_Brokerage_Type = dt.Rows[0]["Mcx_Brokerage_Type"].ToString();
				MCX_brokerage_per_crore = dt.Rows[0]["MCX_brokerage_per_crore"].ToString();
				MCXsymb_brokerage = dt.Rows[0][similer_syml + "_brokerage"].ToString();
				Mcx_Exposure_Type = dt.Rows[0]["Mcx_Exposure_Type"].ToString();
				Intraday_Exposure_Margin_MCX = dt.Rows[0]["Intraday_Exposure_Margin_MCX"].ToString();
				Holding_Exposure_Margin_MCX = dt.Rows[0]["Holding_Exposure_Margin_MCX"].ToString();
				MCX_Exposure_Lot_wise_sym_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_" + similer_syml + "_Intraday"].ToString();
				MCX_Exposure_Lot_wise_sym_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_" + similer_syml + "_Holding"].ToString();
				CDS_Exposure_Type = dt.Rows[0]["CDS_Exposure_Type"].ToString();
				CDS_Brokerage_Type = dt.Rows[0]["CDS_Brokerage_Type"].ToString();
				CDS_brokerage_per_crore = dt.Rows[0]["CDS_brokerage_per_crore"].ToString();
				Intraday_Exposure_Margin_CDS = dt.Rows[0]["Intraday_Exposure_Margin_CDS"].ToString();
				Holding_Exposure_Margin_CDS = dt.Rows[0]["Holding_Exposure_Margin_CDS"].ToString();
				if (dt.Rows[0]["totaldatabase_lot"].ToString() != "")
				{
					totaldatabase_lot = int.Parse(dt.Rows[0]["totaldatabase_lot"].ToString());
				}
				else
				{
					totaldatabase_lot = 0;
				}
			}
			if (totaldatabase_lot > int.Parse(obj.Lot))
			{
				DataTable pretable = Universal.SelectWithDS(string.Concat(new string[]
				{
					"select t_user_order.Id,selectedlotsize,DATE_FORMAT(OrderDate,'%Y-%m-%d') as OrderDate,OrderTime,OrderCategory,TokenNo,OrderPrice,t_user_order.SymbolType,Lot,t_universal_tradeble_tokens.buy,t_universal_tradeble_tokens.sell from t_user_order INNER JOIN t_universal_tradeble_tokens ON t_user_order.TokenNo=t_universal_tradeble_tokens.instrument_token  INNER JOIN  t_trading_all_users_master ON t_trading_all_users_master.Id=t_user_order.UserId where UserId='",
					obj.UserId,
					"' and TokenNo='",
					obj.TokenNo,
					"' and OrderCategory='",
					checkcond,
					"' and OrderStatus='Active' ORDER BY Id"
				}), "t_user_order");
				int incoming_lot = int.Parse(obj.Lot);
				for (int i = 0; i < pretable.Rows.Count; i++)
				{
					string Id = pretable.Rows[i]["Id"].ToString();
					string Lot = pretable.Rows[i]["Lot"].ToString();
					pretable.Rows[i]["buy"].ToString();
					pretable.Rows[i]["sell"].ToString();
					string OrderPrice = pretable.Rows[i]["OrderPrice"].ToString();
					string OrderCategory = pretable.Rows[i]["OrderCategory"].ToString();
					string OrderDate = pretable.Rows[i]["OrderDate"].ToString();
					string OrderTime = pretable.Rows[i]["OrderTime"].ToString();
					if (obj.SymbolType == "MCX")
					{
						if (Mcx_Brokerage_Type == "per_lot")
						{
							finalbrokerage = decimal.Parse(MCXsymb_brokerage);
						}
						else
						{
							finalbrokerage = (decimal.Parse(OrderPrice) + decimal.Parse(obj.OrderPrice)) * decimal.Parse(obj.selectedlotsize) / 10000000m * decimal.Parse(MCX_brokerage_per_crore);
						}
						if (Mcx_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(MCX_Exposure_Lot_wise_sym_Intraday);
							final_indraday_holding = decimal.Parse(MCX_Exposure_Lot_wise_sym_Holding);
						}
						else
						{
							final_intraday_exp = decimal.Parse(OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_MCX);
							final_indraday_holding = decimal.Parse(OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_MCX);
						}
					}
					else if (obj.SymbolType == "NSE")
					{
						if (NSE_Brokerage_Type == "per_lot")
						{
							finalbrokerage = decimal.Parse(Equity_brokerage_per_crore);
						}
						else
						{
							finalbrokerage = (decimal.Parse(OrderPrice) + decimal.Parse(obj.OrderPrice)) * decimal.Parse(obj.selectedlotsize) / 10000000m * decimal.Parse(Equity_brokerage_per_crore);
						}
						if (NSE_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(Intraday_Exposure_Margin_Equity);
							final_indraday_holding = decimal.Parse(Holding_Exposure_Margin_Equity);
						}
						else
						{
							final_intraday_exp = decimal.Parse(OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_Equity);
							final_indraday_holding = decimal.Parse(OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_Equity);
						}
					}
					else if (obj.SymbolType == "OPT")
					{
						if (CDS_Brokerage_Type == "per_lot")
						{
							finalbrokerage = decimal.Parse(CDS_brokerage_per_crore);
						}
						else
						{
							finalbrokerage = (decimal.Parse(OrderPrice) + decimal.Parse(obj.OrderPrice)) * decimal.Parse(obj.selectedlotsize) / 10000000m * decimal.Parse(CDS_brokerage_per_crore);
						}
						if (CDS_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(Intraday_Exposure_Margin_CDS);
							final_indraday_holding = decimal.Parse(Holding_Exposure_Margin_CDS);
						}
						else
						{
							final_intraday_exp = decimal.Parse(OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_CDS);
							final_indraday_holding = decimal.Parse(OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_CDS);
						}
					}
					if (incoming_lot > int.Parse(Lot))
					{
						int sublot = incoming_lot - int.Parse(Lot);
						decimal closevalue = 0m;
						string ActionCloseBy;
						string cmprice;
						if (obj.OrderCategory == "SELL")
						{
							ActionCloseBy = "Sold By Trader";
							cmprice = obj.OrderPrice;
							closevalue = decimal.Parse(cmprice) - decimal.Parse(OrderPrice);
						}
						else
						{
							ActionCloseBy = "Bought By Trader";
							cmprice = obj.OrderPrice;
							closevalue = decimal.Parse(OrderPrice) - decimal.Parse(cmprice);
						}
						decimal final_pl = Math.Round(closevalue * int.Parse(obj.selectedlotsize) * int.Parse(Lot), 1);
						DataTable brokeragetbl = Universal.SelectWithDS("select LedgerBalance,creditLimit from t_trading_all_users_master where Id='" + obj.UserId + "'", "t_trading_all_users_master");
						decimal LedgerBalance = 0m;
						if (brokeragetbl.Rows.Count > 0)
						{
							LedgerBalance = decimal.Parse(brokeragetbl.Rows[0][0].ToString());
							decimal.Parse(brokeragetbl.Rows[0][1].ToString());
						}
						finalbrokerage *= int.Parse(Lot);
						if (Universal.ExecuteNonQuery(string.Concat(new string[]
						{
							"update t_user_order set P_L='",
							final_pl.ToString(),
							"',OrderTypeClose='Market',ActionByClose='",
							ActionCloseBy,
							"',OrderStatus='Closed',Brokerage='",
							finalbrokerage.ToString(),
							"',BroughtBy='",
							cmprice,
							"',ClosedAt='",
							Universal.GetDate,
							"',ClosedTime='",
							Universal.GetTime,
							"',OrderRemark='OrderClosedFromTradeSettlementClient' where Id='",
							Id,
							"'"
						})) == 1)
						{
							string msg = string.Concat(new string[]
							{
								obj.UserName,
								"(",
								obj.UserId,
								") have ",
								obj.OrderType,
								" ",
								obj.Lot,
								" lots of ",
								obj.ScriptName,
								" at ",
								obj.OrderPrice,
								" Auto Closed due to settlement."
							});
							string[] array = new string[11];
							array[0] = "insert into t_action_master (Message,ActionDate,ActionTime,ActionFrom,IpAddress) values('";
							array[1] = msg;
							array[2] = "','";
							array[3] = Universal.GetDate;
							array[4] = "','";
							array[5] = Universal.GetTime;
							array[6] = "','";
							array[7] = obj.UserId;
							array[8] = "','";
							int num = 9;
							IPAddress ipaddress = externalIp;
							array[num] = ((ipaddress != null) ? ipaddress.ToString() : null);
							array[10] = "')";
							Universal.ExecuteNonQuery(string.Concat(array));
						}
						Universal.ExecuteNonQuery(string.Concat(new string[]
						{
							"update t_trading_all_users_master set LedgerBalance='",
							(LedgerBalance - finalbrokerage + final_pl).ToString(),
							"' where Id='",
							obj.UserId,
							"'"
						}));
						if (sublot > 0)
						{
							incoming_lot = sublot;
						}
					}
					else if (incoming_lot <= int.Parse(Lot))
					{
						int sublot2 = int.Parse(Lot) - incoming_lot;
						decimal closevalue2 = 0m;
						string CloseActionBy;
						string actiontype;
						string cmp2;
						if (obj.OrderCategory == "SELL")
						{
							CloseActionBy = "Sold By Trader";
							actiontype = "Bought By Trader";
							cmp2 = obj.OrderPrice;
							closevalue2 = decimal.Parse(cmp2) - decimal.Parse(OrderPrice);
						}
						else
						{
							CloseActionBy = "Bought By Trader";
							actiontype = "Sold By Trader";
							cmp2 = obj.OrderPrice;
							closevalue2 = decimal.Parse(OrderPrice) - decimal.Parse(cmp2);
						}
						decimal final_pl2 = Math.Round(closevalue2 * int.Parse(obj.selectedlotsize) * incoming_lot, 1);
						DataTable brokeragetbl2 = Universal.SelectWithDS("select LedgerBalance,creditLimit from t_trading_all_users_master where Id='" + obj.UserId + "'", "t_trading_all_users_master");
						decimal LedgerBalance2 = 0m;
						if (brokeragetbl2.Rows.Count > 0)
						{
							LedgerBalance2 = decimal.Parse(brokeragetbl2.Rows[0][0].ToString());
							decimal.Parse(brokeragetbl2.Rows[0][1].ToString());
						}
						if (sublot2 > 0)
						{
							decimal marginvalue = final_intraday_exp * sublot2;
							decimal holdmargn = final_indraday_holding * sublot2;
							if (Universal.ExecuteNonQuery(string.Concat(new string[]
							{
								"update t_user_order set Lot='",
								sublot2.ToString(),
								"',MarginUsed='",
								marginvalue.ToString(),
								"',HoldingMarginReq='",
								holdmargn.ToString(),
								"' where Id='",
								Id,
								"'"
							})) == 1)
							{
								string msg2 = string.Concat(new string[]
								{
									obj.UserName,
									"(",
									obj.UserId,
									") have ",
									obj.OrderType,
									" ",
									sublot2.ToString(),
									" lots of ",
									obj.ScriptName,
									" at ",
									obj.OrderPrice,
									". Trade has been modified by Autotrade settlement. "
								});
								string[] array2 = new string[11];
								array2[0] = "insert into t_action_master (Message,ActionDate,ActionTime,ActionFrom,IpAddress) values('";
								array2[1] = msg2;
								array2[2] = "','";
								array2[3] = Universal.GetDate;
								array2[4] = "','";
								array2[5] = Universal.GetTime;
								array2[6] = "','";
								array2[7] = obj.UserId;
								array2[8] = "','";
								int num2 = 9;
								IPAddress ipaddress2 = externalIp;
								array2[num2] = ((ipaddress2 != null) ? ipaddress2.ToString() : null);
								array2[10] = "')";
								Universal.ExecuteNonQuery(string.Concat(array2));
							}
						}
						decimal finalbrokerage2 = finalbrokerage * incoming_lot;
						Universal.ExecuteNonQuery(string.Concat(new string[]
						{
							"update t_trading_all_users_master set LedgerBalance='",
							(LedgerBalance2 - decimal.Parse(finalbrokerage2.ToString()) + final_pl2).ToString(),
							"' where Id='",
							obj.UserId,
							"'"
						}));
						decimal marginvalue2 = final_intraday_exp * incoming_lot;
						decimal holdmargn2 = final_indraday_holding * incoming_lot;
						if (Universal.ExecuteNonQuery(string.Concat(new string[]
						{
							"insert into t_user_order (selectedlotsize,OrderDate,OrderCategory,OrderTime,OrderNo,UserId,UserName,OrderType,ScriptName,TokenNo,ActionBy,OrderPrice,Lot,MarginUsed,HoldingMarginReq,OrderStatus,SymbolType,isstoplossorder,BroughtBy,P_L,Brokerage,ClosedAt,ClosedTime,OrderTypeClose,ActionByClose,OrderRemark) values('",
							obj.selectedlotsize,
							"','",
							OrderDate,
							"','",
							OrderCategory,
							"','",
							OrderTime,
							"','",
							obj.OrderNo,
							"','",
							obj.UserId,
							"','",
							obj.UserName,
							"','",
							obj.OrderType,
							"','",
							obj.ScriptName,
							"','",
							obj.TokenNo,
							"','",
							actiontype,
							"','",
							OrderPrice.Trim().Replace(" ", ""),
							"','",
							incoming_lot.ToString(),
							"','",
							marginvalue2.ToString(),
							"','",
							holdmargn2.ToString(),
							"','Closed','",
							obj.SymbolType,
							"','",
							obj.isstoplossorder,
							"','",
							cmp2,
							"','",
							final_pl2.ToString(),
							"','",
							finalbrokerage2.ToString(),
							"','",
							Universal.GetDate,
							"','",
							Universal.GetTime,
							"','Market','",
							CloseActionBy,
							"','OrderInsertedFromTradeSettlementClient')"
						})) == 1)
						{
							string msg3 = string.Concat(new string[]
							{
								obj.UserName,
								"(",
								obj.UserId,
								") have ",
								OrderCategory,
								" ",
								incoming_lot.ToString(),
								" lots of ",
								obj.ScriptName,
								" at ",
								OrderPrice.Trim().Replace(" ", ""),
								". Auto Closed due to settlement."
							});
							string[] array3 = new string[11];
							array3[0] = "insert into t_action_master (Message,ActionDate,ActionTime,ActionFrom,IpAddress) values('";
							array3[1] = msg3;
							array3[2] = "','";
							array3[3] = Universal.GetDate;
							array3[4] = "','";
							array3[5] = Universal.GetTime;
							array3[6] = "','";
							array3[7] = obj.UserId;
							array3[8] = "','";
							int num3 = 9;
							IPAddress ipaddress3 = externalIp;
							array3[num3] = ((ipaddress3 != null) ? ipaddress3.ToString() : null);
							array3[10] = "')";
							Universal.ExecuteNonQuery(string.Concat(array3));
						}
						if (sublot2 == 0)
						{
							Universal.ExecuteNonQuery("delete from t_user_order where Id='" + Id + "'");
							return;
						}
						return;
					}
				}
				return;
			}
			if (totaldatabase_lot <= int.Parse(obj.Lot))
			{
				DataTable pretable2 = Universal.SelectWithDS(string.Concat(new string[]
				{
					"select t_user_order.Id,selectedlotsize,OrderCategory,TokenNo,OrderPrice,t_user_order.SymbolType,Lot,t_universal_tradeble_tokens.buy,t_universal_tradeble_tokens.sell from t_user_order INNER JOIN t_universal_tradeble_tokens ON t_user_order.TokenNo=t_universal_tradeble_tokens.instrument_token where UserId='",
					obj.UserId,
					"' and TokenNo='",
					obj.TokenNo,
					"' and OrderCategory='",
					checkcond,
					"' and OrderStatus='Active' ORDER BY Lot"
				}), "t_user_order");
				int defflot = int.Parse(obj.Lot) - totaldatabase_lot;
				for (int j = 0; j < pretable2.Rows.Count; j++)
				{
					DataTable brokeragetbl3 = Universal.SelectWithDS("select LedgerBalance,creditLimit from t_trading_all_users_master where Id='" + obj.UserId + "'", "t_trading_all_users_master");
					decimal LedgerBalance3 = 0m;
					if (brokeragetbl3.Rows.Count > 0)
					{
						LedgerBalance3 = decimal.Parse(brokeragetbl3.Rows[0][0].ToString());
						decimal.Parse(brokeragetbl3.Rows[0][1].ToString());
					}
					string Id2 = pretable2.Rows[j]["Id"].ToString();
					string Lot2 = pretable2.Rows[j]["Lot"].ToString();
					pretable2.Rows[j]["buy"].ToString();
					pretable2.Rows[j]["sell"].ToString();
					string selectedlotsize = pretable2.Rows[j]["selectedlotsize"].ToString();
					string OrderPrice2 = pretable2.Rows[j]["OrderPrice"].ToString();
					if (obj.SymbolType == "MCX")
					{
						if (Mcx_Brokerage_Type == "per_lot")
						{
							finalbrokerage = decimal.Parse(MCXsymb_brokerage);
						}
						else
						{
							finalbrokerage = (decimal.Parse(OrderPrice2) + decimal.Parse(obj.OrderPrice)) * decimal.Parse(obj.selectedlotsize) / 10000000m * decimal.Parse(MCX_brokerage_per_crore);
						}
						if (Mcx_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(MCX_Exposure_Lot_wise_sym_Intraday);
							final_indraday_holding = decimal.Parse(MCX_Exposure_Lot_wise_sym_Holding);
						}
						else
						{
							final_intraday_exp = decimal.Parse(OrderPrice2) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_MCX);
							final_indraday_holding = decimal.Parse(OrderPrice2) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_MCX);
						}
					}
					else if (obj.SymbolType == "NSE")
					{
						if (NSE_Brokerage_Type == "per_lot")
						{
							finalbrokerage = decimal.Parse(Equity_brokerage_per_crore);
						}
						else
						{
							finalbrokerage = (decimal.Parse(OrderPrice2) + decimal.Parse(obj.OrderPrice)) * decimal.Parse(obj.selectedlotsize) / 10000000m * decimal.Parse(Equity_brokerage_per_crore);
						}
						if (NSE_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(Intraday_Exposure_Margin_Equity);
							final_indraday_holding = decimal.Parse(Holding_Exposure_Margin_Equity);
						}
						else
						{
							final_intraday_exp = decimal.Parse(OrderPrice2) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_Equity);
							final_indraday_holding = decimal.Parse(OrderPrice2) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_Equity);
						}
					}
					else if (obj.SymbolType == "OPT")
					{
						if (CDS_Brokerage_Type == "per_lot")
						{
							finalbrokerage = decimal.Parse(CDS_brokerage_per_crore);
						}
						else
						{
							finalbrokerage = (decimal.Parse(OrderPrice2) + decimal.Parse(obj.OrderPrice)) * decimal.Parse(obj.selectedlotsize) / 10000000m * decimal.Parse(CDS_brokerage_per_crore);
						}
						if (CDS_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(Intraday_Exposure_Margin_CDS);
							final_indraday_holding = decimal.Parse(Holding_Exposure_Margin_CDS);
						}
						else
						{
							final_intraday_exp = decimal.Parse(OrderPrice2) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_CDS);
							final_indraday_holding = decimal.Parse(OrderPrice2) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_CDS);
						}
					}
					decimal closevalue3 = 0m;
					string ActionCloseBy2;
					string cmprice2;
					if (obj.OrderCategory == "SELL")
					{
						ActionCloseBy2 = "Sold By Trader";
						cmprice2 = obj.OrderPrice;
						closevalue3 = decimal.Parse(cmprice2) - decimal.Parse(OrderPrice2);
					}
					else
					{
						ActionCloseBy2 = "Bought By Trader";
						cmprice2 = obj.OrderPrice;
						closevalue3 = decimal.Parse(OrderPrice2) - decimal.Parse(cmprice2);
					}
					decimal final_pl3 = Math.Round(closevalue3 * int.Parse(selectedlotsize) * int.Parse(Lot2), 1);
					decimal finalbrokerage3 = finalbrokerage * int.Parse(Lot2);
					if (Universal.ExecuteNonQuery(string.Concat(new string[]
					{
						"update t_user_order set P_L='",
						final_pl3.ToString(),
						"',OrderTypeClose='Market',ActionByClose='",
						ActionCloseBy2,
						"',OrderStatus='Closed',Brokerage='",
						finalbrokerage3.ToString(),
						"',BroughtBy='",
						cmprice2,
						"',ClosedAt='",
						Universal.GetDate,
						"',ClosedTime='",
						Universal.GetTime,
						"',OrderRemark='OrderClosedFromTradeSettlementClient' where Id='",
						Id2,
						"'"
					})) == 1)
					{
						string msg4 = string.Concat(new string[]
						{
							obj.UserName,
							"(",
							obj.UserId,
							") have ",
							obj.OrderCategory,
							" ",
							obj.Lot,
							" lots of ",
							obj.ScriptName,
							" at ",
							obj.OrderPrice,
							". Auto Closed due to settlement."
						});
						string[] array4 = new string[11];
						array4[0] = "insert into t_action_master (Message,ActionDate,ActionTime,ActionFrom,IpAddress) values('";
						array4[1] = msg4;
						array4[2] = "','";
						array4[3] = Universal.GetDate;
						array4[4] = "','";
						array4[5] = Universal.GetTime;
						array4[6] = "','";
						array4[7] = obj.UserId;
						array4[8] = "','";
						int num4 = 9;
						IPAddress ipaddress4 = externalIp;
						array4[num4] = ((ipaddress4 != null) ? ipaddress4.ToString() : null);
						array4[10] = "')";
						Universal.ExecuteNonQuery(string.Concat(array4));
					}
					Universal.ExecuteNonQuery(string.Concat(new string[]
					{
						"update t_trading_all_users_master set LedgerBalance='",
						(LedgerBalance3 - decimal.Parse(finalbrokerage3.ToString()) + final_pl3).ToString(),
						"' where Id='",
						obj.UserId,
						"'"
					}));
				}
				if (defflot > 0)
				{
					if (obj.SymbolType == "MCX")
					{
						if (Mcx_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(MCX_Exposure_Lot_wise_sym_Intraday);
							final_indraday_holding = decimal.Parse(MCX_Exposure_Lot_wise_sym_Holding);
						}
						else
						{
							final_intraday_exp = decimal.Parse(obj.OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_MCX);
							final_indraday_holding = decimal.Parse(obj.OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_MCX);
						}
					}
					else if (obj.SymbolType == "NSE")
					{
						if (NSE_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(Intraday_Exposure_Margin_Equity);
							final_indraday_holding = decimal.Parse(Holding_Exposure_Margin_Equity);
						}
						else
						{
							final_intraday_exp = decimal.Parse(obj.OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_Equity);
							final_indraday_holding = decimal.Parse(obj.OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_Equity);
						}
					}
					else if (obj.SymbolType == "OPT")
					{
						if (CDS_Exposure_Type == "per_lot")
						{
							final_intraday_exp = decimal.Parse(Intraday_Exposure_Margin_CDS);
							final_indraday_holding = decimal.Parse(Holding_Exposure_Margin_CDS);
						}
						else
						{
							final_intraday_exp = decimal.Parse(obj.OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Intraday_Exposure_Margin_CDS);
							final_indraday_holding = decimal.Parse(obj.OrderPrice) * decimal.Parse(obj.selectedlotsize) / decimal.Parse(Holding_Exposure_Margin_CDS);
						}
					}
					decimal marginvalue3 = final_intraday_exp * defflot;
					decimal holdmargn3 = final_indraday_holding * defflot;
					if (Universal.ExecuteNonQuery(string.Concat(new string[]
					{
						"insert into t_user_order (selectedlotsize,OrderDate,OrderCategory,OrderTime,OrderNo,UserId,UserName,OrderType,ScriptName,TokenNo,ActionBy,OrderPrice,Lot,MarginUsed,HoldingMarginReq,OrderStatus,SymbolType,isstoplossorder,OrderRemark) values('",
						obj.selectedlotsize,
						"','",
						Universal.GetDate,
						"','",
						obj.OrderCategory,
						"','",
						obj.OrderTime,
						"','",
						obj.OrderNo,
						"','",
						obj.UserId,
						"','",
						obj.UserName,
						"','",
						obj.OrderType,
						"','",
						obj.ScriptName,
						"','",
						obj.TokenNo,
						"','",
						obj.ActionType,
						"','",
						obj.OrderPrice.Trim().Replace(" ", ""),
						"','",
						defflot.ToString(),
						"','",
						marginvalue3.ToString(),
						"','",
						holdmargn3.ToString(),
						"','Active','",
						obj.SymbolType,
						"','",
						obj.isstoplossorder,
						"','OrderInsertedFromTradeSettlementClient')"
					})) == 1)
					{
						string msg5 = string.Concat(new string[]
						{
							obj.UserName,
							"(",
							obj.UserId,
							") have ",
							obj.OrderCategory,
							" ",
							obj.Lot,
							" lots of ",
							obj.ScriptName,
							" at ",
							obj.OrderPrice.Trim().Replace(" ", ""),
							". Traded by client @ Market."
						});
						string[] array5 = new string[11];
						array5[0] = "insert into t_action_master (Message,ActionDate,ActionTime,ActionFrom,IpAddress) values('";
						array5[1] = msg5;
						array5[2] = "','";
						array5[3] = Universal.GetDate;
						array5[4] = "','";
						array5[5] = Universal.GetTime;
						array5[6] = "','";
						array5[7] = obj.UserId;
						array5[8] = "','";
						int num5 = 9;
						IPAddress ipaddress5 = externalIp;
						array5[num5] = ((ipaddress5 != null) ? ipaddress5.ToString() : null);
						array5[10] = "')";
						Universal.ExecuteNonQuery(string.Concat(array5));
					}
				}
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0001D9C4 File Offset: 0x0001BBC4
		[HttpPost]
		public string profiledataapp(string userid)
		{
			string response = "";
			DataTable dt = Universal.SelectWithDS("select IsMCXTrade,IsNSETrade,IsCDSTrade,NSE_Exposure_Type,Mcx_Brokerage_Type,MCX_brokerage_per_crore,BULLDEX_brokerage ,GOLD_brokerage,SILVER_brokerage ,CRUDEOIL_brokerage ,COPPER_brokerage ,NICKEL_brokerage ,ZINC_brokerage ,LEAD_brokerage,NATURALGAS_brokerage,ALUMINIUM_brokerage ,MENTHAOIL_brokerage ,COTTON_brokerage,CPO_brokerage, GOLDM_brokerage, SILVERM_brokerage, SILVERMIC_brokerage, Mcx_Exposure_Type, Intraday_Exposure_Margin_MCX, Holding_Exposure_Margin_MCX, MCX_Exposure_Lot_wise_BULLDEX_Intraday, MCX_Exposure_Lot_wise_BULLDEX_Holding, MCX_Exposure_Lot_wise_GOLD_Intraday, MCX_Exposure_Lot_wise_GOLD_Holding, MCX_Exposure_Lot_wise_SILVER_Intraday, MCX_Exposure_Lot_wise_SILVER_Holding, MCX_Exposure_Lot_wise_CRUDEOIL_Intraday, MCX_Exposure_Lot_wise_CRUDEOIL_Holding, MCX_Exposure_Lot_wise_COPPER_Intraday, MCX_Exposure_Lot_wise_COPPER_Holding, MCX_Exposure_Lot_wise_NICKEL_Intraday, MCX_Exposure_Lot_wise_NICKEL_Holding,MCX_Exposure_Lot_wise_ZINC_Intraday, MCX_Exposure_Lot_wise_ZINC_Holding, MCX_Exposure_Lot_wise_LEAD_Intraday, MCX_Exposure_Lot_wise_LEAD_Holding, MCX_Exposure_Lot_wise_NATURALGAS_Intraday, MCX_Exposure_Lot_wise_NATURALGAS_Holding,MCX_Exposure_Lot_wise_ALUMINIUM_Intraday, MCX_Exposure_Lot_wise_ALUMINIUM_Holding, MCX_Exposure_Lot_wise_MENTHAOIL_Intraday, MCX_Exposure_Lot_wise_MENTHAOIL_Holding, MCX_Exposure_Lot_wise_COTTON_Intraday, MCX_Exposure_Lot_wise_COTTON_Holding,MCX_Exposure_Lot_wise_CPO_Intraday, MCX_Exposure_Lot_wise_CPO_Holding,MCX_Exposure_Lot_wise_GOLDM_Intraday, MCX_Exposure_Lot_wise_GOLDM_Holding, MCX_Exposure_Lot_wise_SILVERM_Intraday, MCX_Exposure_Lot_wise_SILVERM_Holding, MCX_Exposure_Lot_wise_SILVERMIC_Intraday, MCX_Exposure_Lot_wise_SILVERMIC_Holding,CDS_Brokerage_Type ,CDS_brokerage_per_crore ,CDS_Exposure_Type ,Intraday_Exposure_Margin_CDS ,Holding_Exposure_Margin_CDS,NSE_Brokerage_Type,Equity_brokerage_per_crore,Intraday_Exposure_Margin_Equity,Holding_Exposure_Margin_Equity from t_trading_all_users_master where Id='" + userid + "'", "t_trading_all_users_master");
			if (dt.Rows.Count > 0)
			{
				string IsMCXTrade = dt.Rows[0]["IsMCXTrade"].ToString();
				string IsNSETrade = dt.Rows[0]["IsNSETrade"].ToString();
				string IsCDSTrade = dt.Rows[0]["IsCDSTrade"].ToString();
				string Mcx_Brokerage_Type = dt.Rows[0]["Mcx_Brokerage_Type"].ToString();
				string MCX_brokerage_per_crore = dt.Rows[0]["MCX_brokerage_per_crore"].ToString();
				string BULLDEX_brokerage = dt.Rows[0]["BULLDEX_brokerage"].ToString();
				string GOLD_brokerage = dt.Rows[0]["GOLD_brokerage"].ToString();
				string SILVER_brokerage = dt.Rows[0]["SILVER_brokerage"].ToString();
				string CRUDEOIL_brokerage = dt.Rows[0]["CRUDEOIL_brokerage"].ToString();
				string COPPER_brokerage = dt.Rows[0]["COPPER_brokerage"].ToString();
				string NICKEL_brokerage = dt.Rows[0]["NICKEL_brokerage"].ToString();
				string ZINC_brokerage = dt.Rows[0]["ZINC_brokerage"].ToString();
				string LEAD_brokerage = dt.Rows[0]["LEAD_brokerage"].ToString();
				string NATURALGAS_brokerage = dt.Rows[0]["NATURALGAS_brokerage"].ToString();
				string ALUMINIUM_brokerage = dt.Rows[0]["ALUMINIUM_brokerage"].ToString();
				string MENTHAOIL_brokerage = dt.Rows[0]["MENTHAOIL_brokerage"].ToString();
				string COTTON_brokerage = dt.Rows[0]["COTTON_brokerage"].ToString();
				string CPO_brokerage = dt.Rows[0]["CPO_brokerage"].ToString();
				string GOLDM_brokerage = dt.Rows[0]["GOLDM_brokerage"].ToString();
				string SILVERM_brokerage = dt.Rows[0]["SILVERM_brokerage"].ToString();
				string SILVERMIC_brokerage = dt.Rows[0]["SILVERMIC_brokerage"].ToString();
				string Mcx_Exposure_Type = dt.Rows[0]["Mcx_Exposure_Type"].ToString();
				string Intraday_Exposure_Margin_MCX = dt.Rows[0]["Intraday_Exposure_Margin_MCX"].ToString();
				string Holding_Exposure_Margin_MCX = dt.Rows[0]["Holding_Exposure_Margin_MCX"].ToString();
				string MCX_Exposure_Lot_wise_BULLDEX_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_BULLDEX_Intraday"].ToString();
				string MCX_Exposure_Lot_wise_GOLD_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_GOLD_Intraday"].ToString();
				string MCX_Exposure_Lot_wise_SILVER_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_SILVER_Intraday"].ToString();
				string MCX_Exposure_Lot_wise_CRUDEOIL_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_CRUDEOIL_Intraday"].ToString();
				string MCX_Exposure_Lot_wise_COPPER_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_COPPER_Intraday"].ToString();
				string MCX_Exposure_Lot_wise_NICKEL_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_NICKEL_Intraday"].ToString();
				string MCX_Exposure_Lot_wise_ZINC_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_ZINC_Intraday"].ToString();
				string MCX_Exposure_Lot_wise_LEAD_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_LEAD_Intraday"].ToString();
				string MCX_Exposure_Lot_wise_NATURALGAS_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_NATURALGAS_Intraday"].ToString();
				string MCX_Exposure_Lot_wise_ALUMINIUM_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_ALUMINIUM_Intraday"].ToString();
				string MCX_Exposure_Lot_wise_MENTHAOIL_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_MENTHAOIL_Intraday"].ToString();
				string MCX_Exposure_Lot_wise_COTTON_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_COTTON_Intraday"].ToString();
				string MCX_Exposure_Lot_wise_CPO_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_CPO_Intraday"].ToString();
				string MCX_Exposure_Lot_wise_SILVERM_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_SILVERM_Intraday"].ToString();
				string MCX_Exposure_Lot_wise_GOLDM_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_GOLDM_Intraday"].ToString();
				string MCX_Exposure_Lot_wise_SILVERMIC_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_SILVERMIC_Intraday"].ToString();
				string MCX_Exposure_Lot_wise_BULLDEX_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_BULLDEX_Holding"].ToString();
				string MCX_Exposure_Lot_wise_GOLD_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_GOLD_Holding"].ToString();
				string MCX_Exposure_Lot_wise_SILVER_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_SILVER_Holding"].ToString();
				string MCX_Exposure_Lot_wise_CRUDEOIL_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_CRUDEOIL_Holding"].ToString();
				string MCX_Exposure_Lot_wise_COPPER_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_COPPER_Holding"].ToString();
				string MCX_Exposure_Lot_wise_NICKEL_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_NICKEL_Holding"].ToString();
				string MCX_Exposure_Lot_wise_ZINC_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_ZINC_Holding"].ToString();
				string MCX_Exposure_Lot_wise_LEAD_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_LEAD_Holding"].ToString();
				string MCX_Exposure_Lot_wise_NATURALGAS_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_NATURALGAS_Holding"].ToString();
				string MCX_Exposure_Lot_wise_ALUMINIUM_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_ALUMINIUM_Holding"].ToString();
				string MCX_Exposure_Lot_wise_MENTHAOIL_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_MENTHAOIL_Holding"].ToString();
				string MCX_Exposure_Lot_wise_COTTON_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_COTTON_Holding"].ToString();
				string MCX_Exposure_Lot_wise_CPO_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_CPO_Holding"].ToString();
				string MCX_Exposure_Lot_wise_GOLDM_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_GOLDM_Holding"].ToString();
				string MCX_Exposure_Lot_wise_SILVERM_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_SILVERM_Holding"].ToString();
				string MCX_Exposure_Lot_wise_SILVERMIC_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_SILVERMIC_Holding"].ToString();
				string CDS_Brokerage_Type = dt.Rows[0]["CDS_Brokerage_Type"].ToString();
				string CDS_brokerage_per_crore = dt.Rows[0]["CDS_brokerage_per_crore"].ToString();
				string CDS_Exposure_Type = dt.Rows[0]["CDS_Exposure_Type"].ToString();
				string Intraday_Exposure_Margin_CDS = dt.Rows[0]["Intraday_Exposure_Margin_CDS"].ToString();
				string Holding_Exposure_Margin_CDS = dt.Rows[0]["Holding_Exposure_Margin_CDS"].ToString();
				string NSE_Brokerage_Type = dt.Rows[0]["NSE_Brokerage_Type"].ToString();
				string Equity_brokerage_per_crore = dt.Rows[0]["Equity_brokerage_per_crore"].ToString();
				string Intraday_Exposure_Margin_Equity = dt.Rows[0]["Intraday_Exposure_Margin_Equity"].ToString();
				string Holding_Exposure_Margin_Equity = dt.Rows[0]["Holding_Exposure_Margin_Equity"].ToString();
				string NSE_Exposure_Type = dt.Rows[0]["NSE_Exposure_Type"].ToString();
				response += "{";
				response += "\"MCX\":{";
				response = response + "\"IsMCXTrade\":\"" + IsMCXTrade + "\",";
				response = response + "\"Mcx_Brokerage_Type\":\"" + Mcx_Brokerage_Type + "\",";
				if (Mcx_Brokerage_Type == "per_crore")
				{
					response = response + "\"MCX_brokerage_per_crore\":\"" + MCX_brokerage_per_crore + "\",";
				}
				else
				{
					string mcxbrokerage = "{";
					mcxbrokerage = mcxbrokerage + "\"GOLD\":\"" + GOLD_brokerage + "\",";
					mcxbrokerage = mcxbrokerage + "\"BULLDEX\":\"" + BULLDEX_brokerage + "\",";
					mcxbrokerage = mcxbrokerage + "\"SILVER\":\"" + SILVER_brokerage + "\",";
					mcxbrokerage = mcxbrokerage + "\"CRUDEOIL\":\"" + CRUDEOIL_brokerage + "\",";
					mcxbrokerage = mcxbrokerage + "\"COPPER\":\"" + COPPER_brokerage + "\",";
					mcxbrokerage = mcxbrokerage + "\"NICKEL\":\"" + NICKEL_brokerage + "\",";
					mcxbrokerage = mcxbrokerage + "\"ZINC\":\"" + ZINC_brokerage + "\",";
					mcxbrokerage = mcxbrokerage + "\"LEAD\":\"" + LEAD_brokerage + "\",";
					mcxbrokerage = mcxbrokerage + "\"NATURALGAS\":\"" + NATURALGAS_brokerage + "\",";
					mcxbrokerage = mcxbrokerage + "\"ALUMINIUM\":\"" + ALUMINIUM_brokerage + "\",";
					mcxbrokerage = mcxbrokerage + "\"MENTHAOIL\":\"" + MENTHAOIL_brokerage + "\",";
					mcxbrokerage = mcxbrokerage + "\"COTTON\":\"" + COTTON_brokerage + "\",";
					mcxbrokerage = mcxbrokerage + "\"CPO\":\"" + CPO_brokerage + "\",";
					mcxbrokerage = mcxbrokerage + "\"GOLDMINI\":\"" + GOLDM_brokerage + "\",";
					mcxbrokerage = mcxbrokerage + "\"SILVERMINI\":\"" + SILVERM_brokerage + "\",";
					mcxbrokerage = mcxbrokerage + "\"SILVERMIC\":\"" + SILVERMIC_brokerage + "\"";
					mcxbrokerage += "}";
					response = response + "\"MCX_brokerage_per_crore\":" + mcxbrokerage + ",";
				}
				response = response + "\"Mcx_Exposure_Type\":\"" + Mcx_Exposure_Type + "\",";
				if (Mcx_Exposure_Type == "per_turnover")
				{
					response = response + "\"Intraday_Exposure_Margin_MCX\":\"" + Intraday_Exposure_Margin_MCX + "\",";
					response = response + "\"Holding_Exposure_Margin_MCX\":\"" + Holding_Exposure_Margin_MCX + "\"";
				}
				else
				{
					string Intraday_Exposure_Margin_MCXlotvalue = "{";
					Intraday_Exposure_Margin_MCXlotvalue = Intraday_Exposure_Margin_MCXlotvalue + "\"BULLDEX\":\"" + MCX_Exposure_Lot_wise_BULLDEX_Intraday + "\",";
					Intraday_Exposure_Margin_MCXlotvalue = Intraday_Exposure_Margin_MCXlotvalue + "\"GOLD\":\"" + MCX_Exposure_Lot_wise_GOLD_Intraday + "\",";
					Intraday_Exposure_Margin_MCXlotvalue = Intraday_Exposure_Margin_MCXlotvalue + "\"SILVER\":\"" + MCX_Exposure_Lot_wise_SILVER_Intraday + "\",";
					Intraday_Exposure_Margin_MCXlotvalue = Intraday_Exposure_Margin_MCXlotvalue + "\"CRUDEOIL\":\"" + MCX_Exposure_Lot_wise_CRUDEOIL_Intraday + "\",";
					Intraday_Exposure_Margin_MCXlotvalue = Intraday_Exposure_Margin_MCXlotvalue + "\"COPPER\":\"" + MCX_Exposure_Lot_wise_COPPER_Intraday + "\",";
					Intraday_Exposure_Margin_MCXlotvalue = Intraday_Exposure_Margin_MCXlotvalue + "\"NICKEL\":\"" + MCX_Exposure_Lot_wise_NICKEL_Intraday + "\",";
					Intraday_Exposure_Margin_MCXlotvalue = Intraday_Exposure_Margin_MCXlotvalue + "\"ZINC\":\"" + MCX_Exposure_Lot_wise_ZINC_Intraday + "\",";
					Intraday_Exposure_Margin_MCXlotvalue = Intraday_Exposure_Margin_MCXlotvalue + "\"LEAD\":\"" + MCX_Exposure_Lot_wise_LEAD_Intraday + "\",";
					Intraday_Exposure_Margin_MCXlotvalue = Intraday_Exposure_Margin_MCXlotvalue + "\"NATURALGAS\":\"" + MCX_Exposure_Lot_wise_NATURALGAS_Intraday + "\",";
					Intraday_Exposure_Margin_MCXlotvalue = Intraday_Exposure_Margin_MCXlotvalue + "\"ALUMINIUM\":\"" + MCX_Exposure_Lot_wise_ALUMINIUM_Intraday + "\",";
					Intraday_Exposure_Margin_MCXlotvalue = Intraday_Exposure_Margin_MCXlotvalue + "\"MENTHAOIL\":\"" + MCX_Exposure_Lot_wise_MENTHAOIL_Intraday + "\",";
					Intraday_Exposure_Margin_MCXlotvalue = Intraday_Exposure_Margin_MCXlotvalue + "\"COTTON\":\"" + MCX_Exposure_Lot_wise_COTTON_Intraday + "\",";
					Intraday_Exposure_Margin_MCXlotvalue = Intraday_Exposure_Margin_MCXlotvalue + "\"CPO\":\"" + MCX_Exposure_Lot_wise_CPO_Intraday + "\",";
					Intraday_Exposure_Margin_MCXlotvalue = Intraday_Exposure_Margin_MCXlotvalue + "\"SILVERMINI\":\"" + MCX_Exposure_Lot_wise_SILVERM_Intraday + "\",";
					Intraday_Exposure_Margin_MCXlotvalue = Intraday_Exposure_Margin_MCXlotvalue + "\"GOLDMINI\":\"" + MCX_Exposure_Lot_wise_GOLDM_Intraday + "\",";
					Intraday_Exposure_Margin_MCXlotvalue = Intraday_Exposure_Margin_MCXlotvalue + "\"SILVERMIC\":\"" + MCX_Exposure_Lot_wise_SILVERMIC_Intraday + "\"";
					Intraday_Exposure_Margin_MCXlotvalue += "},";
					string Holding_Exposure_Margin_MCXlotvalue = "{";
					Holding_Exposure_Margin_MCXlotvalue = Holding_Exposure_Margin_MCXlotvalue + "\"BULLDEX\":\"" + MCX_Exposure_Lot_wise_BULLDEX_Holding + "\",";
					Holding_Exposure_Margin_MCXlotvalue = Holding_Exposure_Margin_MCXlotvalue + "\"GOLD\":\"" + MCX_Exposure_Lot_wise_GOLD_Holding + "\",";
					Holding_Exposure_Margin_MCXlotvalue = Holding_Exposure_Margin_MCXlotvalue + "\"SILVER\":\"" + MCX_Exposure_Lot_wise_SILVER_Holding + "\",";
					Holding_Exposure_Margin_MCXlotvalue = Holding_Exposure_Margin_MCXlotvalue + "\"CRUDEOIL\":\"" + MCX_Exposure_Lot_wise_CRUDEOIL_Holding + "\",";
					Holding_Exposure_Margin_MCXlotvalue = Holding_Exposure_Margin_MCXlotvalue + "\"COPPER\":\"" + MCX_Exposure_Lot_wise_COPPER_Holding + "\",";
					Holding_Exposure_Margin_MCXlotvalue = Holding_Exposure_Margin_MCXlotvalue + "\"NICKEL\":\"" + MCX_Exposure_Lot_wise_NICKEL_Holding + "\",";
					Holding_Exposure_Margin_MCXlotvalue = Holding_Exposure_Margin_MCXlotvalue + "\"ZINC\":\"" + MCX_Exposure_Lot_wise_ZINC_Holding + "\",";
					Holding_Exposure_Margin_MCXlotvalue = Holding_Exposure_Margin_MCXlotvalue + "\"LEAD\":\"" + MCX_Exposure_Lot_wise_LEAD_Holding + "\",";
					Holding_Exposure_Margin_MCXlotvalue = Holding_Exposure_Margin_MCXlotvalue + "\"NATURALGAS\":\"" + MCX_Exposure_Lot_wise_NATURALGAS_Holding + "\",";
					Holding_Exposure_Margin_MCXlotvalue = Holding_Exposure_Margin_MCXlotvalue + "\"ALUMINIUM\":\"" + MCX_Exposure_Lot_wise_ALUMINIUM_Holding + "\",";
					Holding_Exposure_Margin_MCXlotvalue = Holding_Exposure_Margin_MCXlotvalue + "\"MENTHAOIL\":\"" + MCX_Exposure_Lot_wise_MENTHAOIL_Holding + "\",";
					Holding_Exposure_Margin_MCXlotvalue = Holding_Exposure_Margin_MCXlotvalue + "\"COTTON\":\"" + MCX_Exposure_Lot_wise_COTTON_Holding + "\",";
					Holding_Exposure_Margin_MCXlotvalue = Holding_Exposure_Margin_MCXlotvalue + "\"CPO\":\"" + MCX_Exposure_Lot_wise_CPO_Holding + "\",";
					Holding_Exposure_Margin_MCXlotvalue = Holding_Exposure_Margin_MCXlotvalue + "\"GOLDMINI\":\"" + MCX_Exposure_Lot_wise_GOLDM_Holding + "\",";
					Holding_Exposure_Margin_MCXlotvalue = Holding_Exposure_Margin_MCXlotvalue + "\"SILVERMIC\":\"" + MCX_Exposure_Lot_wise_SILVERMIC_Holding + "\",";
					Holding_Exposure_Margin_MCXlotvalue = Holding_Exposure_Margin_MCXlotvalue + "\"SILVERMINI\":\"" + MCX_Exposure_Lot_wise_SILVERM_Holding + "\"";
					Holding_Exposure_Margin_MCXlotvalue += "}";
					response = response + "\"Intraday_Exposure_Margin_MCX\":" + Intraday_Exposure_Margin_MCXlotvalue;
					response = response + "\"Holding_Exposure_Margin_MCX\":" + Holding_Exposure_Margin_MCXlotvalue;
				}
				response += "},";
				response += "\"NSE\":{";
				response = response + "\"IsNSETrade\":\"" + IsNSETrade + "\",";
				response = response + "\"NSE_Brokerage_Type\":\"" + NSE_Brokerage_Type + "\",";
				response = response + "\"Equity_brokerage_per_crore\":\"" + Equity_brokerage_per_crore + "\",";
				response = response + "\"NSE_Exposure_Type\":\"" + NSE_Exposure_Type + "\",";
				response = response + "\"Intraday_Exposure_Margin_Equity\":\"" + Intraday_Exposure_Margin_Equity + "\",";
				response = response + "\"Holding_Exposure_Margin_Equity\":\"" + Holding_Exposure_Margin_Equity + "\"";
				response += "},";
				response += "\"CDS\":{";
				response = response + "\"IsCDSTrade\":\"" + IsCDSTrade + "\",";
				response = response + "\"CDS_Brokerage_Type\":\"" + CDS_Brokerage_Type + "\",";
				response = response + "\"CDS_brokerage_per_crore\":\"" + CDS_brokerage_per_crore + "\",";
				response = response + "\"CDS_Exposure_Type\":\"" + CDS_Exposure_Type + "\",";
				response = response + "\"Intraday_Exposure_Margin_CDS\":\"" + Intraday_Exposure_Margin_CDS + "\",";
				response = response + "\"Holding_Exposure_Margin_CDS\":\"" + Holding_Exposure_Margin_CDS + "\"";
				response += "}";
				response += "}";
			}
			return response;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0001E7B4 File Offset: 0x0001C9B4
		public string profiledata(string userid)
		{
			string response = "";
			DataTable dt = Universal.SelectWithDS("select IsMCXTrade,IsNSETrade,IsCDSTrade,NSE_Exposure_Type,Mcx_Brokerage_Type,MCX_brokerage_per_crore,BULLDEX_brokerage ,GOLD_brokerage,SILVER_brokerage ,CRUDEOIL_brokerage ,COPPER_brokerage,NICKEL_brokerage ,ZINC_brokerage ,LEAD_brokerage,NATURALGAS_brokerage,ALUMINIUM_brokerage ,MENTHAOIL_brokerage ,COTTON_brokerage,CPO_brokerage, GOLDM_brokerage, SILVERM_brokerage, SILVERMIC_brokerage,ALUMINI_brokerage,CRUDEOILM_brokerage,LEADMINI_brokerage,NATGASMINI_brokerage,ZINCMINI_brokerage,Mcx_Exposure_Type, Intraday_Exposure_Margin_MCX, Holding_Exposure_Margin_MCX, MCX_Exposure_Lot_wise_BULLDEX_Intraday,MCX_Exposure_Lot_wise_ALUMINI_Intraday,MCX_Exposure_Lot_wise_CRUDEOILM_Intraday,MCX_Exposure_Lot_wise_LEADMINI_Intraday,MCX_Exposure_Lot_wise_NATGASMINI_Intraday,MCX_Exposure_Lot_wise_ZINCMINI_Intraday,MCX_Exposure_Lot_wise_BULLDEX_Holding, MCX_Exposure_Lot_wise_GOLD_Intraday, MCX_Exposure_Lot_wise_GOLD_Holding, MCX_Exposure_Lot_wise_SILVER_Intraday, MCX_Exposure_Lot_wise_SILVER_Holding, MCX_Exposure_Lot_wise_CRUDEOIL_Intraday, MCX_Exposure_Lot_wise_CRUDEOIL_Holding, MCX_Exposure_Lot_wise_COPPER_Intraday, MCX_Exposure_Lot_wise_COPPER_Holding, MCX_Exposure_Lot_wise_NICKEL_Intraday, MCX_Exposure_Lot_wise_NICKEL_Holding,MCX_Exposure_Lot_wise_ZINC_Intraday, MCX_Exposure_Lot_wise_ZINC_Holding, MCX_Exposure_Lot_wise_LEAD_Intraday, MCX_Exposure_Lot_wise_LEAD_Holding, MCX_Exposure_Lot_wise_NATURALGAS_Intraday,MCX_Exposure_Lot_wise_ALUMINI_Holding,MCX_Exposure_Lot_wise_CRUDEOILM_Holding,MCX_Exposure_Lot_wise_LEADMINI_Holding,MCX_Exposure_Lot_wise_NATGASMINI_Holding,MCX_Exposure_Lot_wise_ZINCMINI_Holding,MCX_Exposure_Lot_wise_NATURALGAS_Holding,MCX_Exposure_Lot_wise_ALUMINIUM_Intraday, MCX_Exposure_Lot_wise_ALUMINIUM_Holding, MCX_Exposure_Lot_wise_MENTHAOIL_Intraday, MCX_Exposure_Lot_wise_MENTHAOIL_Holding, MCX_Exposure_Lot_wise_COTTON_Intraday, MCX_Exposure_Lot_wise_COTTON_Holding,MCX_Exposure_Lot_wise_CPO_Intraday, MCX_Exposure_Lot_wise_CPO_Holding,MCX_Exposure_Lot_wise_GOLDM_Intraday, MCX_Exposure_Lot_wise_GOLDM_Holding, MCX_Exposure_Lot_wise_SILVERM_Intraday, MCX_Exposure_Lot_wise_SILVERM_Holding, MCX_Exposure_Lot_wise_SILVERMIC_Intraday, MCX_Exposure_Lot_wise_SILVERMIC_Holding,CDS_Brokerage_Type ,CDS_brokerage_per_crore ,CDS_Exposure_Type ,Intraday_Exposure_Margin_CDS ,Holding_Exposure_Margin_CDS,NSE_Brokerage_Type,Equity_brokerage_per_crore,Intraday_Exposure_Margin_Equity,Holding_Exposure_Margin_Equity from t_trading_all_users_master where Id='" + userid + "'", "t_trading_all_users_master");
			if (dt.Rows.Count > 0)
			{
				string IsMCXTrade = dt.Rows[0]["IsMCXTrade"].ToString();
				string IsNSETrade = dt.Rows[0]["IsNSETrade"].ToString();
				string IsCDSTrade = dt.Rows[0]["IsCDSTrade"].ToString();
				string Mcx_Brokerage_Type = dt.Rows[0]["Mcx_Brokerage_Type"].ToString();
				string MCX_brokerage_per_crore = dt.Rows[0]["MCX_brokerage_per_crore"].ToString();
				string BULLDEX_brokerage = dt.Rows[0]["BULLDEX_brokerage"].ToString();
				string GOLD_brokerage = dt.Rows[0]["GOLD_brokerage"].ToString();
				string SILVER_brokerage = dt.Rows[0]["SILVER_brokerage"].ToString();
				string CRUDEOIL_brokerage = dt.Rows[0]["CRUDEOIL_brokerage"].ToString();
				string COPPER_brokerage = dt.Rows[0]["COPPER_brokerage"].ToString();
				string NICKEL_brokerage = dt.Rows[0]["NICKEL_brokerage"].ToString();
				string ZINC_brokerage = dt.Rows[0]["ZINC_brokerage"].ToString();
				string LEAD_brokerage = dt.Rows[0]["LEAD_brokerage"].ToString();
				string NATURALGAS_brokerage = dt.Rows[0]["NATURALGAS_brokerage"].ToString();
				string ALUMINIUM_brokerage = dt.Rows[0]["ALUMINIUM_brokerage"].ToString();
				string MENTHAOIL_brokerage = dt.Rows[0]["MENTHAOIL_brokerage"].ToString();
				string COTTON_brokerage = dt.Rows[0]["COTTON_brokerage"].ToString();
				string CPO_brokerage = dt.Rows[0]["CPO_brokerage"].ToString();
				string GOLDM_brokerage = dt.Rows[0]["GOLDM_brokerage"].ToString();
				string SILVERM_brokerage = dt.Rows[0]["SILVERM_brokerage"].ToString();
				string SILVERMIC_brokerage = dt.Rows[0]["SILVERMIC_brokerage"].ToString();
				string ALUMINI_brokerage = dt.Rows[0]["ALUMINI_brokerage"].ToString();
				string CRUDEOILM_brokerage = dt.Rows[0]["CRUDEOILM_brokerage"].ToString();
				string LEADMINI_brokerage = dt.Rows[0]["LEADMINI_brokerage"].ToString();
				string NATGASMINI_brokerage = dt.Rows[0]["NATGASMINI_brokerage"].ToString();
				string ZINCMINI_brokerage = dt.Rows[0]["ZINCMINI_brokerage"].ToString();
				string Mcx_Exposure_Type = dt.Rows[0]["Mcx_Exposure_Type"].ToString();
				string Intraday_Exposure_Margin_MCX = dt.Rows[0]["Intraday_Exposure_Margin_MCX"].ToString();
				string Holding_Exposure_Margin_MCX = dt.Rows[0]["Holding_Exposure_Margin_MCX"].ToString();
				string MCX_Exposure_Lot_wise_BULLDEX_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_BULLDEX_Intraday"].ToString();
				string MCX_Exposure_Lot_wise_GOLD_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_GOLD_Intraday"].ToString();
				string MCX_Exposure_Lot_wise_ALUMINI_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_ALUMINI_Intraday"].ToString();
				string MCX_Exposure_Lot_wise_CRUDEOILM_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_CRUDEOILM_Intraday"].ToString();
				string MCX_Exposure_Lot_wise_LEADMINI_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_LEADMINI_Intraday"].ToString();
				string MCX_Exposure_Lot_wise_NATGASMINI_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_NATGASMINI_Intraday"].ToString();
				string MCX_Exposure_Lot_wise_ZINCMINI_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_ZINCMINI_Intraday"].ToString();
				string MCX_Exposure_Lot_wise_SILVER_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_SILVER_Intraday"].ToString();
				string MCX_Exposure_Lot_wise_CRUDEOIL_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_CRUDEOIL_Intraday"].ToString();
				string MCX_Exposure_Lot_wise_COPPER_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_COPPER_Intraday"].ToString();
				string MCX_Exposure_Lot_wise_NICKEL_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_NICKEL_Intraday"].ToString();
				string MCX_Exposure_Lot_wise_ZINC_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_ZINC_Intraday"].ToString();
				string MCX_Exposure_Lot_wise_LEAD_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_LEAD_Intraday"].ToString();
				string MCX_Exposure_Lot_wise_NATURALGAS_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_NATURALGAS_Intraday"].ToString();
				string MCX_Exposure_Lot_wise_ALUMINIUM_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_ALUMINIUM_Intraday"].ToString();
				string MCX_Exposure_Lot_wise_MENTHAOIL_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_MENTHAOIL_Intraday"].ToString();
				string MCX_Exposure_Lot_wise_COTTON_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_COTTON_Intraday"].ToString();
				string MCX_Exposure_Lot_wise_CPO_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_CPO_Intraday"].ToString();
				string MCX_Exposure_Lot_wise_SILVERM_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_SILVERM_Intraday"].ToString();
				string MCX_Exposure_Lot_wise_GOLDM_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_GOLDM_Intraday"].ToString();
				string MCX_Exposure_Lot_wise_SILVERMIC_Intraday = dt.Rows[0]["MCX_Exposure_Lot_wise_SILVERMIC_Intraday"].ToString();
				string MCX_Exposure_Lot_wise_BULLDEX_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_BULLDEX_Holding"].ToString();
				string MCX_Exposure_Lot_wise_ALUMINI_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_ALUMINI_Holding"].ToString();
				string MCX_Exposure_Lot_wise_CRUDEOILM_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_CRUDEOILM_Holding"].ToString();
				string MCX_Exposure_Lot_wise_LEADMINI_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_LEADMINI_Holding"].ToString();
				string MCX_Exposure_Lot_wise_NATGASMINI_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_NATGASMINI_Holding"].ToString();
				string MCX_Exposure_Lot_wise_ZINCMINI_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_ZINCMINI_Holding"].ToString();
				string MCX_Exposure_Lot_wise_GOLD_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_GOLD_Holding"].ToString();
				string MCX_Exposure_Lot_wise_SILVER_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_SILVER_Holding"].ToString();
				string MCX_Exposure_Lot_wise_CRUDEOIL_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_CRUDEOIL_Holding"].ToString();
				string MCX_Exposure_Lot_wise_COPPER_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_COPPER_Holding"].ToString();
				string MCX_Exposure_Lot_wise_NICKEL_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_NICKEL_Holding"].ToString();
				string MCX_Exposure_Lot_wise_ZINC_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_ZINC_Holding"].ToString();
				string MCX_Exposure_Lot_wise_LEAD_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_LEAD_Holding"].ToString();
				string MCX_Exposure_Lot_wise_NATURALGAS_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_NATURALGAS_Holding"].ToString();
				string MCX_Exposure_Lot_wise_ALUMINIUM_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_ALUMINIUM_Holding"].ToString();
				string MCX_Exposure_Lot_wise_MENTHAOIL_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_MENTHAOIL_Holding"].ToString();
				string MCX_Exposure_Lot_wise_COTTON_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_COTTON_Holding"].ToString();
				string MCX_Exposure_Lot_wise_CPO_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_CPO_Holding"].ToString();
				string MCX_Exposure_Lot_wise_GOLDM_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_GOLDM_Holding"].ToString();
				string MCX_Exposure_Lot_wise_SILVERM_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_SILVERM_Holding"].ToString();
				string MCX_Exposure_Lot_wise_SILVERMIC_Holding = dt.Rows[0]["MCX_Exposure_Lot_wise_SILVERMIC_Holding"].ToString();
				string CDS_Brokerage_Type = dt.Rows[0]["CDS_Brokerage_Type"].ToString();
				string CDS_brokerage_per_crore = dt.Rows[0]["CDS_brokerage_per_crore"].ToString();
				string CDS_Exposure_Type = dt.Rows[0]["CDS_Exposure_Type"].ToString();
				string Intraday_Exposure_Margin_CDS = dt.Rows[0]["Intraday_Exposure_Margin_CDS"].ToString();
				string Holding_Exposure_Margin_CDS = dt.Rows[0]["Holding_Exposure_Margin_CDS"].ToString();
				string NSE_Brokerage_Type = dt.Rows[0]["NSE_Brokerage_Type"].ToString();
				string Equity_brokerage_per_crore = dt.Rows[0]["Equity_brokerage_per_crore"].ToString();
				string Intraday_Exposure_Margin_Equity = dt.Rows[0]["Intraday_Exposure_Margin_Equity"].ToString();
				string Holding_Exposure_Margin_Equity = dt.Rows[0]["Holding_Exposure_Margin_Equity"].ToString();
				string NSE_Exposure_Type = dt.Rows[0]["NSE_Exposure_Type"].ToString();
				response += "{";
				response += "\"MCX\":{";
				response = response + "\"IsMCXTrade\":\"" + IsMCXTrade + "\",";
				response = response + "\"Mcx_Brokerage_Type\":\"" + Mcx_Brokerage_Type + "\",";
				if (Mcx_Brokerage_Type == "per_crore")
				{
					response = response + "\"MCX_brokerage_per_crore\":\"" + MCX_brokerage_per_crore + "\",";
				}
				else
				{
					string mcxbrokerage = "{";
					mcxbrokerage = mcxbrokerage + "\"GOLD\":\"" + GOLD_brokerage + "\",";
					mcxbrokerage = mcxbrokerage + "\"BULLDEX\":\"" + BULLDEX_brokerage + "\",";
					mcxbrokerage = mcxbrokerage + "\"SILVER\":\"" + SILVER_brokerage + "\",";
					mcxbrokerage = mcxbrokerage + "\"CRUDEOIL\":\"" + CRUDEOIL_brokerage + "\",";
					mcxbrokerage = mcxbrokerage + "\"COPPER\":\"" + COPPER_brokerage + "\",";
					mcxbrokerage = mcxbrokerage + "\"NICKEL\":\"" + NICKEL_brokerage + "\",";
					mcxbrokerage = mcxbrokerage + "\"ZINC\":\"" + ZINC_brokerage + "\",";
					mcxbrokerage = mcxbrokerage + "\"LEAD\":\"" + LEAD_brokerage + "\",";
					mcxbrokerage = mcxbrokerage + "\"NATURALGAS\":\"" + NATURALGAS_brokerage + "\",";
					mcxbrokerage = mcxbrokerage + "\"ALUMINIUM\":\"" + ALUMINIUM_brokerage + "\",";
					mcxbrokerage = mcxbrokerage + "\"MENTHAOIL\":\"" + MENTHAOIL_brokerage + "\",";
					mcxbrokerage = mcxbrokerage + "\"COTTON\":\"" + COTTON_brokerage + "\",";
					mcxbrokerage = mcxbrokerage + "\"CPO\":\"" + CPO_brokerage + "\",";
					mcxbrokerage = mcxbrokerage + "\"GOLDMINI\":\"" + GOLDM_brokerage + "\",";
					mcxbrokerage = mcxbrokerage + "\"SILVERMINI\":\"" + SILVERM_brokerage + "\",";
					mcxbrokerage = mcxbrokerage + "\"SILVERMIC\":\"" + SILVERMIC_brokerage + "\",";
					mcxbrokerage = mcxbrokerage + "\"ALUMINI\":\"" + ALUMINI_brokerage + "\",";
					mcxbrokerage = mcxbrokerage + "\"CRUDEOILM\":\"" + CRUDEOILM_brokerage + "\",";
					mcxbrokerage = mcxbrokerage + "\"LEADMINI\":\"" + LEADMINI_brokerage + "\",";
					mcxbrokerage = mcxbrokerage + "\"NATGASMINI\":\"" + NATGASMINI_brokerage + "\",";
					mcxbrokerage = mcxbrokerage + "\"ZINCMINI\":\"" + ZINCMINI_brokerage + "\"";
					mcxbrokerage += "}";
					response = response + "\"MCX_brokerage_per_crore\":" + mcxbrokerage + ",";
				}
				response = response + "\"Mcx_Exposure_Type\":\"" + Mcx_Exposure_Type + "\",";
				if (Mcx_Exposure_Type == "per_turnover")
				{
					response = response + "\"Intraday_Exposure_Margin_MCX\":\"" + Intraday_Exposure_Margin_MCX + "\",";
					response = response + "\"Holding_Exposure_Margin_MCX\":\"" + Holding_Exposure_Margin_MCX + "\"";
				}
				else
				{
					string Intraday_Exposure_Margin_MCXlotvalue = "{";
					Intraday_Exposure_Margin_MCXlotvalue = Intraday_Exposure_Margin_MCXlotvalue + "\"BULLDEX\":\"" + MCX_Exposure_Lot_wise_BULLDEX_Intraday + "\",";
					Intraday_Exposure_Margin_MCXlotvalue = Intraday_Exposure_Margin_MCXlotvalue + "\"GOLD\":\"" + MCX_Exposure_Lot_wise_GOLD_Intraday + "\",";
					Intraday_Exposure_Margin_MCXlotvalue = Intraday_Exposure_Margin_MCXlotvalue + "\"SILVER\":\"" + MCX_Exposure_Lot_wise_SILVER_Intraday + "\",";
					Intraday_Exposure_Margin_MCXlotvalue = Intraday_Exposure_Margin_MCXlotvalue + "\"CRUDEOIL\":\"" + MCX_Exposure_Lot_wise_CRUDEOIL_Intraday + "\",";
					Intraday_Exposure_Margin_MCXlotvalue = Intraday_Exposure_Margin_MCXlotvalue + "\"COPPER\":\"" + MCX_Exposure_Lot_wise_COPPER_Intraday + "\",";
					Intraday_Exposure_Margin_MCXlotvalue = Intraday_Exposure_Margin_MCXlotvalue + "\"NICKEL\":\"" + MCX_Exposure_Lot_wise_NICKEL_Intraday + "\",";
					Intraday_Exposure_Margin_MCXlotvalue = Intraday_Exposure_Margin_MCXlotvalue + "\"ZINC\":\"" + MCX_Exposure_Lot_wise_ZINC_Intraday + "\",";
					Intraday_Exposure_Margin_MCXlotvalue = Intraday_Exposure_Margin_MCXlotvalue + "\"LEAD\":\"" + MCX_Exposure_Lot_wise_LEAD_Intraday + "\",";
					Intraday_Exposure_Margin_MCXlotvalue = Intraday_Exposure_Margin_MCXlotvalue + "\"NATURALGAS\":\"" + MCX_Exposure_Lot_wise_NATURALGAS_Intraday + "\",";
					Intraday_Exposure_Margin_MCXlotvalue = Intraday_Exposure_Margin_MCXlotvalue + "\"ALUMINIUM\":\"" + MCX_Exposure_Lot_wise_ALUMINIUM_Intraday + "\",";
					Intraday_Exposure_Margin_MCXlotvalue = Intraday_Exposure_Margin_MCXlotvalue + "\"MENTHAOIL\":\"" + MCX_Exposure_Lot_wise_MENTHAOIL_Intraday + "\",";
					Intraday_Exposure_Margin_MCXlotvalue = Intraday_Exposure_Margin_MCXlotvalue + "\"COTTON\":\"" + MCX_Exposure_Lot_wise_COTTON_Intraday + "\",";
					Intraday_Exposure_Margin_MCXlotvalue = Intraday_Exposure_Margin_MCXlotvalue + "\"CPO\":\"" + MCX_Exposure_Lot_wise_CPO_Intraday + "\",";
					Intraday_Exposure_Margin_MCXlotvalue = Intraday_Exposure_Margin_MCXlotvalue + "\"SILVERMINI\":\"" + MCX_Exposure_Lot_wise_SILVERM_Intraday + "\",";
					Intraday_Exposure_Margin_MCXlotvalue = Intraday_Exposure_Margin_MCXlotvalue + "\"GOLDMINI\":\"" + MCX_Exposure_Lot_wise_GOLDM_Intraday + "\",";
					Intraday_Exposure_Margin_MCXlotvalue = Intraday_Exposure_Margin_MCXlotvalue + "\"SILVERMIC\":\"" + MCX_Exposure_Lot_wise_SILVERMIC_Intraday + "\",";
					Intraday_Exposure_Margin_MCXlotvalue = Intraday_Exposure_Margin_MCXlotvalue + "\"ALUMINI\":\"" + MCX_Exposure_Lot_wise_ALUMINI_Intraday + "\",";
					Intraday_Exposure_Margin_MCXlotvalue = Intraday_Exposure_Margin_MCXlotvalue + "\"CRUDEOILM\":\"" + MCX_Exposure_Lot_wise_CRUDEOILM_Intraday + "\",";
					Intraday_Exposure_Margin_MCXlotvalue = Intraday_Exposure_Margin_MCXlotvalue + "\"LEADMINI\":\"" + MCX_Exposure_Lot_wise_LEADMINI_Intraday + "\",";
					Intraday_Exposure_Margin_MCXlotvalue = Intraday_Exposure_Margin_MCXlotvalue + "\"NATGASMINI\":\"" + MCX_Exposure_Lot_wise_NATGASMINI_Intraday + "\",";
					Intraday_Exposure_Margin_MCXlotvalue = Intraday_Exposure_Margin_MCXlotvalue + "\"ZINCMINI\":\"" + MCX_Exposure_Lot_wise_ZINCMINI_Intraday + "\"";
					Intraday_Exposure_Margin_MCXlotvalue += "},";
					string Holding_Exposure_Margin_MCXlotvalue = "{";
					Holding_Exposure_Margin_MCXlotvalue = Holding_Exposure_Margin_MCXlotvalue + "\"BULLDEX\":\"" + MCX_Exposure_Lot_wise_BULLDEX_Holding + "\",";
					Holding_Exposure_Margin_MCXlotvalue = Holding_Exposure_Margin_MCXlotvalue + "\"GOLD\":\"" + MCX_Exposure_Lot_wise_GOLD_Holding + "\",";
					Holding_Exposure_Margin_MCXlotvalue = Holding_Exposure_Margin_MCXlotvalue + "\"SILVER\":\"" + MCX_Exposure_Lot_wise_SILVER_Holding + "\",";
					Holding_Exposure_Margin_MCXlotvalue = Holding_Exposure_Margin_MCXlotvalue + "\"CRUDEOIL\":\"" + MCX_Exposure_Lot_wise_CRUDEOIL_Holding + "\",";
					Holding_Exposure_Margin_MCXlotvalue = Holding_Exposure_Margin_MCXlotvalue + "\"COPPER\":\"" + MCX_Exposure_Lot_wise_COPPER_Holding + "\",";
					Holding_Exposure_Margin_MCXlotvalue = Holding_Exposure_Margin_MCXlotvalue + "\"NICKEL\":\"" + MCX_Exposure_Lot_wise_NICKEL_Holding + "\",";
					Holding_Exposure_Margin_MCXlotvalue = Holding_Exposure_Margin_MCXlotvalue + "\"ZINC\":\"" + MCX_Exposure_Lot_wise_ZINC_Holding + "\",";
					Holding_Exposure_Margin_MCXlotvalue = Holding_Exposure_Margin_MCXlotvalue + "\"LEAD\":\"" + MCX_Exposure_Lot_wise_LEAD_Holding + "\",";
					Holding_Exposure_Margin_MCXlotvalue = Holding_Exposure_Margin_MCXlotvalue + "\"NATURALGAS\":\"" + MCX_Exposure_Lot_wise_NATURALGAS_Holding + "\",";
					Holding_Exposure_Margin_MCXlotvalue = Holding_Exposure_Margin_MCXlotvalue + "\"ALUMINIUM\":\"" + MCX_Exposure_Lot_wise_ALUMINIUM_Holding + "\",";
					Holding_Exposure_Margin_MCXlotvalue = Holding_Exposure_Margin_MCXlotvalue + "\"MENTHAOIL\":\"" + MCX_Exposure_Lot_wise_MENTHAOIL_Holding + "\",";
					Holding_Exposure_Margin_MCXlotvalue = Holding_Exposure_Margin_MCXlotvalue + "\"COTTON\":\"" + MCX_Exposure_Lot_wise_COTTON_Holding + "\",";
					Holding_Exposure_Margin_MCXlotvalue = Holding_Exposure_Margin_MCXlotvalue + "\"CPO\":\"" + MCX_Exposure_Lot_wise_CPO_Holding + "\",";
					Holding_Exposure_Margin_MCXlotvalue = Holding_Exposure_Margin_MCXlotvalue + "\"GOLDMINI\":\"" + MCX_Exposure_Lot_wise_GOLDM_Holding + "\",";
					Holding_Exposure_Margin_MCXlotvalue = Holding_Exposure_Margin_MCXlotvalue + "\"SILVERMIC\":\"" + MCX_Exposure_Lot_wise_SILVERMIC_Holding + "\",";
					Holding_Exposure_Margin_MCXlotvalue = Holding_Exposure_Margin_MCXlotvalue + "\"SILVERMINI\":\"" + MCX_Exposure_Lot_wise_SILVERM_Holding + "\",";
					Holding_Exposure_Margin_MCXlotvalue = Holding_Exposure_Margin_MCXlotvalue + "\"ALUMINI\":\"" + MCX_Exposure_Lot_wise_ALUMINI_Holding + "\",";
					Holding_Exposure_Margin_MCXlotvalue = Holding_Exposure_Margin_MCXlotvalue + "\"CRUDEOILM\":\"" + MCX_Exposure_Lot_wise_CRUDEOILM_Holding + "\",";
					Holding_Exposure_Margin_MCXlotvalue = Holding_Exposure_Margin_MCXlotvalue + "\"LEADMINI\":\"" + MCX_Exposure_Lot_wise_LEADMINI_Holding + "\",";
					Holding_Exposure_Margin_MCXlotvalue = Holding_Exposure_Margin_MCXlotvalue + "\"NATGASMINI\":\"" + MCX_Exposure_Lot_wise_NATGASMINI_Holding + "\",";
					Holding_Exposure_Margin_MCXlotvalue = Holding_Exposure_Margin_MCXlotvalue + "\"ZINCMINI\":\"" + MCX_Exposure_Lot_wise_ZINCMINI_Holding + "\"";
					Holding_Exposure_Margin_MCXlotvalue += "}";
					response = response + "\"Intraday_Exposure_Margin_MCX\":" + Intraday_Exposure_Margin_MCXlotvalue;
					response = response + "\"Holding_Exposure_Margin_MCX\":" + Holding_Exposure_Margin_MCXlotvalue;
				}
				response += "},";
				response += "\"NSE\":{";
				response = response + "\"IsNSETrade\":\"" + IsNSETrade + "\",";
				response = response + "\"NSE_Brokerage_Type\":\"" + NSE_Brokerage_Type + "\",";
				response = response + "\"Equity_brokerage_per_crore\":\"" + Equity_brokerage_per_crore + "\",";
				response = response + "\"NSE_Exposure_Type\":\"" + NSE_Exposure_Type + "\",";
				response = response + "\"Intraday_Exposure_Margin_Equity\":\"" + Intraday_Exposure_Margin_Equity + "\",";
				response = response + "\"Holding_Exposure_Margin_Equity\":\"" + Holding_Exposure_Margin_Equity + "\"";
				response += "},";
				response += "\"CDS\":{";
				response = response + "\"IsCDSTrade\":\"" + IsCDSTrade + "\",";
				response = response + "\"CDS_Brokerage_Type\":\"" + CDS_Brokerage_Type + "\",";
				response = response + "\"CDS_brokerage_per_crore\":\"" + CDS_brokerage_per_crore + "\",";
				response = response + "\"CDS_Exposure_Type\":\"" + CDS_Exposure_Type + "\",";
				response = response + "\"Intraday_Exposure_Margin_CDS\":\"" + Intraday_Exposure_Margin_CDS + "\",";
				response = response + "\"Holding_Exposure_Margin_CDS\":\"" + Holding_Exposure_Margin_CDS + "\"";
				response += "}";
				response += "}";
			}
			return response;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0001F894 File Offset: 0x0001DA94
		[HttpPost]
		public string closeorder(string ordercategory, string ScriptName, string selectedlotsize, string Lot, string orderno, string orderprice, string userid, string SymbolType, string cmp)
		{
			if (DateTime.Now.DayOfWeek.ToString().ToLower() == "saturday" || DateTime.Now.DayOfWeek.ToString().ToLower() == "sunday")
			{
				return "{" + "\"ResponseCode\":\"203\"," + "\"ResponseMessage\":\"Market Not Opened\"" + "}";
			}
			string colname = "";
			if (SymbolType == "MCX")
			{
				colname = "McxStartTrading,McxEndTrading";
			}
			else if (SymbolType == "NSE")
			{
				colname = "EQStartTrading,EQEndTrading";
			}
			else if (SymbolType == "CDS")
			{
				colname = "CdsStartTrading,CdsEndTrading";
			}
			DataTable dt = Universal.SelectWithDS("select " + colname + " from Settings", "tbl");
			if (dt.Rows.Count > 0)
			{
				string _starttime = dt.Rows[0][0].ToString() + ":00";
				string _endttime = dt.Rows[0][1].ToString() + ":00";
				string[] currenttime_sec = (DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":00").Split(new char[]
				{
					':'
				});
				decimal currentseconds = decimal.Parse(currenttime_sec[0]) * 60m * 60m + decimal.Parse(currenttime_sec[1]) * 60m + decimal.Parse(currenttime_sec[2]);
				string[] Startcurrenttime_sec = _starttime.Split(new char[]
				{
					':'
				});
				decimal startseconds = decimal.Parse(Startcurrenttime_sec[0]) * 60m * 60m + decimal.Parse(Startcurrenttime_sec[1]) * 60m + decimal.Parse(Startcurrenttime_sec[2]);
				string[] Endcurrenttime_sec = _endttime.Split(new char[]
				{
					':'
				});
				decimal endseconds = decimal.Parse(Endcurrenttime_sec[0]) * 60m * 60m + decimal.Parse(Endcurrenttime_sec[1]) * 60m + decimal.Parse(Endcurrenttime_sec[2]);
				if (!(currentseconds >= startseconds) || !(currentseconds <= endseconds))
				{
					return "{" + "\"ResponseCode\":\"203\"," + "\"ResponseMessage\":\"Market Not Opened\"" + "}";
				}
			}
			DataTable brokeragetbl = Universal.SelectWithDS("select Mcx_Brokerage_Type,MCX_brokerage_per_crore,NSE_Brokerage_Type,Equity_brokerage_per_crore,CDS_Brokerage_Type,CDS_brokerage_per_crore,BULLDEX_brokerage,GOLD_brokerage,SILVER_brokerage,CRUDEOIL_brokerage,COPPER_brokerage,NICKEL_brokerage,ZINC_brokerage,LEAD_brokerage,NATURALGAS_brokerage,ALUMINIUM_brokerage,MENTHAOIL_brokerage,COTTON_brokerage,CPO_brokerage,GOLDM_brokerage,SILVERM_brokerage,SILVERMIC_brokerage,LedgerBalance,creditLimit from t_trading_all_users_master where Id='" + userid + "'", "t_trading_all_users_master");
			decimal LedgerBalance = 0m;
			decimal finalbrokerage = 0m;
			if (brokeragetbl.Rows.Count > 0)
			{
				LedgerBalance = decimal.Parse(brokeragetbl.Rows[0]["LedgerBalance"].ToString());
			}
			if (SymbolType == "MCX")
			{
				string a = brokeragetbl.Rows[0]["Mcx_Brokerage_Type"].ToString();
				decimal brokerage = decimal.Parse(brokeragetbl.Rows[0]["MCX_brokerage_per_crore"].ToString());
				if (a == "per_lot")
				{
					string[] symarr = ScriptName.Split(new char[]
					{
						'_'
					});
					string similer_syml = this.setsymbol(symarr[0].Trim());
					decimal lotwise_brk = decimal.Parse(brokeragetbl.Rows[0][similer_syml + "_brokerage"].ToString());
					finalbrokerage = decimal.Parse(Lot) * lotwise_brk;
				}
				else
				{
					finalbrokerage = (decimal.Parse(orderprice) + decimal.Parse(cmp)) * decimal.Parse(selectedlotsize) * decimal.Parse(Lot) * brokerage / 10000000m;
				}
			}
			else if (SymbolType == "NSE")
			{
				string a2 = brokeragetbl.Rows[0]["NSE_Brokerage_Type"].ToString();
				decimal brokerage2 = decimal.Parse(brokeragetbl.Rows[0]["NSE_brokerage_per_crore"].ToString());
				if (a2 == "per_lot")
				{
					finalbrokerage = decimal.Parse(Lot) * brokerage2;
				}
				else
				{
					finalbrokerage = (decimal.Parse(orderprice) + decimal.Parse(cmp)) * decimal.Parse(selectedlotsize) * decimal.Parse(Lot) * brokerage2 / 10000000m;
				}
			}
			else if (SymbolType == "CDS")
			{
				string a3 = brokeragetbl.Rows[0]["CDS_Brokerage_Type"].ToString();
				decimal brokerage3 = decimal.Parse(brokeragetbl.Rows[0]["CDS_brokerage_per_crore"].ToString());
				if (a3 == "per_lot")
				{
					finalbrokerage = decimal.Parse(Lot) * brokerage3;
				}
				else
				{
					finalbrokerage = (decimal.Parse(orderprice) + decimal.Parse(cmp)) * decimal.Parse(selectedlotsize) * decimal.Parse(Lot) * brokerage3 / 10000000m;
				}
			}
			decimal diff = 0m;
			if (ordercategory == "SELL")
			{
				diff = decimal.Parse(orderprice) - decimal.Parse(cmp);
			}
			else
			{
				diff = decimal.Parse(cmp) - decimal.Parse(orderprice);
			}
			decimal final_pl = Math.Round(diff * int.Parse(selectedlotsize) * int.Parse(Lot), 1);
			int s = Universal.ExecuteNonQuery(string.Concat(new string[]
			{
				"update t_user_order set P_L='",
				final_pl.ToString(),
				"',OrderStatus='Closed',Brokerage='",
				finalbrokerage.ToString(),
				"',BroughtBy='",
				cmp,
				"',ClosedAt='",
				Universal.GetDate,
				"',ClosedTime='",
				Universal.GetTime,
				"',OrderRemark='OrderClosedFromClientManual' where OrderNo='",
				orderno,
				"'"
			}));
			Universal.ExecuteNonQuery(string.Concat(new string[]
			{
				"update t_trading_all_users_master set LedgerBalance='",
				(LedgerBalance - decimal.Parse(finalbrokerage.ToString()) + final_pl).ToString(),
				"' where Id='",
				userid,
				"'"
			}));
			string msg = "";
			IPAddress externalIp = IPAddress.Parse(Universal.devip);
			if (s == 1)
			{
				string[] array = new string[11];
				array[0] = "insert into t_action_master (Message,ActionDate,ActionTime,ActionFrom,IpAddress) values('";
				array[1] = msg;
				array[2] = "','";
				array[3] = Universal.GetDate;
				array[4] = "','";
				array[5] = Universal.GetTime;
				array[6] = "','";
				array[7] = userid;
				array[8] = "','";
				int num = 9;
				IPAddress ipaddress = externalIp;
				array[num] = ((ipaddress != null) ? ipaddress.ToString() : null);
				array[10] = "')";
				Universal.ExecuteNonQuery(string.Concat(array));
				string appmsg = string.Concat(new string[]
				{
					ScriptName,
					" trades of ",
					Lot,
					" lots has been closed successfully at P/L ",
					final_pl.ToString()
				});
				return "{" + "\"ResponseCode\":\"200\"," + "\"ResponseMessage\":\"" + appmsg + "\"" + "}";
			}
			return "{" + "\"ResponseCode\":\"201\"," + "\"ResponseMessage\":\"Error\"" + "}";
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0002007C File Offset: 0x0001E27C
		public string checktradetime_app(string tradetype, string userid)
		{
			if (DateTime.Now.DayOfWeek.ToString().ToLower() == "saturday" || DateTime.Now.DayOfWeek.ToString().ToLower() == "sunday")
			{
				return "{" + "\"ResponseCode\":\"203\"," + "\"ResponseMessage\":\"Market Not Opened\"" + "}";
			}
			string refid = Universal.ExecuteScalar("SELECT RefId FROM db_tradeing.t_trading_all_users_master where Id='" + userid + "'").ToString();
			string colname = "";
			if (tradetype == "MCX")
			{
				colname = "McxStartTrading,McxEndTrading";
			}
			else if (tradetype == "NSE")
			{
				colname = "EQStartTrading,EQEndTrading";
			}
			else if (tradetype == "CDS")
			{
				colname = "CdsStartTrading,CdsEndTrading";
			}
			DataTable dt = Universal.SelectWithDS(string.Concat(new string[]
			{
				"select ",
				colname,
				" from Settings where UserID='",
				refid,
				"'"
			}), "tbl");
			if (dt.Rows.Count <= 0)
			{
				return "{" + "\"ResponseCode\":\"203\"," + "\"ResponseMessage\":\"User Not Found\"" + "}";
			}
			string _starttime = dt.Rows[0][0].ToString() + ":00";
			string _endttime = dt.Rows[0][1].ToString() + ":00";
			string[] currenttime_sec = (DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":00").Split(new char[]
			{
				':'
			});
			decimal currentseconds = decimal.Parse(currenttime_sec[0]) * 60m * 60m + decimal.Parse(currenttime_sec[1]) * 60m + decimal.Parse(currenttime_sec[2]);
			string[] Startcurrenttime_sec = _starttime.Split(new char[]
			{
				':'
			});
			decimal startseconds = decimal.Parse(Startcurrenttime_sec[0]) * 60m * 60m + decimal.Parse(Startcurrenttime_sec[1]) * 60m + decimal.Parse(Startcurrenttime_sec[2]);
			string[] Endcurrenttime_sec = _endttime.Split(new char[]
			{
				':'
			});
			decimal endseconds = decimal.Parse(Endcurrenttime_sec[0]) * 60m * 60m + decimal.Parse(Endcurrenttime_sec[1]) * 60m + decimal.Parse(Endcurrenttime_sec[2]);
			if (currentseconds >= startseconds && currentseconds <= endseconds)
			{
				return "{" + "\"ResponseCode\":\"200\"," + "\"ResponseMessage\":\"Market Opened\"" + "}";
			}
			return "{" + "\"ResponseCode\":\"203\"," + "\"ResponseMessage\":\"Market Not Opened\"" + "}";
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000203D4 File Offset: 0x0001E5D4
		[HttpPost]
		public string saveorderbyuser(t_order_master obj)
		{
			if (DateTime.Now.DayOfWeek.ToString().ToLower() == "saturday" || DateTime.Now.DayOfWeek.ToString().ToLower() == "sunday")
			{
				return "{" + "\"ResponseCode\":\"203\"," + "\"ResponseMessage\":\"Market Not Opened\"" + "}";
			}
			string refid = Universal.ExecuteScalar("SELECT RefId FROM db_tradeing.t_trading_all_users_master where Id='" + obj.UserId + "'").ToString();
			string symboltype = obj.SymbolType;
			string colname = "";
			if (symboltype == "MCX")
			{
				colname = "McxStartTrading,McxEndTrading";
			}
			else if (symboltype == "NSE")
			{
				colname = "EQStartTrading,EQEndTrading";
			}
			else if (symboltype == "CDS")
			{
				colname = "CdsStartTrading,CdsEndTrading";
			}
			DataTable dt = Universal.SelectWithDS(string.Concat(new string[]
			{
				"select ",
				colname,
				" from Settings where UserID='",
				refid,
				"'"
			}), "tbl");
			if (dt.Rows.Count > 0)
			{
				string _starttime = dt.Rows[0][0].ToString() + ":00";
				string _endttime = dt.Rows[0][1].ToString() + ":00";
				string[] currenttime_sec = (DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":00").Split(new char[]
				{
					':'
				});
				decimal currentseconds = decimal.Parse(currenttime_sec[0]) * 60m * 60m + decimal.Parse(currenttime_sec[1]) * 60m + decimal.Parse(currenttime_sec[2]);
				string[] Startcurrenttime_sec = _starttime.Split(new char[]
				{
					':'
				});
				decimal startseconds = decimal.Parse(Startcurrenttime_sec[0]) * 60m * 60m + decimal.Parse(Startcurrenttime_sec[1]) * 60m + decimal.Parse(Startcurrenttime_sec[2]);
				string[] Endcurrenttime_sec = _endttime.Split(new char[]
				{
					':'
				});
				decimal endseconds = decimal.Parse(Endcurrenttime_sec[0]) * 60m * 60m + decimal.Parse(Endcurrenttime_sec[1]) * 60m + decimal.Parse(Endcurrenttime_sec[2]);
				if (!(currentseconds >= startseconds) || !(currentseconds <= endseconds))
				{
					return "{" + "\"ResponseCode\":\"203\"," + "\"ResponseMessage\":\"Market Not Opened\"" + "}";
				}
			}
			string status = this.checkbeforetrade(obj);
			if (!(status == "true"))
			{
				return "{" + "\"ResponseCode\":\"201\"," + "\"ResponseMessage\":\"" + status + "\"" + "}";
			}
			decimal marginvalue = 0m;
			decimal holdmarginvalue = 0m;
			if (obj.SymbolType == "MCX")
			{
				DataTable dt2 = Universal.SelectWithDS("select Intraday_Exposure_Margin_MCX,Holding_Exposure_Margin_MCX from t_trading_all_users_master where Id='" + obj.UserId + "'", "tbl");
				if (dt2.Rows.Count > 0)
				{
					string Intraday_Exposure_Margin_MCX = dt2.Rows[0]["Intraday_Exposure_Margin_MCX"].ToString();
					string Holding_Exposure_Margin_MCX = dt2.Rows[0]["Holding_Exposure_Margin_MCX"].ToString();
					decimal finallotsize = decimal.Parse(obj.Lot) * decimal.Parse(obj.selectedlotsize);
					marginvalue = decimal.Parse(obj.OrderPrice) * finallotsize / decimal.Parse(Intraday_Exposure_Margin_MCX);
					holdmarginvalue = decimal.Parse(obj.OrderPrice) * finallotsize / decimal.Parse(Holding_Exposure_Margin_MCX);
				}
			}
			else if (obj.SymbolType == "NSE")
			{
				DataTable dt3 = Universal.SelectWithDS("select Intraday_Exposure_Margin_Equity,Holding_Exposure_Margin_Equity from t_trading_all_users_master where Id='" + obj.UserId + "'", "tbl");
				if (dt3.Rows.Count > 0)
				{
					string Intraday_Exposure_Margin_EQUITY = dt3.Rows[0]["Intraday_Exposure_Margin_Equity"].ToString();
					string Holding_Exposure_Margin_EQUITY = dt3.Rows[0]["Holding_Exposure_Margin_Equity"].ToString();
					decimal finallotsize2 = decimal.Parse(obj.Lot) * decimal.Parse(obj.selectedlotsize);
					marginvalue = decimal.Parse(obj.OrderPrice) * finallotsize2 / decimal.Parse(Intraday_Exposure_Margin_EQUITY);
					holdmarginvalue = decimal.Parse(obj.OrderPrice) * finallotsize2 / decimal.Parse(Holding_Exposure_Margin_EQUITY);
				}
			}
			else if (obj.SymbolType == "OPT")
			{
				DataTable dt4 = Universal.SelectWithDS("select Intraday_Exposure_Margin_CDS,Holding_Exposure_Margin_CDS from t_trading_all_users_master where Id='" + obj.UserId + "'", "tbl");
				if (dt4.Rows.Count > 0)
				{
					string Intraday_Exposure_Margin_CDS = dt4.Rows[0]["Intraday_Exposure_Margin_CDS"].ToString();
					string Holding_Exposure_Margin_CDS = dt4.Rows[0]["Holding_Exposure_Margin_CDS"].ToString();
					decimal finallotsize3 = decimal.Parse(obj.Lot) * decimal.Parse(obj.selectedlotsize);
					marginvalue = decimal.Parse(obj.OrderPrice) * finallotsize3 / decimal.Parse(Intraday_Exposure_Margin_CDS);
					holdmarginvalue = decimal.Parse(obj.OrderPrice) * finallotsize3 / decimal.Parse(Holding_Exposure_Margin_CDS);
				}
			}
			obj.MarginUsed = marginvalue.ToString();
			obj.HoldingMarginReq = holdmarginvalue.ToString();
			string date = Universal.GetDate;
			string time = Universal.GetTime;
			obj.OrderDate = date;
			obj.OrderTime = time;
			obj.OrderNo = new Random().Next().ToString();
			string action;
			if (obj.OrderType == "Market")
			{
				if (obj.OrderType == "BUY")
				{
					action = "Bought";
				}
				else
				{
					action = "Sold";
				}
			}
			else
			{
				action = "placed order ";
			}
			(decimal.Parse(obj.OrderPrice) * decimal.Parse(obj.Lot)).ToString();
			string msg = string.Concat(new string[]
			{
				obj.UserName,
				"(",
				obj.UserId,
				") have ",
				action,
				" ",
				obj.Lot,
				" lots of ",
				obj.ScriptName,
				" at ",
				obj.OrderPrice,
				" Traded by client @ ",
				obj.OrderType
			});
			IPAddress externalIp = IPAddress.Parse(Universal.devip);
			if (obj.OrderStatus == "Active")
			{
				string appmsg = this.tradesettlementpost_app(obj);
				return "{" + "\"ResponseCode\":\"200\"," + "\"ResponseMessage\":\"" + appmsg + "\"" + "}";
			}
			Universal.ExecuteNonQuery(string.Concat(new string[]
			{
				"insert into t_user_order (selectedlotsize,OrderDate,OrderCategory,OrderTime,OrderNo,UserId,UserName,OrderType,ScriptName,TokenNo,ActionBy,OrderPrice,Lot,MarginUsed,HoldingMarginReq,OrderStatus,SymbolType,isstoplossorder,OrderRemark) values('",
				obj.selectedlotsize,
				"','",
				date,
				"','",
				obj.OrderCategory,
				"','",
				obj.OrderTime,
				"','",
				obj.OrderNo,
				"','",
				obj.UserId,
				"','",
				obj.UserName,
				"','",
				obj.OrderType,
				"','",
				obj.ScriptName,
				"','",
				obj.TokenNo,
				"','",
				obj.ActionType,
				"','",
				obj.OrderPrice.Trim().Replace(" ", ""),
				"','",
				obj.Lot,
				"','",
				obj.MarginUsed,
				"','",
				obj.HoldingMarginReq,
				"','",
				obj.OrderStatus,
				"','",
				obj.SymbolType,
				"','",
				obj.isstoplossorder,
				"','OrderSavedFromClient')"
			}));
			string[] array = new string[11];
			array[0] = "insert into t_action_master (Message,ActionDate,ActionTime,ActionFrom,IpAddress) values('";
			array[1] = msg;
			array[2] = "','";
			array[3] = Universal.GetDate;
			array[4] = "','";
			array[5] = Universal.GetTime;
			array[6] = "','";
			array[7] = obj.UserId;
			array[8] = "','";
			int num = 9;
			IPAddress ipaddress = externalIp;
			array[num] = ((ipaddress != null) ? ipaddress.ToString() : null);
			array[10] = "')";
			Universal.ExecuteNonQuery(string.Concat(array));
			string json = "{";
			json += "\"ResponseCode\":\"200\",";
			json = string.Concat(new string[]
			{
				json,
				"\"ResponseMessage\":\"Order Placed of ",
				obj.ScriptName,
				"(",
				obj.Lot,
				" Lots) at price ",
				obj.OrderPrice,
				"\""
			});
			return json + "}";
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00020DD8 File Offset: 0x0001EFD8
		public string saveorders(t_order_master obj)
		{
			Universal.ExecuteNonQuery(string.Concat(new string[]
			{
				"delete from tbl_tradepointer where UserId='",
				obj.UserId,
				"';insert into tbl_tradepointer (UserId) values('",
				obj.UserId,
				"')"
			}));
			string response = "";
			string date = Universal.GetDate;
			string time = Universal.GetTime;
			obj.OrderDate = date;
			obj.OrderTime = time;
			obj.OrderNo = new Random().Next().ToString();
			if (obj.OrderType == "Limit")
			{
				if (obj.OrderType == "BUY")
				{
					obj.ActionType = "Bought By Trader";
				}
				else
				{
					obj.ActionType = "Sold By Trader";
				}
			}
			(decimal.Parse(obj.OrderPrice) * decimal.Parse(obj.Lot)).ToString();
			IPAddress externalIp = IPAddress.Parse(Universal.devip);
			if (obj.OrderStatus == "Active")
			{
				this.tradesettlement(obj);
			}
			else if (obj.isedit == "true")
			{
				if (Universal.ExecuteNonQuery(string.Concat(new string[]
				{
					"update t_user_order set OrderDate='",
					date,
					"',OrderTime='",
					obj.OrderTime,
					"',OrderPrice='",
					obj.OrderPrice,
					"',Lot='",
					obj.Lot,
					"',MarginUsed='",
					obj.MarginUsed,
					"',HoldingMarginReq='",
					obj.HoldingMarginReq,
					"',isstoplossorder='",
					obj.isstoplossorder,
					"',OrderRemark='OrderUpdatedFromWeb' where Id='",
					obj.Id,
					"'"
				})) == 1)
				{
					string msg = string.Concat(new string[]
					{
						obj.UserName,
						"(",
						obj.UserId,
						") have updated the order(",
						obj.Id,
						") to ",
						obj.OrderType,
						" ",
						obj.Lot,
						" lots of ",
						obj.ScriptName,
						" at ",
						obj.OrderPrice
					});
					string[] array = new string[11];
					array[0] = "insert into t_action_master (Message,ActionDate,ActionTime,ActionFrom,IpAddress) values('";
					array[1] = msg;
					array[2] = "','";
					array[3] = Universal.GetDate;
					array[4] = "','";
					array[5] = Universal.GetTime;
					array[6] = "','";
					array[7] = obj.UserId;
					array[8] = "','";
					int num = 9;
					IPAddress ipaddress = externalIp;
					array[num] = ((ipaddress != null) ? ipaddress.ToString() : null);
					array[10] = "')";
					Universal.ExecuteNonQuery(string.Concat(array));
					response = "Success";
				}
			}
			else if (Universal.ExecuteNonQuery(string.Concat(new string[]
			{
				"insert into t_user_order (selectedlotsize,OrderDate,OrderCategory,OrderTime,OrderNo,UserId,UserName,OrderType,ScriptName,TokenNo,ActionBy,OrderPrice,Lot,MarginUsed,HoldingMarginReq,OrderStatus,SymbolType,isstoplossorder,OrderRemark) values('",
				obj.selectedlotsize,
				"','",
				date,
				"','",
				obj.OrderCategory,
				"','",
				obj.OrderTime,
				"','",
				obj.OrderNo,
				"','",
				obj.UserId,
				"','",
				obj.UserName,
				"','",
				obj.OrderType,
				"','",
				obj.ScriptName,
				"','",
				obj.TokenNo,
				"','",
				obj.ActionType,
				"','",
				obj.OrderPrice.Trim().Replace(" ", ""),
				"','",
				obj.Lot,
				"','",
				obj.MarginUsed,
				"','",
				obj.HoldingMarginReq,
				"','",
				obj.OrderStatus,
				"','",
				obj.SymbolType,
				"','",
				obj.isstoplossorder,
				"','OrderSavedFromClientManual')"
			})) == 1)
			{
				string msg2 = string.Concat(new string[]
				{
					obj.UserName,
					"(",
					obj.UserId,
					") have set order to ",
					obj.OrderCategory,
					" ",
					obj.OrderType,
					" order of ",
					obj.Lot,
					" lots of ",
					obj.ScriptName,
					" at ",
					obj.OrderPrice.Trim().Replace(" ", "")
				});
				string[] array2 = new string[11];
				array2[0] = "insert into t_action_master (Message,ActionDate,ActionTime,ActionFrom,IpAddress) values('";
				array2[1] = msg2;
				array2[2] = "','";
				array2[3] = Universal.GetDate;
				array2[4] = "','";
				array2[5] = Universal.GetTime;
				array2[6] = "','";
				array2[7] = obj.UserId;
				array2[8] = "','";
				int num2 = 9;
				IPAddress ipaddress2 = externalIp;
				array2[num2] = ((ipaddress2 != null) ? ipaddress2.ToString() : null);
				array2[10] = "')";
				Universal.ExecuteNonQuery(string.Concat(array2));
				response = "Success";
			}
			return response;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0002132C File Offset: 0x0001F52C
		[HttpPost]
		public string contactus(contactus_model obj)
		{
			string json = "{";
			json += "\"ResponseCode\":\"201\",";
			json += "\"ResponseMessage\":\"Error\"";
			json += "}";
			string openaccount;
			if (obj.openaccount == "on")
			{
				openaccount = "Required";
			}
			else
			{
				openaccount = "";
			}
			string rentsoftware;
			if (obj.rentsoftware == "on")
			{
				rentsoftware = "Required";
			}
			else
			{
				rentsoftware = "Not Required";
			}
			string sharesoftware;
			if (obj.sharesoftware == "on")
			{
				sharesoftware = "Required";
			}
			else
			{
				sharesoftware = "";
			}
			string complain;
			if (obj.complain == "on")
			{
				complain = "Required";
			}
			else
			{
				complain = "";
			}
			string whitelevel;
			if (obj.whitelevel == "on")
			{
				whitelevel = "Required";
			}
			else
			{
				whitelevel = "";
			}
			string others;
			if (obj.others == "on")
			{
				others = "Required";
			}
			else
			{
				others = "";
			}
			if (Universal.ExecuteNonQuery(string.Concat(new string[]
			{
				"insert into t_enquiryinfo (name,callno,whatsappno,openaccount,rentsoftware,sharesoftware,complain,whitelevel,others,createdate,message) values('",
				obj.name,
				"','",
				obj.callno,
				"','",
				obj.whatsappno,
				"','",
				openaccount,
				"','",
				rentsoftware,
				"','",
				sharesoftware,
				"','",
				complain,
				"','",
				whitelevel,
				"','",
				others,
				"','",
				Universal.GetDate,
				"','",
				obj.message,
				"')"
			})) == 1)
			{
				string msg = "Thank you for connecting with us!";
				json = "{";
				json += "\"ResponseCode\":\"200\",";
				json = json + "\"ResponseMessage\":\"" + msg + "\"";
				return json + "}";
			}
			return json;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00021554 File Offset: 0x0001F754
		[HttpPost]
		public string getusernotification(string UserId)
		{
			List<usernotification> nlist = new List<usernotification>();
			string year = DateTime.Now.Year.ToString();
			int month = DateTime.Now.Month;
			int date = DateTime.Now.Day;
			string s_month;
			if (month <= 9)
			{
				s_month = "0" + month.ToString();
			}
			else
			{
				s_month = month.ToString();
			}
			string s_date;
			if (date <= 9)
			{
				s_date = "0" + date.ToString();
			}
			else
			{
				s_date = date.ToString();
			}
			string.Concat(new string[]
			{
				year,
				"-",
				s_month,
				"-",
				s_date
			});
			DateTime startdate = this.FirstDayOfWeek(DateTime.Now);
			DateTime enddate = this.LastDayOfWeek(DateTime.Now);
			string sdate = string.Concat(new string[]
			{
				startdate.Year.ToString(),
				"-",
				startdate.Month.ToString(),
				"-",
				startdate.Day.ToString()
			});
			string edate = string.Concat(new string[]
			{
				enddate.Year.ToString(),
				"-",
				enddate.Month.ToString(),
				"-",
				enddate.Day.ToString()
			});
			DataTable dt = Universal.SelectWithDS(string.Concat(new string[]
			{
				"SELECT * FROM db_tradeing.t_Notification where CreatedDate between date('",
				sdate,
				"') and date('",
				edate,
				"') and UserId='",
				UserId,
				"'"
			}), "t_Notification");
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				usernotification obj = new usernotification();
				string Title = dt.Rows[i]["Title"].ToString();
				string Message = dt.Rows[i]["Message"].ToString();
				string CreatedDate = dt.Rows[i]["CreatedDate"].ToString();
				obj.Title = Title;
				obj.Message = Message;
				obj.CreatedDate = CreatedDate;
				nlist.Add(obj);
			}
			return new JavaScriptSerializer().Serialize(nlist);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000217DC File Offset: 0x0001F9DC
		public string canceleallorderfromapp(string UserId, string SymbolType)
		{
			string result;
			try
			{
				DataTable dt = Universal.SelectWithDS("select UserName from t_trading_all_users_master where Id='" + UserId + "'", "t_trading_all_users_master");
				string username = "";
				if (dt.Rows.Count > 0)
				{
					username = dt.Rows[0]["UserName"].ToString();
				}
				if (Universal.ExecuteNonQuery(string.Concat(new string[]
				{
					"delete from t_user_order where UserId='",
					UserId,
					"' and OrderStatus='Pending' and SymbolType='",
					SymbolType,
					"'"
				})) >= 1)
				{
					string msg = string.Concat(new string[]
					{
						username,
						"(",
						UserId,
						") client has been cancelled all pending order of ",
						SymbolType,
						"."
					});
					string msgapp = " All pending order's of " + SymbolType + " have been cancelled successfully";
					IPAddress externalIp = IPAddress.Parse(Universal.devip);
					string[] array = new string[11];
					array[0] = "insert into t_action_master (Message,ActionDate,ActionTime,ActionFrom,IpAddress) values('";
					array[1] = msg;
					array[2] = "','";
					array[3] = Universal.GetDate;
					array[4] = "','";
					array[5] = Universal.GetTime;
					array[6] = "','";
					array[7] = UserId;
					array[8] = "','";
					int num = 9;
					IPAddress ipaddress = externalIp;
					array[num] = ((ipaddress != null) ? ipaddress.ToString() : null);
					array[10] = "')";
					Universal.ExecuteNonQuery(string.Concat(array));
					result = "{" + "\"ResponseCode\":\"200\"," + "\"ResponseMessage\":\"" + msgapp + "\"" + "}";
				}
				else
				{
					result = "{" + "\"ResponseCode\":\"201\"," + "\"ResponseMessage\":\"There is no trade of " + SymbolType + ".\"" + "}";
				}
			}
			catch (Exception ex)
			{
				result = ex.Message;
			}
			return result;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000219A8 File Offset: 0x0001FBA8
		[HttpPost]
		public string getuserclosedorders(string UserId)
		{
			List<t_order_master> orderdata = new List<t_order_master>();
			DataTable dt = Universal.SelectWithDS("SELECT ScriptName,Sum(Lot) as Lots,selectedlotsize, SUM((CASE WHEN OrderCategory = 'SELL' THEN(cast(IFNULL(orderprice, 0) as decimal(10, 2)))else (cast(IFNULL(broughtby, 0) as decimal(10, 2))) END)*Lot)/SUM(Lot) as AvgSell ,SUM((CASE WHEN OrderCategory = 'BUY' THEN(cast(IFNULL(orderprice, 0) as decimal(10, 2))) else(cast(IFNULL(broughtby, 0) as decimal(10, 2))) END)*Lot)/SUM(Lot) as AvgBuy,(cast(IFNULL(SUM(brokerage), 0) as decimal(10, 2))) as Brokerage,   (cast(IFNULL(SUM(P_L), 0) as decimal(10, 2))) as PL, ((cast(IFNULL(SUM(P_L), 0) as decimal(10, 2))) - (cast(IFNULL(SUM(brokerage), 0) as decimal(10, 2)))) as NetPL FROM t_user_order where orderstatus = 'Closed' and UserId = '" + UserId + "'  and week(closedat)= week(convert_tz(now(), '+00:00', '+00:00')) group by ScriptName", "t_user_order");
			if (dt == null)
			{
				return "{" + "\"ResponseCode\":\"500\"," + "\"ResponseMessage\":\" + !! Internal server Error !! + \"" + "}";
			}
			if (dt.Rows.Count != 0)
			{
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					t_order_master ord = new t_order_master();
					ord.ScriptName = dt.Rows[i]["ScriptName"].ToString();
					ord.Lot = dt.Rows[i]["Lots"].ToString();
					ord.selectedlotsize = dt.Rows[i]["selectedlotsize"].ToString();
					ord.Brokerage = Math.Round(decimal.Parse(dt.Rows[i]["Brokerage"].ToString()), 2).ToString();
					ord.OrderPrice = Math.Round(decimal.Parse(dt.Rows[i]["AvgSell"].ToString()), 2).ToString();
					ord.BroughtBy = Math.Round(decimal.Parse(dt.Rows[i]["AvgBuy"].ToString()), 2).ToString();
					ord.P_L = ((decimal.Parse(dt.Rows[i]["AvgSell"].ToString()) - decimal.Parse(dt.Rows[i]["AvgBuy"].ToString())) * decimal.Parse(ord.Lot) * decimal.Parse(ord.selectedlotsize) - decimal.Parse(dt.Rows[i]["Brokerage"].ToString())).ToString();
					ord.MarginUsed = dt.Rows[i]["NetPL"].ToString();
					orderdata.Add(ord);
				}
				return new JavaScriptSerializer().Serialize(orderdata);
			}
			return new JavaScriptSerializer().Serialize(orderdata);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00021C0C File Offset: 0x0001FE0C
		[HttpPost]
		public string getnetpl(string UserId)
		{
			new List<t_order_master>();
			DataTable dt = Universal.SelectWithDS("SELECT Sum(((cast(IFNULL(P_L, 0) as decimal(10,2))) - (cast(IFNULL(brokerage, 0) as decimal(10,2))))) as NetPL FROM t_user_order where orderstatus='Closed' and UserId='" + UserId + "' and week(closedat)=week(convert_tz(now(),'+00:00','+00:00'))", "t_user_order");
			if (dt == null)
			{
				return "{" + "\"ResponseCode\":\"500\"," + "\"ResponseMessage\":\" + !! Internal server Error !! + \"" + "}";
			}
			if (dt.Rows.Count != 0)
			{
				t_order_master ord = new t_order_master();
				ord.P_L = dt.Rows[0]["NetPL"].ToString();
				return new JavaScriptSerializer().Serialize(ord);
			}
			return "{" + "\"ResponseCode\":\"201\"," + "\"ResponseMessage\":\" + 00.00 + \"" + "}";
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00021CC4 File Offset: 0x0001FEC4
		public string getnetplbrkg(string UserId, string fromdate, string todate)
		{
			DataTable dt = Universal.SelectWithDS(string.Concat(new string[]
			{
				"select ROUND(SUM(P_L), 2) AS ProfitLoss, ROUND(SUM(Brokerage), 2) AS Brokerage from t_user_order where UserId='",
				UserId,
				"' and OrderStatus='Closed' and Date(ClosedAt) between '",
				fromdate,
				"' and '",
				todate,
				"';"
			}), "t_user_order");
			int count = dt.Rows.Count;
			return "{" + "\"ProfitLoss\":\"" + dt.Rows[0]["ProfitLoss"].ToString() + "\"," + "\"Brokerage\":\" " + dt.Rows[0]["Brokerage"].ToString() + " \"" + "}";
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00021D84 File Offset: 0x0001FF84
		[HttpPost]
		public string getuserclosedorders_today(string UserId)
		{
			List<t_order_master> orderdata = new List<t_order_master>();
			DataTable dt = Universal.SelectWithDS(string.Concat(new string[]
			{
				"SELECT ScriptName,Sum(Lot) as Lots,selectedlotsize, SUM((CASE WHEN OrderCategory = 'SELL' THEN(cast(IFNULL(orderprice, 0) as decimal(10, 2)))else (cast(IFNULL(broughtby, 0) as decimal(10, 2))) END)*Lot)/SUM(Lot) as AvgSell ,SUM((CASE WHEN OrderCategory = 'BUY' THEN(cast(IFNULL(orderprice, 0) as decimal(10, 2))) else(cast(IFNULL(broughtby, 0) as decimal(10, 2))) END)*Lot)/SUM(Lot) as AvgBuy,(cast(IFNULL(SUM(brokerage), 0) as decimal(10, 2))) as Brokerage,   (cast(IFNULL(SUM(P_L), 0) as decimal(10, 2))) as PL, ((cast(IFNULL(SUM(P_L), 0) as decimal(10, 2))) - (cast(IFNULL(SUM(brokerage), 0) as decimal(10, 2)))) as NetPL FROM t_user_order where orderstatus = 'Closed' and UserId = '",
				UserId,
				"'  and closedat='",
				Universal.GetDate,
				"' group by ScriptName"
			}), "t_user_order");
			if (dt == null)
			{
				return "{" + "\"ResponseCode\":\"500\"," + "\"ResponseMessage\":\" + !! Internal server Error !! + \"" + "}";
			}
			if (dt.Rows.Count != 0)
			{
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					t_order_master ord = new t_order_master();
					ord.ScriptName = dt.Rows[i]["ScriptName"].ToString();
					ord.Lot = dt.Rows[i]["Lots"].ToString();
					ord.selectedlotsize = dt.Rows[i]["selectedlotsize"].ToString();
					ord.Brokerage = Math.Round(decimal.Parse(dt.Rows[i]["Brokerage"].ToString()), 2).ToString();
					ord.OrderPrice = Math.Round(decimal.Parse(dt.Rows[i]["AvgSell"].ToString()), 2).ToString();
					ord.BroughtBy = Math.Round(decimal.Parse(dt.Rows[i]["AvgBuy"].ToString()), 2).ToString();
					ord.P_L = ((decimal.Parse(dt.Rows[i]["AvgSell"].ToString()) - decimal.Parse(dt.Rows[i]["AvgBuy"].ToString())) * decimal.Parse(ord.Lot) * decimal.Parse(ord.selectedlotsize) - decimal.Parse(dt.Rows[i]["Brokerage"].ToString())).ToString();
					ord.MarginUsed = dt.Rows[i]["NetPL"].ToString();
					orderdata.Add(ord);
				}
				return new JavaScriptSerializer().Serialize(orderdata);
			}
			return new JavaScriptSerializer().Serialize(orderdata);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00022008 File Offset: 0x00020208
		[HttpPost]
		public string getnetpl_today(string UserId)
		{
			new List<t_order_master>();
			DataTable dt = Universal.SelectWithDS(string.Concat(new string[]
			{
				"SELECT Sum(((cast(IFNULL(P_L, 0) as decimal(10,2))) - (cast(IFNULL(brokerage, 0) as decimal(10,2))))) as NetPL FROM t_user_order where orderstatus='Closed' and UserId='",
				UserId,
				"' and closedat='",
				Universal.GetDate,
				"'"
			}), "t_user_order");
			if (dt == null)
			{
				return "{" + "\"ResponseCode\":\"500\"," + "\"ResponseMessage\":\" + !! Internal server Error !! + \"" + "}";
			}
			if (dt.Rows.Count != 0)
			{
				t_order_master ord = new t_order_master();
				ord.P_L = dt.Rows[0]["NetPL"].ToString();
				return new JavaScriptSerializer().Serialize(ord);
			}
			return "{" + "\"ResponseCode\":\"201\"," + "\"ResponseMessage\":\" + 00.00 + \"" + "}";
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000220E0 File Offset: 0x000202E0
		[HttpPost]
		public string closealltradesbyapp(t_all_trades_close trades)
		{
			string result;
			try
			{
				List<t_all_trades_details> tradesdetails = new JavaScriptSerializer().Deserialize<List<t_all_trades_details>>(trades.idcmp);
				DataTable dtuser = Universal.SelectWithDS("select LedgerBalance,UserName,Mcx_Brokerage_Type, MCX_brokerage_per_crore,NSE_Brokerage_Type,Equity_brokerage_per_crore,CDS_Brokerage_Type,CDS_brokerage_per_crore,BULLDEX_brokerage,GOLD_brokerage,SILVER_brokerage,CRUDEOIL_brokerage,COPPER_brokerage,NICKEL_brokerage,ZINC_brokerage,LEAD_brokerage,NATURALGAS_brokerage,ALUMINIUM_brokerage,MENTHAOIL_brokerage,COTTON_brokerage,CPO_brokerage,GOLDM_brokerage,SILVERM_brokerage,SILVERMIC_brokerage from t_trading_all_users_master where  id='" + trades.userid + "'", "t_trading_all_users_master");
				string Mcx_Brokerage_Type = dtuser.Rows[0]["Mcx_Brokerage_Type"].ToString();
				string MCX_brokerage_per_crore = dtuser.Rows[0]["MCX_brokerage_per_crore"].ToString();
				string NSE_Brokerage_Type = dtuser.Rows[0]["NSE_Brokerage_Type"].ToString();
				string Equity_brokerage_per_crore = dtuser.Rows[0]["Equity_brokerage_per_crore"].ToString();
				string CDS_Brokerage_Type = dtuser.Rows[0]["CDS_Brokerage_Type"].ToString();
				string CDS_brokerage_per_crore = dtuser.Rows[0]["CDS_brokerage_per_crore"].ToString();
				string lb = dtuser.Rows[0]["LedgerBalance"].ToString();
				string UserName = dtuser.Rows[0]["UserName"].ToString();
				decimal totalbrok = 0m;
				decimal totalPL = 0m;
				if (dtuser != null)
				{
					if (trades != null)
					{
						foreach (t_all_trades_details obj in tradesdetails)
						{
							DataTable dataTable = Universal.SelectWithDS(string.Concat(new string[]
							{
								"select selectedlotsize,ScriptName,OrderPrice,OrderCategory,SymbolType,Lot from t_user_order where id='",
								obj.orderid,
								"' and UserId='",
								trades.userid,
								"'"
							}), "t_user_order");
							string ScriptName = dataTable.Rows[0]["ScriptName"].ToString();
							string OrderPrice = dataTable.Rows[0]["OrderPrice"].ToString();
							string selectedlotsize = dataTable.Rows[0]["selectedlotsize"].ToString();
							string Lot = dataTable.Rows[0]["Lot"].ToString();
							string OrderCategory = dataTable.Rows[0]["OrderCategory"].ToString();
							string SymbolType = dataTable.Rows[0]["SymbolType"].ToString();
							decimal brokerage = 0m;
							if (SymbolType == "MCX")
							{
								if (Mcx_Brokerage_Type == "per_lot")
								{
									string[] symarr = ScriptName.Split(new char[]
									{
										'_'
									});
									string similer_syml = this.setsymbol(symarr[0].Trim());
									brokerage = decimal.Parse(dtuser.Rows[0][similer_syml + "_brokerage"].ToString());
									brokerage = decimal.Parse(Lot) * brokerage;
								}
								else
								{
									brokerage = (decimal.Parse(OrderPrice) + decimal.Parse(obj.cmp)) * decimal.Parse(selectedlotsize) * decimal.Parse(Lot) * decimal.Parse(MCX_brokerage_per_crore) / 10000000m;
								}
							}
							else if (SymbolType == "NSE")
							{
								if (NSE_Brokerage_Type == "per_lot")
								{
									brokerage = decimal.Parse(Lot) * decimal.Parse(Equity_brokerage_per_crore);
								}
								else
								{
									brokerage = (decimal.Parse(OrderPrice) + decimal.Parse(obj.cmp)) * decimal.Parse(selectedlotsize) * decimal.Parse(Lot) * decimal.Parse(Equity_brokerage_per_crore) / 10000000m;
								}
							}
							else if (SymbolType == "CDS")
							{
								if (CDS_Brokerage_Type == "per_lot")
								{
									brokerage = decimal.Parse(Lot) * decimal.Parse(CDS_brokerage_per_crore);
								}
								else
								{
									brokerage = (decimal.Parse(OrderPrice) + decimal.Parse(obj.cmp)) * decimal.Parse(selectedlotsize) * decimal.Parse(Lot) * decimal.Parse(CDS_brokerage_per_crore) / 10000000m;
								}
							}
							decimal pl = 0m;
							string ActionCloseBy;
							if (OrderCategory == "SELL")
							{
								ActionCloseBy = "Bought By Trader";
								pl = decimal.Parse(OrderPrice) - decimal.Parse(obj.cmp);
							}
							else
							{
								ActionCloseBy = "Sold By Trader";
								pl = decimal.Parse(obj.cmp) - decimal.Parse(OrderPrice);
							}
							decimal final_lp = Math.Round(pl * int.Parse(selectedlotsize) * int.Parse(Lot), 1);
							int i = Universal.ExecuteNonQuery(string.Concat(new string[]
							{
								"update t_user_order set OrderStatus='Closed', OrderTypeClose='Market',ActionByClose='",
								ActionCloseBy,
								"',BroughtBy='",
								obj.cmp,
								"',P_L='",
								final_lp.ToString(),
								"',Brokerage='",
								brokerage.ToString(),
								"',ClosedAt='",
								Universal.GetDate,
								"',Remark='Close All ",
								SymbolType,
								" Tardes',ClosedTime='",
								Universal.GetTime,
								"' where Id='",
								obj.orderid,
								"'"
							}));
							string msg = string.Concat(new string[]
							{
								UserName,
								"(",
								trades.userid,
								") have ",
								OrderCategory,
								" ",
								Lot,
								" Lots of ",
								ScriptName,
								" at ",
								OrderPrice,
								" with ",
								final_lp.ToString(),
								" Net P/L. Closed at portfolio Under by ",
								SymbolType,
								" Trades."
							});
							IPAddress externalIp = IPAddress.Parse(Universal.devip);
							if (i > 0)
							{
								string[] array = new string[11];
								array[0] = "insert into t_action_master(Message, ActionDate, ActionTime, ActionFrom, IpAddress) values('";
								array[1] = msg;
								array[2] = "', '";
								array[3] = Universal.GetDate;
								array[4] = "', '";
								array[5] = Universal.GetTime;
								array[6] = "', '";
								array[7] = trades.userid;
								array[8] = "', '";
								int num = 9;
								IPAddress ipaddress = externalIp;
								array[num] = ((ipaddress != null) ? ipaddress.ToString() : null);
								array[10] = "')";
								Universal.ExecuteNonQuery(string.Concat(array));
							}
							totalbrok += brokerage;
							totalPL += final_lp;
						}
						Universal.ExecuteNonQuery(string.Concat(new string[]
						{
							"update t_trading_all_users_master set LedgerBalance='",
							Math.Round(decimal.Parse(lb) - totalbrok + totalPL, 1).ToString(),
							"' where Id='",
							trades.userid,
							"'"
						}));
						result = "{" + "\"ResponseCode\":\"200\"," + "\"ResponseMessage\":\" + !! All Trades successfully closed. !! + \"" + "}";
					}
					else
					{
						result = "{" + "\"ResponseCode\":\"201\"," + "\"ResponseMessage\":\" + !! There no trades to close. !! + \"" + "}";
					}
				}
				else
				{
					result = "{" + "\"ResponseCode\":\"500\"," + "\"ResponseMessage\":\" + !! Internal server Error !! + \"" + "}";
				}
			}
			catch (Exception ex)
			{
				result = "{" + "\"ResponseCode\":\"500\"," + "\"ResponseMessage\":\" " + ex.Message + "\"" + "}";
			}
			return result;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000228B8 File Offset: 0x00020AB8
		public string setsymbol(string val)
		{
			if (val == "ALUMINI")
			{
				return "ALUMINI";
			}
			if (val == "ALUMINIUM")
			{
				return "ALUMINIUM";
			}
			if (val == "ALUMINIUMINI")
			{
				return "ALUMINIUM";
			}
			if (val == "COPPER")
			{
				return "COPPER";
			}
			if (val == "COPPERMINI")
			{
				return "COPPER";
			}
			if (val == "CRUDEOIL")
			{
				return "CRUDEOIL";
			}
			if (val == "CRUDEMINI")
			{
				return "CRUDEOIL";
			}
			if (val == "CRUDEOILM")
			{
				return "CRUDEOILM";
			}
			if (val == "GOLD")
			{
				return "GOLD";
			}
			if (val == "GOLDM")
			{
				return "GOLDM";
			}
			if (val == "GOLDMINI")
			{
				return "GOLDM";
			}
			if (val == "LEAD")
			{
				return "LEAD";
			}
			if (val == "LEADMINI")
			{
				return "LEADMINI";
			}
			if (val == "LEADM")
			{
				return "LEAD";
			}
			if (val == "MENTHAOIL")
			{
				return "MENTHAOIL";
			}
			if (val == "NATURALGAS")
			{
				return "NATURALGAS";
			}
			if (val == "NATGASMINI")
			{
				return "NATGASMINI";
			}
			if (val == "NATURALGASM")
			{
				return "NATURALGAS";
			}
			if (val == "NICKEL")
			{
				return "NICKEL";
			}
			if (val == "SILVER")
			{
				return "SILVER";
			}
			if (val == "SILVERM")
			{
				return "SILVERM";
			}
			if (val == "SILVERMINI")
			{
				return "SILVERM";
			}
			if (val == "SILVERMIC")
			{
				return "SILVERMIC";
			}
			if (val == "ZINC")
			{
				return "ZINC";
			}
			if (val == "ZINCM")
			{
				return "ZINC";
			}
			if (val == "ZINCMINI")
			{
				return "ZINCMINI";
			}
			return val;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00022AB4 File Offset: 0x00020CB4
		public void closealltrades(t_all_trades_close trades)
		{
			try
			{
				foreach (t_all_trades_details o in new JavaScriptSerializer().Deserialize<List<t_all_trades_details>>(trades.idcmp))
				{
					string msg = string.Concat(new string[]
					{
						trades.username,
						"(",
						trades.userid,
						") have ",
						o.ordercat,
						" ",
						o.lot,
						" Lots of ",
						o.scriptname,
						" at ",
						o.orderprice,
						". Auto Closed as account gone short of margin."
					});
					IPAddress externalIp = IPAddress.Parse(Universal.devip);
					string ActionCloseBy;
					if (o.ordercat == "SELL")
					{
						ActionCloseBy = "Bought By Admin";
					}
					else
					{
						ActionCloseBy = "Sold By Admin";
					}
					if (Universal.ExecuteNonQuery(string.Concat(new string[]
					{
						"update t_user_order set OrderStatus='Closed',OrderTypeClose='I-FUND',ActionByClose='",
						ActionCloseBy,
						"',BroughtBy='",
						o.cmp,
						"',P_L='",
						o.acpl,
						"',Brokerage='",
						o.brokrg,
						"',ClosedAt='",
						Universal.GetDate,
						"',Remark='Client Auto',ClosedTime='",
						Universal.GetTime,
						"' where Id='",
						o.orderid,
						"'"
					})) == 1)
					{
						string[] array = new string[11];
						array[0] = "insert into t_action_master(Message, ActionDate, ActionTime, ActionFrom, IpAddress) values('";
						array[1] = msg;
						array[2] = "', '";
						array[3] = Universal.GetDate;
						array[4] = "', '";
						array[5] = Universal.GetTime;
						array[6] = "', '";
						array[7] = trades.userid;
						array[8] = "', '";
						int num = 9;
						IPAddress ipaddress = externalIp;
						array[num] = ((ipaddress != null) ? ipaddress.ToString() : null);
						array[10] = "')";
						Universal.ExecuteNonQuery(string.Concat(array));
					}
				}
				Universal.ExecuteNonQuery("delete from t_user_order where UserId='" + trades.userid + "' and OrderStatus='Pending'");
				Universal.ExecuteNonQuery(string.Concat(new string[]
				{
					"update t_trading_all_users_master set LedgerBalance='",
					trades.lb,
					"' where Id='",
					trades.userid,
					"'"
				}));
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00022D40 File Offset: 0x00020F40
		private static string AddTime(string time, int minutesToAdd)
		{
			return TimeSpan.Parse(time).Add(new TimeSpan(0, minutesToAdd, 0)).ToString("hh\\:mm\\:ss");
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00022D70 File Offset: 0x00020F70
		public bool checktime(string orderdate, string globaltimeforclose, int minutecount)
		{
			DateTime datecheck = DateTime.Now;
			string d;
			if (datecheck.Day <= 9)
			{
				d = "0" + datecheck.Day.ToString();
			}
			else
			{
				d = datecheck.Day.ToString();
			}
			string i;
			if (datecheck.Month <= 9)
			{
				i = "0" + datecheck.Month.ToString();
			}
			else
			{
				i = datecheck.Month.ToString();
			}
			if (!(string.Concat(new string[]
			{
				datecheck.Year.ToString(),
				"-",
				i,
				"-",
				d
			}) == orderdate))
			{
				return true;
			}
			string minm = apiController.AddTime(globaltimeforclose, minutecount);
			string[] currenttime_sec = string.Concat(new string[]
			{
				datecheck.Hour.ToString(),
				":",
				datecheck.Minute.ToString(),
				":",
				datecheck.Second.ToString()
			}).Split(new char[]
			{
				':'
			});
			int currentseconds = int.Parse(currenttime_sec[0]) * 60 * 60 + int.Parse(currenttime_sec[1]) * 60 + int.Parse(currenttime_sec[2]);
			string[] Startcurrenttime_sec = minm.Split(new char[]
			{
				':'
			});
			int startseconds = int.Parse(Startcurrenttime_sec[0]) * 60 * 60 + int.Parse(Startcurrenttime_sec[1]) * 60;
			if (currentseconds <= startseconds)
			{
				Console.WriteLine("You can close this active position after " + minutecount.ToString() + " Minutes.");
				return false;
			}
			return true;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00022F2C File Offset: 0x0002112C
		public string closetrade_from_account_portfolio(string userid, string ordercat, string tokenno, string cmpval)
		{
			string result;
			try
			{
				Universal.ExecuteNonQuery(string.Concat(new string[]
				{
					"delete from tbl_tradepointer where UserId='",
					userid,
					"';insert into tbl_tradepointer (UserId) values('",
					userid,
					"')"
				}));
				DataTable dtuser = Universal.SelectWithDS("select profittradestoptime,LedgerBalance,UserName,Mcx_Brokerage_Type, MCX_brokerage_per_crore,NSE_Brokerage_Type,Equity_brokerage_per_crore,CDS_Brokerage_Type,CDS_brokerage_per_crore,BULLDEX_brokerage,GOLD_brokerage,SILVER_brokerage,CRUDEOIL_brokerage,COPPER_brokerage,NICKEL_brokerage,ZINC_brokerage,LEAD_brokerage,NATURALGAS_brokerage,ALUMINIUM_brokerage,MENTHAOIL_brokerage,COTTON_brokerage,CPO_brokerage,GOLDM_brokerage,SILVERM_brokerage,SILVERMIC_brokerage,ALUMINI_brokerage,CRUDEOILM_brokerage,LEADMINI_brokerage,NATGASMINI_brokerage,ZINCMINI_brokerage from t_trading_all_users_master where id='" + userid + "'", "t_trading_all_users_master");
				string Mcx_Brokerage_Type = dtuser.Rows[0]["Mcx_Brokerage_Type"].ToString();
				string MCX_brokerage_per_crore = dtuser.Rows[0]["MCX_brokerage_per_crore"].ToString();
				string NSE_Brokerage_Type = dtuser.Rows[0]["NSE_Brokerage_Type"].ToString();
				string Equity_brokerage_per_crore = dtuser.Rows[0]["Equity_brokerage_per_crore"].ToString();
				string CDS_Brokerage_Type = dtuser.Rows[0]["CDS_Brokerage_Type"].ToString();
				string CDS_brokerage_per_crore = dtuser.Rows[0]["CDS_brokerage_per_crore"].ToString();
				string lb = dtuser.Rows[0]["LedgerBalance"].ToString();
				string UserName = dtuser.Rows[0]["UserName"].ToString();
				string profittradestoptime = dtuser.Rows[0]["profittradestoptime"].ToString();
				decimal totalbrok = 0m;
				decimal totalPL = 0m;
				if (dtuser != null)
				{
					DataTable orderlist = Universal.SelectWithDS(string.Concat(new string[]
					{
						"select Id,ScriptName,OrderPrice,selectedlotsize,Lot,OrderCategory,SymbolType,DATE_FORMAT(OrderDate,'%Y-%m-%d') as OrderDate,OrderTime from t_user_order where UserId='",
						userid,
						"' and OrderCategory='",
						ordercat,
						"' and TokenNo='",
						tokenno,
						"' and OrderStatus='Active'"
					}), "tbl_ord");
					for (int r = 0; r < orderlist.Rows.Count; r++)
					{
						string orderid = orderlist.Rows[r]["Id"].ToString();
						string ScriptName = orderlist.Rows[r]["ScriptName"].ToString();
						string OrderPrice = orderlist.Rows[r]["OrderPrice"].ToString();
						string selectedlotsize = orderlist.Rows[r]["selectedlotsize"].ToString();
						string Lot = orderlist.Rows[r]["Lot"].ToString();
						string OrderCategory = orderlist.Rows[r]["OrderCategory"].ToString();
						string SymbolType = orderlist.Rows[r]["SymbolType"].ToString();
						string OrderDate = orderlist.Rows[r]["OrderDate"].ToString();
						string OrderTime = orderlist.Rows[r]["OrderTime"].ToString();
						if (this.checktime(OrderDate, OrderTime, int.Parse(profittradestoptime)))
						{
							decimal brokerage = 0m;
							if (SymbolType == "MCX")
							{
								if (Mcx_Brokerage_Type == "per_lot")
								{
									string[] symarr = ScriptName.Split(new char[]
									{
										'_'
									});
									string similer_syml = this.setsymbol(symarr[0].Trim());
									brokerage = decimal.Parse(dtuser.Rows[0][similer_syml + "_brokerage"].ToString());
									brokerage = decimal.Parse(Lot) * brokerage;
								}
								else
								{
									brokerage = (decimal.Parse(OrderPrice) + decimal.Parse(cmpval)) * decimal.Parse(selectedlotsize) * decimal.Parse(Lot) * decimal.Parse(MCX_brokerage_per_crore) / 10000000m;
								}
							}
							else if (SymbolType == "NSE")
							{
								if (NSE_Brokerage_Type == "per_lot")
								{
									brokerage = decimal.Parse(Lot) * decimal.Parse(Equity_brokerage_per_crore);
								}
								else
								{
									brokerage = (decimal.Parse(OrderPrice) + decimal.Parse(cmpval)) * decimal.Parse(selectedlotsize) * decimal.Parse(Lot) * decimal.Parse(Equity_brokerage_per_crore) / 10000000m;
								}
							}
							else if (SymbolType == "OPT")
							{
								if (CDS_Brokerage_Type == "per_lot")
								{
									brokerage = decimal.Parse(Lot) * decimal.Parse(CDS_brokerage_per_crore);
								}
								else
								{
									brokerage = (decimal.Parse(OrderPrice) + decimal.Parse(cmpval)) * decimal.Parse(selectedlotsize) * decimal.Parse(Lot) * decimal.Parse(CDS_brokerage_per_crore) / 10000000m;
								}
							}
							decimal pl = 0m;
							string ActionCloseBy;
							if (OrderCategory == "SELL")
							{
								ActionCloseBy = "Bought By Trader";
								pl = decimal.Parse(OrderPrice) - decimal.Parse(cmpval);
							}
							else
							{
								ActionCloseBy = "Sold By Trader";
								pl = decimal.Parse(cmpval) - decimal.Parse(OrderPrice);
							}
							decimal final_lp = Math.Round(pl * int.Parse(selectedlotsize) * int.Parse(Lot), 1);
							int i = Universal.ExecuteNonQuery(string.Concat(new string[]
							{
								"update t_user_order set OrderStatus='Closed', OrderTypeClose='Market',ActionByClose='",
								ActionCloseBy,
								"',BroughtBy='",
								cmpval,
								"',P_L='",
								final_lp.ToString(),
								"',Brokerage='",
								brokerage.ToString(),
								"',ClosedAt='",
								Universal.GetDate,
								"',Remark='Portfolio Close',ClosedTime='",
								Universal.GetTime,
								"' where Id='",
								orderid,
								"'"
							}));
							string msg = string.Concat(new string[]
							{
								UserName,
								"(",
								userid,
								") have ",
								OrderCategory,
								" ",
								Lot,
								" Lots of ",
								ScriptName,
								" at ",
								cmpval,
								". Closed Under by ",
								SymbolType,
								" Trades."
							});
							IPAddress externalIp = IPAddress.Parse(Universal.devip);
							if (i > 0)
							{
								string[] array = new string[11];
								array[0] = "insert into t_action_master(Message, ActionDate, ActionTime, ActionFrom, IpAddress) values('";
								array[1] = msg;
								array[2] = "', '";
								array[3] = Universal.GetDate;
								array[4] = "', '";
								array[5] = Universal.GetTime;
								array[6] = "', '";
								array[7] = userid;
								array[8] = "', '";
								int num = 9;
								IPAddress ipaddress = externalIp;
								array[num] = ((ipaddress != null) ? ipaddress.ToString() : null);
								array[10] = "')";
								Universal.ExecuteNonQuery(string.Concat(array));
							}
							totalbrok += brokerage;
							totalPL += final_lp;
						}
					}
					Universal.ExecuteNonQuery(string.Concat(new string[]
					{
						"update t_trading_all_users_master set LedgerBalance='",
						Math.Round(decimal.Parse(lb) - totalbrok + totalPL, 1).ToString(),
						"' where Id='",
						userid,
						"'"
					}));
					result = "{" + "\"ResponseCode\":\"200\"," + "\"ResponseMessage\":\" + !! Trades successfully closed. !! + \"" + "}";
				}
				else
				{
					result = "{" + "\"ResponseCode\":\"500\"," + "\"ResponseMessage\":\" + !! Internal server Error !! + \"" + "}";
				}
			}
			catch (Exception ex)
			{
				result = "{" + "\"ResponseCode\":\"500\"," + "\"ResponseMessage\":\" " + ex.Message + "\"" + "}";
			}
			return result;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00023704 File Offset: 0x00021904
		public string getuserbill(string UserId)
		{
			List<t_order_master> list = new List<t_order_master>();
			DataTable dt = Universal.SelectWithDS("select ScriptName,OrderCategory,Lot,DATE_FORMAT(OrderDate,'%d/%m/%Y') as OrderDate,OrderTime,OrderPrice,BroughtBy,DATE_FORMAT(ClosedAt,'%d/%m/%Y') as ClosedAt,ClosedTime,P_L,Brokerage from t_user_order where UserId='" + UserId + "' and week(ClosedAt)=week(now())-1 and year(ClosedAt)=year(now())", "t_user_order");
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				list.Add(new t_order_master
				{
					ScriptName = dt.Rows[i]["ScriptName"].ToString(),
					OrderCategory = dt.Rows[i]["OrderCategory"].ToString(),
					Lot = dt.Rows[i]["Lot"].ToString(),
					OrderDate = dt.Rows[i]["OrderDate"].ToString(),
					OrderTime = dt.Rows[i]["OrderTime"].ToString(),
					OrderPrice = dt.Rows[i]["OrderPrice"].ToString(),
					BroughtBy = dt.Rows[i]["BroughtBy"].ToString(),
					ClosedAt = dt.Rows[i]["ClosedAt"].ToString(),
					ClosedTime = dt.Rows[i]["ClosedTime"].ToString(),
					P_L = dt.Rows[i]["P_L"].ToString(),
					Brokerage = dt.Rows[i]["Brokerage"].ToString()
				});
			}
			return new JavaScriptSerializer().Serialize(list);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000238D4 File Offset: 0x00021AD4
		[HttpGet]
		public Task<string> getformconetntAsync(string userid, string refid, string PayCustomerName, string PayCustomerPhoneNo, string CustomerEmailID, string PayCartAmount, string baseuri)
		{
			apiController.<getformconetntAsync>d__89 <getformconetntAsync>d__;
			<getformconetntAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<getformconetntAsync>d__.userid = userid;
			<getformconetntAsync>d__.refid = refid;
			<getformconetntAsync>d__.PayCustomerName = PayCustomerName;
			<getformconetntAsync>d__.PayCustomerPhoneNo = PayCustomerPhoneNo;
			<getformconetntAsync>d__.CustomerEmailID = CustomerEmailID;
			<getformconetntAsync>d__.PayCartAmount = PayCartAmount;
			<getformconetntAsync>d__.baseuri = baseuri;
			<getformconetntAsync>d__.<>1__state = -1;
			<getformconetntAsync>d__.<>t__builder.Start<apiController.<getformconetntAsync>d__89>(ref <getformconetntAsync>d__);
			return <getformconetntAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0002394C File Offset: 0x00021B4C
		public string webPostMethod(string postData, string URL)
		{
			DataTable tokentbl = Universal.SelectWithDS("select Token,ApiKey from t_token", "tempdata");
			string token = "";
			string apikey = "";
			if (tokentbl.Rows.Count > 0)
			{
				token = tokentbl.Rows[0]["Token"].ToString();
				apikey = tokentbl.Rows[0]["ApiKey"].ToString();
			}
			ServicePointManager.Expect100Continue = true;
			ServicePointManager.SecurityProtocol = 3072;
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
			httpWebRequest.Method = "GET";
			httpWebRequest.Credentials = CredentialCache.DefaultCredentials;
			httpWebRequest.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 7.1; Trident/5.0)";
			httpWebRequest.Accept = "/";
			httpWebRequest.Headers.Add("Authorization", "token " + apikey + ":" + token);
			httpWebRequest.Headers.Add("X-Kite-Version", "3");
			httpWebRequest.UseDefaultCredentials = true;
			httpWebRequest.UseDefaultCredentials = true;
			httpWebRequest.Proxy.Credentials = CredentialCache.DefaultCredentials;
			WebResponse response = httpWebRequest.GetResponse();
			Stream responseStream = response.GetResponseStream();
			StreamReader streamReader = new StreamReader(responseStream);
			string responseFromServer = streamReader.ReadToEnd();
			streamReader.Close();
			responseStream.Close();
			response.Close();
			return responseFromServer;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00023A84 File Offset: 0x00021C84
		public static Task<string> CallPostApi(string PayCustomerName, string PayCustomerPhoneNo, string CustomerEmailID, string PayCartAmount, string baseuri)
		{
			apiController.<CallPostApi>d__91 <CallPostApi>d__;
			<CallPostApi>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<CallPostApi>d__.PayCustomerName = PayCustomerName;
			<CallPostApi>d__.PayCustomerPhoneNo = PayCustomerPhoneNo;
			<CallPostApi>d__.CustomerEmailID = CustomerEmailID;
			<CallPostApi>d__.PayCartAmount = PayCartAmount;
			<CallPostApi>d__.baseuri = baseuri;
			<CallPostApi>d__.<>1__state = -1;
			<CallPostApi>d__.<>t__builder.Start<apiController.<CallPostApi>d__91>(ref <CallPostApi>d__);
			return <CallPostApi>d__.<>t__builder.Task;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00023AE8 File Offset: 0x00021CE8
		public string webPostMethod1(string postData, string URL)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
			httpWebRequest.Method = "post";
			httpWebRequest.Credentials = CredentialCache.DefaultCredentials;
			httpWebRequest.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 7.1; Trident/5.0)";
			httpWebRequest.Accept = "/";
			httpWebRequest.UseDefaultCredentials = true;
			httpWebRequest.ContentType = "application/json";
			WebResponse response = httpWebRequest.GetResponse();
			Stream responseStream = response.GetResponseStream();
			StreamReader streamReader = new StreamReader(responseStream);
			string responseFromServer = streamReader.ReadToEnd();
			streamReader.Close();
			responseStream.Close();
			response.Close();
			return responseFromServer;
		}
	}
}
