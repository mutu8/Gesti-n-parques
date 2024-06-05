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

        public frmPersonal()
        {
            InitializeComponent();
        }
        private string CargoInverso(bool esApoyo)
        {
            return esApoyo ? "Apoyo" : "728";
        }
        public void RecargarPanel()
        {
            CargarUserControlsEmpleados(); // Recargar la página actual
        }
        private void CargarUserControlsEmpleados()
        {
            try
            {
                // 1. Obtener los datos de los empleados (desde logEmpleados)
                DataTable dtEmpleados = logEmleados.Instancia.ObtenerTodosLosEmpleados();

                // 2. Limpiar el FlowLayoutPanel (si es necesario)
                flowLayoutPanel1.Controls.Clear();

                // 3. Crear y agregar UserControls por cada empleado
                foreach (DataRow row in dtEmpleados.Rows)
                {
                    UserControlEmpleado nuevoEmpleado = new UserControlEmpleado();

                    // Configurar las propiedades del UserControl con los datos del empleado
                    nuevoEmpleado.Nombre = row["Nombres"].ToString() + " " + row["Apellidos"].ToString();

                    // Usar CargoInverso para obtener el string del cargo
                    bool esApoyo = (bool)row["esApoyo"];
                    nuevoEmpleado.Rol = CargoInverso(esApoyo); // Asignar el resultado de CargoInverso

                    // ... (Otras propiedades que tenga tu UserControl, como ID, foto, etc.)

                    nuevoEmpleado.Anchor = AnchorStyles.Right | AnchorStyles.Left;
                    nuevoEmpleado.Margin = new Padding(5);

                    // Agregar el UserControl al FlowLayoutPanel
                    flowLayoutPanel1.Controls.Add(nuevoEmpleado);
                }
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
    }
}
