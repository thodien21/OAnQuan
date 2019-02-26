using System.Collections.Generic;
using System.Linq;

namespace OAnQuan.Business
{
    /// <summary>
    /// Player.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Pseudo.
        /// </summary>
        public string Pseudo { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Pool of tokens actually earned in the game
        /// </summary>
        public List<Token> Pool { get; set; }

        /// <summary>
        /// score of player in the game
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// number of games that player win
        /// </summary>
        public int WinNb { get; set; }

        /// <summary>
        /// number of draw games
        /// </summary>
        public int DrawNb { get; set; }

        /// <summary>
        /// number of lose game
        /// </summary>
        public int LoseNb { get; set; }

        /// <summary>
        /// total number of played games
        /// </summary>
        public int GamesNb { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="pseudo">Pseudo.</param>
        /// <param name="password">Password.</param>
        public Player(string pseudo)
        {
            Pseudo = pseudo;
            Pool = new List<Token>();            
        }
        public int GetScore()
        {
            var smallToken = new SmallToken();
            var bigToken = new BigToken();
            var nbSmallToken = 0;
            var nbBigToken = 0;
            foreach (var item in Pool)
            {
                if (item.GetType() == smallToken.GetType())
                {
                    nbSmallToken++;
                }
                else nbBigToken++;
            }
            return Score = nbSmallToken * smallToken.Value + nbBigToken * bigToken.Value;
        }
    }
}
