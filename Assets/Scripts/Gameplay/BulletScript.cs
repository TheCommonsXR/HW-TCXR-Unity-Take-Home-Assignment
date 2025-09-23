using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Gameplay
{
    public class BulletScript : MonoBehaviour
    {
        public int damage = 0;
        public float activeTimer = 10;

        void Update()
        {
            activeTimer -= Time.deltaTime;
            if (activeTimer <= 0)
            {
                Destroy(gameObject);
            }         
        }
    }
}