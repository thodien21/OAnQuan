using System.Collections.Generic;
using System.Linq;

namespace OAnQuan.Business
{
    /// <summary>
    /// Player.
    /// </summary>
    public class Player
    {
        public string FullName { get; set; }
        public long WinGameQty { get; set; }
        public long DrawGameQty { get; set; }
        public long LoseGameQty { get; set; }


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
        public Player(string pseudo, long winGameQty, long loseGameQty, long drawGameQty) : this(pseudo)
        {
            WinGameQty = winGameQty;
            LoseGameQty = loseGameQty;
            DrawGameQty = drawGameQty;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pseudo"></param>
        /// <param name="fullName"></param>
        public Player(string pseudo, string fullName) : this(pseudo)
        {
            FullName = fullName;
        }
       

        /// <summary>
        /// Calculate the earned score from player's pool after turn
        /// </summary>
        /// <returns></returns>
        public int GetScore ()
        {
            var nbBigToken = Square.GetBigTokenQtyFromList(Pool);
            var nbSmallToken = Pool.Count - nbBigToken;
            var smallToken = new SmallToken();
            var bigToken = new BigToken();
            return Score = nbSmallToken * smallToken.Value + nbBigToken * bigToken.Value;
        }
    }
}
