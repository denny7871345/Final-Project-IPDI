using UnityEngine;
using UnityEngine.Events;


public class Minion : Breakable
{
    public GameObject reward;
    public AudioSource deadSound;
    private bool hasGenReward = false;
    private void Start()
    {
        objName = "Minon";
        anim = model.GetComponent<Animator>();
        GoToState(STATE.IDLE);
        transform.position = model.transform.position;
        healthBarPos = transform.position + transform.up * 2.0f;
        healthBarPrefab.transform.position = healthBarPos;
        collider = GetComponent<Collider>();
        deadSound = GetComponent<AudioSource>();

        if (anim != null)
        {
            Debug.Log("anim good");
        }
        else
        {
            Debug.Log("anim not good");
        }

    }

    protected void Update()
    {
        // 讓血量條跟隨怪物位置
        if (healthBarPrefab != null)
        {
            healthBarPos = transform.position + transform.up * 2.0f;
            healthBar.text = health.ToString();
            healthBar.transform.position = healthBarPos;
            healthBarPrefab.transform.rotation = Camera.main.transform.rotation; // 朝向相機
        }

        switch (state)
        {
            case STATE.IDLE:
                if (triggerEnter)
                {
                    triggerEnter = false;
                    break;
                }
                Vector3 forward = transform.position;
                forward.z -= speed;
                transform.position = forward;

                if(transform.position.z < -7)
                {
                    Die();
                }

                //anim.SetBool("IsWalking",false);
                break;


            case STATE.DIE:
                if (triggerEnter)
                {
                    
                    anim.SetTrigger("Die");
                    deadSound.Play();
                    healthBar.enabled = false;
                    triggerEnter = false;
                    break;
                }



                break;
            default:
                break;
        }

    }

    //.
    public override void LeaveBuff()
    {
        Die();
        if (reward != null)
        {
            if (!hasGenReward)
            {
                Instantiate(reward, transform.position, Quaternion.identity);
                hasGenReward = true;
            }
            
        }
    }
    //.
    public void SetHealth(float value)
    {
        health = value;
    }

}
