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
            // No necesitamos obtener los datos de nuevo, ya los tenemos en allLocalidadesData

            int startIndex = (pageNumber - 1) * itemsPerPage;
            int endIndex = Math.Min(startIndex + itemsPerPage, allLocalidadesData.Count);

            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.Controls.Clear();

            for (int i = startIndex; i < endIndex; i++)
            {
                DataRow row = allLocalidadesData[i];
                string nombreLocalidad = row["Nombre_Localidad"].ToString();
                string direccion = row["Direccion"].ToString();
                string url = row["url_Localidad"].ToString();

                // Generar una clave única (igual que antes)
                string key = $"{nombreLocalidad}-{direccion}";

                // Obtener o crear el UserControl (igual que antes)
                UserControlTarget userControl = GetOrCreateUserControl(key, () => {
                    var control = new UserControlTarget();
                    control.SetLocalidadData(nombreLocalidad, direccion, url);
                    return control;
                });

                userControl.Anchor = AnchorStyles.Right | AnchorStyles.Left;
                userControl.Margin = new Padding(5);
                flowLayoutPanel1.Controls.Add(userControl);
            }

            flowLayoutPanel1.ResumeLayout();
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

    
    }
}

