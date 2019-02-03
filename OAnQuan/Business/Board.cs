using System;
using System.Collections.Generic;

namespace OAnQuan.Business
{
    public class Board
    {
        public List<int> Squares { get; set;}
        
        public Board()
        { }

        public Board(List<int> squares)
        {
            Squares = squares;
        }

        /// <summary>
        /// Initialize the board
        /// </summary>
        /// <returns></returns>
        public List<int> CreatNewBoard()
        {
            List<int> newSquares = new List<int>();
            for (int i=0; i<12; i++)
            {
                newSquares.Add(5);
            }
            Squares = newSquares;
            return Squares;
        }
        
        /// <summary>
        /// Play turn
        /// </summary>
        /// <param name="squareId">square identifier</param>
        /// <param name="direction">direction to share the tokens</param>
        /// <returns></returns>
        public int Go(int squareId, Direction direction)
        {
            int tokenQty; //quantity of token in a square
            int gainedTokens = 0; //quantity of tokens gained thanks to turn
            List<int> squaresAfterTurn = Squares;//Take the status of squares before action
            
            //Check if the selected squareId and direction are both authorized and the qty of square is not null:
            while (squareId == 0 || squareId == 6 || squaresAfterTurn[squareId] == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(squareId), "It should not be the big square or empty");
            }

            //The tokens are shared in the selected direction until the next square -after finishing token- is also empty, or is the big square. 
            while (squareId != 0 && squareId != 6 && squaresAfterTurn[squareId] != 0)
            {
                tokenQty = squaresAfterTurn[squareId];//quantity of tokens in the selected square
                squaresAfterTurn[squareId] = 0;//the selected square is emptied to distribute the tokens for its followed squares.
                switch (direction)
                {
                    //dectect direction
                    case Direction.RIGHT:
                        for (int i = 1; i <= tokenQty; i++)
                        {
                            squareId = (squareId + 1) % 12;
                            squaresAfterTurn[squareId]++;
                        }
                        squareId = squareId + 1;
                        break;
                    case Direction.LEFT:
                        for (int i = tokenQty; i >= 1; i--)
                        {
                            squareId = (squareId + 11) % 12;
                            squaresAfterTurn[squareId]++;
                        }
                        squareId = (squareId + 11) % 12;
                        break;
                    //default: exception
                }            
            }

            /*Detect the next squares:
             - squareId = 0 or 6: turn finishes
             - empty: eat the followed squares alternatively*/
            switch(squareId)
            {
                case 0:
                case 6:
                    break;
            }
            while (squaresAfterTurn[squareId] == 0)
            {
                gainedTokens = gainedTokens + squaresAfterTurn[squareId + 1];
                squaresAfterTurn[squareId + 1] = 0;
                squareId = squareId + 2;
            }
            Squares = squaresAfterTurn;//Update status for squares
            return gainedTokens;
        }
    }
}          