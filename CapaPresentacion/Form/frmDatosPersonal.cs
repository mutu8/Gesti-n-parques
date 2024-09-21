using CapaLogica;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MaterialSkin.Controls;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmDatosPersonal : MaterialForm
    {
        public string textoBoton { get; set; } // Ya lo tienes
        private readonly logEmleados _logEmpleados;

        //Img

        private static Cloudinary cloudinary;
        private string imagePath; // Variable para almacenar la ruta de la imagen seleccionada

        public frmLocalidades InstanciFrmL;

        private const string CLOUD_NAME = "do1flp47t";
        private const string API_KEY = "663888233627688";
        private const string API_SECRET = "yAIwkX4Apn-uBs4uHZSzVPGxil8";


        public frmPersonal InstanciFrmE;

        // Nuevas propiedades con get y set
        public string Nombre
        {
            get { return txtNombre.Text; }
            set { txtNombre.Text = value; }
        }

        public string Apellido
        {
            get { return txtApellidos.Text; }
            set { txtApellidos.Text = value; }
        }

        public string Rol
        {
            get { return cboRol.Text; }
            set { cboRol.Text = value; }
        }
        public bool esApoyo
        {
            get; set;
        }

        // Nuevas propiedades
        public DateTime FechaNacimiento
        {
            get { return dateTimePicker1.Value; }
            set { dateTimePicker1.Value = value; }
        }
        public string DireccionCorreo
        {
            get { return txtCorreo.Text; }
            set { txtCorreo.Text = value; }
        }

        public string DNI
        {
            get { return txtDNI.Text; }
            set { txtDNI.Text = value; }
        }

        // Propiedad para acceder al CheckBox
        public bool EsPersonalLimpieza
        {
            get { return materialCheckbox1.Checked; }
            set { materialCheckbox1.Checked = value; }
        }


        public string ImageUrl { get; set; }
        public int idEmpleado { get; set; }


        public frmDatosPersonal()
        {
            InitializeComponent();
            cargarCBO();

            // Establecer el máximo del DateTimePicker
            dateTimePicker1.MinDate = new DateTime(1970, 12, 31);
            dateTimePicker1.MaxDate = DateTime.Today.AddYears(-18);


            Account account = new Account(CLOUD_NAME, API_KEY, API_SECRET);
            cloudinary = new Cloudinary(account);

            this.MaximizeBox = false;
        }
        private void CargarImagenDesdeUrl(string url, PictureBox pictureBox)
        {
            if (string.IsNullOrEmpty(url))
            {
                // No hacer nada si el URL es nulo o vacío
                return;
            }

            using (WebClient clienteWeb = new WebClient())
            {
                try
                {
                    // Descargar la imagen como un arreglo de bytes
                    byte[] bytesImagen = clienteWeb.DownloadData(url);

                    // Convertir el arreglo de bytes en una imagen
                    using (MemoryStream stream = new MemoryStream(bytesImagen))
                    {
                        Image imagen = Image.FromStream(stream);

                        // Establecer la imagen en el PictureBox
                        pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox.Image = imagen;
                    }
                }
                catch (Exception)
                {
                    // Manejo de excepciones sin mostrar MessageBox
                }
            }
        }

        public void uploadImage(string imagePath)
        {
            try
            {
                // Verificar si la ruta de la imagen está vacía
                if (string.IsNullOrEmpty(imagePath))
                {
                    // Si la URL de la imagen no está vacía, eliminar la imagen de Cloudinary
                    if (!string.IsNullOrEmpty(ImageUrl))
                    {
                        DeleteImageByUrl(ImageUrl);
                    }

                    // Asignar una cadena vacía a ImageUrl
                    ImageUrl = "";

                    return; // Salir del método
                }

                // Obtener la fecha actual
                string currentDate = DateTime.Now.ToString("yyyyMMdd");

                // Crear el nombre del archivo con el nombre específico y la fecha actual
                string fileName = $"{this.Text}_{currentDate}{Path.GetExtension(imagePath)}";

                // Verificar si la carpeta específica existe, si no, crearla
                var folderPath = $"{this.Text}";
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(imagePath),
                    PublicId = $"{folderPath}/{fileName}" // Especificar la carpeta y el nombre del archivo
                };

                // Intentar subir la imagen a Cloudinary
                var uploadResult = cloudinary.Upload(uploadParams);

                if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    // Actualizar la URL de la imagen con la URL segura de Cloudinary
                    ImageUrl = uploadResult.SecureUrl.ToString();
                }
            }
            catch (Exception)
            {
                // Manejo de excepciones sin mostrar MessageBox
            }
        }



        public void DeleteImageByUrl(string imageUrl)
        {
            try
            {
                // Si la URL de la imagen no está vacía
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    // Extraer el identificador público de la URL de la imagen
                    var publicId = ExtractPublicIdFromUrl(imageUrl);

                    // Si el identificador público no está vacío, intenta eliminar la imagen existente
                    if (!string.IsNullOrEmpty(publicId))
                    {
                        var deletionParams = new DeletionParams(publicId);
                        var deletionResult = cloudinary.Destroy(deletionParams);

                        if (deletionResult.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            // Imagen eliminada exitosamente
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Manejo de excepciones sin mostrar MessageBox
            }
        }

        private string ExtractPublicIdFromUrl(string imageUrl)
        {
            try
            {
                // Asumiendo que la URL de Cloudinary tiene la forma: http://res.cloudinary.com/<cloud_name>/image/upload/<public_id>.<extension>
                var uri = new Uri(imageUrl);
                var segments = uri.Segments;
                var publicIdWithExtension = segments.Last(); // obtiene la parte final de la URL
                var publicId = Path.GetFileNameWithoutExtension(publicIdWithExtension); // quita la extensión

                return publicId;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void cargarCBO()
        {
            cboRol.Items.Add("Apoyo");
            cboRol.Items.Add("728");
        }
        public string CargoInverso(bool esApoyo)
        {
            return esApoyo ? "Apoyo" : "728";
        }
        private void frmDatosPersonal_Load(object sender, EventArgs e)
        {
            btnAccion.Text = textoBoton;

            if (btnAccion.Text == "")
            {
                txtNombre.Enabled = false;
                txtApellidos.Enabled = false;
                txtDNI.Enabled = false;
                txtCorreo.Enabled = false;
                btnAbrir.Enabled = false;
                btnEliminar.Enabled = false;
                btnGuardar.Enabled = false;
                cboRol.Enabled = false;
                dateTimePicker1.Enabled = false;
                materialCheckbox1.Enabled = false;
            }
            else if(btnAccion.Text == "Guardar")
            {
                materialCheckbox1.Enabled = false;

            }


            CargarImagenDesdeUrl(ImageUrl, ImgCli);
        }

        private bool Cargo(ComboBox cbo)
        {
            if (cbo.Text == "Apoyo") { return true; }
            else return false;
        }

        private void btnAccion_Click(object sender, EventArgs e)
        {
            // 1. Obtén los valores de los controles (TextBox, etc.)
            string nombres = txtNombre.Text.Trim();
            string apellidos = txtApellidos.Text.Trim();
            bool esApoyo = Cargo(cboRol);
            string correo = txtCorreo.Text.Trim();
            string DNI = txtDNI.Text.Trim();
            string url = ImageUrl ?? ""; // Si ImageUrl es null, asigna una cadena vacía ("")

            // Obtener el valor del CheckBox de esPersonalLimpieza
            bool esPersonalLimpieza = materialCheckbox1.Checked;

            //esPersonalLimpieza a texto
            string esPersonal = esPersonalLimpieza ? "Limpieza" : "Parques";

            DateTime fechaNacimiento = dateTimePicker1.Value;

            // Obtener el nombre completo del empleado desde los TextBox
            string nombreCompleto = txtNombre.Text.Trim() + " " + txtApellidos.Text.Trim();

            // 1. Obtener los valores del formulario
            int empleadoId = logEmleados.Instancia.ObtenerEmpleadoIdPorNombre(nombreCompleto); // Implementa esta función

            if (btnAccion.Text == "Agregar")
            {
                try
                {
                    // 2. Validaciones (¡IMPORTANTE!)
                    if (string.IsNullOrWhiteSpace(nombres) || string.IsNullOrWhiteSpace(apellidos))
                    {
                        return; // Detener el proceso si falta información
                    }

                    // 3. Usar logEmpleados para insertar (lógica de negocio)
                    logEmleados.Instancia.InsertarEmpleado(nombres, apellidos, esApoyo, correo, url, DNI, esPersonalLimpieza);
                    //empleadoId = logEmleados.Instancia.ObtenerEmpleadoIdPorNombre(nombreCompleto); // Implementa esta función

                    // 3. Usar logEmpleados para modificar (lógica de negocio)
                    logEmleados.Instancia.ModificarEmpleado(empleadoId, esApoyo, correo, url, DNI, fechaNacimiento);

                    if (materialCheckbox1.Checked)
                    {
                        int nvID = logEmleados.Instancia.ObtenerEmpleadoIdPorNombre(nombres + " " + apellidos);

                        logEmleados.Instancia.ActualizarEstadoEsPersonal(nvID);
                    }

                    // 4. Limpiar controles
                    txtNombre.Clear();
                    txtApellidos.Clear();
                    txtCorreo.Clear();
                    txtDNI.Clear();
                    dateTimePicker1.CustomFormat = " ";
                    dateTimePicker1.Format = DateTimePickerFormat.Custom;
                    cboRol.SelectedIndex = -1;


                   
                    InstanciFrmE.seDebeActualizar = true;
                    //Solo cargar is esPersonal es "Limpieza"
                    if(esPersonal == "Limpieza")
                    {
                        InstanciFrmE.setComboBox("Limpieza");
                        InstanciFrmE.CargarUserControlsEmpleados("", "Limpieza");

                    }
                    else
                    {
                        InstanciFrmE.setComboBox("Parques");
                        InstanciFrmE.CargarUserControlsEmpleados("", "Parques");
                    }

                }
                catch (Exception)
                {
                    // Manejo de excepciones sin mostrar MessageBox
                }
            }
            else if (btnAccion.Text == "Guardar")
            {
                try
                {
                    // 3. Usar logEmpleados para modificar (lógica de negocio)
                    logEmleados.Instancia.ModificarEmpleado(empleadoId, esApoyo, correo, url, DNI, fechaNacimiento);

                    InstanciFrmE.seDebeActualizar = true;

                    //Solo cargar is esPersonal es "Limpieza"
                    if (esPersonal == "Limpieza")
                    {
                        InstanciFrmE.CargarUserControlsEmpleados("", "Limpieza");

                    }
                    else
                    {
                        InstanciFrmE.CargarUserControlsEmpleados("", "Parques");
                    }
                }
                catch (Exception)
                {
                    // Manejo de excepciones sin mostrar MessageBox
                }
            }
        }

        private void frmDatosPersonal_FormClosing(object sender, FormClosingEventArgs e)
        {
            InstanciFrmE.EstadoBloqueado(true);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            uploadImage(imagePath);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ImgCli.Image != null)
                {
                    // Elimina la imagen del formulario
                    ImgCli.Image = null;

                    // Restablece la ruta de la imagen a vacía después de eliminarla
                    imagePath = string.Empty;
                }
            }
            catch (Exception)
            {
                // Manejo de excepciones sin mostrar MessageBox
            }
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fo = new OpenFileDialog();
                DialogResult rs = fo.ShowDialog();
                if (rs == DialogResult.OK)
                {
                    ImgCli.Image = Image.FromFile(fo.FileName);
                    // Guarda la ruta de la imagen seleccionada
                    imagePath = fo.FileName;
                }
            }
            catch (Exception)
            {
                // Manejo de excepciones sin mostrar MessageBox
            }
        }

        // Método para verificar si una cadena está compuesta solo por dígitos numéricos
        private bool EsNumero(string cadena)
        {
            foreach (char c in cadena)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }


        private void txtDNI_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Verificar si el texto tiene más de 8 caracteres
                if (txtDNI.Text.Length > 8)
                {
                    // Si tiene más de 8 caracteres, truncar el texto
                    txtDNI.Text = txtDNI.Text.Substring(0, 8);
                }

                // Verificar si todos los caracteres son números
                if (!string.IsNullOrEmpty(txtDNI.Text) && !EsNumero(txtDNI.Text))
                {
                    // Si no son números, limpiar el campo
                    txtDNI.Text = string.Empty;
                }
            }
            catch (Exception)
            {
                // Manejo de excepciones sin mostrar MessageBox
            }
        }

        private void txtCorreo_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}