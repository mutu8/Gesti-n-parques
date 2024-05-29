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
                d.Descripcion,
                d.Direccion,
                d.Referencias,
                d.Urbanizacion,
                d.Sector, -- Cambio de Jiron a Sector
                d.Latitud,
                d.Longitud,
                d.url_Localidad
            FROM 
                Localidades l
            INNER JOIN 
                Detalles_Localidades d ON l.ID_Detalle_Localidad = d.ID_Detalle_Localidad
            WHERE 
                l.Nombre_Localidad = @NombreLocalidad 
                AND d.Direccion = @Direccion
        ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NombreLocalidad", nombreLocalidad);
                    command.Parameters.AddWithValue("@Direccion", direccion);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    dtDetallesLocalidad.Load(reader);
                }
            }

            return dtDetallesLocalidad;
        }

        public static int InsertarDetallesLocalidades(string descripcion, string referencias, string urbanizacion, string sector, string direccion, decimal latitud, decimal longitud, string url = null)
        {
            int newId = -1;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                INSERT INTO Detalles_Localidades (Descripcion, Referencias, Urbanizacion, Sector, Direccion, Latitud, Longitud, url_Localidad)
                VALUES (@Descripcion, @Referencias, @Urbanizacion, @Sector, @Direccion, @Latitud, @Longitud, @Url);
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
                    command.Parameters.AddWithValue("@Url", (object)url ?? DBNull.Value);

                    connection.Open();
                    // Utilizamos SCOPE_IDENTITY() para obtener el ID generado automáticamente
                    newId = Convert.ToInt32(command.ExecuteScalar());
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
                string query = @"
            SELECT 
                l.ID_Localidad,
                d.ID_Detalle_Localidad
            FROM 
                Localidades l
            INNER JOIN 
                Detalles_Localidades d ON l.ID_Detalle_Localidad = d.ID_Detalle_Localidad
            WHERE 
                l.Nombre_Localidad = @NombreLocalidad 
                AND d.Direccion = @Direccion
        ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NombreLocalidad", nombreLocalidad);
                    command.Parameters.AddWithValue("@Direccion", direccion);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        int idLocalidad = Convert.ToInt32(reader["ID_Localidad"]);
                        int idDetalleLocalidad = Convert.ToInt32(reader["ID_Detalle_Localidad"]);
                        return (idLocalidad, idDetalleLocalidad);
                    }
                    else
                    {
                        // Manejar el caso en que la localidad no se encuentre en la base de datos
                        return (-1, -1); // Por ejemplo, se podría devolver (-1, -1) si no se encuentra la localidad
                    }
                }
            }
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
        public static void ActualizarDetallesLocalidades(int idDetalleLocalidad, string nombreLocalidad, string descripcion, string referencias, string urbanizacion, string sector, string direccion, decimal latitud, decimal longitud)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
        UPDATE Detalles_Localidades 
        SET Descripcion = @Descripcion, Referencias = @Referencias, Urbanizacion = @Urbanizacion, 
            Sector = @Sector, Direccion = @Direccion, Latitud = @Latitud, Longitud = @Longitud
        WHERE ID_Detalle_Localidad = @IdDetalleLocalidad";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Descripcion", descripcion);
                    command.Parameters.AddWithValue("@Referencias", referencias);
                    command.Parameters.AddWithValue("@Urbanizacion", urbanizacion);
                    command.Parameters.AddWithValue("@Sector", sector);
                    command.Parameters.AddWithValue("@Direccion", direccion);
                    command.Parameters.AddWithValue("@Latitud", latitud);
                    command.Parameters.AddWithValue("@Longitud", longitud);
                    command.Parameters.AddWithValue("@IdDetalleLocalidad", idDetalleLocalidad);

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                // Actualizar el nombre en la tabla Localidades
                string queryNombre = @"
        UPDATE Localidades 
        SET Nombre_Localidad = @NombreLocalidad
        WHERE ID_Detalle_Localidad = @IdDetalleLocalidad";

                using (SqlCommand commandNombre = new SqlCommand(queryNombre, connection))
                {
                    commandNombre.Parameters.AddWithValue("@NombreLocalidad", nombreLocalidad);
                    commandNombre.Parameters.AddWithValue("@IdDetalleLocalidad", idDetalleLocalidad);

                    commandNombre.ExecuteNonQuery();
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
