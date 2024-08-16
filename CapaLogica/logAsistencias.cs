using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class logAsistencias
    {
        private static readonly logAsistencias _instancia = new logAsistencias();

        public static logAsistencias Instancia
        {
            get { return logAsistencias._instancia; }
        }

        // Método para insertar una asistencia (usa datGenerarAsistencia)
        public void InsertarAsistencia(int idEmpleado, DateTime fechaAsistencia, bool asistio)
        {
            try
            {
                datGenerarAsistencia.Instancia.InsertarAsistencia(idEmpleado, fechaAsistencia, asistio);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones (registrar en log, notificar al usuario, etc.)
                throw new Exception("Error al insertar asistencia: " + ex.Message);
            }
        }

       
        public bool ValidarAsistenciasPorFecha(DateTime fechaAsistencia)
        {
            try
            {
                return datGenerarAsistencia.Instancia.ValidarQueYaHayanAsistenciasEnElDía(fechaAsistencia);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones (registrar en log, notificar al usuario, etc.)
                throw new Exception("Error al validar asistencias: " + ex.Message);
            }
        }
        public void MarcarTodasAsistenciasDelDia(DataTable Asistencias, DateTime fecha)
        {
            try
            {
                datGenerarAsistencia.Instancia.MarcarTodasAsistenciasDelDia(Asistencias, fecha);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones (registrar en log, notificar al usuario, etc.)
                throw new Exception("Error al marcar todas las asistencias del día: " + ex.Message);
            }
        }

        public void MarcarAsistencia(int idAsistencia)
        {
            datGenerarAsistencia.Instancia.MarcarAsistencia(idAsistencia);
        }

        // Método para listar asistencias por fecha desde la capa lógica
        public DataTable ListarAsistenciasPorFecha(DateTime fecha)
        {
            try
            {
                return datGenerarAsistencia.Instancia.ListarAsistenciasPorFecha(fecha);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener asistencias por fecha", ex);
            }
        }

        // Método para listar asistencias por mes, año y empleado
        public DataTable ListarAsistenciasPorMesAñoEmpleado(int idEmpleado, int mes, int año)
        {
            // Calcular el primer y último día del mes
            DateTime primerDiaDelMes = new DateTime(año, mes, 1);
            DateTime ultimoDiaDelMes = primerDiaDelMes.AddMonths(1).AddDays(-1);

            // Llamar al método de la capa de datos
            return datGenerarAsistencia.Instancia.ListarAsistenciasPorMesAñoEmpleado(idEmpleado, primerDiaDelMes, ultimoDiaDelMes);
        }

        // Método para validar asistencias por fecha
        public bool ValidarAsistenciasPorFechaMes(DateTime fechaAsistencia)
        {
            try
            {
                return datGenerarAsistencia.Instancia.ValidarQueYaHayanAsistenciasEnElDía(fechaAsistencia);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones (registrar en log, notificar al usuario, etc.)
                throw new Exception("Error al validar asistencias: " + ex.Message);
            }
        }
    }
}
