using UnityEngine;
using System.Data;
using System.Data.SQLite;
using System.IO;

public class DatabaseManager
{
    private string dbPath = Path.Combine(Application.dataPath, "GameData/userInfo.db");

    public struct playerInfo
    {
        public int id { get; set; }

        public int health { get; set; }
        public float fireRate { get; set; }
        public int bulletCount { get; set; }      // 每次開火的子彈數量
        public float spreadAngle { get; set; }  // 散射的總角度 (適用於多子彈情況)
        public float bulletSpeed { get; set; }    // 子彈速度
        public float bulletLifeTime { get; set; }    // 子彈速度
        public float bulletDamage { get; set; }    // 子彈傷害

        public playerInfo(int id, int health, int bulletCount, float fireRate, float spreadAngle, float bulletSpeed, float bulletLifeTime, float bulletDamage)
        {
            this.id = id;
            this.health = health;
            this.bulletCount = bulletCount;
            this.fireRate = fireRate;
            this.spreadAngle = spreadAngle;
            this.bulletSpeed = bulletSpeed;
            this.bulletLifeTime = bulletLifeTime;
            this.bulletDamage = bulletDamage;
        }
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
    void InsertData(float fireRate, int bulletCount, float spreadAngle, float bulletSpeed, float bulletLifeTime, float bulletDamage)
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

            string query = "INSERT INTO player (Health, FireRate, BulletCount, SpreadAngle, BulletSpeed, BulletLifeTime, BulletDamage) " + 
                "VALUES (@Health, @FireRate, @BulletCount, @SpreadAngle, @BulletSpeed, @BulletLifeTime, @BulletDamage)";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@FireRate", fireRate);
                command.Parameters.AddWithValue("@BulletCount", bulletCount);
                command.Parameters.AddWithValue("@SpreadAngle", spreadAngle);
                command.Parameters.AddWithValue("@BulletSpeed", bulletSpeed);
                command.Parameters.AddWithValue("@BulletLifeTime", bulletLifeTime);
                command.Parameters.AddWithValue("@BulletDamage", bulletDamage);

                command.ExecuteNonQuery();
            }
        }
    }

    
    void UpdateData(float fireRate, int bulletCount, float spreadAngle, float bulletSpeed, float bulletLifeTime, float bulletDamage)
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

            string query = "INSERT INTO player (Health, FireRate, BulletCount, SpreadAngle, BulletSpeed, BulletLifeTime, BulletDamage) " +
                "VALUES (@Health, @FireRate, @BulletCount, @SpreadAngle, @BulletSpeed, @BulletLifeTime, @BulletDamage)";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@FireRate", fireRate);
                command.Parameters.AddWithValue("@BulletCount", bulletCount);
                command.Parameters.AddWithValue("@SpreadAngle", spreadAngle);
                command.Parameters.AddWithValue("@BulletSpeed", bulletSpeed);
                command.Parameters.AddWithValue("@BulletLifeTime", bulletLifeTime);
                command.Parameters.AddWithValue("@BulletDamage", bulletDamage);

                command.ExecuteNonQuery();
            }
        }
    }

    public playerInfo ReadSpecificData(int targetId)
    {
        if (!File.Exists(dbPath))
        {
            Debug.LogError("Database file not found");
            throw new FileNotFoundException($"Database file not found at path: {dbPath}");
        }

        string connectionString = $"Data Source={dbPath};Version=3;";
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT * FROM player WHERE ID = @id";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                // 使用參數化查詢，避免 SQL Injection
                command.Parameters.AddWithValue("@id", targetId);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        int health = (int)reader.GetFloat(1);
                        float fireRate = reader.GetFloat(2);
                        int bulletCount = (int)reader.GetFloat(3);
                        float spreadAngle = reader.GetFloat(4);
                        float bulletSpeed = reader.GetFloat(5);
                        float bulletLifeTime = reader.GetFloat(6);
                        float bulletDamage = reader.GetFloat(7);

                        playerInfo token = new playerInfo(id,health,bulletCount,fireRate,spreadAngle,bulletSpeed,bulletLifeTime,bulletDamage);
                        Debug.Log($"ID: {id}, Fire Rate: {fireRate},bulletCount: {bulletCount},Spread Angle: {spreadAngle}, bulletSpeed: {bulletSpeed}, bulletLifeTime: {bulletLifeTime}, bulletDamage:{bulletDamage}");
                        return token;
                    }
                    else
                    {
                        throw new FileNotFoundException($"No data found for ID: {targetId}");
                    }
                }
            }
        }
    }

}
