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

                // Obtener las asistencias para el mes y año seleccionados
                DataTable dtAsistencias = logAsistencias.Instancia.ListarAsistenciasPorMesAñoEmpleado(idEmpleado, mes, año);

                // Verificar si hay asistencias registradas para el mes
                if (dtAsistencias.Rows.Count == 0)
                {
                    MessageBox.Show("No hay asistencias registradas para el mes seleccionado.");
                    return;
                }

                // Obtener el nombre del mes en letras
                string mesNombre = new DateTime(año, mes, 1).ToString("MMMM", new CultureInfo("es-ES"));

                // Obtener nombre del empleado
                string nombreEmpleado = comboBoxPersonal.Text;

                // Generar el PDF incluyendo la fecha
                GenerarReporteAsistencias(dtAsistencias, $"Reporte de asistencias del mes de {mesNombre} de {año} para {nombreEmpleado}", true);
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
                    // Crear documento PDF en modo horizontal
                    Document doc = new Document(PageSize.A4.Rotate());
                    try
                    {
                        // Crear el escritor de PDF
                        PdfWriter.GetInstance(doc, new FileStream(saveFileDialog.FileName, FileMode.Create));
                        doc.Open();

                        // Título del reporte
                        iTextSharp.text.Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20, BaseColor.BLACK);
                        Paragraph title = new Paragraph(tituloReporte, titleFont)
                        {
                            Alignment = Element.ALIGN_CENTER,
                            SpacingAfter = 20f
                        };
                        doc.Add(title);

                        // Crear tabla con columnas dinámicas basadas en si se incluye la fecha
                        PdfPTable table = new PdfPTable(incluirFecha ? 5 : 4)
                        {
                            WidthPercentage = 100,
                            SpacingBefore = 10f,
                            SpacingAfter = 10f
                        };
                        // Ajustar el ancho de las columnas (proporcionalmente)
                        float[] widths = incluirFecha ? new float[] { 2f, 1.5f, 1.5f, 2.5f, 2.5f } : new float[] { 1.5f, 1.5f, 2.5f, 2.5f };
                        table.SetWidths(widths);

                        // Añadir encabezados de tabla
                        AddTableHeader(table, incluirFecha);

                        // Añadir datos de la tabla
                        AddTableData(table, dtAsistencias, incluirFecha);

                        // Agregar la tabla completa al documento
                        doc.Add(table);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al generar el PDF: {ex.Message}");
                    }
                    finally
                    {
                        doc.Close(); // Cerrar el documento ANTES de abrirlo

                        // Abrir el PDF automáticamente
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

        private void AddTableHeader(PdfPTable table, bool incluirFecha)
        {
            // Definir encabezados
            string[] headers = incluirFecha ?
                new[] { "Fecha", "Opción", "Sector", "Nombres", "Apellidos" } :
                new[] { "Opción", "Sector", "Nombres", "Apellidos" };

            // Fuente para los encabezados
            iTextSharp.text.Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.WHITE);
            BaseColor headerBackgroundColor = new BaseColor(0, 119, 204); // Color azul oscuro

            // Añadir cada encabezado al PDF
            foreach (string header in headers)
            {
                PdfPCell cell = new PdfPCell(new Phrase(header, headerFont))
                {
                    BackgroundColor = headerBackgroundColor,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Padding = 8
                };
                table.AddCell(cell);
            }
        }

        private void AddTableData(PdfPTable table, DataTable dtAsistencias, bool incluirFecha)
        {
            // Fuente para los datos
            iTextSharp.text.Font dataFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK);
            BaseColor evenRowColor = new BaseColor(245, 245, 245); // Color gris claro para filas pares

            // Añadir datos fila por fila
            bool isEvenRow = false;
            foreach (DataRow row in dtAsistencias.Rows)
            {
                if (incluirFecha)
                {
                    DateTime fecha = Convert.ToDateTime(row["Fecha_Asistencia"]);
                    PdfPCell cellFecha = new PdfPCell(new Phrase(fecha.ToString("dd MMMM yyyy"), dataFont))
                    {
                        BackgroundColor = isEvenRow ? evenRowColor : BaseColor.WHITE,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        Padding = 5
                    };
                    table.AddCell(cellFecha);
                }

                // Opción
                PdfPCell cellOpcion = new PdfPCell(new Phrase(row["Opcion"].ToString(), dataFont))
                {
                    BackgroundColor = isEvenRow ? evenRowColor : BaseColor.WHITE,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Padding = 5
                };
                table.AddCell(cellOpcion);

                // Sector
                PdfPCell cellSector = new PdfPCell(new Phrase(row["Sector"].ToString(), dataFont))
                {
                    BackgroundColor = isEvenRow ? evenRowColor : BaseColor.WHITE,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Padding = 5
                };
                table.AddCell(cellSector);

                // Nombres
                PdfPCell cellNombres = new PdfPCell(new Phrase(row["Nombres"].ToString(), dataFont))
                {
                    BackgroundColor = isEvenRow ? evenRowColor : BaseColor.WHITE,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Padding = 5
                };
                table.AddCell(cellNombres);

                // Apellidos
                PdfPCell cellApellidos = new PdfPCell(new Phrase(row["Apellidos"].ToString(), dataFont))
                {
                    BackgroundColor = isEvenRow ? evenRowColor : BaseColor.WHITE,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Padding = 5
                };
                table.AddCell(cellApellidos);

                isEvenRow = !isEvenRow; // Alternar color de fila
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
