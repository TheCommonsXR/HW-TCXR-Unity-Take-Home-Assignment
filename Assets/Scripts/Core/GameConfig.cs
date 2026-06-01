using UnityEngine;

/// <summary>
/// Game config settings that can be edited in Unity.
/// </summary>
public class GameConfig : MonoBehaviour
{
    [Header("Player Config")]
    public float playerMaxSpeed = 5f;
    public float playerJumpTakeOffSpeed = 5f;
    public float playerYDamage = 1f;
    public int playerMaxHP = 3;
    public Vector2 playerSpawnPoint = new Vector2(0, 0);
}
