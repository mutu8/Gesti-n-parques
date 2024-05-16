using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class logLocalidades
    {
        private static readonly logLocalidades _instancia = new logLocalidades();
        public static logLocalidades Instancia
        {
            get { return logLocalidades._instancia; }
        }
        public DataTable ObtenerLocalidades()
        {
            return datLocalidades.ObtenerLocalidades();
        }
    }
}
