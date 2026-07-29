using UnityEngine;

[CreateAssetMenu(fileName = "GameMode", menuName = "Game/Game Mode")]
public class GameMode : ScriptableObject
{
    public int playerMaxHealth = 3;
    public Vector2 playerSpawnPosition = Vector2.zero;
}