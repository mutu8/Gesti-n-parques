using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using CapaPresentacion;
using CapaLogica;
using MaterialSkin;
using MaterialSkin.Controls;

namespace CapaPresentación.Formularios
{
    public partial class frmMapa : MaterialForm
    {
        GMarkerGoogle marker;
        GMapOverlay markerOverlay;
        private readonly logConversion _logConversion;

        double LatInicial = -8.103034453133846;
        double LngInicial = -79.01766201952019;

        public frmMapa()
        {
            InitializeComponent();
            this.MaximizeBox = false; // Desactivar el botón de maximizar
            _logConversion = logConversion.Instancia; // Instancia del logConversion
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Creando las dimensiones del GMAPCONTROL(herramienta)
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.CanDragMap = true;
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            gMapControl1.Position = new PointLatLng(LatInicial, LngInicial);
            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 24;
            gMapControl1.Zoom = 9;
            gMapControl1.AutoScroll = true;

            // Marcador
            markerOverlay = new GMapOverlay("Marcador");
            marker = new GMarkerGoogle(new PointLatLng(LatInicial, LngInicial), GMarkerGoogleType.blue);
            markerOverlay.Markers.Add(marker); // Agregamos al mapa

            // Agregamos un tooltip de texto a los marcadores
            marker.ToolTipMode = MarkerTooltipMode.Always;
            marker.ToolTipText = string.Format("Ubicación:\n Latitud:{0}\n Longitud:{1}", LatInicial, LngInicial);

            // Ahora agregamos el mapa y el marcador al control map
            gMapControl1.Overlays.Add(markerOverlay);
        }

        private async Task<string> ObtenerYMostrarDireccion(double latitud, double longitud)
        {
            try
            {
                string direccion = await _logConversion.ObtenerDireccion(latitud, longitud); // Llamada a ObtenerDireccion de logConversion
                Console.WriteLine(direccion); // Solo para depuración, puedes quitarlo
                return direccion;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener la dirección: " + ex.Message);
                return null;
            }
        }

        private async void gMapControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Se obtienen los datos de latitud y longitud del mapa donde el usuario hizo clic
            double latitud = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lat;
            double longitud = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lng;

            // Se posicionan en los TextBox de latitud y longitud
            txtlatitud.Text = latitud.ToString();
            txtlongitud.Text = longitud.ToString();

            // Se obtiene la dirección y se muestra en el marcador
            string direccion = await ObtenerYMostrarDireccion(latitud, longitud);

            // Se crea el marcador para moverlo al lugar indicado por el usuario
            marker.Position = new PointLatLng(latitud, longitud);
            // También se agrega el mensaje al marcador, es decir, el ToolTip
            marker.ToolTipText = string.Format("Ubicación:\nLatitud:{0}\nLongitud:{1}", latitud, longitud);
        }

        private void btnSat_Click(object sender, EventArgs e)
        {
            gMapControl1.MapProvider = GMapProviders.GoogleSatelliteMap;
        }

        private void btnOriginal_Click(object sender, EventArgs e)
        {
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
        }

        private void btnRelieve_Click(object sender, EventArgs e)
        {
            gMapControl1.MapProvider = GMapProviders.GoogleTerrainMap;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            trackZoom.Value = Convert.ToInt32(gMapControl1.Zoom);
        }

        private void trackZoom_ValueChanged(object sender, EventArgs e)
        {
            gMapControl1.Zoom = trackZoom.Value;
        }

        private async Task buscarLLAsync(string cad)
        {
            try
            {
                var coordenadas = await _logConversion.ConvertirDireccionALatitudLongitud(cad); // Llamada a ConvertirDireccionALatitudLongitud de logConversion

                if (coordenadas != null)
                {
                    double latitud = coordenadas.Item1;
                    double longitud = coordenadas.Item2;

                    // Aquí es donde debes esperar la obtención de la dirección
                    string desc = await _logConversion.ObtenerDireccion(latitud, longitud);

                    // Utilizar las coordenadas obtenidas como desees, por ejemplo, mostrarlas en TextBoxes
                    Console.WriteLine(latitud.ToString() + longitud.ToString());

                    // Se posicionan en los TextBox de latitud y longitud
                    txtlatitud.Text = latitud.ToString();
                    txtlongitud.Text = longitud.ToString();
                    txtDescripcion.Text = desc;
                }
                else
                {
                    MessageBox.Show("No se pudieron obtener las coordenadas de la dirección especificada.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar las coordenadas: " + ex.Message);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string direccion = txtBusqueda.Text; // Obtener la dirección del TextBox
            _ = buscarLLAsync(direccion);
        }


    }
}
