using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configurations/GameMode")]
public class GameMode : ScriptableObject {
   // Here are some properties that can be changed by the designer after they create a GameMode ScriptableObject in the inspector.
   public Vector3 SpawnPosition;
   public int PlayerHealth = 1;
   public int PlayerBulletDamage = 1;
   public float PlayerBulletSpeed = 1f;
   public float SimulationSpeed = 1f;
   public float PlayerImmunityDurationWhenDamaged = 1f;
   public float MaxPlayerSpeed = 1f;
   public float PlayerJumpTakeoffSpeed = 1f;
   public float GlobalJumpModifier = 1.5f;
   public float GlobalJumpDeceleration = 0.5f;
}
