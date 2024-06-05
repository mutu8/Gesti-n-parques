using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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




        #endregion
        // Método estático para obtener las localidades desde la base de datos
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
                string query = @"
            INSERT INTO Detalles_Localidades (Descripcion, Referencias, Urbanizacion, Sector, Direccion, Latitud, Longitud, url_Localidad, ID_Empleado)
            VALUES (@Descripcion, @Referencias, @Urbanizacion, @Sector, @Direccion, @Latitud, @Longitud, @Url, @IDEmpleado);
            SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Descripcion", descripcion);
                    command.Parameters.AddWithValue("@Referencias", referencias);
                    command.Parameters.AddWithValue("@Urbanizacion", urbanizacion);
                    command.Parameters.AddWithValue("@Sector", sector);
                    command.Parameters.AddWithValue("@Direccion", direccion);
                    command.Parameters.AddWithValue("@Latitud", latitud);
                    command.Parameters.AddWithValue("@Longitud", longitud);
                    command.Parameters.AddWithValue("@Url", url ?? (object)DBNull.Value); // Manejo seguro de nulls
                    command.Parameters.AddWithValue("@IDEmpleado", idEmpleado); // Nuevo parámetro

                    try
                    {
                        connection.Open();
                        newId = Convert.ToInt32(command.ExecuteScalar());
                    }
                    catch (SqlException ex)
                    {
                        // Manejo de errores de SQL Server (puedes registrar el error o mostrar un mensaje al usuario)
                        Console.WriteLine("Error al insertar detalles de localidad: " + ex.Message);
                        throw; // Opcionalmente, relanzar la excepción para que el método llamante también la maneje.
                    }
                }
            }
            return newId;
        }

        public static void InsertarLocalidad(string nombreLocalidad, int idDetalleLocalidad)
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
                // Consulta para eliminar la localidad (referencia principal)
                string queryLocalidad = "DELETE FROM Localidades WHERE ID_Localidad = @ID_Localidad";

                // Consulta para eliminar el detalle de la localidad
                string queryDetalle = "DELETE FROM Detalles_Localidades WHERE ID_Detalle_Localidad = @ID_Detalle_Localidad";

                // Iniciar una transacción para garantizar que ambas consultas se ejecuten como una unidad atómica
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Primero eliminar la localidad (referencia principal)
                    using (SqlCommand command = new SqlCommand(queryLocalidad, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@ID_Localidad", idLocalidad);
                        command.ExecuteNonQuery();
                    }

                    // Luego, eliminar el detalle de la localidad
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
                    // Aquí puedes agregar código para manejar la excepción según los requisitos de tu aplicación
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
                Sector = @Sector, Direccion = @Direccion, Latitud = @Latitud, Longitud = @Longitud,
                ID_Empleado = @IDEmpleado -- Actualizar el ID del Empleado
            WHERE ID_Detalle_Localidad = @IdDetalleLocalidad";

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

       


    }

}
