using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Web.UI;

namespace ATS
{
	// Token: 0x02000004 RID: 4
	public abstract class Universal
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002611 File Offset: 0x00000811
		public static void InitConnection(string ip)
		{
			new Page();
			Universal.CONN_STRING = "Server=144.76.109.43;Port=3306;Database=db_tradeing;Uid=tradenstocko;Pwd=ABab@2345;charset=utf8mb4;default command timeout=0;SslMode=none";
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002624 File Offset: 0x00000824
		public static bool ExecuteTransactionQuery(ArrayList arl)
		{
			DbConnection cn = DbProviderFactories.GetFactory(Universal.ProviderName).CreateConnection();
			cn.ConnectionString = Universal.CONN_STRING;
			if (cn.State == ConnectionState.Open)
			{
				cn.Close();
			}
			cn.Open();
			DbTransaction sqlTran = cn.BeginTransaction();
			DbCommand cmd = cn.CreateCommand();
			cmd.Transaction = sqlTran;
			bool result;
			try
			{
				foreach (object obj in arl)
				{
					string qry = (string)obj;
					cmd.CommandText = qry;
					cmd.ExecuteNonQuery();
				}
				sqlTran.Commit();
				cn.Close();
				result = true;
			}
			catch (Exception)
			{
				sqlTran.Rollback();
				result = false;
			}
			return result;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000026F4 File Offset: 0x000008F4
		public static int ExecuteNonQuery(string cmdText)
		{
			int result;
			try
			{
				DbProviderFactory factory = DbProviderFactories.GetFactory(Universal.ProviderName);
				DbConnection cn = factory.CreateConnection();
				cn.ConnectionString = Universal.CONN_STRING;
				DbCommand dbCommand = factory.CreateCommand();
				dbCommand.CommandText = cmdText;
				dbCommand.Connection = cn;
				if (cn.State == ConnectionState.Open)
				{
					cn.Close();
				}
				cn.Open();
				int num = dbCommand.ExecuteNonQuery();
				cn.Close();
				result = num;
			}
			catch (Exception)
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000276C File Offset: 0x0000096C
		// (set) Token: 0x06000011 RID: 17 RVA: 0x00002773 File Offset: 0x00000973
		public static string currentsession { get; set; }

		// Token: 0x06000012 RID: 18 RVA: 0x0000277C File Offset: 0x0000097C
		public static object ExecuteScalar(string cmdText)
		{
			DbProviderFactory factory = DbProviderFactories.GetFactory(Universal.ProviderName);
			DbConnection cn = factory.CreateConnection();
			cn.ConnectionString = Universal.CONN_STRING;
			DbCommand dbCommand = factory.CreateCommand();
			dbCommand.CommandText = cmdText;
			dbCommand.Connection = cn;
			if (cn.State == ConnectionState.Open)
			{
				cn.Close();
			}
			cn.Open();
			object result = dbCommand.ExecuteScalar();
			cn.Close();
			return result;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000027D8 File Offset: 0x000009D8
		public static ArrayList Select(string cmdText)
		{
			DbProviderFactory factory = DbProviderFactories.GetFactory(Universal.ProviderName);
			DbConnection cn = factory.CreateConnection();
			cn.ConnectionString = Universal.CONN_STRING;
			DbCommand cmd = factory.CreateCommand();
			cmd.CommandText = cmdText;
			cmd.Connection = cn;
			if (cn.State == ConnectionState.Open)
			{
				cn.Close();
			}
			cn.Open();
			ArrayList arl = new ArrayList();
			DbDataReader dr = cmd.ExecuteReader();
			arl.Add("---Select----");
			while (dr.Read())
			{
				arl.Add(dr.GetValue(0).ToString());
			}
			dr.Close();
			cn.Close();
			return arl;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002870 File Offset: 0x00000A70
		public static DataTable SelectWithDS(string cmdText, string TableName)
		{
			DbProviderFactory factory = DbProviderFactories.GetFactory(Universal.ProviderName);
			DbConnection cn = factory.CreateConnection();
			cn.ConnectionString = Universal.CONN_STRING;
			DbDataAdapter cmd = factory.CreateDataAdapter();
			DbCommand cm = factory.CreateCommand();
			cm.CommandText = cmdText;
			cm.Connection = cn;
			cmd.SelectCommand = cm;
			cmd.SelectCommand.Connection = cn;
			DataSet ds = new DataSet();
			cmd.Fill(ds, TableName);
			return ds.Tables[TableName];
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000028E4 File Offset: 0x00000AE4
		public static DataSet SelectWithDSReturnDS(string[] cmdText)
		{
			DbProviderFactory fs = DbProviderFactories.GetFactory(Universal.ProviderName);
			DbConnection cn = fs.CreateConnection();
			cn.ConnectionString = Universal.CONN_STRING;
			DataSet ds = new DataSet();
			int it = 0;
			foreach (string cmdt in cmdText)
			{
				DbDataAdapter cmd = fs.CreateDataAdapter();
				DbCommand cm = fs.CreateCommand();
				cm.CommandText = cmdt;
				cm.Connection = cn;
				cmd.SelectCommand = cm;
				cmd.SelectCommand.Connection = cn;
				cmd.Fill(ds, "tab" + it.ToString());
				it++;
			}
			return ds;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000298C File Offset: 0x00000B8C
		public static DataTable SelectAll(string cmdText)
		{
			DataTable table = new DataTable();
			DbProviderFactory factory = DbProviderFactories.GetFactory(Universal.ProviderName);
			DbConnection cn = factory.CreateConnection();
			cn.ConnectionString = Universal.CONN_STRING;
			DbCommand dbCommand = factory.CreateCommand();
			dbCommand.CommandText = cmdText;
			dbCommand.Connection = cn;
			if (cn.State == ConnectionState.Open)
			{
				cn.Close();
			}
			cn.Open();
			DbDataReader dr = dbCommand.ExecuteReader();
			for (int i = 0; i < dr.FieldCount; i++)
			{
				table.Columns.Add(dr.GetName(i));
			}
			while (dr.Read())
			{
				DataRow temp = table.NewRow();
				for (int j = 0; j < dr.FieldCount; j++)
				{
					temp[j] = dr[j];
				}
				table.Rows.Add(temp);
			}
			dr.Close();
			cn.Close();
			return table;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002A60 File Offset: 0x00000C60
		public static string convertdate(string date)
		{
			string[] datee = date.Split(new char[]
			{
				'/'
			});
			return string.Concat(new string[]
			{
				datee[2],
				"-",
				datee[1],
				"-",
				datee[0]
			});
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002AAC File Offset: 0x00000CAC
		public static string convertToDDMMYYY(string date)
		{
			string[] datee = date.Split(new char[]
			{
				'-'
			});
			return string.Concat(new string[]
			{
				datee[2],
				"/",
				datee[1],
				"/",
				datee[0]
			});
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public static string GetDate
		{
			get
			{
				return string.Concat(new string[]
				{
					DateTime.Now.Year.ToString(),
					"-",
					DateTime.Now.Month.ToString(),
					"-",
					DateTime.Now.Day.ToString()
				});
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002B68 File Offset: 0x00000D68
		public static string GetDateDDMMYY
		{
			get
			{
				return string.Concat(new string[]
				{
					DateTime.Now.AddHours(12.0).AddMinutes(30.0).Day.ToString(),
					"-",
					DateTime.Now.AddHours(12.0).AddMinutes(30.0).Month.ToString(),
					"-",
					DateTime.Now.AddHours(12.0).AddMinutes(30.0).Year.ToString()
				});
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002C40 File Offset: 0x00000E40
		public static string GetTime
		{
			get
			{
				return string.Concat(new string[]
				{
					DateTime.Now.Hour.ToString(),
					":",
					DateTime.Now.Minute.ToString(),
					":",
					DateTime.Now.Second.ToString()
				});
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002CB0 File Offset: 0x00000EB0
		public static string UID
		{
			get
			{
				return string.Concat(new string[]
				{
					DateTime.Now.Day.ToString(),
					DateTime.Now.Month.ToString(),
					DateTime.Now.Year.ToString(),
					DateTime.Now.Hour.ToString(),
					DateTime.Now.Minute.ToString(),
					DateTime.Now.Second.ToString()
				});
			}
		}

		// Token: 0x04000004 RID: 4
		public static string CONN_STRING = "";

		// Token: 0x04000005 RID: 5
		public static string ProviderName = "MySql.Data.MySqlClient";

		// Token: 0x04000006 RID: 6
		public static string LoginBy = "";

		// Token: 0x04000007 RID: 7
		public static string devip = "43.231.124.178";
	}
}
