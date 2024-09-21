using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;

namespace CapaLogica
{
    public class logEmleados
    {
        private static readonly logEmleados _instancia = new logEmleados();
        public static logEmleados Instancia
        {
            get { return logEmleados._instancia; }
        }

        public void ActualizarEstadoEsPersonal(int idEmpleado)
        {
            try
            {
                // Llamar al método de la capa de datos para actualizar el estado
                datEmpleados.Instancia.ActualizarEstadoEsPersonal(idEmpleado);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones (registrar en log, notificar al usuario, etc.)
                throw new Exception("Error al actualizar el estado de esPersonalLimpieza en la capa lógica: " + ex.Message);
            }
        }


        // Método para insertar un empleado (usa datEmpleados)
        public void InsertarEmpleado(string nombres, string apellidos, bool esApoyo, string direccionCorreo, string urlFoto, string DNI, bool? esPersonalLimpieza)
        {
            try
            {
                datEmpleados.Instancia.InsertarEmpleado(nombres, apellidos, esApoyo, direccionCorreo, urlFoto, DNI, esPersonalLimpieza);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones (registrar en log, notificar al usuario, etc.)
                throw new Exception("Error al insertar el empleado: " + ex.Message);
            }
        }


        public DataTable ObtenerTodosLosEmpleados()
        {
            try
            {
                return datEmpleados.Instancia.ObtenerTodosLosEmpleados();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la capa de lógica al obtener los empleados: " + ex.Message);
            }
        }
        public int ObtenerEmpleadoIdPorNombre(string nombreCompleto)
        {
            try
            {
                return datEmpleados.Instancia.ObtenerEmpleadoIdPorNombre(nombreCompleto);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la capa de lógica al obtener el ID del empleado: " + ex.Message);
            }
        }

        public string ObtenerNombrePorID(int id)
        {
            try
            {
                return datEmpleados.Instancia.ObtenerNombreCompletoPorEmpleadoId(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la capa de lógica al obtener el ID del empleado: " + ex.Message);
            }
        }

        public DataTable ObtenerDetallesEmpleadoPorId(int empleadoId)
        {
            try
            {
                return datEmpleados.Instancia.ObtenerDetallesEmpleadoPorId(empleadoId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la capa de lógica al obtener detalles del empleado: " + ex.Message);
            }
        }
        public void ModificarEmpleado(int empleadoId, bool esApoyo, string direccionCorreo, string urlFoto, string DNI, DateTime fechaNacimiento)
        {
            try
            {
                datEmpleados.Instancia.ModificarEmpleado(empleadoId, esApoyo, direccionCorreo, urlFoto, DNI, fechaNacimiento);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la capa de lógica al modificar el empleado: " + ex.Message);
            }
        }
        public bool EliminarEmpleado(int empleadoId, out string mensajeError)
        {
            return datEmpleados.Instancia.EliminarEmpleado(empleadoId, out mensajeError);
        }
        public DataTable ObtenerEmpleadosFiltrados(string filtro, string categoria)
        {
            try
            {
                return datEmpleados.Instancia.ObtenerEmpleadosFiltrados(filtro, categoria);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la capa de lógica al obtener empleados filtrados: " + ex.Message);
            }
        }
        public DataTable ObtenerEmpleadosPersonalYParques(string filtro)
        {
            try
            {
                return datEmpleados.Instancia.ObtenerEmpleadosPersonalYParques(filtro);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones (puedes registrar en un log, mostrar mensajes, etc.)
                throw new Exception("Error al obtener los empleados y parques: " + ex.Message);
            }
        }
        public DataTable ListarEmpleadosQueSeanLimpieza()
        {
            try
            {
                return datEmpleados.Instancia.ListarEmpleadosQueSeanLimpieza();
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception("Error al listar empleados que son personal de limpieza: " + ex.Message);
            }
        }

        public DataTable datObtenerPersonalLimpiezaParaComboBox()
        {
            try
            {
                return datEmpleados.Instancia.datObtenerPersonalLimpiezaParaComboBox();
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception("Error al listar empleados que son personal de limpieza: " + ex.Message);
            }
        }

       
        public DataTable ObtenerPlacas()
        {
            try
            {
                return datEmpleados.Instancia.ObtenerPlacas();  // Llama al método de la capa de datos
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las placas: " + ex.Message);
            }
        }

        // Método para obtener los empleados que no son parte del personal compacta
        public DataTable ListarEmpleadosQueSeanComapacta()
        {
            try
            {
                return datEmpleados.Instancia.datObtenerPersonalCompacta();
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception("Error al listar empleados que son personal de limpieza: " + ex.Message);
            }
        }

        public int InsertarDetallePersonalCompacta(int idEmpleado, int idCompacta, bool esConductor, string zona, string turno)
        {
            try
            {
               return  datEmpleados.Instancia.InsertarDetallePersonalCompacta(idEmpleado, idCompacta, esConductor, zona, turno);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el detalle del personal compacta en la capa lógica: " + ex.Message);
            }
        }


        // Método para insertar una visita compacta
        public void InsertarVisitaCompacta(int id_DetallePersonalCompacta, DateTime fechaVisita, bool completada)
        {
            try
            {
                // Llamar al método de la capa de datos para realizar la inserción
                datEmpleados.Instancia.InsertarVisitaCompacta(id_DetallePersonalCompacta, fechaVisita, completada);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, registrar en un log, notificar al usuario, etc.
                throw new Exception("Error al insertar la visita compacta en la capa lógica: " + ex.Message);
            }
        }

        public int ObtenerIDCompactaPorPlaca(string placaVehicular)
        {
            try
            {
                return datEmpleados.Instancia.ObtenerIDCompactaPorPlaca(placaVehicular);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el ID de la compacta en la capa lógica: " + ex.Message);
            }
        }

        public DataTable ObtenerVisitasCompactasDeHoy()
        {
            try
            {
                return datEmpleados.Instancia.ObtenerVisitasCompactasDeHoy();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las visitas compactas del día en la capa lógica: " + ex.Message);
            }
        }

        public int ObtenerIDVisitaCompacta(string placaVehicular, string nombreEmpleado, string zona, bool esConductor, DateTime fechaVisita)
        {
            try
            {
                // Llamar al método de la capa de datos para obtener el ID de la visita compacta
                return datEmpleados.Instancia.ObtenerIDVisitaCompacta(placaVehicular, nombreEmpleado, zona, esConductor, fechaVisita);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, registrar en un log o lanzar la excepción a la capa superior
                throw new Exception("Error al obtener el ID de la visita compacta en la capa lógica: " + ex.Message);
            }
        }


    }

}

