using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
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

        public DataTable ObtenerTodasLasLocalidades()
        {
            try
            {
                return datLocalidades.Instancia.ObtenerTodasLasLocalidades();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la capa de lógica al obtener las localidades: " + ex.Message);
                Console.WriteLine(ex.Message);
            }
        }
        public string ObtenerNombreLocPorID(int idLocalidad)
        {
            // Utiliza la instancia de datLocalidades para cargar las sectores
            return datLocalidades.Instancia.ObtenerNombreLocalidadPorId(idLocalidad);
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
        public void InsertarDetallesLocalidadesYLocalidades(string nombreLocalidad, string descripcion, string referencias, string urbanizacion, string sector, string direccion, decimal latitud, decimal longitud, int idEmpleado, string url)
        {
            try
            {
                // Insertar detalles de localidades y obtener el ID generado
                int idDetalleLocalidad = datLocalidades.InsertarDetallesLocalidades(descripcion, referencias, urbanizacion, sector, direccion, latitud, longitud, idEmpleado, url); ;

                // Insertar una nueva entrada en la tabla Localidades
                datLocalidades.InsertarLocalidad(nombreLocalidad, idDetalleLocalidad);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones (registrar en log, notificar al usuario, etc.)
                throw new Exception("Error" + ex);
                // También puedes lanzar una excepción aquí si necesitas un manejo más específico o informar al usuario de manera diferente
            }
        }

        public (int, int) ObtenerId(string nombreLocalidad, string direccion)
        {
            // Llama al método que devuelve ambos IDs
            (int idLocalidad, int idDetalleLocalidad) = datLocalidades.Instancia.ObtenerIdLocalidadYDetallePorNombreYDireccion(nombreLocalidad, direccion);

            return (idLocalidad, idDetalleLocalidad);
        }
        public int ObtenerIdLocalidad(string nombreLocalidad, string direccion)
        {
            // Llama al método que devuelve el ID de la localidad
            int idLocalidad = datLocalidades.Instancia.ObtenerIdLocalidad(nombreLocalidad, direccion);

            return idLocalidad; // Devuelve solo el idLocalidad
        }
        public void EliminarLocalidadYDetalle(int idLocalidad, int idDetalleLocalidad)
        {
            datLocalidades.Instancia.EliminarLocalidadYDetalle(idLocalidad, idDetalleLocalidad);
        }
        public void ActualizarDetallesLocalidades(int idDetalleLocalidad, string nombreLocalidad, string descripcion, string referencias, string urbanizacion, string sector, string direccion, decimal latitud, decimal longitud, int idEmpleado)
        {
            // Llama al método en la capa de datos para actualizar los detalles de localidades
            datLocalidades.ActualizarDetallesLocalidades(idDetalleLocalidad, nombreLocalidad, descripcion, referencias, urbanizacion, sector, direccion, latitud, longitud, idEmpleado);
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

        public bool tieneVisitasPendientes(int idLocalidad)
        {
            return datLocalidades.Instancia.TieneVisitasPendientes(idLocalidad);
        }

    }
}
