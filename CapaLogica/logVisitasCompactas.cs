using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class logVisitasCompactas
    {
        private static readonly logVisitasCompactas _instancia = new logVisitasCompactas();

        public static logVisitasCompactas Instancia
        {
            get { return logVisitasCompactas._instancia; }
        }

        // Método para marcar una visita como completada
        public void MarcarVisitaCompletada(int idVisitaCompacta)
        {
            try
            {
                // Llamar al método de la capa de datos para realizar la actualización
                datVisitasCompactas.Instancia.MarcarVisitaCompletada(idVisitaCompacta);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, registro de log o notificación de error al usuario
                throw new Exception("Error en la capa lógica al marcar la visita como completada: " + ex.Message);
            }
        }
    }
}
