using Microsoft.Data.SqlClient;
using System.Net.Http;
using Newtonsoft.Json;
using System;

namespace Waluty
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			SqlStuff.CheckIfTableExists();
			PeriodicUpdate periodicUpdate = new PeriodicUpdate();
			SqlStuff.PrintDatabase(ref dataGridView1);
		}


		private void button1_Click(object sender, EventArgs e)
		{
			SqlStuff.PrintDatabase(ref dataGridView1);
			SqlStuff sqlStuff = new SqlStuff();
			sqlStuff.ClickToUpdate();
		}

        private void ShowAllLabels_Click(object sender, EventArgs e)
        {
			DataGridViewAutoFilter.DataGridViewAutoFilterColumnHeaderCell.RemoveFilter(dataGridView1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
			SqlStuff.PrintDatabase(ref dataGridView1);
        }
    }
}