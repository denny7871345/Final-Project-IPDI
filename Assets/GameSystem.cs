using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public DatabaseManager database;

    public float waveRate = 5f;
    [SerializeField]
    private int wave;
    private float waveTimer = 0f;       // �p�ɾ�
    [SerializeField]
    private STATE state;
    private Canvas canvas;
    private bool triggerEnter;


    private void Start()
    {
        database = FindObjectOfType<DatabaseManager>();
        if (database != null)
        {
            Debug.Log("Found DatabaseManager!");
            // �o�̥i�H�ϥ� databaseManager ����ާ@
        }
        else
        {
            Debug.LogError("DatabaseManager not found!");
        }

        wave = 0;
        GoToState(STATE.IDEL);
        canvas = GetComponentInChildren<Canvas>();
        
        player.PlayerSet(database.playerInfo);
        player.GetBuffEntry(database.powerUp);
        database.playerInfo = player.NowGunStatus(database.playerInfo.skillPoint);
        Debug.Log("now data = " + database.PlayerInfoOutPut());
        
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
                    if(button != null)
                    {
                        button.SetActive(false);
                        Debug.Log("Button has been hide");
                    }
                    
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
                    canvas.enabled = true;
                    roadScroller.RoadWork(false);
                    TextMeshProUGUI text = canvas.GetComponentInChildren<TextMeshProUGUI>();
                    text.text = "You Dead";
                    button.SetActive(true);
                    triggerEnter = false;
                    database.UpdateData(database.nowIndex, database.playerInfo.health, database.playerInfo.fireRate, database.playerInfo.bulletCount, database.playerInfo.spreadAngle, database.playerInfo.bulletSpeed, database.playerInfo.bulletLifeTime, database.playerInfo.bulletDamage, (int)database.playerInfo.skillPoint);
                    
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

    public void AddSkillPoint()
    {
        database.playerInfo.skillPoint++;
        Debug.Log($"Now Skill Point is:{database.playerInfo.skillPoint}");
    }

    public void BackToMenu()
    {

        SceneManager.LoadScene("MainMenu");
    }

}
