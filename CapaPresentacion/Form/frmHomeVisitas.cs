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
using MaterialSkin.Controls;

namespace CapaPresentacion
{
    public partial class frmHomeVisitas : MaterialForm
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

            dataGridView1.Visible = false;
            this.MaximizeBox = false;

            // Configurar el texto del ToolTip para un botón
            toolTip1.SetToolTip(materialFloatingActionButton1, "Programar visitas del día");
            toolTip1.SetToolTip(btnAddNota, "Agregar nota");
            toolTip1.SetToolTip(btnUpdate, "Marcar las visitas como completadas");
            toolTip1.SetToolTip(btnImprimier, "Imprimir reporte del día");
            toolTip1.SetToolTip(materialFloatingActionButton2, "Imprimir reporte de fecha específica");
        }

        private void frmHomeVisitas_Load(object sender, EventArgs e)
        {
            validarDGV();
        }   

        private void validarDGV() 
        {
            if (!logVisitas.Instancia.NoHayVisitasEnElDía())
            {
                AplicarDisenoDataGridView(dataGridView1);
                MostrarLocalidadesConEmpleadosEnDGV();
            }

            else
            {
                ConfigurarDataGridView(dataGridView1);
                MostrarVisitasRegistradasEnDGV();
                dataGridView1.Visible = true;
                dataGridView1.ReadOnly = true;
                btnAddNota.Enabled = false;
                materialFloatingActionButton1.Enabled = false;
            }
        }
        private void MostrarVisitasRegistradasEnDGV()
        {
            try
            {
                // Obtener las visitas del día desde logVisitas
                DataTable dtVisitas = logVisitas.Instancia.ListarVisitasParaDgv(DateTime.Today);

                if (dtVisitas.Rows.Count == 0)
                {
                    MessageBox.Show("No hay visitas registradas para el día.");
                    dataGridView1.DataSource = null; // Limpiar el DataGridView si no hay datos
                    return;
                }

                // Añadir una columna para el estado como texto
                if (!dtVisitas.Columns.Contains("EstadoTexto"))
                {
                    dtVisitas.Columns.Add("EstadoTexto", typeof(string));
                }

                // Actualizar el estado en cada fila
                foreach (DataRow row in dtVisitas.Rows)
                {
                    bool isCompletada = Convert.ToBoolean(row["Estado"]);
                    row["EstadoTexto"] = isCompletada ? "Completada" : "No Completada";
                }

                // Asignar el DataTable con las visitas al DataSource del DataGridView
                dataGridView1.DataSource = dtVisitas;

                // Ocultar columnas que no se necesitan mostrar
                if (dataGridView1.Columns.Contains("ID_Visita"))
                {
                    dataGridView1.Columns["ID_Visita"].Visible = false;
                }

                // Ocultar la columna 'Fecha_Visita'
                if (dataGridView1.Columns.Contains("Fecha_Visita"))
                {
                    dataGridView1.Columns["Fecha_Visita"].Visible = false;
                }

                // Configurar columnas visibles y nombres
                if (dataGridView1.Columns.Contains("Nombre_Localidad"))
                {
                    dataGridView1.Columns["Nombre_Localidad"].HeaderText = "Nombre de la Localidad";
                }

                // Configurar la columna 'NombreEmpleado' para mostrar "Personal Encargado"
                if (dataGridView1.Columns.Contains("NombreEmpleado"))
                {
                    dataGridView1.Columns["NombreEmpleado"].HeaderText = "Personal Encargado";
                }

                // Ocultar la columna 'Estado' y mostrar 'EstadoTexto'
                if (dataGridView1.Columns.Contains("Estado"))
                {
                    dataGridView1.Columns["Estado"].Visible = false;
                }
                if (dataGridView1.Columns.Contains("EstadoTexto"))
                {
                    dataGridView1.Columns["EstadoTexto"].HeaderText = "Estado";
                }

                // Crear y configurar la columna ComboBox para las notas (si no está ya añadida)
                if (!dataGridView1.Columns.Contains("Nota"))
                {
                    DataGridViewComboBoxColumn comboColumnNota = new DataGridViewComboBoxColumn
                    {
                        HeaderText = "Nota",
                        Name = "Nota",
                        DataSource = new List<string> { "", "Limpieza general", "Podado", "Limpieza de pozos" },
                        AutoComplete = true,
                        DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox,
                        FlatStyle = FlatStyle.Flat
                    };

                    dataGridView1.Columns.Add(comboColumnNota);
                }

                // Ajustar el ancho de las columnas automáticamente
                dataGridView1.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las visitas registradas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarLocalidadesConEmpleadosEnDGV()
        {
            try
            {
                // Obtener la lista de localidades con empleados
                List<int> localidadesConEmpleados = new List<int>();
                foreach (DataRow row in dtLocalidadesConEmpleados.Rows)
                {
                    int idLocalidad = Convert.ToInt32(row["ID_Localidad"]);
                    localidadesConEmpleados.Add(idLocalidad);
                }

                // Filtrar las localidades que ya tienen visita programada o completada
                DateTime fechaHoy = DateTime.Today;
                List<int> localidadesConVisitaActiva = new List<int>();

                foreach (int idLocalidad in localidadesConEmpleados)
                {
                    if (logVisitas.Instancia.ExisteVisitaActivaParaFechaLocalidadYEmpleado(fechaHoy, idLocalidad))
                    {
                        localidadesConVisitaActiva.Add(idLocalidad);
                    }
                }

                // Asignar el DataTable filtrado como DataSource del DataGridView
                DataTable dtFiltrado = dtLocalidadesConEmpleados.Clone(); // Clonar la estructura del DataTable original
                foreach (DataRow row in dtLocalidadesConEmpleados.Rows)
                {
                    int idLocalidad = Convert.ToInt32(row["ID_Localidad"]);
                    if (!localidadesConVisitaActiva.Contains(idLocalidad))
                    {
                        dtFiltrado.Rows.Add(row.ItemArray); // Agregar la fila al DataTable filtrado
                    }
                }

                dataGridView1.DataSource = dtFiltrado;

                // Ocultar la columna ID_Localidad si existe
                if (dataGridView1.Columns.Contains("ID_Localidad"))
                {
                    dataGridView1.Columns["ID_Localidad"].Visible = false;
                }

                // Crear y configurar la columna ComboBox para los empleados
                DataGridViewComboBoxColumn comboColumnEmpleado = new DataGridViewComboBoxColumn
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

                // Añadir la columna ComboBox de empleados al DataGridView
                dataGridView1.Columns.Add(comboColumnEmpleado);

                // Crear y configurar la columna ComboBox para las notas
                DataGridViewComboBoxColumn comboColumnNota = new DataGridViewComboBoxColumn
                {
                    HeaderText = "Nota",
                    Name = "Nota",
                    DataSource = new List<string> { "", "Limpieza general", "Podado", "Limpieza de pozos" },
                    AutoComplete = true,
                    DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox,
                    FlatStyle = FlatStyle.Flat
                };

                // Añadir la columna ComboBox de notas al DataGridView
                dataGridView1.Columns.Add(comboColumnNota);

                // Remover la columna de nombre del empleado para evitar duplicación
                dataGridView1.Columns["NombreEmpleado"].Visible = false;

                // Identificar el índice final para la columna "Nota"
                int lastIndex = dataGridView1.Columns.Count; // Total de columnas actualmente en el DataGridView

                // Asegúrate de que "Nota" se coloque al final
                dataGridView1.Columns["Nota"].DisplayIndex = 5;
                
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

                if (logVisitas.Instancia.NoHayVisitasEnElDía() == true)
                {
                    MessageBox.Show("Ya se han generado visitas para el día de hoy");
                    return;
                }

                if (dataGridView1.Visible == false)
                {
                    DialogResult result = MessageBox.Show("¿Desea modificar el personal asigando predeterminado?", "Confirmación", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        dataGridView1.Visible = true;
                    }
                    return;
                }

                if (dataGridView1.Visible == true)
                {
                    DialogResult result2 = MessageBox.Show("¿Desea generar las visitas", "Confirmación", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (result2 == DialogResult.Yes)
                    {
                        if (logVisitas.Instancia.NoHayVisitasEnElDía() == false)
                        {
                            // Iterar sobre todas las filas seleccionadas del DataGridView
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                // Obtener el ID_Localidad y ID_Empleado de cada fila seleccionada
                                int idLocalidad = Convert.ToInt32(row.Cells["ID_Localidad"].Value);
                                int idEmpleado = Convert.ToInt32(row.Cells["Empleado"].Value);
                                string nota = Convert.ToString(row.Cells["Nota"].Value);

                                // Verificar si ya existe una visita activa para esta fecha, localidad e idEmpleado
                                bool visitaActiva = logVisitas.Instancia.ExisteVisitaActivaParaFechaLocalidadYEmpleado(fechaVisita, idLocalidad);

                                if (!visitaActiva)
                                {
                                    // Llamar al método de logVisitas para generar visita con los datos actuales
                                    bool exito = logVisitas.Instancia.GenerarVisitasParaLocalidades(fechaVisita, estado, idLocalidad, idEmpleado, nota);
                                }
                            }
                            btnAddNota.Enabled = false;
                            materialFloatingActionButton1.Enabled = false;
                            MessageBox.Show("Visitas generadas exitosamente.");

                        }
                    }
                    return;
                }
                
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
            test();

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

        private void btnImprimier_Click(object sender, EventArgs e)
        {
            try
            {
                // Captura la fecha actual
                DateTime fechaActual1 = DateTime.Now;

                // Convierte la fecha al formato deseado (yyyy-MM-dd)
                string fechaFormateada = fechaActual1.ToString("yyyy-MM-dd");

                // Parsea la fecha formateada a un tipo DateTime
                DateTime fechaParaConsulta = DateTime.ParseExact(fechaFormateada, "yyyy-MM-dd", null);

                // Obtener las visitas del día desde logVisitas usando la fecha parseada
                DataTable dtVisitas = logVisitas.Instancia.ListarVisitasParaDgv(fechaParaConsulta);


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

                            // Tabla de visitas completadas
                            Paragraph completadasTitle = new Paragraph("Visitas Completadas", titleFont);
                            completadasTitle.Alignment = Element.ALIGN_CENTER;
                            completadasTitle.SpacingAfter = 10f;
                            doc.Add(completadasTitle);

                            PdfPTable tableCompletadas = new PdfPTable(3); // Tres columnas: Nombre del Parque, Nombre del Encargado, Nota
                            tableCompletadas.WidthPercentage = 100;
                            tableCompletadas.SpacingBefore = 10f;
                            tableCompletadas.SpacingAfter = 10f;
                            tableCompletadas.DefaultCell.Padding = 5;
                            tableCompletadas.DefaultCell.BorderColor = BaseColor.LIGHT_GRAY;

                            // Encabezados de columna para completadas
                            string[] headersCompletadas = { "Nombre del Parque", "Nombre del Encargado", "Nota" };

                            // Estilo de los encabezados para completadas
                            iTextSharp.text.Font headerFontCompletadas = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.WHITE);

                            // Fondo del encabezado para completadas
                            PdfPCell headerCellCompletadas = new PdfPCell();
                            headerCellCompletadas.BackgroundColor = BaseColor.DARK_GRAY;
                            headerCellCompletadas.HorizontalAlignment = Element.ALIGN_CENTER;
                            headerCellCompletadas.Padding = 5;

                            // Encabezados de columna para completadas
                            foreach (string header in headersCompletadas)
                            {
                                headerCellCompletadas.Phrase = new Phrase(header, headerFontCompletadas);
                                tableCompletadas.AddCell(headerCellCompletadas);
                            }

                            // Datos para completadas (con formato de fecha y estado)
                            foreach (DataRow row in dtVisitas.Rows)
                            {
                                bool estado = Convert.ToBoolean(row["Estado"]);
                                if (estado)
                                {
                                    PdfPCell parkCell = new PdfPCell(new Phrase(row["Nombre_Localidad"].ToString()));
                                    parkCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tableCompletadas.AddCell(parkCell);

                                    PdfPCell employeeCell = new PdfPCell(new Phrase(row["NombreEmpleado"].ToString()));
                                    employeeCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tableCompletadas.AddCell(employeeCell);

                                    PdfPCell notaCell = new PdfPCell(new Phrase(row["Nota"].ToString()));
                                    notaCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tableCompletadas.AddCell(notaCell);
                                }
                            }

                            // Agregar tabla de completadas al documento
                            doc.Add(tableCompletadas);

                            // Espacio entre tablas
                            Paragraph space = new Paragraph("\n\n");
                            doc.Add(space);

                            // Tabla de visitas no completadas
                            Paragraph noCompletadasTitle = new Paragraph("Visitas No Completadas", titleFont);
                            noCompletadasTitle.Alignment = Element.ALIGN_CENTER;
                            noCompletadasTitle.SpacingAfter = 10f;
                            doc.Add(noCompletadasTitle);

                            PdfPTable tableNoCompletadas = new PdfPTable(3); // Tres columnas: Nombre del Parque, Nombre del Encargado, Nota
                            tableNoCompletadas.WidthPercentage = 100;
                            tableNoCompletadas.SpacingBefore = 10f;
                            tableNoCompletadas.SpacingAfter = 10f;
                            tableNoCompletadas.DefaultCell.Padding = 5;
                            tableNoCompletadas.DefaultCell.BorderColor = BaseColor.LIGHT_GRAY;

                            // Encabezados de columna para no completadas
                            string[] headersNoCompletadas = { "Nombre del Parque", "Nombre del Encargado", "Nota" };

                            // Estilo de los encabezados para no completadas
                            iTextSharp.text.Font headerFontNoCompletadas = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.WHITE);

                            // Fondo del encabezado para no completadas
                            PdfPCell headerCellNoCompletadas = new PdfPCell();
                            headerCellNoCompletadas.BackgroundColor = BaseColor.DARK_GRAY;
                            headerCellNoCompletadas.HorizontalAlignment = Element.ALIGN_CENTER;
                            headerCellNoCompletadas.Padding = 5;

                            // Encabezados de columna para no completadas
                            foreach (string header in headersNoCompletadas)
                            {
                                headerCellNoCompletadas.Phrase = new Phrase(header, headerFontNoCompletadas);
                                tableNoCompletadas.AddCell(headerCellNoCompletadas);
                            }

                            // Datos para no completadas (con formato de fecha y estado)
                            foreach (DataRow row in dtVisitas.Rows)
                            {
                                bool estado = Convert.ToBoolean(row["Estado"]);
                                if (!estado)
                                {
                                    PdfPCell parkCell = new PdfPCell(new Phrase(row["Nombre_Localidad"].ToString()));
                                    parkCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tableNoCompletadas.AddCell(parkCell);

                                    PdfPCell employeeCell = new PdfPCell(new Phrase(row["NombreEmpleado"].ToString()));
                                    employeeCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tableNoCompletadas.AddCell(employeeCell);

                                    PdfPCell notaCell = new PdfPCell(new Phrase(row["Nota"].ToString()));
                                    notaCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tableNoCompletadas.AddCell(notaCell);
                                }
                            }

                            // Agregar tabla de no completadas al documento
                            doc.Add(tableNoCompletadas);
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
                else 
                {
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
                            dataGridView1.DataSource = null; // Esto desasocia el DataSource y limpia los datos.
                            dataGridView1.Rows.Clear(); // Esto elimina todas las filas.
                            dataGridView1.Columns.Clear(); // Esto elimina todas las columnas.
                            validarDGV();
                        }
                        else
                        {
                            MessageBox.Show("Hubo un problema al marcar las visitas. Por favor, revise los registros.");
                        }
                    }
                    else if (result == DialogResult.No)
                    {
                        // Verificar si no hay visitas pendientes del día
                        if (logVisitas.Instancia.NoHayVisitasPendientesDelDia())
                        {
                            MessageBox.Show("No hay visitas pendientes para el día.");
                            return; // Salir del método si no hay visitas pendientes
                        }

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
                                    dataGridView1.DataSource = null; // Esto desasocia el DataSource y limpia los datos.
                                    dataGridView1.Rows.Clear(); // Esto elimina todas las filas.
                                    dataGridView1.Columns.Clear(); // Esto elimina todas las columnas.
                                    validarDGV();
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
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void materialFloatingActionButton2_Click(object sender, EventArgs e)
        {
            using (frmReportesFechas reportesFechas = new frmReportesFechas())
            {
                reportesFechas.StartPosition = FormStartPosition.CenterScreen;

                // Mostrar el formulario como un cuadro de diálogo
                if (reportesFechas.ShowDialog() == DialogResult.OK)
                {

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

        private void btnAddNota_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar si el DataGridView está visible
                if (dataGridView1.Visible)
                {
                    // Solicitar al usuario que ingrese un nuevo valor para la nota
                    string nuevaNota = Microsoft.VisualBasic.Interaction.InputBox("Ingrese una nueva nota:", "Agregar Nota", "");

                    // Verificar que el usuario haya ingresado un valor
                    if (!string.IsNullOrEmpty(nuevaNota))
                    {
                        // Encontrar la columna ComboBox de nota en el DataGridView
                        DataGridViewComboBoxColumn comboColumnNota = null;
                        foreach (DataGridViewColumn column in dataGridView1.Columns)
                        {
                            if (column.Name == "Nota")
                            {
                                comboColumnNota = column as DataGridViewComboBoxColumn;
                                break;
                            }
                        }

                        // Si se encuentra la columna ComboBox de nota
                        if (comboColumnNota != null)
                        {
                            // Añadir el nuevo valor al DataSource del ComboBox
                            if (comboColumnNota.DataSource is List<string> notas)
                            {
                                notas.Add(nuevaNota);
                            }
                            else
                            {
                                comboColumnNota.DataSource = new List<string> { "limpieza general", "podado", "limpieza de pozos", nuevaNota };
                            }

                            // Actualizar el ComboBox en cada fila del DataGridView
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                var cell = row.Cells["Nota"] as DataGridViewComboBoxCell;
                                if (cell != null)
                                {
                                    cell.DataSource = null;
                                    cell.DataSource = comboColumnNota.DataSource;
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe ingresar un valor para la nota.", "Valor requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Aún no ha seleccionado el listado de puntos", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar la nueva nota: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
