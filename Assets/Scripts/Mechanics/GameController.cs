using Platformer.Core;
using Platformer.Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.CinemachineTriggerAction.ActionSettings;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This class exposes the the game model in the inspector, and ticks the
    /// simulation.
    /// </summary> 
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }

        //This model field is public and can be therefore be modified in the 
        //inspector.
        //The reference actually comes from the InstanceRegister, and is shared
        //through the simulation and events. Unity will deserialize over this
        //shared reference when the scene loads, allowing the model to be
        //conveniently configured inside the inspector.
        public PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public bool TimedMode = false;
        public float MaxTime = 120f; // 2 minutes in seconds
        float minuteTimer = 0f;

        void OnEnable()
        {
            Instance = this;

            if (TimedMode) minuteTimer = MaxTime;
        }

        void OnDisable()
        {
            if (Instance == this) Instance = null;
        }

        void Update()
        {
            if (Instance == this) Simulation.Tick();

            if (TimedMode)
            {
                minuteTimer -= Time.deltaTime;

                if (minuteTimer <= 0)
                {
                    // Handle time running out (e.g., end game, respawn, etc.)
                    minuteTimer = 0; // Ensure timer doesn't go negative
                    //Restart scene
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }
    }
}