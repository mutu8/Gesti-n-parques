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
            this.materialTextBox1 = new MaterialSkin.Controls.MaterialTextBox();
            this.materialFloatingActionButton1 = new MaterialSkin.Controls.MaterialFloatingActionButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panelVisitas.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelVisitas
            // 
            this.panelVisitas.Controls.Add(this.materialTextBox1);
            this.panelVisitas.Controls.Add(this.materialFloatingActionButton1);
            this.panelVisitas.Controls.Add(this.flowLayoutPanel1);
            this.panelVisitas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelVisitas.Location = new System.Drawing.Point(0, 0);
            this.panelVisitas.Name = "panelVisitas";
            this.panelVisitas.Size = new System.Drawing.Size(1255, 770);
            this.panelVisitas.TabIndex = 0;
            this.panelVisitas.Paint += new System.Windows.Forms.PaintEventHandler(this.panelVisitas_Paint);
            // 
            // materialTextBox1
            // 
            this.materialTextBox1.AnimateReadOnly = false;
            this.materialTextBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.materialTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.materialTextBox1.Depth = 0;
            this.materialTextBox1.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialTextBox1.LeadingIcon = null;
            this.materialTextBox1.Location = new System.Drawing.Point(109, 42);
            this.materialTextBox1.MaxLength = 50;
            this.materialTextBox1.MouseState = MaterialSkin.MouseState.OUT;
            this.materialTextBox1.Multiline = false;
            this.materialTextBox1.Name = "materialTextBox1";
            this.materialTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.materialTextBox1.Size = new System.Drawing.Size(146, 36);
            this.materialTextBox1.TabIndex = 35;
            this.materialTextBox1.Text = "";
            this.materialTextBox1.TrailingIcon = null;
            this.materialTextBox1.UseTallSize = false;
            this.materialTextBox1.TextChanged += new System.EventHandler(this.materialTextBox1_TextChanged);
            // 
            // materialFloatingActionButton1
            // 
            this.materialFloatingActionButton1.AnimateShowHideButton = true;
            this.materialFloatingActionButton1.Depth = 0;
            this.materialFloatingActionButton1.Icon = global::CapaPresentacion.Properties.Resources.plus__4_;
            this.materialFloatingActionButton1.Location = new System.Drawing.Point(53, 38);
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
        private MaterialSkin.Controls.MaterialTextBox materialTextBox1;
    }
}