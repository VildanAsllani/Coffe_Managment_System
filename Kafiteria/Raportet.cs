using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Kafiteria
{
    public partial class Raportet : Form
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["KonektimiMeDatabaze"].ToString());
        SqlDataAdapter sda;
        DataTable dt;
        SqlCommand command;

        int ID_Admin;
        string EmriAdministratorit;

        public Raportet(int IDA, string EmriAdm)
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            ID_Admin = IDA;
            EmriAdministratorit = EmriAdm;
        }

        private void btnMbrapa_Click(object sender, EventArgs e)
        {
            Form Administrimi = new Administrimi(ID_Admin, EmriAdministratorit);
            Administrimi.Show();
            this.Hide();
        }

        private void Raportet_Load(object sender, EventArgs e)
        {
            CMBPuntoret();
        }
        void CMBPuntoret()
        {
            fill_comboPuntoriDitor("Puntoret", comboBox1);
            fill_comboPuntoriDitor("Puntoret", comboBox2);
        }
        public void fill_comboPuntoriDitor(string table, ComboBox cmb, string columns = "*")
        {
            try
            {
                var dt = new DataTable();

                using (SqlDataAdapter da = new SqlDataAdapter("SELECT " + columns + " FROM [" + table + "] WHERE ID_Pozita=1", conn))
                {
                    da.Fill(dt);
                }

                var row = dt.NewRow();

                row[dt.Columns[0].ToString()] = DBNull.Value;
                row[dt.Columns[1].ToString()] = "Select...";

                dt.Rows.InsertAt(row, 0);

                cmb.DataSource = dt;
                cmb.ValueMember = dt.Columns[0].ToString();
                cmb.DisplayMember = dt.Columns[1].ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DataTable1TableAdapter.Fill(this.DataSetRaportet.DataTable1,dateTimePicker1.Text);
            this.reportViewer1.RefreshReport();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DataTable2TableAdapter.Fill(this.DataSetRaportet.DataTable2,dateTimePicker2.Text,dateTimePicker3.Text);
            this.reportViewer2.RefreshReport();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Select...")
            {
                label5.Text = "Ploteso Puntorin";
                label5.ForeColor = Color.Red;
            }
            else
            {
                this.DataTable3TableAdapter.Fill(this.DataSetRaportet.DataTable3, dateTimePicker4.Text, Convert.ToInt32(comboBox1.SelectedValue));
                this.reportViewer3.RefreshReport();
                label5.Text = "Zgjedh Puntorin";
                label5.ForeColor = Color.Black;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "Select...")
            {
                label7.Text = "Ploteso Puntorin";
                label7.ForeColor = Color.Red;
            }
            else
            {
                this.DataTable4TableAdapter.Fill(this.DataSetRaportet.DataTable4, dateTimePicker5.Text, dateTimePicker6.Text, Convert.ToInt32(comboBox2.SelectedValue));
                this.reportViewer4.RefreshReport();
                label7.Text = "Ploteso Puntorin";
                label7.ForeColor = Color.Black;
            }
        }
      
    }
}
