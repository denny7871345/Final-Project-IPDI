using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject minion;
    public GameObject buffDoor;
    public GameObject treasure;

    public UnityEvent<GameObject,Transform> GenerateThing;
    public UnityEvent GameStart;

    public Transform leftSide;
    public Transform rightSide;

    private Transform side;
    private bool Minion;
    private bool BuffDoor;
    private bool TreasureBox;
    private bool hasntTrigger;

    private void Start()
    {
        side = leftSide;
        hasntTrigger = true;
    }

    private void Update()
    {
        CalculateBuffDoor();
        CalculateMinion();
        CalculateSide();
        CalculateTreasureBox();
        CalculateGameStart();
    }

    private void CalculateMinion()
    {
        Minion = Input.GetKeyDown("j");
        if (Minion)
        {
            GenerateThing?.Invoke(minion, side);
        }
    }

    private void CalculateBuffDoor()
    {
        BuffDoor = Input.GetKeyDown("k");
        if (BuffDoor)
        {
            GenerateThing?.Invoke(buffDoor, side);
        }
    }
    private void CalculateTreasureBox()
    {
        TreasureBox = Input.GetKeyDown("l");
        if (TreasureBox)
        {
            GenerateThing?.Invoke(treasure, side);
        }
    }
    private void CalculateSide()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            side = leftSide;
        }else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            side = rightSide;
        }
    }

    private void CalculateGameStart()
    {
        if (Input.GetKeyDown("space"))
        {
            if (hasntTrigger)
            {
                GameStart?.Invoke();
            }
        }
    }

}
