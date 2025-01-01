using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum STATE
    {
        IDLE,
        LOCOMOTION,
        Die
    }

    public float health = 10f;
    public float velocity = 3.0f;

    public GameObject model;
    [SerializeField]
    private Vector3 movingVec;
    private Animator anim;
    [SerializeField]
    private STATE state;
    private bool triggerEnter = false;
    [SerializeField]
    private float range0;
    [SerializeField]
    private float range1;

    public GunController gunController; // 引用 GunController
    public GameObject healthBarPrefab; // 血量條的 Prefab
    [SerializeField]
    private TextMeshProUGUI healthBar;
    private Vector3 healthBarPos = Vector3.zero;

    private void Awake()
    {
        anim = model.GetComponent<Animator>();
    }

    private void Start()
    {
        transform.position = model.transform.position;
        healthBarPos = transform.position + transform.up * 2.0f;
        healthBarPrefab.transform.position = healthBarPos;

    }

    private void Update()   
    {
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
                    anim.SetBool("Shoot", false);
                    triggerEnter = false;
                    break;
                }
                //anim.SetBool("IsWalking",false);
                break;
            case STATE.LOCOMOTION:
                if (triggerEnter)
                {
                    Fire(true);
                    anim.CrossFadeInFixedTime("Aiming", 0.1f);
                    anim.SetFloat("Y", 1);
                    triggerEnter = false;
                    anim.SetBool("Shoot", true);
                    break;
                }
                anim.SetFloat("X",transform.InverseTransformDirection(movingVec).normalized.x);
                
                if(transform.position.x > range0)
                {
                    SetPositionX(transform, range0);
                }
                else if(transform.position.x < range1)
                {
                    SetPositionX(transform, range1);
                }
                else
                {
                    transform.position += movingVec * velocity;
                }
                
                //面朝哪邊
                //model.transform.forward = Vector3.Slerp(model.transform.forward, movingVec, 0.1f);

                break;
            case STATE.Die:
                anim.SetBool("Dead", true);
                anim.SetBool("Shoot", false);
                Fire(false);
                break;
            default:
                break;
        }
        //Vector3 newVelocity = movingVec * velocity;
        //transform.position += movingVec * velocity * Time.deltaTime;
        //transform.Translate(movingVec * velocity * Time.deltaTime, Space.World);
        //transform.Translate(movingVec * velocity * Time.deltaTime, Space.Self);

        //rigid.velocity = movingVec * velocity;  
    }


    private void GoToState(STATE targetState)
    {
        state = targetState;
        triggerEnter = true;
    }

    //.
    public void Move(Vector3 vector)
    {
        movingVec = vector.normalized;
        //Debug.Log(transform.InverseTransformDirection(movingVec).normalized.x);
    }
    //.
    public void Trigger(bool _trigger)
    {
        if (_trigger)
        {
            GoToState(STATE.LOCOMOTION);
        }
    }

    void SetPositionX(Transform target, float newX)
    {
        Vector3 pos = target.position;
        pos.x = newX;
        target.position = pos;
    }

    private void Fire(bool fire)
    {
        gunController.StartFire(fire);
    }

    private void TakeDamage(float _damage)
    {
        health -= _damage;
        if (health <= 0)
        {
            GoToState(STATE.Die);
        }
    }

    //.
     public void OnCollisionEnter(Collision collision)
     {
        Debug.Log("touch somthing");

        GameObject gameObject = collision.gameObject;

         if(state == STATE.LOCOMOTION)
         {
            Minion enemy = gameObject.GetComponent<Minion>();
            if (enemy != null)
            {
                int damage = (int)enemy.GetLife();
                enemy.TakeDamage(health);
                TakeDamage(damage);
  
                return; // 碰撞到目標後直接返回，避免執行後續代碼
            }

            BuffDoor buff = gameObject.GetComponent<BuffDoor>();
            if (buff != null)
            {
                gunController.GetBuff(buff.buffName,buff.GetLife());
                buff.Die();
                return; // 碰撞到目標後直接返回，避免執行後續代碼
            }

            Treasure treasure = gameObject.GetComponent<Treasure>();
            if (treasure != null)
            {
                gunController.GetBuff(treasure.buffName, treasure.GetLife());
                treasure.Die();
                return; // 碰撞到目標後直接返回，避免執行後續代碼
            }
        }

         //anim.SetBool("isGround", true);
     }

    public void PlayerSet(DatabaseManager.playerInfo _playerInfo)
    {
        health = _playerInfo.health;
        gunController.spreadAngle = _playerInfo.spreadAngle;
        gunController.bulletCount = _playerInfo.bulletCount;
        gunController.bulletDamage = _playerInfo.bulletDamage;
        gunController.bulletLifeTime = _playerInfo.bulletLifeTime;
        gunController.bulletSpeed = _playerInfo.bulletSpeed;
        gunController.fireRate = _playerInfo.fireRate;
    }

}
