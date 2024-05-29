using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using MaterialSkin;
using CapaPresentación.Formularios;
using System.IO;
using System.Net;
using CapaLogica;

namespace CapaPresentacion
{
    public partial class UserControlTarget : UserControl
    {
        public UserControlTarget()
        {
            InitializeComponent();

        }
        private bool frmImagenAbierto = false;
        private frmImagen frmImagenInstancia;
        private bool frmMapaAbierto = false;
        private frmMapa frmMapaInstancia;
        // Propiedad para el nombre de la localidad
        public string NombreLocalidad
        {
            get => txtNombre.Text;
            set => txtNombre.Text = value;
        }

        // Propiedad para la dirección de la localidad
        public string Direccion
        {
            get => txtDireccion.Text;
            set => txtDireccion.Text = value;
        }

        // Método para establecer los datos de la localidad
        public void SetLocalidadData(string nombre, string direccion, string imageUrl) 
        {
            NombreLocalidad = nombre;
            Direccion = direccion;
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


        private void materialFloatingActionButton1_Click(object sender, EventArgs e)
        {
            // Intentar abrir el formulario frmMapa usando la función general
            if (FormUtil.TryOpenForm(() =>
            {
                var frmMapa = new frmMapa();
                frmMapa.textoBoton = "Guardar";

                // Obtener datos de la localidad usando la capa de lógica
                DataTable dtDetallesLocalidad = logLocalidades.ObtenerDetallesLocalidadPorNombreYDireccion(txtNombre.Text, txtDireccion.Text);
                if (dtDetallesLocalidad.Rows.Count > 0)
                {
                    DataRow row = dtDetallesLocalidad.Rows[0];

                    frmMapa.Nombre_Localidad = txtNombre.Text;
                    frmMapa.Descripcion = row["Descripcion"] != DBNull.Value ? row["Descripcion"].ToString() : string.Empty;
                    frmMapa.Direccion = row["Direccion"] != DBNull.Value ? row["Direccion"].ToString() : string.Empty;
                    frmMapa.Referencias = row["Referencias"] != DBNull.Value ? row["Referencias"].ToString() : string.Empty;
                    frmMapa.Urbanizacion = row["Urbanizacion"] != DBNull.Value ? row["Urbanizacion"].ToString() : string.Empty;
                    frmMapa.Sector = row["Sector"] != DBNull.Value ? row["Sector"].ToString() : string.Empty;
                    frmMapa.Latitud = row["Latitud"] != DBNull.Value ? Convert.ToDecimal(row["Latitud"]) : 0;
                    frmMapa.Longitud = row["Longitud"] != DBNull.Value ? Convert.ToDecimal(row["Longitud"]) : 0;
                    frmMapa.ImageUrl = row["url_Localidad"] != DBNull.Value ? row["url_Localidad"].ToString() : string.Empty;

                    if (frmMapa.Latitud != 0 && frmMapa.Longitud != 0)
                    {
                        frmMapa.LatInicial = (double)frmMapa.Latitud;
                        frmMapa.LngInicial = (double)frmMapa.Longitud;
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron detalles para la localidad especificada.");
                }

                frmMapa.StartPosition = FormStartPosition.CenterScreen;
                return frmMapa;
            }))
            {
                // El formulario se abrió exitosamente
                frmMapaAbierto = true;
            }
        }

        private void materialFloatingActionButton2_Click(object sender, EventArgs e)
        {
            // Intentar abrir el formulario frmImagen usando la función general
            if (FormUtil.TryOpenForm(() =>
            {
                var frmImagenInstancia = new frmImagen();

                // Obtener datos de la localidad usando la capa de lógica
                DataTable dtDetallesLocalidad = logLocalidades.ObtenerDetallesLocalidadPorNombreYDireccion(txtNombre.Text, txtDireccion.Text);
                if (dtDetallesLocalidad.Rows.Count > 0)
                {
                    DataRow row = dtDetallesLocalidad.Rows[0];

                    frmImagenInstancia.ImageUrl = row["url_Localidad"] != DBNull.Value ? row["url_Localidad"].ToString() : string.Empty;
                }
                else
                {
                    MessageBox.Show("No se encontraron detalles para la localidad especificada.");
                }

                // Asignar el evento FormClosing
                frmImagenInstancia.FormClosing += (senderForm, eFormClosing) =>
                {
                    // Cuando el formulario se está cerrando, obtener el URL de la imagen actualizado
                    string urlActualizado = frmImagenInstancia.ImageUrl;

                    // Obtener los IDs de detalle de localidad y de localidad
                    (int idLocalidad, int idDetalleLocalidad) = logLocalidades.Instancia.ObtenerId(txtNombre.Text, txtDireccion.Text);

                    // Actualizar la URL de la imagen en la base de datos
                    logLocalidades.Instancia.ActualizarUrlImagen(idDetalleLocalidad, idLocalidad, urlActualizado);
                };

                frmImagenInstancia.StartPosition = FormStartPosition.CenterScreen;
                return frmImagenInstancia;
            }))
            {
                // El formulario se abrió exitosamente
                frmImagenAbierto = true;
            }
        }


        private void materialFloatingActionButton3_Click(object sender, EventArgs e)
        {
            // Obtener el nombre de la localidad y la dirección de donde sea que los obtengas
            string nombreLocalidad = txtNombre.Text; // Debes proporcionar la implementación de esta función
            string direccion = txtDireccion.Text; // Debes proporcionar la implementación de esta función

            // Llamar al método para obtener ambos IDs
            var ids = logLocalidades.Instancia.ObtenerId(nombreLocalidad, direccion);

            // Acceder a los IDs obtenidos
            int idLocalidad = ids.Item1;
            int idDetalleLocalidad = ids.Item2;

            // Hacer lo que necesites con los IDs obtenidos
            if (idLocalidad != -1 && idDetalleLocalidad != -1)
            {
                logLocalidades.Instancia.EliminarLocalidadYDetalle(idLocalidad, idDetalleLocalidad);
                MessageBox.Show("ELIMINICACIÓN COMPLETA");
            }
            else
            {
                MessageBox.Show("La localidad no se encuentra en la base de datos.");
            }
        }

    }
}

