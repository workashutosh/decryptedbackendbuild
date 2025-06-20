using System;

namespace DabbaTrading.Models
{
	// Token: 0x02000007 RID: 7
	public class ActiveOrderWithCMP
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002DE1 File Offset: 0x00000FE1
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002DE9 File Offset: 0x00000FE9
		public int id { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002DF2 File Offset: 0x00000FF2
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002DFA File Offset: 0x00000FFA
		public string tokenno { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002E03 File Offset: 0x00001003
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002E0B File Offset: 0x0000100B
		public string OrderCategory { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002E14 File Offset: 0x00001014
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002E1C File Offset: 0x0000101C
		public string orderprice { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002E25 File Offset: 0x00001025
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002E2D File Offset: 0x0000102D
		public string LotQty { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002E36 File Offset: 0x00001036
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002E3E File Offset: 0x0000103E
		public string selectedLotSize { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002E47 File Offset: 0x00001047
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002E4F File Offset: 0x0000104F
		public string bid { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002E58 File Offset: 0x00001058
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002E60 File Offset: 0x00001060
		public string diffval { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002E69 File Offset: 0x00001069
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002E71 File Offset: 0x00001071
		public string cmp { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002E7A File Offset: 0x0000107A
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002E82 File Offset: 0x00001082
		public string ask { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002E8B File Offset: 0x0000108B
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002E93 File Offset: 0x00001093
		public string pl { get; set; }
	}
}
