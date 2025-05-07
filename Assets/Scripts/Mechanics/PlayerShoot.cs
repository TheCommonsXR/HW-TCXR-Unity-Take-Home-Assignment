using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Fired when the player performs a Jump.
    /// </summary>
    /// <typeparam name="PlayerShoot"></typeparam>
    public class PlayerShoot : Simulation.Event<PlayerShoot>
    {
        public Transform firePoint;
        public GameObject bulletPrefab;
        public float bulletSpeed;
        public int bulletDamage;
        public bool flipped;

        public override void Execute()
        {
            if (flipped){
                bulletSpeed = -bulletSpeed;
            }

            GameObject bullet = UnityEngine.Object.Instantiate(bulletPrefab, firePoint.transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0);
            UnityEngine.Object.Destroy(bullet, 2f);
        }
    }
}