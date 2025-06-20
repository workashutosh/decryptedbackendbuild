using System;

namespace DabbaTrading.Models
{
	// Token: 0x02000008 RID: 8
	public class PaymentResponse
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002E9C File Offset: 0x0000109C
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002EA4 File Offset: 0x000010A4
		public bool Status { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002EAD File Offset: 0x000010AD
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002EB5 File Offset: 0x000010B5
		public string Code { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002EBE File Offset: 0x000010BE
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002EC6 File Offset: 0x000010C6
		public string Message { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002ECF File Offset: 0x000010CF
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002ED7 File Offset: 0x000010D7
		public string PGOrderId { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002EE0 File Offset: 0x000010E0
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002EE8 File Offset: 0x000010E8
		public string PreparePOSTForm { get; set; }
	}
}
