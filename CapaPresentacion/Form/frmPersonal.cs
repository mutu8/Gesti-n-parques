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

        private void CargarComboBox()
        {
            materialComboBox1.Items.Add("Parques");
            materialComboBox1.Items.Add("Limpieza");

            // Selecciona la primera opción por defecto (opcional)
            materialComboBox1.SelectedIndex = -1;
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

                // Obtener empleados según el filtro
                dtEmpleados = string.IsNullOrEmpty(filtro) ?
                              logEmleados.Instancia.ObtenerTodosLosEmpleados() :
                              logEmleados.Instancia.ObtenerEmpleadosFiltrados(filtro);

                panelPersonal.SuspendLayout(); // Suspender el diseño para mejorar el rendimiento

                // Remover y liberar recursos de los controles existentes de manera más eficiente
                foreach (Control control in panelPersonal.Controls)
                {
                    control.Dispose();
                }
                panelPersonal.Controls.Clear();

                allUserControlsEmpleados.Clear();

                // Configurar el batch size para la recolección de basura (ajusta según tus necesidades)
                const int batchSize = 50;
                int controlCount = 0;

                foreach (DataRow row in dtEmpleados.Rows)
                {
                    // Crear el UserControl para cada empleado
                    UserControlEmpleado nuevoEmpleado = new UserControlEmpleado(this)
                    {
                        Nombre = row["Nombres"].ToString() + " " + row["Apellidos"].ToString(),
                        Rol = CargoInverso((bool)row["esApoyo"]),
                        Anchor = AnchorStyles.Right | AnchorStyles.Left,
                        Margin = new Padding(5),
                        BtnEditar = true // Configuración por defecto
                    };

                    // Verificar si es el cumpleaños del empleado
                    DateTime? fechaNacimiento = row["FechaNacimiento"] as DateTime?;
                    if (fechaNacimiento.HasValue)
                    {
                        DateTime fechaHoy = DateTime.Today;
                        DateTime fechaComparacion = new DateTime(fechaHoy.Year, fechaNacimiento.Value.Month, fechaNacimiento.Value.Day);
                        nuevoEmpleado.BtnEditar = fechaComparacion.Date != fechaHoy.Date;
                    }

                    // Agregar el control al panel y a la lista
                    allUserControlsEmpleados.Add(nuevoEmpleado);
                    panelPersonal.Controls.Add(nuevoEmpleado);

                    // Optimizar la memoria con recolección de basura en lotes
                    if (++controlCount % batchSize == 0)
                    {
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                    }
                }

                panelPersonal.ResumeLayout(); // Reanudar el diseño después de agregar los controles
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
            CargarComboBox();
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

        private void materialComboBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Obtener el texto seleccionado en el ComboBox
                string seleccion = materialComboBox1.SelectedItem.ToString();

                DataTable dtEmpleados;

                // Filtrar los empleados según la selección
                if (seleccion == "Parques")
                {
                    dtEmpleados = logEmleados.Instancia.ObtenerEmpleadosPersonalYParques("parques");
                }
                else if (seleccion == "Limpieza")
                {
                    dtEmpleados = logEmleados.Instancia.ObtenerEmpleadosPersonalYParques("limpieza");
                }
                else
                {
                    // Si no se selecciona "Parques" o "Limpieza", obtener todos los empleados
                    dtEmpleados = logEmleados.Instancia.ObtenerTodosLosEmpleados(); // Asegúrate de tener este método en tu capa lógica
                }

                // Limpiar la lista de UserControls y crear nuevos basados en los datos obtenidos
                allUserControlsEmpleados.Clear();
                foreach (DataRow row in dtEmpleados.Rows)
                {
                    UserControlEmpleado nuevoEmpleado = new UserControlEmpleado(this);
                    nuevoEmpleado.Nombre = row["Nombres"].ToString() + " " + row["Apellidos"].ToString();
                    nuevoEmpleado.Rol = CargoInverso((bool)row["esApoyo"]);

                    // Otras propiedades del UserControl
                    nuevoEmpleado.Anchor = AnchorStyles.Right | AnchorStyles.Left;
                    nuevoEmpleado.Margin = new Padding(5);

                    // Obtener la fecha de nacimiento del empleado si no es null
                    DateTime? fechaNacimiento = row["FechaNacimiento"] as DateTime?;
                    if (fechaNacimiento.HasValue)
                    {
                        DateTime fechaHoy = DateTime.Today;
                        DateTime fechaComparacion = new DateTime(fechaHoy.Year, fechaNacimiento.Value.Month, fechaNacimiento.Value.Day);

                        if (fechaComparacion.Date == fechaHoy.Date)
                        {
                            nuevoEmpleado.BtnEditar = false; // Inhabilitar el botón de editar si es su cumpleaños
                        }
                        else
                        {
                            nuevoEmpleado.BtnEditar = true; // Habilitar el botón de editar
                        }
                    }
                    else
                    {
                        nuevoEmpleado.BtnEditar = true; // Habilitar el botón de editar si la fecha de nacimiento es null
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
    }
}
