using CapaLogica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmComapactasAsistencia : Form
    {
        private ToolTip dgvToolTip = new ToolTip();

        public frmComapactasAsistencia()
        {
            InitializeComponent();
        }

        private void FormComapactasAsistencia_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridViewConEstilo(dataGridView1);
            ConfigurarDataGridViewConComboBox(dataGridView1);
            dataGridView1.CellMouseEnter += DataGridView1_CellMouseEnter;
            //dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;  // Suscribirse al evento CellValueChanged

        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Verificar si todas las celdas de la fila están llenas
                bool filaCompleta = true;
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value == null || string.IsNullOrEmpty(cell.Value.ToString()))
                    {
                        filaCompleta = false;
                        break;
                    }
                }

                // Si la fila está completa y no es una nueva fila, añadir una nueva fila
                if (filaCompleta && e.RowIndex == dataGridView1.Rows.Count - 1)
                {
                    dataGridView1.Rows.Add();
                }
            }
        }

        public void ConfigurarDataGridViewConComboBox(DataGridView dgv)
        {
            // Limpiar las columnas y filas existentes
            dgv.Columns.Clear();
            dgv.Rows.Clear();

            // Crear columna para Placas
            DataGridViewComboBoxColumn colPlacas = new DataGridViewComboBoxColumn();
            colPlacas.HeaderText = "Placas";
            colPlacas.Name = "Placas";
            colPlacas.DataSource = logEmleados.Instancia.ObtenerPlacas();  // Método que obtiene las placas desde la capa lógica
            colPlacas.DisplayMember = "PlacaVehicular";
            colPlacas.ValueMember = "PlacaVehicular";
            colPlacas.FillWeight = 15; // Proporción relativa
            dgv.Columns.Add(colPlacas);

            // Crear columna para Empleados Compacta
            DataGridViewComboBoxColumn colEmpleados = new DataGridViewComboBoxColumn();
            colEmpleados.HeaderText = "Empleados Compacta";
            colEmpleados.Name = "Empleados";
            colEmpleados.DataSource = logEmleados.Instancia.ListarEmpleadosQueSeanComapacta();  // Método que obtiene los empleados desde la capa lógica
            colEmpleados.DisplayMember = "NombreCompleto";  // Asegúrate de que esta columna exista en tu DataTable
            colEmpleados.ValueMember = "ID_Empleado";  // Asegúrate de que esta columna exista en tu DataTable
            colEmpleados.FillWeight = 25; // Proporción relativa
            dgv.Columns.Add(colEmpleados);

            // Crear columna para Zona
            DataGridViewComboBoxColumn colZona = new DataGridViewComboBoxColumn();
            colZona.HeaderText = "Zona";
            colZona.Name = "Zona";
            colZona.Items.AddRange(
                "VISTA ALEGRE (TODO LARCO DESDE LA VIRGEN DE FÁTIMA HASTA EL LOCAL DE LARCO)",
                "HUAMAN –TUPAC AMARU (DESDE LA SIRENA HASTA PLANTA DE ASFALTO)",
                "LOS SAUCES – SAN ANDRES (AV. LOS ÁNGELES HASTA HUAMÁN)",
                "BS. AS. CENTRO – NORTE (SOCCER CITY HASTA LA AV. HUAMÁN TABERNA)",
                "BUENOS AIRES SUR (PAUJILES, AV. JUAN PABLO HASTA LA 2 DE MAYO)",
                "ENCALADA – FATIMA VALLE PALMERAS SAN ANDRES - CORTIJO BAJO (DESDE LA VIRGEN, AV. FÁTIMA HASTA LA ENCALADA)",
                "URB. EL GOLF (desde La Virgen de Fátima hasta Prolong. César Vallejo, Taberna, Av. Huamán hasta planta de Asfalto)",
                "URB. CALIFORNIA (Desde la Virgen, Av. Larco, Bolivia hasta la poza, Montevideo)",
                "URB. LAS FLORES (Los Ángeles, Ovalo Marinera)",
                "APOYO – AVENIDA (Soccer City, Av. Huamán, Taberna, Mercado Vista Alegre)"
            );
            colZona.FillWeight = 35; // Proporción relativa
            dgv.Columns.Add(colZona);

            // Crear columna para Turno
            DataGridViewComboBoxColumn colTurno = new DataGridViewComboBoxColumn();
            colTurno.HeaderText = "Turno";
            colTurno.Name = "Turno";
            colTurno.Items.AddRange("Día", "Tarde", "Noche");
            colTurno.FillWeight = 15; // Proporción relativa
            dgv.Columns.Add(colTurno);

            // Crear columna para Cargo
            DataGridViewComboBoxColumn colCargo = new DataGridViewComboBoxColumn();
            colCargo.HeaderText = "Cargo";
            colCargo.Name = "Cargo";
            colCargo.Items.AddRange("Conductor", "Ayudante");
            colCargo.FillWeight = 10; // Proporción relativa
            dgv.Columns.Add(colCargo);

            // Agregar una fila inicial al DataGridView
            dgv.Rows.Add();

            // Vincular el evento EditingControlShowing para capturar los cambios en los ComboBox
            dgv.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dgv_EditingControlShowing);
        }

        private void CargarVisitasCompactasEnDataGridView(DataGridView dgv)
        {
            // Obtener los datos de las visitas compactas del día desde la capa lógica
            DataTable dtVisitasCompactas = logEmleados.Instancia.ObtenerVisitasCompactasDeHoy();

            // Asignar el DataTable al DataGridView
            dgv.DataSource = dtVisitasCompactas;

            // Ocultar las columnas de ID
            dgv.Columns["ID_VisitaCompacta"].Visible = false;
            dgv.Columns["ID_DetallePersonalCompacta"].Visible = false;
            dgv.Columns["ID_Empleado"].Visible = false;
            dgv.Columns["Fecha_Visita"].Visible = false;

            // Renombrar las columnas visibles con nombres más descriptivos
            dgv.Columns["Completada"].HeaderText = "Visita Completada";
            dgv.Columns["Zona"].HeaderText = "Zona de Trabajo";
            dgv.Columns["Turno"].HeaderText = "Turno de Trabajo";
            dgv.Columns["esConductor"].HeaderText = "Es conductor";
            dgv.Columns["NombreCompleto"].HeaderText = "Empleado";
            dgv.Columns["PlacaVehicular"].HeaderText = "Placa Vehicular";

            // Deshabilitar la edición directa de las filas
            dgv.ReadOnly = true;

            // Asegurarse de que los ComboBox sean visibles pero no editables
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                if (column is DataGridViewComboBoxColumn)
                {
                    column.ReadOnly = true;
                }
            }
        }



        // Método para manejar el evento EditingControlShowing y capturar cuando se selecciona una opción en los ComboBox
        private void dgv_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // Aquí puedes agregar lógica adicional si es necesario cuando el usuario edita una celda ComboBox
        }

        private void ConfigurarDataGridViewConEstilo(DataGridView dgv)
        {
            try
            {
                // Configuraciones generales del DataGridView
                dgv.AllowUserToAddRows = false;
                dgv.AllowUserToDeleteRows = false;
                dgv.AllowUserToResizeRows = false;
                dgv.AllowUserToResizeColumns = false;

                dgv.EnableHeadersVisualStyles = false;
                dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
                dgv.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold);
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;

                dgv.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
                dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
                dgv.DefaultCellStyle.Font = new Font("Arial", 9);

                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Ajuste automático para llenar el DataGridView
                dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells; // Ajusta la altura de las filas según el contenido

                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgv.MultiSelect = false; // Evitar la selección múltiple de filas

                // Configurar ancho de columna mínimo
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    col.MinimumWidth = 100;
                }

                // Configurar estilos para filas alternas (striping effect)
                dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
                dgv.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al configurar DataGridView: " + ex.Message);
            }
        }

        private void DataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["Zona"].Index)
            {
                var cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell.Value != null)
                {
                    dgvToolTip.SetToolTip(dataGridView1, cell.Value.ToString());
                }
            }
        }

        public void GuardarAsistencias()
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    // Verificar si la fila es nueva, en ese caso, ignorarla
                    if (row.IsNewRow) continue;

                    // Obtener los valores seleccionados de los ComboBox
                    string placa = row.Cells["Placas"].Value?.ToString();  // Placa vehicular
                    int? idEmpleado = row.Cells["Empleados"].Value != null ? (int?)Convert.ToInt32(row.Cells["Empleados"].Value) : null;  // Empleado seleccionado
                    string zona = row.Cells["Zona"].Value?.ToString();  // Zona seleccionada
                    string turno = row.Cells["Turno"].Value?.ToString();  // Turno seleccionado
                    string cargo = row.Cells["Cargo"].Value?.ToString();  // Cargo seleccionado (Conductor/Ayudante)

                    // Validar que todos los campos estén completos
                    if (string.IsNullOrEmpty(placa) || !idEmpleado.HasValue || string.IsNullOrEmpty(zona) || string.IsNullOrEmpty(turno) || string.IsNullOrEmpty(cargo))
                    {
                        MessageBox.Show("Por favor, completa todos los campos en la fila.", "Campos Incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Convertir el cargo a booleano (1 para Conductor, 0 para Ayudante)
                    bool esConductor = cargo.Equals("Conductor", StringComparison.OrdinalIgnoreCase);

                    // Obtener el ID de la compacta basado en la placa seleccionada
                    int idCompacta = logEmleados.Instancia.ObtenerIDCompactaPorPlaca(placa);

                    // Insertar el detalle de personal compacta y obtener el ID generado
                    int idDetallePersonalCompacta = logEmleados.Instancia.InsertarDetallePersonalCompacta(idEmpleado.Value, idCompacta, esConductor, zona, turno);

                    // Insertar la visita compacta asociada al detalle creado
                    DateTime fechaVisita = DateTime.Now;  // Puedes cambiar esta fecha por la que corresponda
                    bool completada = false;  // Puedes cambiar este valor según el contexto

                    logEmleados.Instancia.InsertarVisitaCompacta(idDetallePersonalCompacta, fechaVisita, completada);
                }

                // Mensaje de confirmación
                MessageBox.Show("Asistencias guardadas exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar las asistencias: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void btnUpdate_Click(object sender, EventArgs e)
        {
            GuardarAsistencias();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void materialFloatingActionButton1_Click(object sender, EventArgs e)
        {
            // Obtener los datos de visitas compactas desde la base de datos
            DataTable visitasCompactas = logEmleados.Instancia.ObtenerVisitasCompactasDeHoy();

            // Crear una instancia del formulario emergente y pasarle los datos
            frmVisitasCompactas frm = new frmVisitasCompactas();

            // Mostrar el formulario emergente
            frm.ShowDialog();
        }
    }
}
