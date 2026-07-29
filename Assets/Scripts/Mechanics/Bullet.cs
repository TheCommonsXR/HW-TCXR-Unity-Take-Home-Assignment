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
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.name == "CinemachineConfiner")
            {
                return;
            }
         Debug.Log(" Bullet collision:  " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Enemies"))
            {
                Debug.Log(" Collided with Enemy! ");
                 var enemyHealth = collision.gameObject.GetComponentInParent<Health>();
                 Debug.Log("Dealing " + bulletdamage + " damage.");
                enemyHealth.Damage(bulletdamage);
                Debug.Log(" Destroying enemy");
                Destroy(collision.gameObject);

            }
        Destroy (gameObject);
    }
}
}
