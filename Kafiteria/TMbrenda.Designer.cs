namespace Kafiteria
{
    partial class TMbrenda
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnMbrapa = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnShtoTavolin = new System.Windows.Forms.Button();
            this.btnRuaj = new System.Windows.Forms.Button();
            this.dataGridTavolinat = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTavolinat)).BeginInit();
            this.SuspendLayout();
            // 
            // btnMbrapa
            // 
            this.btnMbrapa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMbrapa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnMbrapa.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnMbrapa.FlatAppearance.BorderSize = 0;
            this.btnMbrapa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMbrapa.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold);
            this.btnMbrapa.ForeColor = System.Drawing.Color.White;
            this.btnMbrapa.Location = new System.Drawing.Point(1181, 10);
            this.btnMbrapa.Name = "btnMbrapa";
            this.btnMbrapa.Size = new System.Drawing.Size(74, 41);
            this.btnMbrapa.TabIndex = 5;
            this.btnMbrapa.Text = "Mbrapa";
            this.btnMbrapa.UseVisualStyleBackColor = false;
            this.btnMbrapa.Click += new System.EventHandler(this.btnMbrapa_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(34, 65);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1194, 611);
            this.panel1.TabIndex = 6;
            // 
            // btnShtoTavolin
            // 
            this.btnShtoTavolin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShtoTavolin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShtoTavolin.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShtoTavolin.Location = new System.Drawing.Point(996, 685);
            this.btnShtoTavolin.Name = "btnShtoTavolin";
            this.btnShtoTavolin.Size = new System.Drawing.Size(59, 48);
            this.btnShtoTavolin.TabIndex = 7;
            this.btnShtoTavolin.Text = "+";
            this.btnShtoTavolin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnShtoTavolin.UseVisualStyleBackColor = true;
            this.btnShtoTavolin.Click += new System.EventHandler(this.btnShtoTavolin_Click);
            // 
            // btnRuaj
            // 
            this.btnRuaj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRuaj.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(137)))), ((int)(((byte)(200)))));
            this.btnRuaj.FlatAppearance.BorderSize = 0;
            this.btnRuaj.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRuaj.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRuaj.ForeColor = System.Drawing.Color.White;
            this.btnRuaj.Location = new System.Drawing.Point(1061, 685);
            this.btnRuaj.Name = "btnRuaj";
            this.btnRuaj.Size = new System.Drawing.Size(167, 48);
            this.btnRuaj.TabIndex = 8;
            this.btnRuaj.Text = "Ruaj Ndryshimet";
            this.btnRuaj.UseVisualStyleBackColor = false;
            this.btnRuaj.Click += new System.EventHandler(this.btnRuaj_Click);
            // 
            // dataGridTavolinat
            // 
            this.dataGridTavolinat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridTavolinat.Location = new System.Drawing.Point(34, 702);
            this.dataGridTavolinat.Name = "dataGridTavolinat";
            this.dataGridTavolinat.Size = new System.Drawing.Size(24, 57);
            this.dataGridTavolinat.TabIndex = 0;
            this.dataGridTavolinat.Visible = false;
            // 
            // TMbrenda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1264, 742);
            this.Controls.Add(this.dataGridTavolinat);
            this.Controls.Add(this.btnRuaj);
            this.Controls.Add(this.btnShtoTavolin);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnMbrapa);
            this.Name = "TMbrenda";
            this.Text = "TMbrenda";
            this.Load += new System.EventHandler(this.TMbrenda_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTavolinat)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnMbrapa;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnShtoTavolin;
        private System.Windows.Forms.Button btnRuaj;
        private System.Windows.Forms.DataGridView dataGridTavolinat;
    }
}