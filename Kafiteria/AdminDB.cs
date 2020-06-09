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
    public partial class AdminDB : Form
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["KonektimiMeDatabaze"].ToString());
        SqlDataAdapter sda;
        DataTable dt;
        SqlCommand command;

        int ID_Admin;
        string EmriAdministratorit;

        public AdminDB(int IDA, string EmriAdm)
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            tabControl1.SelectedIndexChanged += new EventHandler(tabControl1_SelectedIndexChanged);
            ID_Admin = IDA;
            EmriAdministratorit = EmriAdm;
            label20.Visible = false;
            label24.Visible = false;
            label27.Visible = false;
            ParaqitKategorite();
        }

        private void btnMbrapa_Click(object sender, EventArgs e)
        {
            Form Administrimi = new Administrimi(ID_Admin, EmriAdministratorit);
            Administrimi.Show();
            this.Hide();
        }

        //KETU FILLON KODI I KATEGORIVE

        void ParaqitKategorite()
        {
            sda = new SqlDataAdapter("SELECT * FROM Kategorite", conn);
            dt = new DataTable();
            sda.Fill(dt);
            dataGridViewKategorite.DataSource = dt;
            this.dataGridViewKategorite.Columns[0].Visible = false;
            DataGridViewColumn column = dataGridViewKategorite.Columns[1];
            column.Width = 125;
        }

        private void btnShtoKategorine_Click(object sender, EventArgs e)
        {
            if (txtKategoria.Text != "")
            {
                conn.Open();
                sda = new SqlDataAdapter("INSERT INTO Kategorite(Kategoria) VALUES('" + txtKategoria.Text + "')", conn);
                sda.SelectCommand.ExecuteNonQuery();
                conn.Close();
                ParaqitKategorite();
                MessageBox.Show("Kategoria e re u shtua me sukses");
                txtKategoria.Text = "";
                label27.Visible = false;
            }
            else
            {
                label27.Visible = true;
            }
        }

        private void btnFshijeKategorine_Click(object sender, EventArgs e)
        {
            conn.Open();
            sda = new SqlDataAdapter("DELETE FROM Kategorite WHERE ID_Kategoria='" + ID_Kategoria.Text + "'", conn);
            sda.SelectCommand.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Kategoria u fshi me sukses.");
            ParaqitKategorite();
            txtNdryshoKategorine.Clear();
            ID_Kategoria.Text = "";
            btnFshijeKategorine.Enabled = false;
            btnRuajNdryshimet.Enabled = false;
        }

        private void dataGridViewKategorite_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridViewKategorite.SelectedRows[0].Cells[1].Value.ToString() == "")
            {

            }
            else
            {

                ID_Kategoria.Text = dataGridViewKategorite.SelectedRows[0].Cells[0].Value.ToString();
                txtNdryshoKategorine.Text = dataGridViewKategorite.SelectedRows[0].Cells[1].Value.ToString();
                btnFshijeKategorine.Enabled = true;
                btnRuajNdryshimet.Enabled = true;
            }
        }

        private void btnRuajNdryshimet_Click(object sender, EventArgs e)
        {
            if (txtNdryshoKategorine.Text != "")
            {
                conn.Open();
                sda = new SqlDataAdapter("UPDATE Kategorite SET Kategoria='" + txtNdryshoKategorine.Text + "' WHERE ID_Kategoria='" + ID_Kategoria.Text + "'", conn);
                sda.SelectCommand.ExecuteNonQuery();
                conn.Close();
                ParaqitKategorite();
                MessageBox.Show("Kategoria u ndryshua me sukses.");
                txtNdryshoKategorine.Text = "";
                ID_Kategoria.Text = "";
                btnRuajNdryshimet.Enabled = false;
                btnFshijeKategorine.Enabled = false;

            }
            else
            {
                label24.Visible = true;
            }
        }

        private void btnKerkoKategorine_Click(object sender, EventArgs e)
        {
            if (txtKerkoKategorine.Text != "")
            {
                conn.Open();
                sda = new SqlDataAdapter("SELECT * FROM Kategorite WHERE Kategoria LIKE '%" + txtKerkoKategorine.Text + "%'", conn);
                dt = new DataTable();
                sda.Fill(dt);
                dataGridViewKategorite.DataSource = dt;
                conn.Close();
                label20.Visible = false;
                btnRuajNdryshimet.Enabled = false;
                btnFshijeKategorine.Enabled = false;
                txtKerkoKategorine.Text = "";
            }
            else
            {
                label20.Visible = true;
            }
        }
        //KETU PERFUNDON KODI I KATEGORIVE

        //KETU FILLON KODI I PUNTOREVE
        void ParaqitPuntoret()
        {
            sda = new SqlDataAdapter("SELECT p.ID_Puntori,p.Emri,p.Fjalkalimi,poz.Pozita FROM Puntoret as p, Pozitat as poz WHERE p.ID_Pozita=poz.ID_Pozita", conn);
            dt = new DataTable();
            sda.Fill(dt);
            dataGridPuntoret.DataSource = dt;
            btnFshijePuntorin.Visible = false;
            btnNdryshoPuntorin.Visible = false;
            this.dataGridPuntoret.Columns[0].Visible = false;
            this.dataGridPuntoret.Columns[2].Visible = false;
            DataGridViewColumn column = dataGridPuntoret.Columns[1];
            DataGridViewColumn columnn = dataGridPuntoret.Columns[3];
            column.Width = 125;
            columnn.Width = 125;
            txtFjalkalimiPuntorit.PasswordChar = '●';
        }

        private void btnShtoPuntorin_Click(object sender, EventArgs e)
        {
            if (txtEmriPuntorit.Text == "")
            {
                label33.Visible = true;
            }
            else if (txtFjalkalimiPuntorit.Text == "")
            {
                label35.Visible = true;
            }
            else
            {
                if (cmbPozitat.Text == "Select...") { label37.Visible = true; }
                else
                {
                    conn.Open();
                    sda = new SqlDataAdapter("INSERT INTO Puntoret(Emri,Fjalkalimi,ID_Pozita) VALUES('" + txtEmriPuntorit.Text + "','" + txtFjalkalimiPuntorit.Text + "','" + cmbPozitat.SelectedValue + "')", conn);
                    sda.SelectCommand.ExecuteNonQuery();
                    conn.Close();
                    ParaqitPuntoret();
                    MessageBox.Show("Puntori i ri u shtua me sukses.");
                    label33.Visible = false;
                    label35.Visible = false;
                    label37.Visible = false;
                    cmbPozitat.Text = "Select...";
                    txtEmriPuntorit.Text = "";
                    txtFjalkalimiPuntorit.Text = "";
                }
            }
        }

        void CMBPozita()
        {
            fill_comboPozitat("Pozitat", cmbPozitat);
        }
        public void fill_comboPozitat(string table, ComboBox cmb, string columns = "*")
        {
            try
            {
                var dt = new DataTable();

                using (SqlDataAdapter da = new SqlDataAdapter("SELECT " + columns + " FROM [" + table + "] WHERE ID_Pozita NOT IN(5)", conn))
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

        private void btnNdryshoPuntorin_Click(object sender, EventArgs e)
        {
            if (txtEmriPuntorit.Text == "")
            {
                label33.Visible = true;
            }
            else if (txtFjalkalimiPuntorit.Text == "")
            {
                label35.Visible = true;
            }
            else
            {
                if (cmbPozitat.Text == "Select...") { label37.Visible = true; }
                else
                {
                    conn.Open();
                    sda = new SqlDataAdapter("UPDATE Puntoret SET Emri='" + txtEmriPuntorit.Text + "',Fjalkalimi='" + txtFjalkalimiPuntorit.Text + "',ID_Pozita='" + cmbPozitat.SelectedValue + "' WHERE ID_Puntori='" + lblID_Puntori.Text + "'", conn);
                    sda.SelectCommand.ExecuteNonQuery();
                    conn.Close();
                    ParaqitPuntoret();
                    MessageBox.Show("Te dhenat u ndryshuan me sukses");
                    label33.Visible = false;
                    label35.Visible = false;
                    label37.Visible = false;
                    cmbPozitat.Text = "Select...";
                    txtEmriPuntorit.Text = "";
                    txtFjalkalimiPuntorit.Text = "";
                }
            }
        }

        private void dataGridPuntoret_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridPuntoret.SelectedRows[0].Cells[0].Value.ToString() == "6")
            {
                MessageBox.Show("Nuk mund ta mofidikoni kete user ju lutem mos provoni prap.");
                btnShtoPuntorin.Enabled = true;
            }
            else
            {
                if (dataGridPuntoret.SelectedRows[0].Cells[1].Value.ToString() == "")
                {

                }
                else
                {
                    lblID_Puntori.Text = dataGridPuntoret.SelectedRows[0].Cells[0].Value.ToString();
                    txtEmriPuntorit.Text = dataGridPuntoret.SelectedRows[0].Cells[1].Value.ToString();
                    txtFjalkalimiPuntorit.Text = dataGridPuntoret.SelectedRows[0].Cells[2].Value.ToString();
                    cmbPozitat.Text = dataGridPuntoret.SelectedRows[0].Cells[3].Value.ToString();
                    btnShtoPuntorin.Enabled = false;
                    btnFshijePuntorin.Visible = true;
                    btnNdryshoPuntorin.Visible = true;
                }
            }
        }

        private void btnFshijePuntorin_Click(object sender, EventArgs e)
        {
            if (lblID_Puntori.Text != "")
            {
                conn.Open();
                sda = new SqlDataAdapter("DELETE FROM Puntoret WHERE ID_Puntori='" + lblID_Puntori.Text + "'", conn);
                sda.SelectCommand.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Puntori u fshi me sukses.");
                ParaqitPuntoret();
                cmbPozitat.Text = "Select...";
                txtEmriPuntorit.Text = "";
                txtFjalkalimiPuntorit.Text = "";
                btnFshijePuntorin.Visible = false;
                btnNdryshoPuntorin.Visible = false;
                btnShtoPuntorin.Enabled = true;
                ///VAZHDO 
            }
            else
            {
                MessageBox.Show("Ju nuk keni zgjedhur asnje puntor per ta fshire");
            }
        }

        private void btnKerkoPuntoret_Click(object sender, EventArgs e)
        {
            if (txtKerkoPuntoret.Text != "")
            {
                conn.Open();
                sda = new SqlDataAdapter("SELECT p.ID_Puntori,p.Emri,p.Fjalkalimi,poz.Pozita FROM Puntoret as p, Pozitat as poz WHERE Emri LIKE '%" + txtKerkoPuntoret.Text + "%' AND p.ID_Pozita=poz.ID_Pozita", conn);
                dt = new DataTable();
                sda.Fill(dt);
                dataGridPuntoret.DataSource = dt;
                txtKerkoPuntoret.Text = "";
                conn.Close();
                label30.Visible = false;
            }
            else
            {
                label30.Visible = true;
            }
        }

        private void txtFjalkalimiPuntorit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        //KETU PERFUNDON KODI I PUNTOREVE

        //KETU FILLON KODI I PRODUKTEVE
        void ParaqitProduktet()
        {
            sda = new SqlDataAdapter("SELECT p.ID_Produkti,p.Produkti,p.Njesi,p.Qmimi,p.Depo,k.Kategoria FROM Produktet as p, Kategorite as k WHERE p.ID_Kategoria=k.ID_Kategoria", conn);
            dt = new DataTable();
            sda.Fill(dt);
            dataGridProduktet.DataSource = dt;
            btnFshijeProduktin.Visible = false;
            btnNdryshoProduktin.Visible = false;
            this.dataGridProduktet.Columns[0].Visible = false;
            DataGridViewColumn column = dataGridProduktet.Columns[3];
            column.Width = 60;
            DataGridViewColumn column2 = dataGridProduktet.Columns[4];
            column2.Width = 60;
            DataGridViewColumn column3 = dataGridProduktet.Columns[1];
            column3.Width = 150;
            DataGridViewColumn column4 = dataGridProduktet.Columns[2];
            column4.Width = 100;
        }

        void CMBKategoria()
        {
            fill_comboKategorite("Kategorite", cmbKategorite);
        }
        public void fill_comboKategorite(string table, ComboBox cmb, string columns = "*")
        {
            try
            {
                var dt = new DataTable();

                using (SqlDataAdapter da = new SqlDataAdapter("SELECT " + columns + " FROM [" + table + "]", conn))
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
        private void btnShtoProduktin_Click(object sender, EventArgs e)
        {
            if (txtProdukti.Text == "")
            {
                label48.Visible = true;
            }
            else if (txtNjesia.Text == "")
            {
                label45.Visible = true;
            }
            else if (txtQmimi.Text == "")
            {
                label53.Visible = true;
            }
            else if (txtSasiaNeDepo.Text == "")
            {
                label51.Visible = true;
            }
            else
            {
                if (cmbKategorite.Text == "Select...")
                {
                    label43.Visible = true;
                }
                else
                {
                    double Qmimi;
                    Qmimi = float.Parse(txtQmimi.Text);
                    conn.Open();
                    sda = new SqlDataAdapter("INSERT INTO Produktet(Produkti,Njesi,Qmimi,Depo,ID_Kategoria) VALUES('" + txtProdukti.Text + "','" + txtNjesia.Text + "','" + Qmimi + "','" + txtSasiaNeDepo.Text + "','" + cmbKategorite.SelectedValue + "')", conn);
                    sda.SelectCommand.ExecuteNonQuery();
                    conn.Close();
                    ParaqitProduktet();
                    dataGridProduktet.Refresh();
                    MessageBox.Show("Produkti u shtua me sukses.");
                    txtProdukti.Text = "";
                    txtNjesia.Text = "";
                    txtQmimi.Text = "";
                    txtSasiaNeDepo.Text = "";
                    cmbKategorite.Text = "Select...";
                    label48.Visible = false;
                    label45.Visible = false;
                    label53.Visible = false;
                    label51.Visible = false;
                    label43.Visible = false;
                }

            }
        }

        private void btnNdryshoProduktin_Click(object sender, EventArgs e)
        {
            if (txtProdukti.Text == "")
            {
                label48.Visible = true;
            }
            else if (txtNjesia.Text == "")
            {
                label45.Visible = true;
            }
            else if (txtQmimi.Text == "")
            {
                label53.Visible = true;
            }
            else if (txtSasiaNeDepo.Text == "")
            {
                label51.Visible = true;
            }
            else
            {
                if (cmbKategorite.Text == "Select...")
                {
                    label43.Visible = true;
                }
                else
                {
                    double Qmimi;
                    Qmimi = float.Parse(txtQmimi.Text);
                    conn.Open();
                    sda = new SqlDataAdapter("UPDATE Produktet SET Produkti='" + txtProdukti.Text + "', Njesi='" + txtNjesia.Text + "', Qmimi='" + Qmimi + "', Depo='" + txtSasiaNeDepo.Text + "', ID_Kategoria='" + cmbKategorite.SelectedValue + "' WHERE ID_Produkti='" + lblID_Produkti.Text + "'", conn);
                    sda.SelectCommand.ExecuteNonQuery();
                    conn.Close();
                    ParaqitProduktet();
                    dataGridProduktet.Refresh();
                    MessageBox.Show("Produkti u ndryshua me sukses.");
                    txtProdukti.Text = "";
                    txtNjesia.Text = "";
                    txtQmimi.Text = "";
                    txtSasiaNeDepo.Text = "";
                    cmbKategorite.Text = "Select...";
                    label48.Visible = false;
                    label45.Visible = false;
                    label53.Visible = false;
                    label51.Visible = false;
                    label43.Visible = false;
                    btnFshijeProduktin.Visible = false;
                    btnNdryshoProduktin.Visible = false;
                    btnShtoProduktin.Enabled = true;
                }

            }
        }

        private void dataGridProduktet_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridProduktet.SelectedRows[0].Cells[1].Value.ToString() == "")
            {

            }
            else
            {
                lblID_Produkti.Text = dataGridProduktet.SelectedRows[0].Cells[0].Value.ToString();
                txtProdukti.Text = dataGridProduktet.SelectedRows[0].Cells[1].Value.ToString();
                txtNjesia.Text = dataGridProduktet.SelectedRows[0].Cells[2].Value.ToString();
                txtQmimi.Text = dataGridProduktet.SelectedRows[0].Cells[3].Value.ToString();
                txtSasiaNeDepo.Text = dataGridProduktet.SelectedRows[0].Cells[4].Value.ToString();
                cmbKategorite.Text = dataGridProduktet.SelectedRows[0].Cells[5].Value.ToString();
                btnShtoProduktin.Enabled = false;
                btnFshijeProduktin.Visible = true;
                btnNdryshoProduktin.Visible = true;
            }
        }

        private void btnFshijeProduktin_Click(object sender, EventArgs e)
        {
            conn.Open();
            sda = new SqlDataAdapter("DELETE FROM Produktet WHERE ID_Produkti='" + lblID_Produkti.Text + "'", conn);
            sda.SelectCommand.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Produkti u fshi me sukses.");
            ParaqitProduktet();
            dataGridProduktet.Refresh();
            lblID_Produkti.Text = "";
            txtProdukti.Text = "";
            txtNjesia.Text = "";
            txtQmimi.Text = "";
            txtSasiaNeDepo.Text = "";
            cmbKategorite.Text = "Select...";
            btnFshijeProduktin.Visible = false;
            btnNdryshoProduktin.Visible = false;
            btnShtoProduktin.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtKerkoProduktin.Text != "")
            {
                conn.Open();
                sda = new SqlDataAdapter("SELECT p.ID_Produkti,p.Produkti,p.Njesi,p.Qmimi,p.Depo,k.Kategoria FROM Produktet as p, Kategorite as k WHERE p.ID_Kategoria=k.ID_Kategoria AND p.Produkti LIKE '%" + txtKerkoProduktin.Text + "%'", conn);
                dt = new DataTable();
                sda.Fill(dt);
                dataGridProduktet.DataSource = dt;
                txtKerkoProduktin.Text = "";
                conn.Close();
                label56.Visible = false;
            }
            else
            {
                label56.Visible = true;
            }
        }

        private void txtQmimi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtSasiaNeDepo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        //KETU PERFUNDON KODI I PRODUKTEVE


        //KETU FILLON KODI I SHITJEVE
        void ParaqitShitjet()
        {
            sda = new SqlDataAdapter("SELECT s.Data,pun.Emri,p.Produkti,p.Qmimi,s.Sasia,s.Totali,f.ID_Fatura,f.ID_Tavolina FROM Shitjet as s,Produktet as p, Puntoret as pun,Faturat as f WHERE s.ID_Produkti=p.ID_Produkti AND s.ID_Puntori=pun.ID_Puntori AND s.ID_Fatura=f.ID_Fatura", conn);
            dt = new DataTable();
            sda.Fill(dt);
            dataGridShitjet.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn.Open();
            sda = new SqlDataAdapter("SELECT s.Data,pun.Emri,p.Produkti,p.Qmimi,s.Sasia,s.Totali,f.ID_Fatura,f.ID_Tavolina FROM Shitjet as s,Produktet as p, Puntoret as pun,Faturat as f WHERE s.ID_Produkti=p.ID_Produkti AND s.ID_Puntori=pun.ID_Puntori AND s.ID_Fatura=f.ID_Fatura AND s.Data='" + dateTimePicker1.Text + "'", conn);
            dt = new DataTable();
            sda.Fill(dt);
            dataGridShitjet.DataSource = dt;
            conn.Close();
            dateTimePicker1.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            conn.Open();
            sda = new SqlDataAdapter("SELECT s.Data,pun.Emri,p.Produkti,p.Qmimi,s.Sasia,s.Totali,f.ID_Fatura,f.ID_Tavolina FROM Shitjet as s,Produktet as p, Puntoret as pun,Faturat as f WHERE s.ID_Produkti=p.ID_Produkti AND s.ID_Puntori=pun.ID_Puntori AND s.ID_Fatura=f.ID_Fatura AND s.ID_Fatura='" + textBox4.Text + "'", conn);
            dt = new DataTable();
            sda.Fill(dt);
            dataGridShitjet.DataSource = dt;
            textBox4.Text = "";
            conn.Close();
        }
        //KETU PERFUNDON KODI I SHITJEVE



        private void tabControl1_SelectedIndexChanged(Object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage4)
            {
                txtKerkoKategorine.Text = "";
                txtNdryshoKategorine.Text = "";
                txtKategoria.Text = "";
                ParaqitKategorite();
                label20.Visible = false;
                label24.Visible = false;
                label27.Visible = false;
                btnFshijeKategorine.Enabled = false;
                btnRuajNdryshimet.Enabled = false;
            }
            if (tabControl1.SelectedTab == tabPage5)
            {
                ParaqitPuntoret();
                CMBPozita();
                label33.Visible = false;
                label35.Visible = false;
                label37.Visible = false;
                label30.Visible = false;
                cmbPozitat.Text = "Select...";
                txtEmriPuntorit.Text = "";
                txtFjalkalimiPuntorit.Text = "";
            }
            if (tabControl1.SelectedTab == tabPage6)
            {
                ParaqitProduktet();
                CMBKategoria();
                label43.Visible = false;
                label45.Visible = false;
                label48.Visible = false;
                label51.Visible = false;
                label53.Visible = false;
                label56.Visible = false;
                lblID_Produkti.Text = "";
                txtProdukti.Text = "";
                txtNjesia.Text = "";
                txtQmimi.Text = "";
                txtSasiaNeDepo.Text = "";
                cmbKategorite.Text = "Select...";
                btnNdryshoProduktin.Visible = false;
                btnFshijeProduktin.Visible = false;
                txtKerkoProduktin.Text = "";
            }
            if (tabControl1.SelectedTab == tabPage7)
            {
                ParaqitShitjet();
                dateTimePicker1.Text = "";
                textBox4.Text = "";
            }

        }

       



    }
}
