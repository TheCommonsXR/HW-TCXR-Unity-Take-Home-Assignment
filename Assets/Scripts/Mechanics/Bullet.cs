using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
using Platformer.Mechanics;
using static Platformer.Core.Simulation;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 movementVector;

    public float speed = 5;

    public float lifetime = 3;

    float timer;

    int damage = 1;
    void Start()
    {

    }
    //start life time of bullet and the speed and movement
    public void Init(Vector2 move, int _damage)
    {
        movementVector = move.normalized * speed;
        damage = _damage;
        timer = lifetime;
    }
    // Update is called once per frame

    //move the bullet and reduce life timer
    void Update()
    {
        transform.position += movementVector * Time.deltaTime;
        timer -= Time.deltaTime;
        //destroy after lifetime
        if (timer <= 0)
            Destroy(this.gameObject);
    }
    //on collision reduce hp of enemy or destroy enemy. Destroy bullet no matter what
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemyController = collision.gameObject.GetComponent<EnemyController>();

        if (enemyController)
        {
            var enemyHealth = enemyController.GetComponent<Health>();
            if (enemyHealth)
            {
                for (int i = 0; i < damage; i++)
                {
                    enemyHealth.Decrement();
                }
            }
            else
            {
                Schedule<EnemyDeath>().enemy = enemyController;
            }
            Destroy(gameObject);
        }
    }
}
