using UnityEngine;

public class Boss : Breakable
{
    public float specialAttackCooldown = 10f; // �S������N�o�ɶ�
    //private float specialAttackTimer = 0f;

    public GameObject reward;

    public GameSystem gameSystem;

    public AudioSource deadSound;
    private bool hasGenReward = false;

    private void Start()
    {
        gameSystem = FindObjectOfType<GameSystem>();
        if (gameSystem != null)
        {
            Debug.Log("Found GameSystem!");
            // �o�̥i�H�ϥ� databaseManager ����ާ@
        }
        else
        {
            Debug.LogError("GameSystem not found!");
        }

        objName = "Boss";
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
        // ����q�����H�Ǫ���m
        if (healthBarPrefab != null)
        {
            healthBarPos = transform.position + transform.up * 1.5f;
            healthBar.text = health.ToString();
            healthBar.transform.position = healthBarPos;
            healthBarPrefab.transform.rotation = Camera.main.transform.rotation; // �¦V�۾�
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

                if (transform.position.z < -7)
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


    public void SpecialAttack()
    {
        Debug.Log($"{objName} ����S��ޯ�A�y�����q�ˮ`�I");
        // �K�[�S��ޯ��޿�A�Ҧp�d������αj�O�ޯ�
    }

    public override void LeaveBuff()
    {
        Die();
        if (reward != null)
        {
            if (!hasGenReward)
            {
                gameSystem.AddSkillPoint();
                Instantiate(reward, transform.position, Quaternion.identity);
                hasGenReward = true;
            }

        }
    }

    public void SetHealth(float value)
    {
        health = value;
    }

}
