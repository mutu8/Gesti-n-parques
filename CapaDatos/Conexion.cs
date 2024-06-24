using System;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

namespace CapaDatos
{
    public class Conexion
    {
        private static readonly Conexion _instancia = new Conexion();
        public static Conexion Instancia
        {
            get { return Conexion._instancia; }
        }

        private string connectionString;

        public Conexion()
        {
            //Concatena el machineName en la cadena de conexión
            string machineName = Environment.MachineName;

            //connectionString = "Server=mssql-176029-0.cloudclusters.net,19136;Database=BD_GestionAmbiental;User Id=Yordy;Password=#sWbQy)J[^mJPF9atbpVA^c*b#%zn0ZkqcUDU>EDtaGXBDh<c+65B]w0rf0y#TnU;";
            connectionString = $"Server={machineName}\\SQLEXPRESS;Database=BD_GestionAmbiental;Integrated Security=True;";
        }

        public string obtenerConexion()
        {
            return connectionString;
        }
    }
}
