using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{   // Scriptable Object created to store different game modes
    [CreateAssetMenu(fileName = "Gamemodes",menuName ="Platformer/Game modes")]
    public class Gamemodes : ScriptableObject
    {
        public int playerhp = 3;   // player starting health 
        public Vector3 playerPosition;   // player starting position
        public float playerSpeed = 7f;
        public int enemyHp = 1;
    }
}
