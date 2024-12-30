using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDoor : Breakable
{
    [SerializeField]
    public string buffName = "";
    private float threshold = 30;

    private float damageCount = 0;

   

    private void Start()
    {
        objName = "BuffDoor";
        health = 1;
        healthBar.text = buffName + ValueText();
        transform.position = model.transform.position;
        healthBarPos = transform.position;
        healthBarPrefab.transform.position = healthBarPos;
        collider = GetComponent<Collider>();

        
    }

    private void Update()
    {
        if (healthBarPrefab != null)
        {
            healthBarPos = transform.position;
            healthBar.text = buffName + ValueText();
            healthBar.transform.position = healthBarPos;
            healthBarPrefab.transform.rotation = Camera.main.transform.rotation; // ´Â¦V¬Û¾÷
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
                break;
            case STATE.DIE:
                if (triggerEnter)
                {
                    triggerEnter = false;
                    break;
                }
                break;
            default:
                break;
        }


    }

    public override void TakeDamage(float damage)
    {
        damageCount += damage;
        if(damageCount >= threshold)
        {
            health += 1;
            damageCount -= threshold;
        }
    }

    public override void LeaveBuff()
    {
        Debug.Log($"player get buff : {buffName}{ValueText()}");
    }

    private string ValueText()
    {
        if(health >= 0)
        {
            return "+" + health.ToString();
        }
        else
        {
            return health.ToString();
        }
    }

}
