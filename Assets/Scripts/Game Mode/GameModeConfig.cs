using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Show on the Assets Tab
[CreateAssetMenu(fileName = "GameModeConfig", menuName = "New Game Mode Config", order = 1)]
public class GameModeConfig : ScriptableObject
{
    public Vector3 playerStartPosition = Vector3.zero;
    public int playerHealth = 10;
    public int playerBulletDamage = 20;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
