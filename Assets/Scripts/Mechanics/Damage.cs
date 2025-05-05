using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Represents how much damage an entity will deal to another entity.
    /// </summary>
    public class Damage : MonoBehaviour
    {
        [Range(0, 100000)] // Set a range to make sure the value doesn't go negative, but allow for large values
        public int damageAmount = 1;

        // Allow other classes to access the damageAmount without modifying it
        public int GetDamageAmount()
        {
            return damageAmount;
        }
    }
}
