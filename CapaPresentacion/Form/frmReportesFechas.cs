using iTextSharp.text.pdf;
using iTextSharp.text;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaLogica;
using System.Globalization;
using System.Windows.Controls;

namespace CapaPresentacion
{
    public partial class frmReportesFechas : MaterialForm
    {
        public frmReportesFechas()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            InitializeComboBox();
        }
        public int idEmpleado { get; set; }

        // Propiedad pública para acceder al ComboBox
        public System.Windows.Forms.ComboBox cbo
        {
            get { return cboMeses; }
        }

        // Propiedad pública para acceder al DateTimePicker
        public DateTimePicker DatePicker
        {
            get { return dateTimePicker1; }
        }
        public DomainUpDown DiD
        {
            get { return domainUpDown1; }
        }
        // Método para inicializar el ComboBox con algunos valores
        private void InitializeComboBox()
        {
            string[] meses = new string[]
            {
            "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio",
            "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"
            };
            cboMeses.Items.AddRange(meses);

            // Limpiar los valores existentes
            domainUpDown1.Items.Clear();

            // Agregar los años al DomainUpDown
            domainUpDown1.Items.Add("2024");
            domainUpDown1.Items.Add("2025");
            domainUpDown1.Items.Add("2026");

            // Configurar el valor predeterminado, si es necesario
            domainUpDown1.SelectedIndex = 0; // Selecciona el primer elemento por defecto
        }

        private void frmReportesFechas_Load(object sender, EventArgs e)
        {
            // Establecer la fecha mínima en el DateTimePicker
            dateTimePicker1.MinDate = new DateTime(2024, 7, 1); // 1 de enero de 2024
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

            this.DialogResult = DialogResult.Cancel; // Establecer el resultado del diálogo a Cancel
            this.Close(); // Cerrar el formulario
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            // Verificar si el DateTimePicker es visible
            if (dateTimePicker1.Visible)
            {
                // Capturar la fecha del DateTimePicker
                DateTime selectedDate = dateTimePicker1.Value;
                string formattedDate = selectedDate.ToString("yyyy-MM-dd");
                DateTime parsedDate = DateTime.ParseExact(formattedDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                DateTime fecha = DateTime.Parse(formattedDate);

                if (logVisitas.Instancia.ExisteVisitaActivaParaFecha1parametro(parsedDate))
                {
                    // Obtener las visitas de la fecha seleccionada
                    List<object[]> visitas = logVisitas.Instancia.ListarVisitasParDeFechaEspecificav(fecha);

                    // Ordenar visitas si es necesario
                    var visitasOrdenadas = visitas.OrderBy(v => Convert.ToDateTime(v[0])).ToList();

                    // Configuración del diálogo para guardar el archivo PDF
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "Archivos PDF (*.pdf)|*.pdf";
                        saveFileDialog.FileName = $"Reporte de Visitas {formattedDate}.pdf";

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
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

                                // Fecha seleccionada
                                iTextSharp.text.Font dateFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.BLACK);
                                Paragraph dateParagraph = new Paragraph($"Fecha: {formattedDate}", dateFont);
                                dateParagraph.Alignment = Element.ALIGN_CENTER;
                                dateParagraph.SpacingAfter = 20f;
                                doc.Add(dateParagraph);

                                // Diseño de la tabla
                                PdfPTable table = new PdfPTable(5); // Cinco columnas
                                table.WidthPercentage = 100;
                                table.SpacingBefore = 10f;
                                table.SpacingAfter = 10f;
                                table.DefaultCell.Padding = 10; // Ajustar el padding
                                table.DefaultCell.BorderColor = BaseColor.LIGHT_GRAY;

                                // Ajustar el ancho de las columnas
                                float[] columnWidths = new float[] { 2f, 3f, 3f, 2f, 4f }; // Ajusta según tus necesidades
                                table.SetWidths(columnWidths);

                                // Encabezados de columna
                                string[] headers = { "Fecha de Visita", "Nombre del Localidad", "Nombre del Empleado", "Estado", "Nota" };
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
                                foreach (object[] visita in visitasOrdenadas)
                                {
                                    // Fecha
                                    PdfPCell fechaCell = new PdfPCell(new Phrase(Convert.ToDateTime(visita[0]).ToString("dd/MM/yyyy")));
                                    fechaCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    table.AddCell(fechaCell);

                                    // Nombre del Localidad
                                    PdfPCell localidadCell = new PdfPCell(new Phrase(visita[1].ToString()));
                                    localidadCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    table.AddCell(localidadCell);

                                    // Nombre del Empleado
                                    PdfPCell empleadoCell = new PdfPCell(new Phrase(visita[2].ToString()));
                                    empleadoCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    table.AddCell(empleadoCell);

                                    // Estado
                                    bool estado = Convert.ToBoolean(visita[3]);
                                    string estadoTexto = estado ? "Completada" : "No completada";
                                    PdfPCell statusCell = new PdfPCell(new Phrase(estadoTexto));
                                    statusCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    table.AddCell(statusCell);

                                    // Nota
                                    string notaTexto = visita.Length > 4 && visita[4] != null ? visita[4].ToString() : "";
                                    PdfPCell notaCell = new PdfPCell(new Phrase(notaTexto));
                                    notaCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    table.AddCell(notaCell);
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
                    MessageBox.Show("No hay datos en la fecha ingresada");
                }
            }

            else
            {
                // Verificar si se ha seleccionado un mes
                if (cbo.SelectedIndex < 0)
                {
                    MessageBox.Show("Por favor, seleccione un mes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Salir del método si no se selecciona un mes
                }

                // Obtener el mes del ComboBox
                int mes = cbo.SelectedIndex + 1; // Si los meses están indexados desde 0 (Enero es 0)

                // Obtener el año del DomainUpDown
                int anio = int.Parse(DiD.Text);

                // Supongamos que tienes el ID del empleado en alguna variable, por ejemplo:
                int id = idEmpleado; // Reemplaza con el ID del empleado real

                // Obtener las visitas del empleado específico filtradas por mes y año
                List<object[]> visitas = logVisitas.Instancia.ListarVisitasDeEmpleado(id, mes, anio);

                // Verificar si hay visitas
                if (visitas.Count == 0)
                {
                    MessageBox.Show("No se encontraron visitas para el mes y año seleccionados.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return; // Salir del método si no hay visitas
                }

                // Configuración del diálogo para guardar el archivo PDF
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Archivos PDF (*.pdf)|*.pdf";
                    saveFileDialog.FileName = $"Reporte de Visitas {mes}-{anio}.pdf";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
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

                            // Fecha seleccionada
                            iTextSharp.text.Font dateFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.BLACK);
                            Paragraph dateParagraph = new Paragraph($"Mes: {cbo.Text} Año: {anio}", dateFont);
                            dateParagraph.Alignment = Element.ALIGN_CENTER;
                            dateParagraph.SpacingAfter = 20f;
                            doc.Add(dateParagraph);

                            // Diseño de la tabla (agregar la columna de fecha y ocultar la columna de estado)
                            PdfPTable table = new PdfPTable(5); // Cinco columnas, incluyendo la columna de fecha
                            table.WidthPercentage = 100;
                            table.SpacingBefore = 10f;
                            table.SpacingAfter = 10f;
                            table.DefaultCell.Padding = 5;
                            table.DefaultCell.BorderColor = BaseColor.LIGHT_GRAY;

                            // Encabezados de columna
                            string[] headers = { "Fecha", "Nombre del Localidad", "Nombre del Empleado", "Estado", "Nota" };
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
                            foreach (object[] visita in visitas)
                            {
                                // Fecha
                                // Formatear la fecha para mostrar el nombre del día y la fecha en formato de texto
                                PdfPCell fechaCell = new PdfPCell(new Phrase(Convert.ToDateTime(visita[0]).ToString("dddd d 'de' MMMM")));
                                fechaCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                table.AddCell(fechaCell);

                                // Nombre del Localidad
                                PdfPCell localidadCell = new PdfPCell(new Phrase(visita[1].ToString()));
                                localidadCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                table.AddCell(localidadCell);

                                // Nombre del Empleado
                                PdfPCell empleadoCell = new PdfPCell(new Phrase(visita[2].ToString()));
                                empleadoCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                table.AddCell(empleadoCell);

                                // Estado
                                string estadoTexto = visita[3].ToString(); // Estado ya está en formato de texto
                                PdfPCell statusCell = new PdfPCell(new Phrase(estadoTexto));
                                statusCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                table.AddCell(statusCell);

                                // Nota
                                PdfPCell notaCell = new PdfPCell(new Phrase(visita.Length > 4 ? visita[4].ToString() : ""));
                                notaCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                table.AddCell(notaCell);
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

        }
    }
    }

