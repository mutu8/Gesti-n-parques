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
using CapaEntidad;
using static DevExpress.Utils.Drawing.Helpers.NativeMethods;

namespace CapaPresentación.Formularios
{
    public partial class frmMapa : MaterialForm
    {
        GMarkerGoogle marker;
        GMapOverlay markerOverlay;
        private readonly logConversion _logConversion;

        private double _latInicial = -8.103034453133846;
        private double _lngInicial = -79.01766201952019;

        public double LatInicial
        {
            get { return _latInicial; }
            set { _latInicial = value; }
        }

        public double LngInicial
        {
            get { return _lngInicial; }
            set { _lngInicial = value; }
        }

        public string textoBoton { get; set; }
        public string Nombre_Localidad { get; set; }
        public string Descripcion { get; set; }
        public string Direccion { get; set; }
        public string Referencias { get; set; }
        public string Urbanizacion { get; set; }
        public string Jiron { get; set; }
        public string Manzana { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string ImageUrl { get; set; }
 
        public frmMapa()
        {
            InitializeComponent();
            this.MaximizeBox = false; // Desactivar el botón de maximizar

            // Inicializar _logConversion
            _logConversion = logConversion.Instancia;

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
            gMapControl1.Zoom = 20;
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

            btnAgregar.Text = textoBoton;

            // Mostrar los valores pasados
            txtNombre.Text = Nombre_Localidad;
            txtDescripcion.Text = Descripcion;
            txtDireccion.Text = Direccion;
            txtReferencia.Text = Referencias;
            txtUrbanizacion.Text = Urbanizacion;
            txtJiron.Text = Jiron;
            txtManzana.Text = Manzana;
            txtlatitud.Text = Latitud.ToString();
            txtlongitud.Text = Longitud.ToString();
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
            //string direccion = await ObtenerYMostrarDireccion(latitud, longitud);
            //txtDireccion.Text = direccion;

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
                txtBusqueda.Text = "";
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
                    txtDireccion.Text = desc;

                    // Mover el marcador al nuevo lugar y actualizar el mapa
                    marker.Position = new GMap.NET.PointLatLng(latitud, longitud);
                    marker.ToolTipText = string.Format("Ubicación:\nLatitud:{0}\nLongitud:{1}", latitud, longitud);

                    // Centrar el mapa en la nueva posición del marcador
                    gMapControl1.Position = new GMap.NET.PointLatLng(latitud, longitud);
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


        private void btnBusqueda_MouseClick(object sender, MouseEventArgs e)
        {
            string direccion = txtBusqueda.Text; // Obtener la dirección del TextBox
            _ = buscarLLAsync(direccion);
        }

        private void LimpiarTextBoxes()
        {
            foreach (Control control in Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Clear();
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Obtener los valores de los controles antes de abrir el formulario de imagen
            string nombre = txtNombre.Text;
            string descripcion = txtDescripcion.Text;
            string direccion = txtDireccion.Text;
            string referencia = txtReferencia.Text;
            string urbanizacion = txtUrbanizacion.Text;
            string jiron = txtJiron.Text;
            string manzana = txtManzana.Text;
            decimal latitud = Convert.ToDecimal(txtlatitud.Text);
            decimal longitud = Convert.ToDecimal(txtlongitud.Text);
            
            // La acción específica para el botón "Agregar" se realizará dentro del case correspondiente
                
            switch (btnAgregar.Text)   
            {
                    case "Agregar":

                        frmImagen f = new frmImagen();
                        f.Show();

                        // Manejar el evento FormClosing para obtener el URL de la imagen y luego insertar los detalles
                        f.FormClosing += (senderForm, eFormClosing) =>
                        {
                            // Cuando el formulario se está cerrando, obtén el URL de la imagen
                            string url = f.ImageUrl;

                            // Insertar los detalles de localidades y localidades utilizando los valores almacenados previamente
                            logLocalidades.Instancia.InsertarDetallesLocalidadesYLocalidades(
                                nombre,
                                descripcion,
                                direccion,
                                referencia,
                                urbanizacion,
                                jiron,
                                manzana,
                                latitud,
                                longitud,
                                url);

                            MessageBox.Show("Agregado correctamente!!");
                            LimpiarTextBoxes();
                        };

                        break;
             }
            
        }

        private void materialFloatingActionButton1_Click(object sender, EventArgs e)
        {
            // Intentar abrir el formulario frmMapa usando la función general
            FormUtil.TryOpenForm(() =>
            {
                var frmMapa = new frmMapa();
                frmMapa.textoBoton = "Agregar";
                frmMapa.StartPosition = FormStartPosition.CenterScreen;
                return frmMapa;
            });

            // Capturar la imagen del control de mapa
            Image imagenMapa = gMapControl1.ToImage();

            // Mostrar un diálogo de guardado de archivos
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Archivos de imagen (*.png)|*.png|Todos los archivos (*.*)|*.*";
            saveDialog.Title = "Guardar imagen del mapa";
            saveDialog.FileName = "mapa_capturado";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                // Guardar la imagen en la ubicación seleccionada por el usuario
                imagenMapa.Save(saveDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        private void gMapControl1_Load(object sender, EventArgs e)
        {

        }
    }
}
