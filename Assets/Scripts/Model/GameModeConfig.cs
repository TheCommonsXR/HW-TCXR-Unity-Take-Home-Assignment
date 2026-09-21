using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Model
{
    [CreateAssetMenu(
        fileName = "NewGameMode",
        menuName = "Platformer/Game Mode")]
    public class GameModeConfig : ScriptableObject
    {
        [Min(1)]
        public int startingHealth = 5;

        public Vector2 startingPosition;
    }
}