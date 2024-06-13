using CapaLogica;
using CapaPresentación.Formularios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmLocalidades : Form
    {
        private bool frmMapaAbierto = false;
        private frmMapa frmMapaInstancia;

        public bool seDebeActualizar = false;

        private List<UserControlTarget> allUserControlsLocalidades = new List<UserControlTarget>();

        public List<DataRow> allLocalidadesData; // Almacenar todos los datos

        public frmLocalidades()
        {
            InitializeComponent();
        }

        // Propiedad pública para exponer el panel
        public FlowLayoutPanel PanelLocalidades
        {
            get { return flowLayoutPanel1; }
            set { flowLayoutPanel1 = value; }
        }

        public void EstadoBloqueado(bool bloquear)
        {
            PanelLocalidades.Enabled = bloquear;
            materialFloatingActionButton1.Enabled = bloquear;
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
            try
            {
                // 1. Verificar si es necesario actualizar (antes de cargar datos)
                if (allUserControlsLocalidades.Count > 0 && !seDebeActualizar)
                {
                    return; // No hay cambios, salimos temprano
                }

                // 2. Obtener datos (considerar carga asíncrona si es necesario)
                DataTable dtLocalidades = logLocalidades.Instancia.ObtenerTodasLasLocalidades();

                if (dtLocalidades == null || dtLocalidades.Rows.Count == 0)
                {
                    MessageBox.Show("Error: No hay datos de localidades disponibles.");
                    return;
                }

                // 3. Reutilizar o crear UserControls
                int existingControls = allUserControlsLocalidades.Count;
                int newControlsNeeded = dtLocalidades.Rows.Count - existingControls;

                for (int i = 0; i < newControlsNeeded; i++)
                {
                    allUserControlsLocalidades.Add(new UserControlTarget(this));
                }

                // 4. Actualizar datos en los UserControls existentes
                for (int i = 0; i < dtLocalidades.Rows.Count; i++)
                {
                    DataRow row = dtLocalidades.Rows[i];
                    UserControlTarget userControl = allUserControlsLocalidades[i];

                    userControl.NombreLocalidad = row["Nombre_Localidad"].ToString();
                    userControl.Direccion = row["Direccion"].ToString();
                    // userControl.Url = row["url_Localidad"].ToString(); // Si tienes la columna

                    // Establecer el tamaño fijo de los UserControls (incluyendo padding)
                    userControl.Width = 500; // Ancho sin padding
                    userControl.Height = 169;
                    userControl.Margin = new Padding(5); // Padding de 5 en todos los lados
                }

                // 5. Configurar el FlowLayoutPanel (una sola vez)
                flowLayoutPanel1.SuspendLayout();
                flowLayoutPanel1.Controls.Clear(); // Limpiar antes de agregar

                foreach (var userControl in allUserControlsLocalidades)
                {
                    // Evitar duplicados
                    if (!flowLayoutPanel1.Controls.Contains(userControl))
                    {
                        flowLayoutPanel1.Controls.Add(userControl);
                    }
                }

                flowLayoutPanel1.ResumeLayout(); // Reanudar el diseño

                seDebeActualizar = false; // Reiniciar la bandera
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las localidades: {ex.Message}"); // Interpolación de cadenas
            }
        }





        private void materialFloatingActionButton1_Click(object sender, EventArgs e)
        {

            if (!frmMapaAbierto)
            {
                // Si no está abierto, crear una instancia y mostrar el formulario secundario
                frmMapaInstancia = new frmMapa();
                frmMapaInstancia.textoBoton = "Agregar";
                frmMapaInstancia.InstanciFrmL = this;

                EstadoBloqueado(false);

                // Centrar el formulario en la pantalla antes de mostrarlo
                frmMapaInstancia.StartPosition = FormStartPosition.CenterScreen;

                // Actualizar la bandera cuando el formulario se cierre
                frmMapaInstancia.FormClosed += (s, args) =>
                {
                    frmMapaAbierto = false;
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
            allLocalidadesData = logLocalidades.Instancia.ObtenerLocalidadesParaPanel().AsEnumerable().ToList();
            
            // Verificar si el panel ya contiene elementos
            if (PanelLocalidades.Controls.Count == 0)
            {
                allLocalidadesData = logLocalidades.Instancia.ObtenerLocalidadesParaPanel().AsEnumerable().ToList();
                CargarLocalidadesEnPanel();
            }
        }


        private void flowLayoutPanel1_Scroll(object sender, ScrollEventArgs e)
        {

        }
    }
}

