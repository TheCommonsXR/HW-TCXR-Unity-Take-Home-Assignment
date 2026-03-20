using UnityEngine;
using Platformer.Mechanics;

namespace Platformer.Mechanics
{
    [CreateAssetMenu(fileName = "GameMode", menuName = "Platformer/Game Mode")]
    public class GameMode : ScriptableObject
    {
        public string modeName;
        public int playerHealth = 3;
        public Vector2 playerStartPosition;
        public float playerSpeed = 7f;
    }
}