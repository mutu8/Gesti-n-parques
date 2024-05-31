using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using CapaPresentacion.ClasesForm;
using CapaPresentacion.Properties;
using MaterialSkin;
using System.Reflection.Emit;
using CapaLogica;
using System.IO;
using System.Net;
using CapaPresentacion;
using static CapaLogica.FormUtil;

namespace CapaPresentacion
{
    public partial class Home : Form
    {
        private bool mousePresionado;
        private Point mousePosicion;
        bool primerClic = true;

        List<ucMenu> menuButtons;
        MaterialSkinManager materialSkinManager;
        private string connectionString = logConexion.Instancia.ObtenerConexion();
        public Home()   
        {
            InitializeComponent();
            menuButtons = new List<ucMenu>() {btnHome, btnPuntos, btnPersonal};
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

        private void Menu_menuClick(object sender, EventArgs e)
        {
            ucMenu _menuButton = (ucMenu)sender;

            switch (_menuButton.Name)
            {
                case "btnHome":
                    activeMenu(btnHome, btnPuntos);
                    activeMenu(btnHome, btnPersonal);
                    // Limpiar los controles del panel central
                    LimpiarControles(panelCentral);
                    // Aquí puedes agregar los controles necesarios para "Home"
                    break;

                case "btnPuntos":
                    activeMenu(btnPuntos, btnHome);
                    activeMenu(btnPuntos, btnPersonal);
                    // Crear una instancia del formulario secundario que deseas cargar
                    frmLocalidades f = new frmLocalidades();

                    // Limpiar los controles del panel central
                    LimpiarControles(panelCentral);
                    // Cargar el formulario secundario en el panel central
                    f.TopLevel = false;
                    f.FormBorderStyle = FormBorderStyle.None;
                    f.Dock = DockStyle.Fill;
                    panelCentral.Controls.Add(f);
                    f.Show();
                    break;

                case "btnPersonal":
                    activeMenu(btnPersonal, btnHome);
                    activeMenu(btnPersonal, btnPuntos);
                    // Crear una instancia del formulario secundario que deseas cargar
                    frmPersonal ff = new frmPersonal();

                    // Limpiar los controles del panel central
                    LimpiarControles(panelCentral);
                    // Cargar el formulario secundario en el panel central
                    ff.TopLevel = false;
                    ff.FormBorderStyle = FormBorderStyle.None;
                    ff.Dock = DockStyle.Fill;
                    panelCentral.Controls.Add(ff);
                    ff.Show();
                    break;
            }
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
    }
}
