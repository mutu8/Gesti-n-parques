using System;
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
                    v.Estado
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
                    // Manejo de excepciones (puedes registrar el error, mostrar un mensaje, etc.)
                    // Por ejemplo: MessageBox.Show("Error al listar visitas: " + ex.Message);
                }
            }
            return dt;
        }

        public bool InsertarVisita(DateTime fechaVisita, bool estado, int idLocalidad, int idEmpleado)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                    INSERT INTO Visitas (Fecha_Visita, Estado, ID_Localidad, ID_Empleado)
                    VALUES (@FechaVisita, @Estado, @IDLocalidad, @IDEmpleado)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FechaVisita", fechaVisita);
                        command.Parameters.AddWithValue("@Estado", estado);
                        command.Parameters.AddWithValue("@IDLocalidad", idLocalidad);
                        command.Parameters.AddWithValue("@IDEmpleado", idEmpleado);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0; // Indica si la inserción fue exitosa
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones (registrar el error, mostrar un mensaje, etc.)
                    Console.WriteLine("Error al insertar visita: " + ex.Message);
                    return false; // Indica que la inserción falló
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
                    v.Estado
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




        public bool GenerarVisitasParaTodasLasLocalidadesConEmpleados(DateTime fechaVisita, bool estado)
        {
            DataTable dtLocalidades = ObtenerLocalidadesConEmpleados();
            bool exitoGlobal = true;

            foreach (DataRow row in dtLocalidades.Rows)
            {
                int idLocalidad = Convert.ToInt32(row["ID_Localidad"]);
                int? idEmpleado = ObtenerIdEmpleadoPorIdLocalidad(idLocalidad);

                // Solo intenta insertar visita si hay un idEmpleado válido
                if (idEmpleado.HasValue)
                {
                    // Verificar si ya existe una visita activa para esta fecha, localidad e idEmpleado
                    bool visitaActiva = ExisteVisitaActivaParaFechaLocalidadYEmpleado(fechaVisita, idLocalidad, idEmpleado.Value);

                    if (!visitaActiva)
                    {
                        bool exito = InsertarVisita(fechaVisita, estado, idLocalidad, idEmpleado.Value);
                        if (!exito)
                        {
                            exitoGlobal = false;
                        }
                    }
                    else
                    {
                        // Opcional: Manejar la situación de visita existente (registrando un log, por ejemplo)
                        Console.WriteLine($"Ya existe una visita activa para la localidad {idLocalidad} " +
                                          $"en la fecha {fechaVisita} para el empleado {idEmpleado.Value}");
                        // No modificar exitoGlobal en este caso
                    }
                }
                // Si idEmpleado es null, simplemente continúa con la siguiente localidad
            }

            return exitoGlobal;
        }

        // Método para verificar si ya existe una visita activa para una fecha, localidad e idEmpleado específicos
        private bool ExisteVisitaActivaParaFechaLocalidadYEmpleado(DateTime fechaVisita, int idLocalidad, int idEmpleado)
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
                  AND ID_Empleado = @idEmpleado
                  AND Estado = 1"; // Estado = 1 indica que la visita está activa

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@fechaVisita", fechaVisita);
                        command.Parameters.AddWithValue("@idLocalidad", idLocalidad);
                        command.Parameters.AddWithValue("@idEmpleado", idEmpleado);

                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones (registrar el error, mostrar un mensaje, etc.)
                    Console.WriteLine($"Error al verificar la existencia de visita activa para la fecha {fechaVisita}, " +
                                      $"localidad {idLocalidad} y empleado {idEmpleado}: {ex.Message}");
                    throw; // Re-lanzar la excepción para que sea manejada en la capa superior
                }
            }
        }
        
    }

}
