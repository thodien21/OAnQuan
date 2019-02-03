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
        /// Constructor for Mock tests.
        /// </summary>
        public Player()
        { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="pseudo">Pseudo.</param>
        /// <param name="password">Password.</param>
        public Player(string pseudo, string password)
        {
            Pseudo = pseudo;
            Password = password;
        }
    }
}
