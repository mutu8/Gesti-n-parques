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
            this.label2 = new System.Windows.Forms.Label();
            this.txtDNI = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCorreo = new System.Windows.Forms.TextBox();
            this.ImgCli = new System.Windows.Forms.PictureBox();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnAbrir = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.materialCheckbox1 = new MaterialSkin.Controls.MaterialCheckbox();
            ((System.ComponentModel.ISupportInitialize)(this.ImgCli)).BeginInit();
            this.SuspendLayout();
            // 
            // cboRol
            // 
            this.cboRol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRol.FormattingEnabled = true;
            this.cboRol.Location = new System.Drawing.Point(134, 351);
            this.cboRol.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboRol.Name = "cboRol";
            this.cboRol.Size = new System.Drawing.Size(325, 24);
            this.cboRol.TabIndex = 69;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(126, 93);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 16);
            this.label10.TabIndex = 68;
            this.label10.Text = "Nombre";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(130, 112);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(329, 22);
            this.txtNombre.TabIndex = 67;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(130, 331);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 16);
            this.label6.TabIndex = 62;
            this.label6.Text = "Rol";
            // 
            // btnAccion
            // 
            this.btnAccion.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnAccion.ForeColor = System.Drawing.Color.White;
            this.btnAccion.Location = new System.Drawing.Point(127, 710);
            this.btnAccion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAccion.Name = "btnAccion";
            this.btnAccion.Size = new System.Drawing.Size(327, 36);
            this.btnAccion.TabIndex = 58;
            this.btnAccion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAccion.UseVisualStyleBackColor = false;
            this.btnAccion.Click += new System.EventHandler(this.btnAccion_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(126, 152);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 48;
            this.label1.Text = "Apellidos";
            // 
            // txtApellidos
            // 
            this.txtApellidos.Location = new System.Drawing.Point(130, 171);
            this.txtApellidos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtApellidos.Name = "txtApellidos";
            this.txtApellidos.Size = new System.Drawing.Size(329, 22);
            this.txtApellidos.TabIndex = 45;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(126, 210);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 16);
            this.label2.TabIndex = 71;
            this.label2.Text = "DNI";
            // 
            // txtDNI
            // 
            this.txtDNI.Location = new System.Drawing.Point(130, 229);
            this.txtDNI.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDNI.Name = "txtDNI";
            this.txtDNI.Size = new System.Drawing.Size(329, 22);
            this.txtDNI.TabIndex = 70;
            this.txtDNI.TextChanged += new System.EventHandler(this.txtDNI_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(126, 266);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 16);
            this.label3.TabIndex = 73;
            this.label3.Text = "Correo";
            // 
            // txtCorreo
            // 
            this.txtCorreo.Location = new System.Drawing.Point(130, 286);
            this.txtCorreo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCorreo.Name = "txtCorreo";
            this.txtCorreo.Size = new System.Drawing.Size(329, 22);
            this.txtCorreo.TabIndex = 72;
            this.txtCorreo.TextChanged += new System.EventHandler(this.txtCorreo_TextChanged);
            // 
            // ImgCli
            // 
            this.ImgCli.BackColor = System.Drawing.Color.White;
            this.ImgCli.Location = new System.Drawing.Point(132, 534);
            this.ImgCli.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ImgCli.Name = "ImgCli";
            this.ImgCli.Size = new System.Drawing.Size(327, 76);
            this.ImgCli.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ImgCli.TabIndex = 74;
            this.ImgCli.TabStop = false;
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(357, 676);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(96, 27);
            this.btnEliminar.TabIndex = 78;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnAbrir
            // 
            this.btnAbrir.Location = new System.Drawing.Point(127, 676);
            this.btnAbrir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAbrir.Name = "btnAbrir";
            this.btnAbrir.Size = new System.Drawing.Size(96, 27);
            this.btnAbrir.TabIndex = 77;
            this.btnAbrir.Text = "Abrir";
            this.btnAbrir.UseVisualStyleBackColor = true;
            this.btnAbrir.Click += new System.EventHandler(this.btnAbrir_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(132, 505);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(327, 22);
            this.groupBox1.TabIndex = 76;
            this.groupBox1.TabStop = false;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(243, 676);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(96, 28);
            this.btnGuardar.TabIndex = 75;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(129, 392);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(132, 16);
            this.label4.TabIndex = 79;
            this.label4.Text = "Fecha de nacimiento";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(133, 424);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(321, 22);
            this.dateTimePicker1.TabIndex = 80;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(131, 462);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 16);
            this.label5.TabIndex = 81;
            // 
            // materialCheckbox1
            // 
            this.materialCheckbox1.AutoSize = true;
            this.materialCheckbox1.Depth = 0;
            this.materialCheckbox1.Location = new System.Drawing.Point(129, 466);
            this.materialCheckbox1.Margin = new System.Windows.Forms.Padding(0);
            this.materialCheckbox1.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialCheckbox1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCheckbox1.Name = "materialCheckbox1";
            this.materialCheckbox1.ReadOnly = false;
            this.materialCheckbox1.Ripple = true;
            this.materialCheckbox1.Size = new System.Drawing.Size(202, 37);
            this.materialCheckbox1.TabIndex = 82;
            this.materialCheckbox1.Text = "Es personal de limpieza";
            this.materialCheckbox1.UseVisualStyleBackColor = true;
            // 
            // frmDatosPersonal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 1030);
            this.Controls.Add(this.materialCheckbox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnAbrir);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.ImgCli);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCorreo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDNI);
            this.Controls.Add(this.cboRol);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnAccion);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtApellidos);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmDatosPersonal";
            this.Padding = new System.Windows.Forms.Padding(4, 79, 4, 4);
            this.Sizable = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDatosPersonal_FormClosing);
            this.Load += new System.EventHandler(this.frmDatosPersonal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ImgCli)).EndInit();
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDNI;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCorreo;
        private System.Windows.Forms.PictureBox ImgCli;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnAbrir;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label5;
        private MaterialSkin.Controls.MaterialCheckbox materialCheckbox1;
    }
}