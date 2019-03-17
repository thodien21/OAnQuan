using System.Collections.Generic;
using System.Linq;

namespace OAnQuan.Business
{
    /// <summary>
    /// Player.
    /// </summary>
    public class Player
    {
        private string fullName;
        private int winGameQty;
        private int loseGameQty;
        private int drawGameQty;

        /// <summary>
        /// Pseudo.
        /// </summary>
        public string Pseudo { get; set; }

        /// <summary>
        /// Pool of tokens actually earned in the game
        /// </summary>
        public List<Token> Pool { get; set; }

        /// <summary>
        /// score of player in the game
        /// </summary>
        public int Score { get; set; }

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

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="pseudo"></param>
        /// <param name="winGameQty"></param>
        /// <param name="loseGameQty"></param>
        /// <param name="drawGameQty"></param>
        public Player(string pseudo, int winGameQty, int loseGameQty, int drawGameQty) : this(pseudo)
        {
            this.winGameQty = winGameQty;
            this.loseGameQty = loseGameQty;
            this.drawGameQty = drawGameQty;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pseudo"></param>
        /// <param name="fullName"></param>
        public Player(string pseudo, string fullName) : this(pseudo)
        {
            this.fullName = fullName;
        }

        /// <summary>
        /// Calculate the score from pool
        /// </summary>
        /// <returns></returns>
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
