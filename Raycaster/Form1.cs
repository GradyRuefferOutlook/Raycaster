using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/* Grady Xander Rueffer
 * 152 446 035
 * April 3rd, 2024
 * 
 * Hello Mr. T.
 * Sorry for the later submission, this has been a rather long one to troubleshoot.
 * As it stands, the game is a tech demo made fun (At least I think so).
 * I hope it runs well on your computer, although it is higly CPU intensive (and I don't know the specs of your computer)
 * As far as I know, without removing some vision (I'm experimenting with this using lighting), it cannot run smoother without ruining the effect
 * Enjoy Raycaster!
 * 
 * Controlls:
 * WASD for movement
 * Left and Right Arrows to Turn
 * E to interact
 * (I experimented with the arcade controls, but they are very awkward)
 */




namespace Raycaster
{
    public partial class Form1 : Form
    {
        public static int width, height;

        //Define display fonts
        public static Font titleFont = new Font(new FontFamily("Antiquity print"), 105, FontStyle.Bold, GraphicsUnit.Pixel);
        public static Font infoFont = new Font(new FontFamily("Footlight MT Light"), 35, FontStyle.Bold, GraphicsUnit.Pixel);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            width = this.Width;
            height = this.Height;
            ChangeScreen(this, new GameScreen());
        }

        public static void ChangeScreen(object sender, UserControl next)
        {
            Form f; // will either be the sender or parent of sender

            if (sender is Form)
            {
                f = (Form)sender;                          //f is sender
            }
            else
            {
                UserControl current = (UserControl)sender;  //create UserControl from sender
                f = current.FindForm();                     //find Form UserControl is on
                f.Controls.Remove(current);                 //remove current UserControl
            }

            // add the new UserControl to the middle of the screen and focus on it
            next.Location = new Point(0, 0);
            f.Controls.Add(next);
            next.Focus();
        }
    }
}
