using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scriptable object to easily configure new gamemodes
[CreateAssetMenu(fileName = "Gamemode", menuName = "Gamemode", order = 1)]
public class Gamemode : ScriptableObject
{
    [Tooltip("Player's max health")]
    public int playerHealth;

    [Tooltip("Player's spawn position")]
    public Vector2 playerSpawn;

    [Tooltip("Damage of the player's gun")]
    public int gunDamage;

    [Tooltip("Player's max speed")]
    public int playerSpeed;
    
    [Tooltip("Player's jump strength")]
    public int playerJumpPower;
}
