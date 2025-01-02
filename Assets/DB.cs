using UnityEngine;
using System.Data;
using System.Data.SQLite;
using System.IO;

public class DatabaseManager:MonoBehaviour
{
    private string dbPath;
    private string dbFileName = "userInfo.db";

    private static DatabaseManager instance;

    [SerializeField]
    public PlayerInfo playerInfo;

    public int[] powerUp;
    public int nowIndex;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        // �T�O����b���������ɤ��|�Q�P��
        instance = this;
        DontDestroyOnLoad(gameObject);

        dbPath = Path.Combine(Application.persistentDataPath, dbFileName);
        InitializeDatabase();

    }

    private void InitializeDatabase()
    {
        // 檢查資料庫檔案是否存在
        if (!File.Exists(dbPath))
        {
            Debug.Log("Database not found. Creating a new one...");

            // StreamingAssets 中的初始資料庫路徑
            string sourcePath = Path.Combine(Application.streamingAssetsPath, dbFileName);

            // 複製資料庫檔案到 persistentDataPath
            if (Application.platform == RuntimePlatform.Android)
            {
                // Android 的 StreamingAssets 需透過 WWW 或 UnityWebRequest 複製
                using (var www = UnityEngine.Networking.UnityWebRequest.Get(sourcePath))
                {
                    www.SendWebRequest();
                    while (!www.isDone) { }

                    if (string.IsNullOrEmpty(www.error))
                    {
                        File.WriteAllBytes(dbPath, www.downloadHandler.data);
                        Debug.Log("Database copied to persistentDataPath.");
                    }
                    else
                    {
                        Debug.LogError($"Failed to copy database: {www.error}");
                    }
                }
            }
            else
            {
                File.Copy(sourcePath, dbPath);
                Debug.Log("Database copied to persistentDataPath.");
            }
        }
        else
        {
            Debug.Log("Database found at " + dbPath);
        }
    }


    public struct PlayerInfo
    {

        public int health { get; set; }
        public float fireRate { get; set; }
        public int bulletCount { get; set; }      // �C���}�����l�u�ƶq
        public float spreadAngle { get; set; }  // ���g���`���� (�A�Ω�h�l�u���p)
        public float bulletSpeed { get; set; }    // �l�u�t��
        public float bulletLifeTime { get; set; }    // �l�u�t��
        public float bulletDamage { get; set; }    // �l�u�ˮ`
        public int skillPoint { get; set; }    // �ޯ��I
        public PlayerInfo(int health, int bulletCount, float fireRate, float spreadAngle, float bulletSpeed, float bulletLifeTime, float bulletDamage,int skillPoint)
        {
            this.health = health;
            this.bulletCount = bulletCount;
            this.fireRate = fireRate;
            this.spreadAngle = spreadAngle;
            this.bulletSpeed = bulletSpeed;
            this.bulletLifeTime = bulletLifeTime;
            this.bulletDamage = bulletDamage;
            this.skillPoint = skillPoint;
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
                        int skillPoint = reader.GetInt32(6);
                        Debug.Log($"ID: {id}, fireRate: {fireRate}, SpreadAngle: {SpreadAngle}");
                    }
                }
            }
        }
    }
    public PlayerInfo InsertData(int Health, float fireRate, int bulletCount, float spreadAngle, float bulletSpeed, float bulletLifeTime, float bulletDamage, int skillPoint)
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

            string query = "INSERT INTO player (Health, FireRate, BulletCount, SpreadAngle, BulletSpeed, BulletLifeTime, BulletDamage, SkillPoint) " +
                "VALUES (@Health, @FireRate, @BulletCount, @SpreadAngle, @BulletSpeed, @BulletLifeTime, @BulletDamage, @SkillPoint)";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Health", Health);
                command.Parameters.AddWithValue("@FireRate", fireRate);
                command.Parameters.AddWithValue("@BulletCount", bulletCount);
                command.Parameters.AddWithValue("@SpreadAngle", spreadAngle);
                command.Parameters.AddWithValue("@BulletSpeed", bulletSpeed);
                command.Parameters.AddWithValue("@BulletLifeTime", bulletLifeTime);
                command.Parameters.AddWithValue("@BulletDamage", bulletDamage);
                command.Parameters.AddWithValue("@SkillPoint", skillPoint);

                command.ExecuteNonQuery();
            }
        }
        playerInfo = new PlayerInfo(Health, bulletCount, fireRate, spreadAngle, bulletSpeed, bulletLifeTime, bulletDamage,skillPoint);
        return playerInfo;

    }


    public void UpdateData(int id, int health, float fireRate, int bulletCount, float spreadAngle, float bulletSpeed, float bulletLifeTime, float bulletDamage, int skillPoint)
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
                           "BulletDamage = @BulletDamage, " +
                           "SkillPoint = @SkillPoint " +
                           "WHERE ID = @ID;";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                // �K�[�Ѽ�
                command.Parameters.AddWithValue("@ID", id);
                command.Parameters.AddWithValue("@Health", health);
                command.Parameters.AddWithValue("@FireRate", fireRate);
                command.Parameters.AddWithValue("@BulletCount", bulletCount);
                command.Parameters.AddWithValue("@SpreadAngle", spreadAngle);
                command.Parameters.AddWithValue("@BulletSpeed", bulletSpeed);
                command.Parameters.AddWithValue("@BulletLifeTime", bulletLifeTime);
                command.Parameters.AddWithValue("@BulletDamage", bulletDamage);
                command.Parameters.AddWithValue("@SkillPoint", skillPoint);

                // ����R�O
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
                           "SkillPoint = @SkillPoint " +
                           "WHERE ID = @ID;";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                // �K�[�Ѽ�
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
                    case "skillPoint":
                        command.Parameters.AddWithValue("@SkillPoint", value);
                        break;
                }
                // ����R�O
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
                $"Bullet Damage: {playerInfo.bulletDamage},\n"+
                $"Skill Point: {playerInfo.skillPoint}";
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
                // �ϥΰѼƤƬd�ߡA�קK SQL Injection
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
                        int skillPoint = (int)reader.GetFloat(8);
                        playerInfo = new PlayerInfo(health,bulletCount,fireRate,spreadAngle,bulletSpeed,bulletLifeTime,bulletDamage,skillPoint);
                        Debug.Log($"ID: {id}, Fire Rate: {fireRate},bulletCount: {bulletCount},Spread Angle: {spreadAngle}, bulletSpeed: {bulletSpeed}, bulletLifeTime: {bulletLifeTime}, bulletDamage:{bulletDamage}, skillPoint:{skillPoint}");
                        nowIndex = targetId;
                        return playerInfo;
                    }
                    else
                    {
                        Debug.Log($"No data found for ID: {targetId}");
                        playerInfo = InsertData(100, 0.7f, 1, 5, 3, 2, 10, 0);
                        return playerInfo;
                    }
                }
            }
        }
    }

}
