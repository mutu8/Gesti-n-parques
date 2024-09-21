using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MaterialSkin.Controls;

namespace CapaPresentacion
{
    public partial class FormSeleccionVisitasEmergente : MaterialForm
    {
        private List<int> _visitasNoCompletar;

        public List<int> VisitasNoCompletar
        {
            get { return _visitasNoCompletar; }
        }

        public FormSeleccionVisitasEmergente(DataTable dtVisitas)
        {
            try
            {
                InitializeComponent();
                _visitasNoCompletar = new List<int>();

                // Filtrar solo las visitas no completadas
                DataTable dtVisitasNoCompletadas = dtVisitas.AsEnumerable()
                    .Where(row => !Convert.ToBoolean(row["Estado"]))
                    .CopyToDataTable();

                CargarVisitas(dtVisitasNoCompletadas, panel1);

                // Panel para los botones y el checkbox de seleccionar todo
                Panel panelButtons = new Panel();
                panelButtons.Dock = DockStyle.Bottom;
                panelButtons.Height = 50;
                panelButtons.Padding = new Padding(10);

                // Agregar checkbox para seleccionar/deseleccionar todas las visitas
                CheckBox chkSelectAll = new CheckBox();
                chkSelectAll.Text = "Seleccionar/Deseleccionar Todo";
                chkSelectAll.AutoSize = true;
                chkSelectAll.Location = new Point(20, 10);
                chkSelectAll.CheckedChanged += ChkSelectAll_CheckedChanged;
                panelButtons.Controls.Add(chkSelectAll);

                // Agregar botón "OK"
                Button btnOK = new Button();
                btnOK.Text = "OK";
                btnOK.Size = new Size(75, 30);
                btnOK.Location = new Point(200, 10); // Ajusta la ubicación según tus necesidades
                btnOK.Click += btnOK_Click;
                panelButtons.Controls.Add(btnOK);

                // Panel para la lista de visitas
                Panel panelVisitas = new Panel();
                panelVisitas.Dock = DockStyle.Fill;
                panelVisitas.AutoScroll = true;
                panelVisitas.Padding = new Padding(10);

                // Agregar etiqueta explicativa
                Label lblInstrucciones = new Label();
                lblInstrucciones.Text = "Seleccione las visitas que desea marcar como completadas:";
                lblInstrucciones.AutoSize = true;
                lblInstrucciones.Location = new Point(20, 10);
                lblInstrucciones.Font = new Font(lblInstrucciones.Font, FontStyle.Bold);

                panelVisitas.Controls.Add(lblInstrucciones);

                // Agregar paneles al formulario
                this.Controls.Add(panelVisitas);
                this.Controls.Add(panelButtons);

                // Cargar visitas en el panel de visitas
                CargarVisitas(dtVisitasNoCompletadas, panelVisitas);
            }
            catch (Exception)
            {
                // Manejar la excepción sin mostrar MessageBox
            }
        }

        private void CargarVisitas(DataTable dtVisitas, Panel panelVisitas)
        {
            try
            {
                int posY = 40; // Ajustar posición para dejar espacio al checkbox "Seleccionar/Deseleccionar Todo"

                foreach (DataRow row in dtVisitas.Rows)
                {
                    int idVisita = Convert.ToInt32(row["ID_Visita"]);
                    string fechaVisita = Convert.ToDateTime(row["Fecha_Visita"]).ToString("dd/MM/yyyy");
                    string nombreLocalidad = row["Nombre_Localidad"].ToString();
                    string nombreEmpleado = row["Nombre_Completo_Empleado"].ToString();

                    // Crear checkbox para cada visita
                    CheckBox chkVisita = new CheckBox();
                    chkVisita.Tag = idVisita;
                    chkVisita.Text = $"{nombreLocalidad} - {nombreEmpleado}";
                    chkVisita.Checked = true; // Seleccionar por defecto
                    chkVisita.AutoSize = true;
                    chkVisita.Location = new Point(20, posY);
                    chkVisita.CheckedChanged += ChkVisita_CheckedChanged;

                    panelVisitas.Controls.Add(chkVisita); // Agregar checkbox al panel de visitas
                    posY += 30; // Incremento mayor para separación entre checkboxes
                }
            }
            catch (Exception)
            {
                // Manejar la excepción sin mostrar MessageBox
            }
        }

        private void ChkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = sender as CheckBox;
                bool isChecked = chk.Checked;

                foreach (Control control in this.Controls.OfType<Panel>().First().Controls)
                {
                    if (control is CheckBox && control != chk)
                    {
                        CheckBox chkVisita = (CheckBox)control;
                        chkVisita.Checked = isChecked;
                    }
                }
            }
            catch (Exception)
            {
                // Manejar la excepción sin mostrar MessageBox
            }
        }

        private void ChkVisita_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = sender as CheckBox;
                int idVisita = Convert.ToInt32(chk.Tag);

                if (chk.Checked)
                {
                    if (_visitasNoCompletar.Contains(idVisita))
                    {
                        _visitasNoCompletar.Remove(idVisita);
                    }
                }
                else
                {
                    if (!_visitasNoCompletar.Contains(idVisita))
                    {
                        _visitasNoCompletar.Add(idVisita);
                    }
                }
            }
            catch (Exception)
            {
                // Manejar la excepción sin mostrar MessageBox
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                // Devolver las visitas seleccionadas al formulario principal
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception)
            {
                // Manejar la excepción sin mostrar MessageBox
            }
        }
    }
}
