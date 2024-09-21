using CapaLogica;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.text.pdf.codec.wmf;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FormPersonalLimpieza : Form
    {
        public FormPersonalLimpieza()
        {
            InitializeComponent();

            // Configurar el texto del ToolTip para un botón
            toolTip1.SetToolTip(btnUpdate, "Marcar las asistencias como completadas");
            toolTip1.SetToolTip(btnImprimier, "Imprimir reporte del día");
            toolTip1.SetToolTip(materialFloatingActionButton2, "Imprimir reporte de fecha específica");

        }

        //private void test()
        //{
        //    // Iterar sobre todas las columnas del DataGridView
        //    foreach (DataGridViewColumn column in dataGridView1.Columns)
        //    {
        //        // Establecer el modo de ordenación a NotSortable
        //        column.SortMode = DataGridViewColumnSortMode.NotSortable;
        //    }

        //}

        public void MostrarNombresColumnas(DataGridView dgv)
        {
            List<string> nombresColumnas = new List<string>();

            foreach (DataGridViewColumn columna in dgv.Columns)
            {
                nombresColumnas.Add(columna.Name);
            }

            // Unir todos los nombres de las columnas en una sola cadena separada por comas
            string nombresColumnasTexto = string.Join(", ", nombresColumnas);

            // Mostrar los nombres en un MessageBox
            MessageBox.Show(nombresColumnasTexto, "Nombres de las Columnas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void CargarEmpleadosLimpieza()
        {
            try
            {
                DataTable dtEmpleados = logEmleados.Instancia.ListarEmpleadosQueSeanLimpieza();
                dataGridView1.DataSource = dtEmpleados;
            }
            catch (Exception)
            {
                // Manejar la excepción sin mostrar MessageBox
            }
        }

        private void ConfigurarDataGridView(DataGridView dgv)
        {
            try
            {
                dgv.AllowUserToAddRows = false;
                dgv.AllowUserToDeleteRows = false;
                dgv.AllowUserToResizeRows = false;
                dgv.AllowUserToResizeColumns = false;

                dgv.EnableHeadersVisualStyles = false;
                dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
                dgv.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold);

                dgv.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
                dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                // Ocultar la columna ID
                if (dgv.Columns.Contains("ID_Empleado"))
                {
                    dgv.Columns["ID_Empleado"].Visible = false;
                }

                if (dgv.Columns.Contains("ID_Asistencia"))
                {
                    dgv.Columns["ID_Asistencia"].Visible = false;
                }

                // Cargar las opciones en el ComboBox de cada fila
                if (!dgv.Columns.Contains("Opción"))
                {
                    DataTable dtOpciones = logAsistencias.Instancia.ObtenerOpciones();
                    if (dtOpciones != null && dtOpciones.Rows.Count > 0)
                    {
                        DataGridViewComboBoxColumn comboBoxColumnOpcion = new DataGridViewComboBoxColumn();
                        comboBoxColumnOpcion.Name = "Opción";
                        comboBoxColumnOpcion.DataSource = dtOpciones;
                        comboBoxColumnOpcion.DisplayMember = "Nombre_Opcion";
                        comboBoxColumnOpcion.ValueMember = "ID_Opcion";
                        dgv.Columns.Add(comboBoxColumnOpcion);
                    }
                }

                // Cargar los sectores en el ComboBox de cada fila
                if (!dgv.Columns.Contains("Sector"))
                {
                    DataTable dtSectoresTurnos = logAsistencias.Instancia.ObtenerSectores();
                    if (dtSectoresTurnos != null && dtSectoresTurnos.Rows.Count > 0)
                    {
                        DataGridViewComboBoxColumn comboBoxColumnSector = new DataGridViewComboBoxColumn();
                        comboBoxColumnSector.Name = "Sector";
                        comboBoxColumnSector.DataSource = dtSectoresTurnos;
                        comboBoxColumnSector.DisplayMember = "Sector";
                        comboBoxColumnSector.ValueMember = "ID";
                        dgv.Columns.Add(comboBoxColumnSector);
                    }
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            
        }




        private void EstablecerColumnasReadonly(DataGridView dgv)
        {
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                if (column.Name == "Nombres" || column.Name == "Apellidos")
                {
                    column.ReadOnly = true;
                }
            }
        }

        private void FormPersonalLimpieza_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = null;
                CargarEmpleadosLimpieza();
                ConfigurarDataGridView(dataGridView1);
                EstablecerColumnasReadonly(dataGridView1);

                generarAsistencia();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



        private void generarAsistencia()
        {
            try
            {
                // Obtener la fecha actual
                DateTime now = DateTime.Now;

                // Crear una nueva fecha que solo contiene la parte de la fecha (sin la hora)
                DateTime fechaSoloFecha = new DateTime(now.Year, now.Month, now.Day);

                // Verificar si ya existen asistencias para la fecha actual
                if (!logAsistencias.Instancia.ValidarAsistenciasPorFecha(fechaSoloFecha))
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells["ID_Empleado"].Value != null)
                        {
                            int idEmpleado = Convert.ToInt32(row.Cells["ID_Empleado"].Value);

                            // Obtener el ID de la opción seleccionada en el ComboBox
                            int idOpcion = 2; // Valor predeterminado si el ComboBox no tiene un valor seleccionado

                            if (row.Cells["Opción"].Value != null)
                            {
                                idOpcion = Convert.ToInt32(row.Cells["Opción"].Value);
                            }

                            // Obtener el ID del sector seleccionado en el ComboBox
                            int idSector = 48; // Valor predeterminado si el ComboBox no tiene un valor seleccionado

                            if (row.Cells["Sector"].Value != null)
                            {
                                idSector = Convert.ToInt32(row.Cells["Sector"].Value);
                            }

                            // Llamar a la capa lógica para insertar la asistencia con el ID de la opción y el ID del sector
                            logAsistencias.Instancia.InsertarAsistencia(idEmpleado, fechaSoloFecha, idOpcion, idSector);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                // Verificar si hay filas seleccionadas
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    // Obtener la fila seleccionada
                    DataGridViewRow filaSeleccionada = dataGridView1.SelectedRows[0];

                    // Verificar si la columna "ID_Empleado" existe
                    if (dataGridView1.Columns.Contains("ID_Empleado"))
                    {
                        // Obtener el valor de la columna ID_Empleado en la fila seleccionada
                        var cellValue = filaSeleccionada.Cells["ID_Empleado"].Value;

                        if (cellValue != null)
                        {
                            string id = cellValue.ToString();
                            lblIdEmpleado.Text = id;
                        }
                        else
                        {
                            MessageBox.Show("La celda seleccionada no contiene valor.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

            // Controlar el tipo de error
            if (e.Exception != null)
            {
                // Mostrar un mensaje de error al usuario           

                // Opcionalmente, puedes registrar el error o realizar otras acciones
                // LogError(e.Exception); // Ejemplo de función para registrar errores
            }

            // Establecer la acción que debe tomar el DataGridView
            e.ThrowException = false; // Evitar que el error se propague
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

          
        }
        private DataTable ObtenerDatosDesdeDataGridView(DataGridView dgv)
        {
            // Crear un nuevo DataTable
            DataTable dt = new DataTable();

            // Añadir columnas al DataTable basadas en el DataGridView
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                dt.Columns.Add(column.HeaderText, typeof(string));
            }

            // Añadir filas al DataTable basadas en las filas del DataGridView
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!row.IsNewRow) // No añadir la fila nueva que se encuentra al final del DataGridView
                {
                    DataRow dataRow = dt.NewRow();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dataRow[cell.ColumnIndex] = cell.Value?.ToString(); // Convertir el valor de la celda a cadena
                    }
                    dt.Rows.Add(dataRow);
                }
            }

            return dt;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Mostrar un mensaje de confirmación al usuario
            DialogResult result = MessageBox.Show(
                "¿Está seguro de que desea marcar todas las asistencias de la tabla?",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            DataTable Asistencias = ObtenerDatosDesdeDataGridView(dataGridView1);

            // Verificar la respuesta del usuario
            if (result == DialogResult.Yes)
            {
                try
                {
                    // Obtener la fecha seleccionada de tu interfaz
                    DateTime fechaSeleccionada = DateTime.Now;

                    // Llamar al método para marcar todas las asistencias del día
                    logAsistencias.Instancia.MarcarTodasAsistenciasDelDia(Asistencias, fechaSeleccionada);

                    MessageBox.Show("Todas las asistencias del día han sido marcadas como completadas.");

                    

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private void btnImprimier_Click(object sender, EventArgs e)
        {
            try
            {
                // Configuración del diálogo para guardar el archivo PDF
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Archivos PDF (*.pdf)|*.pdf";
                    saveFileDialog.FileName = $"Reporte - {DateTime.Now:yyyy-MM-dd}.pdf";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Crear un documento en orientación horizontal (landscape)
                        Document doc = new Document(PageSize.A4.Rotate(), 10f, 10f, 20f, 20f);
                        try
                        {
                            PdfWriter.GetInstance(doc, new FileStream(saveFileDialog.FileName, FileMode.Create));
                            doc.Open();

                            // Título del reporte
                            iTextSharp.text.Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.BLACK);
                            Paragraph title = new Paragraph("REPORTE DEL DÍA", titleFont);
                            title.Alignment = Element.ALIGN_CENTER;
                            title.SpacingAfter = 20f;
                            doc.Add(title);

                            // Añadir fecha al reporte
                            iTextSharp.text.Font dateFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.BLACK);
                            Paragraph dateParagraph = new Paragraph($"Fecha: {DateTime.Now.ToString("dd/MM/yyyy")}", dateFont);
                            dateParagraph.Alignment = Element.ALIGN_RIGHT;
                            dateParagraph.SpacingAfter = 10f;
                            doc.Add(dateParagraph);

                            // Crear la tabla PDF con columnas seleccionadas (sin los IDs)
                            PdfPTable pdfTable = new PdfPTable(4); // Número de columnas visibles (Opción, Sector, Nombres, Apellidos)
                            pdfTable.WidthPercentage = 100;
                            pdfTable.SpacingBefore = 20f;
                            pdfTable.SpacingAfter = 20f;

                            // Configurar anchos de columna para distribuir el espacio
                            float[] columnWidths = new float[] { 15f, 25f, 30f, 30f };
                            pdfTable.SetWidths(columnWidths);

                            // Añadir encabezados de columna al PDF
                            string[] columnHeaders = { "Opción", "Sector", "Nombres", "Apellidos" };
                            iTextSharp.text.Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.WHITE);
                            foreach (string columnHeader in columnHeaders)
                            {
                                PdfPCell headerCell = new PdfPCell(new Phrase(columnHeader, headerFont));
                                headerCell.BackgroundColor = BaseColor.GRAY;
                                headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                headerCell.Padding = 8f;
                                pdfTable.AddCell(headerCell);
                            }

                            // Añadir filas al PDF con los nombres en lugar de los IDs
                            iTextSharp.text.Font cellFont = FontFactory.GetFont(FontFactory.HELVETICA, 11, BaseColor.BLACK);
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (!row.IsNewRow) // Evitar agregar la fila "nueva"
                                {
                                    // Obtener el nombre de la opción en lugar del ID
                                    string nombreOpcion = row.Cells["Opción"].FormattedValue.ToString();

                                    // Obtener el nombre del sector en lugar del ID
                                    string nombreSector = row.Cells["Sector"].FormattedValue.ToString();

                                    // Agregar las celdas con los valores correspondientes al PDF
                                    PdfPCell opcionCell = new PdfPCell(new Phrase(nombreOpcion, cellFont));
                                    PdfPCell sectorCell = new PdfPCell(new Phrase(nombreSector, cellFont));
                                    PdfPCell nombresCell = new PdfPCell(new Phrase(row.Cells["Nombres"].Value.ToString(), cellFont));
                                    PdfPCell apellidosCell = new PdfPCell(new Phrase(row.Cells["Apellidos"].Value.ToString(), cellFont));

                                    opcionCell.Padding = 5f;
                                    sectorCell.Padding = 5f;
                                    nombresCell.Padding = 5f;
                                    apellidosCell.Padding = 5f;

                                    pdfTable.AddCell(opcionCell);
                                    pdfTable.AddCell(sectorCell);
                                    pdfTable.AddCell(nombresCell);
                                    pdfTable.AddCell(apellidosCell);
                                }
                            }

                            // Agregar la tabla completa al documento
                            doc.Add(pdfTable);
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
                MessageBox.Show($"Error al generar el reporte: {ex.Message}");
            }
        }



        private void materialFloatingActionButton2_Click(object sender, EventArgs e)
        {
            using (frmReporteAsistencias frmReporteAsistencias = new frmReporteAsistencias())
            {
                frmReporteAsistencias.StartPosition = FormStartPosition.CenterScreen;

                // Mostrar el formulario como un cuadro de diálogo
                if (frmReporteAsistencias.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                DataGridView dgv = sender as DataGridView;

                // Verificar que las columnas "Opción" y "Sector" existan antes de intentar usarlas
                if (!dgv.Columns.Contains("Opción") || !dgv.Columns.Contains("Sector"))
                {
                    return;
                }

                // Asignar los valores seleccionados desde la base de datos
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.Cells["ID_Asistencia"].Value != DBNull.Value && row.Cells["ID_Empleado"].Value != DBNull.Value)
                    {
                        int idAsistencia = Convert.ToInt32(row.Cells["ID_Asistencia"].Value);
                        int idEmpleado = Convert.ToInt32(row.Cells["ID_Empleado"].Value);

                        // Obtener los IDs desde la base de datos
                        var ids = logAsistencias.Instancia.ObtenerIdSectorYIdOpcionPorAsistenciaYEmpleado(idAsistencia, idEmpleado);
                        int idOpcion = ids.idOpcion;
                        int idSectorTurno = ids.idSectorTurno;

                        // Asignar el índice correcto en el ComboBox de la columna "Opción"
                        if (row.Cells["Opción"] is DataGridViewComboBoxCell comboBoxCellOpcion)
                        {
                            comboBoxCellOpcion.Value = idOpcion;
                        }

                        // Asignar el índice correcto en el ComboBox de la columna "Sector"
                        if (row.Cells["Sector"] is DataGridViewComboBoxCell comboBoxCellSector)
                        {
                            comboBoxCellSector.Value = idSectorTurno;
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Manejar la excepción sin mostrar MessageBox
            }
        }
    }
}
