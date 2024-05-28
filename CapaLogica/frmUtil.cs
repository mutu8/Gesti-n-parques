using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaLogica
{
    internal class frmUtil
    {
        private static Dictionary<Type, Form> openForms = new Dictionary<Type, Form>();

        public static bool TryOpenForm<TForm>(Func<TForm> createFormInstance) where TForm : Form
        {
            Type formType = typeof(TForm);

            if (openForms.ContainsKey(formType) && openForms[formType] != null && !openForms[formType].IsDisposed)
            {
                // Si el formulario ya está abierto, mostrarlo y retornar false
                openForms[formType].Focus();
                MessageBox.Show($"{formType.Name} ya está abierto.", "Formulario Abierto", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
