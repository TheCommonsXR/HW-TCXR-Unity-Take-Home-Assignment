using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    public static BulletPoolManager Instance;

    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private int _poolSize = 20;

    private Bullet[] _pool;
    private int _index = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _pool = new Bullet[_poolSize];
        for (int i = 0; i < _poolSize; i++)
        {
            Bullet _bullet = Instantiate(_bulletPrefab, transform);

            _bullet.gameObject.SetActive(false);
            _pool[i] = _bullet;
            GameModeManager.Instance.ApplyProjectileSettings(_bullet);
        }
    }

    public void FireBullet(Vector3 position, Vector2 direction)
    {
        Bullet bullet = _pool[_index];
        _index = (_index + 1) % _poolSize;

        bullet.transform.position = position;
        bullet.gameObject.SetActive(true);
        bullet.Shoot(direction);
    }
}
