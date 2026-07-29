using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;
using Platformer.Model;
using Platformer.Core;

public class EnemySpawner : MonoBehaviour
{
    PlatformerModel model = Simulation.GetModel<PlatformerModel>();

    public GameObject enemyPrefab;
    public PatrolPath path;
    public float spawnTimer;
    public Animator anim;
    public bool waitToSpawn = true; // If true, wait until most recent spawn's death to continue spawning
    GameObject currentEnemy;
    bool isSpawning;
    public float spawnRange; // Player must be within distance to spawn
    public bool startingEnemy = true; // If true, instantly spawns first enemy
    public float speed = 7f;
    public int damage = 1;
    public int health = 3;
    // speed, damage, and health

    void Awake()
    {
        anim.SetFloat("SpawnSpeed", 1f / spawnTimer);

        // Instantly spawn starting enemy without waiting for timer or player proximity
        if (startingEnemy)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        // Spawn enemy and give it path
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemy.GetComponent<EnemyController>().path = path;
        enemy.GetComponent<AnimationController>().maxSpeed = speed;
        enemy.GetComponent<EnemyController>().damage = damage;
        enemy.GetComponent<Health>().maxHP = health;

        if (waitToSpawn)
        {
            currentEnemy = enemy;
        }

        isSpawning = false;
    }

    void Update()
    {
        // If player is within spawn range
        if (Mathf.Abs(transform.position.x - model.player.transform.position.x) <= spawnRange)
        {
            // Wait until currentEnemy is dead before respawning
            // Or if not waiting for currentEnemy to die, infinitely spawn enemies
            if (!isSpawning && ((waitToSpawn && (!currentEnemy || !currentEnemy.GetComponent<AnimationController>().enabled)) || !waitToSpawn))
            {
                anim.SetTrigger("StartSpawn");
                isSpawning = true;
            }
        }
    }
}
