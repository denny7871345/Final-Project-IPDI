using UnityEngine;
using System.Data;
using System.Data.SQLite;
using System.IO;

public class DatabaseManager : MonoBehaviour
{
    private string dbPath;

    public struct playerInfo
    {
        int id;
        float fireRate;
        int bulletCount;       // –Ω秨紆计秖
        float spreadAngle;   // 床甮羆à (続ノ紆薄猵)
        float bulletSpeed;    // 紆硉
        float bulletLifeTime;    // 紆硉
        float bulletDamage;

        public playerInfo(int id, int bulletCount, float fireRate, float spreadAngle, float bulletSpeed, float bulletLifeTime, float bulletDamage)
        {
            this.id = id;
            this.bulletCount = bulletCount;
            this.fireRate = fireRate;
            this.spreadAngle = spreadAngle;
            this.bulletSpeed = bulletSpeed;
            this.bulletLifeTime = bulletLifeTime;
            this.bulletDamage = bulletDamage;
        }
    }

    void Start()
    {
        dbPath = Path.Combine(Application.dataPath, "GameData/userInfo.db");
        ReadSpecificData(1);
        ReadSpecificData(2);
        ReadSpecificData(5);
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
                        int bulletCount = reader.GetInt32(1);
                        float fireRate = reader.GetFloat(1);
                        float SpreadAngle = reader.GetFloat(2);
                        float BulletSpeed = reader.GetFloat(3);
                        float BulletLifeTime = reader.GetFloat(4);
                        float BulletDamage = reader.GetFloat(5);
                        Debug.Log($"ID: {id}, fireRate: {fireRate}, SpreadAngle: {SpreadAngle}");
                    }
                }
            }
        }
    }

    public playerInfo? ReadSpecificData(int targetId)
    {
        if (!File.Exists(dbPath))
        {
            Debug.LogError("Database file not found");
            return null;
        }

        string connectionString = $"Data Source={dbPath};Version=3;";
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT * FROM player WHERE ID = @id";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                // ㄏノ把计て琩高磷 SQL Injection
                command.Parameters.AddWithValue("@id", targetId);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        float fireRate = reader.GetFloat(1);
                        int bulletCount = (int)reader.GetFloat(2);
                        float spreadAngle = reader.GetFloat(3);
                        float bulletSpeed = reader.GetFloat(4);
                        float bulletLifeTime = reader.GetFloat(5);
                        float bulletDamage = reader.GetFloat(6);

                        playerInfo token = new playerInfo(id,bulletCount,fireRate,spreadAngle,bulletSpeed,bulletLifeTime,bulletDamage);
                        Debug.Log($"ID: {id}, Fire Rate: {fireRate},bulletCount: {bulletCount},Spread Angle: {spreadAngle}, bulletSpeed: {bulletSpeed}, bulletLifeTime: {bulletLifeTime}, bulletDamage:{bulletDamage}");
                        return token;
                    }
                    else
                    {
                        Debug.LogWarning($"No data found for ID: {targetId}");
                        return null;
                    }
                }
            }
        }
    }

}
