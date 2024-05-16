using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class logConexion
    {
        private static readonly logConexion _instancia = new logConexion();
        public static logConexion Instancia
        {
            get { return logConexion._instancia; }
        }
        public string ObtenerConexion()
        {
            try
            {
                // Llamamos al método correspondiente en la capa de datos
                return Conexion.Instancia.obtenerConexion();
            }
            catch (Exception ex)
            {
                // Puedes manejar la excepción según tus necesidades
                Console.WriteLine("ERROR " + ex.Message);
                return string.Empty;
            }
        }
    }
}
