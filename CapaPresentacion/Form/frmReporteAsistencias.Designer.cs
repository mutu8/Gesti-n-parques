namespace CapaPresentacion
{
    partial class frmReporteAsistencias
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.domainUpDown1 = new System.Windows.Forms.DomainUpDown();
            this.cboMeses = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.btnCancelar = new MaterialSkin.Controls.MaterialButton();
            this.btnGenerar = new MaterialSkin.Controls.MaterialButton();
            this.Seleccione = new System.Windows.Forms.CheckBox();
            this.comboBoxPersonal = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxPersonal);
            this.groupBox1.Controls.Add(this.Seleccione);
            this.groupBox1.Controls.Add(this.domainUpDown1);
            this.groupBox1.Controls.Add(this.cboMeses);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.btnCancelar);
            this.groupBox1.Controls.Add(this.btnGenerar);
            this.groupBox1.Location = new System.Drawing.Point(6, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(555, 166);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // domainUpDown1
            // 
            this.domainUpDown1.Location = new System.Drawing.Point(426, 55);
            this.domainUpDown1.Name = "domainUpDown1";
            this.domainUpDown1.ReadOnly = true;
            this.domainUpDown1.Size = new System.Drawing.Size(111, 20);
            this.domainUpDown1.TabIndex = 51;
            // 
            // cboMeses
            // 
            this.cboMeses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMeses.FormattingEnabled = true;
            this.cboMeses.Location = new System.Drawing.Point(271, 54);
            this.cboMeses.Name = "cboMeses";
            this.cboMeses.Size = new System.Drawing.Size(131, 21);
            this.cboMeses.TabIndex = 50;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(154, 55);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(248, 20);
            this.dateTimePicker1.TabIndex = 48;
            // 
            // btnCancelar
            // 
            this.btnCancelar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCancelar.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnCancelar.Depth = 0;
            this.btnCancelar.HighEmphasis = true;
            this.btnCancelar.Icon = null;
            this.btnCancelar.Location = new System.Drawing.Point(280, 96);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnCancelar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnCancelar.Size = new System.Drawing.Size(96, 36);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnCancelar.UseAccentColor = false;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGenerar
            // 
            this.btnGenerar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnGenerar.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnGenerar.Depth = 0;
            this.btnGenerar.HighEmphasis = true;
            this.btnGenerar.Icon = null;
            this.btnGenerar.Location = new System.Drawing.Point(185, 96);
            this.btnGenerar.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnGenerar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnGenerar.Size = new System.Drawing.Size(87, 36);
            this.btnGenerar.TabIndex = 0;
            this.btnGenerar.Text = "Generar";
            this.btnGenerar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnGenerar.UseAccentColor = false;
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // Seleccione
            // 
            this.Seleccione.AutoSize = true;
            this.Seleccione.Location = new System.Drawing.Point(229, 19);
            this.Seleccione.Name = "Seleccione";
            this.Seleccione.Size = new System.Drawing.Size(79, 17);
            this.Seleccione.TabIndex = 52;
            this.Seleccione.Text = "Seleccione";
            this.Seleccione.UseVisualStyleBackColor = true;
            this.Seleccione.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // comboBoxPersonal
            // 
            this.comboBoxPersonal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPersonal.FormattingEnabled = true;
            this.comboBoxPersonal.Location = new System.Drawing.Point(16, 54);
            this.comboBoxPersonal.Name = "comboBoxPersonal";
            this.comboBoxPersonal.Size = new System.Drawing.Size(232, 21);
            this.comboBoxPersonal.TabIndex = 53;
            // 
            // frmReporteAsistencias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 249);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmReporteAsistencias";
            this.Sizable = false;
            this.Load += new System.EventHandler(this.frmReporteAsistencias_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DomainUpDown domainUpDown1;
        private System.Windows.Forms.ComboBox cboMeses;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private MaterialSkin.Controls.MaterialButton btnCancelar;
        private MaterialSkin.Controls.MaterialButton btnGenerar;
        private System.Windows.Forms.CheckBox Seleccione;
        private System.Windows.Forms.ComboBox comboBoxPersonal;
    }
}