using OAnQuan.Business;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace OAnQuan.DataAccess
{
    public class PoolDb
    {
        // We use the data source:
        const string connString = "Data Source= C:/Users/ttran/Documents/Visual Studio 2017/Projects/OAnQuan/OAnQuan/DataAccess/DatabaseOAQ.db;Version=3;New=True;Compress=True; ";

        /// <summary>
        /// Insert the pool's player of saved game
        /// </summary>
        /// <param name="board"></param>
        /// <param name="playerId"></param>
        public static void Save(Board board, long playerId)
        {
            // create a new database connection:
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                // open the connection:
                conn.Open();
                // create a new SQL command:
                for (int i = 0; i < 2; i++)
                {
                    List<Token> pool = board.PlayersList[i].Pool;
                    long playerNumber = i + 1;
                    var bigTokenQty = Square.GetBigTokenQtyFromList(pool);
                    var smallTokenQty = pool.Count - bigTokenQty;

                    using (SQLiteCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO T_Pool (SmallTokenQty, BigTokenQty, PlayerId, PlayerNumber) VALUES (@smallTokenQty, @bigTokenQty, @playerId, @playerNumber) ;";
                        cmd.Parameters.AddWithValue("@smallTokenQty", smallTokenQty);
                        cmd.Parameters.AddWithValue("@bigTokenQty", bigTokenQty);
                        cmd.Parameters.AddWithValue("@playerId", playerId);
                        cmd.Parameters.AddWithValue("@playerNumber", playerNumber);
                        // And execute this again
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        /// <summary>
        /// Update the pool's player of saved gamee
        /// </summary>
        /// <param name="board"></param>
        /// <param name="playerId"></param>
        public static void Update(Board board, long playerId)
        {
            // create a new database connection:
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                // open the connection:
                conn.Open();
                // create a new SQL command:
                for (int i = 0; i < 2; i++)
                {
                    List<Token> pool = board.PlayersList[i].Pool;
                    long playerNumber = i + 1;
                    var bigTokenQty = Square.GetBigTokenQtyFromList(pool);
                    var smallTokenQty = pool.Count - bigTokenQty;

                    using (SQLiteCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE T_Pool SET SmallTokenQty = @smallTokenQty, BigTokenQty = @bigTokenQty WHERE PlayerId = @playerId AND PlayerNumber = @playerNumber;";
                        cmd.Parameters.AddWithValue("@smallTokenQty", smallTokenQty);
                        cmd.Parameters.AddWithValue("@bigTokenQty", bigTokenQty);
                        cmd.Parameters.AddWithValue("@playerId", playerId);
                        cmd.Parameters.AddWithValue("@playerNumber", playerNumber);
                        // And execute this again
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
