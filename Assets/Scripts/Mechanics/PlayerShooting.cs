using UnityEngine;


    namespace Platformer.Mechanics
{
    public class playershooting : MonoBehaviour
{
    public GameObject bulletPrefab;

    public float bulletSpeed = 10f;
    public SpriteRenderer bullet_sprite;
    public int bulletDamage;
 public void Awake()
    {
        bullet_sprite = GetComponent<SpriteRenderer>();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Shoot();
        }

    }
public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        int direction = 1;
        if (bullet_sprite.flipX)
        {
            direction = -1;
        }
        bullet.GetComponent<Bullet>().Setup(direction, bulletDamage);
    }
}
}