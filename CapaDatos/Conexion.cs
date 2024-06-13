using System;

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
            string machineName = Environment.MachineName;

            // Concatena el machineName en la cadena de conexión
            connectionString = "Server=mssql-175137-0.cloudclusters.net,10031;Database=BD_GestionAmbiental;User Id=Steven;Password=#sWbQy)J[^mJPF9atbpVA^c*b#%zn0ZkqcUDU>EDtaGXBDh<c+65B]w0rf0y#TnU;";
        }

        public string obtenerConexion()
        {
            return connectionString;
        }
    }
}
