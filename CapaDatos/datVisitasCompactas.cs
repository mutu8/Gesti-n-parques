using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class datVisitasCompactas
    {
        private static string connectionString = Conexion.Instancia.obtenerConexion();
        private static readonly datVisitasCompactas _instancia = new datVisitasCompactas();

        public static datVisitasCompactas Instancia
        {
            get { return datVisitasCompactas._instancia; }
        }

        // Método para marcar una visita como completada
        public void MarcarVisitaCompletada(int idVisitaCompacta)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                UPDATE VisitasCompactas
                SET Completada = 1
                WHERE ID_VisitaCompacta = @ID_VisitaCompacta";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID_VisitaCompacta", idVisitaCompacta);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new Exception("No se encontró ninguna visita con el ID especificado.");
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al marcar la visita como completada: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
