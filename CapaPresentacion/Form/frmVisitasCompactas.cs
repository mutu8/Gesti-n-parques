using CapaLogica;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MaterialSkin.Controls;

namespace CapaPresentacion
{
    public partial class frmVisitasCompactas : MaterialForm
    {
        public frmVisitasCompactas()
        {
            InitializeComponent();
            MaximizeBox = false;
            ConfigurarDataGridViewConEstilo(dataGridView1);
            CargarVisitasCompactasEnDataGridView(dataGridView1);
        }

        private void ConfigurarDataGridViewConEstilo(DataGridView dgv)
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
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;

                dgv.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
                dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
                dgv.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 9);

                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgv.MultiSelect = false;

                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    col.MinimumWidth = 100;
                }

                dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
                dgv.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al configurar DataGridView: " + ex.Message);
            }
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
            dgv.Columns["Zona"].HeaderText = "Zona de Trabajo";
            dgv.Columns["esConductor"].HeaderText = "Conductor";
            dgv.Columns["Completada"].HeaderText = "Completada";
            dgv.Columns["NombreCompleto"].HeaderText = "Empleado";
            dgv.Columns["PlacaVehicular"].HeaderText = "Placa Vehicular";

            // Hacer todas las columnas readonly excepto "Completada"
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                if (column.Name != "Completada")
                {
                    column.ReadOnly = true;
                }
            }

            // Mover las columnas "esConductor" y "Completada" al final
            dgv.Columns["Completada"].DisplayIndex = dgv.Columns.Count - 1;  // Última columna
            dgv.Columns["esConductor"].DisplayIndex = dgv.Columns.Count - 2; // Penúltima columna
        }

        private void frmVisitasCompactas_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns["Zona"].Width = 600;
            dataGridView1.Columns["NombreCompleto"].Width = 150;
            dataGridView1.Columns["Turno"].Width = 50;
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar que la celda no sea el encabezado
            if (e.RowIndex >= 0)
            {
                // Obtener la fila seleccionada
                DataGridViewRow filaSeleccionada = dataGridView1.Rows[e.RowIndex];

                // Obtener datos de la fila
                string placa = filaSeleccionada.Cells["PlacaVehicular"].Value.ToString();
                string empleado = filaSeleccionada.Cells["NombreCompleto"].Value.ToString();
                string zona = filaSeleccionada.Cells["Zona"].Value.ToString();
                bool esConductor = Convert.ToBoolean(filaSeleccionada.Cells["esConductor"].Value);
                bool completada = Convert.ToBoolean(filaSeleccionada.Cells["Completada"].Value);

                // Convertir el valor de la celda a DateTime
                DateTime fechaVisita = Convert.ToDateTime(filaSeleccionada.Cells["Fecha_Visita"].Value);

                // Preguntar al usuario antes de realizar el cambio
                DialogResult dialogResult = MessageBox.Show("¿Deseas marcar la visita como completada?", "Confirmar acción", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    // Obtener el ID de la visita compacta
                    int idVisitaCompacta = logEmleados.Instancia.ObtenerIDVisitaCompacta(placa, empleado, zona, esConductor, fechaVisita);

                    // Actualizar el estado de "Completada" en la base de datos
                    try
                    {
                        logVisitasCompactas.Instancia.MarcarVisitaCompletada(idVisitaCompacta);
                        MessageBox.Show("El estado de la visita ha sido actualizado correctamente.", "Actualización exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al actualizar el estado de la visita: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    // Mostrar el ID de la visita en un label o en otro control
                    lblID.Text = "Id: " + idVisitaCompacta.ToString();
                }
                else
                {
                    // Restaurar el valor anterior de "Completada" si el usuario elige "No"
                    dataGridView1.CancelEdit();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Verificar si hay visitas en la tabla
            if (dataGridView1.Rows.Count > 0)
            {
                // Configurar el diálogo para guardar el archivo PDF
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Archivos PDF (*.pdf)|*.pdf";
                    saveFileDialog.FileName = $"Reporte_de_Visitas_{DateTime.Now:yyyyMMdd}.pdf";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Crear el documento PDF
                        Document doc = new Document();
                        try
                        {
                            PdfWriter.GetInstance(doc, new FileStream(saveFileDialog.FileName, FileMode.Create));
                            doc.Open();

                            // Título del reporte
                            iTextSharp.text.Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.DARK_GRAY);
                            Paragraph title = new Paragraph("REPORTE DE VISITAS", titleFont);
                            title.Alignment = Element.ALIGN_CENTER;
                            title.SpacingAfter = 10f;
                            doc.Add(title);

                            // Fecha actual del reporte
                            iTextSharp.text.Font dateFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.BLACK);
                            Paragraph dateParagraph = new Paragraph($"Fecha del Reporte: {DateTime.Now:dd/MM/yyyy}", dateFont);
                            dateParagraph.Alignment = Element.ALIGN_CENTER;
                            dateParagraph.SpacingAfter = 20f;
                            doc.Add(dateParagraph);

                            // Diseño de la tabla
                            PdfPTable table = new PdfPTable(4); // Cuatro columnas (eliminamos la columna de fecha)
                            table.WidthPercentage = 100;
                            table.SpacingBefore = 10f;
                            table.SpacingAfter = 10f;

                            // Ajustar el ancho de las columnas
                            float[] columnWidths = new float[] { 4f, 3f, 2f, 2f }; // Aumentamos el ancho de la columna "Zona"
                            table.SetWidths(columnWidths);

                            // Encabezados de columna
                            string[] headers = { "Zona de Trabajo", "Empleado", "Conductor", "Completada" };
                            iTextSharp.text.Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.WHITE);

                            // Fondo del encabezado
                            PdfPCell headerCell = new PdfPCell();
                            headerCell.BackgroundColor = BaseColor.DARK_GRAY;
                            headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                            headerCell.Padding = 5;

                            // Agregar encabezados a la tabla
                            foreach (string header in headers)
                            {
                                headerCell.Phrase = new Phrase(header, headerFont);
                                table.AddCell(headerCell);
                            }

                            // Agregar datos a la tabla
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (!row.IsNewRow)
                                {
                                    // Zona de Trabajo
                                    PdfPCell zonaCell = new PdfPCell(new Phrase(row.Cells["Zona"].Value.ToString()));
                                    zonaCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    table.AddCell(zonaCell);

                                    // Empleado
                                    PdfPCell empleadoCell = new PdfPCell(new Phrase(row.Cells["NombreCompleto"].Value.ToString()));
                                    empleadoCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    table.AddCell(empleadoCell);

                                    // Conductor
                                    bool esConductor = Convert.ToBoolean(row.Cells["esConductor"].Value);
                                    PdfPCell conductorCell = new PdfPCell(new Phrase(esConductor ? "Sí" : "No"));
                                    conductorCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    table.AddCell(conductorCell);

                                    // Completada
                                    bool completada = Convert.ToBoolean(row.Cells["Completada"].Value);
                                    PdfPCell completadaCell = new PdfPCell(new Phrase(completada ? "Sí" : "No"));
                                    completadaCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    table.AddCell(completadaCell);
                                }
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
            else
            {
                MessageBox.Show("No hay datos para generar el PDF.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
