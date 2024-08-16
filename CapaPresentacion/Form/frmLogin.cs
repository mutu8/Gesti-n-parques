using CapaPresentacion;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CapaPresentación.Formularios
{
    public partial class frmLogin : Form
    {
        private int intentosFallidos = 0; // Contador de intentos fallidos

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        public frmLogin()
        {
            InitializeComponent();
            this.btnMaximizar.Visible = false;
            this.KeyPreview = true; // Habilitar la captura de eventos de teclado en el formulario
        }


        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string usuarioIngresado = textBox1.Text;
            string claveIngresada = textBox2.Text;

            // Validar si los campos están vacíos
            if (string.IsNullOrEmpty(usuarioIngresado) || string.IsNullOrEmpty(claveIngresada))
            {
                MessageBox.Show("Por favor, ingrese tanto el usuario como la contraseña.");
                return; // Salir del método sin contar como intento fallido
            }

            // Si los campos no están vacíos, continuar con la validación de credenciales
            if (usuarioIngresado == "admin" && claveIngresada == "EKzJRQmfy@c2r=ac&njrBjY<70hFu")
            {
                this.Hide();
                frmPrincipal frm = new frmPrincipal();
                frm.Show();
                intentosFallidos = 0;
            }
            else
            {
                intentosFallidos++;
                MessageBox.Show("Usuario o contraseña incorrectos. Intento " + intentosFallidos + " de 3.");

                if (intentosFallidos >= 3)
                {
                    MessageBox.Show("Has excedido el número máximo de intentos. El sistema se cerrará.");
                    Application.Exit();
                }
            }
        }

       
    }
}
