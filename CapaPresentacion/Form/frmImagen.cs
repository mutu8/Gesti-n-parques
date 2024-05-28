using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MaterialSkin;
using MaterialSkin.Controls;

namespace CapaPresentacion
{
    public partial class frmImagen : MaterialForm
    {
        private static Cloudinary cloudinary;
        private string imagePath; // Variable para almacenar la ruta de la imagen seleccionada

        public double idLocalidad
        {
            get { return idLocalidad; }
            set { idLocalidad = value; }
        }

        private const string CLOUD_NAME = "do1flp47t";
        private const string API_KEY = "663888233627688";
        private const string API_SECRET = "yAIwkX4Apn-uBs4uHZSzVPGxil8";
        public string ImageUrl { get; set; }

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
                // Sube la nueva imagen con el mismo nombre de archivo
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(imagePath)
                };

                var uploadResult = cloudinary.Upload(uploadParams);

                if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    ImageUrl = uploadResult.SecureUrl.ToString();
                    MessageBox.Show("Image uploaded successfully. URL: " + uploadResult.SecureUrl, "Upload Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Image upload failed. Status code: " + uploadResult.StatusCode, "Upload Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (!string.IsNullOrEmpty(imagePath))
            {
                uploadImage(imagePath);
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ninguna imagen.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void EliminarImagenPictureBox()
        {
            if (ImgCli.Image != null)
            {
                // Elimina la imagen del formulario
                ImgCli.Image = null;

                // Restablece la ruta de la imagen a vacía después de eliminarla
                imagePath = string.Empty;

                MessageBox.Show("Imagen eliminada del formulario.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        }

        private void btnEliminarNube_Click(object sender, EventArgs e)
        {
            DeleteImageByUrl(ImageUrl);
        }

        private void frmImagen_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
