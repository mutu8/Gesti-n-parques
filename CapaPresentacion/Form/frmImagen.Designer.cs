namespace CapaPresentacion
{
    partial class frmImagen
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
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnFoto = new System.Windows.Forms.Button();
            this.ImgCli = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnGrabar = new System.Windows.Forms.Button();
            this.btnEliminarNube = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ImgCli)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(82, 139);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(230, 26);
            this.btnEliminar.TabIndex = 46;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnFoto
            // 
            this.btnFoto.Location = new System.Drawing.Point(82, 107);
            this.btnFoto.Name = "btnFoto";
            this.btnFoto.Size = new System.Drawing.Size(230, 26);
            this.btnFoto.TabIndex = 45;
            this.btnFoto.Text = "Subir";
            this.btnFoto.UseVisualStyleBackColor = true;
            this.btnFoto.Click += new System.EventHandler(this.btnFoto_Click);
            // 
            // ImgCli
            // 
            this.ImgCli.BackColor = System.Drawing.Color.White;
            this.ImgCli.Location = new System.Drawing.Point(82, 171);
            this.ImgCli.Name = "ImgCli";
            this.ImgCli.Size = new System.Drawing.Size(230, 181);
            this.ImgCli.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ImgCli.TabIndex = 44;
            this.ImgCli.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(21, 77);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(335, 13);
            this.groupBox1.TabIndex = 43;
            this.groupBox1.TabStop = false;
            // 
            // btnGrabar
            // 
            this.btnGrabar.Location = new System.Drawing.Point(82, 358);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(111, 27);
            this.btnGrabar.TabIndex = 42;
            this.btnGrabar.Text = "Grabar";
            this.btnGrabar.UseVisualStyleBackColor = true;
            this.btnGrabar.Click += new System.EventHandler(this.btnGrabar_Click);
            // 
            // btnEliminarNube
            // 
            this.btnEliminarNube.Location = new System.Drawing.Point(201, 358);
            this.btnEliminarNube.Name = "btnEliminarNube";
            this.btnEliminarNube.Size = new System.Drawing.Size(111, 27);
            this.btnEliminarNube.TabIndex = 47;
            this.btnEliminarNube.Text = "Eliminar (cloud)";
            this.btnEliminarNube.UseVisualStyleBackColor = true;
            this.btnEliminarNube.Click += new System.EventHandler(this.btnEliminarNube_Click);
            // 
            // frmImagen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 406);
            this.Controls.Add(this.btnEliminarNube);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnFoto);
            this.Controls.Add(this.ImgCli);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnGrabar);
            this.Name = "frmImagen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmImagen_FormClosed);
            this.Load += new System.EventHandler(this.frmImagen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ImgCli)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnFoto;
        private System.Windows.Forms.PictureBox ImgCli;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnGrabar;
        private System.Windows.Forms.Button btnEliminarNube;
    }
}