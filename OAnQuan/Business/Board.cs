﻿using System;
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
        /// Play his turn
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

            //Share the tokens until the next quare is empty, or big square
            while (tokenQty != 0 && squareId != 0 && squareId != 6)
            {
                squareId = SmallStep(playerNumber, squareId, direction);
                //token quantity of next square
                tokenQty = SquaresList[squareId].Tokens.Count;
            }

            int nextsquareId = (direction == Direction.RIGHT) ? (squareId + 1) % 12 : (squareId + 11) % 12;
            //Some eaten tokens?
            while (tokenQty == 0 && SquaresList[nextsquareId].Tokens.Count != 0)
            {
                //calculate the quantity of tokens in the next square to see if it is empty
                tokenQty = PutEatenTokensInPool(playerNumber, squareId, direction);
            }

            //Change turn
            Turn = (Turn == 1) ? 2 : 1;
            return SquaresList;
        }

        /// <summary>
        /// Put the tokens of its nextSquare into the player's pool
        /// </summary>
        /// <param name="playerNumber"></param>
        /// <param name="squareId"></param>
        /// <param name="direction"></param>
        /// <returns>quantity of tokens of next square to see if it's empty</returns>
        public int PutEatenTokensInPool(int playerNumber, int squareId, Direction direction)
        {
            //eat the tokens in next square
            int nextSquareId = (direction == Direction.RIGHT) ? (squareId + 1) % 12 : (squareId + 11) % 12;
            List<Token> eatenTokens = SquaresList[nextSquareId].Eaten();

            //add eaten tokens in player's pool
            PlayersList[playerNumber - 1].Pool.AddRange(eatenTokens);

            //move to the next square to see if it is also empty
            return squareId = (direction == Direction.RIGHT) ? (nextSquareId + 1) % 12 : (nextSquareId + 11) % 12;
            ////calculate the quantity of tokens in the next square to see if it is empty
            //return SquaresList[squareId].Tokens.Count;
        }

        /// <summary>
        /// Share the tokens until the next quare is empty, or big square
        /// </summary>
        /// <param name="playerNumber"></param>
        /// <param name="squareId">Id of the square which shares tokens</param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public int SmallStep(int playerNumber, int squareId, Direction direction)
        {
            var tokenQty = SquaresList[squareId].Tokens.Count;
            Square providerSquare = new Square();

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
            //squareId = GetIdOfNextSquare(squareId, direction);
            squareId = (direction == Direction.RIGHT) ? (squareId + 1) % 12 : (squareId + 11) % 12;
            
            return squareId;//Id of next square for next small step
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