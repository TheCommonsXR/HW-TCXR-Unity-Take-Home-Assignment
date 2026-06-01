using UnityEngine;

[CreateAssetMenu(
    fileName = "GameModeConfig",
    menuName = "Platformer/Game Mode Config"
)]
public class GameMode : ScriptableObject
{
    public string modeName = "Normal";

    [Header("Player Settings")]
    public int playerMaxHealth = 3;
    public float playerImmunitySeconds = 1f;

    [Header("Weapon Settings")]
    public int bulletDamage = 1;
    public float bulletSpeed = 10f;
}