using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DarkCollege
{
    public partial class frmDarkCollege : Form
    {
        /*----------Class variables----------*/

        private Game gameLevel = new Game(); //Main game object for managing game functions, etc
        private Graphics g;

        /*---------Class functions-----------*/
        public frmDarkCollege()
        {
            InitializeComponent();
        }

        private void frmDarkCollege_Load(object sender, EventArgs e)
        {
            g = canvas.CreateGraphics();
            gameLevel.startGraphics(g);
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            gameLevel.reDrawGraphics(g);
        }

        private void frmDarkCollege_KeyDown(object sender, KeyEventArgs e)
        {
            gameLevel.changeGameState(e.KeyCode);
        }

        private void tmrClock_Tick(object sender, EventArgs e)
        {
            gameLevel.reDrawGraphics(g);
            int endGameValue = gameLevel.updateHealth(-1);
            if (endGameValue <= 0)
            {
                this.Close();
            }
            else
            {
                lblHealth.Text = "Health: " + endGameValue.ToString();
            }
        }
    }
}
