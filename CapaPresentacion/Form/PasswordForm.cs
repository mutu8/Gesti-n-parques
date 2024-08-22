using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class PasswordForm : MaterialForm
    {
        private const string ClaveCorrecta = "1";

        public PasswordForm()
        {
            InitializeComponent();
            MaximizeBox = false;
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            string contraseñaIngresada = materialMaskedTextBox1.Text.Trim();

            if (contraseñaIngresada == ClaveCorrecta)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Contraseña incorrecta. Inténtalo de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                materialMaskedTextBox1.Text = string.Empty;
                materialMaskedTextBox1.Focus();
            }
        }
    }
}
