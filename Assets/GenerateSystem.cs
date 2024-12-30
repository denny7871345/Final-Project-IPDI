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
    public GameObject Minion;

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
            Instantiate(Minion, RandomSide().position , Quaternion.identity);
        }
        else
        {
            Instantiate(buff, middle.position, Quaternion.identity);
        }
    }

    //隨機左右邊
    private Transform RandomSide()
    {
        int randomIndex = Random.Range(0, positions.Count);


        Debug.Log(randomIndex);
        Transform selectedPosition = positions[randomIndex];    
        return selectedPosition;
    }



}
