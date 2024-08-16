namespace CapaPresentacion
{
    partial class FormPersonalLimpieza
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.lblIdEmpleado = new System.Windows.Forms.Label();
            this.panelCarta = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnDelete = new MaterialSkin.Controls.MaterialFloatingActionButton();
            this.materialFloatingActionButton2 = new MaterialSkin.Controls.MaterialFloatingActionButton();
            this.btnUpdate = new MaterialSkin.Controls.MaterialFloatingActionButton();
            this.btnImprimier = new MaterialSkin.Controls.MaterialFloatingActionButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panelCarta.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 118);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(800, 214);
            this.dataGridView1.TabIndex = 56;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.materialFloatingActionButton2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnUpdate);
            this.panel1.Controls.Add(this.btnImprimier);
            this.panel1.Controls.Add(this.lblIdEmpleado);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 118);
            this.panel1.TabIndex = 55;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(238, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 36;
            this.label3.Text = "ID Empleado:";
            this.label3.Visible = false;
            // 
            // lblIdEmpleado
            // 
            this.lblIdEmpleado.AutoSize = true;
            this.lblIdEmpleado.Location = new System.Drawing.Point(331, 66);
            this.lblIdEmpleado.Name = "lblIdEmpleado";
            this.lblIdEmpleado.Size = new System.Drawing.Size(37, 13);
            this.lblIdEmpleado.TabIndex = 37;
            this.lblIdEmpleado.Text = "_____";
            this.lblIdEmpleado.Visible = false;
            // 
            // panelCarta
            // 
            this.panelCarta.BackColor = System.Drawing.Color.Transparent;
            this.panelCarta.Controls.Add(this.dataGridView1);
            this.panelCarta.Controls.Add(this.panel5);
            this.panelCarta.Controls.Add(this.panel1);
            this.panelCarta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCarta.Location = new System.Drawing.Point(0, 0);
            this.panelCarta.Name = "panelCarta";
            this.panelCarta.Size = new System.Drawing.Size(800, 450);
            this.panelCarta.TabIndex = 4;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Transparent;
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 332);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(800, 118);
            this.panel5.TabIndex = 60;
            // 
            // btnDelete
            // 
            this.btnDelete.AnimateShowHideButton = true;
            this.btnDelete.Depth = 0;
            this.btnDelete.ForeColor = System.Drawing.Color.Transparent;
            this.btnDelete.Icon = global::CapaPresentacion.Properties.Resources.delete;
            this.btnDelete.Location = new System.Drawing.Point(73, 52);
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
            this.materialFloatingActionButton2.Location = new System.Drawing.Point(165, 52);
            this.materialFloatingActionButton2.Mini = true;
            this.materialFloatingActionButton2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialFloatingActionButton2.Name = "materialFloatingActionButton2";
            this.materialFloatingActionButton2.Size = new System.Drawing.Size(40, 40);
            this.materialFloatingActionButton2.TabIndex = 55;
            this.materialFloatingActionButton2.UseVisualStyleBackColor = false;
            this.materialFloatingActionButton2.Click += new System.EventHandler(this.materialFloatingActionButton2_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.AnimateShowHideButton = true;
            this.btnUpdate.Depth = 0;
            this.btnUpdate.Icon = global::CapaPresentacion.Properties.Resources.check;
            this.btnUpdate.Location = new System.Drawing.Point(27, 52);
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
            this.btnImprimier.Location = new System.Drawing.Point(119, 52);
            this.btnImprimier.Mini = true;
            this.btnImprimier.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnImprimier.Name = "btnImprimier";
            this.btnImprimier.Size = new System.Drawing.Size(40, 40);
            this.btnImprimier.TabIndex = 53;
            this.btnImprimier.UseVisualStyleBackColor = false;
            this.btnImprimier.Click += new System.EventHandler(this.btnImprimier_Click);
            // 
            // FormPersonalLimpieza
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelCarta);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormPersonalLimpieza";
            this.Text = "FormPersonalLimpieza";
            this.Load += new System.EventHandler(this.FormPersonalLimpieza_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelCarta.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private MaterialSkin.Controls.MaterialFloatingActionButton btnDelete;
        private MaterialSkin.Controls.MaterialFloatingActionButton materialFloatingActionButton2;
        private System.Windows.Forms.Label label3;
        private MaterialSkin.Controls.MaterialFloatingActionButton btnUpdate;
        private MaterialSkin.Controls.MaterialFloatingActionButton btnImprimier;
        private System.Windows.Forms.Label lblIdEmpleado;
        private System.Windows.Forms.Panel panelCarta;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}