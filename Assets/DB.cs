using UnityEngine;
using System.Data;
using System.Data.SQLite;
using System.IO;

public class DatabaseManager:MonoBehaviour
{
    private string dbPath = Path.Combine(Application.dataPath, "GameData/userInfo.db");

    [SerializeField]
    public PlayerInfo playerInfo;

    public struct PlayerInfo
    {

        public int health { get; set; }
        public float fireRate { get; set; }
        public int bulletCount { get; set; }      // –Ω秨紆计秖
        public float spreadAngle { get; set; }  // 床甮羆à (続ノ紆薄猵)
        public float bulletSpeed { get; set; }    // 紆硉
        public float bulletLifeTime { get; set; }    // 紆硉
        public float bulletDamage { get; set; }    // 紆端甡

        public PlayerInfo(int health, int bulletCount, float fireRate, float spreadAngle, float bulletSpeed, float bulletLifeTime, float bulletDamage)
        {
            this.health = health;
            this.bulletCount = bulletCount;
            this.fireRate = fireRate;
            this.spreadAngle = spreadAngle;
            this.bulletSpeed = bulletSpeed;
            this.bulletLifeTime = bulletLifeTime;
            this.bulletDamage = bulletDamage;
        }
    }

    public void ReadDatabase()
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
    public PlayerInfo InsertData(int Health, float fireRate, int bulletCount, float spreadAngle, float bulletSpeed, float bulletLifeTime, float bulletDamage)
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

            string query = "INSERT INTO player (Health, FireRate, BulletCount, SpreadAngle, BulletSpeed, BulletLifeTime, BulletDamage) " + 
                "VALUES (@Health, @FireRate, @BulletCount, @SpreadAngle, @BulletSpeed, @BulletLifeTime, @BulletDamage)";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Health", Health);
                command.Parameters.AddWithValue("@FireRate", fireRate);
                command.Parameters.AddWithValue("@BulletCount", bulletCount);
                command.Parameters.AddWithValue("@SpreadAngle", spreadAngle);
                command.Parameters.AddWithValue("@BulletSpeed", bulletSpeed);
                command.Parameters.AddWithValue("@BulletLifeTime", bulletLifeTime);
                command.Parameters.AddWithValue("@BulletDamage", bulletDamage);

                command.ExecuteNonQuery();
            }
        }
        playerInfo = new PlayerInfo(Health, bulletCount, fireRate, spreadAngle, bulletSpeed, bulletLifeTime, bulletDamage);
        return playerInfo;

    }


    public void UpdateData(int id, int health, float fireRate, int bulletCount, float spreadAngle, float bulletSpeed, float bulletLifeTime, float bulletDamage)
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

            string query = "UPDATE player SET " +
                           "Health = @Health, " +
                           "FireRate = @FireRate, " +
                           "BulletCount = @BulletCount, " +
                           "SpreadAngle = @SpreadAngle, " +
                           "BulletSpeed = @BulletSpeed, " +
                           "BulletLifeTime = @BulletLifeTime, " +
                           "BulletDamage = @BulletDamage " +
                           "WHERE ID = @ID;";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                // 睰把计
                command.Parameters.AddWithValue("@ID", id);
                command.Parameters.AddWithValue("@Health", health);
                command.Parameters.AddWithValue("@FireRate", fireRate);
                command.Parameters.AddWithValue("@BulletCount", bulletCount);
                command.Parameters.AddWithValue("@SpreadAngle", spreadAngle);
                command.Parameters.AddWithValue("@BulletSpeed", bulletSpeed);
                command.Parameters.AddWithValue("@BulletLifeTime", bulletLifeTime);
                command.Parameters.AddWithValue("@BulletDamage", bulletDamage);

                // 磅︽㏑
                command.ExecuteNonQuery();
            }
        }
    }


    public void UpdateData(int id ,string buffName,float value)
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

            string query = "UPDATE player SET " +
                           "Health = @Health, " +
                           "FireRate = @FireRate, " +
                           "BulletCount = @BulletCount, " +
                           "SpreadAngle = @SpreadAngle, " +
                           "BulletSpeed = @BulletSpeed, " +
                           "BulletLifeTime = @BulletLifeTime, " +
                           "BulletDamage = @BulletDamage " +
                           "WHERE ID = @ID;";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                // 睰把计
                switch (buffName)
                {
                    case "health":
                        command.Parameters.AddWithValue("@Health", value);
                        break;
                    case "fire rate":
                        command.Parameters.AddWithValue("@FireRate", value);
                        break;
                    case "bullet count":
                        command.Parameters.AddWithValue("@BulletCount", value);
                        break;
                    case "spread angle":
                        command.Parameters.AddWithValue("@SpreadAngle", value);
                        break;
                    case "bullet speed":
                        command.Parameters.AddWithValue("@BulletSpeed", value);
                        break;
                    case "bullet lifetime":
                        command.Parameters.AddWithValue("@BulletLifeTime", value);
                        break;
                    case "bullet damage":
                        command.Parameters.AddWithValue("@BulletDamage", value);
                        break;
                }
                // 磅︽㏑
                command.ExecuteNonQuery();
            }
        }
    }

    public string PlayerInfoOutPut()
    {
        string text = $"Fire Rate: {playerInfo.fireRate},\n" +
                $"Bullet Count: {playerInfo.bulletCount},\n" +
                $"Spread Angle: {playerInfo.spreadAngle},\n" +
                $"Bullet Speed: {playerInfo.bulletSpeed},\n" +
                $"Bullet Life Time: {playerInfo.bulletLifeTime},\n" +
                $"Bullet Damage: {playerInfo.bulletDamage}";
        return (text);
    }


    public PlayerInfo ReadSpecificData(int targetId)
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
                // ㄏノ把计て琩高磷 SQL Injection
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

                        playerInfo = new PlayerInfo(health,bulletCount,fireRate,spreadAngle,bulletSpeed,bulletLifeTime,bulletDamage);
                        Debug.Log($"ID: {id}, Fire Rate: {fireRate},bulletCount: {bulletCount},Spread Angle: {spreadAngle}, bulletSpeed: {bulletSpeed}, bulletLifeTime: {bulletLifeTime}, bulletDamage:{bulletDamage}");

                        return playerInfo;
                    }
                    else
                    {
                        Debug.Log($"No data found for ID: {targetId}");
                        playerInfo = InsertData(100,0.7f,1,5,3,2,10);
                        return playerInfo;
                    }
                }
            }
        }
    }

}
