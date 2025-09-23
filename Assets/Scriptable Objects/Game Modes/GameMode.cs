using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameMode", menuName = "Game Settings/Game Mode")]
public class GameMode : ScriptableObject
{
    [Header("Player Settings")]
    public int maxPlayerHealth = 5;
    public Vector3 startingPosition = Vector3.zero;
    public GameObject respawnPosition;

    [Header("Projectile Settings")]
    public float bulletSpeed = 3f;
    public int bulletDamage = 1;
    public float bulletDuration = 3f;

    [Header("Enemy Settings")]
    public int maxEnemyHealth = 1;
    public int enemyDamage = 1;
}
