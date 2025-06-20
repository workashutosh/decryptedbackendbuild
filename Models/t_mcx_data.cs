using System;

namespace DabbaTrading.Models
{
	// Token: 0x0200000D RID: 13
	public class t_mcx_data
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00003232 File Offset: 0x00001432
		// (set) Token: 0x060000AC RID: 172 RVA: 0x0000323A File Offset: 0x0000143A
		public string instrument_token { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00003243 File Offset: 0x00001443
		// (set) Token: 0x060000AE RID: 174 RVA: 0x0000324B File Offset: 0x0000144B
		public string exchange_token { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003254 File Offset: 0x00001454
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x0000325C File Offset: 0x0000145C
		public string tradingsymbol { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00003265 File Offset: 0x00001465
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x0000326D File Offset: 0x0000146D
		public string name { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00003276 File Offset: 0x00001476
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x0000327E File Offset: 0x0000147E
		public string last_price { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00003287 File Offset: 0x00001487
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x0000328F File Offset: 0x0000148F
		public string expiry { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00003298 File Offset: 0x00001498
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x000032A0 File Offset: 0x000014A0
		public string strike { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x000032A9 File Offset: 0x000014A9
		// (set) Token: 0x060000BA RID: 186 RVA: 0x000032B1 File Offset: 0x000014B1
		public string tick_size { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000032BA File Offset: 0x000014BA
		// (set) Token: 0x060000BC RID: 188 RVA: 0x000032C2 File Offset: 0x000014C2
		public string lot_size { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000032CB File Offset: 0x000014CB
		// (set) Token: 0x060000BE RID: 190 RVA: 0x000032D3 File Offset: 0x000014D3
		public string instrument_type { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000032DC File Offset: 0x000014DC
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x000032E4 File Offset: 0x000014E4
		public string segment { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x000032ED File Offset: 0x000014ED
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x000032F5 File Offset: 0x000014F5
		public string exchange { get; set; }
	}
}
