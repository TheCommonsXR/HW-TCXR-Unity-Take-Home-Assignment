using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift) || Input.GetMouseButtonDown(0))
            Shoot();
    }

    private void Shoot()
    {
        if (_spriteRenderer.flipX)
        {
            _firePoint.localPosition = new Vector3(-Mathf.Abs(_firePoint.localPosition.x), _firePoint.localPosition.y, _firePoint.localPosition.z);
        }
        else
        {
            _firePoint.localPosition = new Vector3(Mathf.Abs(_firePoint.localPosition.x), _firePoint.localPosition.y, _firePoint.localPosition.z);
        }

        Vector2 direction = _spriteRenderer.flipX ? Vector2.left : Vector2.right;
        BulletPoolManager.Instance.FireBullet(_firePoint.position, direction);
    }

}
