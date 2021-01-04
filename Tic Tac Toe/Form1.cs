using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tic_Tac_Toe
{
    public partial class Form1 : Form
    {
        TicTacToeBoard mainBoard;

        public Form1()
        {
            InitializeComponent();
            mainBoard = null;
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
            pictureBox4.Visible = false;
            pictureBox5.Visible = false;
            pictureBox6.Visible = false;
            pictureBox7.Visible = false;
            pictureBox8.Visible = false;
            pictureBox9.Visible = false;
            groupBox2.Visible = false;
            button1.Enabled = false;
            button2.Visible = false;

        }

        private void DrawBoard()
        {
            string[,] board = mainBoard.GetUpdatedBoard(-1, -1); // запрашиваем состояние клеток, не изменяя его
            SetImage(pictureBox1, board[0, 0]);
            SetImage(pictureBox2, board[0, 1]);
            SetImage(pictureBox3, board[0, 2]);
            SetImage(pictureBox4, board[1, 0]);
            SetImage(pictureBox5, board[1, 1]);
            SetImage(pictureBox6, board[1, 2]);
            SetImage(pictureBox7, board[2, 0]);
            SetImage(pictureBox8, board[2, 1]);
            SetImage(pictureBox9, board[2, 2]);

        }

        private void SetImage(PictureBox pictureBox, string value)
        {
            if (value == "")
                pictureBox.Image = Tic_Tac_Toe.Properties.Resources.blank;
            if (value == "X")
                pictureBox.Image = Tic_Tac_Toe.Properties.Resources.X;
            if (value == "0")
                pictureBox.Image = Tic_Tac_Toe.Properties.Resources._0;
        }

        private void UpdateBoard(int row, int col)
        {
             if (mainBoard.IsMoveAllowed(row, col))
            {
                string[,] board = mainBoard.GetUpdatedBoard(row, col);
                DrawBoard();
                if (mainBoard.CheckGameEnd())
                {
                    MessageBox.Show("Игра завершена\n" + mainBoard.GetWinner());
                    mainBoard = null;
                                    
                }
            }
            else
            {
                MessageBox.Show("Недопустимый ход");
            }
        }

        private int CheckMode()
        {
            int mode = 0;
            if (radioButton1.Checked) mode = 1; // игрок против компьютера
            if (radioButton2.Checked) mode = 2; // игрок против игрока
            if (radioButton3.Checked) mode = 3;// компьютер против компьютера
            return mode;
        }

        private int CheckWhoStarts()
        {
            int first = 0;
            if (radioButton4.Checked) first = 1; // начинает пользователь
            if (radioButton5.Checked) first = 2; // начинает компьютер
            return first;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            UpdateBoard(0, 0);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            UpdateBoard(0, 1);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            UpdateBoard(0, 2);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            UpdateBoard(1, 0);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            UpdateBoard(1, 1);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            UpdateBoard(1, 2);
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            UpdateBoard(2, 0);
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            UpdateBoard(2, 1);
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            UpdateBoard(2, 2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainBoard = new TicTacToeBoard();
            mainBoard.Mode = CheckMode();
            mainBoard.FirstMove = CheckWhoStarts();
            DrawBoard();
            pictureBox1.Visible = true;
            pictureBox2.Visible = true;
            pictureBox3.Visible = true;
            pictureBox4.Visible = true;
            pictureBox5.Visible = true;
            pictureBox6.Visible = true;
            pictureBox7.Visible = true;
            pictureBox8.Visible = true; 
            pictureBox9.Visible = true;
            if (mainBoard.FirstMove == 2)
            {
                string[,] board = mainBoard.FirstComputerMove();
                DrawBoard();
            }
            if (mainBoard.Mode == 3)
            {
                button2.Enabled = true;
                mainBoard.ComputerVsComputer();
                DrawBoard();
            }

        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
            button1.Enabled = false;
            button2.Visible = false;

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            button1.Enabled = true;
            button2.Visible = false;

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            button1.Enabled = true;
            button2.Visible = true;
            button2.Enabled = false;

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainBoard.ComputerVsComputer();

            DrawBoard();
            if (mainBoard.CheckGameEnd())
            {
                MessageBox.Show("Игра завершена\n" + mainBoard.GetWinner());
                mainBoard = null;

            }

        }
    }
}
