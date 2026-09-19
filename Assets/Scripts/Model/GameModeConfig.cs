
using UnityEngine;
///<summary>
/// Scriptable object to set different gamemodes from the create menu. each asset made from it is in one game mode, scriptable object is a settings files that can be edited in the inspector
///</summary>

namespace Platformer.Model
{
    [CreateAssetMenu(menuName = "Platformer/Game Mode Config", fileName = "NewGameMode")]
    public class GameModeConfig : ScriptableObject
{
    public int playerMaxHP = 3;
    public Vector2 startPosition;
    public float immunity = 1f;
    public int bulletDamage = 1;
}
}
