using CapaLogica;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace CapaPresentacion
{
    public partial class frmHomeVisitas : Form
    {               
        // Obtener los datos desde la capa lógica
        DataTable dtLocalidadesConEmpleados = logVisitas.Instancia.ObtenerLocalidadesConEmpleados2();
        DataTable dtEmpleados = logVisitas.Instancia.ListarEmpleados();
        public frmHomeVisitas()
        {
            InitializeComponent();

            // Cargar visitas para el DataGridView1 al iniciar
            DateTime fechaActual = DateTime.Now.Date;
            DataTable dtVisitas = logVisitas.Instancia.ListarVisitasParaDgv(fechaActual);
            dataGridView1.DataSource = dtVisitas;
        }

        private void frmHomeVisitas_Load(object sender, EventArgs e)
        {
            AplicarDisenoDataGridView(dataGridView1);
            MostrarLocalidadesConEmpleadosEnDGV();

            // Verificar los nombres de las columnas en el DataTable

            foreach (DataColumn column in dtLocalidadesConEmpleados.Columns)
            {
                Console.WriteLine(column.ColumnName);
            }

        }

        private void MostrarLocalidadesConEmpleadosEnDGV()
        {
            try
            {
                // Asignar el DataTable como DataSource del DataGridView
                dataGridView1.DataSource = dtLocalidadesConEmpleados;

                // Ocultar la columna ID_Localidad si existe
                if (dataGridView1.Columns.Contains("ID_Localidad"))
                {
                    dataGridView1.Columns["ID_Localidad"].Visible = false;
                }

                // Crear y configurar la columna ComboBox para los empleados
                DataGridViewComboBoxColumn comboColumn = new DataGridViewComboBoxColumn
                {
                    HeaderText = "Empleado a actualizar",
                    Name = "Empleado",
                    DataPropertyName = "ID_Empleado",
                    DataSource = dtEmpleados,
                    DisplayMember = "NombreCompleto",
                    ValueMember = "ID_Empleado",
                    AutoComplete = true,
                    DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox,
                    FlatStyle = FlatStyle.Flat
                };


                // Añadir la columna ComboBox al DataGridView
                dataGridView1.Columns.Add(comboColumn);


                // Remover la columna de nombre del empleado para evitar duplicación
                dataGridView1.Columns["NombreEmpleado"].Visible = false;

                // Ajustar el ancho de las columnas automáticamente
                dataGridView1.AutoResizeColumns();


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las localidades con empleados: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void materialFloatingActionButton1_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fechaVisita = DateTime.Now.Date; // Fecha de la visita es la fecha actual
                bool estado = false; // Puedes ajustar el estado según sea necesario

                // Iterar sobre todas las filas seleccionadas del DataGridView
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    // Obtener el ID_Localidad y ID_Empleado de cada fila seleccionada
                    int idLocalidad = Convert.ToInt32(row.Cells["ID_Localidad"].Value);
                    int idEmpleado = Convert.ToInt32(row.Cells["Empleado"].Value);

                    // Llamar al método de logVisitas para generar visita con los datos actuales
                    bool exito = logVisitas.Instancia.GenerarVisitasParaTodasLasLocalidadesConEmpleados(fechaVisita, estado, idLocalidad, idEmpleado);

                }

                MessageBox.Show("Visitas generadas exitosamente.");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }





        public void LimpiarDataGridView(DataGridView dgv)
        {
            if (dgv.Rows.Count > 0 || dgv.Columns.Count > 0)
            {
                dgv.SuspendLayout(); // Suspender la actualización visual para mejorar el rendimiento
                dgv.DataSource = null; // Eliminar la referencia al DataSource
                dgv.Rows.Clear(); // Limpiar las filas
                dgv.Columns.Clear(); // Limpiar las columnas
                dgv.ResumeLayout(); // Reanudar la actualización visual
            }
        }

        public void AplicarDisenoDataGridView(DataGridView dgv)
        {
            ConfigurarDataGridView(dgv);
            RenombrarColumnas(dgv);
            RemoverColumnas(dgv);
            AgregarColumnaComboBox(dgv);
            //AsignarValoresComboBox(dgv);
            EstablecerColumnasReadonly(dgv);

        }


        private void ConfigurarDataGridView(DataGridView dgv)
        {


            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.AllowUserToResizeColumns = false;
            //dgv.ReadOnly = true;

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
            dgv.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold);

            dgv.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void RenombrarColumnas(DataGridView dgv)
        {
            dgv.Columns["Estado"].HeaderText = "Completada";
            dgv.Columns["Nombre_Localidad"].HeaderText = "Nombre";
        }

        private void RemoverColumnas(DataGridView dgv)
        {
            dgv.Columns.Remove("Fecha_Visita");
        }

        private void AgregarColumnaComboBox(DataGridView dgv)
        {
            try
            {
                // Verificar si la columna ya está agregada
                DataGridViewComboBoxColumn comboColumn = dgv.Columns["Personal"] as DataGridViewComboBoxColumn;

                if (comboColumn != null)
                {
                    // Actualizar solo el DataSource de la columna existente
                    DataTable dtEmpleados = logVisitas.Instancia.ListarEmpleados();

                    // Añadir un valor por defecto para manejar los valores nulos
                    var defaultRow = dtEmpleados.NewRow();
                    defaultRow["ID_Empleado"] = DBNull.Value;
                    defaultRow["NombreCompleto"] = "-- Seleccione --";
                    dtEmpleados.Rows.InsertAt(defaultRow, 0);

                    // Asignar el DataTable modificado como DataSource del ComboBoxColumn
                    comboColumn.DataSource = dtEmpleados;
                }
                else
                {
                    // Si la columna no está agregada, crearla y configurarla desde cero
                    DataTable dtEmpleados = logVisitas.Instancia.ListarEmpleados();

                    // Crear la columna ComboBox
                    comboColumn = new DataGridViewComboBoxColumn
                    {
                        HeaderText = "Personal",
                        Name = "Personal",
                        DataPropertyName = "ID_Empleado",
                        DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox,
                        FlatStyle = FlatStyle.Flat,
                        AutoComplete = true,
                        DisplayMember = "NombreCompleto",
                        ValueMember = "ID_Empleado",
                        ReadOnly = false
                    };

                    // Añadir un valor por defecto para manejar los valores nulos
                    var defaultRow = dtEmpleados.NewRow();
                    defaultRow["ID_Empleado"] = DBNull.Value;
                    defaultRow["NombreCompleto"] = "-- Seleccione --";
                    dtEmpleados.Rows.InsertAt(defaultRow, 0);

                    // Asignar el DataTable modificado como DataSource del ComboBoxColumn
                    comboColumn.DataSource = dtEmpleados;

                    // Añadir la columna al DataGridView
                    dgv.Columns.Add(comboColumn);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar columna ComboBox: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void AsignarValoresComboBox(DataGridView dgv)
        {
            try
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        string nombreEmpleado = row.Cells["NombreEmpleado"].Value?.ToString();

                        // Buscar el empleado por nombre en el DataTable de empleados
                        DataRow[] empleadoRows = dtEmpleados.Select($"NombreCompleto = '{nombreEmpleado}'");

                        // Verificar si se encontró un empleado
                        if (empleadoRows.Length > 0)
                        {
                            row.Cells["Personal"].Value = empleadoRows[0]["ID_Empleado"];
                        }
                        else
                        {
                            // Si no se encontró ningún empleado, asignar DBNull.Value
                            row.Cells["Personal"].Value = DBNull.Value;
                        }
                    }
                }

                dgv.Columns.Remove("NombreEmpleado");
            }
            catch (Exception ex)
            {

            }
        }

        private void EstablecerColumnasReadonly(DataGridView dgv)
        {
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                if (column.Name != "Empleado a actualizar")
                {
                    column.ReadOnly = true;
                }
            }
        }

        private void ComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null && comboBox.SelectedIndex == 0)
            {
                // Si el índice seleccionado es 0 (primer elemento), limpiar la selección
                comboBox.SelectedIndex = -1; // Limpia la selección
            }
        }

        private void dataGridView1_DataError_1(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                // Verificar si el error se debe a un valor DBNull
                if (dataGridView1[e.ColumnIndex, e.RowIndex].Value == DBNull.Value)
                {
                    // Mostrar un mensaje adecuado al usuario
                    MessageBox.Show($"Error al procesar datos en la fila {e.RowIndex}, columna {e.ColumnIndex}: El valor está vacío o nulo.",
                                    "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                e.ThrowException = false; // Evita que se propague la excepción
            }
            catch (Exception ex)
            {
                // Registrar el error en la consola o en un archivo de registro
                Console.WriteLine($"Error en dataGridView1_DataError: {ex.Message}");
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    // Acceder a la fila seleccionada
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                    // Mostrar ID_Localidad si la columna existe
                    if (dataGridView1.Columns.Contains("ID_Localidad"))
                    {
                        int indexColumnaIDLocalidad = dataGridView1.Columns["ID_Localidad"].Index;
                        object valorIDLocalidad = selectedRow.Cells[indexColumnaIDLocalidad].Value;
                        lblIdLocalidad.Text = valorIDLocalidad?.ToString() ?? "N/A";
                    }

                    // Obtener el ID_Empleado basado en el nombre mostrado en la columna NombreEmpleado
                    if (dataGridView1.Columns.Contains("NombreEmpleado"))
                    {
                        string nombreEmpleado = selectedRow.Cells["NombreEmpleado"].Value?.ToString();

                        // Buscar el ID_Empleado en el DataTable de empleados
                        DataRow[] empleadoRow = dtEmpleados.Select($"NombreCompleto = '{nombreEmpleado}'");
                        if (empleadoRow.Length > 0)
                        {
                            object valorIDEmpleado = empleadoRow[0]["ID_Empleado"];
                            lblIdEmpleado.Text = valorIDEmpleado?.ToString() ?? "N/A";
                        }
                        else
                        {
                            lblIdEmpleado.Text = "N/A"; // En caso de no encontrar el empleado
                        }
                    }

                    // Refrescar la fila seleccionada para actualizar visualmente los cambios
                    selectedRow.DataGridView.InvalidateRow(selectedRow.Index);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener ID_Localidad o ID_Empleado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool cambioPorUsuario = true; // Variable para controlar si el cambio es causado por el usuario

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];

                    if (column.Name == "Empleado")
                    {
                        DataGridViewComboBoxCell comboCell = row.Cells["Empleado"] as DataGridViewComboBoxCell;

                        if (comboCell != null)
                        {
                            // Obtener el nombre seleccionado en el combo box
                            string nombreEmpleado = comboCell.EditedFormattedValue?.ToString();

                            if (!string.IsNullOrEmpty(nombreEmpleado))
                            {
                                // Obtener el ID_Empleado
                                string idEmpleado = logEmleados.Instancia.ObtenerEmpleadoIdPorNombre(nombreEmpleado).ToString();

                                if (!string.IsNullOrEmpty(idEmpleado))
                                {
                                    // Preguntar al usuario si desea actualizar el empleado
                                    if (cambioPorUsuario)
                                    {
                                        DialogResult result = MessageBox.Show($"¿Desea actualizar el empleado a: {nombreEmpleado} (ID: {idEmpleado})?", "Confirmar actualización", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                        if (result == DialogResult.Yes)
                                        {
                                            try
                                            {
                                                logLocalidades.Instancia.ActualizarEmpleadoEnLocalidad(int.Parse(lblIdLocalidad.Text), Convert.ToInt32(idEmpleado));
                                                AgregarColumnaComboBox(dataGridView1);
                                                // Mostrar mensaje de actualización exitosa
                                                MessageBox.Show($"Se actualizó el empleado: {nombreEmpleado}. ID: {idEmpleado}", "Actualización exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                            }
                                            catch (Exception ex)
                                            {
                                               // // Manejar la excepción lanzada por el trigger
                                               // MessageBox.Show($"No se puede asignar el mismo empleado a más de una localidad. Operación cancelada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                               // Desactivar temporalmente el evento CellValueChanged para evitar bucles
                                               // dataGridView1.CellValueChanged -= dataGridView1_CellValueChanged;

                                               // Restaurar el valor anterior si se cancela la operación
                                               //cambioPorUsuario = false;
                                               // comboCell.Value = comboCell.Tag;
                                               // cambioPorUsuario = true;

                                               // Volver a activar el evento CellValueChanged
                                               // dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
                                            }
                                        }
                                        else
                                        {
                                            // Desactivar temporalmente el evento CellValueChanged para evitar bucles
                                            dataGridView1.CellValueChanged -= dataGridView1_CellValueChanged;

                                            // Restaurar el valor anterior si el usuario selecciona "No"
                                            cambioPorUsuario = false;
                                            comboCell.Value = comboCell.Tag;
                                            cambioPorUsuario = true;

                                            // Volver a activar el evento CellValueChanged
                                            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
                                        }
                                    }
                                    else
                                    {
                                        // Restaurar el valor anterior sin preguntar al usuario
                                        comboCell.Value = comboCell.Tag;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show($"No se encontró un empleado con el nombre: {nombreEmpleado}", "Empleado no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                    // Revertir al valor anterior si no se encuentra el empleado
                                    comboCell.Value = comboCell.Tag;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al procesar la celda modificada: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImprimier_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener las visitas del día desde logVisitas
                DataTable dtVisitas = logVisitas.Instancia.ObtenerVisitasDelDia();

                if (dtVisitas.Rows.Count == 0)
                {
                    MessageBox.Show("No hay visitas registradas para el día.");
                    return;
                }

                // Configuración del diálogo para guardar el archivo PDF
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Archivos PDF (*.pdf)|*.pdf";
                    saveFileDialog.FileName = "Reporte de Visitas del Día.pdf";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        Document doc = new Document();
                        try
                        {
                            PdfWriter.GetInstance(doc, new FileStream(saveFileDialog.FileName, FileMode.Create));
                            doc.Open();

                            // Título del cronograma
                            iTextSharp.text.Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.DARK_GRAY);
                            Paragraph title = new Paragraph("CRONOGRAMA", titleFont);
                            title.Alignment = Element.ALIGN_CENTER;
                            title.SpacingAfter = 10f;
                            doc.Add(title);

                            // Fecha actual
                            iTextSharp.text.Font dateFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.BLACK);
                            string fechaActual = DateTime.Now.ToString("dd/MM/yyyy");
                            Paragraph dateParagraph = new Paragraph($"Fecha: {fechaActual}", dateFont);
                            dateParagraph.Alignment = Element.ALIGN_CENTER;
                            dateParagraph.SpacingAfter = 20f;
                            doc.Add(dateParagraph);

                            // Diseño de la tabla (sin la columna ID)
                            PdfPTable table = new PdfPTable(3); // Tres columnas: Nombre del Parque, Nombre del Encargado, Estado
                            table.WidthPercentage = 100;
                            table.SpacingBefore = 10f;
                            table.SpacingAfter = 10f;
                            table.DefaultCell.Padding = 5;
                            table.DefaultCell.BorderColor = BaseColor.LIGHT_GRAY;

                            // Encabezados de columna
                            string[] headers = { "Nombre del Parque", "Nombre del Encargado", "Estado" };

                            // Estilo de los encabezados
                            iTextSharp.text.Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.WHITE);

                            // Fondo del encabezado
                            PdfPCell headerCell = new PdfPCell();
                            headerCell.BackgroundColor = BaseColor.DARK_GRAY;
                            headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                            headerCell.Padding = 5;

                            // Encabezados de columna
                            foreach (string header in headers)
                            {
                                headerCell.Phrase = new Phrase(header, headerFont);
                                table.AddCell(headerCell);
                            }

                            // Datos (con formato de fecha y estado)
                            foreach (DataRow row in dtVisitas.Rows)
                            {
                                PdfPCell parkCell = new PdfPCell(new Phrase(row["Nombre_Localidad"].ToString()));
                                parkCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                table.AddCell(parkCell);

                                PdfPCell employeeCell = new PdfPCell(new Phrase(row["Nombre_Completo_Empleado"].ToString()));
                                employeeCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                table.AddCell(employeeCell);

                                bool estado = Convert.ToBoolean(row["Estado"]);
                                string estadoTexto = estado ? "Completada" : "Pendiente";
                                PdfPCell statusCell = new PdfPCell(new Phrase(estadoTexto));
                                statusCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                table.AddCell(statusCell);
                            }

                            doc.Add(table);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error al generar el PDF: {ex.Message}");
                        }
                        finally
                        {
                            doc.Close(); // Cerrar el documento ANTES de abrirlo

                            // Abrir el PDF
                            try
                            {
                                System.Diagnostics.Process.Start(saveFileDialog.FileName);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Error al abrir el PDF: {ex.Message}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener las visitas del día: {ex.Message}");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener las visitas del día desde logVisitas
                DataTable dtVisitas = logVisitas.Instancia.ObtenerVisitasDelDia();

                if (dtVisitas.Rows.Count == 0)
                {
                    MessageBox.Show("No hay visitas registradas para el día.");
                    return;
                }

                // Mostrar MessageBox para preguntar al usuario
                DialogResult result = MessageBox.Show("¿Desea marcar todas las visitas como completadas?", "Confirmación", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Marcar todas las visitas como completadas
                    bool completada = true; // Marcar como completadas
                    bool exito = logVisitas.Instancia.MarcarVisitasComoCompletadas(dtVisitas.AsEnumerable().Select(r => r.Field<int>("ID_Visita")).ToList(), completada);

                    if (exito)
                    {
                        MessageBox.Show("Todas las visitas han sido marcadas como completadas exitosamente.");
                        // Actualizar el DataGridView u otra acción necesaria después de marcar las visitas
                    }
                    else
                    {
                        MessageBox.Show("Hubo un problema al marcar las visitas. Por favor, revise los registros.");
                    }
                }
                else if (result == DialogResult.No)
                {
                    // Abrir formulario emergente para seleccionar visitas que no marcar como completadas
                    using (FormSeleccionVisitasEmergente form = new FormSeleccionVisitasEmergente(dtVisitas))
                    {
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            List<int> visitasNoCompletar = form.VisitasNoCompletar;

                            // Marcar las visitas seleccionadas como completadas
                            bool exito = logVisitas.Instancia.MarcarVisitasComoCompletadas(
                                dtVisitas.AsEnumerable().Select(r => r.Field<int>("ID_Visita")).Except(visitasNoCompletar).ToList(),
                                true);

                            if (exito)
                            {
                                MessageBox.Show("Las visitas seleccionadas han sido marcadas como completadas.");
                                // Actualizar el DataGridView u otra acción necesaria después de marcar las visitas
                            }
                            else
                            {
                                MessageBox.Show("Hubo un problema al marcar las visitas seleccionadas. Por favor, revise los registros.");
                            }
                        }
                    }
                }
                else if (result == DialogResult.Cancel)
                {
                    // Cancelar la operación
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }








    }
}
