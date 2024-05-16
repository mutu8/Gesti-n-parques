using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaLogica;
using MaterialSkin;
using MaterialSkin.Controls;

namespace CapaPresentacion.ClasesForm
{
    public partial class frmLocalidades : Form
    {

        public frmLocalidades()
        {
            InitializeComponent();
            CargarLocalidades(cboLocalidad);
            //materialSkinManager = MaterialSkinManager.Instance;
            //materiaMaterialSkinManager.Instance.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey900, Accent.Cyan700, TextShade.WHITE);
        }

        private void CargarLocalidades(ComboBox cbo)
        {
            try
            {
                // Llamamos al método en la capa de lógica para obtener las localidades
                DataTable dtLocalidades = logLocalidades.Instancia.ObtenerLocalidades();

                // Verificamos si se obtuvieron datos
                if (dtLocalidades != null && dtLocalidades.Rows.Count > 0)
                {
                    // Limpiamos el ComboBox antes de agregar nuevos elementos
                    cbo.Items.Clear();

                    // Recorremos los datos y los agregamos al ComboBox
                    foreach (DataRow row in dtLocalidades.Rows)
                    {
                        string nombreLocalidad = row["Nombre_Localidad"].ToString();

                        // Agregamos el nombre de la localidad al ComboBox
                        cbo.Items.Add(nombreLocalidad);
                    }

                    // Opcional: Seleccionar el primer elemento en el ComboBox
                    if (cbo.Items.Count > 0)
                    {
                        cbo.SelectedIndex = 0;
                    }
                    cbo.SelectedIndex = -1;
                }
                else
                {
                    // Manejo si no se obtuvieron datos
                    MessageBox.Show("No se encontraron localidades.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                Console.WriteLine("Error al cargar localidades: " + ex.Message);
            }
        }

    }
}
