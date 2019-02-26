using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace OAnQuan.DataAccess
{
    public class PlayerDb
    {
        // We use these three SQLite objects:
        SQLiteCommand cmd;
        SQLiteDataReader dataReader;

        public void InsertPlayer(string pso, string pass)
        {
            // create a new database connection:
            using (SQLiteConnection conn = new SQLiteConnection("Data Source= C:/Users/ttran/Documents/Visual Studio 2017/Projects/OAnQuan/OAnQuan/DatabaseOAQ.db;Version=3;New=True;Compress=True;"))
            {
                // open the connection:
                conn.Open();

                // create a new SQL command:
                cmd = conn.CreateCommand();

                // Lets insert something into our new table:
                cmd.CommandText = "INSERT INTO T_Player (Pseudo, Password) VALUES (@pso, @pass);";
                cmd.Parameters.AddWithValue("@pso", pso);
                cmd.Parameters.AddWithValue("@pass", pass);
                // And execute this again ;D
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        // But how do we read something out of our table ?
        public List<string> DisplayPlayerList()
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source = DatabaseOAQ.db; version=3; New=True; Compress=true;"))
            {
                conn.Open();
                // First lets build a SQL-Query again:
                cmd.CommandText = "SELECT * FROM T_Player";

                // Now the SQLiteCommand object can give us a DataReader-Object:
                dataReader = cmd.ExecuteReader();

                // The SQLiteDataReader allows us to run through the result lines:
                List<string> listData = new List<string>();
                while (dataReader.Read()) // Read() returns true if there is still a result line to read
                {
                    // Print out the content of the text field:
                    //System.Console.WriteLine(dataReader["text"]);
                    String data = dataReader.GetString(1);
                    listData.Add(data);
                }

                // We are ready, now lets cleanup and close our connection:
                conn.Close();
                return listData;
            }
        }
    }
}
