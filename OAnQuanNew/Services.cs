using OAnQuan.Business;
using OAnQuan.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;

namespace OAnQuan
{
    public static class Services
    {
        // We use the data source:
        //for laptop 
        //const string connString = "Data Source= C:/Users/ttran/Documents/Visual Studio 2017/Projects/OAnQuan/OAnQuan/DataAccess/DatabaseOAQ.db;Version=3;New=True;Compress=True;";
        //for fix at home
        public const string connString = "Data Source= C:/Users/Arien/source/repos/OAnQuan/OAnQuanNew/DatabaseOAQ.db;Version=3;New=True;Compress=True;";
        //public const string connString = "Data Source= C:/Users/Arien/source/repos/OAnQuan/OAnQuan/DataAccess/DatabaseOAQ.db;Version=3;New=True;Compress=True;";
        //for fix at school
        //const string connString = "Data Source= C:/Users/adai106/source/repos/thodien21/OAnQuan/OAnQuan/DataAccess/DatabaseOAQ.db;Version=3;New=True;Compress=True;";
        //public const string connString = "Data Source= C:/Users/adai106/source/repos/OAnQuan/OAnQuan/DataAccess/DatabaseOAQ.db;Version=3;New=True;Compress=True;";
        
        //Require path of database
        //public static String DbPath {get; set;}

        /// <summary>
        /// Player who login/sign up
        /// </summary>
        public static Player Player { get; set; }
        public static List<Player> PlayerListWithRanking => PlayerDb.GetRankingPlayerListWithLimit(PlayerDb.CountPlayer());
        public static int PlayerQty => PlayerListWithRanking.Count;

        /// <summary>
        /// Get own player from login/sign up
        /// </summary>
        /// <param name="pseudo"></param>
        /// <param name="pass"></param>
        public static void GetPlayer(string pseudo, string pass)
        {
            Player = PlayerDb.GetPlayer(pseudo, pass);
        }

        //To know if the game is new or taken from saved game
        public static NoveltyOfGame NoveltyOfGame { get; set; }

        /// <summary>
        /// To require when player 1 plays
        /// </summary>
        public static string PseudoPlayer2 { get; set; }
    }
}
