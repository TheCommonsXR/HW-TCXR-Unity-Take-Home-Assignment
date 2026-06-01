using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public int bulletDamage = 1;

    public GameMode gameModeConfig;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Fire()
    {
        if (bulletPrefab == null || firePoint == null)
            return;

        GameObject bulletObject = Instantiate(
            bulletPrefab,
            firePoint.position,
            Quaternion.identity
        );

        BulletController bullet =
            bulletObject.GetComponent<BulletController>();

        if (bullet != null)
        {
            int direction =
                spriteRenderer != null && spriteRenderer.flipX
                    ? -1
                    : 1;

            int damageToUse = bulletDamage;

            if (gameModeConfig != null)
            {
                damageToUse = gameModeConfig.bulletDamage;
                bullet.SetSpeed(gameModeConfig.bulletSpeed);
            }

            bullet.Fire(direction, damageToUse);
        }
    }
}