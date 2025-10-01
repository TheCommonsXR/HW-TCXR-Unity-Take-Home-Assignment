using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public int bulletDamage = 2;
    public KeyCode fireKey = KeyCode.Z;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(fireKey))
        {
            Fire();
        }
    }

    private void Fire()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        var bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.Initialize(bulletDamage, spriteRenderer.flipX ? Vector2.left : Vector2.right);
        
    }
}
