using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Game Mode", menuName ="Game Mode")]
public class GameMode : ScriptableObject
{
    /// <summary>
    /// Max horizontal speed of the player.
    /// </summary>
    public float maxSpeed;
    /// <summary>
    /// Initial jump velocity at the start of a jump.
    /// </summary>
    public float jumpHeight;
    /// <summary>
    /// Maximum health.
    /// </summary>
    public int maxHealth;
    /// <summary>
    /// Amount of time of invincibility the player has after being hit.
    /// </summary>
    public float invincibilityTime;
    /// <summary>
    /// The player's color.
    /// </summary>
    public Color playerColor;

    /// <summary>
    /// The type of bullet the player shoots when firing a bullet
    /// </summary>
    public GameObject bulletType;
    /// <summary>
    /// The amount of damage a bullet does
    /// </summary>
    public float bulletDamage;

    /// <summary>
    /// the player's point of spawn
    /// </summary>
    public Vector3 spawnPoint;
}
