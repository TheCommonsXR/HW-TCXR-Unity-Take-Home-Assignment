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
        [SerializeField]
        [Range(0, 1000)] // Set a range to make sure the value doesn't go negative, but allow for larger values
        int damageAmount = 1;

        // Allow other classes to access the damageAmount without modifying it
        public int GetDamageAmount()
        {
            return damageAmount;
        }

        public void SetDamageAmount(int newDamage)
        {
            damageAmount = Mathf.Max(0, newDamage); // Ensure the damage amount is not negative
        }

    }
}
