

using System.Linq;

namespace SoftwareKingdom.Algorithms.Minmax.Tictactoe
{
    public class TicTacToeEvaluator : IMinMaxEvaluator
    {
        // Constants
        const int INCONSISTENT = -1;

        // Settings
        int side;

        public TicTacToeEvaluator(int side)
        {
            this.side = side;
        }

        public double Evaluate(IMinMaxState inState)
        {
            double result = 0;  
            TicTacToeState tttState = (TicTacToeState)inState;

            int winnerSide = tttState.GetBoardMatch();
            if(winnerSide != TicTacToeState.EMPTY)
            {
                result = EvaluateWinner(winnerSide);
            }

            return result;

        }

        public int EvaluateWinner(int winnerSide) // Check if the winnerside is its own side
        {
            int result = 0;
            if (winnerSide != side) result = - 1;
            if (winnerSide == side) result = 1;
            return result;
        }

        private int CheckSquareConsistency(int currentPossibleWinner, int pieceOnBoard)
        {
           if(pieceOnBoard == currentPossibleWinner)
           {
                return currentPossibleWinner;
           }
           else
           {
                return INCONSISTENT;
           }
        }

    }

  

}


