using UnityEngine;

[CreateAssetMenu(fileName = "GameMode", menuName = "Game/GameMode")]
public class GameMode : ScriptableObject
{
    public string modeName;
    public int playerStartHealth;
    public Transform playerStartPosition;
    // Add any other tunable parameters you want
}
