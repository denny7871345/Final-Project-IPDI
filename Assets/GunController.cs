using UnityEngine;

public class GunController : MonoBehaviour
{
    public BulletController bulletPrefab;  // 子彈 Prefab
    public Transform muzzle;         // 槍口位置
    public float bulletForce = 500f; // 基本子彈推進力

    [Header("Bullet Settings")]
    public bool isFiring;
    public float fireRate = 0.5f;
    public int bulletCount = 1;       // 每次開火的子彈數量
    public float spreadAngle = 10f;   // 散射的總角度 (適用於多子彈情況)
    public float bulletSpeed = 20f;    // 子彈速度
    public float bulletLifeTime = 3f;    // 子彈速度
    public float bulletDamage = 1;
    private float fireTimer = 0f;       // 計時器

    void Update()
    {
        if (isFiring)
        {
            fireTimer += Time.deltaTime;
            if (fireTimer >= fireRate)
            {
                Fire(); // 呼叫 GunController 的 Fire 方法
                fireTimer = 0f; // 重置計時器
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

        // 計算起始角度
        float angleStep = (bulletCount > 1) ? spreadAngle / (bulletCount - 1) : 0;
        float startAngle = -spreadAngle / 2;

        for (int i = 0; i < bulletCount; i++)
        {
            // 計算每顆子彈的角度
            float currentAngle = startAngle + (i * angleStep);
            Quaternion rotation = muzzle.rotation * Quaternion.Euler(0, currentAngle, 0);

            // 生成子彈
            BulletController bullet = Instantiate(bulletPrefab, muzzle.position, rotation);

            // 讓子彈推進
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
