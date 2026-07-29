using UnityEngine;
//using TMPro;


    namespace Platformer.Mechanics
{
    public class playershooting : MonoBehaviour
{
    public GameObject bulletPrefab;

    public float bulletSpeed = 10f;
    public SpriteRenderer bullet_sprite;
    public int bulletDamage;
    //[SerializeField] TMP_Text bullettext;
 public void Awake()
    {
        bullet_sprite = GetComponent<SpriteRenderer>();
        //UpdateHealthText();
    }
    public void Start()
    {
         //UpdateHealthText();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Shoot();

        }

    }
public void Shoot()
    {
        Debug.Log("Key Pressed");
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        int direction = 1;
        if (bullet_sprite.flipX)
        {
            direction = -1;
        }
        bullet.GetComponent<Bullet>().Setup(direction, bulletDamage);
    }
/*public void UpdateHealthText()
    {
        bullettext.text = "Bullet Damage: " + bulletDamage.ToString();
    }*/
}
}