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


        public static DataTable ObtenerDetallesLocalidadPorNombre(string nombreLocalidad)
        {
            DataTable dtDetallesLocalidad = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Detalles_Localidades.* " +
                               "FROM Localidades " +
                               "INNER JOIN Detalles_Localidades " +
                               "ON Localidades.ID_Detalle_Localidad = Detalles_Localidades.ID_Detalle_Localidad " +
                               "WHERE Localidades.Nombre_Localidad = @NombreLocalidad";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NombreLocalidad", nombreLocalidad);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    dtDetallesLocalidad.Load(reader);
                }
            }

            return dtDetallesLocalidad;
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
                    d.Jiron,
                    d.Manzana,
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
        // Método para insertar detalles de localidades y devolver el ID generado
        public static int InsertarDetallesLocalidades(string descripcion, string referencias, string urbanizacion, string manzana, string jiron, string direccion, decimal latitud, decimal longitud, string url)
        {
            int newId = -1; // Inicializar con -1

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
            INSERT INTO Detalles_Localidades (Descripcion, Referencias, Urbanizacion, Manzana, Jiron, Direccion, Latitud, Longitud, url_Localidad)
            VALUES (@Descripcion, @Referencias, @Urbanizacion, @Manzana, @Jiron, @Direccion, @Latitud, @Longitud, @Url);
            SELECT SCOPE_IDENTITY();"; // Obtener el ID autogenerado

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Descripcion", descripcion);
                    command.Parameters.AddWithValue("@Referencias", referencias);
                    command.Parameters.AddWithValue("@Urbanizacion", urbanizacion);
                    command.Parameters.AddWithValue("@Manzana", manzana);
                    command.Parameters.AddWithValue("@Jiron", jiron);
                    command.Parameters.AddWithValue("@Direccion", direccion);
                    command.Parameters.AddWithValue("@Latitud", latitud);
                    command.Parameters.AddWithValue("@Longitud", longitud);
                    command.Parameters.AddWithValue("@Url", url);

                    connection.Open();
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


    }

}
