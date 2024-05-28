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
        public DataTable ObtenerLocalidades()
        {
            return datLocalidades.ObtenerLocalidades();
        }

        // Nuevo método para obtener detalles de una localidad por su nombre
        public DataTable ObtenerDetallesLocalidadPorNombre(string nombreLocalidad)
        {
            return datLocalidades.ObtenerDetallesLocalidadPorNombre(nombreLocalidad);
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
        public void InsertarDetallesLocalidadesYLocalidades(string nombreLocalidad, string descripcion, string referencias, string urbanizacion, string manzana, string jiron, string direccion, decimal latitud, decimal longitud, string url)
        {
            // Insertar detalles de localidades y obtener el ID generado
            int idDetalleLocalidad = datLocalidades.InsertarDetallesLocalidades(descripcion, referencias, urbanizacion, manzana, jiron, direccion, latitud, longitud, url);

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


    }
}
