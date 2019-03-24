using OAnQuan.Business;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Text;

namespace OAnQuan.DataAccess
{
    public class PlayerDb
    {
        // We use the data source:
        const string connString = "Data Source= C:/Users/ttran/Documents/Visual Studio 2017/Projects/OAnQuan/OAnQuan/DataAccess/DatabaseOAQ.db;Version=3;New=True;Compress=True;";

        /// <summary>
        /// Creat the table of player
        /// </summary>
        public static void InitializePlayerTable()
        {
            using (SQLiteConnection db =
                new SQLiteConnection("Data Source=DatabaseOAQ.db; Version = 3; New = True; Compress = True; "))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT EXISTS T_Player " +
                    "(  [PlayerId] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, [Pseudo] text NOT NULL" +
                    ", [Password] text NOT NULL, [isAdmin] bigint NOT NULL, [FullName] text NOT NULL, [WinGameQty] bigint NOT NULL" +
                    ", [DrawGameQty] bigint NOT NULL, [LoseGameQty] bigint NOT NULL)";

                SQLiteCommand createTable = new SQLiteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }

        #region Sign In/ Sign Up
        /// <summary>
        /// Hash the password
        /// </summary>
        /// <param name="input">password inserted by user</param>
        /// <param name="algorithm"></param>
        /// <returns></returns>
        public static string ComputeHash(string input, HashAlgorithm algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }

        /// <summary>
        /// Insert player when the visitor creat a new account (!!!check if this pseudo already existe)
        /// </summary>
        /// <param name="pso">Pseudo taped by visitor</param>
        /// <param name="pass">Password taped by visitor</param>
        public static void InsertPlayer(string pseudo, string password, string fullName)
        {
            // create a new database connection:
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                // open the connection:
                conn.Open();

                // create a new SQL command:
                SQLiteCommand cmd = conn.CreateCommand();

                // Lets insert something into our new table:
                cmd.CommandText = "INSERT INTO T_Player (Pseudo, Password, FullName, IsAdmin, IsDisabled, WinGameQty, DrawGameQty, LoseGameQty) " +
                    "VALUES (@pso, @pass, @fullName, @isAdmin, @isDisabled, @winGameQty, @drawGameQty, @loseGameQty);";
                cmd.Parameters.AddWithValue("@pso", pseudo);
                cmd.Parameters.AddWithValue("@pass", ComputeHash(password, new SHA256CryptoServiceProvider()));
                cmd.Parameters.AddWithValue("@fullName", fullName);
                cmd.Parameters.AddWithValue("@isAdmin", 0);
                cmd.Parameters.AddWithValue("@isDisabled", 0);
                cmd.Parameters.AddWithValue("@winGameQty", 0);
                cmd.Parameters.AddWithValue("@drawGameQty", 0);
                cmd.Parameters.AddWithValue("@loseGameQty", 0);
                // And execute this again ;D
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Get player when user signs in/ or creat a new account
        /// </summary>
        /// <param name="pseudo"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        static public Player GetPlayer(string pseudo, string password)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                conn.Open();

                // create a new SQL command:
                SQLiteCommand cmd = conn.CreateCommand();

                // First lets build a SQL-Query again:
                cmd.CommandText = "SELECT * FROM T_Player WHERE Pseudo = @pso and Password = @pass";
                cmd.Parameters.AddWithValue("@pso", pseudo);
                cmd.Parameters.AddWithValue("@pass", ComputeHash(password, new SHA256CryptoServiceProvider()));

                // Now the SQLiteCommand object can give us a DataReader-Object:
                SQLiteDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    return new Player()
                    {
                        PlayerId = (long)dataReader["PlayerId"],
                        Pseudo = pseudo,
                        Password = password,
                        FullName = (string)dataReader["FullName"],
                        IsAdmin = (long)dataReader["IsAdmin"],
                        IsDisabled = (long)dataReader["IsDisabled"],
                        WinGameQty = (long)dataReader["WinGameQty"],
                        DrawGameQty = (long)dataReader["DrawGameQty"],
                        LoseGameQty = (long)dataReader["LoseGameQty"]
                    };
                }
                else return null;
            }
        }
        #endregion

        /// <summary>
        /// GetPlayerIdFromPseudo
        /// </summary>
        /// <param name="pseudo"></param>
        /// <returns>playerId</returns>
        public static long GetPlayerIdFromPseudo(string pseudo)
        {
            long playerId = 0;
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                conn.Open();
                // create a new SQL command:
                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT PlayerId FROM T_Player WHERE Pseudo = @pseudo";
                    cmd.Parameters.AddWithValue("@pseudo", pseudo);

                    using (SQLiteDataReader dataReader = cmd.ExecuteReader())
                    {
                        if (dataReader.Read()) // Read() returns true if there is still a result line to read
                        {
                            playerId = (long)dataReader["PlayerId"];
                        }
                    }
                }
            }
            return playerId;
        }

        /// <summary>
        /// Get best players with ranking
        /// </summary>
        /// <param name="limit">number of best players to rank</param>
        /// <returns>pseudo, winGameQty, drawGameQty, loseGameQty</returns>
        public static List<Player> GetRanking(int limit)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                conn.Open();

                // create a new SQL command:
                SQLiteCommand cmd = conn.CreateCommand();

                // First lets build a SQL-Query again:
                cmd.CommandText = "SELECT Pseudo, WinGameQty, DrawGameQty, LoseGameQty " +
                    "FROM T_Player GROUP BY Pseudo " +
                    "ORDER BY WinGameQty DESC, LoseGameQty, DrawGameQty DESC " +
                    "LIMIT @limit ";
                cmd.Parameters.AddWithValue("@limit", limit);

                // Now the SQLiteCommand object can give us a DataReader-Object:
                SQLiteDataReader dataReader = cmd.ExecuteReader();

                // The SQLiteDataReader allows us to run through the result lines:
                List<Player> listPlayer = new List<Player>();
                while (dataReader.Read()) // Read() returns true if there is still a result line to read
                {
                    string pseudo = (string)dataReader["Pseudo"];
                    long winGameQty = (long)dataReader["WinGameQty"];
                    long loseGameQty = (long)dataReader["LoseGameQty"];
                    long drawGameQty = (long)dataReader["DrawGameQty"];

                    listPlayer.Add(new Player(pseudo, winGameQty, loseGameQty, drawGameQty));
                }
                return listPlayer;
            }
        }

        #region Update result in database
        /// <summary>
        /// Update the quantity of win games of player
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public static long UpdateWinGameQty(long playerId)
        {
            long variable = 100;
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                conn.Open();
                // create a new SQL command:
                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "UPDATE T_Player SET WinGameQty = WinGameQty+1 WHERE PlayerId = @playerId";
                    cmd.Parameters.AddWithValue("@playerId", playerId);

                    using (SQLiteDataReader dataReader = cmd.ExecuteReader())
                    {
                        if (dataReader.Read()) // Read() returns true if there is still a result line to read
                        {
                            variable = (long)dataReader["WinGameQty"];
                        }
                    }
                }
            }
            return variable;
        }

        /// <summary>
        /// Update the quantity of draw games of player
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public static long UpdateDrawGameQty(long playerId)
        {
            long variable = 100;
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                conn.Open();
                // create a new SQL command:
                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "UPDATE T_Player SET DrawGameQty = DrawGameQty+1 WHERE PlayerId = @playerId";
                    cmd.Parameters.AddWithValue("@playerId", playerId);

                    using (SQLiteDataReader dataReader = cmd.ExecuteReader())
                    {
                        if (dataReader.Read()) // Read() returns true if there is still a result line to read
                        {
                            variable = (long)dataReader["DrawGameQty"];
                        }
                    }
                }
                // We are ready, now lets cleanup and close our connection:
                conn.Close();
            }
            return variable;
        }

        /// <summary>
        /// Update the quantity of lose games of player
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public static long UpdateLoseGameQty(long playerId)
        {
            long variable = 100;
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                conn.Open();
                // create a new SQL command:
                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "UPDATE T_Player SET LoseGameQty = LoseGameQty+1 WHERE PlayerId = @playerId";
                    cmd.Parameters.AddWithValue("@playerId", playerId);

                    using (SQLiteDataReader dataReader = cmd.ExecuteReader())
                    {
                        if (dataReader.Read()) // Read() returns true if there is still a result line to read
                        {
                            variable = (long)dataReader["LoseGameQty"];
                        }
                    }
                }
            }
            return variable;
        }       
        #endregion


        #region Funtionnalities reserved for admins: GetAllPlayer, UpgradePlayerToAdmin, DeactivatePlayer, ReactivatePlayer, Search Player, SeeInfoOfEveryPlayer 
        /// <summary>
        /// Check if this player is Admin
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns>bool</returns>
        public static bool IfAdmin(long playerId)
        {
            long isAdmin = 0;
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                conn.Open();

                //create a new SQL command
                SQLiteCommand cmd = conn.CreateCommand();

                //First lets build a SQL-Query again:
                cmd.CommandText = "SELECT IsAdmin FROM T_Player WHERE PlayerId = @playerId";
                cmd.Parameters.AddWithValue("@playerId", playerId);
                using(SQLiteDataReader dataReader = cmd.ExecuteReader())
                {
                    if(dataReader.Read())
                    {
                        isAdmin = (long)dataReader["IsAdmin"];
                    }
                }
            }
            return (isAdmin == 1)? true : false;
        }

        /// <summary>
        /// Display the list of all players
        /// </summary>
        /// <param name="asAdminId">id of an admin</param>
        /// <returns></returns>
        public static List<Player> GetAllPlayer(long asAdminId)
        {
            List<Player> listPlayer = new List<Player>();
            if (PlayerDb.IfAdmin(asAdminId) == true)//if this is admin
            {
                using (SQLiteConnection conn = new SQLiteConnection(connString))
                {
                    conn.Open();

                    // create a new SQL command:
                    SQLiteCommand cmd = conn.CreateCommand();

                    // First lets build a SQL-Query again:
                    cmd.CommandText = "SELECT Pseudo, FullName FROM T_Player";

                    // Now the SQLiteCommand object can give us a DataReader-Object:
                    SQLiteDataReader dataReader = cmd.ExecuteReader();

                    // The SQLiteDataReader allows us to run through the result lines:
                    while (dataReader.Read()) // Read() returns true if there is still a result line to read
                    {
                        string pseudo = (string)dataReader["Pseudo"];
                        string fullName = (string)dataReader["FullName"];

                        listPlayer.Add(new Player(pseudo, fullName));
                    }
                }
                return listPlayer;
            }
            else throw new ArgumentOutOfRangeException(nameof(asAdminId), "Cette fonctionnalité n'est réservée qu'au administrateur");
        }

        /// <summary>
        /// Admin upgrade a player to admin
        /// </summary>
        /// <param name="asAdminId">id of an admin</param>
        /// <param name="playerId">id of player</param>
        public static void UpgradePlayerToAdmin(long asAdminId, long playerId)
        {
            if (PlayerDb.IfAdmin(asAdminId) == true)//if this is admin
            {
                using (SQLiteConnection conn = new SQLiteConnection(connString))
                {
                    conn.Open();
                    // create a new SQL command:
                    using (SQLiteCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE T_Player SET IsAdmin = 1 WHERE PlayerId = @playerId";
                        cmd.Parameters.AddWithValue("@playerId", playerId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            else throw new ArgumentOutOfRangeException(nameof(asAdminId), "Cette fonctionnalité n'est réservée qu'au administrateur");
        }

        /// <summary>
        /// Admin deactivate a player
        /// </summary>
        /// <param name="asAdminId"></param>
        /// <param name="playerId"></param>
        public static void DeactivatePlayer(long asAdminId, long playerId)
        {
            if (PlayerDb.IfAdmin(asAdminId) == true)//if this is admin
            {
                using (SQLiteConnection conn = new SQLiteConnection(connString))
                {
                    conn.Open();
                    // create a new SQL command:
                    using (SQLiteCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE T_Player SET IsDisabled = 1 WHERE PlayerId = @playerId";
                        cmd.Parameters.AddWithValue("@playerId", playerId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            else throw new ArgumentOutOfRangeException(nameof(asAdminId), "Cette fonctionnalité n'est réservée qu'au administrateur");
        }

        /// <summary>
        /// Admin can reactivate a player
        /// </summary>
        /// <param name="asAdminId"></param>
        /// <param name="playerId"></param>
        public static void ReactivatePlayer(long asAdminId, long playerId)
        {
            if (PlayerDb.IfAdmin(asAdminId) == true)//if this is admin
            {
                using (SQLiteConnection conn = new SQLiteConnection(connString))
                {
                    conn.Open();
                    // create a new SQL command:
                    using (SQLiteCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE T_Player SET IsDisabled = 0 WHERE PlayerId = @playerId";
                        cmd.Parameters.AddWithValue("@playerId", playerId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            else throw new ArgumentOutOfRangeException(nameof(asAdminId), "Cette fonctionnalité n'est réservée qu'au administrateur");
        }
        #endregion
    }
}
