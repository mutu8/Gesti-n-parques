using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaLogica
{
    public static class FormUtil
    {
        private static Dictionary<Type, Form> openForms = new Dictionary<Type, Form>();

        public static bool TryOpenForm<TForm>(Func<TForm> createFormInstance) where TForm : Form
        {
            Type formType = typeof(TForm);

            if (openForms.ContainsKey(formType) && openForms[formType] != null && !openForms[formType].IsDisposed)
            {
                // Si el formulario ya está abierto, mostrarlo y retornar false
                openForms[formType].Focus();
                MessageBox.Show($"Ya hay un formulario abierto.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            // Crear una nueva instancia del formulario
            TForm form = createFormInstance();
            form.FormClosed += (s, args) => openForms.Remove(formType);

            // Mostrar el formulario y guardarlo en el diccionario
            form.Show();
            openForms[formType] = form;
            return true;
        }

        public static bool TryOpenFormSec(Func<Form> formFactory)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == formFactory().GetType())
                {
                    form.Activate();
                    return false; // Ya hay un formulario abierto del mismo tipo
                }
            }

            Form newForm = formFactory();
            newForm.FormClosed += (sender, args) => Application.OpenForms[0].Enabled = true; // Habilitar el formulario principal cuando se cierre el formulario secundario
            newForm.Show();
            Application.OpenForms[0].Enabled = false; // Deshabilitar el formulario principal
            return true; // Se abrió un nuevo formulario
        }

    }
}
