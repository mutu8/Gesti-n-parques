using CapaLogica;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MaterialSkin.Controls;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmImagen : MaterialForm
    {
        private static Cloudinary cloudinary;
        private string imagePath; // Variable para almacenar la ruta de la imagen seleccionada

        public frmLocalidades InstanciFrmL;

        private const string CLOUD_NAME = "do1flp47t";
        private const string API_KEY = "663888233627688";
        private const string API_SECRET = "yAIwkX4Apn-uBs4uHZSzVPGxil8";
        public string ImageUrl { get; set; }
        public int idLocalidad { get; set; }

        private void InitializeCloudinary()
        {
            Account account = new Account(CLOUD_NAME, API_KEY, API_SECRET);
            cloudinary = new Cloudinary(account);
        }
        
        public frmImagen()
        {
            InitializeComponent();
            InitializeCloudinary();
            this.MaximizeBox = false; // Desactivar el botón de maximizar

            toolTip1.SetToolTip(btnAbrir, "Cargar imagen local");
            toolTip1.SetToolTip(btnEliminar, "Borrar imagen local");
            toolTip1.SetToolTip(btnGuardar, "Guardar imagen en la nube");

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
                        //pictureBox.SizeMode = PictureBoxSizeMode.Normal;
                        pictureBox.Image = imagen;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar la imagen: " + ex.Message);
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
                    MessageBox.Show("La ruta de la imagen está vacía.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ImageUrl = ""; // Establecer la URL de la imagen en vacío
                    return; // Salir del método si la ruta está vacía
                }

                // Obtener la fecha y hora actuales
                string currentDate = DateTime.Now.ToString("yyyyMMdd_HHmmss");

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
                    MessageBox.Show("Imagen subida exitosamente. URL: " + uploadResult.SecureUrl, "Subida Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error al subir la imagen. Código de estado: " + uploadResult.StatusCode, "Error al subir", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            MessageBox.Show("Imagen eliminada del servicio en la nube.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Error al eliminar la imagen del servicio en la nube. Código de estado: " + deletionResult.StatusCode, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se pudo obtener el identificador público de la URL proporcionada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No se proporcionó una URL válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            catch
            {
                return null;
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            uploadImage(imagePath);
        }

        private void EliminarImagenPictureBox()
        {
            if (ImgCli.Image != null)
            {
                // Elimina la imagen del formulario
                ImgCli.Image = null;

                // Restablece la ruta de la imagen a vacía después de eliminarla
                imagePath = string.Empty;

                MessageBox.Show("Imagen eliminada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No hay ninguna imagen para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarImagenPictureBox();
        }


        private void btnFoto_Click(object sender, EventArgs e)
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

        private void frmImagen_Load(object sender, EventArgs e)
        {
            CargarImagenDesdeUrl(ImageUrl, ImgCli);
            this.Text = logLocalidades.Instancia.ObtenerNombreLocPorID(idLocalidad);
        }

        private void btnEliminarNube_Click(object sender, EventArgs e)
        {
            DeleteImageByUrl(ImageUrl);
        }

        private void frmImagen_FormClosed(object sender, FormClosedEventArgs e)
        {
            InstanciFrmL.EstadoBloqueado(true);
        }
    }
}
