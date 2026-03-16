using UnityEngine;

namespace Platformer.Model
{
    [CreateAssetMenu(fileName = "GameMode_", menuName = "Platformer/Game Mode Configuration")]
    public class GameModeConfig : ScriptableObject
    {
        [Header("Player")]
        public int playerMaxHealth = 3;
        public int playerStartHealth = 3;

        [Header("Spawn")]
        public Vector3 playerStartPosition = Vector3.zero;
    }
}
