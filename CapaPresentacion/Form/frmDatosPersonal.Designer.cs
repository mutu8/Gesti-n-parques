namespace CapaPresentacion
{
    partial class frmDatosPersonal
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
            this.cboRol = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnAccion = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtApellidos = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cboRol
            // 
            this.cboRol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRol.FormattingEnabled = true;
            this.cboRol.Location = new System.Drawing.Point(18, 186);
            this.cboRol.Name = "cboRol";
            this.cboRol.Size = new System.Drawing.Size(238, 21);
            this.cboRol.TabIndex = 69;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(12, 76);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 13);
            this.label10.TabIndex = 68;
            this.label10.Text = "Nombre";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(15, 92);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(241, 20);
            this.txtNombre.TabIndex = 67;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(15, 170);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 13);
            this.label6.TabIndex = 62;
            this.label6.Text = "Rol";
            // 
            // btnAccion
            // 
            this.btnAccion.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnAccion.ForeColor = System.Drawing.Color.White;
            this.btnAccion.Location = new System.Drawing.Point(18, 225);
            this.btnAccion.Name = "btnAccion";
            this.btnAccion.Size = new System.Drawing.Size(238, 29);
            this.btnAccion.TabIndex = 58;
            this.btnAccion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAccion.UseVisualStyleBackColor = false;
            this.btnAccion.Click += new System.EventHandler(this.btnAccion_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(12, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 48;
            this.label1.Text = "Apellidos";
            // 
            // txtApellidos
            // 
            this.txtApellidos.Location = new System.Drawing.Point(15, 140);
            this.txtApellidos.Name = "txtApellidos";
            this.txtApellidos.Size = new System.Drawing.Size(241, 20);
            this.txtApellidos.TabIndex = 45;
            // 
            // frmDatosPersonal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 300);
            this.Controls.Add(this.cboRol);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnAccion);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtApellidos);
            this.Name = "frmDatosPersonal";
            this.Sizable = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDatosPersonal_FormClosing);
            this.Load += new System.EventHandler(this.frmDatosPersonal_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cboRol;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnAccion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtApellidos;
    }
}