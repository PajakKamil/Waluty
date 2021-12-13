using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;

namespace Waluty
{
	public class SqlStuff
	{
		private static string _databaseName = ";";
		private static string _tableName = "dbo.last_month_currency";
		private static string connectionString = "Data Source=localhost;" +
			   " Initial Catalog =" + _databaseName +
			   "Integrated Security = True;" +
			   "Encrypt = True;" +
			   "TrustServerCertificate = True;" +
			   "User Instance = False";

		#region Add Data From Api To Database
		private bool _can_i_disconnect = false;

		public async void ClickToUpdate()
		{
			if (!CheckIfTableExists())
				return;

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

		public static bool CheckIfTableExists()
		{
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				try
				{
					conn.Open();
					_databaseName = "Waluty";
					string sqlQuery = "SELECT name FROM master.dbo.sysdatabases WHERE dbid > 4 AND name ='" + _databaseName + "';";
					SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQuery, conn);
					//DataSet ds = new DataSet();
					DataTable dtbl = new DataTable();
					dataAdapter.Fill(dtbl);
					if (dtbl.Rows.Count == 0)
					{
						MessageBox.Show("Nie ma bazy");
						throw new Exception("NoDatabase");
					}

					sqlQuery = "SELECT TOP 1 Currency FROM " + _databaseName + "." + _tableName + ";";
					SqlCommand sql = new SqlCommand(sqlQuery, conn);
					sql.ExecuteNonQuery();

				}
				catch (Exception ex)
				{
					if (ex is SqlException)
					{
						MessageBox.Show("Tabela nie istnieje lub jest uszkodzona.\n" + ex.Message, "Warning");
						DialogResult dr = MessageBox.Show("Utrorzyć nową tablicę " + _tableName + " w bazie danych?", "Utworzyć tablicę?", MessageBoxButtons.YesNo);
						switch (dr)
						{
							case DialogResult.Yes:
								try
								{
									string sqlQuery = "CREATE TABLE " + _databaseName + "." + _tableName + "(Currency varchar(50), Code varchar(4), Mid float, effectiveDate Date);";
									SqlCommand sql = new SqlCommand(sqlQuery, conn);
									sql.ExecuteNonQuery();
									MessageBox.Show("Tabela została pomyślnie dodana");
									return true;
								}
								catch (Exception ex1)
								{
									MessageBox.Show(ex1.Message, "Error");
								}
								break;
							case DialogResult.No:
								break;
						} //Koniec switch'a
					} //koniec if'a


					else
					{
						MessageBox.Show("Baza danych nie istnieje.\n" + ex.Message, "Warning");
						//_databaseName = "Walutyes";
						DialogResult dr = MessageBox.Show("Utrorzyć nową bazę danych oraz tablicę " + _databaseName + "?", "Utworzyć bazę?", MessageBoxButtons.YesNo);
						switch (dr)
						{
							case DialogResult.Yes:
								try
								{
									string sqlQuery = "CREATE DATABASE " + _databaseName + ";";
									SqlCommand sql = new SqlCommand(sqlQuery, conn);
									sql.ExecuteNonQuery();
									MessageBox.Show("Baza danych została pomyślnie dodana");
									sqlQuery = "CREATE TABLE " + _databaseName + "." + _tableName + "(Currency varchar(50), Code varchar(4), Mid float, effectiveDate Date);";
									sql = new SqlCommand(sqlQuery,conn);
									sql.ExecuteNonQuery();
									connectionString = "Data Source=localhost;" +
														   " Initial Catalog =" + _databaseName + ";" +
														   "Integrated Security = True;" +
														   "Encrypt = True;" +
														   "TrustServerCertificate = True;" +
														   "User Instance = False";
									return true;
								}
								catch (Exception ex1)
								{
									MessageBox.Show(ex1.Message, "Error");
								}
								break;
							case DialogResult.No:
								break;
						}
						return false;
					}//koniec catch
				} // Koniec using
				connectionString = "Data Source=localhost;" +
									   " Initial Catalog =" + _databaseName + ";" +
									   "Integrated Security = True;" +
									   "Encrypt = True;" +
									   "TrustServerCertificate = True;" +
									   "User Instance = False";
				return true;
			} //Koniec metody
		}
	}
}