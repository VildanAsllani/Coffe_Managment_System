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
    public partial class Tavolinat : Form
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["KonektimiMeDatabaze"].ToString());
        SqlDataAdapter sda;
        DataTable dt;
        SqlCommand command;

        int xLocation, yLocation,Tavolina;

        //int ID_Kamarieri;
        public Tavolinat()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            tabControl1.SelectedIndexChanged += new EventHandler(tabControl1_SelectedIndexChanged);
            //ID_Kamarieri = 1;
            timer1.Start();
            textBox1.PasswordChar='*';
            textBox2.PasswordChar = '*';
        }

        private void btnMbyll_Click(object sender, EventArgs e)
        {
            Form FormaKyqjes = new FormaKyqjes();
            FormaKyqjes.Show();
            this.Hide();
        }

        private void Tavolinat_Load(object sender, EventArgs e)
        {
            conn.Open();
            string query = string.Format("SELECT * FROM Tavolinat");
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.Fill(dt);
            ds.Tables.Add(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Button button = new Button();
                Label lbl = new Label();

                int Lokacioni;
                Lokacioni = Convert.ToInt32(dr["ID_Lokacioni"]);

                if(Lokacioni==1){
                    Point location = button.Location;
                    xLocation = Convert.ToInt32(dr["X"]);
                    yLocation = Convert.ToInt32(dr["Y"]);
                    button.Location = new Point(xLocation, yLocation);


                    button.FlatStyle = FlatStyle.Flat;

                    button.TextAlign = ContentAlignment.BottomLeft;
                    button.ForeColor = Color.White;
                    button.FlatAppearance.BorderColor = Color.FromArgb(126, 204, 143);
                    button.FlatAppearance.BorderSize = 0;
                    if (Convert.ToInt32(dr["Hide"]) == 1)
                    {
                        button.BackColor = Color.FromArgb(126, 204, 143);
                    }
                    else
                    {
                        button.BackColor = Color.FromArgb(255, 77, 77);
                    }

                    button.Margin = new Padding(30, 40, 0, 0);
                    button.Text = dr["ID_Tavolina"].ToString();
                    button.Name = dr["ID_Tavolina"].ToString();
                    button.Font = new Font("Arial Narrow", 15, FontStyle.Bold);
                    button.Size = new Size(55,55);
                    panel1.Controls.Add(button);
                    button.Click += new System.EventHandler(Click_TavolinaMbrenda);
                }
                else if (Lokacioni == 2)
                {
                    Point location = button.Location;
                    xLocation = Convert.ToInt32(dr["X"]);
                    yLocation = Convert.ToInt32(dr["Y"]);
                    button.Location = new Point(xLocation, yLocation);


                    button.FlatStyle = FlatStyle.Flat;
                    button.TextAlign = ContentAlignment.BottomLeft;
                    button.ForeColor = Color.White;
                    button.FlatAppearance.BorderColor = Color.FromArgb(126, 204, 143);
                    button.FlatAppearance.BorderSize = 0;
                    if (Convert.ToInt32(dr["Hide"]) == 1)
                    {
                        button.BackColor = Color.FromArgb(126, 204, 143);
                    }
                    else
                    {
                        button.BackColor = Color.FromArgb(255, 77, 77);
                    }

                    button.Margin = new Padding(30, 40, 0, 0);
                    button.Text = dr["ID_Tavolina"].ToString();
                    button.Name=dr["ID_Tavolina"].ToString();
                    button.Font = new Font("Arial Narrow", 15, FontStyle.Bold);
                    button.Size = new Size(55,55);
                    panel2.Controls.Add(button);
                    button.Click += new System.EventHandler(Click_TavolinaTerasa);
                }
                else
                {
                    Form FormaKyqjes = new FormaKyqjes();
                    FormaKyqjes.Show();
                    this.Hide();
                }
            }
        }
        //KLIKO FILLON KODI PER BUTONAT MBRENDA LOKALIT
        void Click_TavolinaMbrenda(object sender, EventArgs e)
        {
            Button ID = (Button)sender;
            //Tavolina = Convert.ToInt32(ID.Text);
            Tavolina = Convert.ToInt32(ID.Name);
            //MessageBox.Show("ID=" + ID.Text+", Name="+ID.Name);
            panelMbrenda.Visible = true;
        }
         private void button6_Click(object sender, EventArgs e)
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
         private void buttonC_Click(object sender, EventArgs e)
         {
             textBox1.Text = "";
         }
         private void btnClose_Click(object sender, EventArgs e)
         {
             panelMbrenda.Visible = false;
         }
         private void textBox1_TextChanged(object sender, EventArgs e)
         {
             if (textBox1.TextLength == 8)
             {
                 KyqjaMbrenda();
             }
         }

         void KyqjaMbrenda()
         {
             SqlDataAdapter login = new SqlDataAdapter("Select Count(*) from Puntoret Where Fjalkalimi='" + textBox1.Text + "'", conn);
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
         //KETU NDERPREHET KODI PER BUTONAT MBRENDA LOKALIT


         //KETU FILLON KODI PER BUTONAT NE TERAS TE LOKALIT
         void Click_TavolinaTerasa(object sender, EventArgs e)
         {
             Button ID = (Button)sender;
             Label lblIDTavolina = new Label();
             Tavolina = Convert.ToInt32(ID.Text);
             lblIDTavolina.Text = ID.Text;
             panelTerasa.Visible = true;
         }
         private void button15_Click(object sender, EventArgs e)
         {
             Button num = (Button)sender;
             if (textBox2.Text == "")
             {
                 textBox2.Text = num.Text;
             }
             else
             {
                 textBox2.Text = (textBox2.Text + num.Text);
             }
         }

         private void button12_Click(object sender, EventArgs e)
         {
             panelTerasa.Visible = false;
         }
         private void textBox2_TextChanged(object sender, EventArgs e)
         {
             if (textBox2.TextLength == 8)
             {
                 KyqjaTeras();
             }
         }
         void KyqjaTeras()
         {
             SqlDataAdapter login = new SqlDataAdapter("Select Count(*) from Puntoret Where Fjalkalimi='" + textBox2.Text + "'", conn);
             DataTable dt = new DataTable();
             login.Fill(dt);
             if (dt.Rows[0][0].ToString() == "1")
             {
                 SqlDataAdapter loginTipi = new SqlDataAdapter("SELECT ID_Pozita FROM Puntoret WHERE Fjalkalimi='" + textBox2.Text + "'", conn);
                 DataTable dtTipi = new DataTable();
                 loginTipi.Fill(dtTipi);
                 if (dtTipi.Rows[0][0].ToString() == "1")// KYQJA KAMARIERIT.
                 {
                     using (SqlCommand command = new SqlCommand("SELECT ID_Puntori,Emri FROM Puntoret WHERE Fjalkalimi='" + textBox2.Text + "'", conn))
                     {
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
                     textBox2.Text = "";
                 }

             }
             else
             {
                 MessageBox.Show("Fjalkalimi gabim");
                 textBox2.Text = "";
             }
         }

         private void button13_Click(object sender, EventArgs e)
         {
             textBox2.Text = "";
         }
        //KETU NDERPREHET KODI PER BUTONAT NE TERASE TE LOKALIT
         private void tabControl1_SelectedIndexChanged(Object sender, EventArgs e)
         {
             if (tabControl1.SelectedTab == tabPage1)
             {
                 panelTerasa.Visible = false;
                 textBox2.Text = "";
             }
             if (tabControl1.SelectedTab == tabPage2)
             {
                 panelMbrenda.Visible = false;
                 textBox1.Text = "";
             }
         }

         private void timer1_Tick(object sender, EventArgs e)
         {
             DateTime Data = DateTime.Now;
             lblKoha.Text = Data.ToString();
         }
    }
}
