using OAnQuan.Business;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Text;

namespace OAnQuan.DataAccess
{
    public static class PlayerDb
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
                    ", [Password] text NOT NULL, [isAdmin] bigint NULL, [FullName] text NULL, [WinGameQty] bigint NULL" +
                    ", [DrawGameQty] bigint NULL, [LoseGameQty] bigint NULL)";

                SQLiteCommand createTable = new SQLiteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }

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
        /// When the visitor creat a new account (!!!check if this pseudo already existe)
        /// </summary>
        /// <param name="pso">Pseudo taped by visitor</param>
        /// <param name="pass">Password taped by visitor</param>
        public static void InsertPlayer(string _pseudo, string _password, string _fullName)
        {
            // create a new database connection:
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                // open the connection:
                conn.Open();

                // create a new SQL command:
                SQLiteCommand cmd = conn.CreateCommand();

                // Lets insert something into our new table:
                cmd.CommandText = "INSERT INTO T_Player (Pseudo, Password, FullName) VALUES (@pso, @pass, @fullName);";
                cmd.Parameters.AddWithValue("@pso", _pseudo);
                cmd.Parameters.AddWithValue("@pass", ComputeHash(_password, new SHA256CryptoServiceProvider()));
                cmd.Parameters.AddWithValue("@fullName", _fullName);
                // And execute this again ;D
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }        

        /// <summary>
        /// Check if the couple pseudo/password exist when login
        /// </summary>
        /// <param name="_pseudo">taped pseudo</param>
        /// <param name="_password">taped password</param>
        /// <returns></returns>
        static public bool IsThisPlayerExist(string _pseudo, string _password)
        {
            var _isThisPlayerExist = false;
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                conn.Open();

                // create a new SQL command:
                SQLiteCommand cmd = conn.CreateCommand();

                // First lets build a SQL-Query again:
                cmd.CommandText = "SELECT * FROM T_Player where Pseudo = @pso and Password = @pass";
                cmd.Parameters.AddWithValue("@pso", _pseudo);
                cmd.Parameters.AddWithValue("@pass", ComputeHash(_password, new SHA256CryptoServiceProvider()));

                // Now the SQLiteCommand object can give us a DataReader-Object:
                SQLiteDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read()) // Read() returns true if there is still a result line to read
                {
                    _isThisPlayerExist = true;
                }
                
                // We are ready, now lets cleanup and close our connection:
                conn.Close();
                return _isThisPlayerExist;
            }
        }

        /// <summary>
        /// Display the list of all players
        /// </summary>
        /// <returns></returns>
        public static List<Player> GetAllPlayer()
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
                List<Player> listPlayer = new List<Player>();
                while (dataReader.Read()) // Read() returns true if there is still a result line to read
                {
                    string pseudo = (string)dataReader["Pseudo"];
                    string fullName = (string)dataReader["FullName"];
                    
                    listPlayer.Add(new Player(pseudo, fullName));
                }

                // We are ready, now lets cleanup and close our connection:
                conn.Close();
                return listPlayer;
            }
        }

        public static List<Player> Ranking(int _limit)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                conn.Open();

                // create a new SQL command:
                SQLiteCommand cmd = conn.CreateCommand();

                // First lets build a SQL-Query again:
                cmd.CommandText = "SELECT Pseudo, WinGameQty, DrawGameQty, LoseGameQty " +
                    "FROM T_Player GROUP BY Pseudo " +
                    "ORDER BY WinGameQty DESC, LoseGameQty, DrawGaeQty DESC " +
                    "LIMIT @limit ";
                cmd.Parameters.AddWithValue("@limit", _limit);

                // Now the SQLiteCommand object can give us a DataReader-Object:
                SQLiteDataReader dataReader = cmd.ExecuteReader();

                // The SQLiteDataReader allows us to run through the result lines:
                List<Player> listPlayer = new List<Player>();
                while (dataReader.Read()) // Read() returns true if there is still a result line to read
                {
                    string pseudo = (string)dataReader["Pseudo"];
                    int winGameQty = (int)dataReader["WinGameQty"];
                    int loseGameQty = (int)dataReader["LoseGameQty"];
                    int drawGameQty = (int)dataReader["DrawGameQty"];

                    listPlayer.Add(new Player(pseudo, winGameQty, loseGameQty, drawGameQty));
                }

                // We are ready, now lets cleanup and close our connection:
                conn.Close();
                return listPlayer;
            }
        }

    }
}
