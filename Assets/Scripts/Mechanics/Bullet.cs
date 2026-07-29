using UnityEngine;


namespace Platformer.Mechanics
{
public class Bullet : MonoBehaviour
{
   public float speed;
   public int bulletdamage;
   private Rigidbody2D rb;

   public void Awake()
   {
       rb = GetComponent<Rigidbody2D>();
   }

    public void Setup(int direction, int bulletDamage)
    {
        rb.linearVelocity = new Vector2(speed * direction, 0);
        bulletdamage = bulletDamage;
         
    }

    public void OnTriggerEnter2D(Collider2D collision)  
     {
        if (collision.gameObject.CompareTag("Player"))
            {
                return;
            }

        if (collision.gameObject.CompareTag("Enemy"))
            {
                 var enemyHealth = collision.gameObject.GetComponent<Health>();
                enemyHealth.Damage(bulletdamage);

            }
        Destroy (gameObject);
    }
}
}
