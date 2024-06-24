namespace CapaPresentacion
{
    partial class frmLocalidades
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
            this.panelCarta = new System.Windows.Forms.Panel();
            this.materialMaskedTextBox1 = new MaterialSkin.Controls.MaterialMaskedTextBox();
            this.materialFloatingActionButton1 = new MaterialSkin.Controls.MaterialFloatingActionButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panelCarta.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCarta
            // 
            this.panelCarta.BackColor = System.Drawing.Color.Transparent;
            this.panelCarta.Controls.Add(this.materialMaskedTextBox1);
            this.panelCarta.Controls.Add(this.materialFloatingActionButton1);
            this.panelCarta.Controls.Add(this.flowLayoutPanel1);
            this.panelCarta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCarta.Location = new System.Drawing.Point(0, 0);
            this.panelCarta.Name = "panelCarta";
            this.panelCarta.Size = new System.Drawing.Size(1255, 770);
            this.panelCarta.TabIndex = 2;
            // 
            // materialMaskedTextBox1
            // 
            this.materialMaskedTextBox1.AllowPromptAsInput = true;
            this.materialMaskedTextBox1.AnimateReadOnly = false;
            this.materialMaskedTextBox1.AsciiOnly = false;
            this.materialMaskedTextBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.materialMaskedTextBox1.BeepOnError = false;
            this.materialMaskedTextBox1.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals;
            this.materialMaskedTextBox1.Depth = 0;
            this.materialMaskedTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialMaskedTextBox1.HidePromptOnLeave = false;
            this.materialMaskedTextBox1.HideSelection = true;
            this.materialMaskedTextBox1.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Default;
            this.materialMaskedTextBox1.LeadingIcon = null;
            this.materialMaskedTextBox1.Location = new System.Drawing.Point(128, 77);
            this.materialMaskedTextBox1.Mask = "";
            this.materialMaskedTextBox1.MaxLength = 32767;
            this.materialMaskedTextBox1.MouseState = MaterialSkin.MouseState.OUT;
            this.materialMaskedTextBox1.Name = "materialMaskedTextBox1";
            this.materialMaskedTextBox1.PasswordChar = '\0';
            this.materialMaskedTextBox1.PrefixSuffixText = null;
            this.materialMaskedTextBox1.PromptChar = '_';
            this.materialMaskedTextBox1.ReadOnly = false;
            this.materialMaskedTextBox1.RejectInputOnFirstFailure = false;
            this.materialMaskedTextBox1.ResetOnPrompt = true;
            this.materialMaskedTextBox1.ResetOnSpace = true;
            this.materialMaskedTextBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.materialMaskedTextBox1.SelectedText = "";
            this.materialMaskedTextBox1.SelectionLength = 0;
            this.materialMaskedTextBox1.SelectionStart = 0;
            this.materialMaskedTextBox1.ShortcutsEnabled = true;
            this.materialMaskedTextBox1.Size = new System.Drawing.Size(250, 36);
            this.materialMaskedTextBox1.SkipLiterals = true;
            this.materialMaskedTextBox1.TabIndex = 35;
            this.materialMaskedTextBox1.TabStop = false;
            this.materialMaskedTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.materialMaskedTextBox1.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals;
            this.materialMaskedTextBox1.TrailingIcon = null;
            this.materialMaskedTextBox1.UseSystemPasswordChar = false;
            this.materialMaskedTextBox1.UseTallSize = false;
            this.materialMaskedTextBox1.ValidatingType = null;
            this.materialMaskedTextBox1.TextChanged += new System.EventHandler(this.materialMaskedTextBox1_TextChanged);
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
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(61, 123);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1148, 592);
            this.flowLayoutPanel1.TabIndex = 24;
            // 
            // frmLocalidades
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1255, 770);
            this.Controls.Add(this.panelCarta);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmLocalidades";
            this.Text = "cartaForm";
            this.Load += new System.EventHandler(this.frmLocalidades_Load);
            this.panelCarta.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelCarta;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private MaterialSkin.Controls.MaterialFloatingActionButton materialFloatingActionButton1;
        private MaterialSkin.Controls.MaterialMaskedTextBox materialMaskedTextBox1;
    }
}