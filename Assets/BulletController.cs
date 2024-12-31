using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float speed = 20;      // 子彈速度
    private float lifeTime = 5f;   // 子彈存活時間
    private float damage = 1;

    void Start()
    {
        // 設置子彈在指定時間後自動銷毀
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // 讓子彈沿著自身的前方向移動
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 檢測碰撞並處理傷害或其他邏輯
        // Debug.Log("Hit: " + collision.gameObject.name);
        // 假設你有一個 Monster 類別，並處理傷害
        Breakable monster = collision.gameObject.GetComponent<Breakable>();

        if (monster != null)
        {
            //Debug.Log("進入觸發範圍的是 Monster!");
            monster.TakeDamage(damage);
        }
        else
        {
            
        }

        // 延遲銷毀子彈
        BuffDoor buffDoor = collision.gameObject.GetComponent<BuffDoor>();
        if(buffDoor == null)
        {
            Destroy(gameObject, 0.1f);
        }
        
    }

    public void SetValue(float _speed, float _lifeTime, float _damage)
    {
        speed = _speed;
        lifeTime = _lifeTime;
        damage = _damage;
    }
}
