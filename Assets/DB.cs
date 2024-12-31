using MongoDB.Driver;
using UnityEngine;
using System.Collections.Generic;
using MongoDB.Bson;

public class DB : MonoBehaviour
{
    private MongoClient client;
    private IMongoDatabase database;
    private IMongoCollection<BsonDocument> collection;

    void Start()
    {
        client = new MongoClient("mongodb://localhost:27017/GameApp");
        database = client.GetDatabase("GameApp");
        collection = database.GetCollection<BsonDocument>("user");

        // Åª¨ú¼Æ¾Ú
        var filter = Builders<BsonDocument>.Filter.Empty;
        var result = collection.Find(filter).ToList();
        foreach (var doc in result)
        {
            Debug.Log($"ID: {doc["_id"]}, BulletCount: {doc["BulletCount"]}, SpreadAngle: {doc["SpreadAngle"]}");
        }
    }
}


