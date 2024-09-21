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

        public DataTable ObtenerOpciones()
        {
            DataTable dtOpciones = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ID_Opcion, Nombre_Opcion FROM Opcion";

                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dtOpciones);
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al cargar las opciones desde la base de datos: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            return dtOpciones;
        }


        public  DataTable ObtenerSectoresTurnos()
        {
            DataTable dtSectoresTurnos = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ID, Sector, Turno FROM SectoresTurnos";

                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dtSectoresTurnos);
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al cargar los sectores y turnos desde la base de datos: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            return dtSectoresTurnos;
        }

        public void InsertarAsistencia(int idEmpleado, DateTime fechaAsistencia, int idOpcion, int idSector)
        {
            // Asignar el valor predeterminado de 2 si idOpcion es 0 o no válido
            int opcionFinal = idOpcion == 0 ? 2 : idOpcion;

            // Asignar el valor predeterminado de 48 si idSector es 0 o no válido
            int sectorFinal = idSector == 0 ? 48 : idSector;

            string formattedDate = fechaAsistencia.ToString("yyyy-MM-dd");
            DateTime parsedDate = DateTime.ParseExact(formattedDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Asistencias (ID_Empleado, Fecha_Asistencia, ID_Opcion, ID_SectorTurno)
                         VALUES (@ID_Empleado, @FechaAsistencia, @ID_Opcion, @ID_SectorTurno)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID_Empleado", idEmpleado);
                command.Parameters.AddWithValue("@FechaAsistencia", parsedDate.Date);
                command.Parameters.AddWithValue("@ID_Opcion", opcionFinal);
                command.Parameters.AddWithValue("@ID_SectorTurno", sectorFinal);

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
                string query = @"SELECT a.Fecha_Asistencia, a.Asistio, a.ID_Asistencia, o.Nombre_Opcion 
                         FROM Asistencias a
                         LEFT JOIN Opcion o ON a.ID_Opcion = o.ID_Opcion
                         WHERE a.ID_Empleado = @ID_Empleado";

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

        public int ObtenerIdOpcionPorNombreOpcion(string nombreOpcion)
        {
            int idOpcion = -1; // Valor predeterminado para indicar que no se encontró el ID

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ID_Opcion FROM Opcion WHERE Nombre_Opcion = @NombreOpcion";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NombreOpcion", nombreOpcion);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        idOpcion = Convert.ToInt32(result);
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al obtener el ID de la opción: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            return idOpcion;
        }

        public int ObtenerIdSectorPorNombreSector(string nombreSector)
        {
            int idSector = -1; // Valor predeterminado para indicar que no se encontró el ID

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ID FROM SectoresTurnos WHERE Sector = @NombreSector";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NombreSector", nombreSector);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        idSector = Convert.ToInt32(result);
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al obtener el ID del sector: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            return idSector;
        }

        // Método para marcar todas las asistencias de un día específico
        public void MarcarTodasAsistenciasDelDia(DataTable dtAsistencias, DateTime fecha)
        {
            string query = @"
        UPDATE Asistencias
        SET ID_Opcion = @ID_Opcion, ID_SectorTurno = @ID_SectorTurno
        WHERE Fecha_Asistencia = @Fecha AND ID_Asistencia = @ID_Asistencia";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                foreach (DataRow row in dtAsistencias.Rows)
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Obtener los valores necesarios del DataTable
                        int idAsistencia = Convert.ToInt32(row["ID_Asistencia"]);
                        int idOpcion = row["Opción"] != DBNull.Value ? Convert.ToInt32(row["Opción"]) : 2;
                        int idSectorTurno = row["Sector"] != DBNull.Value ? Convert.ToInt32(row["Sector"]) : 48;

                        // Agregar parámetros
                        cmd.Parameters.AddWithValue("@Fecha", fecha.Date);
                        cmd.Parameters.AddWithValue("@ID_Asistencia", idAsistencia);
                        cmd.Parameters.AddWithValue("@ID_Opcion", idOpcion);
                        cmd.Parameters.AddWithValue("@ID_SectorTurno", idSectorTurno);

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
            DataTable dtAsistencias = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
            SELECT 
                e.Nombres, 
                e.Apellidos, 
                o.Nombre_Opcion AS Opcion, 
                s.Sector AS Sector, 
                a.Fecha_Asistencia,
                a.ID_Asistencia
            FROM 
                Asistencias a
            INNER JOIN 
                Empleados e ON a.ID_Empleado = e.ID_Empleado
            LEFT JOIN 
                Opcion o ON a.ID_Opcion = o.ID_Opcion
            LEFT JOIN 
                SectoresTurnos s ON a.ID_SectorTurno = s.ID
            WHERE 
                a.Fecha_Asistencia = @Fecha_Asistencia";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Fecha_Asistencia", fecha);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dtAsistencias);
                }

                // Filtrar el DataTable para eliminar filas sin ID_Asistencia
                var rowsWithAsistencia = dtAsistencias.AsEnumerable()
                                                      .Where(row => !row.IsNull("ID_Asistencia"));

                if (rowsWithAsistencia.Any())
                {
                    dtAsistencias = rowsWithAsistencia.CopyToDataTable();
                }
                else
                {
                    // Si no hay filas con ID_Asistencia, devolver un DataTable vacío con las mismas columnas
                    dtAsistencias = dtAsistencias.Clone();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar asistencias por fecha", ex);
            }

            return dtAsistencias;
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
            A.ID_Asistencia,
            A.Fecha_Asistencia,
            O.Nombre_Opcion AS Opcion,
            S.Sector AS Sector
        FROM 
            Empleados E
        LEFT JOIN 
            Asistencias A ON E.ID_Empleado = A.ID_Empleado 
            AND CAST(A.Fecha_Asistencia AS DATE) BETWEEN @PrimerDiaDelMes AND @UltimoDiaDelMes
        LEFT JOIN 
            Opcion O ON A.ID_Opcion = O.ID_Opcion
        LEFT JOIN 
            SectoresTurnos S ON A.ID_SectorTurno = S.ID
        WHERE 
            E.ID_Empleado = @IdEmpleado";

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
                    throw new Exception("Error al listar asistencias: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            // Filtrar el DataTable para eliminar filas sin ID_Asistencia
            var rowsWithAsistencia = dtAsistencias.AsEnumerable()
                                                  .Where(row => !row.IsNull("ID_Asistencia"));

            if (rowsWithAsistencia.Any())
            {
                dtAsistencias = rowsWithAsistencia.CopyToDataTable();
            }
            else
            {
                // Si no hay filas con ID_Asistencia, devolver un DataTable vacío con las mismas columnas
                dtAsistencias = dtAsistencias.Clone();
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

        public (int idSectorTurno, int idOpcion) ObtenerIdSectorYIdOpcionPorAsistenciaYEmpleado(int idAsistencia, int idEmpleado)
        {
            int idSectorTurno = -1;
            int idOpcion = -1;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                SELECT ID_SectorTurno, ID_Opcion 
                FROM Asistencias 
                WHERE ID_Asistencia = @ID_Asistencia AND ID_Empleado = @ID_Empleado";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID_Asistencia", idAsistencia);
                command.Parameters.AddWithValue("@ID_Empleado", idEmpleado);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            idSectorTurno = reader["ID_SectorTurno"] != DBNull.Value ? Convert.ToInt32(reader["ID_SectorTurno"]) : -1;
                            idOpcion = reader["ID_Opcion"] != DBNull.Value ? Convert.ToInt32(reader["ID_Opcion"]) : -1;
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al obtener el ID del sector y la opción: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            return (idSectorTurno, idOpcion);
        }

    }

}

