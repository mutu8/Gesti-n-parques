using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using CapaLogica;
using CapaPresentación;
using CapaPresentación.Formularios;
using MaterialSkin;
using MaterialSkin.Controls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CapaPresentacion
{
    public partial class frmLocalidades : Form
    {
        private bool frmMapaAbierto = false;
        private frmMapa frmMapaInstancia;
        public frmLocalidades()
        {
            InitializeComponent();
        }

        // Propiedad pública para exponer el panel
        public FlowLayoutPanel PanelLocalidades
        {
            get { return flowLayoutPanel1; }
        }

        private void btnMapa_MouseClick(object sender, MouseEventArgs e)
        {
            // Verificar si el formulario secundario ya está abierto
            if (!frmMapaAbierto)
            {
                // Si no está abierto, crear una instancia y mostrar el formulario secundario
                frmMapaInstancia = new frmMapa();

                // Centrar el formulario en la pantalla antes de mostrarlo
                frmMapaInstancia.StartPosition = FormStartPosition.CenterScreen;

                // Actualizar la bandera cuando el formulario se cierre
                frmMapaInstancia.FormClosed += (s, args) => frmMapaAbierto = false;

                // Mostrar el formulario secundario
                frmMapaInstancia.Show();
                frmMapaAbierto = true; // Actualizar la bandera
            }
            else
            {
                // Si ya está abierto, enfocar el formulario secundario
                frmMapaInstancia.Focus();
            }
        }

        public void CargarLocalidadesEnPanel()
        {

            DataTable localidadesData = logLocalidades.Instancia.ObtenerLocalidadesParaPanel();

            foreach (DataRow row in localidadesData.Rows)
            {
                string nombreLocalidad = row["Nombre_Localidad"].ToString();
                string direccion = row["Direccion"].ToString();
                string url = row["url_Localidad"].ToString();

                UserControlTarget userControl = new UserControlTarget();
                userControl.SetLocalidadData(nombreLocalidad, direccion, url);
                userControl.Anchor = AnchorStyles.Right | AnchorStyles.Left; // Alinear a la derecha e izquierda
                userControl.Margin = new Padding(10); // Espacio de relleno en el lado derecho e izquierdo
                flowLayoutPanel1.Controls.Add(userControl);
            }

        }
        public void RecargarPanel()
        {
            // Suspender el diseño del panel antes de realizar múltiples cambios
            flowLayoutPanel1.SuspendLayout();

            // Limpiar el panel antes de recargar nuevos controles
            flowLayoutPanel1.Controls.Clear();

            // Cargar localidades en el panel
            CargarLocalidadesEnPanel();

            // Reanudar el diseño del panel después de realizar los cambios
            flowLayoutPanel1.ResumeLayout();
        }


        private void materialFloatingActionButton1_Click(object sender, EventArgs e)
        {
            if (!frmMapaAbierto)
            {
                // Si no está abierto, crear una instancia y mostrar el formulario secundario
                frmMapaInstancia = new frmMapa();
                frmMapaInstancia.textoBoton = "Agregar";

                // Centrar el formulario en la pantalla antes de mostrarlo
                frmMapaInstancia.StartPosition = FormStartPosition.CenterScreen;

                // Actualizar la bandera cuando el formulario se cierre
                frmMapaInstancia.FormClosed += (s, args) =>
                {
                    frmMapaAbierto = false;
                    // Recargar el panel cuando el formulario se cierra
                    RecargarPanel();
                };

                // Mostrar el formulario secundario
                frmMapaInstancia.Show();
                frmMapaAbierto = true; // Actualizar la bandera
            }
            else
            {
                // Si ya está abierto, enfocar el formulario secundario
                frmMapaInstancia.Focus();
            }
        }


        private void frmLocalidades_Load(object sender, EventArgs e)
        {
            CargarLocalidadesEnPanel();
        }
    }
}

