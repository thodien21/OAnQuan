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
        public void Play(Player player1, Player player2)
        {
            int squareId = 
            board.Go(player1, squareId, direction);
        }
    }
}
