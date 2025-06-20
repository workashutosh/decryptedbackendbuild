using System;

namespace DabbaTrading.Models
{
	// Token: 0x02000010 RID: 16
	public class t_order_master
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x000033DB File Offset: 0x000015DB
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x000033E3 File Offset: 0x000015E3
		public string Id { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x000033EC File Offset: 0x000015EC
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x000033F4 File Offset: 0x000015F4
		public string OrderDate { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x000033FD File Offset: 0x000015FD
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00003405 File Offset: 0x00001605
		public string isstoplossorder { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x0000340E File Offset: 0x0000160E
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00003416 File Offset: 0x00001616
		public string ClosedTime { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x0000341F File Offset: 0x0000161F
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00003427 File Offset: 0x00001627
		public string OrderTime { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00003430 File Offset: 0x00001630
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00003438 File Offset: 0x00001638
		public string OrderTimeFull { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00003441 File Offset: 0x00001641
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00003449 File Offset: 0x00001649
		public string OrderNo { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00003452 File Offset: 0x00001652
		// (set) Token: 0x060000EF RID: 239 RVA: 0x0000345A File Offset: 0x0000165A
		public string UserId { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00003463 File Offset: 0x00001663
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x0000346B File Offset: 0x0000166B
		public string selectedlotsize { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00003474 File Offset: 0x00001674
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x0000347C File Offset: 0x0000167C
		public string UserName { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00003485 File Offset: 0x00001685
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x0000348D File Offset: 0x0000168D
		public string OrderCategory { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00003496 File Offset: 0x00001696
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x0000349E File Offset: 0x0000169E
		public string OrderType { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000034A7 File Offset: 0x000016A7
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x000034AF File Offset: 0x000016AF
		public string ScriptName { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060000FA RID: 250 RVA: 0x000034B8 File Offset: 0x000016B8
		// (set) Token: 0x060000FB RID: 251 RVA: 0x000034C0 File Offset: 0x000016C0
		public string TokenNo { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060000FC RID: 252 RVA: 0x000034C9 File Offset: 0x000016C9
		// (set) Token: 0x060000FD RID: 253 RVA: 0x000034D1 File Offset: 0x000016D1
		public string ActionType { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060000FE RID: 254 RVA: 0x000034DA File Offset: 0x000016DA
		// (set) Token: 0x060000FF RID: 255 RVA: 0x000034E2 File Offset: 0x000016E2
		public string OrderPrice { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000100 RID: 256 RVA: 0x000034EB File Offset: 0x000016EB
		// (set) Token: 0x06000101 RID: 257 RVA: 0x000034F3 File Offset: 0x000016F3
		public string Lot { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000102 RID: 258 RVA: 0x000034FC File Offset: 0x000016FC
		// (set) Token: 0x06000103 RID: 259 RVA: 0x00003504 File Offset: 0x00001704
		public string MarginUsed { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000104 RID: 260 RVA: 0x0000350D File Offset: 0x0000170D
		// (set) Token: 0x06000105 RID: 261 RVA: 0x00003515 File Offset: 0x00001715
		public string HoldingMarginReq { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000106 RID: 262 RVA: 0x0000351E File Offset: 0x0000171E
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00003526 File Offset: 0x00001726
		public string OrderStatus { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000108 RID: 264 RVA: 0x0000352F File Offset: 0x0000172F
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00003537 File Offset: 0x00001737
		public string SymbolType { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00003540 File Offset: 0x00001740
		// (set) Token: 0x0600010B RID: 267 RVA: 0x00003548 File Offset: 0x00001748
		public string BroughtBy { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00003551 File Offset: 0x00001751
		// (set) Token: 0x0600010D RID: 269 RVA: 0x00003559 File Offset: 0x00001759
		public string P_L { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00003562 File Offset: 0x00001762
		// (set) Token: 0x0600010F RID: 271 RVA: 0x0000356A File Offset: 0x0000176A
		public string Brokerage { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00003573 File Offset: 0x00001773
		// (set) Token: 0x06000111 RID: 273 RVA: 0x0000357B File Offset: 0x0000177B
		public string ActionByClose { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00003584 File Offset: 0x00001784
		// (set) Token: 0x06000113 RID: 275 RVA: 0x0000358C File Offset: 0x0000178C
		public string OrderTypeClose { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00003595 File Offset: 0x00001795
		// (set) Token: 0x06000115 RID: 277 RVA: 0x0000359D File Offset: 0x0000179D
		public string ClosedAt { get; set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000116 RID: 278 RVA: 0x000035A6 File Offset: 0x000017A6
		// (set) Token: 0x06000117 RID: 279 RVA: 0x000035AE File Offset: 0x000017AE
		public string PendingToActiveDateTime { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000118 RID: 280 RVA: 0x000035B7 File Offset: 0x000017B7
		// (set) Token: 0x06000119 RID: 281 RVA: 0x000035BF File Offset: 0x000017BF
		public string actualLot { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600011A RID: 282 RVA: 0x000035C8 File Offset: 0x000017C8
		// (set) Token: 0x0600011B RID: 283 RVA: 0x000035D0 File Offset: 0x000017D0
		public string isedit { get; set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600011C RID: 284 RVA: 0x000035D9 File Offset: 0x000017D9
		// (set) Token: 0x0600011D RID: 285 RVA: 0x000035E1 File Offset: 0x000017E1
		public string cmp { get; set; }
	}
}
