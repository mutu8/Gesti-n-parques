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
using CapaLogica;
using iTextSharp.text.pdf;
using iTextSharp.text;
using MaterialSkin.Controls;

namespace CapaPresentacion
{
    public partial class frmReporteAsistencias : MaterialForm
    {
        public frmReporteAsistencias()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            InitializeComboBox();
        }
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
        private void CargarPersonalLimpieza()
        {
            DataTable dtEmpleados = logEmleados.Instancia.datObtenerPersonalLimpiezaParaComboBox();
            comboBoxPersonal.DisplayMember = "NombreCompleto"; // El nombre a mostrar en el ComboBox
            comboBoxPersonal.ValueMember = "ID_Empleado"; // El valor asociado al item
            comboBoxPersonal.DataSource = dtEmpleados;
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

        private void frmReporteAsistencias_Load(object sender, EventArgs e)
        {

            // Establecer la fecha mínima en el DateTimePicker
            dateTimePicker1.MinDate = new DateTime(2024, 7, 1); // 1 de enero de 2024

            CargarPersonalLimpieza();

            // Ocultar controles si el CheckBox está marcado
            dateTimePicker1.Visible = false;
            // Ocultar controles si el CheckBox no está marcado
            cboMeses.Visible = false;
            comboBoxPersonal.Visible = false;
            domainUpDown1.Visible = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; // Establecer el resultado del diálogo a Cancel
            this.Close(); // Cerrar el formulario
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            // Obtener asistencias para una fecha específica
            if (dateTimePicker1.Visible)
            {
                // Capturar la fecha del DateTimePicker
                DateTime selectedDate = dateTimePicker1.Value;
                string formattedDate = selectedDate.ToString("yyyy-MM-dd");
                DateTime parsedDate = DateTime.ParseExact(formattedDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                // Obtener las asistencias de la fecha seleccionada
                DataTable dtAsistencias = logAsistencias.Instancia.ListarAsistenciasPorFecha(parsedDate);

                // Verificar si hay asistencias registradas para el día
                if (dtAsistencias.Rows.Count == 0)
                {
                    MessageBox.Show("No hay asistencias registradas para el día.");
                    return;
                }

                // Generar el PDF incluyendo la fecha
                GenerarReporteAsistencias(dtAsistencias, $"Reporte de Asistencias - {formattedDate}", false);
            }
            
            // Obtener asistencias para un rango de fechas basado en el mes y año
            else if (cboMeses.Visible && comboBoxPersonal.Visible && domainUpDown1.Visible)
            {
                // Verificar si se ha seleccionado un mes
                if (cboMeses.SelectedIndex == -1)
                {
                    MessageBox.Show("Por favor, seleccione un mes.");
                    return;
                }

                // Verificar si se ha seleccionado un empleado
                if (comboBoxPersonal.SelectedValue == null)
                {
                    MessageBox.Show("Por favor, seleccione un empleado.");
                    return;
                }

                // Verificar si se ha ingresado un año
                if (string.IsNullOrWhiteSpace(domainUpDown1.Text))
                {
                    MessageBox.Show("Por favor, ingrese un año.");
                    return;
                }

                int mes = cboMeses.SelectedIndex + 1; // Asegúrate de que los meses están indexados de 1 a 12
                int año;
                if (!int.TryParse(domainUpDown1.Text, out año))
                {
                    MessageBox.Show("El año ingresado no es válido.");
                    return;
                }
                int idEmpleado = Convert.ToInt32(comboBoxPersonal.SelectedValue);

                // Calcular el primer y último día del mes
                DateTime primerDiaDelMes = new DateTime(año, mes, 1);
                DateTime ultimoDiaDelMes = primerDiaDelMes.AddMonths(1).AddDays(-1);

                // Variable para verificar si hay al menos una asistencia
                bool hayAsistencias = false;

                // Validar asistencias para cada fecha en el rango del mes
                for (DateTime fecha = primerDiaDelMes; fecha <= ultimoDiaDelMes; fecha = fecha.AddDays(1))
                {
                    if (logAsistencias.Instancia.ValidarAsistenciasPorFecha(fecha))
                    {
                        hayAsistencias = true;
                        break; // No es necesario seguir verificando si ya hay asistencias
                    }
                }

                if (!hayAsistencias)
                {
                    MessageBox.Show("No hay asistencias registradas para el mes seleccionado.");
                    return;
                }

                // Obtener el nombre del mes en letras
                string mesNombre = primerDiaDelMes.ToString("MMMM", new CultureInfo("es-ES"));

                // Obtener las asistencias para el mes y año seleccionados
                DataTable dtAsistencias = logAsistencias.Instancia.ListarAsistenciasPorMesAñoEmpleado(idEmpleado, mes, año);

                // Obtener nombre del empleado
                string nombreEmpleado = comboBoxPersonal.Text;

                // Generar el PDF incluyendo la fecha
                GenerarReporteAsistencias(dtAsistencias, $"Reporte de asistencias del mensual", true);
            }



        }


        private void GenerarReporteAsistencias(DataTable dtAsistencias, string tituloReporte, bool incluirFecha)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Archivos PDF (*.pdf)|*.pdf";
                saveFileDialog.FileName = $"{tituloReporte}.pdf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Document doc = new Document();
                    try
                    {
                        PdfWriter.GetInstance(doc, new FileStream(saveFileDialog.FileName, FileMode.Create));
                        doc.Open();

                        // Título del reporte
                        iTextSharp.text.Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.DARK_GRAY);
                        Paragraph title = new Paragraph(tituloReporte, titleFont);
                        title.Alignment = Element.ALIGN_CENTER;
                        title.SpacingAfter = 10f;
                        doc.Add(title);

                        // Subtítulo: Sí Asistió
                        iTextSharp.text.Font subtitleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, BaseColor.BLACK);
                        Paragraph asistioTitle = new Paragraph("Asistió", subtitleFont);
                        asistioTitle.Alignment = Element.ALIGN_LEFT;
                        asistioTitle.SpacingAfter = 10f;
                        doc.Add(asistioTitle);

                        // Crear tabla para asistencias "Sí Asistió"
                        int numColumns = incluirFecha ? 2 : 1; // Determinar el número de columnas
                        PdfPTable tableAsistio = new PdfPTable(numColumns);
                        tableAsistio.WidthPercentage = 100;
                        tableAsistio.SpacingBefore = 10f;
                        tableAsistio.SpacingAfter = 10f;
                        tableAsistio.DefaultCell.Padding = 5;
                        tableAsistio.DefaultCell.BorderColor = BaseColor.LIGHT_GRAY;

                        // Encabezados de columna para "Sí Asistió"
                        string[] headersAsistio = incluirFecha ? new[] { "Fecha", "Nombre" } : new[] { "Nombre" };
                        iTextSharp.text.Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.WHITE);
                        PdfPCell headerCell = new PdfPCell
                        {
                            BackgroundColor = BaseColor.DARK_GRAY,
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            Padding = 5
                        };

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
                                if (incluirFecha)
                                {
                                    try
                                    {
                                        DateTime fechaAsistencia = Convert.ToDateTime(row["Fecha_Asistencia"]);
                                        tableAsistio.AddCell(new Phrase(fechaAsistencia.ToString("dd MMMM yyyy"))); // Formato de fecha completa
                                    }
                                    catch
                                    {
                                        tableAsistio.AddCell(""); // Omite la fecha en caso de error
                                    }
                                }

                                string nombreCompleto = $"{row["Nombres"]} {row["Apellidos"]}";
                                tableAsistio.AddCell(new Phrase(nombreCompleto));
                            }
                        }

                        // Agregar la tabla "Sí Asistió" al documento
                        doc.Add(tableAsistio);

                        // Subtítulo: No Asistió
                        Paragraph noAsistioTitle = new Paragraph("No Asistió", subtitleFont);
                        noAsistioTitle.Alignment = Element.ALIGN_LEFT;
                        noAsistioTitle.SpacingAfter = 10f;
                        doc.Add(noAsistioTitle);

                        // Crear tabla para asistencias "No Asistió"
                        PdfPTable tableNoAsistio = new PdfPTable(numColumns); // Número de columnas ajustado
                        tableNoAsistio.WidthPercentage = 100;
                        tableNoAsistio.SpacingBefore = 10f;
                        tableNoAsistio.SpacingAfter = 10f;
                        tableNoAsistio.DefaultCell.Padding = 5;
                        tableNoAsistio.DefaultCell.BorderColor = BaseColor.LIGHT_GRAY;

                        // Encabezados de columna para "No Asistió"
                        string[] headersNoAsistio = incluirFecha ? new[] { "Fecha", "Nombre" } : new[] { "Nombre" };

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
                                if (incluirFecha)
                                {
                                    try
                                    {
                                        DateTime fechaAsistencia = Convert.ToDateTime(row["Fecha_Asistencia"]);
                                        tableNoAsistio.AddCell(new Phrase(fechaAsistencia.ToString("dd MMMM yyyy"))); // Formato de fecha completa
                                    }
                                    catch
                                    {
                                        tableNoAsistio.AddCell(""); // Omite la fecha en caso de error
                                    }
                                }

                                string nombreCompleto = $"{row["Nombres"]} {row["Apellidos"]}";
                                tableNoAsistio.AddCell(new Phrase(nombreCompleto));
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


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (Seleccione.Checked)
            {
                // Mostrar controles si el CheckBox está marcado
                dateTimePicker1.Visible = false;

                // Ocultar controles si el CheckBox no está marcado
                cboMeses.Visible = true;
                comboBoxPersonal.Visible = true;
                domainUpDown1.Visible = true;

                Seleccione.Text = "Por mes";

            }
            else
            {
                // Ocultar controles si el CheckBox no está marcado
                cboMeses.Visible = false;
                comboBoxPersonal.Visible = false;
                domainUpDown1.Visible = false;

                // Mostrar controles si el CheckBox está marcado
                dateTimePicker1.Visible = true;

                Seleccione.Text = "Por fecha exacta";
            }
        }
    }
}
