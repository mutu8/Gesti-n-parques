using CapaLogica;
using CapaPresentación.Formularios;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MaterialSkin.Controls;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

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

            dateTimePicker1.MinDate = DateTime.Today;
            cboEncargado.Text = logVisitas.Instancia.ObtenerNombreCompletoEmpleadoPorIdLocalidad(idLocalidad);

            this.Text = logLocalidades.Instancia.ObtenerNombreLocPorID(idLocalidad);
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
            dgv.DefaultCellStyle.Font = new System.Drawing.Font("Roboto", 10);
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

            // Validar si el ComboBox de empleado está vacío
            if (string.IsNullOrWhiteSpace(cboEncargado.Text))
            {
                MessageBox.Show("Por favor, seleccione un empleado de la lista.");
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
                MessageBox.Show("Ya hay una visita programada para la fecha ingresada, por favor, verifique los datos.");
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

        private void btnImprimier_Click(object sender, EventArgs e)
        {
            if (dgvVisitas.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.");
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Archivos PDF (*.pdf)|*.pdf";
                saveFileDialog.FileName = this.Text + ".pdf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Document doc = new Document();
                    try
                    {
                        PdfWriter.GetInstance(doc, new FileStream(saveFileDialog.FileName, FileMode.Create));
                        doc.Open();

                        // Agregar título encima de la tabla
                        iTextSharp.text.Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);
                        Paragraph title = new Paragraph("Reporte de Visitas", titleFont);
                        title.Alignment = Element.ALIGN_CENTER;
                        title.SpacingAfter = 20f; // Espacio después del título
                        doc.Add(title);


                        // Diseño de la tabla (sin la columna ID)
                        PdfPTable table = new PdfPTable(dgvVisitas.Columns.Count - 1); // Una columna menos
                        table.WidthPercentage = 100;
                        table.SpacingBefore = 10f;
                        table.SpacingAfter = 10f;
                        table.DefaultCell.Padding = 5;
                        table.DefaultCell.BorderColor = BaseColor.LIGHT_GRAY;

                        // Array de encabezados (sin ID)
                        string[] headers = { "Fecha de Visita", "Lugar de Visita", "Responsable", "Estado" };

                        // Estilo de los encabezados
                        iTextSharp.text.Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);

                        // Encabezados de columna
                        for (int i = 1; i < dgvVisitas.Columns.Count; i++) // Empezar desde la segunda columna
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(headers[i - 1], headerFont)); // Ajustar índice
                            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            table.AddCell(cell);
                        }

                        // Datos (con formato de fecha, alineación y cambio en "Completa")
                        foreach (DataGridViewRow row in dgvVisitas.Rows)
                        {
                            if (row.IsNewRow) continue;

                            for (int i = 1; i < row.Cells.Count; i++) // Empezar desde la segunda celda
                            {
                                DataGridViewCell cell = row.Cells[i];

                                string cellValue;
                                if (cell.OwningColumn.Name == "Estado")
                                {
                                    // Corrección del valor booleano
                                    if (bool.TryParse(cell.Value?.ToString(), out bool completa))
                                    {
                                        cellValue = completa ? "Completada" : "No completada";
                                    }
                                    else
                                    {
                                        cellValue = "Desconocido"; // Valor por defecto si no es booleano
                                    }
                                }
                                else if (cell.Value is DateTime date)
                                {
                                    cellValue = date.ToString("dd/MM/yyyy");
                                }
                                else
                                {
                                    cellValue = cell.Value?.ToString() ?? "";
                                }

                                PdfPCell dataCell = new PdfPCell(new Phrase(cellValue));
                                dataCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                table.AddCell(dataCell);
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
    }
}
