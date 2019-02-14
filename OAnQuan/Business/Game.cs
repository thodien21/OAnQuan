using System;
using System.Collections.Generic;
using System.Text;

namespace OAnQuan.Business
{
    /// <summary>
    /// O An Quan game
    /// </summary>
    class Game
    {
        Board Board { get; set; }
        
        /// <summary>
        /// The player plays his turn
        /// </summary>
        /// <param name="pseudo"></param>
        /// <param name="squareId"></param>
        /// <param name="direction"></param>
        public void Play(Player player, int squareId, Direction direction)
        {
            Board.Go(player, squareId, direction);
        }
    }
}
