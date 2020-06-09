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
    public partial class TMbrenda : Form
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["KonektimiMeDatabaze"].ToString());
        SqlDataAdapter sda;
        DataTable dt;
        SqlCommand command;

        private Point MouseDownLocation;
        int Tavolina;
        int PozicioniX,PozicioniY;
        int ID_Admin;
        string EmriAdministratorit;

        public TMbrenda(int IDA, string EmriAdm)
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            ID_Admin = IDA;
            EmriAdministratorit = EmriAdm;
        }

        private void TMbrenda_Load(object sender, EventArgs e)
        {
            ShfaqTavolinat();
        }

        private void btnMbrapa_Click(object sender, EventArgs e)
        {
            Form Administrimi = new Administrimi(ID_Admin, EmriAdministratorit);
            Administrimi.Show();
            this.Hide();
        }
        void ShfaqTavolinat()
        {
            conn.Open();
            string query = string.Format("SELECT * FROM Tavolinat where ID_Lokacioni=1");
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.Fill(dt);
            ds.Tables.Add(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Button button = new Button();
                Point location = button.Location;
                int xLocation = Convert.ToInt32(dr["X"]);
                int yLocation = Convert.ToInt32(dr["Y"]);
                button.Location = new Point(xLocation, yLocation);

                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.BackColor = Color.FromArgb(191, 191, 191);
                button.ForeColor = Color.Black;
                button.TextAlign = ContentAlignment.BottomLeft;
                button.Margin = new Padding(30, 40, 0, 0);
                button.Text = dr["ID_Tavolina"].ToString();
                button.Font = new Font("Arial Narrow", 15);
                button.Size = new Size(55, 55);
                panel1.Controls.Add(button);
                //button.Click += new EventHandler(button_Click);
                button.MouseMove += new MouseEventHandler(button_MouseMove);
                button.MouseLeave += new EventHandler(button_MouseLeave);

            }
            conn.Close();
        }

        void button_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Button button = (Button)sender;
                //label1.Text = button.Text;
                Tavolina = Convert.ToInt32(button.Text);

                button.Left = e.X + button.Left - MouseDownLocation.X;
                button.Top = e.Y + button.Top - MouseDownLocation.Y;
                Point location = button.Location;
                int xLocation = button.Location.X;
                int yLocation = button.Location.Y;
                button.Location = new Point(xLocation, yLocation);
                PozicioniX = xLocation;
                PozicioniY = yLocation;
                //label2.Text = xLocation.ToString();
                //label3.Text = yLocation.ToString();
            }
        }
        void button_MouseLeave(object sender, EventArgs e)
        {
            conn.Open();
            sda = new SqlDataAdapter("UPDATE Tavolinat SET X='" + PozicioniX + "', Y='" + PozicioniY + "' WHERE ID_Tavolina='" + Tavolina + "'", conn);
            sda.SelectCommand.ExecuteNonQuery();
            conn.Close();
        }

        private void btnShtoTavolin_Click(object sender, EventArgs e)
        {
            conn.Open();
            sda = new SqlDataAdapter("INSERT INTO Tavolinat(X,Y,Hide,ID_Lokacioni) VALUES(1,1,1,1)", conn);
            sda.SelectCommand.ExecuteNonQuery();
            conn.Close();
            ParaqitTavolinat();
            panel1.Update();
            //ShfaqTavolinat();
            if (btnShtoTavolin.Text != "+")
            {
                btnShtoTavolin.Text = (Convert.ToInt32(btnShtoTavolin.Text) + 1).ToString();
            }
            else if(btnShtoTavolin.Text=="+")
            {
                btnShtoTavolin.Text = "1";
            }
        }
        void ParaqitTavolinat()
        {
            sda = new SqlDataAdapter("SELECT * FROM Tavolinat", conn);
            dt = new DataTable();
            sda.Fill(dt);
            dataGridTavolinat.DataSource = dt;
            this.dataGridTavolinat.Columns[1].Visible = false;
            this.dataGridTavolinat.Columns[2].Visible = false;
            this.dataGridTavolinat.Columns[3].Visible = false;
            DataGridViewColumn column = dataGridTavolinat.Columns[0];
            column.Width = 100;
        }

        private void btnRuaj_Click(object sender, EventArgs e)
        {
            Form TMbrenda = new TMbrenda(ID_Admin, EmriAdministratorit);
            TMbrenda.Show();
            this.Hide();
        }

    }
}
