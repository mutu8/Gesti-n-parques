namespace CapaPresentacion
{
    partial class frmPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrincipal));
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnMinimizar = new System.Windows.Forms.Button();
            this.BarraControl = new System.Windows.Forms.Panel();
            this.panelCentral = new System.Windows.Forms.Panel();
            this.BarraIzquierda = new CapaPresentacion.ClasesForm.Gradient_SidebarPanel();
            this.panelOpciones = new System.Windows.Forms.Panel();
            this.btnPersonal = new CapaPresentacion.ClasesForm.ucMenu();
            this.btnPuntos = new CapaPresentacion.ClasesForm.ucMenu();
            this.materialDivider1 = new MaterialSkin.Controls.MaterialDivider();
            this.panelBS = new System.Windows.Forms.Panel();
            this.btnHome = new CapaPresentacion.ClasesForm.ucMenu();
            this.panelBI = new System.Windows.Forms.Panel();
            this.panel5.SuspendLayout();
            this.BarraControl.SuspendLayout();
            this.BarraIzquierda.SuspendLayout();
            this.panelOpciones.SuspendLayout();
            this.panelBS.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Transparent;
            this.panel5.Controls.Add(this.btnCerrar);
            this.panel5.Controls.Add(this.btnMinimizar);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(1117, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(84, 41);
            this.panel5.TabIndex = 0;
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnCerrar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCerrar.BackgroundImage")));
            this.btnCerrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Location = new System.Drawing.Point(45, 7);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(28, 28);
            this.btnCerrar.TabIndex = 2;
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnMinimizar
            // 
            this.btnMinimizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.btnMinimizar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMinimizar.BackgroundImage")));
            this.btnMinimizar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMinimizar.FlatAppearance.BorderSize = 0;
            this.btnMinimizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimizar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMinimizar.Location = new System.Drawing.Point(9, 6);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(28, 28);
            this.btnMinimizar.TabIndex = 0;
            this.btnMinimizar.UseVisualStyleBackColor = false;
            this.btnMinimizar.Click += new System.EventHandler(this.btnMinimizar_Click);
            // 
            // BarraControl
            // 
            this.BarraControl.BackColor = System.Drawing.Color.Black;
            this.BarraControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BarraControl.Controls.Add(this.panel5);
            this.BarraControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.BarraControl.Location = new System.Drawing.Point(0, 0);
            this.BarraControl.Name = "BarraControl";
            this.BarraControl.Size = new System.Drawing.Size(1201, 41);
            this.BarraControl.TabIndex = 6;
            this.BarraControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BarraControl_MouseDown);
            this.BarraControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BarraControl_MouseMove);
            this.BarraControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BarraControl_MouseUp);
            // 
            // panelCentral
            // 
            this.panelCentral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCentral.Location = new System.Drawing.Point(95, 41);
            this.panelCentral.Name = "panelCentral";
            this.panelCentral.Size = new System.Drawing.Size(1106, 798);
            this.panelCentral.TabIndex = 16;
            // 
            // BarraIzquierda
            // 
            this.BarraIzquierda.Controls.Add(this.panelOpciones);
            this.BarraIzquierda.Controls.Add(this.panelBS);
            this.BarraIzquierda.Controls.Add(this.panelBI);
            this.BarraIzquierda.Dock = System.Windows.Forms.DockStyle.Left;
            this.BarraIzquierda.gradientBottom = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(153)))), ((int)(((byte)(102)))));
            this.BarraIzquierda.gradientTop = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(94)))), ((int)(((byte)(98)))));
            this.BarraIzquierda.Location = new System.Drawing.Point(0, 41);
            this.BarraIzquierda.Name = "BarraIzquierda";
            this.BarraIzquierda.Size = new System.Drawing.Size(95, 798);
            this.BarraIzquierda.TabIndex = 7;
            // 
            // panelOpciones
            // 
            this.panelOpciones.BackColor = System.Drawing.Color.Transparent;
            this.panelOpciones.Controls.Add(this.btnPersonal);
            this.panelOpciones.Controls.Add(this.materialDivider1);
            this.panelOpciones.Controls.Add(this.btnPuntos);
            this.panelOpciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOpciones.Location = new System.Drawing.Point(0, 200);
            this.panelOpciones.Name = "panelOpciones";
            this.panelOpciones.Size = new System.Drawing.Size(95, 398);
            this.panelOpciones.TabIndex = 10;
            // 
            // btnPersonal
            // 
            this.btnPersonal.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.btnPersonal.BackColor = System.Drawing.Color.Transparent;
            this.btnPersonal.BorderColor = System.Drawing.Color.Transparent;
            this.btnPersonal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPersonal.Icon = global::CapaPresentacion.Properties.Resources.user__12_;
            this.btnPersonal.Location = new System.Drawing.Point(0, 107);
            this.btnPersonal.Menu = "Personal";
            this.btnPersonal.Name = "btnPersonal";
            this.btnPersonal.Size = new System.Drawing.Size(95, 87);
            this.btnPersonal.TabIndex = 2;
            this.btnPersonal.Load += new System.EventHandler(this.btnPersonal_Load);
            // 
            // btnPuntos
            // 
            this.btnPuntos.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.btnPuntos.BackColor = System.Drawing.Color.Transparent;
            this.btnPuntos.BorderColor = System.Drawing.Color.Transparent;
            this.btnPuntos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPuntos.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPuntos.Icon = global::CapaPresentacion.Properties.Resources.mapas_y_banderass;
            this.btnPuntos.Location = new System.Drawing.Point(0, 0);
            this.btnPuntos.Menu = "Puntos";
            this.btnPuntos.Name = "btnPuntos";
            this.btnPuntos.Size = new System.Drawing.Size(95, 90);
            this.btnPuntos.TabIndex = 0;
            this.btnPuntos.Load += new System.EventHandler(this.btnPuntos_Load);
            // 
            // materialDivider1
            // 
            this.materialDivider1.BackColor = System.Drawing.Color.Transparent;
            this.materialDivider1.Depth = 0;
            this.materialDivider1.Dock = System.Windows.Forms.DockStyle.Top;
            this.materialDivider1.Location = new System.Drawing.Point(0, 90);
            this.materialDivider1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider1.Name = "materialDivider1";
            this.materialDivider1.Size = new System.Drawing.Size(95, 17);
            this.materialDivider1.TabIndex = 0;
            this.materialDivider1.Text = "materialDivider1";
            // 
            // panelBS
            // 
            this.panelBS.BackColor = System.Drawing.Color.Transparent;
            this.panelBS.Controls.Add(this.btnHome);
            this.panelBS.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBS.Location = new System.Drawing.Point(0, 0);
            this.panelBS.Name = "panelBS";
            this.panelBS.Size = new System.Drawing.Size(95, 200);
            this.panelBS.TabIndex = 8;
            // 
            // btnHome
            // 
            this.btnHome.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.btnHome.BackColor = System.Drawing.Color.Transparent;
            this.btnHome.BorderColor = System.Drawing.Color.Transparent;
            this.btnHome.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHome.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnHome.Icon = ((System.Drawing.Image)(resources.GetObject("btnHome.Icon")));
            this.btnHome.Location = new System.Drawing.Point(0, 0);
            this.btnHome.Menu = "Home";
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(95, 73);
            this.btnHome.TabIndex = 0;
            // 
            // panelBI
            // 
            this.panelBI.BackColor = System.Drawing.Color.Transparent;
            this.panelBI.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBI.Location = new System.Drawing.Point(0, 598);
            this.panelBI.Name = "panelBI";
            this.panelBI.Size = new System.Drawing.Size(95, 200);
            this.panelBI.TabIndex = 9;
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1201, 839);
            this.Controls.Add(this.panelCentral);
            this.Controls.Add(this.BarraIzquierda);
            this.Controls.Add(this.BarraControl);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmPrincipal";
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Home_Load);
            this.panel5.ResumeLayout(false);
            this.BarraControl.ResumeLayout(false);
            this.BarraIzquierda.ResumeLayout(false);
            this.panelOpciones.ResumeLayout(false);
            this.panelBS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private ClasesForm.Gradient_SidebarPanel BarraIzquierda;
        private ClasesForm.ucMenu btnPuntos;
        private System.Windows.Forms.Panel panelBS;
        private System.Windows.Forms.Panel panelBI;
        private System.Windows.Forms.Panel panelOpciones;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnMinimizar;
        private System.Windows.Forms.Panel BarraControl;
        private ClasesForm.ucMenu btnHome;
        private ClasesForm.ucMenu btnPersonal;
        private MaterialSkin.Controls.MaterialDivider materialDivider1;
        private System.Windows.Forms.Panel panelCentral;
    }
}