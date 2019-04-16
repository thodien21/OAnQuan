using System;
using System.Collections.Generic;
using System.Linq;

namespace OAnQuan.Business
{
    public class Board
    {
        /// <summary>
        /// list of squares in the board
        /// </summary>
        public List<Square> SquaresList { get; set; }

        /// <summary>
        /// list of players participate in the game
        /// </summary>
        public List<Player> PlayersList { get; set; }

        /// <summary>
        /// Number of player whose turn is on
        /// </summary>
        public int Turn { get; set; }
        public List<int> ClickedSquares { get; set; }

        public const int PlayerQty = 2;

        /// <summary>
        /// Etablish a new board
        /// </summary>
        public Board()
        {
            SquaresList = new List<Square>();
            ClickedSquares = new List<int>();
            PlayersList = new List<Player>();
            for (int i=0; i< PlayerQty; i++)
            {
                PlayersList.Add(new Player(""));
            }

            Turn = new Random().Next(1,3); //Board decides who plays first (1 or 2).

            for(int i=0; i<PlayerQty; i++)
            {
                SquaresList.Add(new BigSquare());
                for (int j = 0; j < 5; j++)
                {
                    SquaresList.Add(new SmallSquare());
                }
            }

            //Affect player number to each small square
            int n = 1;
            for(int k=1; k<= PlayerQty; k++)
            {
                for (int i = n; i < n + 5; i++)
                {
                    SquaresList[i].PlayerNumber = k;
                }
                n = n + 6;
            }
        }

        /// <summary>
        /// play turn
        /// </summary>
        /// <param name="player">player having turn</param>
        /// <param name="squareId">square identifier (1-5) </param>
        /// <param name="direction">direction to share the tokens</param>
        /// <returns></returns>
        public List<Square> Go(int playerNumber, int squareId, Direction direction)
        {
            var selectedSquare = SquaresList[squareId];
            List<Token> eatenTokens = new List<Token>();//the list of tokens which would be eaten
            Square eatenSquare = new Square();
            Square providerSquare = new Square();
            var tokenQty = SquaresList[squareId].Tokens.Count;

            //Check if the selected square is authorized and the qty of provider square is not null:
            //if (selectedSquare.PlayerNumber != playerNumber || tokenQty == 0)
            //{
            //    throw new ArgumentOutOfRangeException(nameof(squareId), "La case choisie doit être dans la rangée de {0} et non vide", PlayersList[playerNumber-1].Pseudo);
            //}

            //While the provider square is not empty and not be big square, it provide its tokens to next squares
            while (tokenQty != 0 && squareId != 0 && squareId != 6)
            {
                //save the tokens in a new object
                providerSquare = SquaresList[squareId];
                //the provider square is emptied by distributing the tokens for its followed squares.
                SquaresList[squareId].Tokens.Clear();

                for (int i = 0; i < tokenQty; i++)
                {
                    //move to the next square
                    squareId = (direction == Direction.RIGHT) ? (squareId + 1) % 12 : (squareId + 11) % 12;
                    //the next square has 1 token in plus
                    SquaresList[squareId].Tokens.Add(new SmallToken());
                }
                //move to the next square
                squareId = (direction == Direction.RIGHT) ? (squareId + 1) % 12 : (squareId + 11) % 12;
                //the quantity of tokens in the next square
                tokenQty = SquaresList[squareId].Tokens.Count;
            }

            //Some eaten tokens?
            while (tokenQty == 0)
            {
                //eat the next square
                squareId = (direction == Direction.RIGHT) ? (squareId + 1) % 12 : (squareId + 11) % 12;
                eatenTokens = SquaresList[squareId].Eaten();
                if(eatenTokens.Count != 0)
                {
                    PlayersList[playerNumber - 1].Pool.AddRange(eatenTokens);//add eaten tokens in player's pool
                }
                else
                {
                    break; //nothing to eat in this case
                }

                //move to the next square to see if it is also empty
                squareId = (direction == Direction.RIGHT) ? (squareId + 1) % 12 : (squareId + 11) % 12;
                //calculate the quantity of tokens in the next square to see if it is empty
                tokenQty = SquaresList[squareId].Tokens.Count;
            }

            //Change turn
            Turn = (Turn == 1) ? 2 : 1;
            return SquaresList;
        }

        /// <summary>
        /// Finalize the results (score, pool of each player; player 1 win/lose/draw)
        /// </summary>
        public Result GetResult()
        {
            if (SquaresList[0].Tokens.Count == 0 && SquaresList[6].Tokens.Count == 0)//Game finishes
            {
                //Add all the tokens on player's side to his pool
                int n = 1;
                for (int k = 1; k <= PlayerQty; k++)
                {
                    for (int i = n; i < n + 5; i++)
                    {
                        PlayersList[k - 1].Pool.AddRange(SquaresList[i].Tokens);
                    }
                    n = n + 6;
                    PlayersList[k - 1].GetScore();//get final score of each player
                }
            }
            if (PlayersList[0].Score > PlayersList[1].Score)
            {
                return Result.WIN;
            }
            else if (PlayersList[0].Score == PlayersList[1].Score)
            {
                return Result.DRAW;
            }
            return Result.LOSE;
        }
    }
}    