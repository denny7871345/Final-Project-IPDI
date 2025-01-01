using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour
{
    enum STATE
    {
        IDEL,
        PROCESS,
        FINAL_BATTLE,
        END
    }
    public GameObject button;
    public GenerateSystem generateSystem;
    public PlayerController player;
    public RoadControll roadScroller;

    public float waveRate = 5f;
    [SerializeField]
    private int wave;
    private float waveTimer = 0f;       // ­p®É¾¹
    [SerializeField]
    private STATE state;
    private Canvas canvas;
    private bool triggerEnter;


    private void Start()
    {
        wave = 0;
        state = STATE.IDEL;
        canvas = GetComponentInChildren<Canvas>();
        DatabaseManager database = new DatabaseManager();
        DatabaseManager.playerInfo token = database.ReadSpecificData(3);
        database.UpdateData(3,"health",2000);
        player.PlayerSet(token);
    }

    private void Update()
    {
        switch (state)
        {
            case STATE.IDEL:
                if (triggerEnter)
                {
                    canvas.enabled = true;
                    triggerEnter = false;
                    button.SetActive(false);
                    break;
                }
                
                break;
            case STATE.PROCESS:
                if (triggerEnter)
                {
                    canvas.enabled = false;
                    triggerEnter = false;
                    break;
                }
                waveTimer += Time.deltaTime;
                if(waveTimer >= waveRate)
                {
                    wave++;
                    generateSystem.WaveGeneration(wave);
                    waveTimer = 0f;
                }
                if( player.health <= 0)
                {
                    GoToState(STATE.END);
                }


                break;
            case STATE.FINAL_BATTLE:
                if (triggerEnter)
                {

                    triggerEnter = false;
                    break;
                }
                break;
            case STATE.END:
                if (triggerEnter)
                {
                    roadScroller.RoadWork(false);
                    canvas.enabled = true;
                    button.SetActive(true);
                    triggerEnter = false;
                    break;
                }
                
                break;
        }
    }

    public void Trigger()
    {
        roadScroller.RoadWork(true);
        GoToState(STATE.PROCESS);
    }
    private void GoToState(STATE targetState)
    {
        state = targetState;
        triggerEnter = true;
    }





}
