using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.ClasesForm
{

    public class Gradient_SidebarPanel : Panel
    {
        public Color gradientTop { get; set; }
        public Color gradientBottom { get; set; }

        public Gradient_SidebarPanel()
        {
            this.Resize += Gradient_SidebarPanel_Resize;
        }

        private void Gradient_SidebarPanel_Resize(object sender, EventArgs e)
        {

            this.Invalidate();

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Llama al método que maneja el dibujado del fondo
            DibujarFondo(e.Graphics);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            // Cuando el tamaño del control cambia, invalida el área para que se vuelva a pintar
            Invalidate();
        }

        private void DibujarFondo(Graphics g)
        {
            // Verificar que el tamaño del control sea válido
            if (ClientRectangle.Width > 0 && ClientRectangle.Height > 0)
            {
                // Crear el pincel lineal usando el tamaño actual del control
                using (LinearGradientBrush linear = new LinearGradientBrush(ClientRectangle, gradientTop, gradientBottom, 90F))
                {
                    // Rellenar el rectángulo del control con el pincel lineal
                    g.FillRectangle(linear, ClientRectangle);
                }
            }
        }



    }
}
