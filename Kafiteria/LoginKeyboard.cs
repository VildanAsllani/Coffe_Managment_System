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
    public partial class LoginKeyboard : Form
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["KonektimiMeDatabaze"].ToString());
        SqlDataAdapter sda;
        DataTable dt;
        SqlCommand command;

        int Tavolina;

        public LoginKeyboard(string Tvl)
        {
            InitializeComponent();
            textBox1.PasswordChar = '*';
            Tavolina = Convert.ToInt32(Tvl);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.TextLength == 8)
            {
                Kyqja();
            }
            if(textBox1.Text==""){
                label1.Text = "";
            }
            label1.Text = textBox1.Text.Length.ToString();
        }
        void Kyqja()
        {
            SqlDataAdapter login = new SqlDataAdapter("Select Count(*) from Puntoret Where Fjalkalimi='" +textBox1.Text + "'", conn);
            DataTable dt = new DataTable();
            login.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                SqlDataAdapter loginTipi = new SqlDataAdapter("SELECT ID_Pozita FROM Puntoret WHERE Fjalkalimi='" + textBox1.Text + "'", conn);
                DataTable dtTipi = new DataTable();
                loginTipi.Fill(dtTipi);
                if (dtTipi.Rows[0][0].ToString() == "1")// KYQJA KAMARIERIT.
                {
                    using (SqlCommand command = new SqlCommand("SELECT ID_Puntori,Emri FROM Puntoret WHERE Fjalkalimi='" + textBox1.Text + "'", conn))
                    {
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            int ID_Kamarieri;
                            ID_Kamarieri = reader.GetInt32(0);
                            string Emri;
                            Emri = reader.GetString(1);

                            Form FormaKamarierit = new FormaKamarierit(Tavolina, ID_Kamarieri, Emri);
                            FormaKamarierit.Show();
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

        private void button2_Click(object sender, EventArgs e)
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
            textBox1.Text = "";
        }
    }
}
