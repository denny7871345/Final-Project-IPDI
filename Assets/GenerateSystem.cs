using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateSystem : MonoBehaviour
{
    [SerializeField]
    public List<Transform> positions;

    public Transform left;
    public Transform right;
    public Transform middle;

    public GameObject buff;
    public GameObject minion;
    public GameObject boss;

    [Header("Nonster Settings")]
    public float baseHealth;
    public float growthRate;
    public float bossHealth;
    public float bossRate;
    private int bosslevel;
    private void Start()
    {
        positions.Add(left);
        positions.Add(right);
        Debug.Log(positions.Count);
    }

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

    //每波生成判斷
    public void WaveGeneration(int wave)
    {

        if(wave %2 == 0)
        {
            float monsterHealth = (float)(baseHealth + growthRate * (wave/2) + Math.Pow((double)(wave / 2), 1.5)) ;
            Minion token = minion.GetComponent<Minion>();
            token.SetHealth((int)monsterHealth);
            Instantiate(token, RandomSide().position , Quaternion.identity);
            if (wave % 10 == 0 || wave == 4)
            {
                Invoke("BossGenrate", 1.5f);
            }
        }
        else
        {
            Instantiate(buff, middle.position, Quaternion.identity);
        }
    }

    //隨機左右邊
    private Transform RandomSide()
    {
        int randomIndex = UnityEngine.Random.Range(0, positions.Count);
        Transform selectedPosition = positions[randomIndex];    
        return selectedPosition;
    }

    private void BossGenrate()
    {
        float monsterHealth = (float)(bossHealth + bossRate * bosslevel + Math.Pow((double)(bosslevel / 2), 1.5));
        Boss token = boss.GetComponent<Boss>();
        token.SetHealth((int)monsterHealth);
        Instantiate(token,middle.position, Quaternion.identity);
        bosslevel++;
    }

}
