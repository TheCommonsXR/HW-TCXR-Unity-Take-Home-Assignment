using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Core;
using Platformer.Mechanics;
using static Platformer.Core.Simulation;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when a bullet hits an enemy.
    /// </summary>
    public class BulletHitEnemy : Simulation.Event<BulletHitEnemy>
    {
        public EnemyController enemy;
        public int damage;

        public override void Execute()
        {
            var enemyHealth = enemy.GetComponent<Health>();
            if (enemyHealth != null)
            {
                for (int i = 0; i < damage; i++)
                {
                    enemyHealth.Decrement();
                }

                if (!enemyHealth.IsAlive)
                {
                    Schedule<EnemyDeath>().enemy = enemy;
                }
            }
        }
    }
}

