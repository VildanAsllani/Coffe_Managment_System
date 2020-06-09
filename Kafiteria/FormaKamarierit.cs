using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Printing;

namespace Kafiteria
{
    public partial class FormaKamarierit : Form
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["KonektimiMeDatabaze"].ToString());
        SqlDataAdapter sda;
        DataTable dt;
        SqlCommand command;

        int Tavolina,ID_Kamarieri;
        string EmriKamarierit;
        
        public FormaKamarierit(int Tav,int IDK,string Emri)
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            EmriKamarierit = Emri;
            Tavolina = Convert.ToInt32(Tav);
            ID_Kamarieri=IDK;
            lblTavolina.Text = "Tavolina: "+Tavolina;
            lblKamarieri.Text = "Perdoruesi: " + EmriKamarierit;
            //lblPerdoruesi.Text = (lblPerdoruesi.Text+" "+EmriKamarierit);
            //lblNumriTavolines.Text=(lblNumriTavolines.Text+" "+Tavolina);
        }
        void Kama()
        {
            if (ShitjetGrid.Rows.Count > 1)
            {
                int ID_Puntori = Int32.Parse(ShitjetGrid.Rows[0].Cells["ID_Puntori"].Value.ToString());
                //MessageBox.Show(ID_Kamarieri + "=" + ID_Puntori);
                if (ID_Kamarieri == ID_Puntori)
                {
                    //MessageBox.Show("Ky puntor eshte i fatures.");
                }
                else
                {
                    Form Tavolinat = new Tavolinat();
                    Tavolinat.Show();
                    this.Hide();
                    MessageBox.Show("Tavolina eshte e zene nga kamarieri tjeter.");
                }
            }
            else if(ShitjetGrid.Rows.Count <= 1){
                //MessageBox.Show("SKa produkte");
            }
        }
        void Kafet()
        {
            string query = string.Format("select p.Produkti,p.ID_Produkti,k.Kategoria FROM Produktet as p, Kategorite as k WHERE p.ID_Kategoria=k.ID_Kategoria AND k.ID_Kategoria=1");
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.Fill(dt);
            ds.Tables.Add(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Button button = new Button();
                button.FlatStyle = FlatStyle.Flat;

                button.BackColor = Color.FromArgb(229, 229, 229);
                button.ForeColor = Color.Black;

                button.FlatAppearance.BorderColor = Color.FromArgb(51, 51, 51);
                button.FlatAppearance.BorderSize = 0;

                button.Text = dr["Produkti"].ToString();
                button.Font = new Font("Microsoft Sans Serif", 12);
                button.Size = new Size(176, 64);
                flowLayoutPanel2.AutoScroll = true;
                flowLayoutPanel2.Controls.Add(button);
                button.Click += new System.EventHandler(Button_Click_Produkti);
            }
        }

        void ParaqitShitjet()
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT pun.ID_Puntori,prod.Produkti,s.Hide,prod.Qmimi,s.Sasia,s.Totali,f.ID_Tavolina,f.ID_Fatura,s.ID_Shitja FROM Shitjet as s,Puntoret as pun, Faturat as f, Produktet as prod WHERE ((s.ID_Fatura=f.ID_Fatura AND prod.ID_Produkti=s.ID_Produkti AND s.ID_Puntori=pun.ID_Puntori) AND (s.Hide=0 AND f.ID_Tavolina=" + Tavolina + "))", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            ShitjetGrid.DataSource = dt;
            this.ShitjetGrid.Columns[0].Visible = false;
            this.ShitjetGrid.Columns[2].Visible = false;
            //this.ShitjetGrid.Columns[6].Visible = false;
            //this.ShitjetGrid.Columns[7].Visible = false;
            this.ShitjetGrid.Columns[8].Visible = false;
            dataGridViewFatura.Columns[4].Visible = false;
            dataGridViewFatura.Columns[5].Visible = false;
            //ShitjetGrid.ScrollBars = ScrollBars.None;
        }

        private void FormaKamarierit_Load(object sender, EventArgs e)
        {
            ParaqitShitjet();
            Kama();
            Kafet();
            TOTALET();
            conn.Open();
            string query = string.Format("SELECT * FROM Kategorite");
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.Fill(dt);
            ds.Tables.Add(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Button button = new Button();
                button.FlatStyle = FlatStyle.Flat;
                button.BackColor = Color.FromArgb(106, 137, 200);
                button.ForeColor = Color.White;

                button.FlatAppearance.BorderColor = Color.FromArgb(9, 178, 207);
                button.FlatAppearance.BorderSize = 0;

                button.Text = dr["Kategoria"].ToString();
                button.Font = new Font("Microsoft Sans Serif", 14);
                button.Size = new Size(176, 64);
                flowLayoutPanel1.Controls.Add(button);
                button.Click += new System.EventHandler(Button_Click_Kategorin);
            }
        }
        void Button_Click_Kategorin(object sender, EventArgs e)
        {
            Button ID = (Button)sender;
            flowLayoutPanel2.Controls.Clear();
            string query = string.Format("select p.Produkti,p.ID_Produkti,k.Kategoria FROM Produktet as p, Kategorite as k WHERE p.ID_Kategoria=k.ID_Kategoria AND k.Kategoria='" + ID.Text + "'");
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.Fill(dt);
            ds.Tables.Add(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Button button = new Button();
                button.FlatStyle = FlatStyle.Flat;

                button.BackColor = Color.FromArgb(229, 229, 229);
                button.ForeColor = Color.Black;

                button.FlatAppearance.BorderColor = Color.FromArgb(51, 51, 51);
                button.FlatAppearance.BorderSize = 0;

                button.Text = dr["Produkti"].ToString();
                button.Font = new Font("Microsoft Sans Serif", 12);
                button.Size = new Size(176, 64);
                flowLayoutPanel2.AutoScroll = true;
                flowLayoutPanel2.Controls.Add(button);
                button.Click += new System.EventHandler(Button_Click_Produkti);
            }
        }

        void Button_Click_Produkti(object sender, EventArgs e)
        {
            Button ID = (Button)sender;
            string query = string.Format("SELECT * FROM Produktet WHERE Produkti='" + ID.Text + "'");
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.Fill(dt);
            ds.Tables.Add(dt);

            foreach (DataRow dr in dt.Rows)
            {
                int Depo;
                Depo = Convert.ToInt32(dr["Depo"]);
                Button button = new Button();
                button.FlatStyle = FlatStyle.Flat;
                button.BackColor = System.Drawing.Color.FromArgb(255, 232, 102);
                button.Text = dr["Produkti"].ToString();
                button.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                button.Size = new Size(105, 100);
                flowLayoutPanel2.AutoScroll = true;
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Produktet WHERE Produkti='" + (dr["Produkti"]) + "'", conn);
                dt = new DataTable();
                ds = new DataSet();
                sda.Fill(dt);
                ds.Tables.Add(dt);
                label4.Enabled = false;
                foreach (DataRow rowProdukti in dt.Rows)
                {
                    int IDP;
                    IDP = Convert.ToInt32(rowProdukti["ID_Produkti"].ToString());
                    if (label4.Text == "" || label4.Text == "0")
                    {
                        if (Depo < 1)
                        {
                            MessageBox.Show("Ju nuk keni kaq sasi ne Depo.");
                        }
                        else
                        {
                            double Totali;
                            int Sasia = 1;
                            int n = dataGridViewFatura.Rows.Add();
                            dataGridViewFatura.Rows[n].Cells[0].Value = rowProdukti["Produkti"];
                            dataGridViewFatura.Rows[n].Cells[1].Value = Sasia;
                            dataGridViewFatura.Rows[n].Cells[2].Value = rowProdukti["Qmimi"];
                            Totali = Sasia * (double)rowProdukti["Qmimi"];
                            dataGridViewFatura.Rows[n].Cells[3].Value = Totali;
                            dataGridViewFatura.Rows[n].Cells[4].Value = IDP;
                            dataGridViewFatura.Rows[n].Cells[5].Value = ID_Kamarieri;
                        }
                    }
                    else if (Depo < Convert.ToInt64(label4.Text))
                    {
                        MessageBox.Show("Ju nuk keni kaq sasi ne Depo.");
                    }
                    else
                    {
                        double Totali;
                        int Sasia;
                        Sasia = Convert.ToInt32(label4.Text);
                        int n = dataGridViewFatura.Rows.Add();
                        dataGridViewFatura.Rows[n].Cells[0].Value = rowProdukti["Produkti"];
                        dataGridViewFatura.Rows[n].Cells[1].Value = Sasia;
                        dataGridViewFatura.Rows[n].Cells[2].Value = rowProdukti["Qmimi"];
                        Totali = Sasia * (double)rowProdukti["Qmimi"];
                        dataGridViewFatura.Rows[n].Cells[3].Value = Totali;
                        dataGridViewFatura.Rows[n].Cells[4].Value = IDP;
                        dataGridViewFatura.Rows[n].Cells[5].Value = ID_Kamarieri;
                    }
                    label4.Text = "";
                }

                TOTALET();
            }
        }






        private void btnMbyll_Click(object sender, EventArgs e)
        {
            Form Tavolinat = new Tavolinat();
            Tavolinat.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }


        void TOTALET()
        {
            double TotaliFatures = 0;
            for (int i = 0; i < dataGridViewFatura.Rows.Count; ++i)
            {
                TotaliFatures = TotaliFatures + Convert.ToDouble(dataGridViewFatura.Rows[i].Cells[3].Value);
            }
            textBox1.Text = TotaliFatures.ToString();
            btntextBox1.Text = TotaliFatures.ToString();

            for (int i = 0; i < ShitjetGrid.Rows.Count; ++i)
            {
                TotaliFatures = TotaliFatures + Convert.ToDouble(ShitjetGrid.Rows[i].Cells[5].Value);
            }
            textBox2.Text = TotaliFatures.ToString();
            btntextBox2.Text = TotaliFatures.ToString();
        }



        void MbushFaturen()
        {
            if (dataGridViewFatura.Rows.Count == 0)
            {

            }
            else
            {
                if (ShitjetGrid.Rows.Count != 1)
                {
                    int ID_Fatura = Int32.Parse(ShitjetGrid.Rows[0].Cells["ID_Fatura"].Value.ToString());
                    foreach (DataGridViewRow row in dataGridViewFatura.Rows)
                    {

                        using (SqlCommand cmd = new SqlCommand("INSERT INTO Shitjet(Data,Sasia,Totali,ID_Produkti,ID_Fatura,ID_Puntori,Hide) VALUES(@Data, @Sasia, @Totali,@ID_Produkti,@ID_Fatura,@ID_Puntori,@Hide)", conn))
                        {
                            int zero = 0;
                            cmd.Parameters.AddWithValue("@Data", DateTime.Now.ToString());
                            cmd.Parameters.AddWithValue("@Sasia", row.Cells["Column2"].Value);
                            cmd.Parameters.AddWithValue("@Totali", row.Cells["Column4"].Value);
                            cmd.Parameters.AddWithValue("@ID_Produkti", row.Cells["Column5"].Value);
                            cmd.Parameters.AddWithValue("@ID_Fatura", ID_Fatura);
                            cmd.Parameters.AddWithValue("@ID_Puntori", row.Cells["Column6"].Value);
                            cmd.Parameters.AddWithValue("@Hide", Convert.ToInt32(zero));
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = new SqlCommand("UPDATE Produktet SET Depo=Depo-@Sasia WHERE ID_Produkti=@ID_Produkti", conn))
                        {
                            cmd.Parameters.AddWithValue("@Sasia", row.Cells["Column2"].Value);
                            cmd.Parameters.AddWithValue("@ID_Produkti", row.Cells["Column5"].Value);
                            cmd.ExecuteNonQuery();
                        }

                    }
                }
                else
                {
                    if (dataGridViewFatura.Rows.Count == 0)
                    {
                        MessageBox.Show("Shtyp Produkte");
                    }
                    else
                    {
                        SqlCommand faturainsert = new SqlCommand("INSERT INTO Faturat(ID_Tavolina) VALUES('" + Tavolina + "')", conn);

                        faturainsert.ExecuteNonQuery();
                        SqlCommand FaturaKrijuar = new SqlCommand("SELECT TOP 1* FROM Faturat ORDER BY ID_Fatura DESC", conn);

                        int result = ((int)FaturaKrijuar.ExecuteScalar());
                        if (result == 0)
                        {
                            MessageBox.Show("Ska fatura");
                        }
                        else
                        {
                            foreach (DataGridViewRow row in dataGridViewFatura.Rows)
                            {

                                using (SqlCommand cmd = new SqlCommand("INSERT INTO Shitjet(Data,Sasia,Totali,ID_Produkti,ID_Fatura,ID_Puntori,Hide) VALUES(@Data, @Sasia, @Totali,@ID_Produkti,@ID_Fatura,@ID_Puntori,@Hide)", conn))
                                {
                                    int zero = 0;
                                    cmd.Parameters.AddWithValue("@Data", DateTime.Now.ToString());
                                    cmd.Parameters.AddWithValue("@Sasia", row.Cells["Column2"].Value);
                                    cmd.Parameters.AddWithValue("@Totali", row.Cells["Column4"].Value);
                                    cmd.Parameters.AddWithValue("@ID_Produkti", row.Cells["Column5"].Value);
                                    cmd.Parameters.AddWithValue("@ID_Fatura", result);
                                    cmd.Parameters.AddWithValue("@ID_Puntori", row.Cells["Column6"].Value);
                                    cmd.Parameters.AddWithValue("@Hide", Convert.ToInt32(zero));
                                    cmd.ExecuteNonQuery();
                                }
                                using (SqlCommand cmd = new SqlCommand("UPDATE Produktet SET Depo=Depo-@Sasia WHERE ID_Produkti=@ID_Produkti", conn))
                                {
                                    cmd.Parameters.AddWithValue("@Sasia", row.Cells["Column2"].Value);
                                    cmd.Parameters.AddWithValue("@ID_Produkti", row.Cells["Column5"].Value);
                                    cmd.ExecuteNonQuery();
                                }

                            }
                        }
                    }
                }
            }
        }
        void Tavolinatelira()
        {
            for (int i = 0; i < ShitjetGrid.Rows.Count; i++)
            {
                SqlCommand cmd = new SqlCommand("UPDATE Tavolinat SET Hide=1 WHERE ID_Tavolina=" + Tavolina , conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        void Tavolinatembylluara()
        {
            if (dataGridViewFatura.RowCount >= 1)
            {
                SqlCommand cmd = new SqlCommand("UPDATE Tavolinat SET Hide=0 WHERE ID_Tavolina=" + Tavolina, conn);
                //label1.Text = "";
                cmd.ExecuteNonQuery();
            }
            else
            {
                // MessageBox.Show("Ska produkte");
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            Button num = (Button)sender;
            if (label4.Text == "")
            {
                label4.Text = num.Text;
            }
            else
            {
                label4.Text = (label4.Text + num.Text);
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            label4.Text = "";
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            if (dataGridViewFatura.Rows.Count == 0)
            {
                conn.Close();
                if (ShitjetGrid.Rows.Count == 1)
                {
                    button13.Enabled = false;
                }
                else
                {
                    PrintimiFatures();
                    for (int i = 0; i < ShitjetGrid.Rows.Count; i++)
                    {
                        SqlCommand cmd = new SqlCommand("UPDATE Shitjet SET Hide=1 WHERE ID_Shitja=@ID_Shitja", conn);
                        cmd.Parameters.AddWithValue("@ID_Shitja", Convert.ToInt32(ShitjetGrid.Rows[i].Cells[8].Value));
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    Tavolinatelira();

                    Form Tavolinat = new Tavolinat();
                    Tavolinat.Show();
                    this.Hide();
                }
            }
            else
            {
                button13.Enabled = false;
            }
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            MbushFaturen();
            Tavolinatembylluara();
            if (dataGridViewFatura.Rows.Count == 0)
            {

            }
            else
            {
                //PrintimiFaturesPerShankist();
            }

            ParaqitShitjet();
            dataGridViewFatura.Rows.Clear();
            //Form Tavolinat = new Tavolinat();
            //Tavolinat.Show();
            //this.Hide();
            lblZbritje.Text = "";
            TOTALET();
        }

        private void dataGridViewFatura_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            if (dataGridViewFatura.Rows.Count == 0)
            {
                MessageBox.Show("Nuk ka produkte per te fshire");
            }
            else
            {
                int rowIndex = dataGridViewFatura.CurrentCell.RowIndex;
                dataGridViewFatura.Rows.RemoveAt(rowIndex);
            }
            TOTALET();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            string selected = this.cmbZbritje.GetItemText(this.cmbZbritje.SelectedItem);
            if (selected == "")
            {
                lblZbritje.Text = "Ju lutem zgjedhni nje zbritje.";
            }
            else
            {
                if (dataGridViewFatura.Rows.Count == 0)
                {
                    MessageBox.Show("Ska Produkte");
                }
                else
                {
                    for (int i = 0; i < dataGridViewFatura.Rows.Count; ++i)
                    {
                        double tot,percent,newtotali;
                        tot = Convert.ToDouble(dataGridViewFatura.Rows[i].Cells[3].Value);
                        percent = Convert.ToDouble(selected) / 100;
                        newtotali = tot * percent;
                        dataGridViewFatura.Rows[i].Cells[3].Value = (tot-newtotali);
                    }
                    lblZbritje.Text = "Zbritje me sukses per ."+selected+"%";
                }
            }
            TOTALET();
        }

        private void PrintimiFatures()
        {
            PrintDialog printDialog = new PrintDialog();
            PrintDocument printDocument = new PrintDocument();
            printDialog.Document = printDocument;

            printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
            DialogResult result = printDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                printDocument.Print();
            }
        }
        void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphic = e.Graphics;
            Font font = new Font("Courier New", 12);
            float fontHeight = font.GetHeight();
            int startX = 10;
            int startY = 10;
            int offset = 40;

            string Produktii = "Artikulli";
            string Qmimii = "Qmimi";
            Produktii = Produktii.PadRight(25);
            string PrdQmi = Produktii + Qmimii;


            graphic.DrawString("Ju mirëpresim përseri!", new Font("Courier New", 20), new SolidBrush(Color.Black), startX, startY);
            offset = offset + (int)fontHeight + 5;
            graphic.DrawString(PrdQmi, new Font("Courier New", 15), new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontHeight + 5;
            graphic.DrawString("-------------------------", new Font("Courier New", 18), new SolidBrush(Color.Black), startX, startY + offset);
            //offset = offset + (int)fontHeight + 10;
            int art = 0;
            foreach (DataGridViewRow dr in ShitjetGrid.Rows)
            {
                string Produkti = Convert.ToString(dr.Cells["Produkti"].Value);
                string Qmimi = Convert.ToString(dr.Cells["Qmimi"].Value) + "x";
                string Sasia = Convert.ToString(dr.Cells["Sasia"].Value);
                string Totali = Convert.ToString(dr.Cells["Totali"].Value);

                string QmimSasia = Qmimi + Sasia;
                if (QmimSasia == "x")
                {

                }
                else
                {
                    offset = offset + (int)fontHeight + 3;
                    string QSLine = QmimSasia.PadLeft(30);
                    graphic.DrawString(QSLine, new Font("Courier New", 15), new SolidBrush(Color.Black), startX, startY + offset);
                    offset = offset + (int)fontHeight + 10;

                    string right = "_____";
                    right = right.PadLeft(30);
                    graphic.DrawString(right, new Font("Courier New", 15), new SolidBrush(Color.Black), startX, startY + offset);


                    string Prodpad = Produkti.PadRight(25);
                    Totali = String.Format("{0:c}", Totali);
                    string prdline = Prodpad + Totali;
                    graphic.DrawString(prdline, new Font("Courier New", 15), new SolidBrush(Color.Black), startX, startY + offset);
                    offset = offset + (int)fontHeight + 1;
                }
                art = art + Convert.ToInt32(dr.Cells["Sasia"].Value);
            }
            //offset = offset + (int)fontHeight + 10;
            graphic.DrawString("-------------------------", new Font("Courier New", 18), new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontHeight + 5;



            string Totalii = "Totali".PadRight(28);
            textBox2.Text = String.Format("{0:c}", textBox2.Text);
            string TotaliFatures = Totalii + textBox2.Text;
            graphic.DrawString(TotaliFatures + "€", new Font("Courier New", 13), new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontHeight + 5;



            //TOTALET ME TVSH DHE TVSH.ja

            double TVSH = 0.18;
            double TOT = Convert.ToDouble(textBox2.Text);
            double patvsh = (TOT - (TOT * TVSH));
            double patvsh1 = TOT * TVSH;
            //MessageBox.Show("TVSH=" + TVSH + ", PaTVSH" + patvsh);


            string TVSHTotalit = "TVSH 18%".PadRight(28);
            string PATVSH = patvsh1.ToString();
            PATVSH = String.Format("{0:c}", PATVSH);
            string PA = TVSHTotalit + PATVSH;
            graphic.DrawString(PA + "€", new Font("Courier New", 13), new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontHeight + 5;


            string TOTALIpatvsh = "TOTALI PA TVSH".PadRight(28);
            string TPATVSH = patvsh.ToString();
            TPATVSH = String.Format("{0:c}", TPATVSH);
            string BTPTVSH = TOTALIpatvsh + TPATVSH;
            graphic.DrawString(BTPTVSH + "€", new Font("Courier New", 13), new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontHeight + 5;


            //Perfundimi TVSH

            string Artikuj = "Artikuj  " + art;
            graphic.DrawString(Artikuj, new Font("Courier New", 13), new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontHeight + 5;

            string Tavolina = "Tavolina " + ShitjetGrid.Rows[0].Cells["ID_Tavolina"].Value.ToString();
            graphic.DrawString(Tavolina, new Font("Courier New", 13), new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontHeight + 5;

            string Fatura = "Fatura   " + ShitjetGrid.Rows[0].Cells["ID_Fatura"].Value.ToString();
            graphic.DrawString(Fatura, new Font("Courier New", 13), new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontHeight + 5;

            DateTime DT = DateTime.Now;
            graphic.DrawString(DT.ToString(), new Font("Courier New", 13), new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontHeight + 5;

            graphic.DrawString("                 Ju faleminderit.               ", new Font("Courier New", 11), new SolidBrush(Color.Black), startX, startY + offset);

        }


    }
}
