using UnityEngine;
using System.Data;
using System.Data.SQLite;
using System.IO;

public class DatabaseManager : MonoBehaviour
{
    private string dbPath;

    void Start()
    {
        dbPath = Path.Combine(Application.dataPath, "GameData/userInfo.db");
        ReadDatabase();
    }

    void ReadDatabase()
    {
        if (!File.Exists(dbPath))
        {
            Debug.LogError("Database file not found");
            return;
        }

        string connectionString = $"Data Source={dbPath};Version=3;";
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT * FROM player";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        float fireRate = reader.GetFloat(1);
                        float SpreadAngle = reader.GetFloat(2);

                        Debug.Log($"ID: {id}, Name: {fireRate}, Description: {SpreadAngle}");
                    }
                }
            }
        }
    }
}
