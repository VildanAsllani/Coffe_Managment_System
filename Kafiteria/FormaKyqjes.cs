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
using System.IO;

namespace Kafiteria
{
    public partial class FormaKyqjes : Form
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["KonektimiMeDatabaze"].ToString());
        SqlDataAdapter sda;
        DataTable dt;
        SqlCommand command;

        public FormaKyqjes()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            timer1.Start();
        }
        private void btnMbyll_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime Data = DateTime.Now;
            lblKoha.Text = Data.ToString();
        }
        private void btnKyquni_Click(object sender, EventArgs e)
        {
            Form Tavolinat = new Tavolinat();
            Tavolinat.Show();
            this.Hide();
        }

        void Kyqja()
        {
            SqlDataAdapter login = new SqlDataAdapter("Select Count(*) from Puntoret Where Fjalkalimi='" + textBox1.Text + "'", conn);
            DataTable dt = new DataTable();
            login.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                SqlDataAdapter loginTipi = new SqlDataAdapter("SELECT ID_Pozita FROM Puntoret WHERE Fjalkalimi='" + textBox1.Text + "'", conn);
                DataTable dtTipi = new DataTable();
                loginTipi.Fill(dtTipi);

                if (dtTipi.Rows[0][0].ToString() == "2")// KYQJA ADMINISTRATORIT.
                {
                    using (SqlCommand command = new SqlCommand("SELECT ID_Puntori,Emri FROM Puntoret WHERE Fjalkalimi='" + textBox1.Text + "'", conn))
                    {
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            int ID_Admin;
                            ID_Admin = reader.GetInt32(0);
                            string Emri;
                            Emri = reader.GetString(1);

                            Form Administrimi = new Administrimi(ID_Admin, Emri);
                            Administrimi.Show();
                            this.Hide();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Fjalkalimi gabim");
                    textBox1.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Fjalkalimi gabim");
                textBox1.Text = "";
            }
        }
        private void btnAdmin_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void FormaKyqjes_Click(object sender, EventArgs e)
        {
            if (panel2.Visible == true)
            {
                panel2.Visible = false;
            }
            else
            {

            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Button num = (Button)sender;
            if (textBox1.Text == "")
            {
                textBox1.Text = num.Text;
            }
            else
            {
                textBox1.Text = (textBox1.Text + num.Text);
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            textBox1.Text="";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.TextLength == 8)
            {
                Kyqja();
            }
        }

        void ParaqitLogon()
        {
            string query = "SELECT ID_Logo,Foto FROM Logo";
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
                command = new SqlCommand(query, conn);
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    string ID;
                    ID = reader[0].ToString();
                    byte[] foto = (byte[])(reader[1]);
                    if (foto == null)
                    {
                        picLogo.Image = null;
                        picLogo.Visible = false;
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream(foto);
                        picLogo.Image = Image.FromStream(ms);
                        panel6.Visible = true;
                    }
                }
                else
                {

                }
                conn.Close();
            }
        }

        private void FormaKyqjes_Load(object sender, EventArgs e)
        {
            ParaqitLogon();
        }
    }
}
