using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public int bulletDamage = 1;

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

        BulletController bullet = bulletObject.GetComponent<BulletController>();

        if (bullet != null)
        {
            int direction = spriteRenderer.flipX ? -1 : 1;
            bullet.Fire(direction, bulletDamage);
        }
    }
}