using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameMode", menuName = "Game/GameMode")]

public class GameMode : ScriptableObject
{
    public string modeName;
    public int startingHealth = 1;
    public Vector3 spawnPosition = Vector3.zero;
    public float moveSpeed = 5f;
    [Header("Weapons")]
    public bool allowShooting = true;

}
