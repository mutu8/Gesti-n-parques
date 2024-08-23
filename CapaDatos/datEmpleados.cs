using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class datEmpleados
    {
        private static string connectionString = Conexion.Instancia.obtenerConexion();
        private static readonly datEmpleados _instancia = new datEmpleados();

        public static datEmpleados Instancia
        {
            get { return datEmpleados._instancia; }
        }

        public void ActualizarEstadoEsPersonal(int idEmpleado)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Consulta SQL para actualizar esPersonalLimpieza a true
                string query = @"
            UPDATE Empleados
            SET esPersonalLimpieza = 1
            WHERE ID_Empleado = @IdEmpleado";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdEmpleado", idEmpleado);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new Exception("No se encontró ningún empleado con el ID especificado.");
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al actualizar el estado de esPersonalLimpieza en la base de datos: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void InsertarEmpleado(string nombres, string apellidos, bool esApoyo, string direccionCorreo, string urlFoto, string dni, bool? esPersonalLimpieza)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
            INSERT INTO Empleados 
            (Nombres, Apellidos, esApoyo, DireccionCorreo, urlFoto, DNI, esPersonalLimpieza)
            VALUES 
            (@Nombres, @Apellidos, @EsApoyo, @DireccionCorreo, @UrlFoto, @DNI, @EsPersonalLimpieza)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nombres", nombres);
                command.Parameters.AddWithValue("@Apellidos", apellidos);
                command.Parameters.AddWithValue("@EsApoyo", esApoyo);
                command.Parameters.AddWithValue("@DireccionCorreo", (object)direccionCorreo ?? DBNull.Value);
                command.Parameters.AddWithValue("@UrlFoto", (object)urlFoto ?? DBNull.Value);
                command.Parameters.AddWithValue("@DNI", (object)dni ?? DBNull.Value);
                command.Parameters.AddWithValue("@esPersonalLimpieza", (object)esPersonalLimpieza ?? DBNull.Value);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al insertar el empleado en la base de datos: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }






        public DataTable ObtenerTodosLosEmpleados()
        {
            DataTable dtEmpleados = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Empleados;"; // Consulta para obtener todos los campos

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    dtEmpleados.Load(reader);
                }
            }

            return dtEmpleados;
        }
        public int ObtenerEmpleadoIdPorNombre(string nombreCompleto)
        {
            int empleadoId = -1; // Valor por defecto si no se encuentra

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ID_Empleado FROM Empleados WHERE Nombres + ' ' + Apellidos = @NombreCompleto";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NombreCompleto", nombreCompleto);

                    connection.Open();
                    object result = command.ExecuteScalar(); // Obtener el primer resultado
                    if (result != null && result != DBNull.Value)
                    {
                        empleadoId = Convert.ToInt32(result);
                    }
                }
            }

            return empleadoId;
        }
        public string ObtenerNombreCompletoPorEmpleadoId(int empleadoId)
        {
            string nombreCompleto = null; // Valor por defecto si no se encuentra

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
        SELECT 
            RTRIM(LTRIM(COALESCE(Nombres, ''))) + ' ' + RTRIM(LTRIM(COALESCE(Apellidos, ''))) AS NombreCompleto 
        FROM 
            Empleados 
        WHERE 
            ID_Empleado = @EmpleadoId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmpleadoId", empleadoId);

                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        nombreCompleto = result.ToString();
                    }
                }
            }

            return nombreCompleto;
        }


        public DataTable ObtenerDetallesEmpleadoPorId(int empleadoId)
        {
            DataTable dtDetallesEmpleado = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Empleados WHERE ID_Empleado = @EmpleadoId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmpleadoId", empleadoId);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    dtDetallesEmpleado.Load(reader);
                }
            }

            return dtDetallesEmpleado;
        }
        public void ModificarEmpleado(int empleadoId, bool esApoyo, string direccionCorreo, string urlFoto, string dni, DateTime fechaNacimiento)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"UPDATE Empleados 
                             SET esApoyo = @EsApoyo,
                                 DireccionCorreo = @DireccionCorreo,
                                 urlFoto = @UrlFoto,
                                 DNI = @DNI,
                                 FechaNacimiento = @FechaNacimiento
                             WHERE ID_Empleado = @EmpleadoId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EmpleadoId", empleadoId);
                        command.Parameters.AddWithValue("@EsApoyo", esApoyo);
                        command.Parameters.AddWithValue("@DireccionCorreo", direccionCorreo);
                        command.Parameters.AddWithValue("@UrlFoto", urlFoto);
                        command.Parameters.AddWithValue("@DNI", dni);
                        command.Parameters.AddWithValue("@FechaNacimiento", fechaNacimiento);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al modificar el empleado en la base de datos: " + ex.Message);
                }
            }
        }



        public bool EliminarEmpleado(int empleadoId, out string mensajeError)
        {
            mensajeError = ""; // Inicializar el mensaje de error como vacío

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Verificar si el empleado está asignado a alguna visita
                if (EmpleadoTieneVisitasAsignadas(empleadoId, connection))
                {
                    mensajeError = "El empleado tiene visitas asignadas y no puede ser eliminado.";
                    return false; // Indicar que la eliminación no fue exitosa
                }

                // Si no tiene visitas asignadas, proceder con la eliminación
                string queryEliminar = "DELETE FROM Empleados WHERE ID_Empleado = @EmpleadoId";
                using (SqlCommand commandEliminar = new SqlCommand(queryEliminar, connection))
                {
                    commandEliminar.Parameters.AddWithValue("@EmpleadoId", empleadoId);
                    commandEliminar.ExecuteNonQuery();
                }
            }

            return true; // Indicar que la eliminación fue exitosa
        }

        // Método para verificar si el empleado tiene visitas asignadas
        private bool EmpleadoTieneVisitasAsignadas(int empleadoId, SqlConnection connection)
        {
            string query = "SELECT COUNT(*) FROM Visitas WHERE ID_Empleado = @EmpleadoId";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@EmpleadoId", empleadoId);
                int count = (int)command.ExecuteScalar();
                return count > 0; // Devolver true si hay visitas asignadas, false en caso contrario
            }
        }

        public DataTable ObtenerEmpleadosFiltrados(string filtro)
        {
            DataTable dtEmpleados = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Nueva consulta que concatena Nombres y Apellidos
                string query = @"SELECT * FROM Empleados
                         WHERE CONCAT(Nombres, ' ', Apellidos) LIKE @Filtro";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Filtro", "%" + filtro + "%");
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    dtEmpleados.Load(reader);
                }
            }

            return dtEmpleados;
        }

        public DataTable ObtenerEmpleadosPersonalYParques(string filtro)
        {
            DataTable dtEmpleados = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Inicializamos el comando de SQL
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;

                    // Establecemos la consulta en función del filtro
                    if (filtro == "parques")
                    {
                        command.CommandText = @"SELECT * FROM Empleados WHERE esPersonalLimpieza IS NULL";
                    }
                    else if (filtro == "limpieza")
                    {
                        command.CommandText = @"SELECT * FROM Empleados WHERE esPersonalLimpieza = 1";
                    }
                    else
                    {
                        throw new ArgumentException("Filtro no válido. Debe ser 'parques' o 'limpieza'.");
                    }

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dtEmpleados.Load(reader);
                    }
                }
            }

            return dtEmpleados;
        }



        public DataTable ListarEmpleadosQueSeanLimpieza()
        {
            DataTable dtEmpleados = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
        SELECT 
            E.ID_Empleado, 
            E.Nombres, 
            E.Apellidos, 
            A.ID_Asistencia
        FROM 
            Empleados E
        LEFT JOIN 
            Asistencias A 
        ON 
            E.ID_Empleado = A.ID_Empleado 
            AND CONVERT(DATE, A.Fecha_Asistencia) = CONVERT(DATE, GETDATE())
        WHERE 
            E.esPersonalLimpieza = 1;
        ";
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dtEmpleados);
                }
                catch (SqlException ex)
                {
                    // Manejo de excepciones
                    throw new Exception("Error al listar empleados: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            return dtEmpleados;
        }


        public DataTable datObtenerPersonalLimpiezaParaComboBox()
        {
            DataTable dtEmpleados = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Consulta para obtener los empleados que son personal de limpieza
                string query = @"
        SELECT 
            ID_Empleado, 
            CONCAT(Nombres, ' ', Apellidos) AS NombreCompleto
        FROM 
            Empleados
        WHERE 
            esPersonalLimpieza = 1;
        ";

                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dtEmpleados);
                }
                catch (SqlException ex)
                {
                    // Manejo de excepciones
                    throw new Exception("Error al obtener el personal de limpieza: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            return dtEmpleados;
        }


    }
}
