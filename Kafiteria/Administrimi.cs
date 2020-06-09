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
    public partial class Administrimi : Form
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["KonektimiMeDatabaze"].ToString());
        SqlDataAdapter sda;
        DataTable dt;
        SqlCommand command;

        string imgLoc = "";
        int ID_Admin;
        string EmriAdministratorit;
        public Administrimi(int IDAdmin, string Emri)
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            ID_Admin = IDAdmin;
            EmriAdministratorit = Emri;
            timer1.Start();
            ShfaqShitjetDitore();
        }

        private void btnMbrapa_Click(object sender, EventArgs e)
        {
            Form FormaKyqjes = new FormaKyqjes();
            FormaKyqjes.Show();
            this.Hide();
        }

        void ShfaqShitjetDitore()
        {
            conn.Open();
            string Data;
            Data = System.DateTime.Now.ToString();
            SqlDataAdapter SDA = new SqlDataAdapter("SELECT s.ID_Shitja,p.Produkti,s.Sasia,s.Totali,p.Qmimi FROM Shitjet as s,Produktet as p WHERE s.ID_Produkti=p.ID_Produkti AND Data='" + Data + "' order by ID_Shitja desc", conn);
            DataTable dt = new DataTable();
            SDA.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string T;
                DataRow dr = dt.Rows[i];
                ListViewItem listitem = new ListViewItem(dr["Produkti"].ToString());
                listitem.SubItems.Add(dr["Qmimi"].ToString());
                listitem.SubItems.Add(dr["Sasia"].ToString());
                listitem.SubItems.Add(dr["Totali"].ToString());
                listView1.Items.Add(listitem);
                listView1.View = View.Details;
            }
            conn.Close();
        }


        private void mbrendaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form TMbrenda = new TMbrenda(ID_Admin, EmriAdministratorit);
            TMbrenda.Show();
            this.Hide();
        }

        private void jashteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form TJashte = new TJashte(ID_Admin, EmriAdministratorit);
            TJashte.Show();
            this.Hide();
        }

        private void raporteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Raportet = new Raportet(ID_Admin, EmriAdministratorit);
            Raportet.Show();
            this.Hide();
        }

        private void administrimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form AdminDB = new AdminDB(ID_Admin, EmriAdministratorit);
            AdminDB.Show();
            this.Hide();
        }
        private void btnZgjedhFoton_Click(object sender, EventArgs e)
        {
            ZgjedhFoton();
        }

        void ZgjedhFoton()
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "JPG Files (*.jpg)|*.jpg|GIF Files(*.gif)|*gif|All Files(*.*)|*.*";
                dlg.Title = "Zgjedh Logon";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    imgLoc = dlg.FileName.ToString();
                    picZgjedhLogo.ImageLocation = imgLoc;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                        picZgjedhLogo.Image = null;
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream(foto);
                        picLogo.Image = Image.FromStream(ms);
                    }
                }
                else
                {

                }
                conn.Close();
                picZgjedhLogo.Image = null;
            }
        }

        private void btnRuajFoton_Click(object sender, EventArgs e)
        {
            if (picZgjedhLogo.Image == null)
            {
                MessageBox.Show("Fillimisht zgjedh logon.");
            }
            else
            {
                try
                {

                    byte[] foto = null;
                    FileStream fs = new FileStream(imgLoc, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    foto = br.ReadBytes((int)fs.Length);
                    string insert = "INSERT INTO Logo(Foto) VALUES(@foto)";
                    string update = "UPDATE Logo SET Foto=@foto Where ID_Logo=1";
                    if (conn.State != ConnectionState.Open)
                    {
                        if (picLogo.Image != null)
                        {
                            conn.Open();
                            command = new SqlCommand(update, conn);
                            command.Parameters.Add(new SqlParameter("@foto", foto));
                            int x = command.ExecuteNonQuery();
                            conn.Close();
                            MessageBox.Show("Logo u ndryshua.");
                        }
                        else
                        {
                            conn.Open();
                            command = new SqlCommand(insert, conn);
                            command.Parameters.Add(new SqlParameter("@foto", foto));
                            int x = command.ExecuteNonQuery();
                            conn.Close();
                            MessageBox.Show("Logoja u vendos.");
                            picLogo.Image = null;
                        }

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                ParaqitLogon();
            }
        }

        private void Administrimi_Load(object sender, EventArgs e)
        {
            ParaqitLogon();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime Data = DateTime.Now;
            lblKoha.Text = Data.ToString();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Info = new Info();
            Info.Show();
        }
    }
}
