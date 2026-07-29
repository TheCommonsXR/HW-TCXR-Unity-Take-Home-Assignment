using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Model;
using Platformer.Core;

public class Checkpoint : MonoBehaviour
{
    PlatformerModel model = Simulation.GetModel<PlatformerModel>();

    public GameObject tree;
    bool isActivated;

    /// <summary>
    /// Set this location as the spawnpoint
    /// </summary>
    public void ActivateCheckpoint()
    {
        // Don't allow resetting old checkpoints
        if (isActivated) return;

        model.spawnPoint = transform;

        // Show tree to indicate spawnpoint set
        tree.SetActive(true);

        isActivated = true;
    }
}