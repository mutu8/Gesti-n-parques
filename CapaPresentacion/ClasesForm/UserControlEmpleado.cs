using CapaLogica;
using MaterialSkin.Controls;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class UserControlEmpleado : UserControl
    {
        private bool frmImagenAbierto = false;
        private frmImagen frmImagenInstancia;
        private bool frmEmpleadoAbierto = false;
        private frmDatosPersonal frmDatosPersonalInstancia;

        public bool BtnEditar
        {
            get { return btnSettings.Enabled; }
            set { btnSettings.Enabled = value; }
        }

        public frmPersonal InstanciFrmE;

        public string Nombre
        {
            get => txtNombre.Text;
            set => txtNombre.Text = value;
        }

        // Propiedad para la dirección de la localidad
        public string Rol
        {
            get => txtRol.Text;
            set => txtRol.Text = value;
        }
        public UserControlEmpleado(frmPersonal frm)
        {
            InitializeComponent();
            this.InstanciFrmE = frm;

            toolTip1.SetToolTip(materialFloatingActionButton2, "Generar reporte de asistencias por mes y año");
            toolTip1.SetToolTip(btbVer, "Visualizar información de personal");
            toolTip1.SetToolTip(btnSettings, "Modificar información de personal");
            toolTip1.SetToolTip(btnDelete, "Eliminar información de personal");

        }

        // Método para establecer los datos de la localidad
        public void SetdData(string nombre, string rol, string imageUrl)
        {
            Nombre = nombre;
            Rol = rol;
            CargarImagenDesdeUrl(imageUrl, pictureBox1);
        }

        private void CargarImagenDesdeUrl(string url, PictureBox pictureBox)
        {
            if (string.IsNullOrEmpty(url))
            {
                // Si la URL es null o vacía, no hacemos nada y mantenemos la imagen actual del PictureBox
                return;
            }

            using (WebClient clienteWeb = new WebClient())
            {
                try
                {
                    // Descargar la imagen como un arreglo de bytes
                    byte[] bytesImagen = clienteWeb.DownloadData(url);

                    // Convertir el arreglo de bytes en una imagen
                    using (MemoryStream stream = new MemoryStream(bytesImagen))
                    {
                        Image imagen = Image.FromStream(stream);

                        // Establecer la imagen en el PictureBox
                        pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox.Image = imagen;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar la imagen: " + ex.Message);
                }
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            using (PasswordForm reportesFechas = new PasswordForm())
            {
                reportesFechas.StartPosition = FormStartPosition.CenterScreen;

                // Mostrar el formulario como un cuadro de diálogo
                if (reportesFechas.ShowDialog() == DialogResult.OK)
                {
                    if (FormUtil.TryOpenForm(() =>
                    {
                        var frmDetallesEmpleado = new frmDatosPersonal();
                        frmDetallesEmpleado.InstanciFrmE = InstanciFrmE;
                        // 1. Obtener el ID del empleado seleccionado (asumiendo que tienes un control para seleccionarlo)
                        // Puedes usar un DataGridView, ListBox, etc. para obtener el ID
                        int empleadoId = logEmleados.Instancia.ObtenerEmpleadoIdPorNombre(Nombre); // Implementa esta función

                        // 2. Obtener los detalles del empleado usando logEmpleados
                        DataTable dtDetallesEmpleado = logEmleados.Instancia.ObtenerDetallesEmpleadoPorId(empleadoId);

                        if (dtDetallesEmpleado.Rows.Count > 0)
                        {
                            DataRow row = dtDetallesEmpleado.Rows[0];

                            frmDetallesEmpleado.idEmpleado = empleadoId;

                            // 3. Asignar los valores a las propiedades del formulario
                            frmDetallesEmpleado.Nombre = row["Nombres"].ToString();
                            frmDetallesEmpleado.Apellido = row["Apellidos"].ToString();
                            frmDetallesEmpleado.Rol = frmDetallesEmpleado.CargoInverso((bool)row["esApoyo"]);
                            frmDetallesEmpleado.Text = row["Nombres"].ToString() + " " + row["Apellidos"].ToString();

                            // Asignar nuevos campos
                            frmDetallesEmpleado.DireccionCorreo = row["DireccionCorreo"].ToString();
                            frmDetallesEmpleado.ImageUrl = row["urlFoto"].ToString();
                            frmDetallesEmpleado.DNI = row["DNI"].ToString();

                            // Obtener y parsear la fecha de nacimiento
                            if (DateTime.TryParse(row["FechaNacimiento"].ToString(), out DateTime fechaNacimiento))
                            {
                                frmDetallesEmpleado.FechaNacimiento = fechaNacimiento;
                            }

                            // Verificar si esPersonalLimpieza es true
                            bool esPersonalLimpieza = row["esPersonalLimpieza"] != DBNull.Value && (bool)row["esPersonalLimpieza"];
                            frmDetallesEmpleado.EsPersonalLimpieza = esPersonalLimpieza;

                            frmDetallesEmpleado.textoBoton = "Guardar";
                        }
                        else
                        {
                            MessageBox.Show("No se encontraron detalles para el empleado seleccionado.");
                        }

                        InstanciFrmE.EstadoBloqueado(false);
                        frmDetallesEmpleado.StartPosition = FormStartPosition.CenterScreen;
                        return frmDetallesEmpleado;
                    }))
                    {
                        // El formulario se abrió exitosamente
                    }
                }
            }
        }

        private void btnImage_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (PasswordForm reportesFechas = new PasswordForm())
            {
                reportesFechas.StartPosition = FormStartPosition.CenterScreen;

                // Mostrar el formulario como un cuadro de diálogo
                if (reportesFechas.ShowDialog() == DialogResult.OK)
                {
                    // Obtener el ID del empleado
                    int empleadoId = logEmleados.Instancia.ObtenerEmpleadoIdPorNombre(txtNombre.Text);

                    // Confirmar la eliminación con el usuario
                    DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar este empleado?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    // Verificar la respuesta del usuario
                    if (result == DialogResult.Yes)
                    {
                        // Realizar la eliminación del empleado
                        string mensajeError;
                        if (logEmleados.Instancia.EliminarEmpleado(empleadoId, out mensajeError))
                        {
                            MessageBox.Show("Empleado eliminado correctamente.");
                            InstanciFrmE.seDebeActualizar = true;

                            //Solo cargar is esPersonal es "Limpieza"
                            if (InstanciFrmE.getComboBox() == "Limpieza")
                            {
                                InstanciFrmE.CargarUserControlsEmpleados("", "Limpieza");

                            }
                            else
                            {
                                InstanciFrmE.CargarUserControlsEmpleados("", "Parques");
                            }

                        }
                        else
                        {
                            MessageBox.Show(mensajeError); // Mostrar el mensaje de error específico
                        }
                    }
                }
            } 
        }

        private void btbVer_Click(object sender, EventArgs e)
        {
            if (FormUtil.TryOpenForm(() =>
            {
                var frmDetallesEmpleado = new frmDatosPersonal();
                frmDetallesEmpleado.InstanciFrmE = InstanciFrmE;
                // 1. Obtener el ID del empleado seleccionado (asumiendo que tienes un control para seleccionarlo)
                // Puedes usar un DataGridView, ListBox, etc. para obtener el ID
                int empleadoId = logEmleados.Instancia.ObtenerEmpleadoIdPorNombre(Nombre); // Implementa esta función

                // 2. Obtener los detalles del empleado usando logEmpleados
                DataTable dtDetallesEmpleado = logEmleados.Instancia.ObtenerDetallesEmpleadoPorId(empleadoId);

                if (dtDetallesEmpleado.Rows.Count > 0)
                {
                    DataRow row = dtDetallesEmpleado.Rows[0];

                    frmDetallesEmpleado.idEmpleado = empleadoId;

                    // 3. Asignar los valores a las propiedades del formulario
                    frmDetallesEmpleado.Nombre = row["Nombres"].ToString();
                    frmDetallesEmpleado.Apellido = row["Apellidos"].ToString();
                    frmDetallesEmpleado.Rol = frmDetallesEmpleado.CargoInverso((bool)row["esApoyo"]);
                    frmDetallesEmpleado.Text = row["Nombres"].ToString() + " " + row["Apellidos"].ToString();

                    // Asignar nuevos campos
                    frmDetallesEmpleado.DireccionCorreo = row["DireccionCorreo"].ToString();
                    frmDetallesEmpleado.ImageUrl = row["urlFoto"].ToString();
                    frmDetallesEmpleado.DNI = row["DNI"].ToString();

                    // Obtener y parsear la fecha de nacimiento
                    if (DateTime.TryParse(row["FechaNacimiento"].ToString(), out DateTime fechaNacimiento))
                    {
                        frmDetallesEmpleado.FechaNacimiento = fechaNacimiento;
                    }

                    // Verificar si esPersonalLimpieza es true
                    bool esPersonalLimpieza = row["esPersonalLimpieza"] != DBNull.Value && (bool)row["esPersonalLimpieza"];
                    frmDetallesEmpleado.EsPersonalLimpieza = esPersonalLimpieza;

                    frmDetallesEmpleado.textoBoton = "";
                }
                else
                {
                    MessageBox.Show("No se encontraron detalles para el empleado seleccionado.");
                }

                InstanciFrmE.EstadoBloqueado(false);
                frmDetallesEmpleado.StartPosition = FormStartPosition.CenterScreen;
                return frmDetallesEmpleado;
            }))
            {
                // El formulario se abrió exitosamente
            }
        }

        private void materialFloatingActionButton2_Click(object sender, EventArgs e)
        {
            int id = logEmleados.Instancia.ObtenerEmpleadoIdPorNombre(txtNombre.Text);
            
            using (frmReportesFechas reportesFechas = new frmReportesFechas())
            {
                reportesFechas.StartPosition = FormStartPosition.CenterScreen;
                // Hacer visible el ComboBox (si el formulario ya está inicializado)
                reportesFechas.cbo.Visible = true;
                reportesFechas.DiD.Visible = true;
                reportesFechas.DatePicker.Visible = false;

                reportesFechas.idEmpleado = id;

                // Mostrar el formulario como un cuadro de diálogo
                if (reportesFechas.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }
    }
}
