using System;

namespace DabbaTrading.Models
{
	// Token: 0x02000014 RID: 20
	public class usernotification
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000174 RID: 372 RVA: 0x000038A3 File Offset: 0x00001AA3
		// (set) Token: 0x06000175 RID: 373 RVA: 0x000038AB File Offset: 0x00001AAB
		public string Title { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000176 RID: 374 RVA: 0x000038B4 File Offset: 0x00001AB4
		// (set) Token: 0x06000177 RID: 375 RVA: 0x000038BC File Offset: 0x00001ABC
		public string Message { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000178 RID: 376 RVA: 0x000038C5 File Offset: 0x00001AC5
		// (set) Token: 0x06000179 RID: 377 RVA: 0x000038CD File Offset: 0x00001ACD
		public string CreatedDate { get; set; }
	}
}
