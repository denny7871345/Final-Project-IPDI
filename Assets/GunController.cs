using UnityEngine;

public class GunController : MonoBehaviour
{
    public BulletController bulletPrefab;  // �l�u Prefab
    public Transform muzzle;         // �j�f��m
    public float bulletForce = 500f; // �򥻤l�u���i�O

    [Header("Bullet Settings")]
    public bool isFiring;
    public float fireRate = 0.5f;
    public int bulletCount = 1;       // �C���}�����l�u�ƶq
    public float spreadAngle = 10f;   // ���g���`���� (�A�Ω�h�l�u���p)
    public float bulletSpeed = 20f;    // �l�u�t��
    public float bulletLifeTime = 3f;    // �l�u�t��
    public float bulletDamage = 1;
    private float fireTimer = 0f;       // �p�ɾ�

    void Update()
    {
        if (isFiring)
        {
            fireTimer += Time.deltaTime;
            if (fireTimer >= fireRate)
            {
                Fire(); // �I�s GunController �� Fire ��k
                fireTimer = 0f; // ���m�p�ɾ�
            }
        }
    }

    public void Fire()
    {
        if (bulletCount <= 0 || bulletPrefab == null || muzzle == null)
        {
            Debug.LogWarning("Invalid bullet configuration!");
            return;
        }

        // �p��_�l����
        float angleStep = (bulletCount > 1) ? spreadAngle / (bulletCount - 1) : 0;
        float startAngle = -spreadAngle / 2;

        for (int i = 0; i < bulletCount; i++)
        {
            // �p��C���l�u������
            float currentAngle = startAngle + (i * angleStep);
            Quaternion rotation = muzzle.rotation * Quaternion.Euler(0, currentAngle, 0);

            // �ͦ��l�u
            BulletController bullet = Instantiate(bulletPrefab, muzzle.position, rotation);

            // ���l�u���i
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(rotation * Vector3.forward * bulletSpeed, ForceMode.Impulse);
                bullet.SetValue(bulletSpeed,bulletLifeTime,bulletDamage);
            }
        }
    }
    //.
    public void StartFire(bool _fire)
    {
        isFiring = _fire;
    }
    //.
    public void GetBuff(string _buffName, float _value)
    {
        switch (_buffName)
        {
            case "fire rate":
                fireRate /= (1 + _value * 0.01f);
                break;
            case "bullet count":
                bulletCount +=  (int)_value;
                break;
            case "bullet speed":
                bulletSpeed *= (1 + _value * 0.01f);
                break;
            case "bullet damage":
                bulletDamage += _value;
                break;
            case "bullet life":
                bulletLifeTime *= (1 + _value * 0.01f);
                break;
        }
    }

}
