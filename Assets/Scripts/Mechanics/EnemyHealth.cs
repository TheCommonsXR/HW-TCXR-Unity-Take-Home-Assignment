using UnityEngine;
using Platformer.Mechanics;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;

public class EnemyHealth : Health
{
    public override void Decrement()
    {
        currentHP = Mathf.Clamp(currentHP - 1, 0, maxHP);
        if (currentHP == 0)
        {
            var ev = Schedule<EnemyDeath>();
            ev.enemy = GetComponent<EnemyController>();
        }
    }
}
