namespace CapaPresentacion
{
    partial class frmPersonal
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
            this.panelVisitas = new System.Windows.Forms.Panel();
            this.materialFloatingActionButton1 = new MaterialSkin.Controls.MaterialFloatingActionButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panelVisitas.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelVisitas
            // 
            this.panelVisitas.Controls.Add(this.materialFloatingActionButton1);
            this.panelVisitas.Controls.Add(this.flowLayoutPanel1);
            this.panelVisitas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelVisitas.Location = new System.Drawing.Point(0, 0);
            this.panelVisitas.Name = "panelVisitas";
            this.panelVisitas.Size = new System.Drawing.Size(1255, 770);
            this.panelVisitas.TabIndex = 0;
            this.panelVisitas.Paint += new System.Windows.Forms.PaintEventHandler(this.panelVisitas_Paint);
            // 
            // materialFloatingActionButton1
            // 
            this.materialFloatingActionButton1.AnimateShowHideButton = true;
            this.materialFloatingActionButton1.Depth = 0;
            this.materialFloatingActionButton1.Icon = global::CapaPresentacion.Properties.Resources.plus_pequeno;
            this.materialFloatingActionButton1.Location = new System.Drawing.Point(53, 43);
            this.materialFloatingActionButton1.Mini = true;
            this.materialFloatingActionButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialFloatingActionButton1.Name = "materialFloatingActionButton1";
            this.materialFloatingActionButton1.Size = new System.Drawing.Size(40, 40);
            this.materialFloatingActionButton1.TabIndex = 34;
            this.materialFloatingActionButton1.Text = "materialFloatingActionButton1";
            this.materialFloatingActionButton1.UseVisualStyleBackColor = true;
            this.materialFloatingActionButton1.Click += new System.EventHandler(this.materialFloatingActionButton1_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(53, 89);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1148, 592);
            this.flowLayoutPanel1.TabIndex = 25;
            // 
            // frmPersonal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1255, 770);
            this.Controls.Add(this.panelVisitas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmPersonal";
            this.Text = "frmVisitas";
            this.Load += new System.EventHandler(this.frmPersonal_Load);
            this.panelVisitas.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelVisitas;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private MaterialSkin.Controls.MaterialFloatingActionButton materialFloatingActionButton1;
    }
}