using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// A designer-authored configuration of game values that defines a "game mode".
    /// Create assets via Assets > Create > Platformer > Game Mode and assign one to a
    /// GameModeController to run it. Adding or tuning a mode requires no code, so the
    /// designer can work entirely inside Unity.
    /// </summary>
    [CreateAssetMenu(fileName = "GameMode", menuName = "Game Mode")]
    public class GameMode : ScriptableObject
    {
        public string modeName = "New Mode";

        [Header("Player")]
        public int playerMaxHealth = 3;
        public float playerMaxSpeed = 7f;
        public int bulletDamage = 1;

        [Header("Spawn")]
        public Vector3 startPosition;

        [Header("Physics")]
        [Tooltip("Global jump modifier applied to the player's initial jump velocity.")]
        public float jumpModifier = 1.5f;
    }
}
