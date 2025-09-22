using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    // Reference to Bullet Prefab
    public GameObject bulletPrefab;
    public float speed = 20.0f;


    public void SpawnBullet(Vector2 position, Vector3 direction, int damageAmt)
    {
        GameObject newBullet = Instantiate(bulletPrefab);
        newBullet.transform.position = position;

        var bulletComp = newBullet.GetComponent<Bullet>();

        bulletComp.speed = speed;
        bulletComp.direction = direction;
        bulletComp.damage = damageAmt;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
