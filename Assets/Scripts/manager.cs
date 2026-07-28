using System.Collections;
using System.Collections.Generic;
using Platformer.Mechanics;
using UnityEngine;

public class manager : MonoBehaviour
{
    // Start is called before the first frame update
    public gamemode select;
    public PlayerController player;
    void Start()
    {
        Health playerHealth = player.GetComponent<Health>();
        playerHealth.SetHealth(select.hp);
        player.transform.position = select.playerStartPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
