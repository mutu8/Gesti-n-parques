using CapaLogica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;


namespace CapaPresentacion
{
    public partial class frmPersonal : Form
    {
        private bool frmDatosAbierto = false;
        private frmDatosPersonal frmDatosInstancia;

        private List<UserControlEmpleado> allUserControlsEmpleados = new List<UserControlEmpleado>(); // Lista para almacenar todos los UserControls

        public bool seDebeActualizar = false;

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

        public void CargarUserControlsEmpleados()
        {
            try
            {
                if (allUserControlsEmpleados.Count == 0 || seDebeActualizar)
                {
                    // Obtener los datos de los empleados y crear los UserControls si no existen o si se debe actualizar
                    DataTable dtEmpleados = logEmleados.Instancia.ObtenerTodosLosEmpleados();

                    if (dtEmpleados == null || dtEmpleados.Rows.Count == 0)
                    {
                        MessageBox.Show("Error: No hay datos de empleados disponibles.");
                        return;
                    }

                    // Limpiar la lista de UserControls antes de agregar los nuevos
                    allUserControlsEmpleados.Clear();

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

                    // Marcar que ya no es necesario actualizar
                    seDebeActualizar = false;
                }

                // Limpiar y actualizar el FlowLayoutPanel proporcionado (siempre se ejecuta)
                panelPersonal.Controls.Clear();
                panelPersonal.SuspendLayout();

                foreach (var empleado in allUserControlsEmpleados)
                {
                    panelPersonal.Controls.Add(empleado);
                }

                panelPersonal.ResumeLayout();
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
                frmDatosInstancia.InstanciFrmE = this;
                // Centrar el formulario en la pantalla antes de mostrarlo
                frmDatosInstancia.StartPosition = FormStartPosition.CenterScreen;
                // Actualizar la bandera cuando el formulario se cierre
                frmDatosInstancia.FormClosed += (s, args) =>
                {
                    frmDatosAbierto = false;
                };
                EstadoBloqueado(false);
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

        }

        private void btnLeft_Click(object sender, EventArgs e)
        {

        }

    }
}
