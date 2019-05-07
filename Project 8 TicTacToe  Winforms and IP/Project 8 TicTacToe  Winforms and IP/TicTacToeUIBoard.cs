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
    partial class TicTacToeUIBoard : Form
    {
        private System.Windows.Forms.Button[] buttonArray = new Button[9];

        public TicTacToeGame currentGame = new TicTacToeGame();

        TCPConnection tcpconnection = new TCPConnection();

        public TicTacToeUIBoard()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            buttonArray[0] = A1;
            buttonArray[1] = A2;
            buttonArray[2] = A3;
            buttonArray[3] = B1;
            buttonArray[4] = B2;
            buttonArray[5] = B3;
            buttonArray[6] = C1;
            buttonArray[7] = C2;
            buttonArray[8] = C3;
            Disable_Buttons();
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentGame.Clear_Board();
            Clear_UI_Board();
            Enable_Buttons();

            if (tcpconnection.isHost)
            {
                currentGame.SetCharacter(PLAYER.X);
            }
            else
            {
                currentGame.SetCharacter(PLAYER.O);   
            }

            if ((tcpconnection.isHost))
            {
                PLAYER firstMove = (currentGame.Who_Goes_First());
                currentGame.Set_Turn(firstMove);
                char firstMoveChar =firstMove==PLAYER.X?'X':'O';
                tcpconnection.stream.WriteByte((byte)(firstMoveChar));
            }
            else
            {
                char firstMove = (char)tcpconnection.stream.ReadByte();
                if (firstMove == 'X' || firstMove == 'O')
                {
                    currentGame.Set_Turn((firstMove == 'X' ? PLAYER.X : PLAYER.O));
                }
                else
                {
                    throw new Exception("Cannot read host's first move decision!");
                }              
            }
            if (currentGame.IsMyTurn())
            {
                MessageDisplay.Text = "Your turn!";
                Refresh_Message_Box();
            }
            else
            {
                //MessageBox.Show("Other Player goes.");
                MessageDisplay.Text = "Other Player goes!";
                Refresh_Message_Box();
                Read_Byte_Update_Game_And_Board();
            }
        }

       



        private void Button_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (!currentGame.IsMyTurn())
            {
                MessageBox.Show("Not my turn, Waiting for other player!");
                return;
            }
 
            if (Validate_Button(b))
            {
                b.Text = currentGame.myCharacter == PLAYER.X ? "X" : "O";

                b.Invalidate();
                b.Update();
                b.Refresh();
                Application.DoEvents();

                char myMovePosition = b.Tag.ToString()[0];
                int pos = myMovePosition - '0' - 1; //subtract out 1 to account for indexing
                int row = pos / 3;
                int column = pos % 3;

                currentGame.EnterMove(row, column);
                //Button opponentsButton = this.buttonArray[pos];
                //opponentsButton.Text = currentGame.myCharacter == PLAYER.X ? "X" : "O";

                bool gameOver = false;
                if (currentGame.Check_Winner())
                {
                    MessageBox.Show($"{currentGame.Check_Player()} wins!");
                    MessageDisplay.Text = "";
                    Refresh_Message_Box();
                    Disable_Buttons();
                    gameOver = true;
                }
                if (gameOver == false && currentGame.Check_For_Draw())
                {
                    Disable_Buttons();
                    MessageBox.Show("Draw!");
                    gameOver = true;
                }

                //player who just made selection's computer freezes after WriteByte due to synchronous nature of method
                tcpconnection.stream.WriteByte((byte)myMovePosition);
                //tcpconnection.stream.BeginWrite(...);

                if (!gameOver) {   // if game not over...
                    currentGame.Update_Player();
                    currentGame.Update_turnCount();
                    MessageDisplay.Text = "Other Player goes!";

                    MessageDisplay.Invalidate();
                    MessageDisplay.Update();
                    MessageDisplay.Refresh();
                    Application.DoEvents();

                    //MessageBox.Show("Other Player goes.");


                    // find the button for row, column and mark it's text to the other player
                    Read_Byte_Update_Game_And_Board(); 
                }
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
            for (int i = 0; i < buttonArray.Length; i++)
            {
                buttonArray[i].Enabled = false;
            }

        }

        private void Connect_Player_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            tcpconnection.Join_Game(IPAdress.Text);

        }

        private void Read_Byte_Update_Game_And_Board()
        {
            char remotePlayerPosition = (char)tcpconnection.stream.ReadByte();
            if (remotePlayerPosition < '1' || remotePlayerPosition > '9')
            {
                throw new Exception("Opponent's move outside of 1-9 boundaries!");
            }
            int pos = remotePlayerPosition - '0' - 1; //subtract out 1 to account for indexing
            int row = pos / 3;
            int column = pos % 3;
            // validate that the board postion is free;
            if (currentGame.IsEmptySquare(row, column))
            {
                currentGame.EnterMove(row, column);
            }
            else
            {
                throw new Exception("Opponent's move is illegal!");
            }
            Button opponentsButton = this.buttonArray[pos];
            opponentsButton.Text = currentGame.myCharacter == PLAYER.X ? "O" : "X";
            bool gameOver = false;
            if (currentGame.Check_Winner())
            {
                MessageBox.Show($"{currentGame.Check_Player()} wins!");
                MessageDisplay.Text = "";
                Refresh_Message_Box();
                Disable_Buttons();
                gameOver = true;
            }
            if (gameOver!= true && currentGame.Check_For_Draw())
            {
                Disable_Buttons();
                MessageDisplay.Text = "Draw!";
                gameOver = true;
            }
            if (!gameOver)
            {
                currentGame.Update_Player();
                currentGame.Update_turnCount();
                MessageDisplay.Text = "Your turn!";
                //MessageBox.Show("Your turn!");
            }
        }

        private void Clear_UI_Board()
        {
            for (int i = 0; i < buttonArray.Length; i++)
            {
                buttonArray[i].Text = "";
            }
        }

        private void Enable_Buttons()
        {
            for (int i = 0; i < buttonArray.Length; i++)
            {
                buttonArray[i].Enabled = true;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is a two player tic tac toe game designed by William A.\n" +
                "Please enter the IPv4 address of your opponent. Click on File->Join Game and then File-> New Game" +
                "if connection is established.");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Refresh_Message_Box()
        {
            MessageDisplay.Invalidate();
            MessageDisplay.Update();
            MessageDisplay.Refresh();
            Application.DoEvents();
        }
    }
}
