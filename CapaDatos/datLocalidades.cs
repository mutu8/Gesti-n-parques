using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class datLocalidades
    {
        #region Singleton
        private static string connectionString = Conexion.Instancia.obtenerConexion();
        private static readonly datLocalidades _instancia = new datLocalidades();

        public static datLocalidades Instancia
        {
            get { return datLocalidades._instancia; }
        }
        public List<string> CargarUrbanizaciones()
        {
            List<string> urbanizaciones = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Nombre_Urbanizacion FROM Urbanizaciones";
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string nombreUrbanizacion = reader["Nombre_Urbanizacion"].ToString();
                        urbanizaciones.Add(nombreUrbanizacion);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al cargar urbanizaciones: " + ex.Message);
                }
            }

            return urbanizaciones;
        }

        public List<string> CargarSectores()
        {
            List<string> sectores = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Nombre_Sector FROM Sectores";

                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string nombreSector = reader["Nombre_Sector"].ToString();
                        sectores.Add(nombreSector);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al cargar sectores: " + ex.Message);
                }
            }

            return sectores;
        }

        public string ObtenerNombreLocalidadPorId(int idLocalidad)
        {
            string nombreLocalidad = string.Empty;

            string query = @"
        SELECT Nombre_Localidad 
        FROM Localidades 
        WHERE ID_Localidad = @ID_Localidad";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_Localidad", idLocalidad);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            nombreLocalidad = result.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejar la excepción apropiadamente
                        throw new Exception($"Error al obtener el nombre de la localidad por su ID ({idLocalidad}): {ex.Message}", ex);
                    }
                }
            }

            return nombreLocalidad;
        }



        #endregion
        // Método estático para obtener las localidades desde la base de datos

        public DataTable ObtenerTodasLasLocalidades()
        {
            DataTable dtLocalidades = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString)) 
            {
                string query = @"
            SELECT l.Nombre_Localidad, dl.Direccion 
            FROM Localidades l
            JOIN Detalles_Localidades dl ON l.ID_Detalle_Localidad = dl.ID_Detalle_Localidad";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    dtLocalidades.Load(reader);
                }
            }

            return dtLocalidades;
        }
        public static DataTable ObtenerLocalidades()
        {
            DataTable dtLocalidades = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Nombre_Localidad FROM Localidades";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    dtLocalidades.Load(reader);
                }
            }

            return dtLocalidades;
        }

        public static DataTable ObtenerLocalidadesConDetalles()
        {
            DataTable dtLocalidades = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT L.Nombre_Localidad, D.Direccion, D.url_Localidad
            FROM Localidades L
            INNER JOIN Detalles_Localidades D ON L.ID_Detalle_Localidad = D.ID_Detalle_Localidad";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    dtLocalidades.Load(reader);
                }
            }

            return dtLocalidades;
        }

        public static DataTable ObtenerDetallesLocalidadPorNombreYDireccion(string nombreLocalidad, string direccion)
        {
            DataTable dtDetallesLocalidad = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
        SELECT 
            d.ID_Detalle_Localidad, -- Agregamos el ID del detalle de localidad
            d.Descripcion,
            d.Direccion,
            d.Referencias,
            d.Urbanizacion,
            d.Sector, 
            d.Latitud,
            d.Longitud,
            d.url_Localidad,
            d.ID_Empleado -- Incluimos el ID del Empleado
        FROM 
            Localidades l
        INNER JOIN 
            Detalles_Localidades d ON l.ID_Detalle_Localidad = d.ID_Detalle_Localidad
        WHERE 
            l.Nombre_Localidad = @NombreLocalidad 
            AND d.Direccion = @Direccion";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NombreLocalidad", nombreLocalidad);
                    command.Parameters.AddWithValue("@Direccion", direccion);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        dtDetallesLocalidad.Load(reader);
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error al obtener detalles de localidad: " + ex.Message);
                        throw; // Opcionalmente, relanzar la excepción
                    }
                }
            }

            return dtDetallesLocalidad;
        }

        public static int InsertarDetallesLocalidades(string descripcion, string referencias, string urbanizacion, string sector, string direccion, decimal latitud, decimal longitud, int idEmpleado, string url = null)
        {
            int newId = -1;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Construir la consulta INSERT dinámicamente
                string query = "INSERT INTO Detalles_Localidades (Descripcion, Referencias, Urbanizacion, Sector, Direccion, Latitud, Longitud, url_Localidad";

                // Agregar ID_Empleado solo si el valor es mayor que cero
                if (idEmpleado > 0)
                {
                    query += ", ID_Empleado";
                }

                query += ") VALUES (@Descripcion, @Referencias, @Urbanizacion, @Sector, @Direccion, @Latitud, @Longitud, @Url";

                // Agregar el parámetro ID_Empleado solo si el valor es mayor que cero
                if (idEmpleado > 0)
                {
                    query += ", @IDEmpleado";
                }

                query += "); SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Descripcion", descripcion);
                    command.Parameters.AddWithValue("@Referencias", referencias);
                    command.Parameters.AddWithValue("@Urbanizacion", urbanizacion);
                    command.Parameters.AddWithValue("@Sector", sector);
                    command.Parameters.AddWithValue("@Direccion", direccion);
                    command.Parameters.AddWithValue("@Latitud", latitud);
                    command.Parameters.AddWithValue("@Longitud", longitud);
                    command.Parameters.AddWithValue("@Url", url ?? (object)DBNull.Value);

                    // Agregar el parámetro ID_Empleado solo si el valor es mayor que cero
                    if (idEmpleado > 0)
                    {
                        command.Parameters.AddWithValue("@IDEmpleado", idEmpleado);
                    }

                    try
                    {
                        connection.Open();
                        newId = Convert.ToInt32(command.ExecuteScalar());
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error al insertar detalles de localidad: " + ex.Message);
                        throw;
                    }
                }
            }

            return newId;
        }



        public static void InsertarLocalidad(string nombreLocalidad, int idDetalleLocalidad)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
              INSERT INTO Localidades (Nombre_Localidad, ID_Detalle_Localidad)
              VALUES (@NombreLocalidad, @ID_Detalle_Localidad)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NombreLocalidad", nombreLocalidad);
                        command.Parameters.AddWithValue("@ID_Detalle_Localidad", idDetalleLocalidad);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 50000) // Número de error personalizado definido por el RAISERROR del trigger
                {
                    // El trigger ha lanzado un error de inserción duplicada
                    throw new Exception("Error" + ex);
                }

            }
        }



        public (int, int) ObtenerIdLocalidadYDetallePorNombreYDireccion(string nombreLocalidad, string direccion)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Normalizar los datos de entrada
                nombreLocalidad = nombreLocalidad.Trim().ToLower();
                direccion = direccion.Trim().ToLower();

                // Consulta SQL parametrizada
                string query = @"
                SELECT l.ID_Localidad, d.ID_Detalle_Localidad
                FROM Localidades l
                LEFT JOIN Detalles_Localidades d ON l.ID_Detalle_Localidad = d.ID_Detalle_Localidad
                WHERE LOWER(l.Nombre_Localidad) = @NombreLocalidad
              AND (LOWER(d.Direccion) = @Direccion OR d.Direccion IS NULL)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Asignar parámetros
                    command.Parameters.AddWithValue("@NombreLocalidad", nombreLocalidad);
                    command.Parameters.AddWithValue("@Direccion", direccion);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int idLocalidad = reader.GetInt32(0);  // Índice 0 para ID_Localidad
                                int idDetalleLocalidad = reader.IsDBNull(1) ? -1 : reader.GetInt32(1); // Índice 1 para ID_Detalle_Localidad (puede ser NULL)
                                return (idLocalidad, idDetalleLocalidad);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejo de excepciones (registrar el error, mostrar un mensaje, etc.)
                        Console.WriteLine("Error al obtener los IDs: " + ex.Message);
                    }
                }
            }

            // Si no se encuentra la localidad, devolver (-1, -1)
            return (-1, -1);
        }
        public int ObtenerIdLocalidad(string nombreLocalidad, string direccion)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Normalizar los datos de entrada
                nombreLocalidad = nombreLocalidad.Trim().ToLower();
                direccion = direccion.Trim().ToLower();

                // Consulta SQL parametrizada (modificada para obtener solo ID_Localidad)
                string query = @"
            SELECT l.ID_Localidad
            FROM Localidades l
            LEFT JOIN Detalles_Localidades d ON l.ID_Detalle_Localidad = d.ID_Detalle_Localidad
            WHERE LOWER(l.Nombre_Localidad) = @NombreLocalidad
                AND (LOWER(d.Direccion) = @Direccion OR d.Direccion IS NULL)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Asignar parámetros
                    command.Parameters.AddWithValue("@NombreLocalidad", nombreLocalidad);
                    command.Parameters.AddWithValue("@Direccion", direccion);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return reader.GetInt32(0); // Índice 0 para ID_Localidad
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejo de excepciones (registrar el error, mostrar un mensaje, etc.)
                        Console.WriteLine("Error al obtener el ID de Localidad: " + ex.Message);
                    }
                }
            }

            // Si no se encuentra la localidad, devolver -1
            return -1;
        }
        public void EliminarLocalidadYDetalle(int idLocalidad, int idDetalleLocalidad)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Consultas para eliminar registros dependientes
                string queryEliminarVisitas = "DELETE FROM Visitas WHERE ID_Localidad = @ID_Detalle_Localidad";

                // Consulta para eliminar la localidad
                string queryLocalidad = "DELETE FROM Localidades WHERE ID_Localidad = @ID_Localidad";

                // Consulta para eliminar el detalle de la localidad
                string queryDetalle = "DELETE FROM Detalles_Localidades WHERE ID_Detalle_Localidad = @ID_Detalle_Localidad";

                // Iniciar una transacción para garantizar que todas las consultas se ejecuten como una unidad atómica
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Primero, eliminar las visitas asociadas a la localidad
                    using (SqlCommand command = new SqlCommand(queryEliminarVisitas, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@ID_Detalle_Localidad", idDetalleLocalidad);
                        command.ExecuteNonQuery();
                    }

                    // Luego, eliminar la localidad
                    using (SqlCommand command = new SqlCommand(queryLocalidad, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@ID_Localidad", idLocalidad);
                        command.ExecuteNonQuery();
                    }

                    // Finalmente, eliminar el detalle de la localidad
                    using (SqlCommand command = new SqlCommand(queryDetalle, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@ID_Detalle_Localidad", idDetalleLocalidad);
                        command.ExecuteNonQuery();
                    }

                    // Si no hay excepciones, confirmar la transacción
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // En caso de una excepción, hacer un rollback de la transacción
                    transaction.Rollback();
                    // Manejar la excepción apropiadamente (p. ej. registrándola, lanzándola nuevamente, mostrando un mensaje de error, etc.)
                    throw new Exception("Error al eliminar la localidad y su detalle asociado", ex);
                }
            }
        }

        public static void ActualizarDetallesLocalidades(int idDetalleLocalidad, string nombreLocalidad, string descripcion, string referencias, string urbanizacion, string sector, string direccion, decimal latitud, decimal longitud, int idEmpleado)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Actualizar Detalles_Localidades
                string queryDetalles = @"
            UPDATE Detalles_Localidades 
            SET Descripcion = @Descripcion, Referencias = @Referencias, Urbanizacion = @Urbanizacion, 
            Sector = @Sector, Direccion = @Direccion, Latitud = @Latitud, Longitud = @Longitud";

                // Solo incluimos la actualización de ID_Empleado si no es null ni cero
                if (idEmpleado > 0)
                {
                    queryDetalles += ", ID_Empleado = @IDEmpleado";
                }

                queryDetalles += " WHERE ID_Detalle_Localidad = @IdDetalleLocalidad";



                using (SqlCommand commandDetalles = new SqlCommand(queryDetalles, connection))
                {
                    // Parámetros para Detalles_Localidades
                    commandDetalles.Parameters.AddWithValue("@Descripcion", descripcion);
                    commandDetalles.Parameters.AddWithValue("@Referencias", referencias);
                    commandDetalles.Parameters.AddWithValue("@Urbanizacion", urbanizacion);
                    commandDetalles.Parameters.AddWithValue("@Sector", sector);
                    commandDetalles.Parameters.AddWithValue("@Direccion", direccion);
                    commandDetalles.Parameters.AddWithValue("@Latitud", latitud);
                    commandDetalles.Parameters.AddWithValue("@Longitud", longitud);
                    commandDetalles.Parameters.AddWithValue("@IdDetalleLocalidad", idDetalleLocalidad);
                    commandDetalles.Parameters.AddWithValue("@IDEmpleado", idEmpleado);

                    try
                    {
                        connection.Open();
                        commandDetalles.ExecuteNonQuery();

                        // Actualizar Localidades (si la conexión sigue abierta)
                        string queryLocalidades = @"
                    UPDATE Localidades 
                    SET Nombre_Localidad = @NombreLocalidad
                    WHERE ID_Detalle_Localidad = @IdDetalleLocalidad";

                        using (SqlCommand commandLocalidades = new SqlCommand(queryLocalidades, connection))
                        {
                            // Parámetros para Localidades
                            commandLocalidades.Parameters.AddWithValue("@NombreLocalidad", nombreLocalidad);
                            commandLocalidades.Parameters.AddWithValue("@IdDetalleLocalidad", idDetalleLocalidad);

                            commandLocalidades.ExecuteNonQuery();
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error al actualizar detalles de localidad: " + ex.Message);
                        throw; // Opcionalmente, relanzar la excepción
                    }
                }
            }
        }


        public static void ActualizarUrlImagen(int idDetalleLocalidad, int idLocalidad, string url)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                UPDATE Detalles_Localidades 
                SET url_Localidad = @Url
                WHERE ID_Detalle_Localidad = @IdDetalleLocalidad;

                UPDATE Localidades
                SET ID_Detalle_Localidad = @IdDetalleLocalidad
                WHERE ID_Localidad = @IdLocalidad";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Url", (object)url ?? DBNull.Value); // Pasar DBNull.Value si el url es null
                    command.Parameters.AddWithValue("@IdDetalleLocalidad", idDetalleLocalidad);
                    command.Parameters.AddWithValue("@IdLocalidad", idLocalidad);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public bool TieneVisitasPendientes(int idLocalidad)
        {
            using (var conexion = new SqlConnection(connectionString))
            {
                conexion.Open();

                string consulta = @"
            SELECT CASE 
                       WHEN EXISTS (
                           SELECT 1 
                           FROM Visitas 
                           WHERE ID_Localidad = @idLocalidad 
                             AND Estado = @EstadoPendiente
                       ) THEN CAST(1 AS BIT) 
                       ELSE CAST(0 AS BIT) 
                   END AS TieneVisitasPendientes"; // Corrección: alias TieneVisitasPendientes

                using (var comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@idLocalidad", idLocalidad);
                    comando.Parameters.AddWithValue("@EstadoPendiente", false);

                    // Corrección: Leer el resultado como booleano directamente
                    bool tieneVisitasPendientes = (bool)comando.ExecuteScalar();

                    return tieneVisitasPendientes;
                }
            }
        }





    }

}
