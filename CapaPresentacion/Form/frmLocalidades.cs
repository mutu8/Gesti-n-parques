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

        // Propiedades de Paginación
        private int currentPage = 1;
        private int itemsPerPage = 9; // Ajusta según sea necesario
        private List<DataRow> allLocalidadesData; // Almacenar todos los datos
       

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

        public void CargarLocalidadesEnPanel(int pageNumber)
        {
            try
            {
                if (allLocalidadesData == null || allLocalidadesData.Count == 0)
                {
                    return; // Salir del método si no hay datos
                }

                int startIndex = (pageNumber - 1) * itemsPerPage;
                int endIndex = Math.Min(startIndex + itemsPerPage, allLocalidadesData.Count);

                flowLayoutPanel1.SuspendLayout();

                // 1. Eliminar UserControls que ya no están en la página actual
                for (int i = flowLayoutPanel1.Controls.Count - 1; i >= 0; i--)
                {
                    UserControlTarget control = (UserControlTarget)flowLayoutPanel1.Controls[i];
                    string key = control.NombreLocalidad + "-" + control.Direccion;

                    // Verificar si la clave está dentro del rango de la página actual
                    int controlIndex = allLocalidadesData.FindIndex(row =>
                        row["Nombre_Localidad"].ToString() == control.NombreLocalidad &&
                        row["Direccion"].ToString() == control.Direccion
                    );

                    if (controlIndex < startIndex || controlIndex >= endIndex)
                    {
                        flowLayoutPanel1.Controls.RemoveAt(i);
                        userControlsCache.Remove(key);
                        control.Dispose();
                    }
                }

                // 2. Agregar nuevos UserControls para la página actual
                for (int i = startIndex; i < endIndex; i++)
                {
                    DataRow row = allLocalidadesData[i];

                    // Verificar si los valores son NULL antes de usarlos
                    string nombreLocalidad = row["Nombre_Localidad"]?.ToString() ?? string.Empty; // Valor predeterminado si es NULL
                    string direccion = row["Direccion"]?.ToString() ?? string.Empty;           // Valor predeterminado si es NULL
                    string url = row["url_Localidad"]?.ToString() ?? string.Empty;            // Valor predeterminado si es NULL

                    string key = $"{nombreLocalidad}-{direccion}";

                    if (!flowLayoutPanel1.Controls.ContainsKey(key))
                    {
                        UserControlTarget userControl = GetOrCreateUserControl(key, () =>
                        {
                            var control = new UserControlTarget(this);
                            control.SetLocalidadData(nombreLocalidad, direccion, url);
                            return control;
                        });

                        userControl.Anchor = AnchorStyles.Right | AnchorStyles.Left;
                        userControl.Margin = new Padding(5);
                        flowLayoutPanel1.Controls.Add(userControl);
                    }
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
            CargarLocalidadesEnPanel(currentPage); // Recargar la página actual
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
            CargarLocalidadesEnPanel(currentPage); // Cargar la página inicial
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            int totalPages = (int)Math.Ceiling((double)allLocalidadesData.Count / itemsPerPage);
            if (currentPage < totalPages)
            {
                currentPage++;
                RecargarPanel();
            }
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                RecargarPanel();
            }
        }

        private void flowLayoutPanel1_Scroll(object sender, ScrollEventArgs e)
        {
            // Calcular la página actual en función de la posición de desplazamiento
            int newPage = currentPage;

            if (newPage != currentPage)
            {
                currentPage = newPage;
                CargarLocalidadesEnPanel(currentPage);
            }
        }
    }
}

