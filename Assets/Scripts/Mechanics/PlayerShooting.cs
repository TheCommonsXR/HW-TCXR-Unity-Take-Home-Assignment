using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    public Transform firePoint;
    private SpriteRenderer spriteRenderer_player;

    void Start()
    {
        spriteRenderer_player = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
          HadleShooting();   
    }

    public void HadleShooting()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        var bullet_gameObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        var bullet = bullet_gameObject.GetComponent<Bullet>();
        Vector2 fireDirection = spriteRenderer_player.flipX ? Vector2.left : Vector2.right;
        bullet.SetDirection(fireDirection);
    }

    
}
