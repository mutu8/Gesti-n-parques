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

    }

}
