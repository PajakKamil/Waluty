using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Waluty
{
	public partial class Form2 : Form
	{
		public Form2()
		{
			InitializeComponent();
			ShowDatabasesOrTables("SELECT name FROM master.dbo.sysdatabases WHERE dbid > 4;");
		}

		private	bool showTable = true;
		private void ShowDatabasesOrTables(string WhatDoYouWantToSee)
		{
			string connectionString = "Data Source=PAJAK-PC;" +
			   " Initial Catalog =Waluty;" +
			   "Integrated Security = True;" +
			   "Encrypt = True;" +
			   "TrustServerCertificate = True;" +
			   "User Instance = False";
			try
			{
			using(SqlConnection conn = new SqlConnection(connectionString))
			{
				conn.Open();
				SqlDataAdapter adapter = new SqlDataAdapter(WhatDoYouWantToSee, conn);
				DataTable dtbl = new DataTable();
				adapter.Fill(dtbl);
				dataGridViewDatabases.DataSource = dtbl;
			}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error");
				return;
			}
		}

        private void button1_Click(object sender, EventArgs e)
        {
			if (showTable)
            {
				ShowDatabasesOrTables("Select TABLE_CATALOG as Baza_danych, TABLE_NAME as Tabela from INFORMATION_SCHEMA.tables TABLE_CATALOG;");
				showTable = false;
			}
            else
            {
				ShowDatabasesOrTables("SELECT name as Nazwa_bazy_danych FROM master.dbo.sysdatabases WHERE dbid > 4");
				showTable = true;
            }
        }
    }
}
