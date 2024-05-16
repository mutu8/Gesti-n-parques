using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class logConversion
    {
        private static readonly logConversion _instancia = new logConversion();
        public static logConversion Instancia
        {
            get { return logConversion._instancia; }
        }

        private readonly datConversion _datConversion;

        private logConversion()
        {
            _datConversion = new datConversion();
        }

        public async Task<string> ObtenerDireccion(double lat, double lng)
        {
            return await _datConversion.ObtenerDireccion(lat, lng);
        }

        public async Task<Tuple<double, double>> ConvertirDireccionALatitudLongitud(string direccion)
        {
            return await _datConversion.ConvertirDireccionALatitudLongitud(direccion);
        }
    }

}
