using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoftwareKingdom.Algorithms.Minmax.Tictactoe
{
    public class TicTacToeState : IMinMaxState
    {
        // Constants
        public const int EMPTY = 0; // Number to denote empty squares
        public const int X_VAL = 1; // Number to denote x squares
        public const int O_VAL = 2; // Number to denote o squares
                             // State variables
        public int[,] board; // Matrix represents board state
        int turn; // X_VAL or O_VAL
        string transitionID;
        public TicTacToeState(int size, int turn = X_VAL)
        {
            board = new int[size, size];
            this.turn = turn;
            transitionID = "root";
        }

        public IMinMaxState[] GetSuccessors()
        {
            List<IMinMaxState> successors = new List<IMinMaxState>();

            if(GetBoardMatch() == EMPTY)
            {
                for (int i = 0; i < board.GetLength(0); i++)
                {
                    for (int j = 0; j < board.GetLength(1); j++)
                    {
                        if (board[i, j] == EMPTY) // If there is an empty square, a successor will be created
                        {
                            TicTacToeState currentSuccessor = Copy();

                            currentSuccessor.board[i, j] = turn; // Put the piece to the board
                            currentSuccessor.SwitchTurn(); // reverse the turn
                            successors.Add(currentSuccessor); // Add to the successors list
                            currentSuccessor.SetTransitionID("" + turn + "_" + i + "_" + j); // Set the annotation of the move
                        }
                    }
                }
            }

           
            return successors.ToArray();
        }

        public string GetTransitionID()
        {
            return transitionID;
        }

        public void SetTransitionID(string transitionID)
        {
            this.transitionID = transitionID;
        }

        public bool Play(int piece, int i, int j)
        {
            bool canPlay = true;
            if(board[i, j] == EMPTY)
            {
                if(turn == piece)
                {
                    board[i, j] = piece;
                    SwitchTurn();
                }
                else
                {
                    canPlay = false;
                }
            }
            else
            {
                canPlay = false;
            }
            return canPlay;
        }

        public void SwitchTurn()
        {
            turn = (turn == X_VAL) ? O_VAL : X_VAL;
        }

        private TicTacToeState Copy()
        {
            TicTacToeState output = new TicTacToeState(board.GetLength(0));
            // Copy the board
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    output.board[i, j] = board[i, j];
                    output.turn = turn;
                    output.transitionID = transitionID;
                }
            }
            return output;
        }

        public int GetBoardMatch()
        {
            int result = EMPTY; // no match

            result = GetRowsMatch();
            if (result == EMPTY)
                result = GetColsMatch();
            if (result == EMPTY)
                result = GetDiags1Match();
            if (result == EMPTY)
                result = GetDiags2Match();

            return result;
        }

        int GetRowsMatch()
        {
            int result = EMPTY;
            int firstValue;
            bool hasMatch;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                firstValue = board[i, 0];
                if (firstValue == EMPTY) continue;
                hasMatch = true;
                for (int j = 1; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == firstValue)
                    {
                        continue;
                    }
                    else
                    {
                        hasMatch = false;
                        break;
                    }
                }

                if (hasMatch)
                {
                    result = firstValue;
                }
            }
            return result;
        }

        int GetColsMatch()
        {
            int result = EMPTY;
            int firstValue;
            bool hasMatch;
            for (int j = 0; j < board.GetLength(1); j++)
            {
                firstValue = board[0, j];
                if (firstValue == EMPTY) continue;
                hasMatch = true;
                for (int i = 1; i < board.GetLength(0); i++)
                {
                    if (board[i, j] == firstValue)
                    {
                        continue;
                    }
                    else
                    {
                        hasMatch = false;
                        break;
                    }
                }

                if (hasMatch)
                {
                    result = firstValue;
                }
            }
            return result;
        }

        int GetDiags1Match()
        {
            int result = EMPTY;
            int firstValue = board[0,0];
            bool hasMatch = true;
            for (int i = 0; i < board.GetLength(0); i++)
            {

                if (board[i,i] == firstValue)
                {
                    continue;
                }
                else
                {
                    hasMatch = false;
                    break;
                }
            }
            if (hasMatch)
            {
                result = firstValue;
            }
            return result;
        }

        int GetDiags2Match()
        {
            int result = EMPTY;
            int nRows = board.GetLength(0);
            int firstValue = board[nRows-1, 0];
            bool hasMatch = true;
            for (int i = 0; i < board.GetLength(0); i++)
            {

                if (board[nRows - 1-i, i] == firstValue)
                {
                    continue;
                }
                else
                {
                    hasMatch = false;
                    break;
                }
            }
            if (hasMatch)
            {
                result = firstValue;
            }
            return result;
        }

    }
}


