namespace CapaPresentacion
{
    partial class UserControlTarget
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControlTarget));
            this.label1 = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnVisitas = new MaterialSkin.Controls.MaterialFloatingActionButton();
            this.btnEliminar = new MaterialSkin.Controls.MaterialFloatingActionButton();
            this.btnImagen = new MaterialSkin.Controls.MaterialFloatingActionButton();
            this.btnAjustes = new MaterialSkin.Controls.MaterialFloatingActionButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Location = new System.Drawing.Point(154, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "Nombre:";
            // 
            // txtDireccion
            // 
            this.txtDireccion.BackColor = System.Drawing.Color.White;
            this.txtDireccion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDireccion.Enabled = false;
            this.txtDireccion.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDireccion.Location = new System.Drawing.Point(236, 82);
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(230, 19);
            this.txtDireccion.TabIndex = 12;
            this.txtDireccion.TextChanged += new System.EventHandler(this.txtDireccion_TextChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel1.Location = new System.Drawing.Point(236, 98);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(230, 1);
            this.panel1.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label2.Location = new System.Drawing.Point(154, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "Dirección:";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.btnVisitas);
            this.panel3.Controls.Add(this.btnEliminar);
            this.panel3.Controls.Add(this.btnImagen);
            this.panel3.Controls.Add(this.btnAjustes);
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.txtNombre);
            this.panel3.Controls.Add(this.txtDireccion);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(480, 169);
            this.panel3.TabIndex = 14;
            // 
            // btnVisitas
            // 
            this.btnVisitas.Depth = 0;
            this.btnVisitas.Icon = ((System.Drawing.Image)(resources.GetObject("btnVisitas.Icon")));
            this.btnVisitas.Location = new System.Drawing.Point(285, 117);
            this.btnVisitas.Mini = true;
            this.btnVisitas.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnVisitas.Name = "btnVisitas";
            this.btnVisitas.Size = new System.Drawing.Size(40, 40);
            this.btnVisitas.TabIndex = 17;
            this.btnVisitas.Text = "materialFloatingActionButton4";
            this.btnVisitas.UseVisualStyleBackColor = true;
            this.btnVisitas.Click += new System.EventHandler(this.materialFloatingActionButton4_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Depth = 0;
            this.btnEliminar.Icon = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Icon")));
            this.btnEliminar.Location = new System.Drawing.Point(423, 117);
            this.btnEliminar.Mini = true;
            this.btnEliminar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(40, 40);
            this.btnEliminar.TabIndex = 16;
            this.btnEliminar.Text = "materialFloatingActionButton3";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.materialFloatingActionButton3_Click);
            // 
            // btnImagen
            // 
            this.btnImagen.Depth = 0;
            this.btnImagen.Icon = ((System.Drawing.Image)(resources.GetObject("btnImagen.Icon")));
            this.btnImagen.Location = new System.Drawing.Point(331, 117);
            this.btnImagen.Mini = true;
            this.btnImagen.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnImagen.Name = "btnImagen";
            this.btnImagen.Size = new System.Drawing.Size(40, 40);
            this.btnImagen.TabIndex = 15;
            this.btnImagen.Text = "materialFloatingActionButton2";
            this.btnImagen.UseVisualStyleBackColor = true;
            this.btnImagen.Click += new System.EventHandler(this.materialFloatingActionButton2_Click);
            // 
            // btnAjustes
            // 
            this.btnAjustes.Depth = 0;
            this.btnAjustes.Icon = ((System.Drawing.Image)(resources.GetObject("btnAjustes.Icon")));
            this.btnAjustes.Location = new System.Drawing.Point(377, 117);
            this.btnAjustes.Mini = true;
            this.btnAjustes.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnAjustes.Name = "btnAjustes";
            this.btnAjustes.Size = new System.Drawing.Size(40, 40);
            this.btnAjustes.TabIndex = 14;
            this.btnAjustes.Text = "materialFloatingActionButton1";
            this.btnAjustes.UseVisualStyleBackColor = true;
            this.btnAjustes.Click += new System.EventHandler(this.materialFloatingActionButton1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Image = global::CapaPresentacion.Properties.Resources.park;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(134, 145);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel2.Location = new System.Drawing.Point(236, 59);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(230, 1);
            this.panel2.TabIndex = 10;
            // 
            // txtNombre
            // 
            this.txtNombre.BackColor = System.Drawing.Color.White;
            this.txtNombre.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNombre.Enabled = false;
            this.txtNombre.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre.Location = new System.Drawing.Point(236, 44);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(230, 19);
            this.txtNombre.TabIndex = 9;
            this.txtNombre.TextChanged += new System.EventHandler(this.txtNombre_TextChanged);
            // 
            // UserControlTarget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Name = "UserControlTarget";
            this.Size = new System.Drawing.Size(480, 169);
            this.Load += new System.EventHandler(this.UserControlTarget_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private MaterialSkin.Controls.MaterialFloatingActionButton btnAjustes;
        private MaterialSkin.Controls.MaterialFloatingActionButton btnImagen;
        private MaterialSkin.Controls.MaterialFloatingActionButton btnEliminar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtNombre;
        private MaterialSkin.Controls.MaterialFloatingActionButton btnVisitas;
    }
}
