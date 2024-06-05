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


namespace CapaPresentación.Formularios
{
    public partial class frmMapa : MaterialForm
    {
        GMarkerGoogle marker;
        GMapOverlay markerOverlay;
        private readonly logConversion _logConversion;

        private double _latInicial = -8.103034453133846;
        private double _lngInicial = -79.01766201952019;
        
        frmLocalidades frm = new frmLocalidades();
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
        public string Sector { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string ImageUrl { get; set; }
        public string Empleado { get; set; }
        public int idEmpleado {  get; set; }
        public frmMapa()
        {
            InitializeComponent();
            this.MaximizeBox = false; // Desactivar el botón de maximizar

            // Inicializar _logConversion
            _logConversion = logConversion.Instancia;
            CargarComboBoxUrbanizaciones();
            CargarComboBoxSectores();
        }

        private void CargarComboBoxUrbanizaciones()
        {
            // Lista completa de urbanizaciones
            List<string> urbanizaciones = new List<string>
            {
                "Centro Histórico", "San Andrés", "El Recreo", "Mansiche", "Las Capullanas", "Covicorti", "Primavera",
                "Huerta Grande", "Los Cedros", "La Intendencia", "Santa María", "Las Casuarinas", "La Arboleda", "Pay Pay",
                "Los Granados", "Los Portales", "Andrés Rázuri", "Los Rosales de San Andrés", "Galeno", "La Esmeralda",
                "Santo Dominguito", "Torres Araujo", "Santa Isabel", "Monserrate", "San Salvador", "Trupal", "Santa Inés",
                "Las Quintanas", "Miraflores", "Mochica", "Aranjuez", "Chicago", "Los Pinos", "San Eloy", "Santa Teresa de Ávila",
                "Chimú", "Huerta Bella", "Vista Bella", "La Noria", "UPAO", "San Isidro", "Libertad", "La Merced", "La Perla",
                "El Alambre", "20 de abril", "San Fernando", "Los Naranjos", "Los Jardines", "El Molino", "Palermo", "El Sol",
                "Vista Hermosa", "Ingeniería", "Daniel Hoyle", "La Rinconada", "Jorge Chávez", "El Bosque", "Independencia",
                "San Luis", "San Vicente"
            };

            // Limpiar el ComboBox
            cboUrb.Items.Clear();

            // Cargar los datos
            cboUrb.Items.AddRange(urbanizaciones.ToArray());
        }
        public void CargarEmpleadosEnComboBox(ComboBox comboBox)
        {
            try
            {
                DataTable dtEmpleados = logEmleados.Instancia.ObtenerTodosLosEmpleados();

                if (dtEmpleados != null && dtEmpleados.Rows.Count > 0)
                {
                    // Agregar una nueva columna para el nombre completo
                    dtEmpleados.Columns.Add("NombreCompleto", typeof(string));

                    foreach (DataRow row in dtEmpleados.Rows)
                    {
                        string nombres = row["Nombres"] != DBNull.Value ? row["Nombres"].ToString().Trim() : string.Empty;
                        string apellidos = row["Apellidos"] != DBNull.Value ? row["Apellidos"].ToString().Trim() : string.Empty;
                        row["NombreCompleto"] = $"{nombres} {apellidos}".Trim();
                    }

                    comboBox.DisplayMember = "NombreCompleto"; // Mostrar el nombre completo
                    comboBox.ValueMember = "ID_Empleado";     // Valor asociado (ID del empleado)
                    comboBox.DataSource = dtEmpleados;
                }
                comboBox.SelectedIndex = -1; // Opcional: seleccionar ningún elemento al inicio
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los empleados: " + ex.Message);
                // Puedes agregar un registro de error aquí si lo deseas
            }
        }

        private void CargarComboBoxSectores()
        {
            List<string> sectores = new List<string>
            {
                "Liberación Social",
                "San Andrés V Etapa",
                "Las Flores",
                "San Andrés - Costado de Paseo de Aguas",
                "California",
                "Huaman",
                "Las Hortenzias",
                "Las Flores - El Golf",
                "Palmeras y Palmas del Golf",
                "San Vicente",
                "Vista Alegre",
                "Golf - Primera Etapa"
            };

            cboSector.Items.Clear();
            cboSector.Items.AddRange(sectores.ToArray());
        }

        private int conversorNombreEmpleado()
        {
            return logEmleados.Instancia.ObtenerEmpleadoIdPorNombre(cboEncargado.Text);
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
            CargarEmpleadosEnComboBox(cboEncargado);

            // Mostrar los valores pasados
            txtNombre.Text = Nombre_Localidad;
            txtDescripcion.Text = Descripcion;
            txtDireccion.Text = Direccion;
            txtReferencia.Text = Referencias;
            cboUrb.SelectedItem = Urbanizacion;
            cboSector.SelectedItem = Sector;
            txtlatitud.Text = Latitud.ToString();
            txtlongitud.Text = Longitud.ToString();
            cboEncargado.Text = logEmleados.Instancia.ObtenerNombrePorID(idEmpleado); ;
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
        // Método para insertar detalles de localidades y localidades
        void InsertarDetallesConImagen(string nombre, string descripcion, string referencia, string urbanizacion, string sector, string direccion, decimal latitud, decimal longitud, string urlImagen)
        {
            // Validar que los campos obligatorios no estén vacíos
            if (!string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(direccion) && sector != null)
            {
                // Insertar detalles solo si los campos obligatorios tienen valor
                logLocalidades.Instancia.InsertarDetallesLocalidadesYLocalidades(
                    nombre,
                    descripcion,
                    referencia,
                    urbanizacion,
                    sector,
                    direccion,
                    latitud,
                    longitud,
                    conversorNombreEmpleado(),
                    urlImagen);

                MessageBox.Show("Agregado correctamente!!");
                LimpiarTextBoxes();
            }
            else
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.");
            }
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Obtener los valores de los controles antes de abrir el formulario de imagen
            string nombre = txtNombre.Text;
            string descripcion = txtDescripcion.Text;
            string direccion = txtDireccion.Text;
            string referencia = txtReferencia.Text;
            string urbanizacion = cboUrb.Text;
            string sector = cboSector.Text;
            decimal latitud = Convert.ToDecimal(txtlatitud.Text);
            decimal longitud = Convert.ToDecimal(txtlongitud.Text);
            int idEmpleado = conversorNombreEmpleado();
            
            switch (btnAgregar.Text)   
            {
                case "Agregar":

                    // Preguntar al usuario si quiere agregar una imagen
                    DialogResult result = MessageBox.Show("¿Desea agregar una imagen?", "Agregar Imagen", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        // Si el usuario quiere agregar una imagen, abrir el formulario de imagen
                        frmImagen f = new frmImagen();
                        f.Show();

                        // Manejar el evento FormClosing para obtener el URL de la imagen y luego insertar los detalles
                        f.FormClosing += (senderForm, eFormClosing) =>
                        {
                            // Cuando el formulario se está cerrando, obtén el URL de la imagen
                            string url = f.ImageUrl;

                            // Insertar detalles con el URL de la imagen obtenido
                            InsertarDetallesConImagen(nombre, descripcion, referencia, urbanizacion, sector, direccion, latitud, longitud, url);
                            frm.RecargarPanel();
                        };
                    }
                    else if (result == DialogResult.No)
                    {
                        // Si el usuario no quiere agregar una imagen, insertar los detalles sin abrir el formulario de imagen
                        InsertarDetallesConImagen(nombre, descripcion, referencia, urbanizacion, sector, direccion, latitud, longitud, null); // Pasar null como URL de imagen
                        frmLocalidades f = new frmLocalidades();
                    }

                    break;


                case "Guardar":
                    try
                    {
                        // Obtener el ID de la localidad y el ID del detalle de localidad
                        (int idLocalidad, int idDetalleLocalidad) = logLocalidades.Instancia.ObtenerId(Nombre_Localidad, Direccion);
                        
                        //Actualizar los detalles de localidades en la base de datos
                        logLocalidades.Instancia.ActualizarDetallesLocalidades(idDetalleLocalidad, nombre, descripcion, referencia, urbanizacion, sector, direccion, latitud, longitud, idEmpleado);

                        MessageBox.Show("ACTUALIZADO CORRECTAMENTE!!");
                        frmLocalidades f = new frmLocalidades();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al actualizar: " + ex.Message);
                    }
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
                frm.RecargarPanel();
            }
        }

        private void gMapControl1_Load(object sender, EventArgs e)
        {

        }

        private void cboEncargado_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
    }
}
