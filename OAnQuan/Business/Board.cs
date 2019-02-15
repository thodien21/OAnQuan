using OAnQuan.Business;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OAnQuan.Business
{
    public class Board
    {
        public List<Square> SquaresList { get; set; }
        public List<Player> PlayersList { get; set; }

        public Board()
        {
            SquaresList = new List<Square>();
            PlayersList = new List<Player>() { new Player(""), new Player("") };

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
                    SquaresList[i].Player = PlayersList[k];//Affect player
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
        public void Go(Player player, int squareId, Direction direction)
        {
            var selectedSquare = SquaresList[squareId];
            List<Token> eatenTokens = new List<Token>();
            Square eatenSquare = new Square();
            Square providerSquare = new Square();
            var tokenQty = SquaresList[squareId].Tokens.Count;

            //Check if the selected square is authorized and the qty of provider square is not null:
            if (selectedSquare.Player != player || tokenQty == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(squareId), "The selected square should correspond to {0} and not be empty", player.Pseudo);
            }

            //While the provider square is not empty, it provide its tokens to next squares
            while (tokenQty != 0)
            {
                providerSquare = SquaresList[squareId];//save the tokens in a new object
                SquaresList[squareId].Tokens.Clear();//the provider square is emptied by distributing the tokens for its followed squares.

                for (int i = 0; i < tokenQty; i++)
                {
                    squareId = (direction == Direction.RIGHT) ? (squareId + 1) % 12 : (squareId + 11) % 12;
                    SquaresList[squareId].Tokens.Add(new SmallToken());//the next square has 1 token in plus
                }
                squareId = (direction == Direction.RIGHT) ? (squareId + 1) % 12 : (squareId + 11) % 12;//next square
                tokenQty = SquaresList[squareId].Tokens.Count;//the quantity of tokens in the next square
            }

            //Some eaten tokens?
            while (tokenQty == 0 )
            {
                squareId = (direction == Direction.RIGHT) ? (squareId + 1) % 12 : (squareId + 11) % 12;//next square
                eatenTokens = SquaresList[squareId].Eaten();
                foreach(var item in eatenTokens)
                {
                    player.Pool.Add(item);
                }
                squareId = (direction == Direction.RIGHT) ? (squareId + 1) % 12 : (squareId + 11) % 12;//next square
                tokenQty = SquaresList[squareId].Tokens.Count;//the quantity of tokens in the next square
            }
        }
    }
}    