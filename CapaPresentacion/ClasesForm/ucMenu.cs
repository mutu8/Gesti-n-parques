using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CapaPresentacion.ClasesForm
{
    public partial class ucMenu : UserControl
    {

        string menuTitle = "";
        Image icon;
        Color _borderColor = Color.Transparent;

        public event EventHandler menuClick;
        public string Menu
        {
            get
            {
                return menuTitle;
            }
            set
            {
                menuTitle = value;
                this.Invalidate();
            }
        }
        public Image Icon
        {

            get
            {
                return icon;
            }
            set
            {
                icon = value;
                this.Invalidate();
            }

        }
        public Color BorderColor
        {
            get
            {
                return _borderColor;
            }
            set
            {
                _borderColor = value;
                this.Invalidate();
            }
        }

        public ucMenu()
        {
            InitializeComponent();
        }

        private void menu_Paint(object sender, PaintEventArgs e)
        {
            menu.Text = menuTitle;
            menu.Image = icon;
            borderPanel.BackColor = _borderColor;
        }

        private void menu_Click_1(object sender, EventArgs e)
        {
            menuClick?.Invoke(this, e);
        }
    }
}
