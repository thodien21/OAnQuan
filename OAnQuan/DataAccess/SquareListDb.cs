﻿using OAnQuan.Business;
using System.Data.SQLite;

namespace OAnQuan.DataAccess
{
    public class SquareListDb
    {
        // We use the data source:
        const string connString = "Data Source= C:/Users/ttran/Documents/Visual Studio 2017/Projects/OAnQuan/OAnQuan/DataAccess/DatabaseOAQ.db;Version=3;New=True;Compress=True; ";


        /// <summary>
        /// Insert the square list of saved game
        /// </summary>
        /// <param name="board"></param>
        /// <param name="playerId"></param>
        public static void Save(long playerId, Board board)
        {
            // create a new database connection:
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                // open the connection:
                conn.Open();
                // create a new SQL command:
                for (int i = 0; i < 12; i++)
                {
                    Square square = board.SquaresList[i];
                    var squareNumber = i + 1;
                    var bigTokenQty = Square.GetBigTokenQtyFromList(square.Tokens);
                    var smallTokenQty = square.Tokens.Count - bigTokenQty;

                    using (SQLiteCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO T_SquareList (SmallTokenQty, BigTokenQty, PlayerId, SquareNumber) VALUES (@smallTokenQty, @bigTokenQty, @playerId, @squareNumber) ;";
                        cmd.Parameters.AddWithValue("@smallTokenQty", smallTokenQty);
                        cmd.Parameters.AddWithValue("@bigTokenQty", bigTokenQty);
                        cmd.Parameters.AddWithValue("@playerId", playerId);
                        cmd.Parameters.AddWithValue("@squareNumber", squareNumber);
                        // And execute this again
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        /// <summary>
        /// Update the square list of saved game
        /// </summary>
        /// <param name="board"></param>
        /// <param name="playerId"></param>
        public static void Update(long playerId, Board board)
        {
            // create a new database connection:
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                // open the connection:
                conn.Open();
                // create a new SQL command:
                for (int i = 0; i < 12; i++)
                {
                    Square square = board.SquaresList[i];
                    var squareNumber = i + 1;
                    var bigTokenQty = Square.GetBigTokenQtyFromList(square.Tokens);
                    var smallTokenQty = square.Tokens.Count - bigTokenQty;

                    using (SQLiteCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE T_SquareList SET SmallTokenQty = @smallTokenQty, BigTokenQty = @bigTokenQty WHERE PlayerId = @playerId AND SquareNumber = @squareNumber;";
                        cmd.Parameters.AddWithValue("@smallTokenQty", smallTokenQty);
                        cmd.Parameters.AddWithValue("@bigTokenQty", bigTokenQty);
                        cmd.Parameters.AddWithValue("@playerId", playerId);
                        cmd.Parameters.AddWithValue("@squareNumber", squareNumber);
                        // And execute this again
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
