using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaLogica
{
    public class logLocalidades
    {
        private static readonly logLocalidades _instancia = new logLocalidades();
        public static logLocalidades Instancia
        {
            get { return logLocalidades._instancia; }
        }
        public List<string> ObtenerUrbanizaciones()
        {
            // Utiliza la instancia de datLocalidades para cargar las urbanizaciones
            return datLocalidades.Instancia.CargarUrbanizaciones();
        }

        public List<string> ObtenerSectores()
        {
            // Utiliza la instancia de datLocalidades para cargar las sectores
            return datLocalidades.Instancia.CargarSectores();
        }

        public DataTable ObtenerLocalidadesParaPanel()
        {
            return datLocalidades.ObtenerLocalidadesConDetalles();
        }
        public static DataTable ObtenerDetallesLocalidadPorNombreYDireccion(string nombreLocalidad, string direccion)
        {
            return datLocalidades.ObtenerDetallesLocalidadPorNombreYDireccion(nombreLocalidad, direccion);
        }
        // Nuevo método para insertar una localidad con sus detalles
        public void InsertarDetallesLocalidadesYLocalidades(string nombreLocalidad, string descripcion, string referencias, string urbanizacion, string sector, string direccion, decimal latitud, decimal longitud, string url)
        {
            // Insertar detalles de localidades y obtener el ID generado
            int idDetalleLocalidad = datLocalidades.InsertarDetallesLocalidades(descripcion, referencias, urbanizacion, sector, direccion, latitud, longitud, url); ;

            // Insertar una nueva entrada en la tabla Localidades
            datLocalidades.InsertarLocalidad(nombreLocalidad, idDetalleLocalidad);
        }
        public (int, int) ObtenerId(string nombreLocalidad, string direccion)
        {
            // Llama al método que devuelve ambos IDs
            (int idLocalidad, int idDetalleLocalidad) = datLocalidades.Instancia.ObtenerIdLocalidadYDetallePorNombreYDireccion(nombreLocalidad, direccion);

            return (idLocalidad, idDetalleLocalidad);
        }

        public void EliminarLocalidadYDetalle(int idLocalidad, int idDetalleLocalidad)
        {
            datLocalidades.Instancia.EliminarLocalidadYDetalle(idLocalidad, idDetalleLocalidad);
        }
        public void ActualizarDetallesLocalidades(int idDetalleLocalidad, string nombreLocalidad, string descripcion, string referencias, string urbanizacion, string sector, string direccion, decimal latitud, decimal longitud)
        {
            // Llama al método en la capa de datos para actualizar los detalles de localidades
            datLocalidades.ActualizarDetallesLocalidades(idDetalleLocalidad, nombreLocalidad, descripcion, referencias, urbanizacion, sector, direccion, latitud, longitud);
        }

        public void ActualizarUrlImagen(int idDetalleLocalidad, int idLocalidad, string url)
        {
            try
            {
                // Llama al método en la capa de datos para actualizar la URL de la imagen y el ID de detalle de localidad
                datLocalidades.ActualizarUrlImagen(idDetalleLocalidad, idLocalidad, url);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar URL de imagen: " + ex.Message);
            }
        }

    }
}
