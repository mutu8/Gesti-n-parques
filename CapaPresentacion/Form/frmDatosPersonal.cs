using CapaLogica;
using MaterialSkin.Controls;
using System;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmDatosPersonal : MaterialForm
    {
        public string textoBoton { get; set; } // Ya lo tienes
        private readonly logEmleados _logEmpleados;

        public frmPersonal InstanciFrmE;

        // Nuevas propiedades con get y set
        public string Nombre
        {
            get { return txtNombre.Text; }
            set { txtNombre.Text = value; }
        }

        public string Apellido
        {
            get { return txtApellidos.Text; }
            set { txtApellidos.Text = value; }
        }

        public string Rol
        {
            get { return cboRol.Text; }
            set { cboRol.Text = value; }
        }
        public bool esApoyo
        {
            get; set;
        }
        public frmDatosPersonal()
        {
            InitializeComponent();
            this.MaximizeBox = false; // Desactivar el botón de maximizar
            cargarCBO();
        }

        private void cargarCBO()
        {
            cboRol.Items.Add("Apoyo");
            cboRol.Items.Add("728");
        }
        public string CargoInverso(bool esApoyo)
        {
            return esApoyo ? "Apoyo" : "728";
        }
        private void frmDatosPersonal_Load(object sender, EventArgs e)
        {
            btnAccion.Text = textoBoton;

            if (btnAccion.Text == "Guardar")
            {
                txtNombre.Enabled = false;
                txtApellidos.Enabled = false;
            }
        }

        private bool Cargo(ComboBox cbo)
        {
            if (cbo.Text == "Apoyo") { return true; }
            else return false;
        }

        private void btnAccion_Click(object sender, EventArgs e)
        {
            // 1. Obtén los valores de los controles (TextBox, etc.)
            string nombres = txtNombre.Text.Trim();
            string apellidos = txtApellidos.Text.Trim();
            bool esApoyo = Cargo(cboRol);

            // Obtener el nombre completo del empleado desde los TextBox
            string nombreCompleto = txtNombre.Text.Trim() + " " + txtApellidos.Text.Trim();

            // 1. Obtener los valores del formulario
            int empleadoId = logEmleados.Instancia.ObtenerEmpleadoIdPorNombre(nombreCompleto); // Implementa esta función

            if (btnAccion.Text == "Agregar")
            {
                try
                {
                    // 2. Validaciones (¡IMPORTANTE!)
                    if (string.IsNullOrWhiteSpace(nombres) || string.IsNullOrWhiteSpace(apellidos))
                    {
                        MessageBox.Show("Por favor, ingrese los nombres y apellidos.");
                        return; // Detener el proceso si falta información
                    }

                    // 3. Usar logEmpleados para insertar (lógica de negocio)
                    logEmleados.Instancia.InsertarEmpleado(nombres, apellidos, esApoyo);

                    // 4. Mensaje de éxito y limpiar controles
                    MessageBox.Show("Empleado agregado correctamente.");
                    txtApellidos.Clear();
                    txtApellidos.Clear();
                    cboRol.SelectedIndex = -1;

                    InstanciFrmE.seDebeActualizar = true;
                    InstanciFrmE.CargarUserControlsEmpleados();
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones: mostrar mensaje amigable al usuario
                    MessageBox.Show("Ya hay un regitro con datos igual en la base de datos, por favor, verifique la información");
                }
            }
            else if (btnAccion.Text == "Guardar")
            {
                try
                {

                    // 3. Usar logEmpleados para modificar (lógica de negocio)
                    logEmleados.Instancia.ModificarEmpleado(empleadoId, esApoyo);

                    // 4. Mensaje de éxito y limpiar controles
                    MessageBox.Show("Empleado modificado correctamente.");

                    InstanciFrmE.seDebeActualizar = true;
                    InstanciFrmE.CargarUserControlsEmpleados();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al modificar el empleado: " + ex.Message);
                }
            }
        }

        private void frmDatosPersonal_FormClosing(object sender, FormClosingEventArgs e)
        {
            InstanciFrmE.EstadoBloqueado(true);
        }
    }
}