﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}