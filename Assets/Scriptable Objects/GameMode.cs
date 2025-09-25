using UnityEngine;

[CreateAssetMenu(fileName = "GameMode", menuName = "Platformer/Game Mode", order = 1)]
public class GameMode : ScriptableObject
{
    public string modeName;
    public int startingHealth = 3;
    public Vector3 spawnPoint;
    public float jumpModifier = 1.5f;
    public float jumpDeceleration = 0.5f;
    public int gunDamage = 1;
}