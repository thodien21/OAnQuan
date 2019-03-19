using System.Data.SQLite;

namespace OAnQuan.DataAccess
{
    public static class BoardDb
    {
        // We use the data source:
        const string connString = "Data Source= C:/Users/ttran/Documents/Visual Studio 2017/Projects/OAnQuan/OAnQuan/DataAccess/DatabaseOAQ.db;Version=3;New=True;Compress=True; ";

        /// <summary>
        /// Check if the player has saved a game
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public static bool CheckIfContainsPlayerId(long playerId)
        {
            bool contains = false;
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                conn.Open();

                // create a new SQL command:
                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT PlayerId FROM T_Board WHERE PlayerId = @playerId";
                    cmd.Parameters.AddWithValue("@playerId", playerId);
                    using (SQLiteDataReader dataReader = cmd.ExecuteReader())
                    {
                        if (dataReader.Read()) // Read() returns true if there is still a result line to read
                        {
                            contains = true;
                        }
                    }
                }
            }
            return contains;
        }



        /// <summary>
        /// Save board
        /// </summary>
        /// <param name="turn"></param>
        /// <param name="player2Pseudo"></param>
        /// <param name="playerId"></param>
        public static void Save(long turn, string player2Pseudo, long playerId)
        {
            // create a new database connection:
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                // open the connection:
                conn.Open();
                // create a new SQL command:
                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO T_Board (Turn, Player2Pseudo, PlayerId) VALUES (@turn, @player2Pseudo, @playerId) ;";
                    cmd.Parameters.AddWithValue("@turn", turn);
                    cmd.Parameters.AddWithValue("@player2Pseudo", player2Pseudo);
                    cmd.Parameters.AddWithValue("@playerId", playerId);
                    // And execute this again
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Update board
        /// </summary>
        /// <param name="turn"></param>
        /// <param name="player2Pseudo"></param>
        /// <param name="playerId"></param>
        public static void Update(long turn, string player2Pseudo, long playerId)
        {
            // create a new database connection:
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                // open the connection:
                conn.Open();
                // create a new SQL command:
                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "UPDATE T_Board SET Turn = @turn, Player2Pseudo = @player2Pseudo WHERE PlayerId = @playerId;";
                    cmd.Parameters.AddWithValue("@turn", turn);
                    cmd.Parameters.AddWithValue("@player2Pseudo", player2Pseudo);
                    cmd.Parameters.AddWithValue("@playerId", playerId);
                    // And execute this again
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
