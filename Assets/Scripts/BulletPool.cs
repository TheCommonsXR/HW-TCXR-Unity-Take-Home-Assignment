using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance { get; private set; }

    public Bullet bulletPrefab;
    public int poolSize = 20;

    private Queue<Bullet> pool = new Queue<Bullet>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        } else
        {
            Instance = this;
        }

        for (int i = 0; i < poolSize; i++)
        {
            var bullet = Instantiate(bulletPrefab, transform);
            bullet.gameObject.SetActive(false);
            pool.Enqueue(bullet);
        }
    }

    public Bullet GetBullet(Vector2 pos, Vector2 dir, int dmg)
    {
        Bullet bullet = pool.Count > 0 ? pool.Dequeue() : Instantiate(bulletPrefab, transform);
        bullet.transform.position = pos;
        bullet.Init(dir, dmg);
        return bullet;
    }

    public void ReturnToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        pool.Enqueue(bullet);
    }
}
