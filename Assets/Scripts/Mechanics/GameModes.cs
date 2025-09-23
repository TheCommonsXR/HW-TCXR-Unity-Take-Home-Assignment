using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Platformer.Model;
using Platformer.Core;

namespace Platformer.Mechanics
{
    public class GameModes : MonoBehaviour
    {
        public PlayerController player;
        public bool speedyMode = false;
        public bool hardMode = false;
        public bool easyMode = false;
        public bool customMode = false;

        public int customHP;
        public float customWalkSpeed;
        public float customJumpSpeed;
        public int customBulletDamage;
        public Vector3 customSpawnPosition;

        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        void Start()
        {
            // for faster gameplay
            if (speedyMode)
            {
                player.maxSpeed = 50f;
                player.jumpTakeOffSpeed = 20f;
            }

            if (hardMode) // hard difficulty setting
            {
                player.health.maxHP = 1;
            }
            else if (easyMode) // easy difficulty setting
            {
                player.health.maxHP = 5;
                player.bulletDamage = 10;
            }

            if (customMode) // apply custom values
            {
                player.maxSpeed = customWalkSpeed;
                player.jumpTakeOffSpeed = customJumpSpeed;
                player.health.maxHP = customHP;
                player.bulletDamage = customBulletDamage;
                model.spawnPoint.transform.position = customSpawnPosition;
                player.Teleport(model.spawnPoint.transform.position);
            }
        }
    }   
}
