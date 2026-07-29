using UnityEngine;

[CreateAssetMenu(fileName = "GameMode", menuName = "Scriptable Objects/GameMode")]
public class GameMode : ScriptableObject
{
    public string gameModeName;
    public int startingbulletdamage;
    public int startingPlayerHealth;
    public int damagebyenemy;

    public Vector3 playerStartingPosition;
    
    public Transform startingSpawnPoint;
}
