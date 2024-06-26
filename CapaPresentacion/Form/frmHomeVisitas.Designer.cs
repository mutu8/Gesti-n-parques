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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panelCarta = new System.Windows.Forms.Panel();
            this.btnUpdate = new MaterialSkin.Controls.MaterialFloatingActionButton();
            this.btnImprimier = new MaterialSkin.Controls.MaterialFloatingActionButton();
            this.lblIdEmpleado = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblIdLocalidad = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.materialFloatingActionButton1 = new MaterialSkin.Controls.MaterialFloatingActionButton();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panelCarta.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Controls.Add(this.dataGridView1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(61, 123);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1148, 592);
            this.flowLayoutPanel1.TabIndex = 24;
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
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(1145, 589);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError_1);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // panelCarta
            // 
            this.panelCarta.BackColor = System.Drawing.Color.Transparent;
            this.panelCarta.Controls.Add(this.btnUpdate);
            this.panelCarta.Controls.Add(this.btnImprimier);
            this.panelCarta.Controls.Add(this.lblIdEmpleado);
            this.panelCarta.Controls.Add(this.label3);
            this.panelCarta.Controls.Add(this.lblIdLocalidad);
            this.panelCarta.Controls.Add(this.label1);
            this.panelCarta.Controls.Add(this.materialFloatingActionButton1);
            this.panelCarta.Controls.Add(this.flowLayoutPanel1);
            this.panelCarta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCarta.Location = new System.Drawing.Point(0, 0);
            this.panelCarta.Name = "panelCarta";
            this.panelCarta.Size = new System.Drawing.Size(1255, 770);
            this.panelCarta.TabIndex = 3;
            // 
            // btnUpdate
            // 
            this.btnUpdate.AnimateShowHideButton = true;
            this.btnUpdate.Depth = 0;
            this.btnUpdate.Icon = global::CapaPresentacion.Properties.Resources.done;
            this.btnUpdate.Location = new System.Drawing.Point(153, 77);
            this.btnUpdate.Mini = true;
            this.btnUpdate.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(40, 40);
            this.btnUpdate.TabIndex = 54;
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnImprimier
            // 
            this.btnImprimier.AnimateShowHideButton = true;
            this.btnImprimier.BackColor = System.Drawing.Color.Transparent;
            this.btnImprimier.Depth = 0;
            this.btnImprimier.Icon = global::CapaPresentacion.Properties.Resources.printer__3_;
            this.btnImprimier.Location = new System.Drawing.Point(107, 77);
            this.btnImprimier.Mini = true;
            this.btnImprimier.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnImprimier.Name = "btnImprimier";
            this.btnImprimier.Size = new System.Drawing.Size(40, 40);
            this.btnImprimier.TabIndex = 53;
            this.btnImprimier.UseVisualStyleBackColor = false;
            this.btnImprimier.Click += new System.EventHandler(this.btnImprimier_Click);
            // 
            // lblIdEmpleado
            // 
            this.lblIdEmpleado.AutoSize = true;
            this.lblIdEmpleado.Location = new System.Drawing.Point(497, 91);
            this.lblIdEmpleado.Name = "lblIdEmpleado";
            this.lblIdEmpleado.Size = new System.Drawing.Size(37, 13);
            this.lblIdEmpleado.TabIndex = 37;
            this.lblIdEmpleado.Text = "_____";
            this.lblIdEmpleado.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(404, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 36;
            this.label3.Text = "ID Empleado:";
            this.label3.Visible = false;
            // 
            // lblIdLocalidad
            // 
            this.lblIdLocalidad.AutoSize = true;
            this.lblIdLocalidad.Location = new System.Drawing.Point(314, 91);
            this.lblIdLocalidad.Name = "lblIdLocalidad";
            this.lblIdLocalidad.Size = new System.Drawing.Size(37, 13);
            this.lblIdLocalidad.TabIndex = 35;
            this.lblIdLocalidad.Text = "_____";
            this.lblIdLocalidad.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(221, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "ID Localidad:";
            this.label1.Visible = false;
            // 
            // materialFloatingActionButton1
            // 
            this.materialFloatingActionButton1.AnimateShowHideButton = true;
            this.materialFloatingActionButton1.Depth = 0;
            this.materialFloatingActionButton1.ForeColor = System.Drawing.Color.Transparent;
            this.materialFloatingActionButton1.Icon = global::CapaPresentacion.Properties.Resources.plus__4_;
            this.materialFloatingActionButton1.Location = new System.Drawing.Point(61, 77);
            this.materialFloatingActionButton1.Mini = true;
            this.materialFloatingActionButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialFloatingActionButton1.Name = "materialFloatingActionButton1";
            this.materialFloatingActionButton1.Size = new System.Drawing.Size(40, 40);
            this.materialFloatingActionButton1.TabIndex = 33;
            this.materialFloatingActionButton1.Text = "materialFloatingActionButton1";
            this.materialFloatingActionButton1.UseVisualStyleBackColor = true;
            this.materialFloatingActionButton1.Click += new System.EventHandler(this.materialFloatingActionButton1_Click);
            // 
            // frmHomeVisitas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1255, 770);
            this.Controls.Add(this.panelCarta);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmHomeVisitas";
            this.Load += new System.EventHandler(this.frmHomeVisitas_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panelCarta.ResumeLayout(false);
            this.panelCarta.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panelCarta;
        private MaterialSkin.Controls.MaterialFloatingActionButton materialFloatingActionButton1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblIdLocalidad;
        private System.Windows.Forms.Label lblIdEmpleado;
        private System.Windows.Forms.Label label3;
        private MaterialSkin.Controls.MaterialFloatingActionButton btnImprimier;
        private MaterialSkin.Controls.MaterialFloatingActionButton btnUpdate;
    }
}