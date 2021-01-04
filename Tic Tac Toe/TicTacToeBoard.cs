using System;
using System.Collections.Generic;
using System.Text;

namespace Tic_Tac_Toe
{
    public class TicTacToeBoard
    {
        private string[,] board;
        private bool player1Move;
        private string winner;


        public int Mode { get; set; }
        public int FirstMove { get; set; } //кто начинает - компьютер или пользователь

        public TicTacToeBoard()
        {
            board = new string[3, 3];
            //Mode = 0;
            InitializeBoard();

        }

        private void InitializeBoard()
        {
            player1Move = true;
            winner = "Ничья";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = "";
                }
            }
        }

        public string[,] FirstComputerMove()
        {
            board[1, 1] = "X";
            player1Move = !player1Move;
            return board;
        }

        private RowCol ConvertToField(int field)
        {
            RowCol rc = new RowCol();

            if (field == 1)
            {
                rc.Row = 0;
                rc.Col = 0;
            }
            if (field == 2)
            {
                rc.Row = 0;
                rc.Col = 1;
            }
            if (field == 3)
            {
                rc.Row = 0;
                rc.Col = 2;
            }
            if (field == 4)
            {
                rc.Row = 1;
                rc.Col = 0;
            }
            if (field == 5)
            {
                rc.Row = 1;
                rc.Col = 1;
            }
            if (field == 6)
            {
                rc.Row = 1;
                rc.Col = 2;
            }
            if (field == 7)
            {
                rc.Row = 2;
                rc.Col = 0;
            }
            if (field == 8)
            {
                rc.Row = 2;
                rc.Col = 1;
            }
            if (field == 9)
            {
                rc.Row = 2;
                rc.Col = 2;
            }
            return rc;
        }

        
        public void ComputerVsComputer()
        {
            
            Random rand = new Random();
            int field = rand.Next(1, 10);
            RowCol rc = ConvertToField(field);

            while (board[rc.Row, rc.Col] != "" )
            {
                field = rand.Next(1, 10);
                rc = ConvertToField(field);
            }
            
            if (!player1Move)
            {
                board[rc.Row, rc.Col] = "0";
                player1Move = !player1Move;
                return;
            }
            if (player1Move)
            {
                board[rc.Row, rc.Col] = "X";
                player1Move = !player1Move;
                return;
            }
        }

        public string[,] GetUpdatedBoard(int row, int col)
        {
            if (row < 0 && col < 0 ) return board;
            switch (Mode)
            {
                case 1: // игрок против компьютера
                    
                        if (FirstMove == 1) // начинает пользователь
                        {
                            if (player1Move) board[row, col] = "X";
                            else board[row, col] = "0";
                            player1Move = !player1Move;
                            if (!player1Move)  //ход компьютера
                            {
                                RowCol rc = GetComputerMove("X"); //если начинает пользователь, то комп ходит нулями
                                GetUpdatedBoard(rc.Row, rc.Col);
                            }
                        }
                        if (FirstMove == 2) //начинает комп
                        {
                            if (!player1Move) board[row, col] = "0";
                            else board[row, col] = "X";
                            player1Move = !player1Move;
                            if (player1Move)
                            {
                                RowCol rc = GetComputerMove("0"); //если начинает пользователь, то комп ходит нулями
                                GetUpdatedBoard(rc.Row, rc.Col);
                            }
                        }
                break;
                
                case 2: // игрок против игрока
                    {
                        if (player1Move)
                        {
                            board[row, col] = "X";
                        }
                        else
                        {
                            board[row, col] = "0";
                        }//изменение клетки
                        player1Move = !player1Move;
                        break;
                    }
            }

            return board;
        }

        private RowCol GetComputerMove(string sign)
        {
            RowCol rc = new RowCol();

            // проверка можно ли выиграть
            string s;
            if (sign == "X") s = "0";
            else s = "X";

            rc = WinPossible(s);
            if (rc.Row >= 0 && rc.Col >= 0) return rc;

            // защита

            rc = WinPossible(sign);
            if (rc.Row >= 0 && rc.Col >= 0) return rc;

            //нападение
            if (board[1, 1] == "")  //проверка центра
            {
                rc.Row = 1;
                rc.Col = 1;
                return rc;
            }

            if (board [0, 0] == "")
            {
                rc.Row = 0;
                rc.Col = 0;
                return rc;
            }
            if (board[0, 2] == "")
            {
                rc.Row = 0;
                rc.Col = 2;
                return rc;
            }
            if (board[2, 0] == "")
            {
                rc.Row = 2;
                rc.Col = 0;
                return rc;
            }
            if (board[2, 2] == "")
            {
                rc.Row = 2;
                rc.Col = 2;
                return rc;
            }

            if (board[0, 1] == "")
            {
                rc.Row = 0;
                rc.Col = 1;
                return rc;
            }
            if (board[1, 0] == "")
            {
                rc.Row = 1;
                rc.Col = 0;
                return rc;
            }
            if (board[1, 2] == "")
            {
                rc.Row = 1;
                rc.Col = 2;
                return rc;
            }
            if (board[2, 1] == "")
            {
                rc.Row = 2;
                rc.Col = 1;
                return rc;
            }

            return rc;
        }

        private RowCol WinPossible (string sign) // sign - за кого играет выигрывающий
        {
            RowCol rc = new RowCol();
            if (board[0, 0] == "" && ((board[0, 1] == sign && board[0, 2] == sign) || (board[1, 0] == sign && board[2, 0] == sign) || (board[1, 1] == sign && board[2, 2] == sign)))
            {
                rc.Row = 0;
                rc.Col = 0;
                return rc;
            }
            if (board[0, 2] == "" && ((board[0, 0] == sign && board[0, 1] == sign) || (board[1, 1] == sign && board[2, 0] == sign) || (board[1, 2] == sign && board[2, 2] == sign)))
            {
                rc.Row = 0;
                rc.Col = 2;
                return rc;
            }
            if (board[2, 0] == "" && ((board[0, 0] == sign && board[1, 0] == sign) || (board[1, 1] == sign && board[0, 2] == sign) || (board[2, 1] == sign && board[2, 2] == sign)))
            {
                rc.Row = 2;
                rc.Col = 0;
                return rc;
            }
            if (board[2, 2] == "" && ((board[0, 0] == sign && board[1, 1] == sign) || (board[2, 0] == sign && board[2, 1] == sign) || (board[0, 2] == sign && board[1, 2] == sign)))
            {
                rc.Row = 2;
                rc.Col = 2;
                return rc;
            }

            if (board[0, 1] == "" && ((board[1, 1] == sign && board[2, 1] == sign) || (board[0, 0] == sign && board[0, 2] == sign)))
            {
                rc.Row = 0;
                rc.Col = 1;
                return rc;
            }
            if (board[1, 0] == "" && ((board[1, 1] == sign && board[1, 2] == sign) || (board[0, 0] == sign && board[2, 0] == sign)))
            {
                rc.Row = 1;
                rc.Col = 0;
                return rc;
            }
            if (board[1, 2] == "" && ((board[1, 0] == sign && board[1, 1] == sign) || (board[0, 2] == sign && board[2, 2] == sign)))
            {
                rc.Row = 1;
                rc.Col = 2;
                return rc;
            }
            if (board[2, 1] == "" && ((board[0, 1] == sign && board[1, 1] == sign) || (board[2, 0] == sign && board[2, 2] == sign)))
            {
                rc.Row = 2;
                rc.Col = 1;
                return rc;
            }
            rc.Row = -1;
            rc.Col = -1;
            return rc;

        }

        public bool IsMoveAllowed(int row, int col)
        {
            if (board[row, col] != "") return false;
            else return true;
        }

        public bool CheckGameEnd()
        {
            if (board[0, 0] != "" && board[0, 0] == board[0, 1] && board[0, 1] == board[0, 2])
            {
                SetWinner();
                return true;
            }
            if (board[1, 0] != "" && board[1, 0] == board[1, 1] && board[1, 1] == board[1, 2])
            {
                SetWinner();
                return true;
            }
            if (board[2, 0] != "" && board[2, 0] == board[2, 1] && board[2, 1] == board[2, 2])
            {
                SetWinner();
                return true;
            }
            if (board[0, 0] != "" && board[0, 0] == board[1, 0] && board[1, 0] == board[2, 0])
            {
                SetWinner();
                return true;
            }
            if (board[0, 1] != "" && board[0, 1] == board[1, 1] && board[1, 1] == board[2, 1])
            {
                SetWinner();
                return true;
            }
            if (board[0, 2] != "" && board[0, 2] == board[1, 2] && board[1, 2] == board[2, 2])
            {
                SetWinner();
                return true;
            }
            if (board[0, 0] != "" && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
            {
                SetWinner();
                return true;
            }
            if (board[0, 2] != "" && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
            {
                SetWinner();
                return true;
            }


            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == "") return false;  //проверка есть ли свободные клетки
                }
            }
            return true;
        }

        public void SetWinner()
        {
            if (player1Move) winner = "Нолики победили";
            else winner = "Крестики победили";
        }

        public string GetWinner()
        {
            return winner;
        }
    }
}
