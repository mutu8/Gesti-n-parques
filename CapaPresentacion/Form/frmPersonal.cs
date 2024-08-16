using CapaLogica;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

            toolTip1.SetToolTip(materialFloatingActionButton1, "Agregar nuevo personal");
            toolTip1.SetToolTip(materialTextBox1, "Buscar");
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

        public void CargarUserControlsEmpleados(string filtro = "")
        {
            try
            {
                DataTable dtEmpleados;

                // Obtener datos de empleados (todos o filtrados)
                if (string.IsNullOrEmpty(filtro))
                {
                    dtEmpleados = logEmleados.Instancia.ObtenerTodosLosEmpleados(); // Obtener todos los empleados
                    seDebeActualizar = false; // Reiniciar la bandera después de cargar
                }
                else
                {
                    dtEmpleados = logEmleados.Instancia.ObtenerEmpleadosFiltrados(filtro); // Obtener empleados filtrados
                }

                // Limpiar la lista de UserControls y crear nuevos basados en los datos obtenidos
                allUserControlsEmpleados.Clear();
                foreach (DataRow row in dtEmpleados.Rows)
                {
                    UserControlEmpleado nuevoEmpleado = new UserControlEmpleado(this);
                    nuevoEmpleado.Nombre = row["Nombres"].ToString() + " " + row["Apellidos"].ToString();
                    nuevoEmpleado.Rol = CargoInverso((bool)row["esApoyo"]);

                    // ... (Otras propiedades que tenga tu UserControl, como ID, foto, etc.)
                    nuevoEmpleado.Anchor = AnchorStyles.Right | AnchorStyles.Left;
                    nuevoEmpleado.Margin = new Padding(5);

                    // Obtener la fecha de nacimiento del empleado si no es null
                    DateTime? fechaNacimiento = row["FechaNacimiento"] as DateTime?;

                    if (fechaNacimiento.HasValue)
                    {
                        // Crear una fecha con el día y mes de hoy
                        DateTime fechaHoy = DateTime.Today;
                        DateTime fechaComparacion = new DateTime(fechaHoy.Year, fechaNacimiento.Value.Month, fechaNacimiento.Value.Day);

                        // Verificar si la fecha de nacimiento es la misma que la de hoy (solo día y mes)
                        if (fechaComparacion.Date == fechaHoy.Date)
                        {
                            nuevoEmpleado.BtnEditar = false; // Inhabilitar el botón de editar
                        }
                        else
                        {
                            nuevoEmpleado.BtnEditar = true; // Habilitar el botón de editar
                        }
                    }
                    else
                    {
                        nuevoEmpleado.BtnEditar = true; // Por ejemplo, habilitar el botón si la fecha de nacimiento es null
                    }



                    allUserControlsEmpleados.Add(nuevoEmpleado);
                }

                // Actualizar el FlowLayoutPanel
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

        private void materialTextBox1_TextChanged(object sender, EventArgs e)
        {
            string filtro = materialTextBox1.Text;
            CargarUserControlsEmpleados(filtro); // Recargar con el filtro aplicado
        }
    }
}
