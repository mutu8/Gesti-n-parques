using CapaLogica;
using CapaPresentación.Formularios;
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


namespace CapaPresentacion
{
    public partial class frmPersonal : Form
    {
        private bool frmDatosAbierto = false;
        private frmDatosPersonal frmDatosInstancia;

        private List<UserControlEmpleado> allUserControlsEmpleados = new List<UserControlEmpleado>(); // Lista para almacenar todos los UserControls

        // Propiedades de Paginación
        private int currentPage = 1;
        private int itemsPerPage = 9; // Ajusta según sea necesario

        public frmPersonal()
        {
            InitializeComponent();
        }

        // Propiedad pública para exponer el panel
        public FlowLayoutPanel panelPersonal
        {
            get { return flowLayoutPanel1; }
            set { flowLayoutPanel1 = value; }
        }

        public void EstadoBloqueado(bool bloquear)
        {
            panelPersonal.Enabled = bloquear;
            materialFloatingActionButton1.Enabled = bloquear;
        }
        private string CargoInverso(bool esApoyo)
        {
            return esApoyo ? "Apoyo" : "728";
        }
        public void RecargarPanel()
        {
            CargarUserControlsEmpleados(); // Recargar la página actual
        }
        private void CargarUserControlsEmpleados(int pageNumber = 1)
        {
            try
            {
                if (allUserControlsEmpleados.Count == 0)
                {
                    // 1. Obtener los datos de los empleados y crear los UserControls si no existen
                    DataTable dtEmpleados = logEmleados.Instancia.ObtenerTodosLosEmpleados();

                    if (dtEmpleados == null || dtEmpleados.Rows.Count == 0)
                    {
                        MessageBox.Show("Error: No hay datos de empleados disponibles.");
                        return;
                    }

                    foreach (DataRow row in dtEmpleados.Rows)
                    {
                        UserControlEmpleado nuevoEmpleado = new UserControlEmpleado(this);
                        nuevoEmpleado.Nombre = row["Nombres"].ToString() + " " + row["Apellidos"].ToString();
                        nuevoEmpleado.Rol = CargoInverso((bool)row["esApoyo"]);
                        // ... (Otras propiedades que tenga tu UserControl, como ID, foto, etc.)
                        nuevoEmpleado.Anchor = AnchorStyles.Right | AnchorStyles.Left;
                        nuevoEmpleado.Margin = new Padding(5);

                        allUserControlsEmpleados.Add(nuevoEmpleado); // Agregar a la lista
                    }
                }

                int startIndex = (pageNumber - 1) * itemsPerPage;
                int endIndex = Math.Min(startIndex + itemsPerPage, allUserControlsEmpleados.Count);

                flowLayoutPanel1.SuspendLayout();
                flowLayoutPanel1.Controls.Clear(); // Limpiar el FlowLayoutPanel

                // 2. Agregar los UserControls correspondientes a la página actual
                for (int i = startIndex; i < endIndex; i++)
                {
                    flowLayoutPanel1.Controls.Add(allUserControlsEmpleados[i]);
                }

                flowLayoutPanel1.ResumeLayout();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los empleados: " + ex.Message);
            }
        }

        private void materialFloatingActionButton1_Click(object sender, EventArgs e)
        {
            if (!frmDatosAbierto)
            {
                // Si no está abierto, crear una instancia y mostrar el formulario secundario
                frmDatosInstancia = new frmDatosPersonal();
                frmDatosInstancia.textoBoton = "Agregar";

                // Centrar el formulario en la pantalla antes de mostrarlo
                frmDatosInstancia.StartPosition = FormStartPosition.CenterScreen;

                // Actualizar la bandera cuando el formulario se cierre
                frmDatosInstancia.FormClosed += (s, args) =>
                {
                    frmDatosAbierto = false;
                    // Recargar el panel cuando el formulario se cierra
                    //RecargarPanel();
                };

                // Mostrar el formulario secundario
                frmDatosInstancia.Show();
                frmDatosAbierto = true; // Actualizar la bandera
            }
            else
            {
                // Si ya está abierto, enfocar el formulario secundario
                frmDatosInstancia.Focus();
            }
            
        }

        private void panelVisitas_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmPersonal_Load(object sender, EventArgs e)
        {
            CargarUserControlsEmpleados();
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            // Calcular el número total de páginas
            int totalPages = (int)Math.Ceiling((double)allUserControlsEmpleados.Count / itemsPerPage);

            if (currentPage < totalPages)
            {
                currentPage++;
                CargarUserControlsEmpleados(currentPage); // Cargar la siguiente página
            }
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                CargarUserControlsEmpleados(currentPage); // Cargar la página anterior
            }
        }

    }
}
