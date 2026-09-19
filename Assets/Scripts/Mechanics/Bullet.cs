using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics

///<summary>
/// Putting this Script on Bullet prefab, it makes the bullet fly, hurt the enemy and then disappear
/// </summary>

{
    public class Bullet : MonoBehaviour
{
    public float speed = 12f; //speed of the bullet
    [System.NonSerialized] public int damage; //bullet damage, player fills this when shooting
    [System.NonSerialized] public Vector2 direction; 

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = direction * speed; //push the bullet in the direction at the 12f speed, then unity physiscs carry the bullet forward
        Destroy(gameObject, 2f); // if the bullet dosent hit anything then destroy it after 2 seconds
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger || other.GetComponent<PlayerController>() != null) return; //ignore coins and player so the bullet dosent hit either of them
        var enemy = other.GetComponent<EnemyController>(); 
        if (enemy != null)
        {
            var health = enemy.GetComponent<Health>(); //enemy health
            health.Decrement(damage); //damage to enemy health depening on the bullets damage 
            if (!health.IsAlive) Schedule<EnemyDeath>().enemy = enemy; //enemy dies after health drops to 0
        }
        Destroy(gameObject);
    }
}
}