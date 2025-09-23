using Platformer.Core;
using Platformer.Mechanics;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the enemy is hurt
    /// </summary>
    /// <typeparam name="EnemyDamaged"></typeparam>
    public class EnemyDamaged : Simulation.Event<EnemyDamaged>
    {
        public EnemyController enemy;

        public override void Execute()
        {
            if (enemy.audioSource && enemy.ouch)
                enemy.audioSource.PlayOneShot(enemy.ouch);

            enemy.animator.SetTrigger("hurt");
        }
    }
}