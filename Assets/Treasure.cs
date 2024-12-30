using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : Breakable
{
    [SerializeField]
    public string buffName = "Unknow";

    public List<string> buffList;

    private void Start()
    {
        buffName = RandomBuff();
        objName = "Treasure";
        health = 1;
        healthBar.text = buffName + ValueText();
        transform.position = model.transform.position;
        healthBarPos = transform.position + transform.up * 2.0f;
        healthBarPrefab.transform.position = healthBarPos;
        collider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (healthBarPrefab != null)
        {
            healthBarPos = transform.position + transform.up * 2.0f;
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
                if (transform.position.z < -7)
                {
                    Die();
                }
                break;
            default:
                break;
        }


    }

    public override void TakeDamage(float damage)
    {
       
    }

    private string ValueText()
    {
        if (health >= 0)
        {
            return "+" + health.ToString();
        }
        else
        {
            return health.ToString();
        }
    }

    public override void Die()
    {
        if (collider != null)
        {
            Destroy(collider);
        }
        Invoke("DestroyObject", 2f);
    }

    private string RandomBuff()
    {
        int randomIndex = Random.Range(0, buffList.Count);
        string name = buffList[randomIndex];
        return name;
    }

}
