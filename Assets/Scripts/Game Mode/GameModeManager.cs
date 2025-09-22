using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    // Configuration to Use
    public GameModeConfig config;

    public GameObject playerGameObject;
    public GameController controller;

    void Start()
    {
        // Initialize According to current Game Mode Configuration
        if (playerGameObject != null)
        {
            playerGameObject.transform.position = config.playerStartPosition;
            playerGameObject.GetComponent<Health>().maxHP = config.playerHealth;
            playerGameObject.GetComponent<Health>().ResetHP();
            playerGameObject.GetComponent<PlayerController>().bulletDamage = config.playerBulletDamage;

            controller.model.spawnPoint.position = config.playerStartPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
