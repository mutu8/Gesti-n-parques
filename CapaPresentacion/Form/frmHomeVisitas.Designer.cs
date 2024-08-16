namespace CapaPresentacion
{
    partial class frmHomeVisitas
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelCarta = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAddNota = new MaterialSkin.Controls.MaterialFloatingActionButton();
            this.btnDelete = new MaterialSkin.Controls.MaterialFloatingActionButton();
            this.materialFloatingActionButton2 = new MaterialSkin.Controls.MaterialFloatingActionButton();
            this.label3 = new System.Windows.Forms.Label();
            this.btnUpdate = new MaterialSkin.Controls.MaterialFloatingActionButton();
            this.materialFloatingActionButton1 = new MaterialSkin.Controls.MaterialFloatingActionButton();
            this.btnImprimier = new MaterialSkin.Controls.MaterialFloatingActionButton();
            this.label1 = new System.Windows.Forms.Label();
            this.lblIdEmpleado = new System.Windows.Forms.Label();
            this.lblIdLocalidad = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panelCarta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCarta
            // 
            this.panelCarta.BackColor = System.Drawing.Color.Transparent;
            this.panelCarta.Controls.Add(this.dataGridView1);
            this.panelCarta.Controls.Add(this.panel5);
            this.panelCarta.Controls.Add(this.panel1);
            this.panelCarta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCarta.Location = new System.Drawing.Point(3, 64);
            this.panelCarta.Name = "panelCarta";
            this.panelCarta.Size = new System.Drawing.Size(1249, 703);
            this.panelCarta.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 118);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(1249, 467);
            this.dataGridView1.TabIndex = 56;
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Transparent;
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 585);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1249, 118);
            this.panel5.TabIndex = 60;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.btnAddNota);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.materialFloatingActionButton2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnUpdate);
            this.panel1.Controls.Add(this.materialFloatingActionButton1);
            this.panel1.Controls.Add(this.btnImprimier);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblIdEmpleado);
            this.panel1.Controls.Add(this.lblIdLocalidad);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1249, 118);
            this.panel1.TabIndex = 55;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // btnAddNota
            // 
            this.btnAddNota.AnimateShowHideButton = true;
            this.btnAddNota.Depth = 0;
            this.btnAddNota.ForeColor = System.Drawing.Color.Transparent;
            this.btnAddNota.Icon = global::CapaPresentacion.Properties.Resources.plus__4_;
            this.btnAddNota.Location = new System.Drawing.Point(119, 47);
            this.btnAddNota.Mini = true;
            this.btnAddNota.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnAddNota.Name = "btnAddNota";
            this.btnAddNota.Size = new System.Drawing.Size(40, 40);
            this.btnAddNota.TabIndex = 57;
            this.btnAddNota.Text = "materialFloatingActionButton3";
            this.btnAddNota.UseVisualStyleBackColor = true;
            this.btnAddNota.Click += new System.EventHandler(this.btnAddNota_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AnimateShowHideButton = true;
            this.btnDelete.Depth = 0;
            this.btnDelete.ForeColor = System.Drawing.Color.Transparent;
            this.btnDelete.Icon = global::CapaPresentacion.Properties.Resources.delete;
            this.btnDelete.Location = new System.Drawing.Point(73, 47);
            this.btnDelete.Mini = true;
            this.btnDelete.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(40, 40);
            this.btnDelete.TabIndex = 56;
            this.btnDelete.Text = "materialFloatingActionButton3";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // materialFloatingActionButton2
            // 
            this.materialFloatingActionButton2.AnimateShowHideButton = true;
            this.materialFloatingActionButton2.BackColor = System.Drawing.Color.Transparent;
            this.materialFloatingActionButton2.Depth = 0;
            this.materialFloatingActionButton2.Icon = global::CapaPresentacion.Properties.Resources.file__1_;
            this.materialFloatingActionButton2.Location = new System.Drawing.Point(257, 47);
            this.materialFloatingActionButton2.Mini = true;
            this.materialFloatingActionButton2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialFloatingActionButton2.Name = "materialFloatingActionButton2";
            this.materialFloatingActionButton2.Size = new System.Drawing.Size(40, 40);
            this.materialFloatingActionButton2.TabIndex = 55;
            this.materialFloatingActionButton2.UseVisualStyleBackColor = false;
            this.materialFloatingActionButton2.Click += new System.EventHandler(this.materialFloatingActionButton2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(506, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 36;
            this.label3.Text = "ID Empleado:";
            this.label3.Visible = false;
            // 
            // btnUpdate
            // 
            this.btnUpdate.AnimateShowHideButton = true;
            this.btnUpdate.Depth = 0;
            this.btnUpdate.Icon = global::CapaPresentacion.Properties.Resources.check;
            this.btnUpdate.Location = new System.Drawing.Point(165, 47);
            this.btnUpdate.Mini = true;
            this.btnUpdate.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(40, 40);
            this.btnUpdate.TabIndex = 54;
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // materialFloatingActionButton1
            // 
            this.materialFloatingActionButton1.AnimateShowHideButton = true;
            this.materialFloatingActionButton1.Depth = 0;
            this.materialFloatingActionButton1.ForeColor = System.Drawing.Color.Transparent;
            this.materialFloatingActionButton1.Icon = global::CapaPresentacion.Properties.Resources.magic_wand;
            this.materialFloatingActionButton1.Location = new System.Drawing.Point(27, 47);
            this.materialFloatingActionButton1.Mini = true;
            this.materialFloatingActionButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialFloatingActionButton1.Name = "materialFloatingActionButton1";
            this.materialFloatingActionButton1.Size = new System.Drawing.Size(40, 40);
            this.materialFloatingActionButton1.TabIndex = 33;
            this.materialFloatingActionButton1.Text = "materialFloatingActionButton1";
            this.materialFloatingActionButton1.UseVisualStyleBackColor = true;
            this.materialFloatingActionButton1.Click += new System.EventHandler(this.materialFloatingActionButton1_Click);
            // 
            // btnImprimier
            // 
            this.btnImprimier.AnimateShowHideButton = true;
            this.btnImprimier.BackColor = System.Drawing.Color.Transparent;
            this.btnImprimier.Depth = 0;
            this.btnImprimier.Icon = global::CapaPresentacion.Properties.Resources.printer__3_;
            this.btnImprimier.Location = new System.Drawing.Point(211, 47);
            this.btnImprimier.Mini = true;
            this.btnImprimier.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnImprimier.Name = "btnImprimier";
            this.btnImprimier.Size = new System.Drawing.Size(40, 40);
            this.btnImprimier.TabIndex = 53;
            this.btnImprimier.UseVisualStyleBackColor = false;
            this.btnImprimier.Click += new System.EventHandler(this.btnImprimier_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(323, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "ID Localidad:";
            this.label1.Visible = false;
            // 
            // lblIdEmpleado
            // 
            this.lblIdEmpleado.AutoSize = true;
            this.lblIdEmpleado.Location = new System.Drawing.Point(599, 61);
            this.lblIdEmpleado.Name = "lblIdEmpleado";
            this.lblIdEmpleado.Size = new System.Drawing.Size(37, 13);
            this.lblIdEmpleado.TabIndex = 37;
            this.lblIdEmpleado.Text = "_____";
            this.lblIdEmpleado.Visible = false;
            // 
            // lblIdLocalidad
            // 
            this.lblIdLocalidad.AutoSize = true;
            this.lblIdLocalidad.Location = new System.Drawing.Point(416, 61);
            this.lblIdLocalidad.Name = "lblIdLocalidad";
            this.lblIdLocalidad.Size = new System.Drawing.Size(37, 13);
            this.lblIdLocalidad.TabIndex = 35;
            this.lblIdLocalidad.Text = "_____";
            this.lblIdLocalidad.Visible = false;
            // 
            // frmHomeVisitas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1255, 770);
            this.Controls.Add(this.panelCarta);
            this.Name = "frmHomeVisitas";
            this.Sizable = false;
            this.Load += new System.EventHandler(this.frmHomeVisitas_Load);
            this.panelCarta.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelCarta;
        private MaterialSkin.Controls.MaterialFloatingActionButton materialFloatingActionButton1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblIdLocalidad;
        private System.Windows.Forms.Label lblIdEmpleado;
        private System.Windows.Forms.Label label3;
        private MaterialSkin.Controls.MaterialFloatingActionButton btnImprimier;
        private MaterialSkin.Controls.MaterialFloatingActionButton btnUpdate;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel5;
        private MaterialSkin.Controls.MaterialFloatingActionButton materialFloatingActionButton2;
        private MaterialSkin.Controls.MaterialFloatingActionButton btnDelete;
        private MaterialSkin.Controls.MaterialFloatingActionButton btnAddNota;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}