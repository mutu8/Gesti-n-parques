using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class datVisitas
    {
        private static string connectionString = Conexion.Instancia.obtenerConexion();
        private static readonly datVisitas _instancia = new datVisitas();

        public static datVisitas Instancia
        {
            get { return datVisitas._instancia; }
        }

        // Método para obtener el ID del empleado por ID de la localidad
        private int? ObtenerIdEmpleadoPorIdLocalidad(int idLocalidad)
        {
            int? idEmpleado = null; // Valor por defecto en caso de no encontrar ningún resultado

            string query = @"
            SELECT dl.ID_Empleado 
            FROM Localidades l
            JOIN Detalles_Localidades dl ON l.ID_Detalle_Localidad = dl.ID_Detalle_Localidad
            WHERE l.ID_Localidad = @ID_Localidad";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_Localidad", idLocalidad);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            idEmpleado = Convert.ToInt32(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejar la excepción apropiadamente
                        throw new Exception($"Error al obtener el ID del empleado por ID de la localidad ({idLocalidad}): {ex.Message}", ex);
                    }
                }
            }

            return idEmpleado;
        }




        // Método para obtener el nombre completo del empleado por ID
        public string ObtenerNombreCompletoEmpleado(int idEmpleado)
        {
            string nombreCompleto = string.Empty;

            string query = "SELECT Nombres + ' ' + Apellidos AS NombreCompleto FROM Empleados WHERE ID_Empleado = @ID_Empleado";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_Empleado", idEmpleado);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            nombreCompleto = result.ToString();

                            Console.WriteLine(nombreCompleto);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejar la excepción apropiadamente (p. ej. registrándola, lanzándola nuevamente, mostrando un mensaje de error, etc.)
                        throw new Exception("Error al obtener el nombre completo del empleado", ex);
                    }
                }
            }

            return nombreCompleto;
        }

        // Método para obtener el nombre completo del empleado por ID de la localidad
        public string ObtenerNombreCompletoEmpleadoPorIdLocalidad(int idLocalidad)
        {
            try
            {
                int? idEmpleado = ObtenerIdEmpleadoPorIdLocalidad(idLocalidad);
                if (!idEmpleado.HasValue)
                {
                    return string.Empty; // Devolver un espacio en blanco si no hay empleado asignado
                }

                return ObtenerNombreCompletoEmpleado(idEmpleado.Value);
            }
            catch (Exception ex)
            {
                // Manejar la excepción apropiadamente
                throw new Exception($"Error al obtener el nombre completo del empleado por ID de la localidad ({idLocalidad}): {ex.Message}", ex);
            }
        }




        public DataTable ListarVisitas(int idLocalidad) // Recibe el ID de la localidad como parámetro
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                SELECT 
                    v.ID_Visita,
                    v.Fecha_Visita,
                    l.Nombre_Localidad,
                    CONCAT(e.Nombres, ' ', e.Apellidos) AS NombreEmpleado,
                    v.Estado,
                    v.Nota -- Añadido: Seleccionar la columna Nota
                FROM Visitas v
                INNER JOIN Detalles_Localidades dl ON v.ID_Localidad = dl.ID_Detalle_Localidad
                INNER JOIN Localidades l ON dl.ID_Detalle_Localidad = l.ID_Detalle_Localidad
                INNER JOIN Empleados e ON v.ID_Empleado = e.ID_Empleado
                WHERE v.ID_Localidad = @IDLocalidad"; // Filtrar por ID_Localidad

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Agregar el parámetro para la consulta
                        command.Parameters.AddWithValue("@IDLocalidad", idLocalidad);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones
                    // Puedes registrar el error o mostrar un mensaje
                    // Ejemplo: MessageBox.Show("Error al listar visitas: " + ex.Message);
                }
            }
            return dt;
        }


        public bool InsertarVisita(DateTime fechaVisita, bool estado, int idLocalidad, int idEmpleado, string nota)
        {
            // Verificar que idEmpleado sea válido (diferente de null y mayor que 0)
            if (idEmpleado <= 0)
            {
                Console.WriteLine("No se pudo insertar la visita: ID_Empleado inválido.");
                return false; // Indica que la inserción falló por ID_Empleado inválido
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
            INSERT INTO Visitas (Fecha_Visita, Estado, ID_Localidad, ID_Empleado, Nota)
            VALUES (@FechaVisita, @Estado, @IDLocalidad, @IDEmpleado, @Nota)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FechaVisita", fechaVisita);
                        command.Parameters.AddWithValue("@Estado", estado);
                        command.Parameters.AddWithValue("@IDLocalidad", idLocalidad);
                        command.Parameters.AddWithValue("@IDEmpleado", idEmpleado);
                        command.Parameters.AddWithValue("@Nota", string.IsNullOrEmpty(nota) ? (object)DBNull.Value : nota);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0; // Indica si la inserción fue exitosa
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Error SQL al insertar visita: " + sqlEx.Message);
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al insertar visita: " + ex.Message);
                    return false;
                }
            }
        }






        public bool ObtenerEstadoVisita(int idVisita)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                SELECT Estado
                FROM Visitas
                WHERE ID_Visita = @IDVisita";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IDVisita", idVisita);

                        object result = command.ExecuteScalar(); // Obtener el valor de la columna Estado

                        if (result != null && result != DBNull.Value)
                        {
                            return Convert.ToBoolean(result);
                        }
                        else
                        {
                            // Manejar el caso en que no se encuentra la visita o el estado es nulo
                            throw new Exception($"No se encontró la visita con ID: {idVisita} o su estado es nulo.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones (registrar el error, mostrar un mensaje, etc.)
                    Console.WriteLine("Error al obtener el estado de la visita: " + ex.Message);
                    throw; // Re-lanzar la excepción para que sea manejada en la capa superior
                }
            }
        }
        public bool ActualizarEstado(int idVisita, bool nuevoEstado)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                UPDATE Visitas 
                SET Estado = @NuevoEstado
                WHERE ID_Visita = @IDVisita";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NuevoEstado", nuevoEstado);
                        command.Parameters.AddWithValue("@IDVisita", idVisita);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0; // Indica si la actualización fue exitosa
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones (registrar el error, mostrar un mensaje, etc.)
                    Console.WriteLine("Error al actualizar estado de la visita: " + ex.Message);
                    return false; // Indica que la actualización falló
                }
            }
        }
        public DataTable ListarVisitasParaDgv(DateTime fechaActual) // Solo recibe la fecha actual
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                SELECT 
                    v.Fecha_Visita,
                    l.Nombre_Localidad,
                    CONCAT(e.Nombres, ' ', e.Apellidos) AS NombreEmpleado,
                    v.Estado,
                    v.Nota -- Incluir la nueva columna Nota
                FROM Visitas v
                INNER JOIN Detalles_Localidades dl ON v.ID_Localidad = dl.ID_Detalle_Localidad
                INNER JOIN Localidades l ON dl.ID_Detalle_Localidad = l.ID_Detalle_Localidad
                INNER JOIN Empleados e ON v.ID_Empleado = e.ID_Empleado
                WHERE v.Fecha_Visita = @FechaActual"; // Filtrar solo por fecha actual

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FechaActual", fechaActual);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones (registrar, mostrar mensaje, etc.)
                    throw; // Re-lanzar la excepción para que sea manejada en capas superiores
                }
            }
            return dt;
        }


        public List<object[]> ListarVisitasParDeFechaEspecifica(DateTime fecha)
        {
            List<object[]> visitas = new List<object[]>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
            SELECT 
                v.Fecha_Visita,
                l.Nombre_Localidad,
                CONCAT(e.Nombres, ' ', e.Apellidos) AS NombreEmpleado,
                v.Estado,
                v.Nota
            FROM Visitas v
            INNER JOIN Localidades l ON v.ID_Localidad = l.ID_Localidad
            INNER JOIN Detalles_Localidades dl ON l.ID_Detalle_Localidad = dl.ID_Detalle_Localidad
            INNER JOIN Empleados e ON v.ID_Empleado = e.ID_Empleado
            WHERE v.Fecha_Visita = @FechaActual";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FechaActual", fecha);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                object[] visita = new object[5];
                                visita[0] = reader.GetDateTime(0); // Fecha_Visita
                                visita[1] = reader.GetString(1);  // Nombre_Localidad
                                visita[2] = reader.GetString(2);  // NombreEmpleado
                                visita[3] = reader.GetBoolean(3); // Estado
                                visita[4] = reader.IsDBNull(4) ? null : reader.GetString(4); // Nota
                                visitas.Add(visita);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones
                    throw; // Re-lanzar la excepción para que sea manejada en capas superiores
                }
            }
            return visitas;
        }



        public DataTable ListarEmpleados()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                        SELECT ID_Empleado, CONCAT(Nombres, ' ', Apellidos) AS NombreCompleto
                        FROM Empleados";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones
                    throw;
                }
            }
            return dt;
        }

        public DataTable ObtenerLocalidadesConEmpleados()
        {
            DataTable dt = new DataTable();

            string query = @"
            SELECT l.ID_Localidad
            FROM Localidades l
            JOIN Detalles_Localidades dl ON l.ID_Detalle_Localidad = dl.ID_Detalle_Localidad
            WHERE dl.ID_Empleado IS NOT NULL";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error al obtener localidades con empleados: " + ex.Message, ex);
                    }
                }
            }

            return dt;
        }

        public DataTable ObtenerLocalidadesConEmpleados2()
        {
            DataTable dt = new DataTable();

            string query = @"
            SELECT l.ID_Localidad, l.Nombre_Localidad, 
                   ISNULL(CONCAT(e.Nombres, ' ', e.Apellidos), 'Sin Asignar') AS NombreEmpleado, 
                   ISNULL(e.ID_Empleado, 0) AS ID_Empleado
            FROM Localidades l
            LEFT JOIN Detalles_Localidades dl ON l.ID_Detalle_Localidad = dl.ID_Detalle_Localidad
            LEFT JOIN Empleados e ON dl.ID_Empleado = e.ID_Empleado";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error al obtener localidades con empleados: " + ex.Message, ex);
                    }
                }
            }

            return dt;
        }

        public bool GenerarVisitasParaLocalidades(DateTime fechaVisita, bool estado, int idLocalidad, int idEmpleado, string nota)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Insertar visita solo si no existe una activa para esta fecha y localidad
                    bool exito = InsertarVisita(fechaVisita, estado, idLocalidad, idEmpleado, nota);
                    return exito;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al generar visitas para todas las localidades: " + ex.Message);
                return false;
            }
        }




        // Método para verificar si ya existe una visita activa para una fecha, localidad e idEmpleado específicos
        public bool ExisteVisitaActivaParaFechaLocalidad(DateTime fechaVisita, int idLocalidad)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                    SELECT COUNT(*)
                    FROM Visitas
                    WHERE Fecha_Visita = @fechaVisita
                      AND ID_Localidad = @idLocalidad
                      AND Estado IN (0, 1)"; // Estado = 0 indica visitas completadas y Estado = 1 indica visitas activas

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@fechaVisita", fechaVisita);
                        command.Parameters.AddWithValue("@idLocalidad", idLocalidad);

                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones (registrar el error, mostrar un mensaje, etc.)
                    Console.WriteLine($"Error al verificar la existencia de visita activa para la fecha {fechaVisita} y " +
                                      $"localidad {idLocalidad}: {ex.Message}");
                    throw; // Re-lanzar la excepción para que sea manejada en la capa superior
                }
            }
        }
        public bool ExisteVisitaActivaParaFecha1parametro(DateTime fechaVisita)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                SELECT COUNT(*)
                FROM Visitas
                WHERE Fecha_Visita = @fechaVisita
                AND Estado IN (0, 1)"; // Estado = 0 indica visitas completadas y Estado = 1 indica visitas activas

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@fechaVisita", fechaVisita);

                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones (registrar el error, mostrar un mensaje, etc.)
                    Console.WriteLine($"Error al verificar la existencia de visitas activas para la fecha {fechaVisita}: {ex.Message}");
                    throw; // Re-lanzar la excepción para que sea manejada en la capa superior
                }
            }
        }

        // Método para obtener todas las visitas del día actual
        public DataTable ObtenerVisitasDelDia()
        {
            DataTable dtVisitas = new DataTable();

            string query = @"
            SELECT 
                v.ID_Visita, 
                v.Fecha_Visita, 
                v.Estado, 
                l.Nombre_Localidad, 
                CONCAT(e.Nombres, ' ', e.Apellidos) AS Nombre_Completo_Empleado
            FROM 
                Visitas v
            INNER JOIN 
                Localidades l ON v.ID_Localidad = l.ID_Localidad
            INNER JOIN 
                Detalles_Localidades dl ON l.ID_Detalle_Localidad = dl.ID_Detalle_Localidad
            INNER JOIN 
                Empleados e ON v.ID_Empleado = e.ID_Empleado
            WHERE 
                CONVERT(date, v.Fecha_Visita) = CONVERT(date, GETDATE())";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dtVisitas);
                    }
                    catch (Exception ex)
                    {
                        // Manejar la excepción apropiadamente
                        throw new Exception($"Error al obtener las visitas del día actual: {ex.Message}", ex);
                    }
                }
            }

            return dtVisitas;
        }

        public bool MarcarVisitasComoCompletadas(List<int> idsVisitas, bool completada)
        {
            bool exitoGlobal = true;

            foreach (int idVisita in idsVisitas)
            {
                bool exito = false;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        string query = @"
                    UPDATE Visitas
                    SET Estado = @Completada
                    WHERE ID_Visita = @ID_Visita";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Completada", completada);
                            command.Parameters.AddWithValue("@ID_Visita", idVisita);

                            int rowsAffected = command.ExecuteNonQuery();
                            exito = rowsAffected > 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejo de la excepción
                        Console.WriteLine($"Error al marcar la visita con ID {idVisita} como completada: {ex.Message}");
                        exito = false;
                    }
                }

                if (!exito)
                {
                    exitoGlobal = false;
                }
            }

            return exitoGlobal;
        }

        public bool NoHayVisitasPendientesDelDia()
        {
            bool noHayPendientes = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT COUNT(*) FROM Visitas WHERE Fecha_Visita = @Fecha AND Estado = 0";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Fecha", DateTime.Today);

                    connection.Open();
                    int count = (int)command.ExecuteScalar();

                    if (count == 0)
                    {
                        noHayPendientes = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return noHayPendientes;
        }
        public bool HayVisitasGeneradasHoy()
        {
            bool hayGeneradas = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                    SELECT COUNT(*) 
                    FROM Visitas v
                    INNER JOIN Localidades l ON v.ID_Localidad = l.ID_Localidad
                    INNER JOIN Detalles_Localidades dl ON l.ID_Detalle_Localidad = dl.ID_Detalle_Localidad
                    WHERE v.Fecha_Visita = @Fecha  AND Estado IN (0, 1)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Fecha", DateTime.Today);

                    connection.Open();
                    int count = (int)command.ExecuteScalar();

                    // Obtener el total de localidades
                    string queryTotalLocalidades = "SELECT COUNT(*) FROM Localidades";
                    SqlCommand commandTotalLocalidades = new SqlCommand(queryTotalLocalidades, connection);
                    int totalLocalidades = (int)commandTotalLocalidades.ExecuteScalar();

                    // Verificar si todas las localidades tienen visitas completadas hoy
                    if (count >= totalLocalidades)
                    {
                        hayGeneradas = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al verificar las visitas generadas hoy: " + ex.Message);
            }

            return hayGeneradas;
        }
        public List<object[]> ObtenerVisitasDeEmpleadoEspecifico(int idEmpleado, int mes, int anio)
        {
            List<object[]> visitas = new List<object[]>();

            // Ajusta la consulta para filtrar por mes y año
            string query = @"
    SELECT v.Fecha_Visita, 
           l.Nombre_Localidad, 
           e.Nombres + ' ' + e.Apellidos AS NombreEmpleado, 
           CASE WHEN v.Estado = 1 THEN 'Completada' ELSE 'No completada' END AS Estado, 
           v.Nota
    FROM Visitas v
    JOIN Localidades l ON v.ID_Localidad = l.ID_Localidad
    JOIN Empleados e ON v.ID_Empleado = e.ID_Empleado
    WHERE v.ID_Empleado = @ID_Empleado
    AND MONTH(v.Fecha_Visita) = @Mes
    AND YEAR(v.Fecha_Visita) = @Anio";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_Empleado", idEmpleado);
                    command.Parameters.AddWithValue("@Mes", mes);
                    command.Parameters.AddWithValue("@Anio", anio);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Extraer los datos y agregar al listado
                                object[] visita = new object[5];
                                visita[0] = reader["Fecha_Visita"];
                                visita[1] = reader["Nombre_Localidad"];
                                visita[2] = reader["NombreEmpleado"];
                                visita[3] = reader["Estado"];
                                visita[4] = reader["Nota"];

                                visitas.Add(visita);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejar la excepción apropiadamente
                        throw new Exception($"Error al obtener las visitas del empleado con ID ({idEmpleado}) para el mes {mes} y año {anio}: {ex.Message}", ex);
                    }
                }
            }

            return visitas;
        }

        public bool HayVisitasCompletadasEnFecha(DateTime fechaVisita)
        {
            bool hayVisitasCompletadas = false;

            string query = @"
            SELECT COUNT(*) 
            FROM Visitas 
            WHERE Fecha_Visita = @Fecha_Visita AND Estado = 1";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Fecha_Visita", fechaVisita.Date);

                    try
                    {
                        connection.Open();
                        int count = Convert.ToInt32(command.ExecuteScalar());
                        hayVisitasCompletadas = count > 0;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error al verificar si hay visitas completadas en la fecha", ex);
                    }
                }
            }

            return hayVisitasCompletadas;
        }

    }

}
