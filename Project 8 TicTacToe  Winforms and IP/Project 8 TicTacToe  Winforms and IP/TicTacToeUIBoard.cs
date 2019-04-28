using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_8_TicTacToe__Winforms_and_IP
{
    public partial class TicTacToeUIBoard : Form
    {
        public TicTacToeGame currentGame = new TicTacToeGame();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Disable_Buttons();
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentGame.Clear_Board();

            try //this will help ignore not button objects on winforms in the upcoming foreach statement
            {
                foreach (Control c in Controls)
                {
                    Button b = (Button)c;
                    b.Enabled = true;
                }
            }
            catch { }

            MessageBox.Show("{0} goes first!", currentGame.Check_Player().ToString());
        }

        public TicTacToeUIBoard()
        {
            InitializeComponent();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is a two player tic tac toe game. Designed by William A.");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (Validate_Button(b))
            {
                if (currentGame.Check_Player() == TURN.Player1)
                {
                    b.Text = "X";
                }
                else
                {
                    b.Text = "O";
                }
                currentGame.Update_Board(b.Tag);
                if (currentGame.Check_Winner())
                {
                    MessageBox.Show("{0} wins!", currentGame.Check_Player().ToString());
                    Disable_Buttons();
                }
                if (currentGame.Check_For_Draw())
                {
                    Disable_Buttons();
                    MessageBox.Show("Draw!");  
                }
                currentGame.Update_Player();
                currentGame.Update_turnCount();

            }
            
        }

        private bool Validate_Button(Button button)
        {
            if (button.Text == "")
            {
                return true;
            }
            else
            {
                MessageBox.Show("Can't click there!");
                return false;
            }
        }

        private void Disable_Buttons()
        {
            try //this will help ignore not button objects on winforms in the upcoming foreach statement
            {
                foreach (Control c in Controls)
                {
                    Button b = (Button)c;
                    b.Enabled = false;
                }
            }
            catch { }

        }

       
    }
}
