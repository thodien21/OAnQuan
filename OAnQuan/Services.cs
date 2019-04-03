﻿using OAnQuan.Business;
using OAnQuan.DataAccess;
using System.Collections.Generic;

namespace OAnQuan
{
    public static class Services
    {
        /// <summary>
        /// Player who login/sign up
        /// </summary>
        public static Player Player { get; set; }
        public static List<Player> PlayerListWithRanking => PlayerDb.GetRanking(PlayerDb.CountPlayer());
        public static int PlayerQty => PlayerListWithRanking.Count;
        public static Player PlayerChosenForInfo { get; set; }

        /// <summary>
        /// Get own player from login/sign up
        /// </summary>
        /// <param name="pseudo"></param>
        /// <param name="pass"></param>
        public static void GetPlayer(string pseudo, string pass)
        {
            Player = PlayerDb.GetPlayer(pseudo, pass);
        }

        /// <summary>
        /// Get own player from pseudo
        /// </summary>
        /// <param name="pseudo"></param>
        /// <param name="pass"></param>
        public static void GetPlayer(string pseudo)
        {
            PlayerChosenForInfo = PlayerDb.GetPlayerFromPseudo(pseudo);
        }
    }
}