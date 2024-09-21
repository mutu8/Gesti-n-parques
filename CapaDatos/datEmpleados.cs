using System;
using System.Collections.Generic;
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

        public DataTable ObtenerEmpleadosFiltrados(string filtro, string categoria)
        {
            DataTable dtEmpleados = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Base de la consulta SQL
                string query = @"SELECT * FROM Empleados
                         WHERE CONCAT(Nombres, ' ', Apellidos) LIKE @Filtro";

                // Ajustar la consulta según la categoría seleccionada
                if (categoria == "Limpieza")
                {
                    query += " AND esPersonalLimpieza = 1"; // Filtra empleados de limpieza
                }
                else if (categoria == "Parques")
                {
                    query += " AND (esPersonalLimpieza = 0 OR esPersonalLimpieza IS NULL)"; // Filtra empleados que no son de limpieza o tienen NULL en esPersonalLimpieza
                }

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Parámetro para el filtro de texto (nombres y apellidos)
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

        public DataTable ObtenerPlacas()
        {
            DataTable dtPlacas = new DataTable();

            using (SqlConnection connection = new SqlConnection(Conexion.Instancia.obtenerConexion()))
            {
                string query = "SELECT PlacaVehicular FROM DetalleCompactas";  // Consulta para obtener las placas

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dtPlacas);
                }
            }

            return dtPlacas;
        }

        public DataTable datObtenerPersonalCompacta()
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
                    esPersonalCompacta = 0;
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
                    throw new Exception("Error al obtener el personal compactas: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            return dtEmpleados;
        }
        public int InsertarDetallePersonalCompacta(int idEmpleado, int idCompacta, bool esConductor, string zona, string turno)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Consulta SQL para insertar un nuevo detalle y devolver el ID generado
                string query = @"
            INSERT INTO DetallePersonalCompacta (ID_Empleado, ID_Compacta, esConductor, Zona, Turno)
            OUTPUT INSERTED.ID_PersonalCompacta
            VALUES (@ID_Empleado, @ID_Compacta, @EsConductor, @Zona, @Turno)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID_Empleado", idEmpleado);
                command.Parameters.AddWithValue("@ID_Compacta", idCompacta);
                command.Parameters.AddWithValue("@EsConductor", esConductor);
                command.Parameters.AddWithValue("@Zona", zona);
                command.Parameters.AddWithValue("@Turno", turno);

                try
                {
                    connection.Open();
                    // Retornar el ID generado por la inserción
                    return (int)command.ExecuteScalar();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al insertar el detalle del personal compacta: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }


        public void InsertarVisitaCompacta(int id_DetallePersonalCompacta, DateTime fechaVisita, bool completada)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Consulta SQL para insertar una nueva visita compacta
                string query = @"
            INSERT INTO VisitasCompactas (ID_DetallePersonalCompacta, Fecha_Visita, Completada)
            VALUES (@ID_DetallePersonalCompacta, @Fecha_Visita, @Completada)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID_DetallePersonalCompacta", id_DetallePersonalCompacta);
                command.Parameters.AddWithValue("@Fecha_Visita", fechaVisita);
                command.Parameters.AddWithValue("@Completada", completada);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new Exception("Error al insertar la visita compacta. No se afectaron filas.");
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al insertar la visita compacta en la base de datos: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public int ObtenerIDCompactaPorPlaca(string placaVehicular)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Consulta SQL para obtener el ID de la compacta a partir de la placa vehicular
                string query = @"
                SELECT ID_Compacta 
                FROM DetalleCompactas 
                WHERE PlacaVehicular = @PlacaVehicular";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PlacaVehicular", placaVehicular);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    // Verificar si se obtuvo algún resultado
                    if (result != null)
                    {
                        return Convert.ToInt32(result);  // Convertir el resultado a entero (ID_Compacta)
                    }
                    else
                    {
                        throw new Exception("No se encontró ningún vehículo con la placa proporcionada.");
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al obtener el ID de la compacta: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public DataTable ObtenerVisitasCompactasDeHoy()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                SELECT 
                    vc.ID_VisitaCompacta, 
                    vc.ID_DetallePersonalCompacta, 
                    vc.Fecha_Visita, 
                    vc.Completada, 
                    dp.ID_Empleado, 
                    dp.Zona, 
                    dp.Turno, 
                    dp.esConductor, 
                    e.Nombres + ' ' + e.Apellidos AS NombreCompleto,
                    c.PlacaVehicular
                FROM VisitasCompactas vc
                JOIN DetallePersonalCompacta dp ON vc.ID_DetallePersonalCompacta = dp.ID_PersonalCompacta
                JOIN Empleados e ON dp.ID_Empleado = e.ID_Empleado
                JOIN DetalleCompactas c ON dp.ID_Compacta = c.ID_Compacta
                WHERE vc.Fecha_Visita = CAST(GETDATE() AS DATE)";

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dtVisitasCompactas = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(dtVisitasCompactas);
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al obtener las visitas compactas del día: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }

                return dtVisitasCompactas;
            }
        }

        public int ObtenerIDVisitaCompacta(string placaVehicular, string nombreEmpleado, string zona, bool esConductor, DateTime fechaVisita)
        {
            int idVisitaCompacta = -1; // Valor por defecto si no se encuentra

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT vc.ID_VisitaCompacta
            FROM VisitasCompactas vc
            JOIN DetallePersonalCompacta dp ON vc.ID_DetallePersonalCompacta = dp.ID_PersonalCompacta
            JOIN Empleados e ON dp.ID_Empleado = e.ID_Empleado
            JOIN DetalleCompactas c ON dp.ID_Compacta = c.ID_Compacta
            WHERE c.PlacaVehicular = @PlacaVehicular
            AND e.Nombres + ' ' + e.Apellidos = @NombreEmpleado
            AND dp.Zona = @Zona
            AND dp.esConductor = @EsConductor
            AND vc.Fecha_Visita = @FechaVisita";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PlacaVehicular", placaVehicular);
                command.Parameters.AddWithValue("@NombreEmpleado", nombreEmpleado);
                command.Parameters.AddWithValue("@Zona", zona);
                command.Parameters.AddWithValue("@EsConductor", esConductor);
                command.Parameters.AddWithValue("@FechaVisita", fechaVisita);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        idVisitaCompacta = Convert.ToInt32(result);  // Asignar el ID si se encuentra
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al obtener el ID de la visita compacta: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            return idVisitaCompacta;
        }


    }

}
