using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;

public class PlayerConfigLoader : MonoBehaviour
{
    public int currentHealth;

    private Health healthComponent;

    PlatformerModel model = Simulation.GetModel<PlatformerModel>();
    // Start is called before the first frame update
    void Start()
    {
        var player = model.player;
        player.Teleport(model.spawnPoint.transform.position);
        healthComponent = GetComponent<Health>();
        if (healthComponent != null)        
        {
            healthComponent.SetCurrentHP(currentHealth);
            healthComponent.UpdateHealthBar();
        }
    }
}
