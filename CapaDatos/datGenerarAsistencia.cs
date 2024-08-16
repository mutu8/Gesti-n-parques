using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace CapaDatos
{
    public class datGenerarAsistencia
    {
        private static string connectionString = Conexion.Instancia.obtenerConexion();
        private static readonly datGenerarAsistencia _instancia = new datGenerarAsistencia();

        public static datGenerarAsistencia Instancia
        {
            get { return datGenerarAsistencia._instancia; }
        }

        public void InsertarAsistencia(int idEmpleado, DateTime fechaAsistencia, bool asistio)
        {

            // Formatear la fecha en el formato deseado
            string formattedDate = fechaAsistencia.ToString("yyyy-MM-dd");

            // Parsear la cadena formateada de vuelta a DateTime
            DateTime parsedDate = DateTime.ParseExact(formattedDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Asistencias (ID_Empleado, Fecha_Asistencia, Asistio)
                     VALUES (@ID_Empleado, @FechaAsistencia, @Asistio)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID_Empleado", idEmpleado);
                command.Parameters.AddWithValue("@FechaAsistencia", parsedDate.Date);
                command.Parameters.AddWithValue("@Asistio", asistio);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al insertar la asistencia en la base de datos: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public DataTable ListarAsistenciasPorEmpleado(int idEmpleado)
        {
            DataTable dtAsistencias = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT Fecha_Asistencia, Asistio, Nota, ID_Asistencia 
                     FROM Asistencias 
                     WHERE ID_Empleado = @ID_Empleado";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID_Empleado", idEmpleado);

                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dtAsistencias);
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al listar asistencias del empleado: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            return dtAsistencias;
        }

        public bool ValidarQueYaHayanAsistenciasEnElDía(DateTime fechaAsistencia)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT COUNT(*) FROM Asistencias 
                         WHERE CAST(Fecha_Asistencia AS DATE) = @FechaAsistencia";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FechaAsistencia", fechaAsistencia.Date);

                try
                {
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al validar la asistencia en la base de datos: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        // Método para marcar todas las asistencias de un día específico
        public void MarcarTodasAsistenciasDelDia(DataTable dtAsistencias, DateTime fecha)
        {
            string query = @"
            UPDATE Asistencias
            SET Asistio = 1
            WHERE Fecha_Asistencia = @Fecha AND ID_Asistencia = @ID_Asistencia";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                foreach (DataRow row in dtAsistencias.Rows)
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Obtener los valores necesarios del DataGridView
                        int idAsistencia = Convert.ToInt32(row["ID_Asistencia"]);

                        // Agregar parámetros
                        cmd.Parameters.AddWithValue("@Fecha", fecha.Date);
                        cmd.Parameters.AddWithValue("@ID_Asistencia", idAsistencia);

                        try
                        {
                            int rowsAffected = cmd.ExecuteNonQuery();
                            Console.WriteLine($"{rowsAffected} filas actualizadas para la asistencia ID {idAsistencia}.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error al actualizar la asistencia ID {idAsistencia}: {ex.Message}");
                        }
                    }
                }
            }


        }


        public void MarcarAsistencia(int idAsistencia)
        {
            string querySelect = @"
            SELECT Asistio
            FROM Asistencias
            WHERE ID_Asistencia = @ID_Asistencia";

                    string queryUpdate = @"
            UPDATE Asistencias
            SET Asistio = @Asistio
            WHERE ID_Asistencia = @ID_Asistencia";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Obtener el valor actual de Asistio
                    bool estadoActual;
                    using (SqlCommand cmdSelect = new SqlCommand(querySelect, conn))
                    {
                        cmdSelect.Parameters.AddWithValue("@ID_Asistencia", idAsistencia);

                        var result = cmdSelect.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            estadoActual = Convert.ToBoolean(result);
                        }
                        else
                        {
                            // Si no se encuentra el ID, no hacer nada
                            return;
                        }
                    }

                    // Determinar el valor opuesto
                    bool estadoOpuesto = !estadoActual;

                    // Convertir el booleano a entero (0 o 1)
                    int estadoAsistio = estadoOpuesto ? 1 : 0;

                    // Actualizar la base de datos con el valor opuesto
                    using (SqlCommand cmdUpdate = new SqlCommand(queryUpdate, conn))
                    {
                        cmdUpdate.Parameters.AddWithValue("@ID_Asistencia", idAsistencia);
                        cmdUpdate.Parameters.AddWithValue("@Asistio", estadoAsistio); // Usar el entero para el parámetro

                        int rowsAffected = cmdUpdate.ExecuteNonQuery();
                        Console.WriteLine($"{rowsAffected} filas actualizadas para la asistencia ID {idAsistencia}.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al actualizar la asistencia ID {idAsistencia}: {ex.Message}");
                }
            }
        }

        // Método para listar asistencias por fecha
        public DataTable ListarAsistenciasPorFecha(DateTime fecha)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Consulta para obtener los nombres y apellidos desde la tabla Empleados
                    string query = @"
                SELECT e.Nombres, e.Apellidos, a.Asistio 
                FROM Asistencias a
                INNER JOIN Empleados e ON a.ID_Empleado = e.ID_Empleado
                WHERE a.Fecha_Asistencia = @Fecha_Asistencia";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Fecha_Asistencia", fecha);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar asistencias por fecha", ex);
            }
        }

        public DataTable ListarAsistenciasPorMesAñoEmpleado(int idEmpleado, DateTime primerDiaDelMes, DateTime ultimoDiaDelMes)
        {
            DataTable dtAsistencias = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                SELECT 
                    E.ID_Empleado, 
                    E.Nombres, 
                    E.Apellidos, 
                    A.Asistio,
                    A.ID_Asistencia,
                    A.Fecha_Asistencia
                FROM 
                    Empleados E
                LEFT JOIN 
                    Asistencias A 
                ON 
                    E.ID_Empleado = A.ID_Empleado 
                    AND CAST(A.Fecha_Asistencia AS DATE) BETWEEN @PrimerDiaDelMes AND @UltimoDiaDelMes
                WHERE 
                    E.ID_Empleado = @IdEmpleado;
            ";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
                command.Parameters.AddWithValue("@PrimerDiaDelMes", primerDiaDelMes);
                command.Parameters.AddWithValue("@UltimoDiaDelMes", ultimoDiaDelMes);

                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dtAsistencias);
                }
                catch (SqlException ex)
                {
                    // Manejo de excepciones
                    throw new Exception("Error al listar asistencias: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            return dtAsistencias;
        }

        public bool ValidarSiHayAsistenciasEnElMes(int idEmpleado, DateTime primerDiaDelMes, DateTime ultimoDiaDelMes)
        {
            // Obtener las asistencias para el mes y año seleccionados
            DataTable dtAsistencias = ListarAsistenciasPorMesAñoEmpleado(idEmpleado, primerDiaDelMes, ultimoDiaDelMes);

            // Verificar si hay asistencias registradas para el mes
            return dtAsistencias.Rows.Count > 0;
        }


    }

}

