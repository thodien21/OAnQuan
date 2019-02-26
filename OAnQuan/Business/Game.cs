using System;
using System.Collections.Generic;
using System.Text;

namespace OAnQuan.Business
{
    /// <summary>
    /// O An Quan game
    /// </summary>
    public class Game
    {
        Board board = new Board();
        public Player Player1 { get => board.PlayersList[0]; }
        public Player Player2 { get => board.PlayersList[1]; }

        /// <summary>
        /// constructor with 2 parameters: the player and second player
        /// </summary>
        /// <param name="pseudo1"></param>
        /// <param name="pseudo2"></param>
        public Game(string pseudo1, string pseudo2)
        {
            board.PlayersList[0].Pseudo = pseudo1;
            board.PlayersList[1].Pseudo = pseudo2;
        }

        /// <summary>
        /// The player plays his turn
        /// </summary>
        /// <param name="pseudo"></param>
        /// <param name="squareId"></param>
        /// <param name="direction"></param>
        public void Play()
        {
            while(board.SquaresList[0].Tokens.Count != 0 && board.SquaresList[6].Tokens.Count != 0)
            {
                //board.Go(Player1, squareId, direction);
            }
        }
    }
}
