using System;

namespace ATS
{
	// Token: 0x02000003 RID: 3
	public class Class1
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000021E0 File Offset: 0x000003E0
		public string RupeesToWord(string MyNumber)
		{
			string Paisa = "";
			string Words = "";
			string[] place = new string[]
			{
				" Thousand ",
				null,
				" Lakh ",
				null,
				" Crore ",
				null,
				" Arab ",
				null,
				" Kharab "
			};
			int DecimalPlace = MyNumber.IndexOf(".");
			if (DecimalPlace > 0)
			{
				string Temp = MyNumber.Substring(DecimalPlace + 1, 2);
				Paisa = " and " + this.ConvertTens(Temp) + " Paisa";
				MyNumber = MyNumber.Substring(0, DecimalPlace);
			}
			if (MyNumber.Length <= 0 || MyNumber.Length > 2)
			{
				string Hundreds = this.ConvertHundreds(MyNumber.Substring(MyNumber.Length - 3));
				MyNumber = MyNumber.Substring(0, MyNumber.Length - 3);
				int iCount = 0;
				while (MyNumber != "")
				{
					if (MyNumber.Length == 1)
					{
						string Temp = MyNumber.Substring(MyNumber.Length - 1);
						Words = Words.Insert(0, this.ConvertDigit(Temp) + place[iCount]);
						MyNumber = MyNumber.Substring(0, MyNumber.Length - 1);
					}
					else
					{
						string Temp = MyNumber.Substring(MyNumber.Length - 2);
						Words = Words.Insert(0, this.ConvertTens(Temp) + place[iCount]);
						MyNumber = MyNumber.Substring(0, MyNumber.Length - 2);
					}
					iCount += 2;
				}
				return Words + Hundreds + Paisa + " Only";
			}
			if (MyNumber.Length == 1)
			{
				return this.ConvertDigit(MyNumber) + Paisa + " Only.";
			}
			return this.ConvertTens(MyNumber) + Paisa + " Only.";
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002380 File Offset: 0x00000580
		private string ConvertHundreds(string MyNumber)
		{
			string Result = "";
			MyNumber = ("000" + MyNumber).Substring(("000" + MyNumber).Length - 3);
			if (MyNumber.Substring(0, 1) != "0")
			{
				Result = this.ConvertDigit(MyNumber.Substring(0, 1)) + " Hundreds ";
			}
			if (MyNumber.Substring(1, 1) != "0")
			{
				Result += this.ConvertTens(MyNumber.Substring(1));
			}
			else
			{
				Result += this.ConvertDigit(MyNumber.Substring(2));
			}
			return Result.Trim();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002428 File Offset: 0x00000628
		private string ConvertTens(string MyTens)
		{
			string Result = "";
			if (int.Parse(MyTens.Substring(0, 1)) == 1)
			{
				switch (int.Parse(MyTens.ToString()))
				{
				case 10:
					Result = "Ten";
					break;
				case 11:
					Result = "Eleven";
					break;
				case 12:
					Result = "Twelve";
					break;
				case 13:
					Result = "Thirteen";
					break;
				case 14:
					Result = "Fourteen";
					break;
				case 15:
					Result = "Fifteen";
					break;
				case 16:
					Result = "Sixteen";
					break;
				case 17:
					Result = "Seventeen";
					break;
				case 18:
					Result = "Eighteen";
					break;
				case 19:
					Result = "Nineteen";
					break;
				}
			}
			else
			{
				switch (int.Parse(MyTens.Substring(0, 1)))
				{
				case 2:
					Result = "Twenty ";
					break;
				case 3:
					Result = "Thirty ";
					break;
				case 4:
					Result = "Forty ";
					break;
				case 5:
					Result = "Fifty ";
					break;
				case 6:
					Result = "Sixty ";
					break;
				case 7:
					Result = "Seventy ";
					break;
				case 8:
					Result = "Eighty ";
					break;
				case 9:
					Result = "Ninety ";
					break;
				}
				Result += this.ConvertDigit(MyTens.Substring(1, 1));
			}
			return Result;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000258C File Offset: 0x0000078C
		private string ConvertDigit(string MyDigit)
		{
			switch (int.Parse(MyDigit))
			{
			case 1:
				return "One";
			case 2:
				return "Two";
			case 3:
				return "Three";
			case 4:
				return "Four";
			case 5:
				return "Five";
			case 6:
				return "Six";
			case 7:
				return "Seven";
			case 8:
				return "Eight";
			case 9:
				return "Nine";
			default:
				return "";
			}
		}
	}
}
