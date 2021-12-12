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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=PAJAK-PC;" +
               " Initial Catalog =;" +
               "Integrated Security = True;" +
               "Encrypt = True;" +
               "TrustServerCertificate = True;" +
               "User Instance = False";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                try
                {
                    string sql = "CREATE DATABASE " + newDataBaseName.Text + ";";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    if(checkBox1.Checked)
                    {
                        sql = "CREATE TABLE " + newDataBaseName.Text + ".dbo." + newTableName.Text + "(Currency varchar(50), Code varchar(4), Mid float, effectiveDate Date);";
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Connection error");
                }

            }
    }
    }
}
