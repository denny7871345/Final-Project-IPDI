using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Breakable : MonoBehaviour
{
    public enum STATE
    {
        IDLE,
        ATTACK,
        DIE
    }

    public GameObject model;
    public string objName;       // 物體名稱
    public float health = 100f;      // 怪物生命值
    public float speed = 0.02f;
    
    
    public GameObject healthBarPrefab; // 血量條的 Prefab
    [SerializeField]
    protected TextMeshProUGUI healthBar;

    protected Animator anim;
    protected Vector3 healthBarPos = Vector3.zero;
    protected new Collider collider;

    [SerializeField]
    protected STATE state;
    protected bool triggerEnter = false;


    // 通用受傷邏輯
    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        //Debug.Log($"{monsterName} 受到 {damage} 傷害，剩餘生命值：{health}");

        if (health <= 0)
        {
            LeaveBuff();
        }
    }

    // 通用死亡邏輯
    public virtual void Die()
    {        
        if (collider != null)
        {
            Destroy(collider);
        }

        GoToState(STATE.DIE);
        Invoke("DestroyObject", 1.9f);
    }

    public float GetLife()
    {
        return health;
    }

    public virtual void LeaveBuff()
    {

    }

    protected void GoToState(STATE targetState)
    {
        state = targetState;
        triggerEnter = true;
    }

    void DestroyObject()
    {
        Destroy(gameObject);
        Debug.Log("物體已被銷毀");
    }
}
