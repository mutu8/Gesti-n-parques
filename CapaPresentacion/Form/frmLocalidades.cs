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

        // Diccionario para almacenar las instancias de los UserControl
        private Dictionary<string, UserControlTarget> userControlsCache = new Dictionary<string, UserControlTarget>();

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

        // Método para obtener o crear UserControl
        private UserControlTarget GetOrCreateUserControl(string key, Func<UserControlTarget> createControl)
        {
            if (!userControlsCache.ContainsKey(key))
            {
                userControlsCache[key] = createControl();
            }
            return userControlsCache[key];
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
                if (allLocalidadesData == null || allLocalidadesData.Count == 0)
                {
                    return; // Salir del método si no hay datos
                }

                flowLayoutPanel1.SuspendLayout();
                flowLayoutPanel1.Controls.Clear(); // Limpiar el FlowLayoutPanel

                // Obtener el tamaño visible del FlowLayoutPanel
                int visibleWidth = flowLayoutPanel1.ClientSize.Width;
                int visibleHeight = flowLayoutPanel1.ClientSize.Height;

                // Calcular el tamaño de los UserControls basándose en el tamaño visible y el número de columnas
                int numColumns = 2; // Número de columnas
                int controlWidth = visibleWidth / numColumns - SystemInformation.VerticalScrollBarWidth; // Ancho del UserControl ajustado para tener en cuenta la barra de desplazamiento vertical
                int controlHeight = 200; // Altura predeterminada del UserControl

                // Ajustar las propiedades de margen y flujo del FlowLayoutPanel
                flowLayoutPanel1.Padding = new Padding(0);
                flowLayoutPanel1.Margin = new Padding(0);
                flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;

                foreach (DataRow row in allLocalidadesData)
                {
                    // Verificar si los valores son NULL antes de usarlos
                    string nombreLocalidad = row.Field<string>("Nombre_Localidad") ?? string.Empty;
                    string direccion = row.Field<string>("Direccion") ?? string.Empty;
                    string url = row.Field<string>("url_Localidad") ?? string.Empty;

                    string key = $"{nombreLocalidad}-{direccion}";

                    // Crear o recuperar UserControl del caché
                    UserControlTarget userControl = GetOrCreateUserControl(key, () =>
                    {
                        var control = new UserControlTarget(this);
                        control.SetLocalidadData(nombreLocalidad, direccion, url);
                        control.Width = controlWidth;
                        control.Height = controlHeight;
                        control.Margin = new Padding(5); // Ajustar el margen del UserControl
                        return control;
                    });

                    userControl.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                    flowLayoutPanel1.Controls.Add(userControl);
                }

                flowLayoutPanel1.ResumeLayout();
            }
            catch (Exception ex)
            {
                // Manejar cualquier otra excepción que pueda ocurrir
                MessageBox.Show("Error al cargar las localidades: " + ex.Message);
                // Puedes registrar el error en un archivo de registro o realizar otras acciones apropiadas
            }
        }



        public void RecargarPanel()
        {
            CargarLocalidadesEnPanel(); // Recargar la página actual
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
            allLocalidadesData = logLocalidades.Instancia.ObtenerLocalidadesParaPanel().AsEnumerable().ToList();
            CargarLocalidadesEnPanel(); // Cargar la página inicial
        }


        private void flowLayoutPanel1_Scroll(object sender, ScrollEventArgs e)
        {

        }
    }
}

