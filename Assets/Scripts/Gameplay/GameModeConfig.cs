using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameModeConfig", menuName = "Game Mode")]
public class GameModeConfig : ScriptableObject
{
    // Allows the designer to set the Player and enemies initial health
    // as well as the player's spawn point
    public string modeName = "Default";
    public int playerStartingHealth = 3;
    public int enemyStartingHealth = 3;
    public Vector3 playerSpawnPoint = Vector3.zero;
}
