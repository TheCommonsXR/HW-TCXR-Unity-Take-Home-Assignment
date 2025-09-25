using UnityEngine;
using Platformer.Mechanics;
using Platformer.Gameplay;

public class Bullet : KinematicObject
{
    public int damage = 1;
    public float speed = 10.0f;
    public float lifetime = 3.0f;

    float currentLifeTimer;
    Vector2 direction;

    public void Init(Vector2 dir)
    {
        direction = dir.normalized;
        velocity = direction * speed;
        targetVelocity = direction * speed; 
        currentLifeTimer = lifetime;
        gravityModifier = 0f;
        gameObject.SetActive(true);
    }

    protected override void ComputeVelocity()
    {
        targetVelocity = direction * speed;
    }

    protected override void Update()
    {
        base.Update();

        currentLifeTimer -= Time.deltaTime;

        if (currentLifeTimer <= 0f)
        {
            BulletPool.Instance.ReturnToPool(this);
        }
    }
    protected override void FixedUpdate()
    {
        Vector2 deltaPosition = velocity * Time.deltaTime;
        body.position += deltaPosition;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var health = other.GetComponent<Health>();
        if (health != null)
        {
            // Only deal damage to enemies
            if (other.GetComponent<PlayerController>() != null) return;

            int numDmg = 0;

            while (numDmg < damage)
            {
                health.Decrement();
                numDmg++;
            }
            Debug.Log(other.gameObject.name);
            BulletPool.Instance.ReturnToPool(this);
        } else
        {
            Debug.Log(other.gameObject.name);
            BulletPool.Instance.ReturnToPool(this);
        }
    }
}
