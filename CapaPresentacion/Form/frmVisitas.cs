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
using CapaPresentación.Formularios;
using MaterialSkin;
using MaterialSkin.Controls;

namespace CapaPresentacion
{
    public partial class frmVisitas : MaterialForm
    {
        public int idLocalidad { get; set; }

        public frmLocalidades InstanciFrmL;
        public frmVisitas()
        {
            InitializeComponent();
            this.MaximizeBox = false; // Desactivar el botón de 
            cargarVisitas();
        }

        private void cargarVisitas() 
        {
            DataTable dtVisitas = logVisitas.Instancia.ListarVisitas(idLocalidad);
            dgvVisitas.DataSource = dtVisitas;
        }

        private void frmVisitas_Load(object sender, EventArgs e)
        {
            
            cargarVisitas();
            PersonalizarDataGridView(dgvVisitas, this);
            groupBox1.Visible = false;

            frmMapa frm = new frmMapa();
            frm.CargarEmpleadosEnComboBox(cboEncargado);

            cboEncargado.Text=logVisitas.Instancia.ObtenerNombreCompletoEmpleadoPorIdLocalidad(idLocalidad);
        }
        public static void PersonalizarDataGridView(DataGridView dgv, Form form)
        {
            // Colores Material Design (puedes ajustarlos a tu gusto)
            Color primaryColor = Color.FromArgb(33, 150, 243); // Azul Material
            Color lightPrimaryColor = Color.FromArgb(100, 181, 246); // Azul claro
            Color accentColor = Color.FromArgb(255, 193, 7); // Amarillo Material
            Color backgroundColor = Color.White;

            dgv.BackgroundColor = backgroundColor;
            dgv.GridColor = Color.FromArgb(224, 224, 224); // Gris claro (dividers)
            dgv.BorderStyle = BorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = primaryColor;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.DefaultCellStyle.SelectionBackColor = accentColor;
            dgv.EnableHeadersVisualStyles = false;

            // Más personalización (opcional)
            dgv.DefaultCellStyle.Font = new Font("Roboto", 10);
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            
            // Alternar filas (opcional)
            dgv.RowsDefaultCellStyle.BackColor = backgroundColor;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = lightPrimaryColor;

            // Ajustar automáticamente el alto de las filas
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // Evitar la edición de celdas
            dgv.ReadOnly = true;

            // Seleccionar fila completa al hacer clic
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Ocultar encabezados de fila (incluido el botón)
            dgv.RowHeadersVisible = false;

            // Bloquear el cambio de tamaño de columnas y filas por el usuario
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;

            dgv.Columns["ID_Visita"].Visible = false;
            dgv.Columns["Fecha_Visita"].HeaderText = "Fecha";
            dgv.Columns["Nombre_Localidad"].HeaderText = "Localidad";
            dgv.Columns["NombreEmpleado"].HeaderText = "Encargado";
            dgv.Columns["Estado"].HeaderText = "Completada";

            // Habilitar ajuste de texto en celdas
            dgv.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            btnAgregar.Visible = false;

            dgvVisitas.Visible = false;
            groupBox1.Visible = true;
            btnUpdate.Visible = false;

            // Obtener el tamaño actual del formulario
            Size tamañoActual = this.Size;

            // Calcular el nuevo tamaño (por ejemplo, aumentar en 50 píxeles en ambas dimensiones)
            Size nuevoTamaño = new Size(tamañoActual.Width, tamañoActual.Height - 60);

            // Establecer el nuevo tamaño del formulario
            this.Size = nuevoTamaño;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // 1. Validación de datos
            if (!DateTime.TryParse(dateTimePicker1.Text, out DateTime fechaVisita))
            {
                MessageBox.Show("Ingrese una fecha de visita válida.");
                return;
            }

            bool estado = false; // Asumimos que tienes un CheckBox para el estado

            // Obtén los IDs de localidad y empleado (ajusta según tus controles)
            
            if (idLocalidad == -1)
            {
                MessageBox.Show("La localidad no existe. Por favor, selecciónela de la lista.");
                return;
            }

            int idEmpleado = logEmleados.Instancia.ObtenerEmpleadoIdPorNombre(cboEncargado.Text);

            // 2. Insertar la visita
            bool exito = logVisitas.Instancia.InsertarVisita(fechaVisita, estado, idLocalidad, idEmpleado);

            // 3. Mostrar resultado
            if (exito)
            {
                MessageBox.Show("Visita registrada exitosamente.");
                // Actualizar el DataGridView si es necesario
                dgvVisitas.DataSource = logVisitas.Instancia.ListarVisitas(idLocalidad);
            }
            else
            {
                MessageBox.Show("Error al registrar la visita. Por favor, inténtelo de nuevo.");
            }


            // Obtener el tamaño actual del formulario
            Size tamañoActual = this.Size;

            // Calcular el nuevo tamaño (por ejemplo, aumentar en 50 píxeles en ambas dimensiones)
            Size nuevoTamaño = new Size(tamañoActual.Width, tamañoActual.Height + 60);

            // Establecer el nuevo tamaño del formulario
            this.Size = nuevoTamaño;

            dgvVisitas.Visible = true;
            groupBox1.Visible = false;
            btnUpdate.Visible = true;

            btnAgregar.Visible = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Obtener el tamaño actual del formulario
            Size tamañoActual = this.Size;

            // Calcular el nuevo tamaño (por ejemplo, aumentar en 50 píxeles en ambas dimensiones)
            Size nuevoTamaño = new Size(tamañoActual.Width, tamañoActual.Height + 60);

            // Establecer el nuevo tamaño del formulario
            this.Size = nuevoTamaño;

            dgvVisitas.Visible = true;
            groupBox1.Visible = false;
            btnUpdate.Visible = true;

            btnAgregar.Visible = true;
        }

        private void dgvVisitas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si se hizo clic en una fila válida (no en el encabezado)
            if (e.RowIndex >= 0)
            {
                // Obtener la fila seleccionada
                DataGridViewRow filaSeleccionada = dgvVisitas.Rows[e.RowIndex];

                // Verificar si la fila está seleccionada
                if (filaSeleccionada.Selected)
                {
                    // Obtener el ID de la visita (asumiendo que está en la primera columna oculta)
                    int idVisita;
                    if (int.TryParse(filaSeleccionada.Cells["ID_Visita"].Value.ToString(), out idVisita))
                    {
                        lblID.Text = idVisita.ToString();
                    }
                    else
                    {
                        // Manejar el caso en que el valor es DBNull
                        MessageBox.Show("El ID de la visita no es válido.");
                    }
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Validar ID de la visita
                if (!int.TryParse(lblID.Text, out int idVisita))
                {
                    return;
                }

                // 2. Obtener el estado actual de la visita
                bool estadoActual;
                try
                {
                    estadoActual = logVisitas.Instancia.ObtenerEstadoVisita(idVisita);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener el estado de la visita: " + ex.Message);
                    return; // Salir si no se puede obtener el estado
                }

                if (estadoActual)
                {
                    MessageBox.Show("La visita ya está marcada como completada.");
                    return;
                }

                // 3. Confirmar la actualización (opcional)
                DialogResult result = MessageBox.Show("¿Está seguro que desea actualizar el estado de la visita?", "Confirmar Actualización", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                {
                    return;
                }

                // 4. Actualizar el estado
                bool exito = logVisitas.Instancia.ActualizarEstadoVisita(idVisita, true);

                // 5. Mostrar resultado y actualizar DataGridView
                if (exito)
                {
                    MessageBox.Show("Estado de la visita actualizado exitosamente.");
                    cargarVisitas();
                }
                else
                {
                    MessageBox.Show("Error al actualizar el estado de la visita. Por favor, inténtelo de nuevo.");
                }
            }
            catch (Exception ex)
            {
                // 6. Manejo de excepciones generales
                MessageBox.Show("Error inesperado al actualizar la visita: " + ex.Message);
                // Puedes registrar el error en un archivo de registro aquí
            }
        }

        private void frmVisitas_FormClosed(object sender, FormClosedEventArgs e)
        {
            InstanciFrmL.EstadoBloqueado(true);
        }
    }
}
