using Platformer.Mechanics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    public static GameModeManager Instance;

    [field: SerializeField] public GameMode currentMode;

    [SerializeField] private PlayerController player;
    [SerializeField] private EnemyController[] enemies;

    private GameController _gameController;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _gameController = GetComponent<GameController>();
    }

    private void Start()
    {
        if (currentMode != null)
        {
            ApplyGameMode(currentMode);
        }
    }

    public void ApplyGameMode(GameMode mode)
    {
        ApplyPlayerSettings(mode);
        ApplyEnemySettings(mode);
        // Projectile settings are not created on Awake and are handled by Bullet Pool Manager
    }

    private void ApplyPlayerSettings(GameMode mode)
    {
        player.health.SetNewMaxHP(mode.maxPlayerHealth);
        player.transform.position = mode.startingPosition;

        GameObject spawnPoint = Instantiate(currentMode.respawnPosition, transform);
        _gameController.model.spawnPoint = spawnPoint.transform;
    }

    private void ApplyEnemySettings(GameMode mode)
    {
        foreach (EnemyController enemy in enemies)
        {
            enemy.health.SetNewMaxHP(currentMode.maxEnemyHealth);
            enemy.Damage = currentMode.enemyDamage;
        }
    }

    public void ApplyProjectileSettings(Bullet projectile)
    {
        projectile.speed = currentMode.bulletSpeed;
        projectile.damage = currentMode.bulletDamage;
        projectile.duration = currentMode.bulletDuration;
    }
}
