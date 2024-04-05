using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Raycaster
{
    public partial class MenuScreen : UserControl
    {
        //Define Image and brushes
        Image backdrop = Properties.Resources.MenuBackdrop;
        SolidBrush textBrush = new SolidBrush(Color.White);

        public MenuScreen()
        {
            InitializeComponent();
        }

        private void MenuScreen_Load(object sender, EventArgs e)
        {
            this.Size = new Size(Form1.width, Form1.height);
        }

        private void MenuScreen_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(backdrop, new Rectangle(0, 0, Form1.width, Form1.height));
            e.Graphics.DrawString("Hi There", Form1.titleFont, textBrush, new Point(25, 25));
            e.Graphics.DrawString("Hi There", Form1.infoFont, textBrush, new Rectangle(25, 100, 800, 50));
        }
    }
}
