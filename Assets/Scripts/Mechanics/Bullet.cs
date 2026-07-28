using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
using Platformer.Mechanics;
using static Platformer.Core.Simulation;
using UnityEngine;
//question 3
public class Bullet : MonoBehaviour
{

    private int dmg;
    public float bulletSpeed = 10f;

    public Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void getPlayerInfo(Vector2 playerDirection, int playerdmg)
    {
        direction = playerDirection;
        dmg = playerdmg;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * bulletSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        EnemyController enemy = other.GetComponent<EnemyController>();
        if(enemy != null)
        {
              Debug.Log("Enemy controller found");
            Health hp = enemy.GetComponent<Health>();
            if (hp != null)
        {

            hp.Decrement(dmg);
  
                if (!hp.IsAlive)
                {
                    Schedule<EnemyDeath>().enemy = enemy;
                }
        }
          else
    {

    }
        Destroy(gameObject);
        }
    }
}
