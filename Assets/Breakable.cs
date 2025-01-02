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
    public string objName;       // ����W��
    public float health = 100f;      // �Ǫ��ͩR��
    public float speed = 0.02f;
    
    
    public GameObject healthBarPrefab; // ��q���� Prefab
    [SerializeField]
    protected TextMeshProUGUI healthBar;

    protected Animator anim;
    protected Vector3 healthBarPos = Vector3.zero;
    protected new Collider collider;

    [SerializeField]
    protected STATE state;
    protected bool triggerEnter = false;


    // �q�Ψ����޿�
    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        //Debug.Log($"{monsterName} ���� {damage} �ˮ`�A�Ѿl�ͩR�ȡG{health}");

        if (health <= 0)
        {
            LeaveBuff();
        }
    }

    // �q�Φ��`�޿�
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
        Debug.Log("����w�Q�P��");
    }
}
