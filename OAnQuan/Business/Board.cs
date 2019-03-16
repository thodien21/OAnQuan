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

        /// <summary>
        /// Etablish a new board
        /// </summary>
        public Board()
        {
            SquaresList = new List<Square>();
            PlayersList = new List<Player>() { new Player(""), new Player("") };
            Turn = new Random().Next(1,2); //Board decides who plays first.

            for(int i=0; i<PlayersList.Count; i++)
            {
                SquaresList.Add(new BigSquare());
                for (int j = 0; j < 5; j++)
                {
                    SquaresList.Add(new SmallSquare());
                }
            }

            //Affect player to each small square
            int n = 1;
            for(int k=0; k< PlayersList.Count; k++)
            {
                for (int i = n; i < n + 5; i++)
                {
                    //Affect player to the square
                    SquaresList[i].Player = PlayersList[k];
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
        public List<Square> Go(Player player, int squareId, Direction direction)
        {
            var selectedSquare = SquaresList[squareId];
            List<Token> eatenTokens = new List<Token>();//the list of tokens which would be eaten
            Square eatenSquare = new Square();
            Square providerSquare = new Square();
            var tokenQty = SquaresList[squareId].Tokens.Count;

            //Check if the selected square is authorized and the qty of provider square is not null:
            if (selectedSquare.Player != player || tokenQty == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(squareId), "The selected square should correspond to {0} and not be empty", player.Pseudo);
            }

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
                //move to the next square
                squareId = (direction == Direction.RIGHT) ? (squareId + 1) % 12 : (squareId + 11) % 12;
                eatenTokens = SquaresList[squareId].Eaten();
                if(eatenTokens.Count != 0)
                {
                    foreach (var item in eatenTokens)
                    {
                        player.Pool.Add(item);
                    }
                }
                else
                {
                    break;
                }
                //move to the next square
                squareId = (direction == Direction.RIGHT) ? (squareId + 1) % 12 : (squareId + 11) % 12;
                //the quantity of tokens in the next square
                tokenQty = SquaresList[squareId].Tokens.Count;
            }

            if(SquaresList[0].Tokens.Count == 0 && SquaresList[6].Tokens.Count == 0)
            {
                GetResult();
            }

            return SquaresList;
        }

        public Result GetResult()
        {
            //PlayersList[0].GamesNb++;
            if (PlayersList[0].Score > PlayersList[1].Score)
            {
                //PlayersList[0].WinNb++;
                return Result.WIN;
            }
            else if (PlayersList[0].Score == PlayersList[1].Score)
            {
                //PlayersList[0].DrawNb++;
                return Result.DRAW;
            }
            //PlayersList[0].LoseNb++;
            return Result.LOSE;
        }
    }
}    