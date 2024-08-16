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
            toolTip1.SetToolTip(btnDelete, "Eliminar fila");
            toolTip1.SetToolTip(btnUpdate, "Marcar las asistencias como completadas");
            toolTip1.SetToolTip(btnImprimier, "Imprimir reporte del día");
            toolTip1.SetToolTip(materialFloatingActionButton2, "Imprimir reporte de fecha específica");

        }

        private void test()
        {
            // Iterar sobre todas las columnas del DataGridView
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                // Establecer el modo de ordenación a NotSortable
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

        }
        private void CargarEmpleadosLimpieza()
        {
            try
            {
                DataTable dtEmpleados = logEmleados.Instancia.ListarEmpleadosQueSeanLimpieza();
                dataGridView1.DataSource = dtEmpleados;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar empleados: " + ex.Message);
            }
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

            // Ocultar la columna ID
            if (dgv.Columns.Contains("ID_Empleado"))
            {
                dgv.Columns["ID_Empleado"].Visible = false;
            }

            // Ocultar la columna ID
            if (dgv.Columns.Contains("ID_Asistencia"))
            {
                dgv.Columns["ID_Asistencia"].Visible = false;
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
            dataGridView1.DataSource = null;
            CargarEmpleadosLimpieza();
            ConfigurarDataGridView(dataGridView1);
            EstablecerColumnasReadonly(dataGridView1);
            test();

            generarAsistencia();
        }



        private void generarAsistencia() 
        {
            // Obtener la fecha actual
            DateTime now = DateTime.Now;

            // Crear una nueva fecha que solo contiene la parte de la fecha (sin la hora)
            DateTime fechaSoloFecha = new DateTime(now.Year, now.Month, now.Day);


            if (!logAsistencias.Instancia.ValidarAsistenciasPorFecha(fechaSoloFecha))
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["ID_Empleado"].Value != null)
                    {
                        int idEmpleado = Convert.ToInt32(row.Cells["ID_Empleado"].Value);


                        // Llamar a la capa lógica para insertar la asistencia con estado false
                        logAsistencias.Instancia.InsertarAsistencia(idEmpleado, fechaSoloFecha, false);
                    }
                }
            }

            else return;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
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


        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Verifica si hay una fila seleccionada
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Elimina la fila seleccionada del DataGridView
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una fila para eliminar.");
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

            // Verificar que el cambio esté en la columna de CheckBox y que la fila sea válida
            if (e.ColumnIndex == dataGridView1.Columns["Asistio"].Index && e.RowIndex >= 0)
            {
                // Obtener la fila actual
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                //// Obtener el valor del CheckBox (se espera que sea bool)
                bool isChecked = Convert.ToBoolean(row.Cells["Asistio"].Value);

                // Si ya está marcado, evitar que el usuario lo cambie
                if (isChecked)
                {
                    MessageBox.Show("No se puede desmarcar la asistencia ya completada.", "Asistencia Completada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Cancelar el cambio en el DataGridView
                    dataGridView1.CancelEdit();
                    return; // Salir del método sin realizar cambios
                }

                // Si no está marcado, continuar con el proceso de marcado
                int idAsistencia = Convert.ToInt32(row.Cells["ID_Asistencia"].Value);

                // Llamar al método para actualizar la asistencia
                logAsistencias.Instancia.MarcarAsistencia(idAsistencia);

                // Actualizar el valor del CheckBox en el DataGridView
                row.Cells["Asistio"].Value = true; // Marca como completado
            }
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
                "¿Está seguro de que desea marcar todas las asistencias del día como completadas?",
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

                    // Llama a Refresh si es necesario para actualizar la visualización
                    dataGridView1.Refresh();
                    //Ejecutar el método FormPersonalLimpieza_Load
                    FormPersonalLimpieza_Load(sender, e);
             
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
                // Capturar la fecha actual
                DateTime fechaActual = DateTime.Now.Date;

                // Obtener las asistencias del día desde la capa lógica
                DataTable dtAsistencias = logAsistencias.Instancia.ListarAsistenciasPorFecha(fechaActual);

                // Validar si hay asistencias registradas para el día
                if (dtAsistencias.Rows.Count == 0)
                {
                    MessageBox.Show("No hay asistencias registradas para el día.");
                    return;
                }

                // Configuración del diálogo para guardar el archivo PDF
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Archivos PDF (*.pdf)|*.pdf";
                    saveFileDialog.FileName = $"Reporte de Asistencias del Día - {fechaActual.ToString("yyyy-MM-dd")}.pdf";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        Document doc = new Document();
                        try
                        {
                            PdfWriter.GetInstance(doc, new FileStream(saveFileDialog.FileName, FileMode.Create));
                            doc.Open();

                            // Título del reporte
                            iTextSharp.text.Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.DARK_GRAY);
                            Paragraph title = new Paragraph("REPORTE DE ASISTENCIAS", titleFont);
                            title.Alignment = Element.ALIGN_CENTER;
                            title.SpacingAfter = 10f;
                            doc.Add(title);

                            // Fecha actual
                            iTextSharp.text.Font dateFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.BLACK);
                            Paragraph dateParagraph = new Paragraph($"Fecha: {fechaActual.ToString("dd/MM/yyyy")}", dateFont);
                            dateParagraph.Alignment = Element.ALIGN_CENTER;
                            dateParagraph.SpacingAfter = 20f;
                            doc.Add(dateParagraph);

                            // Subtítulo: Sí Asistió
                            iTextSharp.text.Font sectionTitleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, BaseColor.BLACK);
                            Paragraph asistioTitle = new Paragraph("Asistió", sectionTitleFont);
                            asistioTitle.Alignment = Element.ALIGN_LEFT;
                            asistioTitle.SpacingAfter = 10f;
                            doc.Add(asistioTitle);

                            // Crear tabla para asistencias "Sí Asistió"
                            PdfPTable tableAsistio = new PdfPTable(2); // Dos columnas: Nombre, Apellido
                            tableAsistio.WidthPercentage = 100;
                            tableAsistio.SpacingBefore = 10f;
                            tableAsistio.SpacingAfter = 10f;
                            tableAsistio.DefaultCell.Padding = 5;
                            tableAsistio.DefaultCell.BorderColor = BaseColor.LIGHT_GRAY;

                            // Encabezados de columna para "Sí Asistió"
                            string[] headersAsistio = { "Nombre", "Apellido" };

                            // Estilo de los encabezados
                            iTextSharp.text.Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.WHITE);
                            PdfPCell headerCell = new PdfPCell();
                            headerCell.BackgroundColor = BaseColor.DARK_GRAY;
                            headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                            headerCell.Padding = 5;

                            // Añadir encabezados a la tabla "Sí Asistió"
                            foreach (string header in headersAsistio)
                            {
                                headerCell.Phrase = new Phrase(header, headerFont);
                                tableAsistio.AddCell(headerCell);
                            }

                            // Añadir los datos de las asistencias "Sí Asistió"
                            foreach (DataRow row in dtAsistencias.Rows)
                            {
                                bool asistio = Convert.ToBoolean(row["Asistio"]);
                                if (asistio)
                                {
                                    tableAsistio.AddCell(new Phrase(row["Nombres"].ToString()));
                                    tableAsistio.AddCell(new Phrase(row["Apellidos"].ToString()));
                                }
                            }

                            // Agregar la tabla "Sí Asistió" al documento
                            doc.Add(tableAsistio);

                            // Subtítulo: No Asistió
                            Paragraph noAsistioTitle = new Paragraph("Faltó", sectionTitleFont);
                            noAsistioTitle.Alignment = Element.ALIGN_LEFT;
                            noAsistioTitle.SpacingAfter = 10f;
                            doc.Add(noAsistioTitle);

                            // Crear tabla para asistencias "No Asistió"
                            PdfPTable tableNoAsistio = new PdfPTable(2); // Dos columnas: Nombre, Apellido
                            tableNoAsistio.WidthPercentage = 100;
                            tableNoAsistio.SpacingBefore = 10f;
                            tableNoAsistio.SpacingAfter = 10f;
                            tableNoAsistio.DefaultCell.Padding = 5;
                            tableNoAsistio.DefaultCell.BorderColor = BaseColor.LIGHT_GRAY;

                            // Encabezados de columna para "No Asistió"
                            string[] headersNoAsistio = { "Nombre", "Apellido" };

                            // Añadir encabezados a la tabla "No Asistió"
                            foreach (string header in headersNoAsistio)
                            {
                                headerCell.Phrase = new Phrase(header, headerFont);
                                tableNoAsistio.AddCell(headerCell);
                            }

                            // Añadir los datos de las asistencias "No Asistió"
                            foreach (DataRow row in dtAsistencias.Rows)
                            {
                                bool asistio = Convert.ToBoolean(row["Asistio"]);
                                if (!asistio)
                                {
                                    tableNoAsistio.AddCell(new Phrase(row["Nombres"].ToString()));
                                    tableNoAsistio.AddCell(new Phrase(row["Apellidos"].ToString()));
                                }
                            }

                            // Agregar la tabla "No Asistió" al documento
                            doc.Add(tableNoAsistio);
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
                MessageBox.Show($"Error al obtener las asistencias del día: {ex.Message}");
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
    }
}
