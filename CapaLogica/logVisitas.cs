using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;

namespace CapaLogica
{
    public class logVisitas
    {
        private static readonly logVisitas _instancia = new logVisitas();
        public static logVisitas Instancia
        {
            get { return logVisitas._instancia; }
        }
        public DataTable ListarVisitas(int idLocalidad)
        {
            // Utiliza la instancia de datVisitas para obtener los datos, pasando el ID de la localidad
            return datVisitas.Instancia.ListarVisitas(idLocalidad);
        }
        public bool InsertarVisita(DateTime fechaVisita, bool estado, int idLocalidad, int idEmpleado, string nota)
        {
            return datVisitas.Instancia.InsertarVisita(fechaVisita, estado, idLocalidad, idEmpleado, nota);
        }
        public bool ActualizarEstadoVisita(int idVisita, bool nuevoEstado)
        {
            return datVisitas.Instancia.ActualizarEstado(idVisita, nuevoEstado);
        }
        public bool ObtenerEstadoVisita(int idVisita)
        {
            return datVisitas.Instancia.ObtenerEstadoVisita(idVisita);
        }

        public string ObtenerNombreCompletoEmpleadoPorIdLocalidad(int idLocalidad)
        {
            try
            {
                return datVisitas.Instancia.ObtenerNombreCompletoEmpleadoPorIdLocalidad(idLocalidad);
            }
            catch (Exception ex)
            {
                // Manejar la excepción apropiadamente (p. ej. registrándola, lanzándola nuevamente, mostrando un mensaje de error, etc.)
                throw new Exception("Error al obtener el nombre completo del empleado por ID de la localidad desde logVisitas", ex);
            }
        }
        public DataTable ListarVisitasParaDgv(DateTime fechaActual)
        {
            return datVisitas.Instancia.ListarVisitasParaDgv(fechaActual);
        }

        public List<object[]> ListarVisitasParDeFechaEspecificav(DateTime fecha)
        {
            return datVisitas.Instancia.ListarVisitasParDeFechaEspecifica(fecha);
        }



        public DataTable ListarEmpleados()
        {
            return datVisitas.Instancia.ListarEmpleados();
        }
        public DataTable ObtenerLocalidadesConEmpleados()
        {
            return datVisitas.Instancia.ObtenerLocalidadesConEmpleados();
        }
        public DataTable ObtenerLocalidadesConEmpleados2()
        {
            return datVisitas.Instancia.ObtenerLocalidadesConEmpleados2();
        }
        public bool GenerarVisitasParaLocalidades(DateTime fechaVisita, bool estado, int idLocalidad, int idEmpleado, string nota)
        {
            return datVisitas.Instancia.GenerarVisitasParaLocalidades(fechaVisita, estado, idLocalidad, idEmpleado, nota);
        }
        // Método para obtener todas las visitas del día actual
        public DataTable ObtenerVisitasDelDia()
        {
            DataTable dtVisitas = new DataTable();

            try
            {
                // Llamar al método correspondiente en datVisitas para obtener las visitas del día actual
                dtVisitas = datVisitas.Instancia.ObtenerVisitasDelDia();
            }
            catch (Exception ex)
            {
                // Manejar la excepción apropiadamente
                throw new Exception($"Error al obtener las visitas del día actual desde datVisitas: {ex.Message}", ex);
            }

            return dtVisitas;
        }
        public bool MarcarVisitasComoCompletadas(List<int> idsVisitas, bool completada)
        {
            // Llamar al método del datVisitas para marcar las visitas como completadas
            bool exito = datVisitas.Instancia.MarcarVisitasComoCompletadas(idsVisitas, completada);

            // Aquí podrías realizar acciones adicionales después de marcar las visitas, como registrar en un log o enviar notificaciones, etc.

            return exito;
        }
        public bool NoHayVisitasPendientesDelDia()
        {
            return datVisitas.Instancia.NoHayVisitasPendientesDelDia();
        }

        public bool NoHayVisitasEnElDía()
        {
            return datVisitas.Instancia.HayVisitasGeneradasHoy();
        }
        public bool ExisteVisitaActivaParaFechaLocalidadYEmpleado(DateTime fechaVisita, int idLocalidad) 
        {
            return datVisitas.Instancia.ExisteVisitaActivaParaFechaLocalidad( fechaVisita,  idLocalidad);
        }

        public bool ExisteVisitaActivaParaFecha1parametro(DateTime fechaVisita)
        {
            return datVisitas.Instancia.ExisteVisitaActivaParaFecha1parametro(fechaVisita);
        }

        // Método para listar visitas de un empleado específico filtradas por mes y año
        public List<object[]> ListarVisitasDeEmpleado(int idEmpleado, int mes, int anio)
        {
            // Utiliza la instancia de datVisitas para obtener los datos, pasando el ID del empleado, mes y año
            return datVisitas.Instancia.ObtenerVisitasDeEmpleadoEspecifico(idEmpleado, mes, anio);
        }
        public bool HayVisitasCompletadasEnFecha(DateTime fechaVisita)
        {
            return datVisitas.Instancia.HayVisitasCompletadasEnFecha(fechaVisita);
        }

    }
}
