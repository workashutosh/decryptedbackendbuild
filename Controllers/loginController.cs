using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Mvc;
using ATS;
using DabbaTrading.Models;

namespace DabbaTrading.Controllers
{
	// Token: 0x02000018 RID: 24
	public class loginController : Controller
	{
		// Token: 0x0600031E RID: 798 RVA: 0x00023B74 File Offset: 0x00021D74
		public ActionResult Index()
		{
			return base.View();
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00023B74 File Offset: 0x00021D74
		public ActionResult contactus()
		{
			return base.View();
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00023B74 File Offset: 0x00021D74
		public ActionResult registration()
		{
			return base.View();
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00023B74 File Offset: 0x00021D74
		public ActionResult forgotpassword()
		{
			return base.View();
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00024954 File Offset: 0x00022B54
		public Task<string> SubmitClientDataAsync(string jsonContent)
		{
			loginController.<SubmitClientDataAsync>d__5 <SubmitClientDataAsync>d__;
			<SubmitClientDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<SubmitClientDataAsync>d__.jsonContent = jsonContent;
			<SubmitClientDataAsync>d__.<>1__state = -1;
			<SubmitClientDataAsync>d__.<>t__builder.Start<loginController.<SubmitClientDataAsync>d__5>(ref <SubmitClientDataAsync>d__);
			return <SubmitClientDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00024998 File Offset: 0x00022B98
		[HttpPost]
		public Task<string> SendOtpSmsAsync(string mobileNumber)
		{
			loginController.<SendOtpSmsAsync>d__6 <SendOtpSmsAsync>d__;
			<SendOtpSmsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<SendOtpSmsAsync>d__.mobileNumber = mobileNumber;
			<SendOtpSmsAsync>d__.<>1__state = -1;
			<SendOtpSmsAsync>d__.<>t__builder.Start<loginController.<SendOtpSmsAsync>d__6>(ref <SendOtpSmsAsync>d__);
			return <SendOtpSmsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x000249DC File Offset: 0x00022BDC
		[HttpPost]
		public Task<string> VerifyOtpAsync(string verificationId, string code)
		{
			loginController.<VerifyOtpAsync>d__7 <VerifyOtpAsync>d__;
			<VerifyOtpAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<VerifyOtpAsync>d__.verificationId = verificationId;
			<VerifyOtpAsync>d__.code = code;
			<VerifyOtpAsync>d__.<>1__state = -1;
			<VerifyOtpAsync>d__.<>t__builder.Start<loginController.<VerifyOtpAsync>d__7>(ref <VerifyOtpAsync>d__);
			return <VerifyOtpAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00024A28 File Offset: 0x00022C28
		[HttpPost]
		public Task<string> registrationAsync(string txtfirstname, string txtlastname, string txtmob, string txtemail, string txtusernm, string txtupassword, string txtaadhar, string txtpanno, string txtcity, string txtPincode, string txtaddress, string txtdomainname)
		{
			loginController.<registrationAsync>d__8 <registrationAsync>d__;
			<registrationAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<registrationAsync>d__.<>4__this = this;
			<registrationAsync>d__.txtfirstname = txtfirstname;
			<registrationAsync>d__.txtlastname = txtlastname;
			<registrationAsync>d__.txtmob = txtmob;
			<registrationAsync>d__.txtemail = txtemail;
			<registrationAsync>d__.txtusernm = txtusernm;
			<registrationAsync>d__.txtupassword = txtupassword;
			<registrationAsync>d__.txtaadhar = txtaadhar;
			<registrationAsync>d__.txtpanno = txtpanno;
			<registrationAsync>d__.txtcity = txtcity;
			<registrationAsync>d__.txtaddress = txtaddress;
			<registrationAsync>d__.<>1__state = -1;
			<registrationAsync>d__.<>t__builder.Start<loginController.<registrationAsync>d__8>(ref <registrationAsync>d__);
			return <registrationAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00024AC4 File Offset: 0x00022CC4
		public Task<bool> changepassword(string password, string phoneno)
		{
			loginController.<changepassword>d__9 <changepassword>d__;
			<changepassword>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<changepassword>d__.password = password;
			<changepassword>d__.phoneno = phoneno;
			<changepassword>d__.<>1__state = -1;
			<changepassword>d__.<>t__builder.Start<loginController.<changepassword>d__9>(ref <changepassword>d__);
			return <changepassword>d__.<>t__builder.Task;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00024B10 File Offset: 0x00022D10
		[HttpPost]
		public ActionResult contactus(contactus_model obj)
		{
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
				"insert into t_enquiryinfo (name,callno,whatsappno,openaccount,rentsoftware,sharesoftware,complain,whitelevel,others,createdate,message,userid,domainname) values('",
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
				"','",
				obj.txtcontactusuderid,
				"','",
				obj.txtdomainname,
				"')"
			})) == 1)
			{
				return base.RedirectToAction("index", "login");
			}
			return base.View();
		}

		// Token: 0x04000144 RID: 324
		private static readonly HttpClient client = new HttpClient();
	}
}
