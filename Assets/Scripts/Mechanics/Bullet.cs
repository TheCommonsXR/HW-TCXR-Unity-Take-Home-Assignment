using Platformer.Mechanics;
using System.Collections;
using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 3f;
    public int damage = 1;
    public float duration = 3f;

    private Vector2 _direction;
    private Coroutine flightTime;

    private void OnEnable()
    {
        if (flightTime != null)
        {
            StopCoroutine(flightTime);
        }

        flightTime = StartCoroutine(SetInactiveAfterLifetime());
    }

    private void Update()
    {
        transform.Translate(_direction * speed * Time.deltaTime);
    }

    private IEnumerator SetInactiveAfterLifetime()
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }

    public void Shoot(Vector2 dir) => _direction = dir.normalized;

}
