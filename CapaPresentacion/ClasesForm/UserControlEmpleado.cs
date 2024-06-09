using CapaLogica;
using CapaPresentación.Formularios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class UserControlEmpleado : UserControl
    {
        private bool frmImagenAbierto = false;
        private frmImagen frmImagenInstancia;
        private bool frmEmpleadoAbierto = false;
        private frmDatosPersonal frmDatosPersonalInstancia;


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

                    // 3. Asignar los valores a las propiedades del formulario
                    frmDetallesEmpleado.Nombre = row["Nombres"].ToString();
                    frmDetallesEmpleado.Apellido = row["Apellidos"].ToString();
                    frmDetallesEmpleado.Rol = frmDetallesEmpleado.CargoInverso((bool)row["esApoyo"]);
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

        private void btnImage_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
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
                    //InstanciFrmE.();
                }
                else
                {
                    MessageBox.Show(mensajeError); // Mostrar el mensaje de error específico
                }
            }

        }

    }
}
