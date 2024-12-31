using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float speed = 20;      // �l�u�t��
    private float lifeTime = 5f;   // �l�u�s���ɶ�
    private float damage = 1;

    void Start()
    {
        // �]�m�l�u�b���w�ɶ���۰ʾP��
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // ���l�u�u�ۦۨ����e��V����
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �˴��I���óB�z�ˮ`�Ψ�L�޿�
        // Debug.Log("Hit: " + collision.gameObject.name);
        // ���]�A���@�� Monster ���O�A�óB�z�ˮ`
        Breakable monster = collision.gameObject.GetComponent<Breakable>();

        if (monster != null)
        {
            //Debug.Log("�i�JĲ�o�d�򪺬O Monster!");
            monster.TakeDamage(damage);
        }
        else
        {
            
        }

        // ����P���l�u
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
