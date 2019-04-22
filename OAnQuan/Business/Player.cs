using OAnQuan.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OAnQuan.Business
{
    /// <summary>
    /// Player.
    /// </summary>
    public class Player
    {
        #region properties
        public string FullName { get; set; }

        public string Pseudo { get; set; }
        public long PlayerId { get; internal set; }
        public string Password { get; internal set; }

        public long IsAdmin { get; set; }
        public string IsAdminString => (IsAdmin == 1) ? "Oui" : "Non";

        public long IsEnabled { get; set; }
        public string IsEnabledString => (IsEnabled == 1) ? "Oui" : "Non";

        public long WinGameQty { get; set; }
        public long DrawGameQty { get; set; }
        public long LoseGameQty { get; set; }
        public long TotalGameQty => WinGameQty + DrawGameQty + LoseGameQty;
        public long Ranking => GetOwnRanking();

        /// <summary>
        /// Pool of tokens actually earned in the game
        /// </summary>
        public List<Token> Pool { get; set; }

        /// <summary>
        /// actual score of player in the game
        /// </summary>
        public int Score => Pool.Sum(p => p.Value);
        #endregion

        #region constructors
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

        public Player()
        {
        }
        #endregion

        #region functionalities for player
        //// <summary>
        /// Get ranking of own player
        /// </summary>
        /// <returns>int</returns>
        public int GetOwnRanking()
        {
            var ownRanking = 0;
            var count = PlayerDb.CountPlayer();
            var playerListRanking = PlayerDb.GetRanking(count);
            for (int i = 0; i < count; i++)
            {
                if (playerListRanking[i].Pseudo == Pseudo)
                {
                    ownRanking = i+1;
                    break;
                }
            }
            return ownRanking;
        }

        public List<String> GetGroupedInfo()
        {
            var listInfo= new List<string>();
            listInfo.Add(Pseudo);
            listInfo.Add(FullName);
            return listInfo;
        }

        /// <summary>
        /// If it's the first time player saves the game -> Save board, if not the first time-> Update board
        /// </summary>
        /// <param name="turn">turn of which player?</param>
        /// <param name="player2Pseudo">pseudo of player number 2</param>
        /// <param name="playerId">player identity</param>
        public void SaveOrUpdateGame(long playerId, string player2Pseudo, long turn, Board board)
        {
            bool contains = BoardDb.CheckIfBoardDbContainsPlayerId(playerId);
            if (contains)
            {
                BoardDb.Update(playerId, player2Pseudo, turn);
                SquareListDb.Update(playerId, board);
                PoolDb.Update(playerId, board);
            }
            else
            {
                BoardDb.Save(playerId, player2Pseudo, turn);
                SquareListDb.Save(playerId, board);
                PoolDb.Save(playerId, board);
            }
        }

        /// <summary>
        /// Update result in database
        /// </summary>
        /// <param name="board"></param>
        /// <param name="playerId"></param>
        public void UpdateResult(Board board)
        {
            var result = board.GetResult();
            switch (result)
            {
                case Result.WIN:
                    this.WinGameQty++;
                    break;
                case Result.DRAW:
                    this.DrawGameQty++;
                    break;
                case Result.LOSE:
                    this.LoseGameQty++;
                    break;
                default: break;
            }
        }

        public List<Token> EatTokensInSquare(Square square)
        {
            List<Token> earnedTokens = new List<Token>();
            earnedTokens.AddRange(square.Tokens);

            square.Tokens.Clear();//Remove tokens in eaten square
            Pool.AddRange(earnedTokens);//Add eaten tokens in player's pool

            return earnedTokens;
        }
        #endregion

        #region functionalities for Admin
        private void GetAllPlayer()
        {
            if(IsAdmin == 1)
            {
                PlayerDb.GetAllPlayer();
            }
            else throw new ArgumentOutOfRangeException(nameof(PlayerId), "Cette fonctionnalité n'est réservée qu'au administrateur");
        }

        private void UpgradePlayerToAdmin(long playerId)
        {
            if (IsAdmin == 1)
            {
                PlayerDb.UpgradePlayerToAdmin(playerId);
            }
            else throw new ArgumentOutOfRangeException(nameof(PlayerId), "Cette fonctionnalité n'est réservée qu'au administrateur");
        }

        private void DeactivatePlayer(long playerId)
        {
            if (IsAdmin == 1)
            {
                PlayerDb.DeactivatePlayer(playerId);
            }
            else throw new ArgumentOutOfRangeException(nameof(PlayerId), "Cette fonctionnalité n'est réservée qu'au administrateur");
        }

        private void ReactivatePlayer(long playerId)
        {
            if (IsAdmin == 1)
            {
                PlayerDb.ReactivatePlayer(playerId);
            }
            else throw new ArgumentOutOfRangeException(nameof(PlayerId), "Cette fonctionnalité n'est réservée qu'au administrateur");
        }
        #endregion
    }
}
