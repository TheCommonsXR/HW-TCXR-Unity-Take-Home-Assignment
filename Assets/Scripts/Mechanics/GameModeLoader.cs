using UnityEngine;
using Platformer.Model;

///<summary>
/// can put this on an empty object in the scene. when the game starts, it copies the chosen mode from gamemode and puts those numbers on the player
/// </summary>

namespace Platformer.Mechanics
{
    public class GameModeLoader : MonoBehaviour
    {
        public GameModeConfig mode;

        void Start()
        {
            if (mode == null) return;
            var model = GameController.Instance.model;
            var player = model.player;

            player.health.maxHP = mode.playerMaxHP;
            player.health.ResetToMax();
            player.immunity = mode.immunity;
            player.bulletDamage = mode.bulletDamage;

            model.spawnPoint.position = mode.startPosition;
            player.Teleport(mode.startPosition);
        }        
    }
}
