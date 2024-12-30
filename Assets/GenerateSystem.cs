using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateSystem : MonoBehaviour
{
   public void Generate(GameObject gameObject, Transform pos)
    {
        Instantiate(gameObject, pos.position, Quaternion.identity);
    }

    public void GenerateMinion(Minion gameObject, Transform pos)
    {
        Instantiate(gameObject, pos.position, Quaternion.identity);
    }

    public void GenerateBuffDoor(GameObject gameObject, Transform pos)
    {
        Instantiate(gameObject, pos.position, Quaternion.identity);
    }
    public void GenerateTreasure(Treasure gameObject, Transform pos)
    {
        Instantiate(gameObject, pos.position, Quaternion.identity);
    }
}
