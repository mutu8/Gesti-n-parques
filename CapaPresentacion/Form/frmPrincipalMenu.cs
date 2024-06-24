using CapaLogica;
using CapaPresentacion.ClasesForm;
using CapaPresentación.Formularios;
using MaterialSkin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmPrincipal : Form
    {
        private frmLogin loginForm; // Variable para almacenar la instancia de frmLogin

        private bool mousePresionado;
        private Point mousePosicion;
        bool primerClic = true;

        List<ucMenu> menuButtons;
        MaterialSkinManager materialSkinManager;
        private string connectionString = logConexion.Instancia.ObtenerConexion();
        public frmPrincipal()
        {
            InitializeComponent();
            menuButtons = new List<ucMenu>() { btnHome, btnPuntos, btnPersonal };
            ClickMenu(menuButtons);
        }

        public void LoadFormPanel(Form form)
        {
            // Eliminar todos los controles existentes en el panelCentral
            this.panelCentral.Controls.Clear();

            // Asignar el nuevo formulario al panelCentral
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            this.panelCentral.Controls.Add(form);
            this.panelCentral.Tag = form;
            form.Show();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    
        private void botones(ucMenu btnHabilitado, ucMenu btnDeshabilitado)
        {
            if(btnHabilitado.Enabled == false) 
            {
                btnHabilitado.Enabled = true;
                btnDeshabilitado.Enabled = false;
            }

            btnDeshabilitado.Enabled = false;
        }

        private void Menu_menuClick(object sender, EventArgs e)
        {
            ucMenu _menuButton = (ucMenu)sender;

            // Diccionario para mapear botones a formularios y acciones
            var mapaAcciones = new Dictionary<string, Action>()
            {
                { "btnHome", () =>
                    {
                        btnPersonal.Enabled = btnPuntos.Enabled = true;
                        btnHome.Enabled = false;
                        activeMenu(btnHome, btnPuntos, btnPersonal);
                        CargarFormularioEnPanel(new frmHomeVisitas());
                    }
                },
                { "btnPuntos", () =>
                    {
                        activeMenu(btnPuntos, btnHome, btnPersonal);
                        botones(btnPersonal, btnPuntos);
                        btnHome.Enabled = true;
                        CargarFormularioEnPanel(new frmLocalidades());
                    }
                },
                { "btnPersonal", () =>
                    {
                        activeMenu(btnPersonal, btnHome, btnPuntos);
                        botones(btnPuntos, btnPersonal);
                        btnHome.Enabled = true;
                        CargarFormularioEnPanel(new frmPersonal());
                    }
                }
            };

            // Verificar si hay formularios a cerrar (usando LINQ para simplificar)
            var formulariosACerrar = Application.OpenForms.Cast<Form>()
                .Where(f => f != this && (f is frmImagen || f is frmDatosPersonal || f is frmMapa || f is frmVisitas))
                .ToArray();

            if (formulariosACerrar.Length > 0)
            {
                var resultado = MessageBox.Show("Hay formularios abiertos. ¿Desea continuar y cerrarlos?",
                                                "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.No)
                {
                    return; // No hacer nada si el usuario elige "No"
                }

                // Cerrar los formularios encontrados
                foreach (var formulario in formulariosACerrar)
                {
                    formulario.Close();
                }
            }

            // Ejecutar la acción asociada al botón
            if (mapaAcciones.TryGetValue(_menuButton.Name, out var accion))
            {
                accion();
            }
        }

        // Método auxiliar para cargar formularios en el panel (sin cambios)
        private void CargarFormularioEnPanel(Form formulario)
        {
            LimpiarControles(panelCentral);
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            panelCentral.Controls.Add(formulario);
            formulario.Show();
        }

        private void activeMenu(ucMenu _active, params ucMenu[] _inactive)
        {
            _active.BorderColor = Color.White;
            foreach (ucMenu inactive in _inactive)
            {
                inactive.BorderColor = Color.Transparent;
            }
        }
        private void ClickMenu(List<ucMenu> _menu)
        {

            foreach (var menu in _menu)
            {
                menu.menuClick += Menu_menuClick;
            }

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

        private void BarraControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mousePresionado = true;
                mousePosicion = e.Location;
            }
        }

        private void BarraControl_MouseUp(object sender, MouseEventArgs e)
        {
            mousePresionado = false;
        }

        private void BarraControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (mousePresionado)
            {
                Point nuevaPosicion = PointToScreen(e.Location);
                Location = new Point(nuevaPosicion.X - mousePosicion.X, nuevaPosicion.Y - mousePosicion.Y);
            }
        }


        private void LimpiarControles(Control container)
        {
            // Verificar si existen controles en el estado visible especificado
            if (!ExistenControlesEnEstado(container.Controls.OfType<Control>().ToArray(), false))
            {
                // Eliminar y liberar todos los controles hijos del contenedor
                while (container.Controls.Count > 0)
                {
                    var control = container.Controls[0];
                    container.Controls.Remove(control);
                    control.Dispose();
                }
            }
        }


        private bool ExistenControlesEnEstado(Control[] controles, bool estado)
        {
            foreach (Control control in controles)
            {
                if (control.Visible == estado)
                {
                    return true;
                }
            }
            return false;
        }


        private void btnPuntos_Load(object sender, EventArgs e)
        {

        }

        private void Home_Load(object sender, EventArgs e)
        {
            if (loginForm != null && !loginForm.Visible)
            {
                loginForm.Close();
            }
        }

        private void btnPersonal_Load(object sender, EventArgs e)
        {

        }

        private void materialDivider3_Click(object sender, EventArgs e)
        {

        }
    }
}
