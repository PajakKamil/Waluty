﻿using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;

namespace Waluty
{
	public class SqlStuff
	{
		#region Add Data From Api To Database
		private bool _can_i_disconnect = false;
		private static string _tableName = "Waluty.dbo.last_month_currency";

		public async void ClickToUpdate()
		{
			string connectionString = "Data Source=PAJAK-PC;" +
			   " Initial Catalog =Waluty;" +
			   "Integrated Security = True;" +
			   "Encrypt = True;" +
			   "TrustServerCertificate = True;" +
			   "User Instance = False";
			SqlConnection conn = new SqlConnection(connectionString);
			try
			{
				conn.Open();
				//MessageBox.Show("Connected!");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error");
			}
			DateOnly lastMonth = LastMonth();
			string url;
			string url1;
			if (lastMonth.Month < 10)
			{
				url = "https://api.nbp.pl/api/exchangerates/tables/A/" + lastMonth.Year + "-0" + lastMonth.Month + "-01/"
					+ lastMonth.Year + "-0" + lastMonth.Month + "-" + DateTime.DaysInMonth(lastMonth.Year, lastMonth.Month) + "?format=json";

				url1 = "https://api.nbp.pl/api/exchangerates/tables/B/" + lastMonth.Year + "-0" + lastMonth.Month + "-01/"
						+ lastMonth.Year + "-0" + lastMonth.Month + "-" + DateTime.DaysInMonth(lastMonth.Year, lastMonth.Month) + "?format=json";
			}
			else
			{
				url = "https://api.nbp.pl/api/exchangerates/tables/A/" + lastMonth.Year + "-" + lastMonth.Month + "-01/"
						+ lastMonth.Year + "-" + lastMonth.Month + "-" + DateTime.DaysInMonth(lastMonth.Year, lastMonth.Month) + "?format=json";
				url1 = "https://api.nbp.pl/api/exchangerates/tables/B/" + lastMonth.Year + "-" + lastMonth.Month + "-01/"
						+ lastMonth.Year + "-" + lastMonth.Month + "-" + DateTime.DaysInMonth(lastMonth.Year, lastMonth.Month) + "?format=json";
			}
			//MessageBox.Show(url);

			using (HttpClient client = new HttpClient())
			{
                try
                {
					await client.GetStringAsync(url);
                    string sqlQuery = "DELETE FROM " + _tableName + ";";
                    SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Error");
					return;
                }
            }
			DownloadExchangeRate(url, conn);
			DownloadExchangeRate(url1, conn);
		}


		private async void DownloadExchangeRate(string url, SqlConnection conn)        //async - coś jak wątek, async nie może mieć ref
		{
			
			List<Root> root = new List<Root>();

			using (HttpClient client = new HttpClient())
			{
				var json = await client.GetStringAsync(url);        //await czeka na async. WYKORZYSTUJE SIĘ TYLKO W async.
				root = JsonConvert.DeserializeObject<List<Root>>(json);
				//root[] - data,
				//rates[] - waluty;
			}
			SaveInDb(ref root, conn);
			if (_can_i_disconnect == true)
			{
				conn.Close();
				
				MessageBox.Show("successfully updated!");
			}
			else
				_can_i_disconnect = true;
		}

		private DateOnly LastMonth()
		{
			DateOnly today = new DateOnly(DateTime.Today.Year, DateTime.Today.Month - 1, DateTime.Today.Day);
			IOSystem save = new IOSystem();
			if (today.Month <= 01)
			{
				DateOnly help = new DateOnly(today.Year - 1, 12, 31);
				save.Save(help.Year, help.Month);
				return help;
			}
			save.Save(today.Year, today.Month);
			return today;
		}

		private void SaveInDb(ref List<Root> root, SqlConnection conn)
		{

			//POTWÓR!
			root.ForEach(x =>
			{
				x.rates.ForEach(y =>
				{
					UpdateDatabase(x, y, conn);
				});
			});
		}

		private static void UpdateDatabase(Root x, Rate y, SqlConnection conn)
		{
			string sqlQuery = "INSERT INTO " + _tableName + "(Currency, Code, Mid, EffectiveDate) VALUES(@Currency, @Code, @Mid, @EffectiveDate)";

			SqlCommand cmd = new SqlCommand(sqlQuery, conn);
			cmd.CommandText = sqlQuery;
			cmd.Parameters.AddWithValue("@Currency", y.currency);
			cmd.Parameters.AddWithValue("@Code", y.code);
			cmd.Parameters.AddWithValue("@Mid", y.mid);
			cmd.Parameters.AddWithValue("@EffectiveDate", x.effectiveDate);
			cmd.ExecuteNonQuery();
		}
		#endregion


		public static void PrintDatabase(ref DataGridView dataGridView1)
		{
			string connectionString = "Data Source=PAJAK-PC;" +
			   " Initial Catalog =Waluty;" +
			   "Integrated Security = True;" +
			   "Encrypt = True;" +
			   "TrustServerCertificate = True;" +
			   "User Instance = False";
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				conn.Open();
				SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM " + _tableName + ";", conn);
				DataTable dtbl = new DataTable();
				adapter.Fill(dtbl);
				DataView dv = new DataView(dtbl);
				BindingSource source = new BindingSource();
				source.DataSource = dv;
				dataGridView1.DataSource = source;
				//dataGridView1.DataSource = dtbl;
			}
		}
	}
}